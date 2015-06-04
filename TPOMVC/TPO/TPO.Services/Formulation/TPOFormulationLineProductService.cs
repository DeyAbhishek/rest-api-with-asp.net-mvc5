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

namespace TPO.Services.Formulation
{
    public class TPOFormulationLineProductService : ServiceBase, ITpoService<TPOFormulationLineProductDto>
    {
        public int Add(TPOFormulationLineProductDto dto)
        {
            var entity = Mapper.Map<TPOFormulationLineProductDto, TPOFormulationLineProduct>(dto);
            try
            {
                _repository.Repository<TPOFormulationLineProduct>().Insert(entity);
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

        public List<TPOFormulationLineProductDto> GetAll()
        {
            var entities = _repository.Repository<TPOFormulationLineProduct>().GetAll().ToList();
            return Mapper.Map<List<TPOFormulationLineProduct>, List<TPOFormulationLineProductDto>>(entities);
        }

        public List<TPOFormulationLineProductDto> GetByWorkOrder(int lineID, int workOrderID) 
        {
            var workOrder = _repository.Repository<WorkOrder>().GetById(workOrderID);
            List<TPOFormulationLineProductDto> data = new List<TPOFormulationLineProductDto>();
            if (workOrder.TPOProduct != null) 
            {
                data.AddRange(Mapper.Map<List<TPOFormulationLineProduct>, List<TPOFormulationLineProductDto>>(workOrder.TPOProduct.TPOFormulationLineProducts.ToList()));
            }
            return data;
        }

        public TPOFormulationLineProductDto Get(int id)
        {
            var entity = _repository.Repository<TPOFormulationLineProduct>().GetById(id);
            return Mapper.Map<TPOFormulationLineProduct, TPOFormulationLineProductDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOFormulationLineProduct>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOFormulationLineProductDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOFormulationLineProduct>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOFormulationLineProduct>().Update(entity);
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

        public TPOFormulationLineProductDto GetByPlantIdProductId(int plantId, int productId)
        {
            var entity = _repository.Repository<TPOFormulationLineProduct>().GetAllBy(f => f.PlantID == plantId && f.TPOProductID == productId).FirstOrDefault();
            return Mapper.Map<TPOFormulationLineProduct, TPOFormulationLineProductDto>(entity);
        }
    }
}
