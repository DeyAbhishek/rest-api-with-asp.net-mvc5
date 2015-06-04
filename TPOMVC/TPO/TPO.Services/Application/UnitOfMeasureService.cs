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
    public class UnitOfMeasureService : ServiceBase, ITpoService<UnitOfMeasureDto>
    {
        public int Add(UnitOfMeasureDto dto)
        {
            var entity = new UnitOfMeasure();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasure>().Insert(entity);
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

        public List<UnitOfMeasureDto> GetAll()
        {
            var entities = _repository.Repository<UnitOfMeasure>().GetAll().ToList();

            return Mapper.Map<List<UnitOfMeasure>, List<UnitOfMeasureDto>>(entities);
        }

        public List<UnitOfMeasureDto> GetAllByUoMTypeId(int typeId)
        {
            var entities = _repository.Repository<UnitOfMeasure>().GetAllBy(u => u.TypeID == typeId).ToList();

            return Mapper.Map<List<UnitOfMeasure>, List<UnitOfMeasureDto>>(entities);
        }

        public List<UnitOfMeasureDto> GetByTypeCode(string name) 
        {
            System.Linq.Expressions.Expression<Func<UnitOfMeasure, bool>> filterExpression = uom => uom.UnitOfMeasureType.Code == name;
            var entities = _repository.Repository<UnitOfMeasure>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<UnitOfMeasure>, List<UnitOfMeasureDto>>(entities);
        }

        public UnitOfMeasureDto Get(int id)
        {
            return Map(GetById(id));
        }

        private UnitOfMeasureDto Map(UnitOfMeasure entity)
        {
            return Mapper.Map<UnitOfMeasure, UnitOfMeasureDto>(entity);
        }
        
        private UnitOfMeasure GetById(int id)
        {
            return _repository.Repository<UnitOfMeasure>().GetById(id);
        }

        public UnitOfMeasureDto GetByCode(string code)
        {
            var entity = _repository.Repository<UnitOfMeasure>().GetAllBy(t => t.Code == code).FirstOrDefault();
            return Map(entity);
        }

        public UnitOfMeasureDto GetDefaultByTypeCode(string typeCode) 
        {
            var entity = _repository.Repository<UnitOfMeasureDefault>().GetAllBy(u => u.UnitOfMeasureType.Code == typeCode).FirstOrDefault();
            return Map(entity.UnitOfMeasure);
        }

        public void Delete(int id)
        {
            _repository.Repository<UnitOfMeasure>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(UnitOfMeasureDto dto)
        {
            try
            {
                var entity = GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasure>().Update(entity);
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
