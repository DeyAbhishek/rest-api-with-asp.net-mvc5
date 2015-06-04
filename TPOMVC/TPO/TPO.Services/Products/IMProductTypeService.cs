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
    public class IMProductTypeService : ServiceBase, ITpoService<IMProductTypeDto>
    {
        public int Add(IMProductTypeDto dto)
        {
            IMProductType entity = Mapper.Map<IMProductTypeDto, IMProductType>(dto);
            try
            {
                _repository.Repository<IMProductType>().Insert(entity);
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

        public List<IMProductTypeDto> GetAll()
        {
            var entities = _repository.Repository<IMProductType>().GetAll().ToList();
            return Mapper.Map<List<IMProductType>, List<IMProductTypeDto>>(entities);
        }

        public IMProductTypeDto Get(int id)
        {
            var entity = _repository.Repository<IMProductType>().GetById(id);
            return Mapper.Map<IMProductType, IMProductTypeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<IMProductType>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(IMProductTypeDto dto)
        {
            try
            {
                var entity = _repository.Repository<IMProductType>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<IMProductType>().Update(entity);
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
