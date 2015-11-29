using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Logic
{
    //public abstract class ImageAnalyzer
    //{
    //    protected const int _minPixelCount = 200;

    //    private Dictionary<int, LinkedList<Point>> _areas;
    //    private int[][] _labels;
    //    private Dictionary<int, Shape> _shapes;

    //    private Dictionary<int, double> _squares;
    //    private Dictionary<int, double> _centerOfMassX;
    //    private Dictionary<int, double> _centerOfMassY;
    //    private Dictionary<int, double> _perimeter;
    //    private Dictionary<int, double> _compact;
    //    private Dictionary<int, double> _elongation;
    //    private Dictionary<int, double> _orientationOfTheMainAxis;
    //    private Dictionary<string, Dictionary<int, double>> _moments;

    //    public Bitmap SourceImage { get; private set; }

    //    public Bitmap LabelingImage { get; protected set; }

    //    public int[][] Labels
    //    {
    //        get
    //        {
    //            return _labels;
    //        }
    //        protected set
    //        {
    //            Initialize(value);
    //            _labels = value;
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> Squeres
    //    {
    //        get
    //        {
    //            return _squares ?? (_squares = СalculateSquares(_areas));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> CenterOfMassX
    //    {
    //        get
    //        {
    //            return _centerOfMassX ?? (_centerOfMassX = CalculateCenterOfMassX(_areas));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> CenterOfMassY
    //    {
    //        get
    //        {
    //            return _centerOfMassY ?? (_centerOfMassY = CalculateCenterOfMassY(_areas));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> Perimeter
    //    {
    //        get
    //        {
    //            return _perimeter ?? (_perimeter = CalculatePerimeter(_labels));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> Compact
    //    {
    //        get
    //        {
    //            return _compact ?? (_compact = CalculateCompact(Perimeter, Squeres));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> Elongation
    //    {
    //        get
    //        {
    //            return _elongation ?? (_elongation = CalculateElongation(_areas));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, double> OrientationOfTheMainAxis
    //    {
    //        get
    //        {
    //            return _orientationOfTheMainAxis ?? (_orientationOfTheMainAxis = CalculateOrientationOfTheMainAxis(_areas));
    //        }
    //    }

    //    public IReadOnlyDictionary<int, Shape> Shapes
    //    {
    //        get
    //        {
    //            if (_shapes == null)
    //            {
    //                InitializeShapes();
    //            }
    //            return _shapes;
    //        }
    //    }

    //    protected ImageAnalyzer(Bitmap image)
    //    {
    //        SourceImage = image;
    //    }

    //    public abstract void Labeling();

    //    public Bitmap gg(IDictionary<int, int> areaNumbers)
    //    {
    //        int[][] labels = new int[_labels.Length][];
    //        for (int i = 0; i < _labels.Length; i++)
    //        {
    //            labels[i] = new int[_labels[i].Length];
    //            for (int j = 0; j < _labels[i].Length; j++)
    //            {
    //                if (areaNumbers.ContainsKey(_labels[i][j]))
    //                {
    //                    labels[i][j] = areaNumbers[_labels[i][j]];
    //                }
    //            }
    //        }
    //        return ConvertToBitmap(labels);
    //    }

    //    public IReadOnlyDictionary<int, double> GetMoment(int i, int j)
    //    {
    //        string key = string.Concat(i, j);
    //        Dictionary<int, double> value;
    //        if (!_moments.TryGetValue(key, out value))
    //        {
    //            value = _areas.ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(
    //                            a => a.Key,
    //                            a => a.Value.Sum<Point>(p => Math.Pow(p.X - CenterOfMassX[a.Key], i) * Math.Pow(p.Y - CenterOfMassY[a.Key], j)));
    //            _moments.Add(key, value);
    //        }
    //        return value;
    //    }

    //    protected Bitmap ConvertToBitmap(int[][] labels)
    //    {
    //        Bitmap image = new Bitmap(labels.Length, labels.Max(x => x.Length));
    //        for (int y = 0; y < image.Height; ++y)
    //        {
    //            for (int x = 0; x < image.Width; ++x)
    //            {
    //                switch (labels[x][y])
    //                {
    //                    case 0:
    //                        image.SetPixel(x, y, Color.Black);
    //                        break;
    //                    case 1:
    //                        image.SetPixel(x, y, Color.MistyRose);
    //                        break;
    //                    case 2:
    //                        image.SetPixel(x, y, Color.Salmon);
    //                        break;
    //                    case 3:
    //                        image.SetPixel(x, y, Color.Red);
    //                        break;
    //                    case 4:
    //                        image.SetPixel(x, y, Color.Blue);
    //                        break;
    //                    case 5:
    //                        image.SetPixel(x, y, Color.Yellow);
    //                        break;
    //                    case 6:
    //                        image.SetPixel(x, y, Color.Pink);
    //                        break;
    //                    case 7:
    //                        image.SetPixel(x, y, Color.Plum);
    //                        break;
    //                    case 8:
    //                        image.SetPixel(x, y, Color.MediumVioletRed);
    //                        break;
    //                    case 9:
    //                        image.SetPixel(x, y, Color.ForestGreen);
    //                        break;
    //                    case 10:
    //                        image.SetPixel(x, y, Color.Firebrick);
    //                        break;
    //                    case 11:
    //                        image.SetPixel(x, y, Color.MidnightBlue);
    //                        break;
    //                    default:
    //                        image.SetPixel(x, y, Color.FromKnownColor((KnownColor)labels[x][y]));
    //                        break;
    //                }
    //            }
    //        }
    //        return image;
    //    }

    //    protected void LabelsFiltering(int[][] labels)
    //    {
    //        Dictionary<int, int> counter = new Dictionary<int, int>();
    //        for (int i = 0; i < labels.Length; i++)
    //        {
    //            for (int j = 0; j < labels[i].Length; j++)
    //            {
    //                if (counter.ContainsKey(labels[i][j]))
    //                {
    //                    counter[labels[i][j]]++;
    //                }
    //                else
    //                {
    //                    counter.Add(labels[i][j], 1);
    //                }
    //            }
    //        }
    //        var trash = counter.Where(x => x.Value < _minPixelCount).Select(x => x.Key);
    //        for (int i = 0; i < labels.Length; i++)
    //        {
    //            for (int j = 0; j < labels[i].Length; j++)
    //            {
    //                if (trash.Contains(labels[i][j]))
    //                {
    //                    labels[i][j] = 0;
    //                }
    //            }
    //        }
    //    }

    //    private void Reset()
    //    {
    //        _squares = null;
    //        _centerOfMassX = null;
    //        _centerOfMassY = null;
    //        _perimeter = null;
    //        _compact = null;
    //        _elongation = null;
    //        _moments = null;
    //        _orientationOfTheMainAxis = null;
    //        _shapes = null;
    //    }

    //    private void Initialize(int[][] labels)
    //    {
    //        Reset();
    //        _areas = new Dictionary<int, LinkedList<Point>>();
    //        _moments = new Dictionary<string, Dictionary<int, double>>();
    //        for (int x = 0; x < labels.Length; x++)
    //        {
    //            for (int y = 0; y < labels[x].Length; y++)
    //            {
    //                if (labels[x][y] != 0)
    //                {
    //                    if (!_areas.ContainsKey(labels[x][y]))
    //                    {
    //                        _areas.Add(labels[x][y], new LinkedList<Point>());
    //                    }
    //                    _areas[labels[x][y]].AddLast(new Point { X = x, Y = y });
    //                }
    //            }
    //        }
    //    }

    //    private void InitializeShapes()
    //    {
    //        _shapes = _areas.ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, Shape>(a => a.Key, a => new Shape());
    //        foreach (var shape in _shapes)
    //        {
    //            shape.Value.Square = Squeres[shape.Key];
    //            shape.Value.CenterOfMassX = CenterOfMassX[shape.Key];
    //            shape.Value.CenterOfMassY = CenterOfMassY[shape.Key];
    //            shape.Value.Perimeter = Perimeter[shape.Key];
    //            shape.Value.Compact = Compact[shape.Key];
    //            shape.Value.Elongation = Elongation[shape.Key];
    //            shape.Value.OrientationOfTheMainAxis = OrientationOfTheMainAxis[shape.Key];
    //        }
    //    }

    //    private Dictionary<int, double> СalculateSquares(IDictionary<int, LinkedList<Point>> areas)
    //    {
    //        return areas
    //            .ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(a => a.Key, a => a.Value.Count);
    //    }

    //    private Dictionary<int, double> CalculateCenterOfMassX(IDictionary<int, LinkedList<Point>> areas)
    //    {
    //        return areas
    //            .ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(a => a.Key, a => a.Value.Sum<Point>(p => p.X) / (double)a.Value.Count);
    //    }

    //    private Dictionary<int, double> CalculateCenterOfMassY(IDictionary<int, LinkedList<Point>> areas)
    //    {
    //        return areas
    //            .ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(a => a.Key, a => a.Value.Sum<Point>(p => p.Y) / (double)a.Value.Count);
    //    }

    //    private Dictionary<int, double> CalculatePerimeter(int[][] labels)
    //    {
    //        Dictionary<int, double> perimeter = new Dictionary<int, double>();
    //        for (int x = 0; x < labels.Length; x++)
    //        {
    //            for (int y = 0; y < labels[x].Length; y++)
    //            {
    //                if (labels[x][y] != 0)
    //                {
    //                    if ((x > 0 && labels[x - 1][y] != labels[x][y]) ||
    //                        (y > 0 && labels[x][y - 1] != labels[x][y]) ||
    //                        (x < (labels.GetLength(0) - 1) && labels[x + 1][y] != labels[x][y]) ||
    //                        (y < (labels[x].Length - 1) && labels[x][y + 1] != labels[x][y]))
    //                    {
    //                        if (!perimeter.ContainsKey(labels[x][y]))
    //                        {
    //                            perimeter.Add(labels[x][y], 0);
    //                        }
    //                        perimeter[labels[x][y]]++;
    //                    }
    //                }
    //            }
    //        }
    //        return perimeter;
    //    }

    //    private Dictionary<int, double> CalculateCompact(IReadOnlyDictionary<int, double> perimeters, IReadOnlyDictionary<int, double> squares)
    //    {
    //        Dictionary<int, double> compacts = new Dictionary<int, double>();
    //        foreach (var perimeter in perimeters)
    //        {
    //            if (squares.ContainsKey(perimeter.Key))
    //            {
    //                double compact = Math.Pow(perimeter.Value, 2) / (double)squares[perimeter.Key];
    //                compacts.Add(perimeter.Key, compact);
    //            }
    //        }
    //        return compacts;
    //    }

    //    private Dictionary<int, double> CalculateElongation(IDictionary<int, LinkedList<Point>> areas)
    //    {
    //        return areas
    //           .ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(a => a.Key, a => ElongationFunction()(a.Key));
    //    }

    //    private Dictionary<int, double> CalculateOrientationOfTheMainAxis(IDictionary<int, LinkedList<Point>> areas)
    //    {
    //        return areas
    //            .ToDictionary<KeyValuePair<int, LinkedList<Point>>, int, double>(a => a.Key, a => OrientationOfTheMainAxisFunction()(a.Key));
    //    }

    //    private Func<int, double> OrientationOfTheMainAxisFunction()
    //    {
    //        var moment_2_0 = GetMoment(2, 0);
    //        var moment_0_2 = GetMoment(0, 2);
    //        var moment_1_1 = GetMoment(1, 1);
    //        return i =>
    //        {
    //            return 0.5 * Math.Atan(2 * moment_1_1[i] / moment_2_0[i] - moment_0_2[i]);
    //        };
    //    }

    //    private Func<int, double> ElongationFunction()
    //    {
    //        var moment_2_0 = GetMoment(2, 0);
    //        var moment_0_2 = GetMoment(0, 2);
    //        var moment_1_1 = GetMoment(1, 1);
    //        return i =>
    //        {
    //            var sum = moment_2_0[i] + moment_0_2[i];
    //            var sqrt = Math.Sqrt(Math.Pow(moment_2_0[i] - moment_0_2[i], 2) + 4 * Math.Pow(moment_1_1[i], 2));
    //            return (sum + sqrt) / (sum - sqrt);
    //        };
    //    }
    //}

}
