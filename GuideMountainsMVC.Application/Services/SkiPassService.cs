﻿using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;

namespace GuideMountainsMVC.Application.Services
{
    public class SkiPassService : ISkiPassService
    {
        private readonly ISkiPassRepository _skiPassRepository;
        private readonly ISkiPassTypeRepository _skiPassTypeRepository;
        private readonly IMapper _mapper;

        public SkiPassService(ISkiPassRepository skiPassRepository, IMapper mapper, ISkiPassTypeRepository skiPassTypeRepository)
        {
            _skiPassRepository = skiPassRepository;
            _mapper = mapper;
            _skiPassTypeRepository = skiPassTypeRepository;
        }

        public ListSkiPassVm GetAllSkiPasses(int pageSize, int currentPage, string searchString)
        {
            var query = _skiPassRepository.GetAllSkiPasses();

            // Oblicz liczbę wszystkich rekordów
            var totalCount = query.Count();

            // Pobierz SkiPassy dla aktualnej strony
            var skiPasses = query.Skip((currentPage - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            // Stwórz obiekt widoku
            var viewModel = new ListSkiPassVm
            {
                SkiPasses = _mapper.Map<List<SkiPassForListVm>>(skiPasses),
                CurrentPage = currentPage,
                PageSize = pageSize,
                Count = totalCount
            };

            return viewModel;
        }

        public SkiPassDetailVm GetSkiPassDetail(int id)
        {
            var skiPass = _skiPassRepository.GetSkiPassById(id);
            return _mapper.Map<SkiPassDetailVm>(skiPass);
        }

        public int AddSkiPass(NewSkiPassVm newSkiPass)
        {
            var skiPass = _mapper.Map<SkiPass>(newSkiPass);

            if (newSkiPass.ImageContent != null)
            {
                skiPass.Image = newSkiPass.Image; // ✅ Poprawnie przypisujemy tutaj!
            }

            _skiPassRepository.AddSkiPass(skiPass);
            return skiPass.Id;
        }
        public void UpdateSkiPass(NewSkiPassVm updatedSkiPass)
        {
            var skiPass = _mapper.Map<SkiPass>(updatedSkiPass);
            _skiPassRepository.UpdateSkiPass(skiPass);
        }

        public void DeleteSkiPass(int id)
        {
            var skiPass = _skiPassRepository.GetSkiPassById(id);
            if (skiPass != null)
            {
                _skiPassRepository.DeleteSkiPass(skiPass);
            }
        }
        public ListSkiPassVm GetSkiPassByCountryId(int countryForeignKey)
        {
            // Pobieramy miejsca powiązane z krajem
            var places = _skiPassRepository.GetByCountryId(countryForeignKey).ToList();

            // Mapowanie danych na ViewModel (ListMountainPlaceVm)
            var model = new ListSkiPassVm
            {
                SkiPasses = places.Select(mp => new SkiPassForListVm
                {
                    Id = mp.Id,
                    Description = mp.Description,
                    CountryId = mp.CountryId,
                    Image = mp.Image,
                }).ToList()
            };

            return model;
        }
        public List<SkiPassTypeVm> GetAllSkiPassTypes()
        {
            var types = _skiPassRepository.GetAllSkiPassTypes();
            return types.Select(t => new SkiPassTypeVm
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
        }
        public SkiPassTypeVm GetSkiPassTypeById(int id)
        {
            var type = _skiPassTypeRepository.GetById(id); // zakładam, że masz ten repo
            return _mapper.Map<SkiPassTypeVm>(type);
        }

        public SkiPassVm GetSkiPassById(int id)
        {
            var entity = _skiPassRepository.GetSkiPassById(id);
            return _mapper.Map<SkiPassVm>(entity);
        }
        public ListSkiPassVm GetSkiPassesByMountainPlaceId(int mountainPlaceId)
        {
            var skiPasses = _skiPassRepository.GetByMountainPlaceId(mountainPlaceId).ToList();

            var model = new ListSkiPassVm
            {
                SkiPasses = _mapper.Map<List<SkiPassForListVm>>(skiPasses)
            };

            return model;
        }

    }
}
