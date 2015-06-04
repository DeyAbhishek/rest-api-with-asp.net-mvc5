using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Services.Production;
using TPO.Web.Models;
using TPO.Common.Enums;

namespace TPO.Web.Controllers
{
    public class ProductionShiftDefaultController : BaseController
    {
        // GET: ProductionShiftDefault
        public ActionResult Index(int lineId = -1)
        {
            ProductionLinesModel model = new ProductionLinesModel() {Id = lineId};

            if (lineId > 0)
                model = GetProductionLine(lineId);

            return View(model);
        }

        private ProductionLinesModel GetProductionLine(int lineId)
        {
            ProductionLinesModel model  = new ProductionLinesModel();
            using (ProductionLineService service = new ProductionLineService())
            {
                var dto = service.Get(lineId);
                model = Mapper.Map<ProductionLinesDto, ProductionLinesModel>(dto);
            }
            return model;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetLines()
        {
            List<ProductionLinesModel> productionLines = new List<ProductionLinesModel>();
            using (ProductionLineService service = new ProductionLineService())
            {
                var dto = service.GetAll().OrderBy(r => r.LineDesc).ToList();
                productionLines.AddRange(Mapper.Map<List<ProductionLinesDto>, List<ProductionLinesModel>>(dto));
            }
            return Json(productionLines, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllProductionShiftDefaults(int lineId)
        {
            List<ProductionShiftUseModel> productionShiftUse = new List<ProductionShiftUseModel>();
            using (ProductionShiftUseService service = new ProductionShiftUseService())
            {
                var dto = service.GetAllByLineId(lineId);
                productionShiftUse.AddRange(Mapper.Map<List<ProductionShiftUseDto>, List<ProductionShiftUseModel>>(dto));
                productionShiftUse.AddRange(AddMissingShifts(dto, lineId));
            }
            return Json(productionShiftUse, JsonRequestBehavior.AllowGet);
            
        }

        private IEnumerable<ProductionShiftUseModel> AddMissingShifts(List<ProductionShiftUseDto> shifts, int lineId)
        {
            List<ProductionShiftUseModel> additionalShifts = new List<ProductionShiftUseModel>();
            using (ProductionShiftService service = new ProductionShiftService())
            {
                var dtos = service.GetAllByPlantId(CurrentPlantId);
                foreach (var dto in dtos)
                {
                    if (shifts.FirstOrDefault(s => s.ShiftID == dto.ID) != null) continue;
                    ProductionShiftUseModel additionalShift = new ProductionShiftUseModel()
                    {
                        PlantId = CurrentPlantId,
                        LineID = lineId,
                        Day1Minutes = 0,
                        Day2Minutes = 0,
                        Day3Minutes = 0,
                        Day4Minutes = 0,
                        Day5Minutes = 0,
                        Day6Minutes = 0,
                        Day7Minutes = 0,
                        ShiftCode = dto.Code,
                        ShiftID = dto.ID
                    };
                    additionalShifts.Add(additionalShift);
                }
            }
            return additionalShifts;
        }

        [HttpPost]
        public JsonResult UpdateRow(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            ProductionShiftUseModel productionShiftUse = JsonConvert.DeserializeObject<ProductionShiftUseModel>(row);

            try
            {
                if (productionShiftUse != null)
                {
                    productionShiftUse.LastModified = DateTime.Now;
                    productionShiftUse.ModifiedBy = CurrentUser;
                    productionShiftUse.PlantId = CurrentPlantId;
                    ProductionShiftUseDto dto = new ProductionShiftUseDto();
                    using (ProductionShiftUseService service = new ProductionShiftUseService())
                    {
                        if (productionShiftUse.Id == 0)
                        {
                            productionShiftUse.EnteredBy = CurrentUser;
                            productionShiftUse.DateEntered = DateTime.Now;
                            Mapper.Map(productionShiftUse, dto);
                            service.Add(dto);
                        }
                        else
                        {
                            dto = service.Get(productionShiftUse.Id);
                            Mapper.Map(productionShiftUse, dto);
                            service.Update(dto);
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            productionShiftUse.ResponseMessage = responseMessage;

            return Json(productionShiftUse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetDefaultShift(int lineId)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionLineScheduleService service = new ProductionLineScheduleService())
                {
                    var dtos = service.GetAllByLineId(lineId);
                    foreach (var dto in dtos)
                        service.ResetShiftDefaults(dto);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetAllDefaultShift()
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionLineScheduleService service = new ProductionLineScheduleService())
                {
                    var dtos = service.GetAllByPlantId(CurrentPlantId);
                    foreach (var dto in dtos)
                        service.ResetShiftDefaults(dto);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetProductionLineSchedule(int lineId, string startDate)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionShiftUseService service = new ProductionShiftUseService())
                {
                    var dtos = service.GetAllByLineId(lineId);
                    foreach (var dto in dtos)
                        service.SetProductionLineSchedule(dto, startDate);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetAllProductionLineSchedule(string startDate)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionShiftUseService service = new ProductionShiftUseService())
                {
                    var dtos = service.GetAllByPlantId(CurrentPlantId);
                    foreach (var dto in dtos)
                        service.SetProductionLineSchedule(dto, startDate);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
    }
}