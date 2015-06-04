using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPO.Common;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class IMProductionService : ServiceBaseGeneric<IMProdDto, IMProd>, ITpoService<IMProdDto>
    {
        public List<IMProdDto> GetByPlantLineShiftDate(int plantId, int lineId, int shiftId, DateTime productionDate)
        {
            List<IMProdDto> items = new List<IMProdDto>();
            items = GetAllByPlantId(plantId).Where(delegate(IMProdDto item) { return item.LineID == lineId && item.ShiftID == shiftId && item.ProductionDate == productionDate; }).ToList();
            return items;
        }
        public List<IMProdDto> CreateMultipleEntries(int entries, int plantId, int lineId, int shiftId, int workOrderId, DateTime productionDate, int parts, double weight, int weightUoMID, int? batchId, DateTime? adhesiveManufacturesDate, string comment, string enteredBy) 
        {
            List<IMProdDto> items = new List<IMProdDto>();
            try
            {
                for (int idx = 0; idx < entries; idx++ )
                {
                    IMProd entity = new IMProd();
                    IMProdDto dto = CreateDto(plantId, lineId, shiftId, workOrderId, productionDate, parts, weight, weightUoMID, batchId, adhesiveManufacturesDate, comment, enteredBy);
                    AutoMapper.Mapper.Map(dto, entity);

                    _repository.Repository<IMProd>().Insert(entity);
                    
                    if (entity.ID > 0)
                        items.Add(Get(entity.ID));
                }

                CommitUnitOfWork();
            }
            catch(Exception exc)
            {
                items.Clear();
                throw;
            }

            return items;
        }
        public int CreateSingleEntry(int plantId, int lineId, int shiftId, int workOrderId, DateTime productionDate, int parts, double weight, int weightUoMID, int? batchId, DateTime? adhesiveManufacturesDate, string comment, string enteredBy)
        {
            int returnValue = 0;

            IMProdDto dto = CreateDto(plantId, lineId, shiftId, workOrderId, productionDate, parts, weight, weightUoMID, batchId, adhesiveManufacturesDate, comment, enteredBy);
            returnValue = Add(dto);
            return returnValue;
        }
        public void EditSingleEntry(IMProdDto dto)
        {
            Update(dto);
            //TODO: Something with the label writer
        }
    
        private IMProdDto CreateDto(int plantId, int lineId, int shiftId, int workOrderId, DateTime productionDate, int parts, double weight, int weightUoMID, int? batchId, DateTime? adhesiveManufacturesDate, string comment, string enteredBy)
        {
            IMProdDto dto = new IMProdDto();
            dto.PlantID = plantId;
            dto.LineID = lineId;
            dto.ShiftID = shiftId;
            dto.ProductionDate = productionDate;
            dto.WorkOrderID = workOrderId;
            dto.PartsCarton = parts;
            dto.CartonWeight = weight;
            dto.WeightUoMID = weightUoMID;
            dto.Comment = comment;
            dto.AdhesionManufacturesDate = adhesiveManufacturesDate;

            dto.BatchID = batchId;
            dto.BatchNumber = 0;
            dto.MasterRollID = null;
            dto.RawMaterialReceivedID = 0;

            dto.DateEntered = DateTime.Now;
            dto.EnteredBy = enteredBy;
            dto.LastModified = DateTime.Now;
            dto.ModifiedBy = enteredBy;

            FillInAssociatedValues(dto);

            return dto;
        }

        private void FillInAssociatedValues(IMProdDto dto)
        {
            // fill in codes, id's , etc.
            using (Products.TPOCProductRollService service = new Products.TPOCProductRollService())
            {
                if (dto.WeightUoMID == 0)
                    dto.WeightUoMID = service.GetUomId(dto.WeightUoM);
                if (dto.IMProductID == 0 && dto.WorkOrderID > 0)
                    dto.IMProductID = service.GetIMProductId(dto);
                if (!dto.BatchID.HasValue)
                    dto.BatchNumber = service.GetBatchNumber(dto);
                if (string.IsNullOrWhiteSpace(dto.Code))
                    dto.Code = service.GetNewRollOrCartonCode(dto);
            }
        }

        public int  CreateEntries(IMProdDto dto)
        {
            int returnValue = 0;
            FillInAssociatedValues(dto);
            returnValue = Add(dto);
            return returnValue;
        }
    }
}
