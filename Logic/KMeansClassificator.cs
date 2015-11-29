using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    //public interface IClassificator
    //{
    //    IDictionary<int, int> Classification();
    //}

    //public class KMeansClassificator : IClassificator
    //{
    //    private Random _random;
    //    private IDictionary<int, int> _result;
    //    private IEnumerable<KeyValuePair<int, Shape>> _shapes;
    //    private int _clasterCount;
    //    private Shape[] _clusters;

    //    public KMeansClassificator(IEnumerable<KeyValuePair<int, Shape>> shapes, int clasterCount)
    //    {
    //        _clasterCount = clasterCount;
    //        _shapes = shapes;
    //        Initialize(clasterCount);
    //    }

    //    public IDictionary<int, int> Classification()
    //    {
    //        InitializeClusters(_clasterCount);
    //        do
    //        {
    //            List<Dictionary<int, double>> distance = CalculateDistance();
    //            ReClassification(distance);
    //        }
    //        while (ReInitializeClusters());
    //        return _result;
    //    }

    //    private void Initialize(int clasterCount)
    //    {
    //        _random = new Random();
    //        _result = new Dictionary<int, int>();
    //        foreach (var shape in _shapes)
    //        {
    //            _result.Add(shape.Key, 0);
    //        }
    //        _clusters = new Shape[clasterCount];
    //    }

    //    private void InitializeClusters(int clasterCount)
    //    {
    //        IList<int> numbers = Extensions.UniqueRandomArray(0, clasterCount, clasterCount);
    //        for (int i = 0; i < clasterCount; i++)
    //        {
    //            _clusters[i] = new Shape()
    //            {
    //                Square = _shapes.ElementAt(numbers[i]).Value.Square,//     GetStartValue(x => x.Value.Square),
    //                CenterOfMassX = _shapes.ElementAt(numbers[i]).Value.CenterOfMassX,//GetStartValue(x => x.Value.CenterOfMassX),
    //                CenterOfMassY = _shapes.ElementAt(numbers[i]).Value.CenterOfMassY,//GetStartValue(x => x.Value.CenterOfMassY),
    //                Perimeter = _shapes.ElementAt(numbers[i]).Value.Perimeter,//GetStartValue(x => x.Value.Perimeter),
    //                Compact = _shapes.ElementAt(numbers[i]).Value.Compact,//GetStartValue(x => x.Value.Compact),
    //                Elongation = _shapes.ElementAt(numbers[i]).Value.Elongation,//GetStartValue(x => x.Value.Elongation),
    //                OrientationOfTheMainAxis = _shapes.ElementAt(numbers[i]).Value.OrientationOfTheMainAxis,//GetStartValue(x => x.Value.OrientationOfTheMainAxis)
    //            };
    //        }
    //    }

    //    private bool ReInitializeClusters()
    //    {
    //        Shape[] clusters = new Shape[_clasterCount];
    //        for (int i = 0; i < _clasterCount; )
    //        {
    //            clusters[i++] = new Shape()
    //            {
    //                Square = GetNewValue(x => x.Value.Square, i),
    //                CenterOfMassX = GetNewValue(x => x.Value.CenterOfMassX, i),
    //                CenterOfMassY = GetNewValue(x => x.Value.CenterOfMassY, i),
    //                Perimeter = GetNewValue(x => x.Value.Perimeter, i),
    //                Compact = GetNewValue(x => x.Value.Compact, i),
    //                Elongation = GetNewValue(x => x.Value.Elongation, i),
    //                OrientationOfTheMainAxis = GetNewValue(x => x.Value.OrientationOfTheMainAxis, i)
    //            };
    //        }
    //        bool changed = clusters.EqualsList(_clusters);
    //        _clusters = clusters;
    //        return !changed;
    //    }

    //    private double GetStartValue(Func<KeyValuePair<int, Shape>, double> selector)
    //    {
    //        return _random.Next((int)_shapes.Min(selector), (int)_shapes.Max(selector)) + _random.NextDouble();
    //    }

    //    private double GetNewValue(Func<KeyValuePair<int, Shape>, double> selector, int cluster)
    //    {
    //        var shapeNumbers = _result.Where(x => x.Value == cluster).Select(x => x.Key);
    //        var shapes = _shapes.Where(x => shapeNumbers.Contains(x.Key));
    //        return shapes.Average(selector);
    //    }

    //    private List<Dictionary<int, double>> CalculateDistance()
    //    {
    //        List<Dictionary<int, double>> distance = new List<Dictionary<int, double>>(_clusters.Length);
    //        for (int i = 0; i < _clusters.Length; i++)
    //        {
    //            distance.Add(new Dictionary<int, double>());
    //            foreach (var shape in _shapes)
    //            {
    //                distance[i].Add(shape.Key, _clusters[i].GetDistance(shape.Value));
    //            }
    //        }
    //        return distance;
    //    }

    //    private void ReClassification(List<Dictionary<int, double>> distance)
    //    {
    //        foreach (var shape in _shapes)
    //        {
    //            int shapeNumber = 0;
    //            double minValue = Double.MaxValue;
    //            for (int j = 0; j < distance.Count; j++)
    //            {
    //                double value = distance[j][shape.Key];
    //                if (value <= minValue)
    //                {
    //                    minValue = value;
    //                    shapeNumber = j;
    //                }
    //            }
    //            _result[shape.Key] = shapeNumber + 1;
    //        }
    //    }
    //}

}
