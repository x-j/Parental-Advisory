using System;
using System.Collections.Generic;
using System.Drawing;

namespace Parental_Advisory {
    class ImageTransformations {

        private const int DEFAULT_ITERATIONS = 1;

        public static Image ApplyUniformQuantization(Bitmap original, int kR, int kG, int kB) {

            Bitmap newImage = original.Clone() as Bitmap;

            //create centers of cuboids:
            int[] rEdges = new int[kR + 1];
            int[] gEdges = new int[kG + 1];
            int[] bEdges = new int[kB + 1];

            for(int i = 0; i < kR + 1; i++)
                rEdges[i] = (255 / kR) * i;

            for(int i = 0; i < kG + 1; i++)
                gEdges[i] = (255 / kG) * i;

            for(int i = 0; i < kB + 1; i++)
                bEdges[i] = (255 / kB) * i;

            int[] rCenters = new int[kR];
            int[] gCenters = new int[kG];
            int[] bCenters = new int[kB];

            for(int i = 0; i < kR; i++)
                rCenters[i] = (rEdges[i] + rEdges[i + 1]) / 2;

            for(int i = 0; i < kG; i++)
                gCenters[i] = (gEdges[i] + gEdges[i + 1]) / 2;

            for(int i = 0; i < kB; i++)
                bCenters[i] = (bEdges[i] + bEdges[i + 1]) / 2;

            List<int[]> cuboidCenters = new List<int[]>(kR * kG * kB);

            for(int i = 0; i < kR; i++) {
                for(int j = 0; j < kG; j++) {
                    for(int k = 0; k < kB; k++) {
                        int[] newPoint = { rCenters[i], gCenters[j], bCenters[k] };
                        cuboidCenters.Add(newPoint);
                    }
                }
            }

            for(int y = 0; y < original.Height; y++) {
                for(int x = 0; x < original.Width; x++) {

                    var pixel = original.GetPixel(x, y);
                    int[] point = { pixel.R, pixel.G, pixel.B };

                    int minDistance = Int32.MaxValue;
                    int[] minPoint = new int[3];
                    foreach(var center in cuboidCenters) {
                        int distance = MathHelpers.CalculateDistance(point, center);
                        if(distance <= minDistance) {
                            minDistance = distance;
                            minPoint = center;
                        }
                    }
                    Color newPixel = Color.FromArgb(minPoint[0], minPoint[1], minPoint[2]);
                    newImage.SetPixel(x, y, newPixel);
                }
            }
            return newImage;
        }

        public static Image ApplyOrderedDithering(Bitmap original, int levels, int n) {

            Bitmap greyscaleImage = ConvertToGreyscale(original);
            Bitmap newImage = original.Clone() as Bitmap;

            int[] levelValues = new int[levels];
            for(int i = 0; i < levels; i++)
                levelValues[i] = (int)((255.0 / (levels - 1)) * i);

            double[,] bayerM = new double[n, n];

            //initialize Bayer matrix:
            if(n == 2)
                bayerM = new double[,]{{0,2},
                                    {3,1}};
            else if(n == 3)
                bayerM = new double[,] {{0,7,3},
                                     {6,5,2},
                                     {4,1,8}};
            else if(n == 4)
                bayerM = new double[,] {{0,8,2,10},
                                     {12,4,14,6},
                                     {3,11,1,9},
                                     {15,7,13,5}};
            else if(n == 6)
                bayerM = new double[,] {{0,28,12,2,30,14},
                                     {24,20,8,26,22,10},
                                     {16,4,32,18,6,34},
                                     {3,31,15,1,29,13},
                                     {27,23,11,25,21,9},
                                     {19,7,35,17,5,33}};


            for(int y = 0; y < original.Height; y++) {
                for(int x = 0; x < original.Width; x++) {

                    int bayerX = x % n;
                    int bayerY = y % n;

                    double bayerValue = bayerM[bayerX, bayerY] / Math.Pow(n, 2);

                    double intensity = greyscaleImage.GetPixel(x, y).R;
                    double i = (intensity / 255);
                    int col = (int)Math.Floor((levels - 1) * i);
                    double re = (levels - 1) * i - col;
                    if(re >= bayerValue && col < levels - 1)
                        col++;

                    newImage.SetPixel(x, y, Color.FromArgb(levelValues[col], levelValues[col], levelValues[col]));
                }
            }
            return newImage;
        }

        public static Image ApplyThresholding(Bitmap original, int levels, double k) {
            Bitmap greyscaleImage = ConvertToGreyscale(original);
            Bitmap newImage = greyscaleImage.Clone() as Bitmap;

            int[] levelValues = new int[levels];
            for(int i = 0; i < levels; i++)
                levelValues[i] = (int)((255.0 / (levels - 1)) * i);

            int[] thresholds = new int[levels - 1];
            for(int i = 0; i < levels - 1; i++)
                thresholds[i] = (int)(levelValues[i] + k * (levelValues[i + 1] - levelValues[i]));

            for(int y = 0; y < greyscaleImage.Height; y++) {
                for(int x = 0; x < greyscaleImage.Width; x++) {
                    int intensity = greyscaleImage.GetPixel(x, y).R;
                    for(int i = 0; i < levels - 1; i++) {
                        if(thresholds[i] >= intensity) {
                            Color newIntensity = Color.FromArgb(levelValues[i], levelValues[i], levelValues[i]);
                            newImage.SetPixel(x, y, newIntensity);
                            break;
                        } else if(levels == 2)
                            newImage.SetPixel(x, y, Color.White);
                    }

                }
            }
            return newImage;
        }

        public static Image ApplyMedianFilter(Bitmap original, int matrix_size) {

            Bitmap newImage = original.Clone() as Bitmap;
            Bitmap greyscaleImage = ConvertToGreyscale(newImage);

            for(int y = 0; y < greyscaleImage.Height; y++) {
                for(int x = 0; x < greyscaleImage.Width; x++) {

                    List<int> neighbours = new List<int>();

                    for(int matrixY = -matrix_size / 2; matrixY <= matrix_size / 2; matrixY++) {
                        for(int matrixX = -matrix_size / 2; matrixX <= matrix_size / 2; matrixX++) {

                            int sourceX = x + matrixX;
                            int sourceY = y + matrixY;

                            if(sourceX < 0)
                                sourceX = 0;

                            if(sourceX >= original.Width)
                                sourceX = original.Width - 1;

                            if(sourceY < 0)
                                sourceY = 0;

                            if(sourceY >= original.Height)
                                sourceY = original.Height - 1;

                            Color source = greyscaleImage.GetPixel(sourceX, sourceY);
                            neighbours.Add(source.R);
                        }
                    }
                    neighbours.Sort();
                    int middle;
                    if(neighbours.Count % 2 != 0)
                        middle = neighbours[neighbours.Count / 2];
                    else
                        middle = (neighbours[neighbours.Count / 2] + neighbours[(neighbours.Count / 2) - 1]) / 2;
                    newImage.SetPixel(x, y, Color.FromArgb(middle, middle, middle));
                }
            }

            return newImage;
        }

        public static Bitmap ConvertToGreyscale(Bitmap image) {
            Bitmap greyscaleImage = image.Clone() as Bitmap;
            //convert to greyscale:
            for(int y = 0; y < image.Height; y++) {
                for(int x = 0; x < image.Width; x++) {
                    Color oldColor = image.GetPixel(x, y);
                    int newValue = (oldColor.R + oldColor.G + oldColor.B) / 3;
                    Color newColor = Color.FromArgb(newValue, newValue, newValue);
                    greyscaleImage.SetPixel(x, y, newColor);
                }
            }
            return greyscaleImage;
        }

        public static Image ApplyFunction(Bitmap original, Graph graph, FilterType filter = FilterType.NULL_TRANSFORM, double filterValue = 0) {

            Bitmap newImage = original.Clone() as Bitmap;

            switch(filter) {
                case (FilterType.NULL_TRANSFORM):
                    break;

                case (FilterType.INVERT):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.MaterialPoints.Values[i];
                        graph.MoveMaterialPoint(i, point.X, graph.Panel.Height - point.Y);
                    }
                    break;

                case (FilterType.BRIGHTEN):
                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point point = graph.AbstractPoints.Values[i];
                        graph.MoveAbstractPoint(i, point.X, point.Y + (int)filterValue);
                    }
                    break;

                case (FilterType.CONTRAST):

                    double coefficient = (((double)(filterValue)));

                    for(int i = 0; i < graph.CountPoints(); i++) {
                        Point p = graph.AbstractPoints.Values[i];
                        graph.MoveAbstractPoint(i, p.X, (int)(coefficient * (p.Y - 128) + 128));
                    }
                    break;
            }

            for(int y = 0; y < newImage.Height; y++) {
                for(int x = 0; x < newImage.Width; x++) {

                    Color oldColor = newImage.GetPixel(x, y);
                    int red = oldColor.R;
                    int green = oldColor.G;
                    int blue = oldColor.B;

                    red = ApplyFilterToChannel(red, graph);
                    green = ApplyFilterToChannel(green, graph);
                    blue = ApplyFilterToChannel(blue, graph);

                    Color newColor = Color.FromArgb(red, green, blue);

                    newImage.SetPixel(x, y, newColor);
                }
            }
            return newImage;
        }

        public static Image ApplyConvolution(Bitmap original, FilterType filter, double filterValue, int matrix_size, int iterations = DEFAULT_ITERATIONS) {

            Bitmap newImage = original.Clone() as Bitmap;

            double[,] matrix = new double[matrix_size, matrix_size];
            double divisor = 1;

            switch(filter) {
                case (FilterType.BLUR):
                    matrix = new double[,] { { 1, 1, 1 },
                                            { 1, 1, 1 },
                                            { 1, 1, 1 } };
                    divisor = Math.Pow(2, matrix_size);
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
            for(int i = 0; i < matrix_size; i++) {
                for(int j = 0; j < matrix_size; j++)
                    matrix[i, j] /= divisor;
            }

            for(int i = 0; i < iterations; i++) {
                for(int y = 0; y < original.Height; y++) {
                    for(int x = 0; x < original.Width; x++) {

                        int red = 0;
                        int green = 0;
                        int blue = 0;

                        for(int matrixY = -matrix_size / 2; matrixY <= matrix_size / 2; matrixY++) {
                            for(int matrixX = -matrix_size / 2; matrixX <= matrix_size / 2; matrixX++) {
                                int sourceX = x + matrixX;
                                int sourceY = y + matrixY;

                                if(sourceX < 0)
                                    sourceX = 0;

                                if(sourceX >= original.Width)
                                    sourceX = original.Width - 1;

                                if(sourceY < 0)
                                    sourceY = 0;

                                if(sourceY >= original.Height)
                                    sourceY = original.Height - 1;

                                Color source = original.GetPixel(sourceX, sourceY);

                                red += (int)(source.R * matrix[matrixX + matrix_size / 2, matrixY + matrix_size / 2]);
                                green += (int)(source.G * matrix[matrixX + matrix_size / 2, matrixY + matrix_size / 2]);
                                blue += (int)(source.B * matrix[matrixX + matrix_size / 2, matrixY + matrix_size / 2]);
                            }
                        }
                        red = MathHelpers.Clamp(red, 0, 255);
                        blue = MathHelpers.Clamp(blue, 0, 255);
                        green = MathHelpers.Clamp(green, 0, 255);

                        newImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }
                original = newImage;
            }

            return newImage;
        }


        public static int ApplyFilterToChannel(int intensity, Graph graph) {

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
            var linFunction = MathHelpers.BuildLinFunction(a, b);
            int newValue = MathHelpers.EvaluateLinFun(intensity, linFunction);

            return MathHelpers.Clamp(newValue, 0, 255);
        }

    }
}
