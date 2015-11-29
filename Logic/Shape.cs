using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Logic
{
    public class Shape
    {
        public Shape()
        {
            Points = new List<Point>();
            MaxX = int.MinValue;
            MinX = int.MaxValue;
            MaxY = int.MinValue;
            MinY = int.MaxValue;
        }

        public int Marker { get; set; }
        public int Square { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Perimetr { get; set; }
        public double Density { get; set; }
        public double Elongation { get; set; }

        public double Compact { get; set; }

        public double OrientationOfTheMainAxis { get; set; }

        public Shape ParentShape;

        public List<Shape> ChildShapes { get; set; }

        public int MaxX { get; private set; }
        public int MinX { get; private set; }
        public int MaxY { get; private set; }
        public int MinY { get; private set; }

        public int GetSquareWithChildren()
        {
            int res = Square;
            if (ChildShapes == null)
                return res;
            res += ChildShapes.Sum(childShape => childShape.GetSquareWithChildren());
            return res;
        }

        public int GetOutPerimenter()
        {
            int res = Perimetr;
            if (ChildShapes == null)
                return res;
            res -= ChildShapes.Sum(c => c.Perimetr);
            return res;
        }

        public void AddPoint(Point newPoint)
        {
            Points.Add(newPoint);
            if (newPoint.X > MaxX)
                MaxX = newPoint.X;
            if (newPoint.X < MinX)
                MinX = newPoint.X;
            if (newPoint.Y > MaxY)
                MaxY = newPoint.Y;
            if (newPoint.Y < MinY)
                MinY = newPoint.Y;
        }

        public double GetAttitudeheightToWeight()
        {
            return ((double)(MaxY-MinY))/(MaxX - MinX);
        }

        public List<Point> Points { get; private set; }

        //public void FillCircleShape()
        //{
        //    Points = Points.AsParallel().OrderBy((s) => s.X).ThenBy(s => s.Y).ToList();
        //    Dictionary<int, Tuple<int, int>> rows = new Dictionary<int, Tuple<int, int>>();
        //    for (int i = MinX; i <= MaxX; i++)
        //    {
        //        rows.Add(i, new Tuple<int, int>(int.MaxValue, int.MinValue));
        //    }
        //    List<Point> newPoints = new List<Point>();
        //    int x = -1;
        //    int minY = int.MaxValue, maxY = 
        //    foreach (Point point in Points)
        //    {
                
        //    }
        //}
    }
}
