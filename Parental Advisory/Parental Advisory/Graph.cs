using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Parental_Advisory {
    class Graph {

        //Start and End are start- and endpoints in the context of the panel
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point EndX { get; private set; }
        public Point StartY { get; private set; }
        public Dictionary<Point, Point> Dictionary { get; private set; }
        public SortedList<int, Point> MaterialPoints { get; private set; }
        public SortedList<int, Point> AbstractPoints { get; private set; }
        public Panel Panel { get; private set; }

        public int CountPoints() => AbstractPoints.Count;

        public Graph(Panel graphpanel) {

            Panel = graphpanel;
            Start = new Point(0, graphpanel.Height);
            End = Point.Add(Start, new Size(graphpanel.Width, -graphpanel.Height));
            EndX = new Point(graphpanel.Width, graphpanel.Height);
            StartY = new Point(0, 0);

            AbstractPoints = new SortedList<int, Point>();
            MaterialPoints = new SortedList<int, Point>();
            Dictionary = new Dictionary<Point, Point>();

            AddMaterialPoint(Start);
            AddMaterialPoint(End);
        }

        internal void Reset() {
            MaterialPoints.Clear();
            AbstractPoints.Clear();
            Dictionary.Clear();
            AddMaterialPoint(Start);
            AddMaterialPoint(End);
        }

        //panelPointAdd adds a point from the panel
        internal void AddMaterialPoint(Point p) {
            try {
                MaterialPoints.Add(p.X, p);

                //translate the new point into the abstract 0-255 context
                Point abstractP = CreateAbstractFromMaterial(p);
                AbstractPoints.Add(abstractP.X, abstractP);

                Dictionary.Add(p, abstractP);
            } catch (ArgumentException) {
                Random r = new Random();
                int v = r.Next(0, 100);
                p.X += (v > 50 ? 1 : -1);
                MaterialPoints.Add(p.X, p);

                //translate the new point into the abstract 0-255 context
                Point abstractP = CreateAbstractFromMaterial(p);
                AbstractPoints.Add(abstractP.X, abstractP);

                Dictionary.Add(p, abstractP);
            }
        }

        internal void MoveMaterialPoint(int panelIndex, int newX, int newY) {

            Point p = MaterialPoints.Values[panelIndex];
            var abstractP = Dictionary[p];
            MaterialPoints.Remove(p.X);
            Dictionary.Remove(p);
            AbstractPoints.Remove(abstractP.X);

            //move the point on the panel
            p.X = newX;
            p.Y = newY;

            AddMaterialPoint(p);
        }

        internal void MoveMaterialPoint(Point? caughtPoint, int newX, int newY) {

            int index = MaterialPoints.IndexOfValue((Point)caughtPoint);
            Point p = MaterialPoints.Values[index];

            var abstractP = Dictionary[p];
            MaterialPoints.Remove(p.X);
            Dictionary.Remove(p);
            AbstractPoints.Remove(abstractP.X);

            //move the point on the panel
            p.X = newX;
            p.Y = newY;

            AddMaterialPoint(p);
        }
        internal void MoveAbstractPoint(int i, int newX, int newY) {
            Point p = AbstractPoints.Values[i];
            Point materialP = new Point();

            AbstractPoints.Remove(p.X);

            foreach (var point in Dictionary.Keys) {
                if (Dictionary[point].Equals(p))
                    materialP = point;
            }
            Dictionary.Remove(materialP);
            MaterialPoints.Remove(materialP.X);

            p.X = newX;
            p.Y = newY;

            materialP = CreateMaterialFromAbstract(p);

            AddMaterialPoint(materialP);
        }


        public Point CreateAbstractFromMaterial(Point p) {
            int abstractX = (int)(((double)p.X / Panel.Width) * 255);
            int abstractY = (int)(((double)(Panel.Height - p.Y) / Panel.Height) * 255);
            return new Point(abstractX, abstractY);
        }

        public Point CreateMaterialFromAbstract(Point p) {
            int materialX = (int)(((double)p.X / 255) * Panel.Width);
            int materialY = -(int)(((double)p.Y / 255) * Panel.Height) + Panel.Height;
            return new Point(materialX, materialY);
        }

        internal void RemoveMaterialPoint(Point p) {
            var abstractP = Dictionary[p];
            MaterialPoints.Remove(p.X);
            Dictionary.Remove(p);
            AbstractPoints.Remove(abstractP.X);
        }


    }
}
