using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Logic
{
    public class Shape
    {
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

        public List<Point> Points = new List<Point>();
    }
}
