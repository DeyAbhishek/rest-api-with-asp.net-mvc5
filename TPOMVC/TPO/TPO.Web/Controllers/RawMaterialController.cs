using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class RawMaterialController : BaseController
    {
        public ActionResult Index()
        {
            return View(new RawMaterial() { PlantId = CurrentPlantId});
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllRawMaterialResult(int? rows, int? page)
        {
            rows = rows ?? DefaultPageSize;
            page = page ?? DefaultPage;

            int total;
            List<RawMaterial> rawMaterials = new List<RawMaterial>();
            using (RawMaterialService svc = new RawMaterialService())
            {
                var dtos = svc.GetAllByPlantId(CurrentPlantId);
                total = dtos.Count;
                var currentPageDtos = dtos.OrderBy(r => r.Id).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToList();
                rawMaterials.AddRange(Mapper.Map<List<RawMaterialDto>, List<RawMaterial>>(currentPageDtos));
            }
            return BuildJsonResult(rawMaterials, total);
            //return Json(rawMaterials, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllWeightUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("W");
        }
        [HttpGet]
        public JsonResult GetAllUoMResultByCode(string code)
        {
            UnitOfMeasureTypeModel uomType =
                Mapper.Map<UnitOfMeasureTypeDto, UnitOfMeasureTypeModel>((new UnitOfMeasureTypeService()).GetByCode(code));
            if (uomType != null)
            {
                // TODO: what if uomType is null?
                List<UnitOfMeasureModel> uoms = new List<UnitOfMeasureModel>();
                using (UnitOfMeasureService service = new UnitOfMeasureService())
                {
                    var dtos = service.GetAllByUoMTypeId(uomType.Id);
                    uoms.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dtos));
                }
                return Json(uoms, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetAllRawMaterialVendorResult()
        {
            List<RawMaterialVendorModel> rawMaterialVendors = new List<RawMaterialVendorModel>();
            using (RawMaterialVendorService svc = new RawMaterialVendorService())
            {
                var dtos = svc.GetByPlantId(CurrentPlantId);
                rawMaterialVendors.AddRange(Mapper.Map<List<RawMaterialVendorDto>, List<RawMaterialVendorModel>>(dtos));
            }
            return Json(rawMaterialVendors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxRawMaterialUpdate(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            RawMaterial rawMaterial = JsonConvert.DeserializeObject<RawMaterial>(row);

            try
            {
                if (rawMaterial != null)
                {
                    rawMaterial.LastModified = DateTime.Now;
                    RawMaterialDto dto = new RawMaterialDto();
                    using (RawMaterialService service = new RawMaterialService())
                    {
                        Mapper.Map(rawMaterial, dto);
                        if (rawMaterial.Id > 0)
                            service.Update(dto);
                        else
                        {
                            dto.PlantId = CurrentPlantId;
                            service.Add(dto);
                        }
                    }
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
        public JsonResult AjaxRawMaterialDelete(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            RawMaterial rawMaterial = JsonConvert.DeserializeObject<RawMaterial>(row);

            try
            {
                if (rawMaterial != null)
                {
                    RawMaterialDto dto = new RawMaterialDto();
                    using (RawMaterialService service = new RawMaterialService())
                    {
                        Mapper.Map(rawMaterial, dto);
                        if (rawMaterial.Id > 0)
                            service.Delete(dto.Id);
                    }
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