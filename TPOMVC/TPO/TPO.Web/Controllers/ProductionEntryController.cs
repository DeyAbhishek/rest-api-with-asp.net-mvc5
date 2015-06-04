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
using TPO.Services.Rework;
using TPO.Services.Scrap;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

using SecurityTask = TPO.Common.Enums.SecurityTask;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ProductionEntryController : BaseController
    {
        //
        // GET: /ProductionEntry/
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            ProductionEntryViewModel model = new ProductionEntryViewModel();
            model.ProductionLineId = id;
            model.ProductionLineTypeCode = string.Empty;

            if (model.ProductionLineId > 0) 
            {
                model.ProductionDate = DateTime.Today;
                using (TPO.Services.Production.ProdLineTypeService typeSvc = new Services.Production.ProdLineTypeService()) 
                {
                    var typeDto = typeSvc.GetByLineID(id);
                    if (typeDto != null) 
                    {
                        model.ProductionLineTypeCode = typeDto.ProdLineTypeCode;
                        model.ProductionLineTypeDesc = typeDto.ProdLineTypeDesc;
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchProductionComment(int? productionLineId, int? shiftId, int? workOrderId, DateTime? productionDate)
        {
            ProdCommentModel model = new ProdCommentModel();
            if (productionLineId.HasValue && shiftId.HasValue && workOrderId.HasValue && productionDate.HasValue)
            {
                using (TPO.Services.Production.ProdCommentService service = new Services.Production.ProdCommentService())
                {
                    var dto = service.GetByLineShiftWorkOrderDate(productionLineId.Value, shiftId.Value, workOrderId.Value, productionDate.Value);
                    model = AutoMapper.Mapper.Map<ProdCommentDto, ProdCommentModel>(dto);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
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
        public JsonResult FetchProductionMetrics(int productionLineId, int shiftId, int workOrderId, DateTime productionDate)
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
        public JsonResult FetchSupervisorOnShift(int? productionLineId, int? shiftId, DateTime? productionDate)
        {
            SupervisorOnShiftModel model = new SupervisorOnShiftModel();
            if (productionLineId.HasValue && shiftId.HasValue && productionDate.HasValue)
            {
                using (TPO.Services.Production.SupervisorOnShiftService service = new Services.Production.SupervisorOnShiftService())
                {
                    var dto = service.GetByPlantLineShiftDate(CurrentPlantId, productionLineId.Value, shiftId.Value, productionDate.Value);
                    model = AutoMapper.Mapper.Map<SupervisorOnShiftDto, SupervisorOnShiftModel>(dto);
                }
                if (model == null)
                    model = new SupervisorOnShiftModel();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult FetchMasterRolls(int productionLineId, int shiftId, DateTime productionDate, int workOrderId = 0)
        {
            List<TPOCProductRoll> items = new List<TPOCProductRoll>();
            using (TPOCProductRollService service = new TPOCProductRollService())
            {
                var dtos = service.GetMasterRollsByShift(productionLineId, shiftId, productionDate).Where(delegate(TPOCProductRollDto d) { return workOrderId == 0 ? true : d.WorkOrderID == workOrderId; });
                AutoMapper.Mapper.Map(dtos, items);
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchSupervisors()
        {
            List<UserModel> supervisors = new List<UserModel>();
            using (TPO.Services.Application.SecurityService service = new Services.Application.SecurityService())
            {
                supervisors = AutoMapper.Mapper.Map<List<UserDto>, List<UserModel>>(service.GetByRole(TPO.Common.Constants.Roles.ShiftSupervisor).OrderBy(u => u.Username).ToList());
            }
            return Json(supervisors, JsonRequestBehavior.AllowGet);
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
            if (productionLineId > 0)
            {
                using (TPO.Services.Production.WorkOrderService service = new Services.Production.WorkOrderService())
                {
                    workOrders.AddRange(AutoMapper.Mapper.Map<List<WorkOrderDto>, List<WorkOrderModel>>(service.GetByLineID(productionLineId).OrderBy(w => w.Code).ToList()));
                }
            }
            return Json(workOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
        public JsonResult FetchWorkOrderStatus(int productionLineID)
        {
            List<ProdLine> items = new List<ProdLine>();
            using (TPO.Services.Production.ProductionLineService service = new Services.Production.ProductionLineService())
            {

            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSupervisorOnShift(int? productionLineId, int? shiftId, int? workOrderId, DateTime? productionDate, string supervisor)
        {
            SupervisorOnShiftModel model = new SupervisorOnShiftModel();
            using (TPO.Services.Production.SupervisorOnShiftService service = new Services.Production.SupervisorOnShiftService())
            {
                var dto = service.GetByPlantLineShiftDate(CurrentPlantId, productionLineId.Value, shiftId.Value, productionDate.Value);
                if (dto == null)
                    dto = new SupervisorOnShiftDto();
                
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;
                dto.SupervisorName = supervisor;
                
                if (dto.ID == 0)
                {
                    dto.PlantID = CurrentPlantId;
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
        public JsonResult SaveProdComment(int? productionLineId, int? shiftId, int? workOrderId, DateTime? productionDate, string comment)
        {
            ProdCommentModel model = new ProdCommentModel();
            using (TPO.Services.Production.ProdCommentService service = new Services.Production.ProdCommentService())
            {
                var dto = service.GetByLineShiftWorkOrderDate(productionLineId.Value, shiftId.Value, workOrderId.Value, productionDate.Value);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;
                dto.Comment = comment;

                if (dto.ID == 0)
                {
                    dto.LineID = productionLineId.Value;
                    dto.ShiftID = shiftId.Value;
                    dto.WorkOrderID = workOrderId.Value;
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

	}
}