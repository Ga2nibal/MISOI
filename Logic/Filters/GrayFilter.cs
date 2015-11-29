using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Filters
{
    class GrayFilter : BaseAbstractFilter
    {
        public Bitmap ConvertToBitmap(Color[] pixels, int width, int height)
        {
            var result = new Bitmap(width, height);

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    result.SetPixel(j, i, pixels[(i * result.Width) + j]);
                }
            }

            return result;
        }

        public override Bitmap Filter(Bitmap image)
        {
            Bitmap bmap = (Bitmap)image.Clone();
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    byte gray = (byte)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            return bmap;
        }
    }
}
