using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Parental_Advisory {

    public partial class MainWindow : Form {

        private const bool DEBUG = true;
        private const int GRAPH_LINE_WIDTH = 3;
        private const int POINT_CATCH_RADIUS = 8;

        private bool graphIsUpdated;
        private Image OriginalImage;
        private SliderDialog slider;

        public string ImageFilename { get; private set; }

        private Graph graph;
        private int caughtPointIndex;
        private Point newCaughtPosition;

        private enum FunctionFilters {
            NULL_TRANSFORM, INVERT, BRIGHTEN, CONTRAST
        }

        public MainWindow() {
            InitializeComponent();
            graph = new Graph(graphPanel);

            graphIsUpdated = false;
            slider = new SliderDialog();

            caughtPointIndex = -1;  //initialize to -1, because nothing is caught.

            if(DEBUG) {
                if(File.Exists("temp.bmp")) {
                    UpdateImageDisplay(new Bitmap("temp.bmp"));
                    OriginalImage = new Bitmap("temp.bmp");
                } else {
                    openFileDialog.ShowDialog();
                    File.Copy(ImageFilename, "temp.bmp");
                }
                ImageFilename = "temp.bmp";
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {
            ImageFilename = openFileDialog.FileName;
            OriginalImage = new Bitmap(ImageFilename);
            graph.Reset();
            UpdateImageDisplay(OriginalImage);
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
            Pen newPosPen = new Pen(Color.DarkSlateGray, 2);
            newPosPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Brush pointBrush = new SolidBrush(Color.DarkRed);
            Pen dashedPen = new Pen(Color.DimGray, 1);
            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            Point[] workingCopy = new Point[graph.CountPoints()];
            graph.MaterialPoints.Values.CopyTo(workingCopy, 0);

            if(caughtPointIndex > 0) {
                workingCopy[caughtPointIndex] = newCaughtPosition;
                for(int i = 0; i < workingCopy.Length - 1; i++) {
                    for(int j = 0; j < workingCopy.Length - 1; j++) {
                        if(workingCopy[j].X > workingCopy[j + 1].X) {
                            Point temp = new Point(workingCopy[j].X, workingCopy[j].Y);
                            workingCopy[j] = workingCopy[j + 1];
                            workingCopy[j + 1] = temp;
                        }
                    }
                }
            }

            //the for loop below draws the correct lines between points
            for(int i = 1; i < workingCopy.Length; i++) {
                Point a = workingCopy[i - 1];
                Point b = workingCopy[i];

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
                    graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
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
                    graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                }
                if(b.Y < 0) {
                    segmentInRange = false;

                    Point abstractA = graph.CreateAbstractFromMaterial(a);
                    Point abstractB = graph.CreateAbstractFromMaterial(b);

                    var linFunction = BuildLinFunction(abstractA, abstractB);

                    Point abstractCeiling = new Point((int)((255.0 - linFunction.Item1) / linFunction.Item2), 255);
                    Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                    graphics.DrawLine(linePen, a, materialCeiling);
                    graphics.DrawLine(linePen, materialCeiling, graph.End);
                    graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
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
                    graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                }
                if(segmentInRange) {
                    graphics.DrawLine(linePen, a, b);
                    graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                }
            }

            if(caughtPointIndex > 0)
                graphics.DrawEllipse(newPosPen, newCaughtPosition.X - GRAPH_LINE_WIDTH, newCaughtPosition.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);

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

        private void ProcessImage(FunctionFilters filter = FunctionFilters.NULL_TRANSFORM, int value = 0) {
            var newImage = ApplyFunctionFilter(OriginalImage, filter, value);
            newImage.Save("temp1.bmp");
            UpdateImageDisplay(newImage);
        }

        Image ApplyFunctionFilter(Image bitmap, FunctionFilters filter = FunctionFilters.NULL_TRANSFORM, int filterValue = 0) {

            Bitmap newImage = bitmap.Clone() as Bitmap;

            switch(filter) {
                case (FunctionFilters.NULL_TRANSFORM):
                    break;

                case (FunctionFilters.INVERT):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MoveMaterialPoint(i, point.X, graphPanel.Height - point.Y);
                    }
                    break;

                case (FunctionFilters.BRIGHTEN):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MoveMaterialPoint(i, point.X, point.Y - filterValue);
                    }
                    break;

                case (FunctionFilters.CONTRAST):

                    double coefficient = 1 + ((((double)(filterValue))/255));

                    for(int i = 1; i < graph.CountPoints(); i++) {
                        Point a = graph.MaterialPoints.Values[i-1];
                        Point b = graph.MaterialPoints.Values[i];
                        Point abstractA = graph.Dictionary[a];
                        Point abstractB = graph.Dictionary[b];

                        var linFunction = BuildLinFunction(abstractA, abstractB);
                        Point middle = new Point((abstractB.X - abstractA.X)/2, EvaluateLinFun((abstractB.X - abstractA.X)/2, linFunction));

                        double newSlope = coefficient * linFunction.Item2;
                        double newIntercept = middle.Y - newSlope * middle.X;

                        Point newAA = new Point(abstractA.X, (int)(newSlope * (abstractA.X)  + newIntercept));
                        Point newAB = new Point(abstractB.X, (int)(newSlope * (abstractB.X)  + newIntercept));

                        Point newMA = graph.CreateMaterialFromAbstract(newAA);
                        Point newMB = graph.CreateMaterialFromAbstract(newAB);

                        graph.MoveMaterialPoint(a, newMA.X, newMA.Y);
                        graph.MoveMaterialPoint(b, newMB.X, newMB.Y);
                    }
                    break;
            }

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

        private int EvaluateLinFun(int x, Tuple<double, double> linFunction) {
            return (int)(linFunction.Item2 * x + linFunction.Item1);
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
            int newValue = EvaluateLinFun(intensity, linFunction);

            return Clamp(newValue, 0, 255);
        }

        private void resetButton_Click(object sender, EventArgs e) {
            graph.Reset();
            UpdateImageDisplay(new Bitmap(ImageFilename));
        }

        private void invertButton_Click(object sender, EventArgs e) {
            ProcessImage(FunctionFilters.INVERT);
        }

        private void brightnessButton_Click(object sender, EventArgs e) {
            slider.ShowDialog();
            if(slider.ValueObtained) {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FunctionFilters.BRIGHTEN, value);
            }
        }
        private void contrastButton_Click(object sender, EventArgs e) {
            slider.ShowDialog();
            if(slider.ValueObtained) {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FunctionFilters.CONTRAST, value);
            }
        }

        private void blurButton_Click(object sender, EventArgs e) {

        }

        private void pictureBox_Click(object sender, EventArgs e) {

        }
      
        //adding, moving, deleting points:
        #region
        private void graphPanel_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                foreach(var point in graph.MaterialPoints.Values) {
                    if(AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS)) {
                        caughtPointIndex = graph.MaterialPoints.IndexOfValue(point);
                    }
                }
            }
        }

        private void graphPanel_MouseMove(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left && caughtPointIndex > 0) {
                newCaughtPosition = e.Location;
                graphIsUpdated = false;
                graphPanel.Refresh();
            }
        }

        private void graphPanel_MouseUp(object sender, MouseEventArgs e) {
            if(caughtPointIndex > 0) {
                newCaughtPosition = e.Location;
                graph.MoveMaterialPoint(caughtPointIndex, e.X, e.Y);
                caughtPointIndex = -1;
                graphPanel.Refresh();
                ProcessImage();
            }
        }

        private void graphPanel_MouseClick(object sender, MouseEventArgs e) {

            switch(e.Button) {

                case (MouseButtons.Left):
                    if(caughtPointIndex == -1 && graph.CountPoints() < 8) {
                        for(int i = 0; i < graph.CountPoints(); i++) {
                            Point point = graph.MaterialPoints.Values[i];
                            if(AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS) || graph.MaterialPoints.ContainsKey(e.X))
                                return;
                        }
                        graph.AddMaterialPoint(e.Location);
                        ProcessImage();
                    }
                    break;

                case (MouseButtons.Right):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        if(AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS)) {
                            graph.RemoveMaterialPoint(point);
                            ProcessImage();
                        }
                    }
                    break;
            }
        }
        #endregion

        //some helpful math functions:
        #region
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if(value.CompareTo(min) < 0)
                return min;
            if(value.CompareTo(max) > 0)
                return max;
            return value;
        }
        public Tuple<double, double> BuildLinFunction(Point a, Point b) {
            double[] exes = { a.X, b.X };
            double[] whys = { a.Y, b.Y };
            return Fit.Line(exes, whys);
        }
        public bool AreCloseTogether(Point p1, Point p2, int radius) {
            //hilarious method, makes clicking "on" a vertex easier.
            int xDistance = p1.X - p2.X;
            int yDistance = p1.Y - p2.Y;
            if(xDistance * xDistance + yDistance * yDistance <= radius)
                return true;
            else
                return false;
        }
        #endregion

    }
}
