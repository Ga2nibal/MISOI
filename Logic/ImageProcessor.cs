using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

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
                        x[_markedImage[i, j]].Points.Add(new Point(i, j));
                    }
                }
            }
            _shapes = x.Values.ToList();


            var classificator = new ShapePropertyHelper(_markedImage);
            //foreach (Shape shape in _shapes)
            //{
            //    classificator.CalculateProperties(shape);
            //}

            _shapes.Sort((s, s1) => s1.Points.Count - s.Points.Count);
            int newMark = 0;
            Dictionary<int, Shape> top20Shape = _shapes.Take(30).ToDictionary(s =>
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
                    if (top20Shape.TryGetValue(_markedImage[i, j], out curShape))
                    {
                        _markedImage[i, j] = curShape.Marker;
                    }
                    else
                    {
                        _markedImage[i, j] = -1;
                    }
                }
            }

            _shapes = top20Shape.Values.ToList();
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
            Debugger.Launch();
            var circles = Shapes.Where(s => helper.IsCircle(s)).ToList();
            Bitmap res = new Bitmap(_markedImage.GetLength(0), _markedImage.GetLength(1));
            for (int x = 0; x < res.Width; x++)
                for (int y = 0; y < res.Height; y++)
                {
                    int mark = _markedImage[x, y];
                    if (mark < 0 || circles.All(c => c.Marker != mark))
                        res.SetPixel(x, y, Color.Black);
                    else
                        res.SetPixel(x, y, sourceBitmap.GetPixel(x,y));
                }
            return res;
        }
    }
}
