using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface ISkiPassTypeRepository
    {
        IEnumerable<SkiPassType> GetAll();
        SkiPassType GetById(int id);

    }
}