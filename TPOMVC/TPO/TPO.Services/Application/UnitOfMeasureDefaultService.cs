using System.Data.Entity.Validation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Application
{
    public class UnitOfMeasureDefaultService : ServiceBase, ITpoService<UnitOfMeasureDefaultDto>
    {
        //not implemented
        public int Add(UnitOfMeasureDefaultDto dto)
        {
            //var entity = new UnitOfMeasureDefault();
            //try
            //{
            //    Mapper.Map(dto, entity);
            //    _repository.Repository<UnitOfMeasureDefault>().Insert(entity);
            //    _repository.Save();
            //}
            //catch (Exception ex)
            //{
            //    // TODO: Handle error                
            //}
            //return entity.ID;

            throw new NotImplementedException();
        }

        public List<UnitOfMeasureDefaultDto> GetAll()
        {
            var entities = _repository.Repository<UnitOfMeasureDefault>().GetAll().ToList();

            return Mapper.Map<List<UnitOfMeasureDefault>, List<UnitOfMeasureDefaultDto>>(entities);
        }

        public List<UnitOfMeasureDefaultDto> GetAllByUoMTypeId(int typeId)
        {
            var entities = _repository.Repository<UnitOfMeasureDefault>().GetAllBy(u => u.UoMTypeID == typeId).ToList();

            return Mapper.Map<List<UnitOfMeasureDefault>, List<UnitOfMeasureDefaultDto>>(entities);
        }

        public List<UnitOfMeasureDefaultDto> GetAllByPlantId(int plantId)
        {
            var entities = _repository.Repository<UnitOfMeasureDefault>().GetAllBy(u => u.PlantID == plantId).ToList();

            return Mapper.Map<List<UnitOfMeasureDefault>, List<UnitOfMeasureDefaultDto>>(entities);
        }

        //not implemented
        public List<UnitOfMeasureDefaultDto> GetByTypeName(string name)
        {
            //System.Linq.Expressions.Expression<Func<UnitOfMeasureDefault, bool>> filterExpression = uom => uom.UnitOfMeasureType.Code == name;
            //var entities = _repository.Repository<UnitOfMeasureDefault>().GetAllBy(filterExpression).ToList();
            //return Mapper.Map<List<UnitOfMeasureDefault>, List<UnitOfMeasureDefaultDto>>(entities);

            throw new NotImplementedException();
        }

        public UnitOfMeasureDefaultDto Get(int id)
        {
            return Map(GetById(id));
        }

        private UnitOfMeasureDefaultDto Map(UnitOfMeasureDefault entity)
        {
            return Mapper.Map<UnitOfMeasureDefault, UnitOfMeasureDefaultDto>(entity);
        }

        private UnitOfMeasureDefault GetById(int id)
        {
            return _repository.Repository<UnitOfMeasureDefault>().GetById(id);
        }

        //not implemented
        public UnitOfMeasureDefaultDto GetByCode(string code)
        {
            //var entity = _repository.Repository<UnitOfMeasureDefault>().GetAllBy(t => t.Code == code).FirstOrDefault();

            //return Map(entity);
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _repository.Repository<UnitOfMeasureDefault>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(UnitOfMeasureDefaultDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                var entity = GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasureDefault>().Update(entity);
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
