using System.Drawing;

namespace Logic.Filters
{
    public class BoxFilter : BaseAbstractFilter
    {
        private readonly int[] _dX = {1, -1, -1};
        private readonly int[] _dY = {-1, -1, 1};
        private readonly int[] _kernel = {-1, 9, -1};
        private int _power = 20;

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public Bitmap ConvertToBitmap(Color[] pixels, int width, int height)
        {
            var result = new Bitmap(width, height);

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    result.SetPixel(j, i, pixels[(i*result.Width) + j]);
                }
            }

            return result;
        }

        public override Bitmap Filter(Bitmap image)
        {
            if (_power >= 0 && _power < 20)
            {
                _kernel[1] = _power;
            }

            int height = image.Height;
            int width = image.Width;
            var temp = new Color[height*width];


            int denominator = 0;
            for (int i = 0; i < _kernel.Length; i++)
            {
                denominator += _kernel[i];
            }
            if (denominator == 0) denominator = 1;


            int red, green, blue, ired, igreen, iblue;
            Color rgb;

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    red = green = blue = 0;

                    for (int k = 0; k < _kernel.Length; k++)
                    {
                        rgb = image.GetPixel(j + _dX[k], i + _dY[k]);
                        red += rgb.R*_kernel[k];
                        green += rgb.G*_kernel[k];
                        blue += rgb.B*_kernel[k];
                    }

                    ired = red/denominator;
                    igreen = green/denominator;
                    iblue = blue/denominator;

                    if (ired > 255)
                        ired = 255;
                    else if (ired < 0) ired = 0;

                    if (igreen > 255)
                        igreen = 255;
                    else if (igreen < 0) igreen = 0;

                    if (iblue > 255)
                        iblue = 255;
                    else if (iblue < 0) iblue = 0;

                    temp[(i*width) + j] = Color.FromArgb(255, ired, igreen, iblue);
                }
            }

            return ConvertToBitmap(temp, width, height);
        }
    }
}