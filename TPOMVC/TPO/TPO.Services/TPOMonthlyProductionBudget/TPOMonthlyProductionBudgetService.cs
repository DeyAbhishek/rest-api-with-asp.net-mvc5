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

namespace TPO.Services.TPOMonthlyProductionBudget
{
    public class TPOMonthlyProductionBudgetService : ServiceBase, ITpoService<ProductionBudgetDto>
    {


        public int Add(ProductionBudgetDto dto)
        {
            var entity = new ProductionBudget();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<ProductionBudget>().Insert(entity);
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


        public List<ProductionBudgetDto> GetAll()
        {
            var entities = _repository.Repository<ProductionBudget>().GetAll().ToList();
            return Mapper.Map<List<ProductionBudget>, List<ProductionBudgetDto>>(entities);
        }

        public ProductionBudgetDto Get(int id)
        {
            var entity = _repository.Repository<ProductionBudget>().GetById(id);
            return Mapper.Map<ProductionBudget, ProductionBudgetDto>(entity);
        }

        public List<ProductionBudgetDto> GetByYear(int typeId)
        {
            var entities = _repository.Repository<ProductionBudget>().GetAll().Where(q => q.Year == typeId).ToList();

            return Mapper.Map<List<ProductionBudget>,List < ProductionBudgetDto >> (entities);
        }

        public void Delete(int id)
        {
             throw new NotImplementedException();
        }


        public void Update(ProductionBudgetDto dto)
        {
            var entity = _repository.Repository<ProductionBudget>().GetById(dto.ID);
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<ProductionBudget>().Update(entity);
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
