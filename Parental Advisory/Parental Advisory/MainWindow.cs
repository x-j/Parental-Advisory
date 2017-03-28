using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Collections.Generic;

namespace Parental_Advisory {

    public partial class MainWindow : Form {

        private const bool DEBUG = true;
        private const int GRAPH_LINE_WIDTH = 3;
        private const int POINT_CATCH_RADIUS = 8;
        private const int MATRIX_SIZE = 3;
        private const int DEFAULT_THRESHOLDING_VALUE = 4;

        private bool graphIsUpdated;
        private Image OriginalImage;
        private SliderDialog slider;

        public string ImageFilename { get; private set; }

        private Graph graph;
        private int caughtPointIndex;
        private Point newCaughtPosition;

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
            BringToFront();
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
            Pen newPosPen = new Pen(Color.DarkSlateGray, 2) {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };
            Brush pointBrush = new SolidBrush(Color.DarkRed);
            Pen dashedPen = new Pen(Color.DimGray, 1) {
                DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot
            };

            Point[] workingCopies = new Point[graph.CountPoints()];
            graph.MaterialPoints.Values.CopyTo(workingCopies, 0);

            try {
                if(caughtPointIndex > 0) {
                    workingCopies[caughtPointIndex] = newCaughtPosition;
                    for(int i = 0; i < workingCopies.Length - 1; i++) {
                        for(int j = 0; j < workingCopies.Length - 1; j++) {
                            if(workingCopies[j].X > workingCopies[j + 1].X) {
                                Point temp = new Point(workingCopies[j].X, workingCopies[j].Y);
                                workingCopies[j] = workingCopies[j + 1];
                                workingCopies[j + 1] = temp;
                            }
                        }
                    }
                }

                //the for loop below draws the correct lines between points
                for(int i = 1; i < workingCopies.Length; i++) {
                    Point a = workingCopies[i - 1];
                    Point b = workingCopies[i];

                    bool segmentInRange = true;

                    if(a.Y > graphPanel.Height) {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = MathHelpers.BuildLinFunction(abstractA, abstractB);
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

                        var linFunction = MathHelpers.BuildLinFunction(abstractA, abstractB);
                        Point abstractZero = new Point((int)(-linFunction.Item1 / linFunction.Item2), 0);
                        Point materialZero = graph.CreateMaterialFromAbstract(abstractZero);

                        if(i == workingCopies.Length - 1)
                            graphics.DrawLine(linePen, graph.EndX, materialZero);
                        graphics.DrawLine(linePen, materialZero, a);
                        graphics.FillEllipse(pointBrush, materialZero.X - GRAPH_LINE_WIDTH, materialZero.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                    if(b.Y < 0) {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = MathHelpers.BuildLinFunction(abstractA, abstractB);

                        Point abstractCeiling = new Point((int)((255.0 - linFunction.Item1) / linFunction.Item2), 255);
                        Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                        graphics.DrawLine(linePen, a, materialCeiling);
                        if(i == workingCopies.Length - 1)
                            graphics.DrawLine(linePen, materialCeiling, graph.End);
                        graphics.FillEllipse(pointBrush, materialCeiling.X - GRAPH_LINE_WIDTH, materialCeiling.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                        graphics.FillEllipse(pointBrush, a.X - GRAPH_LINE_WIDTH, a.Y - GRAPH_LINE_WIDTH, GRAPH_LINE_WIDTH * 2, GRAPH_LINE_WIDTH * 2);
                    }
                    if(a.Y < 0) {
                        segmentInRange = false;

                        Point abstractA = graph.CreateAbstractFromMaterial(a);
                        Point abstractB = graph.CreateAbstractFromMaterial(b);

                        var linFunction = MathHelpers.BuildLinFunction(abstractA, abstractB);

                        Point abstractCeiling = new Point((int)((255 - linFunction.Item1) / linFunction.Item2), 255);
                        Point materialCeiling = graph.CreateMaterialFromAbstract(abstractCeiling);

                        if(i == 1)
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
            } catch(OverflowException) {
                return bitmap;
            }
            linePen.Dispose();
            pointBrush.Dispose();
            graphics.Dispose();
            return bitmap;
        }

        private void ProcessImage(FilterType filter = FilterType.NULL_TRANSFORM, double param1 = 0, double param2 = 0, double param3 = 0) {

            Image newImage = null;
            Bitmap currentImage = pictureBox.Image as Bitmap;
            if(filter <= FilterType.CONTRAST)
                newImage = ImageTransformations.ApplyFunction(OriginalImage as Bitmap, graph, filter, param1);
            else if(FilterType.BLUR <= filter && filter <= FilterType.GAUSSIAN_SMOOTHING)
                newImage = ImageTransformations.ApplyConvolution(currentImage, filter, param1,MATRIX_SIZE);
            else if(filter == FilterType.MEDIAN)
                newImage = ImageTransformations.ApplyMedianFilter(currentImage, MATRIX_SIZE);
            else if(filter == FilterType.THRESHOLDING)
                newImage = ImageTransformations.ApplyThresholding(currentImage, (int)param1, param2);
            else if(filter == FilterType.ORDERED_DITHERING)
                newImage = ImageTransformations.ApplyOrderedDithering(currentImage, (int)param1, (int)param2);
            else if(filter == FilterType.UNIFORM_QUANTIZATION)
                newImage = ImageTransformations.ApplyUniformQuantization(currentImage, (int)param1, (int)param2, (int)param3);
            newImage.Save("temp1.bmp");
            UpdateImageDisplay(newImage);
        }

        private void resetButton_Click(object sender, EventArgs e) {
            graph.Reset();
            UpdateImageDisplay(new Bitmap(ImageFilename));
        }

        private void invertButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.INVERT);
        }

        private void brightnessButton_Click(object sender, EventArgs e) {
            slider.ShowDialog();
            if(slider.ValueObtained) {
                var value = slider.Value;
                slider.Reset();
                ProcessImage(FilterType.BRIGHTEN, value);
            }
        }
        private void contrastButton_Click(object sender, EventArgs e) {
            OtherSlider tempSlider = new OtherSlider();
            tempSlider.ShowDialog();
            if(tempSlider.ValueObtained) {
                var value = tempSlider.Value;
                ProcessImage(FilterType.CONTRAST, value);
            }
        }

        private void blurButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.BLUR);
        }

        private void sharpenButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.SHARPEN);
        }

        private void edgeDetectButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.EDGE_DETECT);
        }

        private void embossButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.EMBOSS);
        }
        private void medianButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.MEDIAN);
        }
        private void gaussianButton_Click(object sender, EventArgs e) {
            ProcessImage(FilterType.GAUSSIAN_SMOOTHING);
        }
        private void thresholdButton_Click(object sender, EventArgs e) {
            OtherSlider tempSlider = new OtherSlider();
            tempSlider.label.Text = "Provide the M value";
            tempSlider.upDown.Minimum = 2;
            tempSlider.upDown.Maximum = 256;
            tempSlider.upDown.Value = 4;
            tempSlider.upDown.DecimalPlaces = 0;
            tempSlider.ShowDialog();
            if(tempSlider.ValueObtained) {
                var mValue = tempSlider.Value;
                if(MathHelpers.IsPowerOf2(mValue)) {
                    tempSlider.label.Text = "Provide the T value";
                    tempSlider.upDown.Minimum = 0;
                    tempSlider.upDown.Maximum = 1;
                    tempSlider.upDown.Value = (decimal)0.5;
                    tempSlider.upDown.DecimalPlaces = 2;
                    tempSlider.ShowDialog();
                    var tValue = tempSlider.Value;
                    ProcessImage(FilterType.THRESHOLDING, mValue, tValue);
                } else
                    MessageBox.Show("Value should be a power of 2.");
            }
        }
        private void orderedDitheringButton_Click(object sender, EventArgs e) {
            OtherSlider tempSlider = new OtherSlider();
            tempSlider.label.Text = "Provide the k value (number of colours)";
            tempSlider.upDown.Minimum = 2;
            tempSlider.upDown.Maximum = 256;
            tempSlider.upDown.Value = 4;
            tempSlider.upDown.DecimalPlaces = 0;
            tempSlider.ShowDialog();
            if(tempSlider.ValueObtained) {
                var kValue = tempSlider.Value;
                if(MathHelpers.IsPowerOf2(kValue)) {
                    tempSlider.label.Text = "Provide the N value (size of lookup matrix)";
                    tempSlider.upDown.Minimum = 2;
                    tempSlider.Value = 2;
                    tempSlider.upDown.Maximum = 6;
                    tempSlider.ShowDialog();
                    var tValue = tempSlider.Value;
                    ProcessImage(FilterType.ORDERED_DITHERING, kValue, tValue);
                } else
                    MessageBox.Show("Value should be a power of 2.");
            }
        }

        private void uquantizationButton_Click(object sender, EventArgs e) {
            QuantizationControl qc = new QuantizationControl();
            qc.ShowDialog();
            if(qc.ValueObtained) {
                double kR = (double)qc.RupDown.Value;
                double kG = (double)qc.GupDown.Value;
                double kB = (double)qc.BupDown.Value;
                ProcessImage(FilterType.UNIFORM_QUANTIZATION, kR, kG, kB);
            }
        }



        private void pictureBox_Click(object sender, EventArgs e) {

        }

        //adding, moving, deleting points:
        #region
        private void graphPanel_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                foreach(var point in graph.MaterialPoints.Values) {
                    if(MathHelpers.AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS)) {
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

                bool hasStartPoint = false;
                bool hasEndPoint = false;

                foreach(var point in graph.AbstractPoints.Values) {
                    if(point.X == 0)
                        hasStartPoint = true;
                    if(point.X == 255)
                        hasEndPoint = true;
                }

                if(!hasStartPoint)
                    graph.AddMaterialPoint(new Point(0, graphPanel.Height));
                if(!hasEndPoint)
                    graph.AddMaterialPoint(new Point(graphPanel.Width, 0));

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
                            if(MathHelpers.AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS) || graph.MaterialPoints.ContainsKey(e.X))
                                return;
                        }
                        graph.AddMaterialPoint(e.Location);
                        ProcessImage();
                    }
                    break;

                case (MouseButtons.Right):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        if(MathHelpers.AreCloseTogether(point, e.Location, POINT_CATCH_RADIUS)) {
                            graph.RemoveMaterialPoint(point);
                            break;
                        }
                    }
                    bool hasStartPoint = false;
                    bool hasEndPoint = false;

                    foreach(var point in graph.AbstractPoints.Values) {
                        if(point.X == 0)
                            hasStartPoint = true;
                        if(point.X == 255)
                            hasEndPoint = true;
                    }

                    if(!hasStartPoint)
                        graph.AddMaterialPoint(new Point(0, graphPanel.Height));
                    if(!hasEndPoint)
                        graph.AddMaterialPoint(new Point(graphPanel.Width, 0));
                    graphPanel.Refresh();
                    ProcessImage();
                    break;
            }
        }
        #endregion

        
    }
}
