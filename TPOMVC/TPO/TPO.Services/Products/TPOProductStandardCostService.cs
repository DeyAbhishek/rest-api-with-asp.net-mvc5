using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Data;
using TPO.Common.DTOs;
using TPO.Services.Core;
using System.Data.Entity.Validation;


namespace TPO.Services.Products
{
    public class TPOProductStandardCostService : ServiceBase, ITpoService<TPOProductStandardCostDto>
    {
        public int Add(TPOProductStandardCostDto dto)
        {
            var entity = Mapper.Map<TPOProductStandardCostDto, TPOProductStandardCost>(dto);
            try
            {
                _repository.Repository<TPOProductStandardCost>().Insert(entity);
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

        public List<TPOProductStandardCostDto> GetAll()
        {
            var entities = _repository.Repository<TPOProductStandardCost>().GetAll().ToList();
            return Mapper.Map<List<TPOProductStandardCost>, List<TPOProductStandardCostDto>>(entities);
        }

        public List<TPOProductStandardCostDto> GetByProductID(int tpoProductID) 
        {
            var entities = _repository.Repository<TPOProductStandardCost>().GetAllBy(r => r.TPOProductID == tpoProductID).ToList();
            return Mapper.Map<List<TPOProductStandardCost>, List<TPOProductStandardCostDto>>(entities);
        }

        public TPOProductStandardCostDto Get(int id)
        {
            var entity = _repository.Repository<TPOProductStandardCost>().GetById(id);
            return Mapper.Map<TPOProductStandardCost, TPOProductStandardCostDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOProductStandardCost>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOProductStandardCostDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOProductStandardCost>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOProductStandardCost>().Update(entity);
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
