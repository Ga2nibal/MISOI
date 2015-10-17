using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Filters
{
    interface IFilter
    {
        Task<Bitmap> AsyncFilter(Bitmap image);

        Bitmap Filter(Bitmap image);
    }
}
