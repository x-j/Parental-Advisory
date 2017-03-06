using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Parental_Advisory {
    public partial class MainWindow : Form {

        private bool graphIsUpdated;
        private Image CurrentImage;
        private SliderDialog slider;
        private bool debug;

        public string ImageFilename { get; private set; }

        private Graph graph;

        private enum FunctionFilters { NULL_TRANSFORM, INVERT, BRIGHTEN, CONTRAST }

        public MainWindow() {
            InitializeComponent();
            graph = new Graph(graphPanel);

            graphIsUpdated = false;
            slider = new SliderDialog();

            debug = true;

            if (debug) {
                if (File.Exists("temp.bmp")) {
                    UpdateImageDisplay(new Bitmap("temp.bmp"));
                    CurrentImage = new Bitmap("temp.bmp");
                } else
                    openFileDialog.ShowDialog();
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            ImageFilename = openFileDialog.FileName;
            CurrentImage = new Bitmap(ImageFilename);
            graph.Reset();
            UpdateImageDisplay(CurrentImage);
        }

        private void UpdateImageDisplay(Image newImage = null) {
            if (newImage != null)
                pictureBox.Image = newImage;
            graphPanel.Visible = true;
            resetButton.Visible = true;
            filterPanel.Visible = true;
            graphIsUpdated = false;
            graphPanel.Refresh();
        }

        private void loadImageButton_Click(object sender, EventArgs e) {
            openFileDialog.ShowDialog();
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e) {
            if (!graphIsUpdated) {
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

            for (int i = 1; i < graph.CountPoints(); i++) {
                Point a = graph.PanelPoints.Values[i - 1];
                Point b = graph.PanelPoints.Values[i];

                bool segmentInRange = true;

                if (a.Y > graphPanel.Height) {
                    segmentInRange = false;

                    double[] exes = { a.X, b.X };
                    double[] whys = { a.Y, b.Y };
                    var linFunction = Fit.Line(exes, whys);
                    Point zero = new Point((int)(-linFunction.Item1 / linFunction.Item2), 0);

                    graphics.DrawLine(linePen, graph.Start, zero);
                    graphics.DrawLine(linePen, zero, b);
                    graphics.FillEllipse(pointBrush, zero.X - 3, zero.Y - 3, 6, 6);
                    graphics.FillEllipse(pointBrush, b.X - 3, b.Y - 3, 6, 6);
                }
                if (b.Y < 0) {
                    segmentInRange = false;

                    double[] exes = { a.X, b.X };
                    double[] whys = { a.Y, b.Y };
                    var linFunction = Fit.Line(exes, whys);
                    Point ceiling = new Point((int)((255 - linFunction.Item1) / linFunction.Item2), 0);

                    graphics.DrawLine(linePen, a, ceiling);
                    graphics.DrawLine(linePen, ceiling, graph.End);
                    graphics.FillEllipse(pointBrush, ceiling.X - 3, ceiling.Y - 3, 6, 6);
                    graphics.FillEllipse(pointBrush, b.X - 3, b.Y - 3, 6, 6);
                }
                if (segmentInRange) {
                    graphics.DrawLine(linePen, a, b);
                    graphics.FillEllipse(pointBrush, a.X - 3, a.Y - 3, 6, 6);
                    graphics.FillEllipse(pointBrush, b.X - 3, b.Y - 3, 6, 6);
                }
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
            UpdateImageDisplay(CurrentImage);
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

            switch (filter) {
                case (FunctionFilters.NULL_TRANSFORM):
                    return newImage;

                case (FunctionFilters.INVERT):
                    for (int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.PanelPoints.Values[i];
                        graph.MovePanelPoint(i, point.X, graphPanel.Height - point.Y);
                    }
                    break;

                case (FunctionFilters.BRIGHTEN):
                    for (int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.PanelPoints.Values[i];
                        graph.MovePanelPoint(i, point.X, point.Y - filterValue);
                    }
                    break;
            }
            graphIsUpdated = false;
            graphPanel.Refresh();

            for (int y = 0; y < newImage.Height; y++) {
                for (int x = 0; x < newImage.Width; x++) {

                    Color oldColor = newImage.GetPixel(x, y);
                    int red = oldColor.R;
                    int green = oldColor.G;
                    int blue = oldColor.B;

                    red = ApplyFilterToChannel(red);
                    green = ApplyFilterToChannel(green);
                    blue = ApplyFilterToChannel(blue);

                    Color newColor = Color.FromArgb(red, green, blue);

                    newImage.SetPixel(x, y, newColor);
                }
            }
            return newImage;
        }

        private int ApplyFilterToChannel(int intensity) {

            Point a = new Point(), b = new Point();
            for (int i = 1; i < graph.CountPoints(); i++) {
                var point = graph.AbstractPoints.Values[i];
                if (point.X >= intensity) {
                    a = graph.AbstractPoints.Values[i - 1];
                    b = graph.AbstractPoints.Values[i];
                    break;
                }
            }
            double[] exes = { a.X, b.X };
            double[] whys = { a.Y, b.Y };

            //linFunction will be a tuple of doubles, first one is q, the second one is p
            //y = px + q
            var linFunction = Fit.Line(exes, whys);
            int newValue = (int)(intensity * linFunction.Item2 + linFunction.Item1);

            return Clamp(newValue, 0, 255);
        }

        private void brightnessButton_Click(object sender, EventArgs e) {
            slider.ShowDialog();
            if (slider.ValueObtained) {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FunctionFilters.BRIGHTEN, value);
            }
        }

        private void pictureBox_Click(object sender, EventArgs e) {

        }
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;
            return value;
        }

    }
}
