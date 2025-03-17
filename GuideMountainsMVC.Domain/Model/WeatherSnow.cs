using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class Snow
    {
        public double? The1h { get; set; } // Śnieg w ostatniej godzinie (w mm)
        public double? The3h { get; set; } // Śnieg w ostatnich 3 godzinach (w mm)
        public double? The24h { get; set; } // Śnieg w ostatnich 24 godzinach (w mm)
    }
}
