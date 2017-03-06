using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Parental_Advisory {
    class Graph {

        //Start and End are start- and endpoints in the context of the Panel
        public Panel Panel { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point EndX { get; private set; }
        public Point StartY { get; private set; }
        private Dictionary<Point, Point> dictionary;
        public SortedList<int, Point> MaterialPoints { get; private set; }
        public SortedList<int, Point> AbstractPoints { get; private set; }
        public int CountPoints() => AbstractPoints.Count;

        public Graph(Panel graphPanel) {

            Panel = graphPanel;
            Start = new Point(0, graphPanel.Height);
            End = Point.Add(Start, new Size(graphPanel.Width, -graphPanel.Height));
            EndX = new Point(graphPanel.Width, graphPanel.Height);
            StartY = new Point(0, 0);

            AbstractPoints = new SortedList<int, Point>();
            MaterialPoints = new SortedList<int, Point>();
            dictionary = new Dictionary<Point, Point>();

            AddPanelPoint(Start);
            AddPanelPoint(End);
        }

        internal void Reset() {
            MaterialPoints.Clear();
            AbstractPoints.Clear();
            dictionary.Clear();
            AddPanelPoint(Start);
            AddPanelPoint(End);
        }

        //PanelPointAdd adds a point from the panel
        internal void AddPanelPoint(Point p) {
            MaterialPoints.Add(p.X, p);

            //translate the new point into the abstract 0-255 context
            Point abstractP = CreateAbstractFromMaterial(p);
            AbstractPoints.Add(abstractP.X, abstractP);

            dictionary.Add(p, abstractP);
        }

        internal void MovePanelPoint(int panelIndex, int newX, int newY) {

            Point p = MaterialPoints.Values[panelIndex];
            var abstractP = dictionary[p];
            MaterialPoints.Remove(p.X);
            dictionary.Remove(p);
            AbstractPoints.Remove(abstractP.X);

            //move the point on the panel
            p.X = newX;
            p.Y = newY;

            AddPanelPoint(p);
        }

        public Point CreateAbstractFromMaterial(Point p) {
            int abstractX = (int)(((double)p.X / Panel.Width) * 255);
            int abstractY = (int)(((double)(255 - p.Y) / Panel.Height) * 255);
            return new Point(abstractX, abstractY);
        }

        public Point CreateMaterialFromAbstract(Point p) {
            int materialX = (int)(((double)p.X / 255) * Panel.Width);
            int materialY = -(int)(((double)p.Y / 255) * Panel.Height) + Panel.Height;
            return new Point(materialX, materialY);
        }
    }
}
