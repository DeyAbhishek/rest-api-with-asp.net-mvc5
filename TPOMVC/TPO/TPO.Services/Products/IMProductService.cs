using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Products
{
    public class IMProductService : ServiceBase, ITpoService<IMProductDto>
    {
        public int Add(IMProductDto dto)
        {
            dto.LastModified = DateTime.Now;
            IMProduct entity = Mapper.Map<IMProductDto, IMProduct>(dto);
            try
            {
                _repository.Repository<IMProduct>().Insert(entity);
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

        public List<IMProductDto> GetAll()
        {
            var entities = _repository.Repository<IMProduct>().GetAll().ToList();
            return Mapper.Map<List<IMProduct>, List<IMProductDto>>(entities);
        }

        public IMProductDto Get(int id)
        {
            var entity = _repository.Repository<IMProduct>().GetById(id);
            return Mapper.Map<IMProduct, IMProductDto>(entity);
        }

        public List<IMProductDto> GetByPlant(int plantID)
        {
            Expression<Func<IMProduct, bool>> filterExpression = p => p.PlantID == plantID;
            var entities = _repository.Repository<IMProduct>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<IMProduct>, List<IMProductDto>>(entities);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<IMProduct>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(IMProductDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<IMProduct>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<IMProduct>().Update(entity);
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
