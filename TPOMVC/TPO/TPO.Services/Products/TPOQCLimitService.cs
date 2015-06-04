using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TPO.Services.Core;
using TPO.Common.DTOs;
using TPO.Data;
using AutoMapper;
using System.Data.Entity.Validation;

namespace TPO.Services.Products
{
    public class TPOQCLimitService : ServiceBase, ITpoService<TPOQCLimitDto>
    {
        public int Add(TPOQCLimitDto dto)
        {
            throw new NotImplementedException("Use TPOProductService instead.");
        }

        public List<TPOQCLimitDto> GetAll()
        {
            throw new NotImplementedException("Use TPOProductService instead.");
        }

        public TPOQCLimitDto Get(int id)
        {
            var entity = _repository.Repository<TPOProduct>().GetById(id);
            var dto = Mapper.Map<TPOProduct, TPOQCLimitDto>(entity);
            dto.Code = entity.ProductCode;
            dto.ProductDescription = entity.ProductDesc;
            return dto;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Use TPOProductService instead.");
        }

        public void Update(TPOQCLimitDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOProduct>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOProduct>().Update(entity);
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
