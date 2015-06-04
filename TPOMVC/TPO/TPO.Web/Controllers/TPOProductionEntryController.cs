using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Data;
using TPO.Services;
using TPO.Services.Downtime;
using TPO.Services.Production;
using TPO.Services.Products;
using TPO.Services.RawMaterials;
using TPO.Services.Scrap;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using SecurityTask = TPO.Common.Enums.SecurityTask;

namespace TPO.Web.Controllers
{
    public class TPOProductionEntryController : BaseController
    {
        // GET: TPOProductionEntry
        [HttpGet]
        public ActionResult Index()
        {
            TPOProductionEntryModel model = new TPOProductionEntryModel();

            try
            {
                model.ProductionDate = DateTime.Now;
                using (TPO.Services.Application.SecurityService service = new Services.Application.SecurityService())
                {
                    model.SupervisorList = new SelectList(service.GetByRole(TPO.Common.Constants.Roles.ShiftSupervisor).OrderBy(u => u.Username).ToList(), "Username", "Username");
                }
            }
            catch (Exception exc)
            {
                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = exc.Message;
                model.ResponseMessage.ActionStatus = "error";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TPOProductionEntryModel model)
        {
            try
            {
                using (TPO.Services.Application.SecurityService service = new Services.Application.SecurityService())
                {
                    model.SupervisorList = new SelectList(service.GetByRole(TPO.Common.Constants.Roles.ShiftSupervisor).OrderBy(u => u.Username).ToList(), "Username", "Username");
                }

                //model = FetchTPOFormulationGridData(model);
            }
            catch (Exception exc)
            {
                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = exc.Message;
                model.ResponseMessage.ActionStatus = "error";
            }
            return View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchProductionLines()
        {
            List<ProductionLinesDto> productionLines = new List<ProductionLinesDto>();
            using (TPO.Services.Production.ProductionLineService service = new TPO.Services.Production.ProductionLineService())
            {
                productionLines = service.GetByPlant(CurrentPlantId);
            }
            // create default DTO
            ProductionLinesDto defaultItem = new ProductionLinesDto();
            defaultItem.ID = 0;
            defaultItem.LineDesc = "--- Select Line ---";
            productionLines.Insert(0, defaultItem);


            return Json(productionLines, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchProductionMetrics(int productionLineId, int shiftId, int workOrderId, DateTime productionDate )
        {
            double completionPercentage = 90;
            double scrapPercentage = 90;
            double scrapWeight = 900;
            string scrapWeightUoM = "LB";

            /*
            using ( TPO.Services.Production.ProductionShiftService service = new Services.Production.ProductionShiftService() )
            {
                scrapPercentage = service.GetScrapPercent(CurrentPlantId, productionLineId, shiftId, productionDate);
                scrapWeight = service.GetScrapWeight(CurrentPlantId, productionLineId, shiftId, productionDate);
            }

            using (TPO.Services.Production.WorkOrderService service = new Services.Production.WorkOrderService())
            {
                completionPercentage = service.GetCompletionPercentage(CurrentPlantId, productionLineId, workOrderId);
            }
            */

            var results = new { CompletionPercentage = completionPercentage, ScrapPercentage = scrapPercentage, ScrapWeight = scrapWeight, ScrapWeightUoM = scrapWeightUoM };
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchProductionShifts()
        {
            List<ProductionShiftDto> productionShifts = new List<ProductionShiftDto>();
            using (TPO.Services.Production.ProductionShiftService service = new TPO.Services.Production.ProductionShiftService())
            {
                productionShifts = service.GetAllByPlantId(CurrentPlantId);
            }
            // create default DTO
            ProductionShiftDto defaultItem = new ProductionShiftDto();
            defaultItem.ID = 0;
            defaultItem.Code = "--- Select Shift ---";
            productionShifts.Insert(0, defaultItem);

            return Json(productionShifts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchWorkOrder(int workOrderId)
        {
            WorkOrderModel model = new WorkOrderModel();
            using (TPO.Services.Production.WorkOrderService service = new Services.Production.WorkOrderService())
            {
                var workOrder = service.Get(workOrderId);
                AutoMapper.Mapper.Map(workOrder, model);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchWorkOrders(int productionLineId)
        {
            List<WorkOrderModel> workOrders = new List<WorkOrderModel>();
            if ( productionLineId > 0 )
            { 
                using ( TPO.Services.Production.WorkOrderService service = new Services.Production.WorkOrderService())
                {
                    workOrders.AddRange(AutoMapper.Mapper.Map<List<WorkOrderDto>, List<WorkOrderModel>>(service.GetByLineID(productionLineId).OrderBy(w => w.Code).ToList()));
                }
            }
            return Json(workOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchProdComment(int? productionLineId , int? shiftId, int? workOrderId, DateTime? productionDate)
        { 
            ProdCommentModel model = new ProdCommentModel();
            if ( productionLineId.HasValue && shiftId.HasValue && workOrderId.HasValue && productionDate.HasValue)
            { 
                using ( TPO.Services.Production.ProdCommentService service = new Services.Production.ProdCommentService())
                {
                    var dto = service.GetByLineShiftWorkOrderDate(productionLineId.Value, shiftId.Value, workOrderId.Value, productionDate.Value);
                    model = AutoMapper.Mapper.Map<ProdCommentDto, ProdCommentModel>(dto);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchSupervisorOnShift(int? productionLineId, int? shiftId, int? workOrderId, DateTime? productionDate)
        {
            SupervisorOnShiftModel model = new SupervisorOnShiftModel();
            if (productionLineId.HasValue && shiftId.HasValue && workOrderId.HasValue && productionDate.HasValue)
            {
                using (TPO.Services.Production.SupervisorOnShiftService service = new Services.Production.SupervisorOnShiftService())
                {
                    var dto = service.GetByPlantLineShiftDate(productionLineId.Value, shiftId.Value, workOrderId.Value, productionDate.Value);
                    if ( dto != null )
                        model = AutoMapper.Mapper.Map<SupervisorOnShiftDto, SupervisorOnShiftModel>(dto);
                }
            }

            if ( model.Id == 0 )
            {
                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = "NoData";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult GetMasterRollComments(int masterRollId)
        {
            string comments = string.Empty;
            if ( masterRollId > 0 )
            { 
                using (TPOCProductRollService service = new TPOCProductRollService())
                {
                    var dto = service.Get(masterRollId);
                    comments = dto.Comments;
                }
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveSupervisorOnShift(int? productionLineId, int? shiftId, int? workOrderId, DateTime? productionDate, string supervisor)
        {
            SupervisorOnShiftModel model = new SupervisorOnShiftModel();
            using (TPO.Services.Production.SupervisorOnShiftService service = new Services.Production.SupervisorOnShiftService())
            {
                var dto = service.GetByPlantLineShiftDate(productionLineId.Value, shiftId.Value, workOrderId.Value, productionDate.Value);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;
                dto.SupervisorName = supervisor;
                if ( dto.ID == 0 )
                {
                    dto.LineID = productionLineId.Value;
                    dto.ShiftID = shiftId.Value;
                    dto.ProductionDate = productionDate.Value;
                    dto.EnteredBy = CurrentUser;
                    dto.DateEntered = DateTime.Now;

                    dto.ID = service.Add(dto);
                }
                else
                {
                    service.Update(dto);
                }

                // refresh and pass it back
                dto = service.Get(dto.ID);
                AutoMapper.Mapper.Map(dto, model);
            }

            return Json(model, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult SaveProdEntry(string prodEntry)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                TPOCProductRollModel productRoll = JsonConvert.DeserializeObject<TPOCProductRollModel>(prodEntry);
                productRoll.PlantId = CurrentPlantId;
                productRoll.Modified = true;
                productRoll.LastModified = DateTime.Now;
                productRoll.DateEntered = DateTime.Now;
                productRoll.EnteredBy = CurrentUser;
                productRoll.ModifiedBy = CurrentUser;
                productRoll.RawMaterialReceivedId = GetRawMaterialReceivedId(CurrentPlantId, productRoll.LotNumber);
                if (productRoll.MasterRollId == 0) productRoll.MasterRollId = null;

                using (TPOCProductRollService service = new TPOCProductRollService())
                {
                    service.EditProdEntry(Mapper.Map<TPOCProductRollModel, TPOCProductRollDto>(productRoll));
                }
                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception ex)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, ex.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveProductionEntry(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;
            try
            {
                TPOCProductRollModel productRoll = JsonConvert.DeserializeObject<TPOCProductRollModel>(row);
                using (TPOCProductRollService service = new TPOCProductRollService())
                {
                    service.Delete(productRoll.Id);
                }
                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
            }
            catch (Exception ex)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedDelete, ex.Message);
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProdEntry(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;
            try
            {
                TPOCProductRollModel productRoll = JsonConvert.DeserializeObject<TPOCProductRollModel>(row);
                if (productRoll.Id > 0)
                {
                    using (TPOCProductRollService service = new TPOCProductRollService())
                    {
                        var dto = service.Get(productRoll.Id);
                        if (dto.ProductCode != productRoll.ProductCode)
                            dto.ProductCode = productRoll.ProductCode;
                        if (dto.Code != productRoll.Code)
                            dto.Code = productRoll.Code;

                        service.Save(dto);
                    }
                    responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                }
                else
                    responseMessage = SetResponseMesssage(ActionTypeMessage.Error, "Can't make changes to new product roll");
            }
            catch (Exception ex)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedDelete, ex.Message);
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        private int GetRawMaterialReceivedId(int plantId, string lotNumber)
        {
            return (new RawMaterialReceivedService()).GetByPlantIdLotNumber(plantId, lotNumber).Id;
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult GetAllProductRolls(int lineId)
        {
            List<TPOCProductRollModel> tpoCProductRollModels = new List<TPOCProductRollModel>();
            using (TPO.Services.Products.TPOCProductRollService service = new Services.Products.TPOCProductRollService())
            {
                var dtos = service.GetByLineID(lineId);
                tpoCProductRollModels = Mapper.Map<List<TPOCProductRollDto>, List<TPOCProductRollModel>>(dtos);
            }
            return Json(tpoCProductRollModels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult GetCurrentRawMaterials(int productionLineId)
        {
            List<TPOCurrentRawMaterialModel> rawMaterialReceived = new List<TPOCurrentRawMaterialModel>();
            using (TPO.Services.RawMaterials.TPOCurrentRawMaterialService service = new Services.RawMaterials.TPOCurrentRawMaterialService())
            {
                var dtos = service.GetAllByLineID(productionLineId);
                rawMaterialReceived = Mapper.Map<List<TPOCurrentRawMaterialDto>, List<TPOCurrentRawMaterialModel>>(dtos);
            }
            return Json(rawMaterialReceived, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchWorkOrderStatus(int productionLineID)
        {
            List<ProdLine> items = new List<ProdLine>();
            using (TPO.Services.Production.ProductionLineService service = new Services.Production.ProductionLineService()) { 
                
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public TPOProductionEntryModel FetchMatrixData(TPOProductionEntryModel originalModel) 
        {
            















            return originalModel;
        }
    }
}