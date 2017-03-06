using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Parental_Advisory {
    public partial class MainWindow : Form {

        private bool graphIsUpdated;
        private Image CurrentImage;
        private SliderDialog slider;
        private bool debug;

        public string ImageFilename {
            get;
            private set;
        }

        private class Graph {

            public List<Point> Points {
                get;
            }
            public Point Start {
                get;
            }
            public Point End {
                get;
            }
            public List<Tuple<Point, Point>> Segments {
                get;
                private set;
            }

            public Graph(Panel graphPanel) {
                Start = new Point(0, graphPanel.Height);
                End = Point.Add(Start, new Size(graphPanel.Width, -graphPanel.Height));

                Points = new List<Point>();
                Segments = new List<Tuple<Point, Point>>();

                AddPoint(Start);
                AddPoint(End);
            }

            internal void Reset() {
                Points.Clear();
                AddPoint(Start);
                AddPoint(End);
            }

            internal void AddPoint(Point p) {
                Points.Add(p);
                Refresh();
            }

            internal void Refresh() {
                if(Points.Count > 1) {
                    Segments.Clear();
                    for(int i = 1; i < Points.Count; i++) {
                        var pair = new Tuple<Point, Point>(Points[i - 1], Points[i]);
                        Segments.Add(pair);
                    }

                }
            }
        }

        private Graph graph;

        private enum FunctionFilters {
            INVERT, BRIGHTEN, CONTRAST
        }

        public MainWindow() {
            InitializeComponent();
            graph = new Graph(graphPanel);

            graphIsUpdated = false;
            slider = new SliderDialog();

            debug = true;

            if(debug) {
                if(File.Exists("temp.bmp")) {
                    UpdateImageDisplay(new Bitmap("temp.bmp"));
                    CurrentImage = new Bitmap("temp.bmp");
                } else
                    openFileDialog.ShowDialog();
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {
            ImageFilename = openFileDialog.FileName;
            CurrentImage = new Bitmap(ImageFilename);
            graph.Reset();
            UpdateImageDisplay(CurrentImage);
        }

        private void UpdateImageDisplay(Image newImage = null) {
            pictureBox.Image = newImage;
            graphPanel.Visible = true;
            resetButton.Visible = true;
            filterPanel.Visible = true;
            graphIsUpdated = false;
            graph.Refresh();
            graphPanel.Refresh();
        }

        private void loadImageButton_Click(object sender, EventArgs e) {
            openFileDialog.ShowDialog();
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e) {
            if(!graphIsUpdated) {
                graphPanel.BackgroundImage = MakeGraph();
                graphIsUpdated = true;
            }
        }

        //creates the image of the graph (with the dashed lines and everything)
        private Image MakeGraph() {

            Bitmap bitmap = new Bitmap(graphPanel.Width, graphPanel.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen linePen = new Pen(Color.DarkBlue, 2);
            Brush pointBrush = new SolidBrush(Color.DarkRed);
            Pen dashedPen = new Pen(Color.DimGray, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            foreach(var segment in graph.Segments) {
                Point a = segment.Item1;
                Point b = segment.Item2;
                graphics.DrawLine(linePen, a, b);
                graphics.FillEllipse(pointBrush, a.X - 3, a.Y - 3, 6, 6);
                graphics.FillEllipse(pointBrush, b.X - 3, b.Y - 3, 6, 6);
            }

            var halfHeight = graphPanel.Height / 2;
            var halfWidth = graphPanel.Width / 2;

            Point north = new Point(halfWidth, 0);
            Point south = new Point(halfWidth, graphPanel.Height);
            Point east = new Point(0, halfHeight);
            Point west = new Point(graphPanel.Width, halfHeight);

            graphics.DrawLine(dashedPen, north, south);
            graphics.DrawLine(dashedPen, east, west);

            linePen.Dispose();
            pointBrush.Dispose();
            graphics.Dispose();
            return bitmap;

        }

        private void resetButton_Click(object sender, EventArgs e) {
            graph.Reset();
            UpdateImageDisplay();
        }

        private void invertButton_Click(object sender, EventArgs e) {

            ProcessImage(FunctionFilters.INVERT);

        }

        private void ProcessImage(FunctionFilters filter, int value = 0) {

            var newImage = ApplyFilter(CurrentImage, filter, value);
            newImage.Save("temp1.bmp");
            UpdateImageDisplay(newImage);

        }

        Image ApplyFilter(Image bitmap, FunctionFilters filter, int filterValue = 0) {

            Bitmap newImage = bitmap.Clone() as Bitmap;

            switch(filter) {

                case (FunctionFilters.INVERT):
                    for(int i = 0; i < graph.Points.Count; i++) {
                        Point p = new Point(new Size(graph.Points[i]));
                        p.Y = graphPanel.Height - p.Y;
                        graph.Points[i] = p;
                    }
                    break;

                case (FunctionFilters.BRIGHTEN):
                    for(int i = 0; i < graph.Points.Count; i++) {
                        Point p = new Point(new Size(graph.Points[i]));
                        p.Y += filterValue;
                        // p.Y = Math.Min(graphPanel.Height, p.Y);
                        graph.Points[i] = p;
                    }
                    break;
            }
            graph.Refresh();

            List<int> rgbGraphPoints = new List<int>();
            int white = 255 * 3;
            foreach(var point in graph.Points) {
                int rgb = (point.X / graphPanel.Width) * white;
                rgbGraphPoints.Add(rgb);
            }
            List<Tuple<int, int>> rgbSegments = new List<Tuple<int, int>>();
            for(int i = 1; i < rgbGraphPoints.Count; i++)
                rgbSegments.Add(new Tuple<int, int>(rgbGraphPoints[i - 1], rgbGraphPoints[i]));

            for(int y = 0; y < newImage.Height; y++) {
                for(int x = 0; x < newImage.Width; x++) {

                    Color oldColor = newImage.GetPixel(x, y);
                    int red = oldColor.R;
                    int green = oldColor.G;
                    int blue = oldColor.B;

                    int rgb = red + blue + green;
                    int segmentIndex = 0;

                    foreach(var segment in rgbSegments) {
                        if(rgb > segment.Item1 && rgb <= segment.Item2)
                            segmentIndex = rgbSegments.IndexOf(segment);
                    }

                    var segm = graph.Segments[segmentIndex];

                    Point a = segm.Item1;
                    Point b = segm.Item2;

                    //solve a linear equation y = p*x + q

                    double p = ((a.Y - b.Y) / (double)(a.X - b.X));
                    //int p = -1;
                    int q = (int)(a.Y - p * a.X);

                    //calculate the point's "x" coordinate:
                    int xcoord = (int)(rgb / (255.0 * 3.0)) * graphPanel.Width;
                    //calculate the the "new y":
                    int newval = (int)(p * xcoord + q);
                    //get the difference between the current value and the original:
                    int diff = newval - xcoord;
                    //translate the difference into [0-255]:
                    int realDiff = (int)((diff / (double)graphPanel.Width) * 255);

                    red += realDiff;
                    red = Math.Max(0, red);
                    red = Math.Min(255, red);
                    green += realDiff;
                    green = Math.Max(0, green);
                    green = Math.Min(255, green);
                    blue += realDiff;
                    blue = Math.Max(0, blue);
                    blue = Math.Min(255, blue);


                    Color newColor = Color.FromArgb(red, green, blue);

                    newImage.SetPixel(x, y, newColor);
                }
            }

            return newImage;

        }

        private void brightnessButton_Click(object sender, EventArgs e) {
            slider.ShowDialog();
            if(slider.ValueObtained) {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FunctionFilters.BRIGHTEN, value);
            }
        }

        private void pictureBox_Click(object sender, EventArgs e) {

        }
    }
}
