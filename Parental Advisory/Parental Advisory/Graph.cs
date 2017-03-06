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
        private Dictionary<Point, Point> dictionary;
        public SortedList<int, Point> PanelPoints { get; private set; }
        public SortedList<int, Point> AbstractPoints { get; private set; }
        public int CountPoints() => AbstractPoints.Count;
        public Graph(Panel graphPanel) {

            Panel = graphPanel;
            Start = new Point(0, graphPanel.Height);
            End = Point.Add(Start, new Size(graphPanel.Width, -graphPanel.Height));

            AbstractPoints = new SortedList<int, Point>();
            PanelPoints = new SortedList<int, Point>();
            dictionary = new Dictionary<Point, Point>();

            AddPanelPoint(Start);
            AddPanelPoint(End);
        }

        internal void Reset() {
            PanelPoints.Clear();
            AbstractPoints.Clear();
            dictionary.Clear();
            AddPanelPoint(Start);
            AddPanelPoint(End);
        }

        //PanelPointAdd adds a point from the panel
        internal void AddPanelPoint(Point p) {
            PanelPoints.Add(p.X, p);
            //translate the new point into the abstract 0-255 context
            int abstractX = (int) (((double)p.X / Panel.Width) * 255);
            int abstractY = 255 - (int) (((double)p.Y / Panel.Height) * 255);
            Point abstractP = new Point(abstractX, abstractY);
            AbstractPoints.Add(abstractX, abstractP);

            dictionary.Add(p, abstractP);
        }

        internal void MovePanelPoint(int panelIndex, int newX, int newY) {

            Point p = PanelPoints.Values[panelIndex];
            var abstractP = dictionary[p];
            PanelPoints.Remove(p.X);
            dictionary.Remove(p);
            AbstractPoints.Remove(abstractP.X);

            //move the point on the panel
            p.X = newX;
            p.Y = newY;

            AddPanelPoint(p);
            //move the abstract representation
            //abstractP.X = (p.X / Panel.Width) * 255;
            //abstractP.Y = 255 - (p.Y / Panel.Height) * 255;
        }
    }
}
