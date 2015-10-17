namespace Lab1.Filters
{
    class FilterFactory
    {
        public static IFilter Create(string filterKey)
        {
            IFilter result = null;
            switch (filterKey)
            {
                case FilterNames.MediumFilter:
                    result = new MedianFilter();
                    break;

                case FilterNames.MonochromeFilter:
                    result = new MonochromeFilter();
                    break;

                case FilterNames.SharpenFilter:
                    result = new SharpenFilter();
                    break;

                case FilterNames.BoundaryFilter:
                    result = new BoundaryFilter();
                    break;
            }

            return result;
        }
    }
}
