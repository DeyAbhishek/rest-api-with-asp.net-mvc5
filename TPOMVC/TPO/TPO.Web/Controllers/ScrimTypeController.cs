using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Scrim;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using Newtonsoft.Json;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ScrimTypeController : BaseController
    {
        //
        // GET: /ScrimType/
        public ActionResult Index()
        {
            return View(new ScrimTypeModel { PlantID = CurrentPlantId, IsLiner = false });
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllScrimTypeResult(bool isLiner)
        {
            List<ScrimTypeModel> scrimTypes = new List<ScrimTypeModel>();
            using (ScrimTypeService svc = new ScrimTypeService())
            {
                var dtos = svc.GetAllByPlantId(CurrentPlantId, isLiner);
                scrimTypes.AddRange(Mapper.Map<List<ScrimTypeDto>, List<ScrimTypeModel>>(dtos));
            }
            return Json(scrimTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllLengthUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("L");
        }

        [HttpGet]
        public JsonResult GetAllWidthUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("D");
        }

        [HttpGet]
        public JsonResult GetAllAreaUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("A");
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
                return null;
            }

        [HttpPost]
        public ActionResult AjaxTypeUpdate(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                ScrimTypeModel scrimType = JsonConvert.DeserializeObject<ScrimTypeModel>(row);
                if (scrimType != null)
                {
                    scrimType.LastModified = DateTime.Now;
                    ScrimTypeDto dto = new ScrimTypeDto();
                    using (ScrimTypeService service = new ScrimTypeService())
                    {
                        Mapper.Map(scrimType, dto);
                        if (scrimType.ID > 0)
                            service.Update(dto);
                        else
                        {
                            dto.PlantID = CurrentPlantId;
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
                ScrimTypeModel scrimType = JsonConvert.DeserializeObject<ScrimTypeModel>(row);
                if (scrimType != null)
                {
                    ScrimTypeDto dto = new ScrimTypeDto();
                    using (ScrimTypeService service = new ScrimTypeService())
                    {
                        Mapper.Map(scrimType, dto);
                        if (scrimType.ID > 0)
                            service.Delete(dto.ID);
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