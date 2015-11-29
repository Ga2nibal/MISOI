using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Filters
{
    class RevertFilter : BaseAbstractFilter
    {
        public override Bitmap Filter(Bitmap image)
        {
            var revertImage = (Bitmap)image.Clone();
            int width = image.Width;
            int height = image.Height;

            // Update the image with the sharpened pixels.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    var sourcepixel = image.GetPixel(x, y);
                    int r = 255 - sourcepixel.R;
                    int g = 255 - sourcepixel.G;
                    int b = 255 - sourcepixel.B;
                    revertImage.SetPixel(x, y, Color.FromArgb(sourcepixel.A, r, g, b));
                }
            }

            return revertImage;
        }
    }
}
