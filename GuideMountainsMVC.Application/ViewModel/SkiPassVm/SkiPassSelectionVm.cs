using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class SkiPassSelectionVm
    {
        public int SkiPassId { get; set; }
        public int SkiPassTypeId { get; set; }
        public int Days { get; set; }
        public int Quantity { get; set; }
    }
}
