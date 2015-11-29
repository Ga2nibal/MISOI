using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic
{
    public class ShapePropertyHelper
    {
        private int[,] image;

        public ShapePropertyHelper(int[,] image)
        {
            this.image = image;
        }

        public bool IsCircle(Shape shape)
        {
            int maxX = 0, minX=image.GetLength(0)-1, maxY=0, minY=image.GetLength(1)-1;
            for(int x = 0; x < image.GetLength(0); x++)
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    if (image[x, y] == shape.Marker)
                    {
                        if (x > maxX)
                            maxX = x;
                        if (x < minX)
                            minX = x;
                        if (y > maxY)
                            maxY = y;
                        if (y < minY)
                            minY = y;
                    }
                }
            double xRad = (maxX - minX)/2;
            double yRad = (maxY - minY)/2;
            if (xRad <= 0 || yRad <= 0 || (Math.Abs(1 - xRad/yRad)) > 0.12)
            {
                MessageBox.Show(String.Format("m:{0}, 1.:   {1}", shape.Marker, (Math.Abs(1 - xRad/yRad).ToString())));
                return false;
            }
            double rad = (xRad + yRad)/2;
            double per = 2*Math.PI*rad;
            double shapePer = (double) shape.GetOutPerimenter();
            double shapeSq = (double) shape.GetSquareWithChildren();
            double sq = Math.PI * rad * rad;
            Console.WriteLine(sq+shapeSq);
            //if ((Math.Abs(1 - per / (shapePer))) > 0.1)
            //    return false;
            if ((Math.Abs(1 - sq / (shapeSq))) > 0.26)
            {
                MessageBox.Show(String.Format("m:{0}, 2.:   {1}", shape.Marker, (Math.Abs(1 - sq / (shapeSq)))));
                return false;
            }
            //if ((Math.Abs(1 - shape.Elongation)) > 0.15)
            //    return false;
            //if (shape.ChildShapes == null || shape.ChildShapes.Count == 0)
            //    return false;
            return true;
        }

        /// <summary>
        ///     Чем меньше, тем больше похож
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public static double GetSimilarityCoefficientToCircle(Shape shape)
        {
            double byRadius = Math.Abs(1 - shape.GetAttitudeheightToWeight());

            double xRad = (shape.MaxX - shape.MinX)/2;
            double yRad = (shape.MaxY - shape.MinY)/2;
             double rad = (xRad + yRad)/2;
            double matnSq = Math.PI*rad*rad;

            double bySquere = Math.Abs(1 - (matnSq/shape.Square));

            return byRadius + bySquere;
        }

        public void CalculateProperties(Shape shape)
        {
            CalculateSquare(shape);
            //CalculateCenterOfMass(shape);
            //CalculatePerimetr(shape);
            //CalculateDensity(shape);
            //CalculateElongation(shape);
        }

        public void FindParents(IEnumerable<Shape> shapes_)
        {
            List<Shape> shapes = shapes_.ToList();
            foreach (Shape shape in shapes)
            {
                int parentMark = FindParentMark(shape);
                if (parentMark > 0)
                {
                    shape.ParentShape = shapes_.First(s => s.Marker == parentMark);
                    if(shape.ParentShape.ChildShapes == null)
                        shape.ParentShape.ChildShapes = new List<Shape>();
                    shape.ParentShape.ChildShapes.Add(shape);
                }
            }
        }

        private void CalculateSquare(Shape shape)
        {
            int square = 0;
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    if (image[i, j] == shape.Marker)
                    {
                        square++;
                    }
                }
            }
            shape.Square = square;
        }

        private void CalculateCenterOfMass(Shape shape)
        {
            int xSum = 0, ySum = 0;
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    if (image[i, j] == shape.Marker)
                    {
                        xSum += i;
                        ySum += j;
                    }
                }
            }
            shape.CenterX = xSum / shape.Square;
            shape.CenterY = ySum / shape.Square;
        }

        private void CalculatePerimetr(Shape shape)
        {
            int parentMark = -1;
            int perimetr = 0;
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    if (image[i, j] == shape.Marker)
                    {
                        if (IsBoundary(i, j))
                        {

                            perimetr++;
                        }
                    }
                }
            }
            shape.Perimetr = perimetr;
        }

        private void CalculateDensity(Shape shape)
        {
            shape.Density = Math.Pow(shape.Perimetr, 2) / shape.Square;
        }

        private void CalculateElongation(Shape shape)
        {
            double moment20 = CalculateMoment(shape,2, 0);
            double moment02 = CalculateMoment(shape,0, 2);
            double moment11 = CalculateMoment(shape,1, 1);
            var root = Math.Sqrt(Math.Pow(moment20 - moment02, 2) + 4 * Math.Pow(moment11, 2));
            shape.Elongation = (moment20 + moment02 + root) / (moment20 + moment02 - root);
        }

        private double CalculateMoment(Shape shape, int i, int j)
        {
            double moment = 0;
            for (int k = 0; k < image.GetLength(0); k++)
            {
                for (int l = 0; l < image.GetLength(1); l++)
                {
                    if (image[k, l] == shape.Marker)
                    {
                        moment += Math.Pow(k - shape.CenterX, i)*Math.Pow(l - shape.CenterY, j);
                    }
                }
            }
            return moment;
        }

        private int FindParentMark(Shape shape)
        {
            if (image[shape.CenterX, shape.CenterY] != shape.Marker)
                return -1;
            int parentId = -1;
            for (int x = shape.CenterX; x > 0; x--)
                if (image[x, shape.CenterY] > 0 && image[x, shape.CenterY] != shape.Marker)
                {
                    parentId = image[x, shape.CenterY];
                    break;
                }
            if (parentId < 0)
                return parentId;
            for (int x = shape.CenterX; x < image.GetLength(0) - 1; x++)
                if (image[x, shape.CenterY] > 0 && image[x, shape.CenterY] != shape.Marker)
                {
                    if (parentId != image[x, shape.CenterY])
                        return -1;
                    else
                        break;
                }
            for (int y = shape.CenterY; y < image.GetLength(1) - 1; y++)
                if (image[shape.CenterX, y] > 0 && image[shape.CenterX, y] != shape.Marker)
                {
                    if (parentId != image[shape.CenterX, y])
                        return -1;
                    else
                        break;
                }
            for (int y = shape.CenterY; y > 0; y--)
                if (image[shape.CenterX, y] > 0 && image[shape.CenterX, y] != shape.Marker)
                {
                    if (parentId != image[shape.CenterX, y])
                        return -1;
                    else
                        break;
                }

            return parentId;
        }

        private bool IsBoundary(int x, int y)
        {
            if (image[x,y] < 0)
            {
                return false;
            }
            if (x > 0 && image[x - 1,y] < 0)
            {
                return true;
            }
            if (x < image.GetLength(0) - 1 && image[x + 1, y] < 0)
            {
                return true;
            }
            if (y > 0 && image[x, y - 1] < 0)
            {
                return true;
            }
            if (y < image.GetLength(1) - 1 && image[x, y + 1] < 0)
            {
                return true;
            }
            return false;
        }
    }
}
