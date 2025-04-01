using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.Services
{
    public class SkiPassTypeService : ISkiPassTypeService
    {
        private readonly ISkiPassTypeRepository _skiPassTypeRepository;

        public SkiPassTypeService(ISkiPassTypeRepository skiPassTypeRepository)
        {
            _skiPassTypeRepository = skiPassTypeRepository;
        }

        public List<SkiPassType> GetAll()
        {
            return _skiPassTypeRepository.GetAll().ToList();
        }

        public SkiPassType GetSkiPassTypeById(int id)
        {
            return _skiPassTypeRepository.GetById(id);
        }
    }
}