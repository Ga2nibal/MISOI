using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1.Filters
{
    class MedianFilter : BaseAbstractFilter
    {
        public override Bitmap Filter(Bitmap image)
        {
            var arrR = new int[8];
            var arrG = new int[8];
            var arrB = new int[8];
            var outImage = new Bitmap(image);

            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    for (int i1 = 0; i1 < 2; i1++)
                        for (int j1 = 0; j1 < 2; j1++)
                        {
                            var p = image.GetPixel(i + i1 - 1, j + j1 - 1);

                            //arrR[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) & 0xff;
                            //arrG[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) >> 8 & 0xff;
                            //arrB[i1 * 3 + j1] = ((p.R + p.G + p.B) / 3) >> 16 & 0xff;

                            arrR[i1 * 3 + j1] = p.R;
                            arrG[i1 * 3 + j1] = p.G;
                            arrB[i1 * 3 + j1] = p.B;
                        }
                    Array.Sort(arrR);
                    Array.Sort(arrG);
                    Array.Sort(arrB);

                    outImage.SetPixel(i, j, Color.FromArgb(arrR[4], arrG[4], arrB[4]));
                }
            }
            return outImage;
        }
    }
}
