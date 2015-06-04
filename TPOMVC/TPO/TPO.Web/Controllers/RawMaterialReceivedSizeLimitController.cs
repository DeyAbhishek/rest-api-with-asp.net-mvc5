using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class RawMaterialReceivedSizeLimitController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(int? id, string code, double lowLimit, double highLimit, bool view)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                RawMaterialReceivedSizeLimitModel model = new RawMaterialReceivedSizeLimitModel();
                if (id.HasValue)
                {
                    using (RawMaterialReceivedSizeLimitService service = new RawMaterialReceivedSizeLimitService())
                    {
                        Mapper.Map(service.Get(id.Value), model);
                    }
                }
                else
                {
                    model.PlantId = CurrentPlantId;
                    model.EnteredBy = CurrentUser;
                    model.DateEntered = DateTime.Now;
                }

                model.Code = code;
                model.LowLimit = lowLimit;
                model.HighLimit = highLimit;
                model.View = view;
                model.ModifiedBy = CurrentUser;
                model.LastModified = DateTime.Now;

                using (RawMaterialReceivedSizeLimitService service = new RawMaterialReceivedSizeLimitService())
                {
                    RawMaterialReceivedSizeLimitDto dto = new RawMaterialReceivedSizeLimitDto();
                    Mapper.Map(model, dto);
                    if (id.HasValue)
                        service.Update(dto);
                    else
                        service.Add(dto);
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
        public JsonResult Delete(int id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (RawMaterialReceivedSizeLimitService service = new RawMaterialReceivedSizeLimitService())
                {
                    service.Delete(id);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllReceiptContainerResult()
        {
            List<RawMaterialReceivedSizeLimitModel> sizeLimit = new List<RawMaterialReceivedSizeLimitModel>();
            using (RawMaterialReceivedSizeLimitService service = new RawMaterialReceivedSizeLimitService())
            {
                var dtos = service.GetAllByPlantId(CurrentPlantId);
                sizeLimit.AddRange(Mapper.Map<List<RawMaterialReceivedSizeLimitDto>, List<RawMaterialReceivedSizeLimitModel>>(dtos));
            }
            return Json(sizeLimit, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetVisibleReceiptContainerResult() 
        {
            List<RawMaterialReceivedSizeLimitModel> data = new List<RawMaterialReceivedSizeLimitModel>();
            using (RawMaterialReceivedSizeLimitService svc = new RawMaterialReceivedSizeLimitService()) 
            {
                var dtos = svc.GetAllVisibleByPlantId(CurrentPlantId);
                data.AddRange(Mapper.Map<List<RawMaterialReceivedSizeLimitDto>, List<RawMaterialReceivedSizeLimitModel>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPlantList()
        {

            List<Plant> plants = new List<Plant>();
            using (Services.PlantService service = new Services.PlantService())
            {
                var dtos = service.GetAll();
                plants.AddRange(Mapper.Map<List<PlantDto>, List<Plant>>(dtos));
            }
            return Json(plants, JsonRequestBehavior.AllowGet);
        }
    }
}