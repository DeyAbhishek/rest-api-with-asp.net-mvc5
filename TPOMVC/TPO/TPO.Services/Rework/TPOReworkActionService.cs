using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Services.Core;
using TPO.Data;
using TPO.Common.DTOs;
using AutoMapper;
using System.Data.Entity.Validation;

namespace TPO.Services.Rework
{
    public class TPOReworkActionService : ServiceBaseGeneric<TPOReworkActionDto, TPOReworkAction>, ITpoService<TPOReworkActionDto>
    {
        public List<TPOReworkActionDto> GetByStartRollID(int rollID) 
        {
            var entities = _repository.Repository<TPOReworkAction>().GetAllBy(r => r.StartRollID == rollID || r.StartRoll2ID == rollID).ToList();
            return MapEntityList(entities);
        }

        public List<TPOReworkActionDto> GetProductionEntriesByLine(int lineID) 
        {
            List<TPOReworkActionDto> data = new List<TPOReworkActionDto>();
            var entities = _repository.Repository<TPOReworkAction>().GetAllBy(a => a.LineID == lineID && !a.OutputScrapID.HasValue).ToList();
            for (int i = 0; i < entities.Count; i++) 
            {
                //TODO Handle additional mappings
                var dto = MapEntity(entities[i]);
                data.Add(dto);
            }
            return data;
        }

        public List<TPOReworkActionDto> GetScrapEntriesByLine(int lineID)
        {
            List<TPOReworkActionDto> data = new List<TPOReworkActionDto>();
            var entities = _repository.Repository<TPOReworkAction>().GetAllBy(a => a.LineID == lineID && a.OutputScrapID.HasValue).ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                //TODO Handle additional mappings
                var dto = MapEntity(entities[i]);
                data.Add(dto);
            }
            return data;
        }


        public ReworkProductionEntryDto EnterReworkProduct(ReworkProductionEntryDto input)
        {
            bool proceed = true;
            string message = string.Empty;
            if (!input.StartRoll2ID.HasValue)
            {
                proceed = CheckReworkEntry(input, out message);
            }
            if (proceed) 
            {
                if (!string.IsNullOrEmpty(input.RollIn)) 
                {
                    #warning There are calls out to the archive tables in the old version of this code
                    var rollCount = _repository.Repository<TPOCProductRoll>().GetAllBy(r => r.Code == input.RollIn).Count();
                    if (rollCount > 0) 
                    {
                        proceed = false;
                        message = string.Format("Roll number {0} already in use.", input.RollIn);
                    }
                }
            }
            if (proceed) 
            {
                var lineDto = (new TPO.Services.Production.ProductionLineService()).Get(input.LineID);
                var lineTypeDTO = (new TPO.Services.Production.ProdLineTypeService()).Get(lineDto.LineTypeID);

                //Create TPOCProductRoll record
                TPOCProductRollDto cRollDto = new TPOCProductRollDto();
                cRollDto.ProductionDate = input.ProductionDate;
                cRollDto.WorkOrderID = lineDto.CurrentWorkOrderID.HasValue ? lineDto.CurrentWorkOrderID.Value : 0;
                cRollDto.LengthUoMID = input.LengthUoMID;
                cRollDto.WeightUoMID = input.WeightUoMID;
                cRollDto.ProductID = input.TPOProductID;

                TPO.Services.Products.TPOCProductRollService cProdSvc = new Products.TPOCProductRollService();
                if (string.IsNullOrEmpty(input.RollIn)) 
                {
                    input.RollIn = cProdSvc.GetNewRollOrCartonCode(cRollDto, lineDto, lineTypeDTO);
                }
                cRollDto.BatchNumber = cProdSvc.GetBatchNumber(cRollDto);
                cRollDto.DateEntered = DateTime.Now;
                cRollDto.EnteredBy = input.EnteredBy;
                cRollDto.LastModified = cRollDto.DateEntered;
                cRollDto.ModifiedBy = cRollDto.EnteredBy;

                cRollDto.ID = cProdSvc.Add(cRollDto);

                TPOReworkAction action = new TPOReworkAction()
                {
                    LineID = input.LineID,
                    ProductionDate = input.ProductionDate,
                    ShiftID = input.ShiftID,
                    StartRollID = input.StartRollID,
                    StartRoll2ID = input.StartRoll2ID,
                    OutputRollID = cRollDto.ID,
                    TypeID = input.RWType,
                    Width = input.Width,
                    WidthUoMID = input.WidthUoMID,
                    Length = input.Length,
                    LengthUoMID = input.LengthUoMID,
                    Weight = input.Weight,
                    WeightUoMID = input.WeightUoMID,
                    Comments = input.Comments,
                    DateEntered = cRollDto.DateEntered,
                    EnteredBy = cRollDto.EnteredBy,
                    LastModified = cRollDto.DateEntered,
                    ModifiedBy = cRollDto.EnteredBy,
                    PlantID = input.PlantID
                };

                if (cProdSvc.IsFlashingMasterRoll(cRollDto))
                    cProdSvc.AddToReworkList(cRollDto);
                using (TPOReworkRollService rwRollSvc = new TPOReworkRollService()) 
                {
                    rwRollSvc.CheckRollProcessed(input.StartRollID);
                    if (input.StartRoll2ID.HasValue)
                        rwRollSvc.CheckRollProcessed(input.StartRoll2ID.Value);
                }

                //TODO: Print label
            }
            input.Message = message;
            return input;
        }
        public ReworkProductionEntryDto EnterReworkScrap(ReworkProductionEntryDto input) 
        {
            bool proceed = true;
            string message = string.Empty;
            if (!input.StartRoll2ID.HasValue)
            {
                proceed = CheckReworkEntry(input, out message);
            }
            if (proceed) 
            {
                TPOReworkRollService rwRollSvc = new TPOReworkRollService();

                //Get prodLine
                var prodLine = _repository.Repository<ProdLine>().GetById(input.LineID);

                //Get master roll, if any
                TPO.Services.Products.TPOCProductRollService cProdRollSvc = new Products.TPOCProductRollService();
                var masterRoll = cProdRollSvc.GetMasterRollByCode(input.RollIn);
                int? masterRollID = masterRoll != null ? masterRoll.ID : (int?)null;
                
                //Get scrap id
                TPO.Services.Scrap.TPOLineScrapService tpoScrapSvc = new Scrap.TPOLineScrapService();
                string scrapId = tpoScrapSvc.GetNewScrapCode(input.LineID, input.ProductionDate);
                
                //Get batch number
                TPO.Services.Production.TPOBatchService batchSvc = new Production.TPOBatchService();
                int batchNumber = batchSvc.GetTPOProdBatchNumber(input.LineID, input.TPOProductID, input.EnteredBy, masterRollID);

                //Get scrap code from TPOLineScrap
                var lineScraps = tpoScrapSvc.GetByReworkRollID(input.StartRollID);
                string scrapCode = lineScraps != null && lineScraps.Count > 0  ? lineScraps[0].ScrapCode : "11e";


                var now = DateTime.Now;
                TPOLineScrapDto newScrapDto = new TPOLineScrapDto()
                {
                    Code = scrapId,
                    ProductionDate = input.ProductionDate,
                    ShiftID = input.ShiftID,
                    BatchNumber = batchNumber,
                    Length = input.Length,
                    LengthUoMID = input.LengthUoMID,
                    Weight = input.Weight,
                    WeightUoMID = input.WeightUoMID,
                    Width = input.Width,
                    WidthUoMID = input.WidthUoMID,
                    TypeID = input.RWType,
                    Comment = input.Comments,
                    WorkOrderID = prodLine.CurrentWorkOrderID.Value,
                    PlantID = input.PlantID,
                    DateEntered = now,
                    EnteredBy = input.EnteredBy,
                    LastModified = now,
                    ModifiedBy = input.EnteredBy
                };
                newScrapDto.ID = tpoScrapSvc.Add(newScrapDto);

                TPOReworkActionDto actionDto = new TPOReworkActionDto()
                {
                    LineID = input.LineID,
                    ProductionDate = input.ProductionDate,
                    ShiftID = input.ShiftID,
                    StartRollID = input.StartRollID,
                    StartRoll2ID = input.StartRoll2ID,
                    OutputRollID = masterRollID,
                    NewProductID = input.TPOProductID,
                    TypeID = input.RWType,
                    Weight = input.Weight,
                    WeightUoMID = input.WeightUoMID,
                    Width = input.Width,
                    WidthUoMID = input.WidthUoMID,
                    Length = input.Length,
                    LengthUoMID = input.LengthUoMID,
                    DateEntered = now,
                    EnteredBy = input.EnteredBy,
                    OutputScrapID = newScrapDto.ID
                };


                actionDto.ID = Add(actionDto);

                if (input.StartRoll2ID.HasValue)
                {
                    rwRollSvc.CheckRollProcessed(input.StartRoll2ID.Value);
                }
                if (input.RWType == 2) 
                {
                    //If scrap type 2, enter roll into rework table
                    TPOReworkRollDto rwRollDto = new TPOReworkRollDto()
                    {
                        Code = scrapId,
                        LineID = input.LineID,
                        TPOProductID = input.TPOProductID,
                        ShiftID = input.ShiftID,
                        Length = input.Length,
                        LengthUoMID = input.LengthUoMID,
                        DateEntered = now,
                        EnteredBy = input.EnteredBy,
                        LastModified = now,
                        ModifiedBy = input.EnteredBy
                    };
                    rwRollDto.ID = rwRollSvc.Add(rwRollDto);
                }
                if (input.RWType == 3) 
                {
                    //If scrap type 3, add roll into reclaim wip

                    TPO.Services.Reclaim.TPOReclaimActionService rcActionSvc = new Reclaim.TPOReclaimActionService();
                    rcActionSvc.EnterReclaimScrap(input.PlantID, input.LineID, input.TPOProductID, input.Weight, input.ProductionDate, scrapId, input.EnteredBy);
                }

                //TODO:  Print label

            }
            input.Message = message;
            return input;
        }

        public bool CheckReworkEntry(ReworkProductionEntryDto dto, out string message)
        {
            bool proceed = false;
            var roll = _repository.Repository<TPOReworkRoll>().GetById(dto.StartRollID);
            double areaIn = dto.Length * dto.Width;
            double rollArea = roll.Length * roll.Width;

            var actions = GetByStartRollID(dto.StartRollID);
            double actionsArea = actions.Sum(a => a.Length) * actions.Sum(a => a.Width);
            double actionsWeight = actions.Sum(a => a.Weight);

            int check = 0;
            if (areaIn > (rollArea - actionsArea)) 
            {
                check = 1;
            }
            if (dto.Weight > (roll.Weight - actionsWeight)) 
            {
                check += 2;
            }
            switch (check) 
            {
                case 1: 
                    {
                        message = string.Format("The entry has a greater area than the starting roll.\r\nEntry Area: {0}\r\nStarting Roll Remaining Area: {1}", areaIn, (rollArea - actionsArea));
                    }break;
                case 2: 
                    {
                        message = string.Format("The entry has a greater weight than the starting roll.\r\nEntry Weight: {0}\r\nStarting Roll RemainingWeight: {1}", dto.Weight, (roll.Weight - actionsWeight));
                    }break;
                case 3: 
                    {
                        message = string.Format("The entry has a greater area and weight than the starting roll.\r\nEntry Area / Weight: {0}/{1}\r\nStarting Roll Remaining Area / Weight: {2}/{3}", areaIn, dto.Weight, (rollArea - actionsArea), (roll.Weight - actionsWeight));
                    }break;
                default: 
                    {
                        message = "Ok";
                        proceed = true;
                    }break;
            }
            return proceed;
        }

        private string GetRollNumber(int lineID) 
        {
            return string.Empty;
        }

        
    }
}
