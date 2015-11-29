using System.Linq;
using Logic.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//namespace Logic
//{
//    public class ImageСonverter
//    {
//        #region PrivateFields

//        private double _maxBrightness;
//        private double _minBrightness;
//        private double _scale = 0.005;

//        #endregion

//        #region Properties

//        public Bitmap Image
//        {
//            get;
//            private set;
//        }

//        public double MaxBrightness
//        {
//            get { return _maxBrightness; }
//        }

//        public double MinBrightness
//        {
//            get { return _minBrightness; }
//        }

//        public BitmapSource BitmapSource
//        {
//            get
//            {
//                return ConvertBitmapToBitmapSource(Image);
//            }
//        }

//        #endregion

//        #region Constructors

//        public ImageСonverter(string uri)
//        {
//            using (Stream stream = new FileStream(uri, FileMode.Open))
//            {
//                Image = new Bitmap(stream);
//            }
//            Initialize();
//        }

//        public ImageСonverter(Bitmap bitmap)
//        {
//            Image = bitmap;
//            Initialize();
//        }

//        #endregion

//        #region PublicMethods

//        public BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
//        {
//            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
//        }

//        public Bitmap Convert(IConverter converter)
//        {
//            Bitmap newImage = new Bitmap(Image.Width, Image.Height);
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    Color pixel = Image.GetPixel(x, y);
//                    double brightness = GetBrightness(pixel);
//                    double newBrightness = converter.Convert(brightness);
//                    double scale = brightness / newBrightness;
//                    Color newColor = Color.FromArgb(pixel.A, UpdateCoordinateToByte(pixel.R * scale),
//                        UpdateCoordinateToByte(pixel.G * scale), UpdateCoordinateToByte(pixel.B * scale));
//                    newImage.SetPixel(x, y, newColor);
//                }
//            }
//            return newImage;
//        }

//        public Bitmap Convert(IFilter filter)
//        {
//            Bitmap newImage = new Bitmap(Image.Width, Image.Height);
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    newImage.SetPixel(x, y, filter.Filter(GetColor, x, y));
//                }
//            }
//            return newImage;
//        }

//        public Bitmap Binarization(IBinarization binarizator)
//        {
//            Bitmap newImage = new Bitmap(Image.Width, Image.Height);
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    Color pixel = Image.GetPixel(x, y);
//                    bool brightness = binarizator.Binarization(GetBrightness(pixel));
//                    newImage.SetPixel(x, y, brightness ? Color.Black : Color.White);
//                }
//            }
//            return newImage;
//        }

//        public IList<Item> GetBrightnessStatistic()
//        {
//            Dictionary<int, int> statistic = new Dictionary<int, int>();
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    double brightness = GetBrightness(Image.GetPixel(x, y));
//                    int nubmer = (int)(brightness / _scale);
//                    if (statistic.ContainsKey(nubmer))
//                    {
//                        statistic[nubmer]++;
//                    }
//                    else
//                    {
//                        statistic.Add(nubmer, 1);
//                    }
//                }
//            }
//            return statistic.Select(s => new Item() { Value = s.Key * _scale, Count = s.Value }).ToList();
//        }

//        #endregion

//        #region PrivateMethods

//        private void Initialize()
//        {
//            _maxBrightness = GetMaxBrightness();
//            _minBrightness = GetMinBrightness();
//        }

//        private byte UpdateCoordinateToByte(double value)
//        {
//            return (byte)UpdateCoordinate((int)value, Byte.MinValue, Byte.MaxValue);
//        }

//        private int UpdateCoordinate(int value, int minValue, int maxValue)
//        {
//            return value < minValue ? minValue : (value > maxValue ? maxValue : value);
//        }

//        private void UpdateCoordinate(ref int x, ref int y)
//        {
//            x = UpdateCoordinate(x, 0, Image.Width - 1);
//            y = UpdateCoordinate(y, 0, Image.Height - 1);
//        }

//        private Color GetColor(int x, int y)
//        {
//            UpdateCoordinate(ref x, ref y);
//            return Image.GetPixel(x, y);
//        }

//        private double GetBrightness(Color pixel)
//        {
//            return GetBrightness(pixel.R, pixel.G, pixel.B);
//        }

//        private double GetBrightness(byte red, byte green, byte blue)
//        {
//            return (0.3 * red + 0.59 * green + 0.11 * blue) / 255.0;
//        }

//        private double GetMaxBrightness()
//        {
//            double brightness = double.MinValue;
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    double newBrightness = GetBrightness(Image.GetPixel(x, y));
//                    if (newBrightness > brightness)
//                    {
//                        brightness = newBrightness;
//                    }
//                }
//            }
//            return brightness;
//        }

//        private double GetMinBrightness()
//        {
//            double brightness = double.MaxValue;
//            for (int x = 0; x < Image.Width; x++)
//            {
//                for (int y = 0; y < Image.Height; y++)
//                {
//                    double newBrightness = GetBrightness(Image.GetPixel(x, y));
//                    if (newBrightness < brightness)
//                    {
//                        brightness = newBrightness;
//                    }
//                }
//            }
//            return brightness;
//        }

//        #endregion
//    }

//    public class Item
//    {
//        public double Value { get; set; }

//        public int Count { get; set; }
//    }
//}
