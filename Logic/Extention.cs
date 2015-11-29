using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logic
{
    public static class Extensions
    {
        public static double GetDistance(this Shape thisShape, Shape shape)
        {
            double distance = 0;
            Type type = typeof(Shape);
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(x => x.CanRead);
            foreach (var property in properties)
            {
                distance += Math.Pow((double)property.GetValue(thisShape) - (double)property.GetValue(shape), 2);
            }
            return Math.Sqrt(distance);
        }

        public static bool EqualsList(this IList<Shape> thisShapes, IList<Shape> shapes)
        {
            Type type = typeof(Shape);
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(x => x.CanRead);
            for (int i = 0; i < thisShapes.Count; i++)
            {
                foreach (var property in properties)
                {
                    if (!((double)property.GetValue(thisShapes[i])).Equals((double)property.GetValue(shapes[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static IList<int> UniqueRandomArray(int minValue, int maxValue, int count)
        {
            Random random = new Random();
            List<int> result = new List<int>(count);
            do
            {
                int value = random.Next(minValue, maxValue);
                if (!result.Contains(value))
                {
                    result.Add(value);
                }
            } while (result.Count != count);
            return result;
        }
    }

}
