using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Formulation;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOFormulationController : BaseController
    {
        //
        // GET: /TPOFormulation/
        [HttpGet]
        public ActionResult Index(int id=0)
        {
            if (id == 0) 
            {
                return View(new TPOFormulationModel());
            }
            using (var service = new TPOFormulationService())
            {
                TPOFormulationModel model =
                    Mapper.Map<TPOFormulationDto, TPOFormulationModel>(service.Get(id));

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(TPOFormulationModel model)
        {
            int Id;
            if (ModelState.IsValid)
            {
                TPOFormulationDto dto = Mapper.Map<TPOFormulationModel, TPOFormulationDto>(model);

            using (var service = new TPOFormulationService())
            {
                if (model.Id > 0)
                {
                    Id = model.Id;
                    service.Update(dto);
                }
                else
                {
                    dto.Extruders = 0;
                    dto.PlantID = CurrentPlantId;
                    dto.Description = "New Formulation";
                    service.Add(dto);
                    Id = service.GetAll().Last().ID;
                }
            }
            SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                SetResponseMesssage(ActionTypeMessage.FailedSave);
                return View(model);
            }
            return RedirectToAction("Index", new { id = Id });
        }


        [HttpPost]
        public JsonResult AjaxTypeUpdate(string row, int formulationID, int extruderID)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                TPOFormulationRawMaterialModel formulation = JsonConvert.DeserializeObject<TPOFormulationRawMaterialModel>(row);
                if (formulation != null)
                {
                    formulation.LastModified = DateTime.Now;

                    TPOFormulationRawMaterialDto dto = new TPOFormulationRawMaterialDto();
                    using (TPOFormulationRawMaterialService service = new TPOFormulationRawMaterialService())
                    {
                        Mapper.Map(formulation, dto);
                        dto.PlantID = CurrentPlantId;
                        dto.TPOFormulationID = formulationID;
                        dto.TPOExtruderID = extruderID;
                        using (RawMaterialService rawMaterialService = new RawMaterialService())
                        {
                            dto.RawMaterialCode = rawMaterialService.Get(formulation.RawMaterialID).Code;
                        }
                        if (formulation.ID > 0)
                        {
                            service.Update(dto);
                        }
                        else
                        {
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
        public JsonResult AjaxTypeDelete(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                TPOFormulationRawMaterialModel formulation = JsonConvert.DeserializeObject<TPOFormulationRawMaterialModel>(row);
                if (formulation != null)
                {
                    TPOFormulationRawMaterialDto dto = new TPOFormulationRawMaterialDto();
                    using (TPOFormulationRawMaterialService service = new TPOFormulationRawMaterialService())
                    {
                        Mapper.Map(formulation, dto);
                        if (formulation.ID > 0)
                        {
                            service.Delete(dto.ID);
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllFormulationResult()
        {
            List<TPOFormulationModel> formulations = new List<TPOFormulationModel>();
            using (TPOFormulationService svc = new TPOFormulationService())
            {
               var dtos = svc.GetAll();
               formulations.AddRange(Mapper.Map<List<TPOFormulationDto>, List<TPOFormulationModel>>(dtos));
            }
            return Json(formulations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllExtruderResult()
        {
            List<TPOExtruderModel> extruder = new List<TPOExtruderModel>();
            using (TPOExtruderService svc = new TPOExtruderService())
            {
                var dtos = svc.GetAll();
                extruder.AddRange(Mapper.Map<List<TPOExtruderDto>, List<TPOExtruderModel>>(dtos));
            }
            return Json(extruder, JsonRequestBehavior.AllowGet);
        }

    }
}
