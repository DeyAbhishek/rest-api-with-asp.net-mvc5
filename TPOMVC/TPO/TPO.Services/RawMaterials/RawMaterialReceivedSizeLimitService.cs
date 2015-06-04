using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialReceivedSizeLimitService : ServiceBase, ITpoService<RawMaterialReceivedSizeLimitDto>
    {
        public int Add(RawMaterialReceivedSizeLimitDto dto)
        {
            var entity = new RawMaterialReceivedSizeLimit();
            try
            {
                _repository.Repository<RawMaterialReceivedSizeLimit>().Insert(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity.ID;
        }

        public List<RawMaterialReceivedSizeLimitDto> GetAll()
        {
            var entities = _repository.Repository<Data.RawMaterialReceivedSizeLimit>().GetAll();
            if (entities != null)
            {
                return AutoMapper.Mapper.Map<List<Data.RawMaterialReceivedSizeLimit>, List<RawMaterialReceivedSizeLimitDto>>(entities.ToList());
            }
            return new List<RawMaterialReceivedSizeLimitDto>();
        }

        public List<RawMaterialReceivedSizeLimitDto> GetAllVisibleByPlantId(int plantID) 
        {
            var entities = _repository.Repository<RawMaterialReceivedSizeLimit>().GetAllBy(r => r.PlantID == plantID && r.View == true).ToList();
            return Mapper.Map<List<Data.RawMaterialReceivedSizeLimit>, List<RawMaterialReceivedSizeLimitDto>>(entities);
        }

        public List<RawMaterialReceivedSizeLimitDto> GetAllByPlantId(int plantId)
        {
            Expression<Func<Data.RawMaterialReceivedSizeLimit, bool>> filterExpression = p => p.PlantID == plantId;

            var entities = _repository.Repository<Data.RawMaterialReceivedSizeLimit>().GetAllBy(filterExpression).ToList();

            return AutoMapper.Mapper.Map<List<Data.RawMaterialReceivedSizeLimit>, List<RawMaterialReceivedSizeLimitDto>>(entities);
        }

        public RawMaterialReceivedSizeLimitDto Get(int id)
        {
            var dto = new RawMaterialReceivedSizeLimitDto();
            var entity = _repository.Repository<Data.RawMaterialReceivedSizeLimit>().GetById(id);

            return Mapper.Map(entity, dto);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialReceivedSizeLimit>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialReceivedSizeLimitDto dto)
        {
            var entity = _repository.Repository<RawMaterialReceivedSizeLimit>().GetById(dto.Id);
            Mapper.Map<RawMaterialReceivedSizeLimitDto, RawMaterialReceivedSizeLimit>(dto, entity);

            try
            {
                _repository.Repository<RawMaterialReceivedSizeLimit>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

    }
}
