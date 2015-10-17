using System.Drawing;
using System.Threading.Tasks;

namespace Lab1.Filters
{
    abstract class BaseAbstractFilter : IFilter
    {
        public Task<Bitmap> AsyncFilter(Bitmap image)
        {
            return Task.Run(() => Filter(image));
        }

        public abstract Bitmap Filter(Bitmap image);
    }
}
