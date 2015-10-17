using System.Drawing;

namespace Lab1.Filters
{
    class MonochromeFilter : BaseAbstractFilter
    {
        public int Level { get; set; }

        public override Bitmap Filter(Bitmap image)
        {
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    var color = image.GetPixel(i, j);
                    int sr = (color.R + color.G + color.B) / 3;
                    image.SetPixel(i, j, (sr < Level ? Color.Black : Color.White));
                }
            }
            return image;
        }
    }
}
