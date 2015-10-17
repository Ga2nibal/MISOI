using System.Drawing;
using System.Threading.Tasks;

namespace Lab1.Filters
{
    class BoundaryFilter : BaseAbstractFilter
    {
        public override Bitmap Filter(Bitmap image)
        {
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    if (IsBoundary(image, i, j))
                    {
                        image.SetPixel(i, j, Color.White);
                    }
                }
            }
            return image;
        }

        private int GetBrightness(Color c)
        {
            return (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
        }

        private int GetBrightness(Bitmap bitmap, int x, int y)
        {
            Color color = bitmap.GetPixel(x, y);
            return GetBrightness(color);
        }

        private bool IsBoundary(Bitmap bitmap, int x, int y)
        {
            int Black = 0;
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
    }
}
