using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services
{
    public class PlantService : ServiceBase, ITpoService<PlantDto>
    {
        public int Add(PlantDto dto)
        {
            throw new NotImplementedException();
        }

        public List<PlantDto> GetAll()
        {
            var entities = _repository.Repository<Plant>().GetAll().ToList();
            return Mapper.Map<List<Plant>, List<PlantDto>>(entities);
        }

        public PlantDto Get(int id)
        {
            var entity = _repository.Repository<Plant>().GetById(id);
            return Mapper.Map<Plant, PlantDto>(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PlantDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
