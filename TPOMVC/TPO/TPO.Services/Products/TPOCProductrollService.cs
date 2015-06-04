using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPO.Common.DTOs;
using TPO.Common.Interfaces;

using TPO.Data;
using TPO.Services.Core;
using AutoMapper;
using TPO.Services.Production;
using TPO.Services.Formulation;
using TPO.Services.Application;
using TPO.Services.Scrim;
using TPO.Services.RawMaterials;

namespace TPO.Services.Products
{
    public class TPOCProductRollService : ServiceBaseGeneric<TPOCProductRollDto, TPOCProductRoll>, ITpoService<TPOCProductRollDto>
    {
        public TPOCProductRollDto GetMasterRollByCode(string code) 
        {
            var entity = _repository.Repository<TPOCProductRoll>().GetAllBy(r => r.Code == code && !r.MasterRollID.HasValue).FirstOrDefault();
            return MapEntity(entity);
        }
        public List<TPOCProductRollDto> GetByLineID(int productionLineId)
        {
            return MapEntityList(_repository.Repository<TPOCProductRoll>().GetAllBy(p => p.LineID == productionLineId));
        }

        public List<TPOCProductRollDto> GetMasterRollsByShift(int lineID, int shiftID, DateTime productionDate) 
        {
            var entities = _repository.Repository<TPOCProductRoll>().GetAllBy(r => r.LineID == lineID && r.ProductionDate == productionDate && r.ShiftID == shiftID  && !r.MasterRollID.HasValue).ToList();
            return Mapper.Map<List<TPOCProductRoll>, List<TPOCProductRollDto>>(entities);
        }

        public void EditProdEntry(TPOCProductRollDto tpoCProductRollDto)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(tpoCProductRollDto.Code))
            {
                count = _repository.Repository<TPOCProductRoll>().GetAllBy(r => r.Code == tpoCProductRollDto.Code).Count();
                // can't add a new roll with same name as one that exists
                if (count > 0 && tpoCProductRollDto.ID == 0)
                    tpoCProductRollDto.Code = string.Empty;
            }

            ProductionLinesDto prodLineDto = GetProdLineDto(tpoCProductRollDto.LineID);
            ProdLineTypeDto prodLineTypeDto = GetProdLineTypeDto(prodLineDto.LineTypeID);


            if (string.IsNullOrEmpty(tpoCProductRollDto.Code))
            {
                tpoCProductRollDto.Code = GetNewRollOrCartonCode(tpoCProductRollDto, prodLineDto, prodLineTypeDto);
            }

            tpoCProductRollDto.ProductID = GetTPOProductId(tpoCProductRollDto);
            tpoCProductRollDto.BatchNumber = GetBatchNumber(tpoCProductRollDto);
            tpoCProductRollDto.LengthUoMID = GetUomId(tpoCProductRollDto.LengthUoM);
            tpoCProductRollDto.WeightUoMID = GetUomId(tpoCProductRollDto.WeightUoM);

            TPOCProductRoll entity;
            if (tpoCProductRollDto.ID == 0)
            {
                entity = new TPOCProductRoll();
                Mapper.Map(tpoCProductRollDto, entity);
                _repository.Repository<TPOCProductRoll>().Insert(entity);
            }
            else
            {
                entity = GetById(tpoCProductRollDto.ID);
                _repository.Repository<TPOCProductRoll>().Update(entity);
            }
                
            if (IsFlashingMasterRoll(tpoCProductRollDto))
                AddToReworkList(tpoCProductRollDto);

            if (prodLineTypeDto.ProdLineTypeCode == "TPO" || prodLineTypeDto.ProdLineTypeCode == "CO")
                UpdateScrimUsageEntry(tpoCProductRollDto);

            CommitUnitOfWork();
        }

        private void UpdateScrimUsageEntry(TPOCProductRollDto tpoCProductRollDto)
        {
            if (tpoCProductRollDto.MasterRollID.HasValue)
                UpdateScrimUsageEntryForMasterRoll(tpoCProductRollDto);
            else
                UpdateScrimUsageEntryForContractorRoll(tpoCProductRollDto, 1);
        }

        private void UpdateScrimUsageEntryForContractorRoll(TPOCProductRollDto tpoCProductRollDto, double divisor)
        {
            DateTime now = DateTime.Now;
            TPOBatch batch = _repository.Repository<TPOBatch>().GetAllBy(b => b.BatchNumber == tpoCProductRollDto.BatchNumber && b.IsScrim == true).FirstOrDefault();
            ScrimRoll roll = _repository.Repository<ScrimRoll>().GetById(batch.ScrimRollID);
            ScrimActionType type = _repository.Repository<ScrimActionType>().GetAllBy(t => t.Code == "PR" && t.PlantID == tpoCProductRollDto.PlantID).FirstOrDefault();

            double actionLength = (double)ConvertUoM(tpoCProductRollDto.Length, tpoCProductRollDto.LengthUoMID, roll.LengthUoMID) / divisor;
            double actionWeight = (double)ConvertUoM(tpoCProductRollDto.Weight, tpoCProductRollDto.WeightUoMID, roll.WeightUoMID) / divisor;

            ScrimAction action = new ScrimAction() {
                ActionDate = now,
                ActionLength = actionLength,
                ActionReasonText = "Production Entry",
                ActionWeight = actionWeight,
                DateEntered = now,
                EndLength = roll.Length - actionLength,
                EndWeight = roll.Weight - actionWeight,
                EnteredBy = tpoCProductRollDto.ModifiedBy,
                FleeceProd = false,
                LastModified = now,
                LineID = tpoCProductRollDto.LineID,
                ModifiedBy = tpoCProductRollDto.ModifiedBy,
                PlantID = tpoCProductRollDto.PlantID,
                ReasonID = null,
                RollID = null,
                ScrimRollID = roll.ID,
                ShiftID = tpoCProductRollDto.ShiftID,
                StartLength = roll.Length,
                StartWeight = roll.Weight,
                TypeID = type.ID
            };

            _repository.Repository<ScrimAction>().Insert(action);

        }

        private decimal ConvertUoM(double measurement, int oldUoMID, int newUoMId)
        {
            return (new UoMConversionService()).ConvertUoM(oldUoMID, (decimal)measurement, newUoMId);
        }

        private void UpdateScrimUsageEntryForMasterRoll(TPOCProductRollDto tpoCProductRoll)
        {
            List<TPOCProductRollDto> rollsInMaster = GetByMasterRollId(tpoCProductRoll.MasterRollID.Value);
            int divisor = rollsInMaster.Count;

            foreach (TPOCProductRollDto rollInMaster in rollsInMaster)
                UpdateScrimUsageEntryForContractorRoll(rollInMaster, divisor);
        }

        private List<TPOCProductRollDto> GetByMasterRollId(int masterRollId)
        {
            return MapEntityList(_repository.Repository<TPOCProductRoll>().GetAllBy(r => r.MasterRollID == masterRollId));
        }

        public void AddToReworkList(TPOCProductRollDto tpoCProductRollDto)
        {
            // TODO: Implement me
            throw new NotImplementedException();
        }

        public bool IsFlashingMasterRoll(TPOCProductRollDto tpoCProductRollDto)
        {
            // TODO: Implement me
            return false;
        }

        public int GetUomId(string code)
        {
            return (new UnitOfMeasureService()).GetByCode(code).ID;
        }

        public int GetTPOProductId(IProductEntryDto tpoCProductRollDto)
        {
            WorkOrderDto workOrder = (new WorkOrderService()).Get(tpoCProductRollDto.WorkOrderID);
            if (workOrder.TPOProductID.HasValue)
                return workOrder.TPOProductID.Value;
            else
                return workOrder.IMProductID.Value;
        }

        public int GetIMProductId(IProductEntryDto tpoCProductRollDto)
        {
            WorkOrderDto workOrder = (new WorkOrderService()).Get(tpoCProductRollDto.WorkOrderID);
            if (workOrder.IMProductID.HasValue)
                return workOrder.IMProductID.Value;
            else
                return workOrder.TPOProductID.Value;
        }

        public int GetBatchNumber(IProductEntryDto tpoCProductRollDto)
        {
            int batchNumber = 0;
            if (tpoCProductRollDto.MasterRollID.HasValue)
            {
                TPOCProductRollDto masterRoll = Get(tpoCProductRollDto.MasterRollID.Value);
                return masterRoll.BatchNumber;
            }
            else
            {
                bool newBatch = false;
                TPOBatchService batchService = new TPOBatchService();
                List<TPOBatchDto> batch = batchService.GetByLineID(tpoCProductRollDto.LineID);
                if (batch == null || batch.Count == 0)
                    newBatch = true;
                else
                    batchNumber = batch[0].BatchNumber;

                TPOFormulationLineProductDto formulationLineProduct = GetFormulationLineProduct(tpoCProductRollDto);
                newBatch = CheckNewBatchRequirements(tpoCProductRollDto, newBatch, batchService, batch, formulationLineProduct);

                if (newBatch)
                    return GetNewBatch(tpoCProductRollDto, ++batchNumber, formulationLineProduct.TPOFormulationID);
                else
                    return batch[0].BatchNumber;
            }
        }

        private bool CheckNewBatchRequirements(IProductEntryDto tpoCProductRollDto, bool newBatch, TPOBatchService batchService, List<TPOBatchDto> batch, TPOFormulationLineProductDto formulationLineProduct)
        {
            if (!newBatch)
                newBatch = CheckFormulationForBatch(newBatch, batch, formulationLineProduct);

            if (!newBatch)
                newBatch = CheckScrimRollCountsForBatch(tpoCProductRollDto, newBatch, batchService, batch);

            if (!newBatch)
                newBatch = CheckScrimRollsForBatch(tpoCProductRollDto, newBatch);

            if (!newBatch)
                newBatch = CheckRawMaterialCountsForBatch(tpoCProductRollDto, newBatch, batchService, batch);

            if (!newBatch)
                newBatch = CheckRawMaterialsForBatch(tpoCProductRollDto, newBatch);
            return newBatch;
        }

        private bool CheckRawMaterialsForBatch(IProductEntryDto tpoCProductRollDto, bool newBatch)
        {
            int rawMaterialsOfThisLot = GetCurrentRawMaterialCountForThisLot(tpoCProductRollDto);
            if (rawMaterialsOfThisLot == 0)
                newBatch = true;
            return newBatch;
        }

        private bool CheckRawMaterialCountsForBatch(IProductEntryDto tpoCProductRollDto, bool newBatch, TPOBatchService batchService, List<TPOBatchDto> batch)
        {
            int oldRawMaterialCount = GetRawMaterialCountForBatch(batchService, batch);
            int newRawMaterialCount = GetTPOCurrentRawMaterialsCount(tpoCProductRollDto);
            if (oldRawMaterialCount != newRawMaterialCount)
                newBatch = true;
            return newBatch;
        }

        private bool CheckScrimRollsForBatch(IProductEntryDto tpoCProductRollDto, bool newBatch)
        {
            int scrimsOfThisLot = GetCurrentScrimCountForThisLot(tpoCProductRollDto);
            if (scrimsOfThisLot == 0)
                newBatch = true;
            return newBatch;
        }

        private bool CheckScrimRollCountsForBatch(IProductEntryDto tpoCProductRollDto, bool newBatch, TPOBatchService batchService, List<TPOBatchDto> batch)
        {
            int oldScrimCount = GetScrimCountForBatch(batchService, batch);
            int newScrimCount = GetTPOCurrentScrimCount(tpoCProductRollDto);
            if (oldScrimCount != newScrimCount)
                newBatch = true;
            return newBatch;
        }

        private bool CheckFormulationForBatch(bool newBatch, List<TPOBatchDto> batch, TPOFormulationLineProductDto formulationLineProduct)
        {
            if (formulationLineProduct.TPOFormulationID != batch[0].FormulationID)
                newBatch = true;
            return newBatch;
        }

        private int GetNewBatch(IProductEntryDto tpoCProductRollDto, int batchNumber, int formulationID)
        {
            List<TPOCurrentRawMaterial> rmList = _repository.Repository<TPOCurrentRawMaterial>().GetAllBy(r => r.LineID == tpoCProductRollDto.LineID).ToList();

            DateTime now = DateTime.Now;
            foreach (TPOCurrentRawMaterial rm in rmList)
            {
                TPOBatch batch = new TPOBatch()
                {
                    BatchNumber = batchNumber,
                    DateEntered = now,
                    EnteredBy = tpoCProductRollDto.ModifiedBy,
                    FormulationID = formulationID,
                    IsScrim = false,
                    LastModified = now,
                    LineID = tpoCProductRollDto.LineID,
                    ModifiedBy = tpoCProductRollDto.ModifiedBy,
                    PlantID = tpoCProductRollDto.PlantID,
                    RawMaterialID = rm.RawMaterialReceived.RawMaterialID,
                    RawMaterialReceivedID = rm.RawMaterialReceivedID.Value,
                    ScrimRollID = null
                };
                _repository.Repository<TPOBatch>().Insert(batch);
            }

            List<TPOCurrentScrim> scrimList = _repository.Repository<TPOCurrentScrim>().GetAllBy(s => s.ProdLineID == tpoCProductRollDto.LineID && s.ScrimPos != "NA").ToList();

            foreach (TPOCurrentScrim scrim in scrimList)
            {
                if (scrim.Scrim1RollID != null)
                {
                    TPOBatch batch = new TPOBatch()
                    {
                        BatchNumber = batchNumber,
                        DateEntered = now,
                        EnteredBy = tpoCProductRollDto.ModifiedBy,
                        FormulationID = formulationID,
                        IsScrim = true,
                        LastModified = now,
                        LineID = tpoCProductRollDto.LineID,
                        ModifiedBy = tpoCProductRollDto.ModifiedBy,
                        PlantID = tpoCProductRollDto.PlantID,
                        ScrimRollID = scrim.Scrim1RollID.Value,
                        RawMaterialID = null,
                        RawMaterialReceivedID = null
                    };
                    _repository.Repository<TPOBatch>().Insert(batch);
                }
                if (scrim.Scrim2RollID != null)
                {
                    TPOBatch batch = new TPOBatch()
                    {
                        BatchNumber = batchNumber,
                        DateEntered = now,
                        EnteredBy = tpoCProductRollDto.ModifiedBy,
                        FormulationID = formulationID,
                        IsScrim = true,
                        LastModified = now,
                        LineID = tpoCProductRollDto.LineID,
                        ModifiedBy = tpoCProductRollDto.ModifiedBy,
                        PlantID = tpoCProductRollDto.PlantID,
                        ScrimRollID = scrim.Scrim2RollID.Value,
                        RawMaterialID = null,
                        RawMaterialReceivedID = null
                    };
                    _repository.Repository<TPOBatch>().Insert(batch);
                }
            }
            return batchNumber;
        }

        private int GetCurrentRawMaterialCountForThisLot(IProductEntryDto tpoCProductRollDto)
        {
            TPOEntities context = new TPOEntities();

            var x = from b in context.TPOBatches
                    join cr in context.TPOCurrentRawMaterials
                        on new { l = b.LineID, rr = b.RawMaterialReceivedID.Value }
                        equals new { l = cr.LineID, rr = cr.RawMaterialReceivedID.Value }
                    select cr;

            return x.Count();

        }

        private int GetTPOCurrentRawMaterialsCount(IProductEntryDto tpoCProductRollDto)
        {
            return (new TPOCurrentRawMaterialService()).GetAllByLineID(tpoCProductRollDto.LineID).Count();
        }

        private int GetRawMaterialCountForBatch(TPOBatchService batchService, List<TPOBatchDto> batch)
        {
            return batch.Count(s => s.IsScrim == false);
        }

        private int GetCurrentScrimCountForThisLot(IProductEntryDto tpoCProductRollDto)
        {
            TPOEntities context = new TPOEntities();
            var x = from b in context.TPOBatches
                    join cs in context.TPOCurrentScrims
                        on b.ScrimRollID equals cs.Scrim1RollID
                    select cs;

            return x.Count();
            
        }

        private int GetTPOCurrentScrimCount(IProductEntryDto tpoCProductRollDto)
        {
            return (new TPOCurrentScrimService()).GetByLineID(tpoCProductRollDto.LineID).Where(s => s.ScrimPos != "NA").Count();
        }

        private int GetScrimCountForBatch(TPOBatchService batchService, List<TPOBatchDto> batch)
        {
            return batch.Count(s => s.IsScrim == true);
        }

        private TPOFormulationLineProductDto GetFormulationLineProduct(IProductEntryDto tpoCProductRollDto)
        {
            return (new TPOFormulationLineProductService()).GetByPlantIdProductId(tpoCProductRollDto.PlantID, tpoCProductRollDto.ProductID);
        }

        private ProdLineTypeDto GetProdLineTypeDto(int lineTypeId)
        {
            return (new ProdLineTypeService()).Get(lineTypeId);
        }

        private ProductionLinesDto GetProdLineDto(int lineId)
        {
            return (new ProductionLineService()).Get(lineId);
        }

        public string GetNewRollOrCartonCode(IProductEntryDto dto)
        {
            ProductionLinesDto productionLinesDto = GetProdLineDto(dto.LineID);
            ProdLineTypeDto prodLineTypeDto = GetProdLineTypeDto(productionLinesDto.LineTypeID);
            return GetNewRollOrCartonCode(dto, productionLinesDto, prodLineTypeDto);
        }

        public string GetNewRollOrCartonCode(IProductEntryDto tpoCProductRollDto, ProductionLinesDto prodLineDto, ProdLineTypeDto prodLineTypeDto)
        {
            switch (prodLineTypeDto.ProdLineTypeCode)
            {
                case "TPO":
                    if (prodLineDto.TPOMorC == "M")
                        return GetMasterRollNumber(tpoCProductRollDto, prodLineDto);
                    else if (!tpoCProductRollDto.MasterRollID.HasValue)
                        return GetChildRollNumber(tpoCProductRollDto, prodLineDto);
                    else
                        return GetMasterChildRollNumber(tpoCProductRollDto, prodLineDto);
                    break;
                case "WI":
                    return GetMasterChildRollNumber(tpoCProductRollDto, prodLineDto);
                    break;
                case "RW":
                    return GetChildRollNumber(tpoCProductRollDto, prodLineDto);
                    break;
                case "IM":
                    return GetIMRollNumber(tpoCProductRollDto, prodLineDto);
                    break;
                case "CO":
                    return GetChildRollNumber(tpoCProductRollDto, prodLineDto);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown Production Line Type");
            }
        }

        private string GetIMRollNumber(IProductEntryDto tpoCProductRollDto, ProductionLinesDto prodLineDto)
        {
            bool old = false;
            string newLotNumber = GetNewLotNumber(tpoCProductRollDto);

            if (tpoCProductRollDto.RawMaterialReceivedID != 0)
            {
                try
                {
                    RawMaterialReceived rm = _repository.Repository<RawMaterialReceived>().GetById(tpoCProductRollDto.RawMaterialReceivedID);
                    if (rm.LotNumber.Substring(0, 7) != newLotNumber)
                        old = true;
                }
                catch
                {
                    old = true;
                }
            }

            WorkOrder workOrder = _repository.Repository<WorkOrder>().GetById(tpoCProductRollDto.WorkOrderID);

            string newProdCode = string.Empty;

            if (old)
                newProdCode = FillOldIMLot(newLotNumber, workOrder);
            else
                newProdCode = FillCurrentIMLot(newLotNumber, workOrder);
            return newProdCode;
        }

        private string FillOldIMLot(string newLotNumber, WorkOrder workOrder)
        {
            string newProdCode = string.Empty;
            int cartonsUsed = workOrder.IMProds.Count();
            string maxProdCode = workOrder.IMProds.Max(p => p.Code);
            int maxCode;
            if (cartonsUsed > 0)
            {
                if (!int.TryParse(maxProdCode.Substring(maxProdCode.Length - 3), out maxCode) || maxCode > cartonsUsed)
                    maxCode = 10000;
            }
            else
                maxCode = 10000;

            if (maxCode < 10000)
            {
                newProdCode = newLotNumber + maxCode.ToString("000");
            }
            else
            {
                newProdCode = FillCurrentIMLot(newLotNumber, workOrder);
            }
            return newProdCode;
        }

        private string FillCurrentIMLot(string newLotNumber, WorkOrder workOrder)
        {
            string newProdCode = string.Empty;
            int cartonsUsed = workOrder.IMProds.Count();
            string maxProdCode = workOrder.IMProds.Max(p => p.Code);
            int maxCode = 0;
            try
            {
                if (cartonsUsed > 0)
                {
                    if (!int.TryParse(maxProdCode.Substring(maxProdCode.Length - 3), out maxCode) || maxCode > cartonsUsed)
                        int.TryParse(maxProdCode.Substring(maxProdCode.Length - 5), out maxCode);
                    else
                    {
                        int.TryParse(maxProdCode.Substring(8, 2), out maxCode);
                        maxCode++;
                        maxCode *= 1000;
                    }

                }
            }
            catch
            {
                maxCode = 1000;
            }
            if (maxCode <= 0)
                maxCode = 1000;
            newProdCode = newLotNumber + maxCode.ToString("00000");
            return newProdCode;
        }

        private string GetNewLotNumber(IProductEntryDto tpoCProductRoll)
        {
            return tpoCProductRoll.ProductionDate.DayOfYear.ToString("000");
        }

        private string GetMasterChildRollNumber(IProductEntryDto tpoCProductRollDto, ProductionLinesDto prodLineDto)
        {
            int count = _repository.Repository<TPOCProductRoll>().GetAllBy(pr => pr.MasterRollID == tpoCProductRollDto.MasterRollID).Count();
            count++;
            return GetMasterRollNumber(tpoCProductRollDto, prodLineDto) + count.ToString("00");
        }

        private string GetChildRollNumber(IProductEntryDto tpoCProductRollDto, ProductionLinesDto prodLineDto)
        {
            // TODO: FIXME
            string masterRollNumber = GetMasterRollNumber(tpoCProductRollDto, prodLineDto);
            int count = _repository.Repository<TPOCProductRoll>().GetAllBy(pr => pr.Code.StartsWith(masterRollNumber)).Count();
            count++;
            return masterRollNumber + count.ToString("00");
        }

        private string GetMasterRollNumber(IProductEntryDto tpoCProductRollDto, ProductionLinesDto prodLineDto)
        {
            return prodLineDto.LabelID.ToString("00") +
                (tpoCProductRollDto.ProductionDate.Year % 100).ToString("00") +
                tpoCProductRollDto.ProductionDate.DayOfYear.ToString("000") + "01";
        }

        public List<TPOCProductRollDto> GetChildRollsByShift(int lineID, int shiftID, DateTime productionDate)
        {
            var entities = _repository.Repository<TPOCProductRoll>().GetAllBy(r => r.LineID == lineID && r.ProductionDate == productionDate && r.ShiftID == shiftID && r.MasterRollID.HasValue == true).ToList();
            return Mapper.Map<List<TPOCProductRoll>, List<TPOCProductRollDto>>(entities);
        }
    }
}
