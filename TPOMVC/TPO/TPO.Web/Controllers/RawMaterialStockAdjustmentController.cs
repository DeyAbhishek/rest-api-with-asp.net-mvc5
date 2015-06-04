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
    public class RawMaterialStockAdjustmentController : BaseController
    {
        //
        // GET: /RawMaterialStockAdjustment/
        [HttpGet]
        public ActionResult Index()
        {
            List<RawMaterial> rawMaterials = new List<RawMaterial>();
            using (RawMaterialService svc = new RawMaterialService())
            {
                var dtos = svc.GetAll();
                rawMaterials.AddRange(Mapper.Map<List<RawMaterialDto>, List<RawMaterial>>(dtos));
            }
            ViewBag.RawMaterialsList = rawMaterials;

            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetLots(int? rawMaterialId)
        {
            List<RawMaterialReceived> results = new List<RawMaterialReceived>();
            if (rawMaterialId.HasValue)
            {
                using (var svc = new Services.RawMaterials.RawMaterialReceivedService())
                {
                    List<RawMaterialReceivedDto> entities = svc.GetAvailable(CurrentPlantId, rawMaterialId.Value, false );
                    results.AddRange(Mapper.Map<List<RawMaterialReceivedDto>, List<Models.RawMaterialReceived>>(entities));
                }
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetUsedLots(int? rawMaterialId)
        {
            List<RawMaterialReceived> results = new List<RawMaterialReceived>();
            if (rawMaterialId.HasValue)
            {
                using (var svc = new Services.RawMaterials.RawMaterialReceivedService())
                {
                    List<RawMaterialReceivedDto> entities = svc.GetAvailable(CurrentPlantId, rawMaterialId.Value, true);
                    results.AddRange(Mapper.Map<List<RawMaterialReceivedDto>, List<Models.RawMaterialReceived>>(entities));
                }
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(int RawMaterialId, int LotNumber, int AdjustmentType, double AdjustmentAmount, int AdjustmentUoM, string AdjustmentReason)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (RawMaterialReceivedService svc = new RawMaterialReceivedService())
                    svc.AdjustStock(CurrentPlantId, RawMaterialId, LotNumber, AdjustmentType, (decimal)AdjustmentAmount, AdjustmentUoM, AdjustmentReason, this.CurrentUser);

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