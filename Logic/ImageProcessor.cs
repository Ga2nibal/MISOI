using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class ImageProcessor
    {
        private Bitmap _previousBitmap;
        private bool _imageChanged = false;
        List<Shape> _shapes = new List<Shape>();
        private int[,] _markedImage;

        Color[] _colors = new Color[20]
            {
                Color.White, Color.Yellow, Color.Tomato, Color.SpringGreen, Color.SkyBlue,
                Color.Salmon, Color.Purple, Color.Orchid, Color.Orange, Color.Olive, Color.Moccasin, Color.MidnightBlue,
                Color.LimeGreen, Color.LightCoral, Color.LawnGreen, Color.Indigo, Color.GreenYellow, Color.Gold,
                Color.Fuchsia, Color.ForestGreen
            };

        public Bitmap CurrentBitmap { get; set; }

        public bool ImageChanged
        {
            get { return _imageChanged; }
        }

        public void ClearImage()
        {
            CurrentBitmap = null;
            _imageChanged = false;
        }

        public Dictionary<int, int> GetHistogram()
        {
            List<int> brightness = new List<int>();
            for (int i = 0; i < CurrentBitmap.Width; i++)
            {
                for (int j = 0; j < CurrentBitmap.Height; j++)
                {
                    Color c = CurrentBitmap.GetPixel(i, j);
                    brightness.Add(GetBrightness(c));
                }
            }

            var hystogram = new Dictionary<int, int>();
            for (int i = 0; i < 256; i++)
            {
                hystogram.Add(i, brightness.Count(b => b == i));
            }

            return hystogram;
        }
        public void Erosion(int depth)
        {
            Bitmap dest = new Bitmap(CurrentBitmap.Width, CurrentBitmap.Height);
            for (int i = 0; i < CurrentBitmap.Width; i++)
            {
                for (int j = 0; j < CurrentBitmap.Height; j++)
                {
                    if (IsBoundary(CurrentBitmap, i, j))
                    {
                        for (int k = 1; k <= depth; k++)
                        {
                            if (i - k >= 0)
                            {
                                dest.SetPixel(i - k, j, Color.Black);
                            }
                            if (i + k < dest.Width)
                            {
                                dest.SetPixel(i + k, j, Color.Black);
                            }
                            if (j - k >= 0)
                            {
                                dest.SetPixel(i, j - k, Color.Black);
                            }
                            if (j + k < dest.Height)
                            {
                                dest.SetPixel(i, j + k, Color.Black);
                            }
                            dest.SetPixel(i, j, Color.Black);
                        }
                    }
                    else
                    {
                        dest.SetPixel(i, j, CurrentBitmap.GetPixel(i, j));
                    }
                }
            }
            CurrentBitmap = dest;
        }

        public void Dilation(int depth)
        {
            Bitmap dest = new Bitmap(CurrentBitmap.Width, CurrentBitmap.Height);
            for (int i = 0; i < CurrentBitmap.Width; i++)
            {
                for (int j = 0; j < CurrentBitmap.Height; j++)
                {
                    if (IsBoundary(CurrentBitmap, i, j))
                    {
                        for (int k = 1; k <= depth; k++)
                        {
                            if (i - k >= 0)
                            {
                                dest.SetPixel(i - k, j, Color.White);
                            }
                            if (i + k < dest.Width)
                            {
                                dest.SetPixel(i + k, j, Color.White);
                            }
                            if (j - k >= 0)
                            {
                                dest.SetPixel(i, j - k, Color.White);
                            }
                            if (j + k < dest.Height)
                            {
                                dest.SetPixel(i, j + k, Color.White);
                            }
                            dest.SetPixel(i, j, Color.White);
                        }
                    }
                    else
                    {
                        dest.SetPixel(i, j, CurrentBitmap.GetPixel(i, j));
                    }
                }
            }
            CurrentBitmap = dest;
        }

        public bool IsBoundary(Bitmap bitmap, int x, int y)
        {
            int Black = 0, White = 255;
            if (GetBrightness(bitmap, x, y) == Black)
            {
                return false;
            }
            if (x > 0 && GetBrightness(bitmap, x - 1, y) == Black)
            {
                return true;
            }
            if (x < bitmap.Width - 1 && GetBrightness(bitmap, x + 1, y) == Black)
            {
                return true;
            }
            if (y > 0 && GetBrightness(bitmap, x, y - 1) == Black)
            {
                return true;
            }
            if (y < bitmap.Height - 1 && GetBrightness(bitmap, x, y + 1) == Black)
            {
                return true;
            }
            return false;
        }

        public void SequentialScan()
        {
            _markedImage = new int[CurrentBitmap.Width, CurrentBitmap.Height];
            for (int i = 0; i <_markedImage.GetLength(0); ++i)
                for (int j = 0; j <_markedImage.GetLength(1); ++j)
                   _markedImage[i, j] = -1;
            int currentClass = -1;
            for (int i = 1; i < CurrentBitmap.Width; i++)
            {
                for (int j = 1; j < CurrentBitmap.Height; j++)
                {
                    Color pixel = CurrentBitmap.GetPixel(i, j);
                    if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0) // White - цвет обьекта. Black - фоновый цвет
                    {

                    }
                    else
                    {
                        if (_markedImage[i - 1, j] < 0 && _markedImage[i, j - 1] < 0)
                            // оба пикселя не отнесены ни к одной из областей
                        {
                            ++currentClass;
                           _markedImage[i, j] = currentClass;
                        }
                        else if (_markedImage[i - 1, j] >= 0 ^ _markedImage[i, j - 1] >= 0)
                        {
                            if (_markedImage[i - 1, j] >= 0)
                            {
                               _markedImage[i, j] =_markedImage[i - 1, j];
                            }
                            else
                            {
                               _markedImage[i, j] =_markedImage[i, j - 1];
                            }
                        }
                        else if (_markedImage[i - 1, j] >= 0 && _markedImage[i, j - 1] >= 0)
                        {
                            if (_markedImage[i - 1, j] == _markedImage[i, j - 1])
                            {
                               _markedImage[i, j] =_markedImage[i, j - 1];
                            }
                            else
                            {
                               _markedImage[i, j] =_markedImage[i - 1, j];
                                for (int k = 1; k <_markedImage.GetLength(0); k++)
                                {
                                    for (int l = 1; l <_markedImage.GetLength(1); l++)
                                    {
                                        if (_markedImage[k, l] == _markedImage[i, j - 1])
                                        {
                                           _markedImage[k, l] =_markedImage[i - 1, j];
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            _shapes = new List<Shape>();
            Dictionary<int, Shape> x = new Dictionary<int, Shape>();
            for (int i = 1; i <_markedImage.GetLength(0); i++)
            {
                for (int j = 1; j <_markedImage.GetLength(1); j++)
                {
                    if (_markedImage[i, j] >= 0)
                    {
                        if (!x.ContainsKey(_markedImage[i, j]))
                        {
                            var shape = new Shape() { Marker = _markedImage[i, j] };
                            x.Add(_markedImage[i, j], shape);
                        }
                        x[_markedImage[i, j]].AddPoint(new Point(i, j));
                    }
                }
            }
            _shapes = x.Values.ToList();


            var classificator = new ShapePropertyHelper(_markedImage);
            //foreach (Shape shape in _shapes)
            //{
            //    classificator.CalculateProperties(shape);
            //}

            _shapes = _shapes.AsParallel().Where(s => s.Points.Count > 10000 && Math.Abs(1 - s.GetAttitudeheightToWeight()) < 0.25).ToList();
            _shapes.Sort((s, s1) => s1.Points.Count - s.Points.Count);
            int newMark = 0;
            Dictionary<int, Shape> topShapes =
                _shapes.ToDictionary(s =>
                    {
                        int oldMark = s.Marker;
                        s.Marker = newMark;
                        newMark++;
                        return oldMark;
                    });
            for (int i = 1; i < _markedImage.GetLength(0); i++)
            {
                for (int j = 1; j < _markedImage.GetLength(1); j++)
                {
                    Shape curShape;
                    if (topShapes.TryGetValue(_markedImage[i, j], out curShape))
                    {
                        _markedImage[i, j] = curShape.Marker;
                    }
                    else
                    {
                        _markedImage[i, j] = -1;
                    }
                }
            }

            _shapes = topShapes.Values.ToList();
            classificator.FindParents(_shapes);
            //ClearEdges();
            classificator = new ShapePropertyHelper(_markedImage);
            foreach (Shape shape in _shapes)
            {
                classificator.CalculateProperties(shape);
            }
        }

        private void ClearEdges()
        {
            for (int x = 2; x < _markedImage.GetLength(0) - 2; x++)
            {
                for (int y = 2; y < _markedImage.GetLength(1) - 2; y++)
                {
                    if(_markedImage[x, y] < 0)
                    {
                        int newMark = -1;
                        for (int i = -2; i <= 2; i++)
                        {
                            for (int j = -2; j <= 2; j++)
                            {
                                if (_markedImage[x + i, y + j] >= 0)
                                {
                                    if (newMark < 0)
                                    {
                                        newMark = _markedImage[x + i, y + j];
                                        continue;
                                    }
                                    if (newMark != _markedImage[x + i, y + j])
                                        goto next;
                                }
                            }
                        }
                        _markedImage[x, y] = newMark;
                    }
                next: Console.WriteLine("dd");
                }
            }

            //int prevMark = -1;
            //for (int x = 1; x < _markedImage.GetLength(0) - 1; x++)
            //{     
            //    prevMark = -1;
            //    for(int y = 2; y < _markedImage.GetLength(1)-2; y++)
            //    {
            //        if(_markedImage[x,y]<0 && prevMark>=0)
            //        {
            //            int nextMark = prevMark;
            //            int downPos = _markedImage.GetLength(1)-1;
            //            bool flag = true;
            //            for (int y2 = y+1; y2 < _markedImage.GetLength(1); y2++)
            //            {
            //                if (_markedImage[x, y2] > 0)
            //                {
            //                    if(_markedImage[x, y2] != prevMark)
            //                    {
            //                        flag = false;
            //                    }
            //                    else
            //                        downPos = y2;
            //                    break;
            //                }
            //                else
            //                {
            //                    if((_markedImage[x-1, y2] > 0 && _markedImage[x-1, y2] != prevMark)
            //                        || (_markedImage[x-2, y2] > 0 && _markedImage[x-2, y2] != prevMark)
            //                        || (_markedImage[x+1, y2] > 0 && _markedImage[x+1, y2] != prevMark)
            //                        || (_markedImage[x+2, y2] > 0 && _markedImage[x+2, y2] != prevMark))
            //                    {
            //                        flag = false;
            //                        break;
            //                    }
            //                }
            //            }
            //            if (flag)
            //            {
            //                for (int y2 = y + 1; y2 <= downPos; y2++)
            //                    _markedImage[x, y2] = prevMark;
            //            }
            //        }
            //        else
            //            prevMark = _markedImage[x,y];
            //    }
            //}
        }

        public int GetBrightness(Color c)
        {
            return (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
        }

        public int GetBrightness(Bitmap bitmap, int x, int y)
        {
            Color color = bitmap.GetPixel(x, y);
            return GetBrightness(color);
        }

        public void Grayscale()
        {
            _previousBitmap = (Bitmap)CurrentBitmap.Clone();
            Bitmap bmap = (Bitmap)CurrentBitmap.Clone();
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    byte gray = (byte)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            _imageChanged = true;
            CurrentBitmap = bmap;
        }

        public void Contrast(double contrast)
        {
            _previousBitmap = (Bitmap)CurrentBitmap.Clone();
            Bitmap bmap = (Bitmap)CurrentBitmap.Clone();
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);

                    double pR = SetContrast(contrast, c.R);
                    double pG = SetContrast(contrast, c.G);
                    double pB = SetContrast(contrast, c.B);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            _imageChanged = true;
            CurrentBitmap = bmap;
        }

        private double SetContrast(double contrast, byte value)
        {
            double newValue = value / 255.0;
            newValue -= 0.5;
            newValue *= contrast;
            newValue += 0.5;
            newValue *= 255;

            if (newValue < 0) newValue = 0;
            if (newValue > 255) newValue = 255;

            return newValue;
        }

        public void Undo()
        {
            CurrentBitmap = _previousBitmap;
        }

        public List<Shape> Shapes { get { return _shapes; } } 

        public Bitmap GetMarkedBitmap()
        {
            Bitmap res = new Bitmap(_markedImage.GetLength(0), _markedImage.GetLength(1));
            for(int x = 0; x < res.Width; x++)
                for (int y = 0; y < res.Height; y++)
                {
                    int mark = _markedImage[x, y];
                    if(mark<0)
                        res.SetPixel(x, y, Color.Black);
                    else if (_colors.Length <= mark)
                    {
                        res.SetPixel(x, y, Color.White);
                    }
                    else 
                        res.SetPixel(x, y, _colors[mark]);
                }
            return res;
        }

        public Bitmap GetCircle()
        {
            var helper = new ShapePropertyHelper(_markedImage);
            var circles = Shapes.Where(s => helper.IsCircle(s)).ToList();
            Bitmap res = new Bitmap(_markedImage.GetLength(0), _markedImage.GetLength(1));
            for (int x = 0; x < res.Width; x++)
                for (int y = 0; y < res.Height; y++)
                {
                    int mark = _markedImage[x, y];
                    if (mark < 0 || circles.All(c => c.Marker != mark))
                        res.SetPixel(x, y, Color.Black);
                    else
                        res.SetPixel(x, y, _colors[mark]);
                }
            return res;
        }

        public Bitmap GetCircle(Bitmap sourceBitmap)
        {
            var helper = new ShapePropertyHelper(_markedImage);
            double koefToCircle = ShapePropertyHelper.GetSimilarityCoefficientToCircle(Shapes[0]);
            Shape circleShape = Shapes[0];
            for (int i = 1; i < Shapes.Count; i++)
            {
                double simCoef = ShapePropertyHelper.GetSimilarityCoefficientToCircle(Shapes[i]);
                if (simCoef < koefToCircle)
                {
                    koefToCircle = simCoef;
                    circleShape = Shapes[i];
                }
            }
            Bitmap res = new Bitmap(_markedImage.GetLength(0), _markedImage.GetLength(1));
            for(int i = 0; i < res.Width; i++)
                for(int j = 0; j < res.Height; j++)
                    res.SetPixel(i,j,Color.Black);
            int circleMark = circleShape.Marker;

            //for (int y = circleShape.MinY; y <= circleShape.MaxY; y++)
            //{
            //    int startCircleInRow = 0, endCircleInRow = -1;
            //    for (int x = circleShape.MinX; x < circleShape.MaxX; x++)
            //    {
            //        int mark = _markedImage[x, y];
            //        if (circleMark == mark)
            //        {
            //            if (startCircleInRow == 0)
            //                startCircleInRow = x;
            //            endCircleInRow = x;
            //        }
            //        //int mark = _markedImage[x, y];
            //        //if (mark < 0 || circleShape.Marker != mark)
            //        //    res.SetPixel(x, y, Color.Black);
            //        //else
            //        //    res.SetPixel(x, y, sourceBitmap.GetPixel(x, y));
            //    }
            //    for (int x1 = startCircleInRow; x1 <= endCircleInRow; x1++)
            //    {
            //        Color curColor = sourceBitmap.GetPixel(x1, y);
            //        int sum = curColor.R + curColor.G + curColor.B;
            //        double partOfRed = ((double) curColor.R)/sum;
            //        if (partOfRed > 0.5)
            //        {
            //            for (int x = startCircleInRow; x <= endCircleInRow; x++)
            //            {
            //                res.SetPixel(x, y, sourceBitmap.GetPixel(x, y));
            //            }
            //            break;
            //        }
            //    }
            //}

            for (int y = circleShape.MinY; y <= circleShape.MaxY; y++)
            {
                int startCircleInRow = 0, endCircleInRow = -1;
                for (int x = circleShape.MinX; x < circleShape.MaxX; x++)
                {
                    int mark = _markedImage[x, y];
                    if (circleMark == mark)
                    {
                        if (startCircleInRow == 0)
                            startCircleInRow = x;
                        endCircleInRow = x;
                    }
                    //int mark = _markedImage[x, y];
                    //if (mark < 0 || circleShape.Marker != mark)
                    //    res.SetPixel(x, y, Color.Black);
                    //else
                    //    res.SetPixel(x, y, sourceBitmap.GetPixel(x, y));
                }
                for (int x = startCircleInRow; x <= endCircleInRow; x++)
                {
                    res.SetPixel(x, y, sourceBitmap.GetPixel(x, y));
                }
            }
            return res;
        }

        public Dictionary<int, List<Point>> GetCirclePoints(Bitmap sourceBitmap, out Bitmap resultBitmap)
        {
            double koefToCircle = ShapePropertyHelper.GetSimilarityCoefficientToCircle(Shapes[0]);
            Shape circleShape = Shapes[0];
            for (int i = 1; i < Shapes.Count; i++)
            {
                double simCoef = ShapePropertyHelper.GetSimilarityCoefficientToCircle(Shapes[i]);
                if (simCoef < koefToCircle)
                {
                    koefToCircle = simCoef;
                    circleShape = Shapes[i];
                }
            }
            Dictionary<int, List<Point>> res = new Dictionary<int, List<Point>>();
            resultBitmap = new Bitmap(_markedImage.GetLength(0), _markedImage.GetLength(1));
            for (int i = 0; i < resultBitmap.Width; i++)
                for (int j = 0; j < resultBitmap.Height; j++)
                    resultBitmap.SetPixel(i, j, Color.Black);
            int circleMark = circleShape.Marker;
            for (int y = circleShape.MinY; y <= circleShape.MaxY; y++)
            {
                int startCircleInRow = 0, endCircleInRow = -1;
                for (int x = circleShape.MinX; x < circleShape.MaxX; x++)
                {
                    int mark = _markedImage[x, y];
                    if (circleMark == mark)
                    {
                        if (startCircleInRow == 0)
                            startCircleInRow = x;
                        endCircleInRow = x;
                    }
                }
                res.Add(y, new List<Point>());
                for (int x = startCircleInRow; x <= endCircleInRow; x++)
                {
                    res[y].Add(new Point(x, y));
                    resultBitmap.SetPixel(x,y, sourceBitmap.GetPixel(x,y));
                }
            }
            return res;
        }

        public static Dictionary<int, List<Point>> GetCounterArea(Bitmap sourceImage, Dictionary<int, List<Point>> circlePoints,
            out Bitmap resultBitmap)
        {
            resultBitmap = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < resultBitmap.Width; i++)
                for (int j = 0; j < resultBitmap.Height; j++)
                    resultBitmap.SetPixel(i, j, Color.Black);
            Dictionary<int, List<int>> rowIntervals = new Dictionary<int, List<int>>();
            int rowsCount = circlePoints.Count;
            int skipCount = rowsCount / 5;
            List<int> rowsKeys = circlePoints.Keys.OrderBy(i => i).Take(rowsCount - skipCount).Skip(skipCount).ToList();
            foreach (int rowsKey in rowsKeys)
            {
                rowIntervals.Add(rowsKey, new List<int>());
                int counter = 0;
                foreach (Point circlePoint in circlePoints[rowsKey])
                {
                    Color pixel = sourceImage.GetPixel(circlePoint.X, circlePoint.Y);
                    if (pixel.R == 0)
                    {
                        if (counter != 0)
                            rowIntervals[rowsKey].Add(counter);
                        counter = 0;
                    }
                    else
                        counter++;
                }
            }

            foreach (int rowsKey in rowsKeys)
            {
                if (rowIntervals[rowsKey].Count < 15)
                    rowIntervals.Remove(rowsKey);
                //else
                //{
                //    foreach (Point cPoint in circlePoints[rowsKey])
                //    {
                //        resultBitmap.SetPixel(cPoint.X, cPoint.Y, sourceImage.GetPixel(cPoint.X, cPoint.Y));
                //    }
                //}
            }
            rowsKeys = rowIntervals.Keys.OrderBy(k=>k).ToList();
            List<List<int>> groupRows = new List<List<int>>();
            int lastElement = rowsKeys[0];
            groupRows.Add(new List<int>());
            int groupcounter = 0;
            for (int i = 1; i < rowsKeys.Count; i++)
            {
                groupRows[groupcounter].Add(lastElement);
                if (rowsKeys[i] - 1 != lastElement)
                {
                    groupRows.Add(new List<int>());
                    groupcounter++;
                }
                lastElement = rowsKeys[i]; 
            }

            for (int i = groupRows.Count - 1; i >= 0; i--)
            {
                if (groupRows[i].Count < 10) //10 - min groupLineCount
                {
                    foreach (int row in groupRows[i])
                    {
                        rowsKeys.Remove(row);
                        rowIntervals.Remove(row);
                    }
                    groupRows.RemoveAt(i);
                }
            }

            //              group, Dispersia
            List<KeyValuePair<int,double>> groupDespersia = new List<KeyValuePair<int, double>>();
            for (int i = 0; i < groupRows.Count; i++)
            {
                List<int> rows = groupRows[i];
                int firstRow = (int)(rows.Count*0.3);
                int avgRow = (int) (rows.Count/2);
                int lastRow = (int) (rows.Count*0.8);
                double sumOfDisp = CalculateDispersia(rowIntervals[groupRows[i][firstRow]]) +
                                   CalculateDispersia(rowIntervals[groupRows[i][avgRow]])
                                   + CalculateDispersia(rowIntervals[groupRows[i][lastRow]]);
                groupDespersia.Add(new KeyValuePair<int, double>(i, sumOfDisp));
            }

            KeyValuePair<int, double> minGroup = groupDespersia.OrderBy(kv => kv.Value).First();
            List<int> counteRows = groupRows[minGroup.Key];

            Dictionary<int, List<Point>> res = new Dictionary<int, List<Point>>();
            foreach (int counteRow in counteRows)
            {
                res.Add(counteRow, circlePoints[counteRow]);
            }

            foreach (var rowpoints in res.Values)
            {
                foreach (Point cPoint in rowpoints)
                {
                    resultBitmap.SetPixel(cPoint.X, cPoint.Y, sourceImage.GetPixel(cPoint.X, cPoint.Y));
                }
            }

            foreach (var rowpoints in res.Values)
            {
                foreach (Point cPoint in rowpoints)
                {
                    resultBitmap.SetPixel(cPoint.X, cPoint.Y, sourceImage.GetPixel(cPoint.X, cPoint.Y));
                }
            }

            return res;
        }

        //without first & last interval
        private static double CalculateDispersia(List<int> intervals)
        {
            intervals = intervals.Take(intervals.Count - 1).Skip(1).ToList();//Sorry GC
            double avg = ((double)intervals.Sum())/intervals.Count;
            return intervals.Select(i => (avg - i)*(avg - i)).Sum()/intervals.Count;
        }

        public static Dictionary<int, List<Point>> DetectCounterNumbers(Bitmap sourceBitmap,
            Dictionary<int, List<Point>> rowsPoints, out Bitmap outBitmap)
        {
            List<Point> allRowsPoints = rowsPoints.SelectMany(kv => kv.Value).ToList();
            List<int> columnsIndexes = allRowsPoints.Select(p => p.X).Distinct().OrderBy(c => c).ToList();
            Dictionary<int, List<Point>> columnPoints = new Dictionary<int, List<Point>>();
            foreach (int columnsIndex in columnsIndexes)
            {
                columnPoints.Add(columnsIndex, allRowsPoints.Where(p => p.X == columnsIndex).ToList());
            }

            Color zeroColor = Color.FromArgb(0, 0, 0, 0);
            outBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            for (int i = 0; i < outBitmap.Width; i++)
                for (int j = 0; j < outBitmap.Height; j++)
                    outBitmap.SetPixel(i, j, zeroColor);

            int rowsCount = rowsPoints.Count;
            for (int i = columnsIndexes.Count-1; i >=0;i--)
            {
                int columnIndex = columnsIndexes[i];
                //int blackCount = 0;
                //foreach (Point p in columnPoints[columnIndex])
                //{
                //    Color color = sourceBitmap.GetPixel(p.X, p.Y);
                //    blackCount += color.R;
                //}
                if /*(blackCount!=0)*/(columnPoints[columnIndex].Count != rowsCount 
                    || columnPoints[columnIndex].All(p => sourceBitmap.GetPixel(p.X, p.Y).R == 255))
                    //if all column white == without object
                {
                    columnPoints.Remove(columnIndex);
                    columnsIndexes.RemoveAt(i);
                }
            }

            List<List<int>> groupColumns = new List<List<int>>();
            int previousElement = columnsIndexes[0];
            groupColumns.Add(new List<int>());
            int groupcounter = 0;
            for (int i = 1; i < columnsIndexes.Count; i++)
            {
                groupColumns[groupcounter].Add(previousElement);
                if (columnsIndexes[i] - 1 != previousElement)
                {
                    groupColumns.Add(new List<int>());
                    groupcounter++;
                }
                previousElement = columnsIndexes[i];
            }

            Dictionary<int, List<Point>> objectsDictResult = new Dictionary<int, List<Point>>();
            for (int objCounter = 0; objCounter < groupColumns.Count; objCounter++)
            {
                objectsDictResult.Add(objCounter, new List<Point>());
                foreach (int column in groupColumns[objCounter])
                    objectsDictResult[objCounter].AddRange(columnPoints[column]);
            }

            foreach (var rowpoints in objectsDictResult.Values)
            {
                foreach (Point cPoint in rowpoints)
                {
                    outBitmap.SetPixel(cPoint.X, cPoint.Y, sourceBitmap.GetPixel(cPoint.X, cPoint.Y));
                }
            }

            return objectsDictResult;
        }
    }
}
