using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Services.RawMaterials;
using TPO.Web.Models;
using TPO.Web.ActionFilters;
using TPO.Common.Enums;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class RawMaterialQCTestsController : BaseController
    {
        //
        // GET: /RawMaterialQCTests/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetRawMaterialList()
        {
            List<RawMaterial> rawMaterials = new List<RawMaterial>();
            using (RawMaterialService svc = new RawMaterialService())
            {
                // TODO: Uncomment line below and delete call to GetAll once PlantId set properly
                //var dtos = svc.GetAllByPlantId(LocalCurrentPlantId);
                var dtos = svc.GetAll();
                rawMaterials.AddRange(Mapper.Map<List<RawMaterialDto>, List<RawMaterial>>(dtos));
            }
            return Json(rawMaterials, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult RawMaterialTest(int rawMaterialId)
        {
            RawMaterialTestModel model;
            using (RawMaterialTestService service = new RawMaterialTestService())
            {
                var dtos = service.GetByRawMatId(rawMaterialId);
                model = Mapper.Map<RawMaterialTestDto, RawMaterialTestModel>(dtos);
                if (model == null)
                {
                    model = new RawMaterialTestModel();
                    model.PlantId = CurrentPlantId;
                    model.RawMaterialId = rawMaterialId;
                }
            }
            return PartialView("QCTest", model);
        }

        [HttpPost]
        public ActionResult RawMaterialTest(int rawMaterialId, RawMaterialTestModel model)
        {
            using (RawMaterialTestService service = new RawMaterialTestService())
            {
                model.LastModified = DateTime.Now;
                model.ModifiedBy = CurrentUser;
                model.PlantId = CurrentPlantId;
                if (model.ACLimitTypeID == 0) model.ACLimitTypeID = null;
                if (model.CBLimitTypeID == 0) model.CBLimitTypeID = null;
                if (model.ColorLimitTypeID == 0) model.ColorLimitTypeID = null;
                if (model.MFLimitTypeID == 0) model.MFLimitTypeID = null;
                if (model.MoistLimitTypeID == 0) model.MoistLimitTypeID = null;

                RawMaterialTestDto dto;
                if (model.Id > 0)
                {
                    dto = service.GetByRawMatId(model.RawMaterialId);
                    Mapper.Map(model, dto);
                    if (string.IsNullOrEmpty(dto.EnteredBy))
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                    }
                    service.Update(dto);
                }
                else
                {
                    dto = new RawMaterialTestDto();
                    Mapper.Map(model, dto);
                    dto.EnteredBy = CurrentUser;
                    dto.DateEntered = DateTime.Now;
                    service.Add(dto);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AjaxTestUpdate(string row)
        {
            RawMaterial rawMaterial = JsonConvert.DeserializeObject<RawMaterial>(row);
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

            return RedirectToAction("Index");
        }
	}
}