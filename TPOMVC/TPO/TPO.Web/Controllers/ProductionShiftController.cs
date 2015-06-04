using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Services.Scrim;
using TPO.Web.Models;
using TPO.Services.Production;
using TPO.Common.Enums;

namespace TPO.Web.Controllers
{
    public class ProductionShiftController : BaseController
    {
        // GET: ProductionShift
        public ActionResult Index()
        {
            return View(new ProductionShiftModel());
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllProductionShifts()
        {
            List<ProductionShiftModel> productionShifts = new List<ProductionShiftModel>();
            using (ProductionShiftService svc = new ProductionShiftService())
            {
                var dtos = svc.GetAllByPlantId(CurrentPlantId);
                productionShifts.AddRange(Mapper.Map<List<ProductionShiftDto>, List<ProductionShiftModel>>(dtos));
            }
            return Json(productionShifts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllProductionShiftTypes()
        {
            List<ProductionShiftTypeModel> productionShiftTypes = new List<ProductionShiftTypeModel>();
            using (ProductionShiftTypeService svc = new ProductionShiftTypeService())
            {
                var dtos = svc.GetAll();
                productionShiftTypes.AddRange(Mapper.Map<List<ProductionShiftTypeDto>, List<ProductionShiftTypeModel>>(dtos));
            }
            return Json(productionShiftTypes, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public JsonResult InsertShift(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            ProductionShiftModel productionShift = JsonConvert.DeserializeObject<ProductionShiftModel>(row);

            try
            {

                if (productionShift != null)
                {
                    productionShift.LastModified = DateTime.Now;
                    productionShift.DateEntered = DateTime.Now;
                    productionShift.EnteredBy = CurrentUser;
                    productionShift.ModifiedBy = CurrentUser;
                    productionShift.PlantId = CurrentPlantId;
                    productionShift.Code = LookupShiftTypeCode(productionShift.TypeID);
                    ProductionShiftDto dto = new ProductionShiftDto();
                    using (ProductionShiftService service = new ProductionShiftService())
                    {
                        Mapper.Map(productionShift, dto);
                        service.Add(dto);
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            productionShift.ResponseMessage = responseMessage;

            return Json(productionShift, JsonRequestBehavior.AllowGet);
        }

        private string LookupShiftTypeCode(int ID)
        {
            ProductionShiftTypeModel productionShiftType = new ProductionShiftTypeModel() {Code = string.Empty};
            using (ProductionShiftTypeService service = new ProductionShiftTypeService())
            {
                var dto = service.Get(ID);
                productionShiftType = Mapper.Map<ProductionShiftTypeDto, ProductionShiftTypeModel>(dto);
            }
            return productionShiftType.Code;
        }

        [HttpPost]
        public JsonResult UpdateShift(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            ProductionShiftModel productionShift = JsonConvert.DeserializeObject<ProductionShiftModel>(row);

            try
            {
                if (productionShift != null)
                {
                    productionShift.LastModified = DateTime.Now;
                    productionShift.ModifiedBy = CurrentUser;
                    productionShift.Code = LookupShiftTypeCode(productionShift.TypeID);
                    ProductionShiftDto dto = new ProductionShiftDto();
                    using (ProductionShiftService service = new ProductionShiftService())
                    {
                        dto = service.Get(productionShift.Id);
                        Mapper.Map(productionShift, dto);
                        service.Update(dto);
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            productionShift.ResponseMessage = responseMessage;

            return Json(productionShift, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteShift(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            ProductionShiftModel productionShift = JsonConvert.DeserializeObject<ProductionShiftModel>(row);

            try
            {
                if (productionShift != null && productionShift.Id > 0)
                {
                    using (ProductionShiftService service = new ProductionShiftService())
                    {
                        service.Delete(productionShift.Id);
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            productionShift.ResponseMessage = responseMessage;

            return Json(productionShift, JsonRequestBehavior.AllowGet);
        }

    }
}