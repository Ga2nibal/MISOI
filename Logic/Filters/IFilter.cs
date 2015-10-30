using System.Drawing;
using System.Threading.Tasks;

namespace Logic.Filters
{
    public interface IFilter
    {
        Task<Bitmap> AsyncFilter(Bitmap image);

        Bitmap Filter(Bitmap image);
    }
}