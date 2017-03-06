using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Parental_Advisory {

    public partial class MainWindow : Form {

        private const bool DEBUG = true;
        private const int GRAPH_LINE_WIDTH = 3;

        private bool graphIsUpdated;
        private Image CurrentImage;
        private SliderDialog slider;

        public string ImageFilename {
            get; private set;
        }

        private Graph graph;

        private enum FunctionFilters {
            NULL_TRANSFORM, INVERT, BRIGHTEN, CONTRAST
        }

        public MainWindow() {
            InitializeComponent();
            graph = new Graph(graphPanel);

            graphIsUpdated = false;
            slider = new SliderDialog();

            if(DEBUG) {
                if(File.Exists("temp.bmp")) {
                    UpdateImageDisplay(new Bitmap("temp.bmp"));
                    CurrentImage = new Bitmap("temp.bmp");
                } else {
                    openFileDialog.ShowDialog();
                    File.Copy(ImageFilename, "temp.bmp");
                }
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {
            ImageFilename = openFileDialog.FileName;
            CurrentImage = new Bitmap(ImageFilename);
            graph.Reset();
            UpdateImageDisplay(CurrentImage);
        }

        private void UpdateImageDisplay(Image newImage = null) {
            if(newImage != null)
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

            Pen linePen = new Pen(Color.DarkBlue, GRAPH_LINE_WIDTH);
            Brush pointBrush = new SolidBrush(Color.DarkRed);
            Pen dashedPen = new Pen(Color.DimGray, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            for(int i = 1; i < graph.CountPoints(); i++) {
                Point a = graph.MaterialPoints.Values[i - 1];
                Point b = graph.MaterialPoints.Values[i];

                bool segmentInRange = true;

                if(a.Y > graphPanel.Height) {
                    segmentInRange = false;

                    Point abstractA = graph.CreateAbstractFromMaterial(a);
                    Point abstractB = graph.CreateAbstractFromMaterial(b);

                    var linFunction = BuildLinFunction(abstractA, abstractB);
                    Point abstractZero = new Point((int)(-linFunction.Item1 / linFunction.Item2), 0);
                    Point materialZero = graph.CreateMaterialFromAbstract(abstractZero);

                    graphics.DrawLine(linePen, graph.Start, materialZero);
                    graphics.DrawLine(linePen, materialZero, b);
                    graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                }
                if(b.Y > graphPanel.Height) {
                    segmentInRange = false;

                    Point abstractA = graph.CreateAbstractFromMaterial(a);
                    Point abstractB = graph.CreateAbstractFromMaterial(b);

                    var linFunction = BuildLinFunction(abstractA, abstractB);
                    Point abstractZero = new Point((int)(-linFunction.Item1 / linFunction.Item2), 0);
                    Point materialZero = graph.CreateMaterialFromAbstract(abstractZero);

                    graphics.DrawLine(linePen, graph.EndX, materialZero);
                    graphics.DrawLine(linePen, materialZero, a);
                    graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                    graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                }
                if(b.Y < 0) {
                    segmentInRange = false;

                    Point abstractA = graph.CreateAbstractFromMaterial(a);
                    Point abstractB = graph.CreateAbstractFromMaterial(b);

                    var linFunction = BuildLinFunction(abstractA, abstractB);

                    Point abstractCeiling = new Point((int)((255 - linFunction.Item1) / linFunction.Item2), 255);
                    Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                    graphics.DrawLine(linePen, a, materialCeiling);
                    graphics.DrawLine(linePen, materialCeiling, graph.End);
                    graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                }
                if(a.Y < 0) {
                    segmentInRange = false;

                    Point abstractA = graph.CreateAbstractFromMaterial(a);
                    Point abstractB = graph.CreateAbstractFromMaterial(b);

                    var linFunction = BuildLinFunction(abstractA, abstractB);

                    Point abstractCeiling = new Point((int)((255 - linFunction.Item1) / linFunction.Item2), 255);
                    Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                    graphics.DrawLine(linePen, graph.StartY, materialCeiling);
                    graphics.DrawLine(linePen, materialCeiling, b);
                    graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                }
                if(segmentInRange) {
                    graphics.DrawLine(linePen, a, b);
                    graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH*2, GRAPH_LINE_WIDTH*2);
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

            switch(filter) {
                case (FunctionFilters.NULL_TRANSFORM):
                    return newImage;

                case (FunctionFilters.INVERT):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MovePanelPoint(i, point.X, graphPanel.Height - point.Y);
                    }
                    break;

                case (FunctionFilters.BRIGHTEN):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MovePanelPoint(i, point.X, point.Y - filterValue);
                    }
                    break;
            }
            graphIsUpdated = false;
            graphPanel.Refresh();

            for(int y = 0; y < newImage.Height; y++) {
                for(int x = 0; x < newImage.Width; x++) {

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
            for(int i = 1; i < graph.CountPoints(); i++) {
                var point = graph.AbstractPoints.Values[i];
                if(point.X >= intensity) {
                    a = graph.AbstractPoints.Values[i - 1];
                    b = graph.AbstractPoints.Values[i];
                    break;
                }
            }
            //linFunction will be a tuple of doubles, first one is q, the second one is p
            //y = px + q
            var linFunction = BuildLinFunction(a, b);
            int newValue = (int)(intensity * linFunction.Item2 + linFunction.Item1);

            return Clamp(newValue, 0, 255);
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

        //some helpful math functions:
        #region
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if(value.CompareTo(min) < 0)
                return min;
            if(value.CompareTo(max) > 0)
                return max;
            return value;
        }
        private Tuple<double, double> BuildLinFunction(Point a, Point b) {
            double[] exes = { a.X, b.X };
            double[] whys = { a.Y, b.Y };
            return Fit.Line(exes, whys);
        }
        #endregion
    }
}
