namespace Logic.Filters
{
    public class FilterFactory
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

                case FilterNames.BoxFilter:
                    result = new BoxFilter();
                    break;

                case FilterNames.Revert:
                    result = new RevertFilter();
                    break;

                case FilterNames.Gray:
                    result = new GrayFilter();
                    break;
            }

            return result;
        }
    }
}