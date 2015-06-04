using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Application
{
    public class UnitOfMeasureTypeService : ServiceBase, ITpoService<UnitOfMeasureTypeDto>
    {
        public int Add(UnitOfMeasureTypeDto dto)
        {
            var entity = new UnitOfMeasureType();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasureType>().Insert(entity);
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

        public List<UnitOfMeasureTypeDto> GetAll()
        {
            var entities = _repository.Repository<UnitOfMeasureType>().GetAll().ToList();

            return Mapper.Map<List<UnitOfMeasureType>, List<UnitOfMeasureTypeDto>>(entities);
        }

        public UnitOfMeasureTypeDto Get(int id)
        {
            return Map(GetById(id));
        }

        private UnitOfMeasureTypeDto Map(UnitOfMeasureType entity)
        {
            return Mapper.Map<UnitOfMeasureType, UnitOfMeasureTypeDto>(entity);
        }

        private UnitOfMeasureType GetById(int id)
        {
            return _repository.Repository<UnitOfMeasureType>().GetById(id);
        }

        public UnitOfMeasureTypeDto GetByCode(string code)
        {
            var entity = _repository.Repository<UnitOfMeasureType>().GetAllBy(t => t.Code == code).FirstOrDefault();
            return Map(entity);
        }

        public UnitOfMeasureTypeDto GetByDescription(string code)
        {
            var entity = _repository.Repository<UnitOfMeasureType>().GetAllBy(t => t.Description == code).FirstOrDefault();
            return Map(entity);
        }

        public void Delete(int id)
        {
            _repository.Repository<UnitOfMeasureType>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(UnitOfMeasureTypeDto dto)
        {
            try
            {
                var entity = GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasureType>().Update(entity);
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
