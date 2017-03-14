using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace Parental_Advisory
{

    public partial class MainWindow : Form
    {

        private const bool DEBUG = true;
        private const int GRAPH_LINE_WIDTH = 3;
        private const int POINT_CATCH_RADIUS = 8;
        private const int MATRIX_SIZE = 3;
        private const int DEFAULT_THRESHOLDING_VALUE = 4;
        private const int DEFAULT_ITERATIONS = 1;

        private bool graphIsUpdated;
        private Image OriginalImage;
        private SliderDialog slider;

        public string ImageFilename { get; private set; }

        private Graph graph;
        private int caughtPointIndex;
        private Point newCaughtPosition;

        private enum FilterType
        {
            NULL_TRANSFORM, INVERT, BRIGHTEN, CONTRAST, BLUR,
            SHARPEN,
            EDGE_DETECT,
            EMBOSS,
            MEDIAN,
            GAUSSIAN_SMOOTHING,
            THRESHOLDING
        }

        public MainWindow()
        {
            InitializeComponent();
            graph = new Graph(graphPanel);

            graphIsUpdated = false;
            slider = new SliderDialog();

            caughtPointIndex = -1;  //initialize to -1, because nothing is caught.

            if (DEBUG)
            {
                if (File.Exists("temp.bmp"))
                {
                    UpdateImageDisplay(new Bitmap("temp.bmp"));
                    OriginalImage = new Bitmap("temp.bmp");
                }
                else
                {
                    openFileDialog.ShowDialog();
                    File.Copy(ImageFilename, "temp.bmp");
                }
                ImageFilename = "temp.bmp";
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            BringToFront();
            ImageFilename = openFileDialog.FileName;
            OriginalImage = new Bitmap(ImageFilename);
            graph.Reset();
            UpdateImageDisplay(OriginalImage);
        }

        private void UpdateImageDisplay(Image newImage = null)
        {
            if (newImage != null)
                pictureBox.Image = newImage;
            graphPanel.Visible = true;
            resetButton.Visible = true;
            filterPanel.Visible = true;
            graphIsUpdated = false;
            graphPanel.Refresh();
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            if (!graphIsUpdated)
            {
                graphPanel.BackgroundImage = MakeGraph();
                graphIsUpdated = true;
            }
        }

        //creates the image of the graph (with the dashed lines and everything)
        private Image MakeGraph()
        {

            Bitmap bitmap = new Bitmap(graphPanel.Width, graphPanel.Height);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen linePen = new Pen(Color.DarkBlue, GRAPH_LINE_WIDTH);
            Pen newPosPen = new Pen(Color.DarkSlateGray, 2)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };
            Brush pointBrush = new SolidBrush(Color.DarkRed);
            Pen dashedPen = new Pen(Color.DimGray, 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot
            };

            Point[] workingCopies = new Point[graph.CountPoints()];
            graph.MaterialPoints.Values.CopyTo(workingCopies, 0);

            try
            {
                if (caughtPointIndex > 0)
                {
                    workingCopies[caughtPointIndex] = newCaughtPosition;
                    for (int i = 0; i < workingCopies.Length - 1; i++)
                    {
                        for (int j = 0; j < workingCopies.Length - 1; j++)
                        {
                            if (workingCopies[j].X > workingCopies[j + 1].X)
                            {
                                Point temp = new Point(workingCopies[j].X, workingCopies[j].Y);
                                workingCopies[j] = workingCopies[j + 1];
                                workingCopies[j + 1] = temp;
                            }
                        }
                    }
                }

                //the for loop below draws the correct lines between points
                for (int i = 1; i < workingCopies.Length; i++)
                {
                    Point a = workingCopies[i - 1];
                    Point b = workingCopies[i];

                    bool segmentInRange = true;

                    if (a.Y > graphPanel.Height)
                    {
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
                    if (b.Y > graphPanel.Height)
                    {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = BuildLinFunction(abstractA, abstractB);
                        Point abstractZero = new Point((int)(-linFunction.Item1 / linFunction.Item2), 0);
                        Point materialZero = graph.CreateMaterialFromAbstract(abstractZero);

                        if (i == workingCopies.Length - 1)
                            graphics.DrawLine(linePen, graph.EndX, materialZero);
                        graphics.DrawLine(linePen, materialZero, a);
                        graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                    if (b.Y < 0)
                    {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = BuildLinFunction(abstractA, abstractB);

                        Point abstractCeiling = new Point((int)((255.0 - linFunction.Item1) / linFunction.Item2), 255);
                        Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                        graphics.DrawLine(linePen, a, materialCeiling);
                        if (i == workingCopies.Length - 1)
                            graphics.DrawLine(linePen, materialCeiling, graph.End);
                        graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                    if (a.Y < 0)
                    {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = BuildLinFunction(abstractA, abstractB);

                        Point abstractCeiling = new Point((int)((255 - linFunction.Item1) / linFunction.Item2), 255);
                        Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                        if (i == 1)
                            graphics.DrawLine(linePen, graph.StartY, materialCeiling);
                        graphics.DrawLine(linePen, materialCeiling, b);
                        graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                    if (segmentInRange)
                    {
                        graphics.DrawLine(linePen, a, b);
                        graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, b.X - GRAPH_LINE_WIDTH, b.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                }

                if (caughtPointIndex > 0)
                    graphics.DrawEllipse(newPosPen, newCaughtPosition.X - GRAPH_LINE_WIDTH, newCaughtPosition.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);

                var halfHeight = graphPanel.Height / 2;
                var halfWidth = graphPanel.Width / 2;

                Point north = new Point(halfWidth, 0);
                Point south = new Point(halfWidth, graphPanel.Height);
                Point east = new Point(0, halfHeight);
                Point west = new Point(graphPanel.Width, halfHeight);

                graphics.DrawLine(dashedPen, north, south);
                graphics.DrawLine(dashedPen, east, west);
            }
            catch (OverflowException)
            {
                return bitmap;
            }
            linePen.Dispose();
            pointBrush.Dispose();
            graphics.Dispose();
            return bitmap;
        }

        private void ProcessImage(FilterType filter = FilterType.NULL_TRANSFORM, double param1 = 0, double param2 = 0)
        {

            Image newImage;
            if (filter <= FilterType.CONTRAST)
                newImage = ApplyFunction(OriginalImage as Bitmap, filter, param1);
            else if (filter == FilterType.MEDIAN)
                newImage = ApplyMedianFilter(pictureBox.Image as Bitmap);
            else if (filter == FilterType.THRESHOLDING)
                newImage = ApplyThresholding(pictureBox.Image as Bitmap, (int)param1, param2);
            else
                newImage = ApplyConvolution(pictureBox.Image as Bitmap, filter, param1);
            newImage.Save("temp1.bmp");
            UpdateImageDisplay(newImage);
        }

        private Image ApplyThresholding(Bitmap original, int levels, double k)
        {
            Bitmap greyscaleImage = ConvertToGreyscale(original);
            Bitmap newImage = greyscaleImage.Clone() as Bitmap;

            int[] levelValues = new int[levels];
            for (int i = 0; i < levels; i++)
                levelValues[i] = (int)((255.0 / (levels - 1)) * i);

            int[] thresholds = new int[levels - 1];
            for (int i = 0; i < levels - 1; i++)
                thresholds[i] = (int)(levelValues[i] + k*(levelValues[i+1] - levelValues[i]));

            for (int y = 0; y < greyscaleImage.Height; y++)
            {
                for (int x = 0; x < greyscaleImage.Width; x++)
                {
                    int intensity = greyscaleImage.GetPixel(x, y).R;
                    for (int i = 0; i < levels - 1; i++)
                    {
                        if (thresholds[i] >= intensity)
                        {
                            Color newIntensity = Color.FromArgb(levelValues[i], levelValues[i], levelValues[i]);
                            newImage.SetPixel(x, y, newIntensity);
                            break;
                        }
                        else if (levels == 2)
                            newImage.SetPixel(x, y, Color.White);
                    }

                }
            }

            return newImage;
        }

        private Image ApplyMedianFilter(Bitmap original)
        {

            Bitmap newImage = original.Clone() as Bitmap;
            Bitmap greyscaleImage = ConvertToGreyscale(newImage);

            for (int y = 0; y < greyscaleImage.Height; y++)
            {
                for (int x = 0; x < greyscaleImage.Width; x++)
                {

                    List<int> neighbours = new List<int>();

                    for (int matrixY = -MATRIX_SIZE / 2; matrixY <= MATRIX_SIZE / 2; matrixY++)
                    {
                        for (int matrixX = -MATRIX_SIZE / 2; matrixX <= MATRIX_SIZE / 2; matrixX++)
                        {

                            int sourceX = x + matrixX;
                            int sourceY = y + matrixY;

                            if (sourceX < 0)
                                sourceX = 0;

                            if (sourceX >= original.Width)
                                sourceX = original.Width - 1;

                            if (sourceY < 0)
                                sourceY = 0;

                            if (sourceY >= original.Height)
                                sourceY = original.Height - 1;

                            Color source = greyscaleImage.GetPixel(sourceX, sourceY);
                            neighbours.Add(source.R);
                        }
                    }
                    neighbours.Sort();
                    int middle;
                    if (neighbours.Count % 2 != 0)
                        middle = neighbours[neighbours.Count / 2];
                    else
                        middle = (neighbours[neighbours.Count / 2] + neighbours[(neighbours.Count / 2) - 1]) / 2;
                    newImage.SetPixel(x, y, Color.FromArgb(middle, middle, middle));
                }
            }

            return newImage;
        }

        private Bitmap ConvertToGreyscale(Bitmap image)
        {
            Bitmap greyscaleImage = image.Clone() as Bitmap;
            //convert to greyscale:
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color oldColor = image.GetPixel(x, y);
                    int newValue = (oldColor.R + oldColor.G + oldColor.B) / 3;
                    Color newColor = Color.FromArgb(newValue, newValue, newValue);
                    greyscaleImage.SetPixel(x, y, newColor);
                }
            }
            return greyscaleImage;
        }

        Image ApplyFunction(Bitmap original, FilterType filter = FilterType.NULL_TRANSFORM, double filterValue = 0)
        {

            Bitmap newImage = original.Clone() as Bitmap;

            switch (filter)
            {
                case (FilterType.NULL_TRANSFORM):
                    break;

                case (FilterType.INVERT):
                    for (int i = 0; i < graph.CountPoints(); i++)
                    {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MoveMaterialPoint(i, point.X, graphPanel.Height - point.Y);
                    }
                    break;

                case (FilterType.BRIGHTEN):
                    for (int i = 0; i < graph.CountPoints(); i++)
                    {
                        Point point = graph.AbstractPoints.Values[i];
                        graph.MoveAbstractPoint(i, point.X, point.Y + (int)filterValue);
                    }
                    break;

                case (FilterType.CONTRAST):

                    double coefficient = (((double)(filterValue)));

                    for (int i = 0; i < graph.CountPoints(); i++)
                    {
                        Point p = graph.AbstractPoints.Values[i];
                        graph.MoveAbstractPoint(i, p.X, (int)(coefficient * (p.Y - 128) + 128));
                    }
                    break;
            }

            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {

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

        private Image ApplyConvolution(Bitmap original, FilterType filter, double filterValue)
        {

            Bitmap newImage = original.Clone() as Bitmap;

            double[,] matrix = new double[MATRIX_SIZE, MATRIX_SIZE];
            double divisor = 1;
            int iterations = 1;

            switch (filter)
            {
                case (FilterType.BLUR):
                    matrix = new double[,] { { 1, 1, 1 },
                                            { 1, 1, 1 },
                                            { 1, 1, 1 } };
                    divisor = Math.Pow(2, MATRIX_SIZE);
                    break;

                case (FilterType.SHARPEN):
                    matrix = new double[,] { { 0, -1, 0 },
                                            { -1, 5, -1 },
                                            { 0, -1, 0 } };
                    break;

                case (FilterType.EDGE_DETECT):
                    matrix = new double[,] { { -1, -1, -1 },
                                            { -1, 8, -1 },
                                            { -1, -1, -1 } };
                    break;

                case (FilterType.EMBOSS):
                    matrix = new double[,] { { -2, -1, 0 },
                                            { -1, 1, 1 },
                                            { 0, 1, 2 } };
                    break;

                case (FilterType.GAUSSIAN_SMOOTHING):
                    matrix = new double[,] { { 1, 2, 1 },
                                            { 2, 4, 2 },
                                            { 1, 2, 1 } };
                    divisor = 16;
                    break;
            }
            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                for (int j = 0; j < MATRIX_SIZE; j++)
                    matrix[i, j] /= divisor;
            }
            iterations = DEFAULT_ITERATIONS;

            for (int i = 0; i < iterations; i++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {

                        int red = 0;
                        int green = 0;
                        int blue = 0;

                        for (int matrixY = -MATRIX_SIZE / 2; matrixY <= MATRIX_SIZE / 2; matrixY++)
                        {
                            for (int matrixX = -MATRIX_SIZE / 2; matrixX <= MATRIX_SIZE / 2; matrixX++)
                            {
                                int sourceX = x + matrixX;
                                int sourceY = y + matrixY;

                                if (sourceX < 0)
                                    sourceX = 0;

                                if (sourceX >= original.Width)
                                    sourceX = original.Width - 1;

                                if (sourceY < 0)
                                    sourceY = 0;

                                if (sourceY >= original.Height)
                                    sourceY = original.Height - 1;

                                Color source = original.GetPixel(sourceX, sourceY);

                                red += (int)(source.R * matrix[matrixX + MATRIX_SIZE / 2, matrixY + MATRIX_SIZE / 2]);
                                green += (int)(source.G * matrix[matrixX + MATRIX_SIZE / 2, matrixY + MATRIX_SIZE / 2]);
                                blue += (int)(source.B * matrix[matrixX + MATRIX_SIZE / 2, matrixY + MATRIX_SIZE / 2]);
                            }
                        }
                        red = Clamp(red, 0, 255);
                        blue = Clamp(blue, 0, 255);
                        green = Clamp(green, 0, 255);

                        newImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }
                original = newImage;
            }

            return newImage;
        }

        private int ApplyFilterToChannel(int intensity)
        {

            Point a = new Point(), b = new Point();
            for (int i = 1; i < graph.CountPoints(); i++)
            {
                var point = graph.AbstractPoints.Values[i];
                if (point.X >= intensity)
                {
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

        private void resetButton_Click(object sender, EventArgs e)
        {
            graph.Reset();
            UpdateImageDisplay(new Bitmap(ImageFilename));
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            ProcessImage(FilterType.INVERT);
        }

        private void brightnessButton_Click(object sender, EventArgs e)
        {
            slider.ShowDialog();
            if (slider.ValueObtained)
            {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FilterType.BRIGHTEN, value);
            }
        }
        private void contrastButton_Click(object sender, EventArgs e)
        {
            OtherSlider tempSlider = new OtherSlider();
            tempSlider.ShowDialog();
            if (tempSlider.ValueObtained)
            {
                var value = tempSlider.Value;
                ProcessImage(FilterType.CONTRAST, value);
            }
        }

        private void blurButton_Click(object sender, EventArgs e)
        {
            //slider.MoveSliderTo(-255);
            //slider.ShowDialog();
            //if (slider.ValueObtained) {
            //    var value = slider.Value;
            //    slider.Reset();
            //    ProcessImage(FilterType.BLUR, value);
            //}
            ProcessImage(FilterType.BLUR);
        }

        private void sharpenButton_Click(object sender, EventArgs e)
        {
            //slider.MoveSliderTo(-255);
            //slider.ShowDialog();
            //if (slider.ValueObtained) {
            //    var value = slider.Value;
            //    slider.Reset();
            //    ProcessImage(FilterType.SHARPEN, value);
            //}
            ProcessImage(FilterType.SHARPEN);
        }

        private void edgeDetectButton_Click(object sender, EventArgs e)
        {
            //slider.MoveSliderTo(-255);
            //slider.ShowDialog();
            //if(slider.ValueObtained) {
            //    var value = slider.Value;
            //    slider.Reset();
            //    ProcessImage(FilterType.EDGE_DETECT, value);
            //}
            ProcessImage(FilterType.EDGE_DETECT);
        }

        private void embossButton_Click(object sender, EventArgs e)
        {
            //slider.MoveSliderTo(-255);
            //slider.ShowDialog();
            //if(slider.ValueObtained) {
            //    var value = slider.Value;
            //    slider.Reset();
            //    ProcessImage(FilterType.EMBOSS, value);
            //}
            ProcessImage(FilterType.EMBOSS);
        }
        private void medianButton_Click(object sender, EventArgs e)
        {
            ProcessImage(FilterType.MEDIAN);
        }
        private void gaussianButton_Click(object sender, EventArgs e)
        {
            ProcessImage(FilterType.GAUSSIAN_SMOOTHING);
        }
        private void thresholdButton_Click(object sender, EventArgs e)
        {
            OtherSlider tempSlider = new OtherSlider();
            tempSlider.label.Text = "Provide the M value";
            tempSlider.upDown.Minimum = 2;
            tempSlider.upDown.Maximum = 256;
            tempSlider.upDown.Value = 4;
            tempSlider.upDown.DecimalPlaces = 0;
            tempSlider.ShowDialog();
            if (tempSlider.ValueObtained)
            {
                var mValue = tempSlider.Value;
                if (IsPowerOf2(mValue))
                {
                    tempSlider.label.Text = "Provide the T value";
                    tempSlider.upDown.Minimum = 0;
                    tempSlider.upDown.Maximum = 1;
                    tempSlider.upDown.Value = (decimal)0.5;
                    tempSlider.upDown.DecimalPlaces = 2;
                    tempSlider.ShowDialog();
                    var tValue = tempSlider.Value;
                    ProcessImage(FilterType.THRESHOLDING, mValue, tValue);
                }
                else
                    MessageBox.Show("Value should be a power of 2.");
            }
        }


        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        //adding, moving, deleting points:
        #region
        private void graphPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (var point in graph.MaterialPoints.Values)
                {
                    if (AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS))
                    {
                        caughtPointIndex = graph.MaterialPoints.IndexOfValue(point);
                    }
                }
            }
        }

        private void graphPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && caughtPointIndex > 0)
            {
                newCaughtPosition = e.Location;
                graphIsUpdated = false;
                graphPanel.Refresh();
            }
        }

        private void graphPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (caughtPointIndex > 0)
            {
                newCaughtPosition = e.Location;
                graph.MoveMaterialPoint(caughtPointIndex, e.X, e.Y);
                caughtPointIndex = -1;

                bool hasStartPoint = false;
                bool hasEndPoint = false;

                foreach (var point in graph.AbstractPoints.Values)
                {
                    if (point.X == 0) hasStartPoint = true;
                    if (point.X == 255) hasEndPoint = true;
                }

                if (!hasStartPoint)
                    graph.AddMaterialPoint(new Point(0, graphPanel.Height));
                if (!hasEndPoint)
                    graph.AddMaterialPoint(new Point(graphPanel.Width, 0));

                graphPanel.Refresh();
                ProcessImage();
            }
        }

        private void graphPanel_MouseClick(object sender, MouseEventArgs e)
        {

            switch (e.Button)
            {

                case (MouseButtons.Left):
                    if (caughtPointIndex == -1 && graph.CountPoints() < 8)
                    {
                        for (int i = 0; i < graph.CountPoints(); i++)
                        {
                            Point point = graph.MaterialPoints.Values[i];
                            if (AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS) || graph.MaterialPoints.ContainsKey(e.X))
                                return;
                        }
                        graph.AddMaterialPoint(e.Location);
                        ProcessImage();
                    }
                    break;

                case (MouseButtons.Right):
                    for (int i = 0; i < graph.CountPoints(); i++)
                    {
                        Point point = graph.MaterialPoints.Values[i];
                        if (AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS))
                        {
                            graph.RemoveMaterialPoint(point);
                            break;
                        }
                    }
                    bool hasStartPoint = false;
                    bool hasEndPoint = false;

                    foreach (var point in graph.AbstractPoints.Values)
                    {
                        if (point.X == 0)
                            hasStartPoint = true;
                        if (point.X == 255)
                            hasEndPoint = true;
                    }

                    if (!hasStartPoint)
                        graph.AddMaterialPoint(new Point(0, graphPanel.Height));
                    if (!hasEndPoint)
                        graph.AddMaterialPoint(new Point(graphPanel.Width, 0));
                    graphPanel.Refresh();
                    ProcessImage();
                    break;
            }
        }
        #endregion

        //some helpful math functions:
        #region
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;
            return value;
        }
        public Tuple<double, double> BuildLinFunction(Point a, Point b)
        {
            double[] exes = { a.X, b.X };
            double[] whys = { a.Y, b.Y };
            return Fit.Line(exes, whys);
        }
        public bool AreCloseTogether(Point p1, Point p2, int radius)
        {
            //hilarious method, makes clicking "on" a vertex easier.
            int xDistance = p1.X - p2.X;
            int yDistance = p1.Y - p2.Y;
            if (xDistance * xDistance + yDistance * yDistance <= radius)
                return true;
            else
                return false;
        }
        private int EvaluateLinFun(int x, Tuple<double, double> linFunction)
        {
            return (int)(linFunction.Item2 * x + linFunction.Item1);
        }
        private bool IsPowerOf2(double number)
        {
            double log = Math.Log(number, 2);
            double pow = Math.Pow(2, Math.Round(log));
            return pow == number;
        }

        #endregion


    }
}
