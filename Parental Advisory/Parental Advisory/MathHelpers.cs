using MathNet.Numerics;
using System;
using System.Drawing;

namespace Parental_Advisory {
    class MathHelpers {
        //some helpful math functions:
        #region
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if(value.CompareTo(min) < 0)
                return min;
            if(value.CompareTo(max) > 0)
                return max;
            return value;
        }
        public static Tuple<double, double> BuildLinFunction(Point a, Point b) {
            double[] exes = { a.X, b.X };
            double[] whys = { a.Y, b.Y };
            return Fit.Line(exes, whys);
        }
        public static bool AreCloseTogether(Point p1, Point p2, int radius) {
            //hilarious method, makes clicking "on" a vertex easier.
            int xDistance = p1.X - p2.X;
            int yDistance = p1.Y - p2.Y;
            if(xDistance * xDistance + yDistance * yDistance <= radius)
                return true;
            else
                return false;
        }
        public static int EvaluateLinFun(int x, Tuple<double, double> linFunction) {
            return (int)(linFunction.Item2 * x + linFunction.Item1);
        }
        public static bool IsPowerOf2(double number) {
            double log = Math.Log(number, 2);
            double pow = Math.Pow(2, Math.Round(log));
            return pow == number;
        }

        public static int CalculateDistance(int[] point, int[] center) {

            int deltaX = point[0] - center[0];
            int deltaY = point[1] - center[1];
            int deltaZ = point[2] - center[2];

            return (int)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        #endregion
    }
}
