using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface ISkiPassTypeService
    {
        List<SkiPassType> GetAll();
        SkiPassType GetSkiPassTypeById(int id);
    }
}
