using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Services.Application;
using TPO.Services.Downtime;
using TPO.Services.Formulation;
using TPO.Services.Production;
using TPO.Services.Products;
using TPO.Services.Scrap;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    public class WorkOrderShiftDataController : BaseController
    {
        [HttpGet]
        public JsonResult GetWorkOrderShiftData(int lineID, int shiftID, int workOrderID, DateTime prodDate) 
        {
            WorkOrderShiftDataModel model = new WorkOrderShiftDataModel();

            //Set default units of measure
            using (UnitOfMeasureService svc = new UnitOfMeasureService()) 
            {
                var lengthDefault = svc.GetDefaultByTypeCode("L");
                if (lengthDefault != null)
                    model.LengthUnitOfMeasure = lengthDefault.Code;
                var weightDefault = svc.GetDefaultByTypeCode("W");
                if (weightDefault != null)
                    model.WeightUnitOfMeasure = weightDefault.Code;
            }

            //Get production line
            ProductionLinesDto lineDto = null;
            using (ProductionLineService lineSvc = new ProductionLineService())
            {
                lineDto = lineSvc.Get(lineID);
            }
            if (lineDto != null)
            {
                List<TPOCProductRollDto> rollDtos = new List<TPOCProductRollDto>();
                using (TPOCProductRollService rollSvc = new TPOCProductRollService())
                {
                    if (lineDto.TPOMorC == "M")
                    {
                        //Get master rolls
                        rollDtos.AddRange(rollSvc.GetMasterRollsByShift(lineID, shiftID, prodDate));
                    }
                    else
                    {
                        //Get child rolls
                        rollDtos.AddRange(rollSvc.GetChildRollsByShift(lineID, shiftID, prodDate));
                    }
                }
                //Filter for rolls specific to the work order
                var workOrderRolls = rollDtos.Where(r => r.WorkOrderID == workOrderID).ToList();
                model.RollCount1 = workOrderRolls.Count;
                model.Length1 = workOrderRolls.Sum(r => r.Length);
                model.Weight1 = workOrderRolls.Sum(r => r.Weight);
                model.ShiftRollWeight = rollDtos.Sum(r => r.Weight);

                //Get scraps
                List<TPOLineScrapDto> scrapDtos = new List<TPOLineScrapDto>();
                using (TPOLineScrapService scrapSvc = new TPOLineScrapService())
                {
                    scrapDtos.AddRange(scrapSvc.GetByShift(shiftID, prodDate));
                }
                //Filter for scraps specific to the work order
                var woScraps = scrapDtos.Where(s => s.WorkOrderID == workOrderID).ToList();
                for (int i = 0; i < woScraps.Count; i++) 
                {
                    switch (woScraps[i].TPOLineScrapTypeDescription) 
                    {
                        case "Second": 
                            {
                                model.Length2 += woScraps[i].Length;
                                model.Weight2 += woScraps[i].Weight;
                            }break;
                        case "Third": 
                            {
                                model.Length3 += woScraps[i].Length;
                                model.Weight3 += woScraps[i].Weight;
                            }break;
                        case "Fourth": 
                            {
                                model.Length4 += woScraps[i].Length;
                                model.Weight4 += woScraps[i].Weight;
                            }break;
                    }
                }
                model.ShiftScrapWeight = scrapDtos.Sum(s => s.Weight);

                //Get downtime
                List<DownTimeDto> dtDtos = new List<DownTimeDto>();
                using (DownTimeService dtSvc = new DownTimeService()) 
                {
                    dtDtos.AddRange(dtSvc.GetByShift(lineID, shiftID, prodDate));
                }

                var woDowntime = dtDtos.Where(dt => dt.WorkOrderID == workOrderID).ToList();
                model.DownTimeMinutes = woDowntime.Sum(dt => dt.DownTimeMinutes);
                model.ShiftDownTimeMinutes = dtDtos.Sum(dt => dt.DownTimeMinutes);

                //Get production line schedule
                List<ProductionLineScheduleDto> plSchedDtos = new List<ProductionLineScheduleDto>();
                using (ProductionLineScheduleService plSchedSvc = new ProductionLineScheduleService()) 
                {
                    plSchedDtos.AddRange(plSchedSvc.GetByShift(lineID, shiftID, prodDate));
                }

                model.ScheduledRunTime = plSchedDtos.Sum(s => s.MinutesScheduled);

                //Check if feeder entry forms need to be used
                if (lineDto.LineDescCode == "TPO")
                {
                    model.RMUse = true;
                    List<TPOFormulationLineProductDto> formDtos = new List<TPOFormulationLineProductDto>();
                    using (TPOFormulationLineProductService formSvc = new TPOFormulationLineProductService())
                    {
                        formDtos.AddRange(formSvc.GetByWorkOrder(lineID, workOrderID));
                    }
                    if (formDtos.Count > 0)
                    {
                        model.Form = formDtos.First().TPOFormulationID;
                        model.Extruders = formDtos.First().TPOFormulationExtruders;
                    }

                    List<WorkOrderShiftDataDto> eorDtos = new List<WorkOrderShiftDataDto>();
                    using (WorkOrderShiftDataService eorSvc = new WorkOrderShiftDataService())
                    {
                        eorDtos.AddRange(eorSvc.GetByShift(lineID, shiftID, prodDate));

                        if (eorDtos.Count == 0)
                        {
                            eorSvc.Add(new WorkOrderShiftDataDto()
                            {
                                LineID = lineID,
                                ProductionDate = prodDate,
                                ShiftID = shiftID,
                                WorkOrderID = workOrderID,
                                PlantID = CurrentPlantId,
                                EnteredBy = CurrentUser,
                                DateEntered = DateTime.Now,
                                ModifiedBy = CurrentUser,
                                LastModified = DateTime.Now
                            });
                        }
                        else
                        {
                            model.ScrimA = eorDtos.First().ScrimAreaUsed;
                            model.ScrimW = eorDtos.First().ScrimWeightUsed;
                            model.Resin = eorDtos.First().DrainedResin;
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(int lineID, int shiftID, int workOrderID, DateTime prodDate, double scrimA, double scrimW, double resin) 
        {
            WorkOrderShiftDataDto model = new WorkOrderShiftDataDto();
            List<WorkOrderShiftDataDto> eorDtos = new List<WorkOrderShiftDataDto>();
            using (WorkOrderShiftDataService eorSvc = new WorkOrderShiftDataService())
            {
                model = eorSvc.GetAll().Where(q => q.LineID == lineID && q.ShiftID == shiftID && q.WorkOrderID == workOrderID && q.ProductionDate == prodDate).ToList().FirstOrDefault();

                model.ScrimAreaUsed = scrimA;
                model.ScrimWeightUsed = scrimW;
                model.DrainedResin = resin;

                eorSvc.Update(model);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetIMWorkOrderShiftData(int lineID, int shiftID)
        {
            WorkOrderShiftDataIMModel model = new WorkOrderShiftDataIMModel();


            //WorkOrderShiftDataModel model = new WorkOrderShiftDataModel();

            ////Set default units of measure
            //using (UnitOfMeasureService svc = new UnitOfMeasureService())
            //{
            //    var lengthDefault = svc.GetDefaultByTypeCode("L");
            //    if (lengthDefault != null)
            //        model.LengthUnitOfMeasure = lengthDefault.Code;
            //    var weightDefault = svc.GetDefaultByTypeCode("W");
            //    if (weightDefault != null)
            //        model.WeightUnitOfMeasure = weightDefault.Code;
            //}

            ////Get production line
            //ProductionLinesDto lineDto = null;
            //using (ProductionLineService lineSvc = new ProductionLineService())
            //{
            //    lineDto = lineSvc.Get(lineID);
            //}
            //if (lineDto != null)
            //{
            //    List<TPOCProductRollDto> rollDtos = new List<TPOCProductRollDto>();
            //    using (TPOCProductRollService rollSvc = new TPOCProductRollService())
            //    {
            //        if (lineDto.TPOMorC == "M")
            //        {
            //            //Get master rolls
            //            rollDtos.AddRange(rollSvc.GetMasterRollsByShift(lineID, shiftID, prodDate));
            //        }
            //        else
            //        {
            //            //Get child rolls
            //            rollDtos.AddRange(rollSvc.GetChildRollsByShift(lineID, shiftID, prodDate));
            //        }
            //    }
            //    //Filter for rolls specific to the work order
            //    var workOrderRolls = rollDtos.Where(r => r.WorkOrderID == workOrderID).ToList();
            //    model.RollCount1 = workOrderRolls.Count;
            //    model.Length1 = workOrderRolls.Sum(r => r.Length);
            //    model.Weight1 = workOrderRolls.Sum(r => r.Weight);
            //    model.ShiftRollWeight = rollDtos.Sum(r => r.Weight);

            //    //Get scraps
            //    List<TPOLineScrapDto> scrapDtos = new List<TPOLineScrapDto>();
            //    using (TPOLineScrapService scrapSvc = new TPOLineScrapService())
            //    {
            //        scrapDtos.AddRange(scrapSvc.GetByShift(shiftID, prodDate));
            //    }
            //    //Filter for scraps specific to the work order
            //    var woScraps = scrapDtos.Where(s => s.WorkOrderID == workOrderID).ToList();
            //    for (int i = 0; i < woScraps.Count; i++)
            //    {
            //        switch (woScraps[i].TPOLineScrapTypeDescription)
            //        {
            //            case "Second":
            //                {
            //                    model.Length2 += woScraps[i].Length;
            //                    model.Weight2 += woScraps[i].Weight;
            //                } break;
            //            case "Third":
            //                {
            //                    model.Length3 += woScraps[i].Length;
            //                    model.Weight3 += woScraps[i].Weight;
            //                } break;
            //            case "Fourth":
            //                {
            //                    model.Length4 += woScraps[i].Length;
            //                    model.Weight4 += woScraps[i].Weight;
            //                } break;
            //        }
            //    }
            //    model.ShiftScrapWeight = scrapDtos.Sum(s => s.Weight);

            //    //Get downtime
            //    List<DownTimeDto> dtDtos = new List<DownTimeDto>();
            //    using (DownTimeService dtSvc = new DownTimeService())
            //    {
            //        dtDtos.AddRange(dtSvc.GetByShift(lineID, shiftID, prodDate));
            //    }

            //    var woDowntime = dtDtos.Where(dt => dt.WorkOrderID == workOrderID).ToList();
            //    model.DownTimeMinutes = woDowntime.Sum(dt => dt.DownTimeMinutes);
            //    model.ShiftDownTimeMinutes = dtDtos.Sum(dt => dt.DownTimeMinutes);

            //    //Get production line schedule
            //    List<ProductionLineScheduleDto> plSchedDtos = new List<ProductionLineScheduleDto>();
            //    using (ProductionLineScheduleService plSchedSvc = new ProductionLineScheduleService())
            //    {
            //        plSchedDtos.AddRange(plSchedSvc.GetByShift(lineID, shiftID, prodDate));
            //    }

            //    model.ScheduledRunTime = plSchedDtos.Sum(s => s.MinutesScheduled);

            //    //Check if feeder entry forms need to be used
            //    if (lineDto.LineDescCode == "TPO")
            //    {
            //        model.RMUse = true;
            //        List<TPOFormulationLineProductDto> formDtos = new List<TPOFormulationLineProductDto>();
            //        using (TPOFormulationLineProductService formSvc = new TPOFormulationLineProductService())
            //        {
            //            formDtos.AddRange(formSvc.GetByWorkOrder(lineID, workOrderID));
            //        }
            //        if (formDtos.Count > 0)
            //        {
            //            model.Form = formDtos.First().TPOFormulationID;
            //            model.Extruders = formDtos.First().TPOFormulationExtruders;
            //        }

            //        List<WorkOrderShiftDataDto> eorDtos = new List<WorkOrderShiftDataDto>();
            //        using (WorkOrderShiftDataService eorSvc = new WorkOrderShiftDataService())
            //        {
            //            eorDtos.AddRange(eorSvc.GetByShift(lineID, shiftID, prodDate));

            //            if (eorDtos.Count == 0)
            //            {
            //                eorSvc.Add(new WorkOrderShiftDataDto()
            //                {
            //                    LineID = lineID,
            //                    ProductionDate = prodDate,
            //                    ShiftID = shiftID,
            //                    WorkOrderID = workOrderID,
            //                    PlantID = CurrentPlantId,
            //                    EnteredBy = CurrentUser,
            //                    DateEntered = DateTime.Now,
            //                    ModifiedBy = CurrentUser,
            //                    LastModified = DateTime.Now
            //                });
            //            }
            //            else
            //            {
            //                model.ScrimA = eorDtos.First().ScrimAreaUsed;
            //                model.ScrimW = eorDtos.First().ScrimWeightUsed;
            //                model.Resin = eorDtos.First().DrainedResin;
            //            }
            //        }
            //    }
            //}
            return Json(model, JsonRequestBehavior.AllowGet);
        }
	}
}