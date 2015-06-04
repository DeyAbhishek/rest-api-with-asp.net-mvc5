using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using System;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class UnitOfMeasureController : BaseController
    {
        #region ActionResults

        // insert parameter to get plant id
        [HttpGet]
        public ActionResult EditDefault()
        {
            return View();
        }

        #endregion

        #region JsonResults

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllUnitOfMeasureDefaults()
        {
            List<UnitOfMeasureDefaultViewModel> uomDefaults = new List<UnitOfMeasureDefaultViewModel>();
            using (UnitOfMeasureDefaultService service = new UnitOfMeasureDefaultService())
            {
                var dtos = service.GetAllByPlantId(CurrentPlantId);
                uomDefaults.AddRange(Mapper.Map<List<UnitOfMeasureDefaultDto>, List<UnitOfMeasureDefaultViewModel>>(dtos));
            }
            return Json(uomDefaults, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetDefaultUoMByType(string typeCode = "") 
        {
            UnitOfMeasureModel model = new UnitOfMeasureModel();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                var dto = svc.GetDefaultByTypeCode(typeCode);
                model = Mapper.Map<UnitOfMeasureDto, UnitOfMeasureModel>(dto);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUoMsByType(int typeId = 1)
        {
            List<UnitOfMeasureModel> uoms = new List<UnitOfMeasureModel>();
            using (UnitOfMeasureService service = new UnitOfMeasureService())
            {
                var dtos = service.GetAllByUoMTypeId(typeId);
                uoms.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dtos));
            }
            return Json(uoms, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public JsonResult AjaxDefaultUpdate(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                UnitOfMeasureDefaultViewModel unitOfMeasureDefault = JsonConvert.DeserializeObject<UnitOfMeasureDefaultViewModel>(row);
                if (unitOfMeasureDefault != null)
                {
                    unitOfMeasureDefault.LastModified = DateTime.Now;
                    unitOfMeasureDefault.ModifiedBy = CurrentUser;
                    UnitOfMeasureDefaultDto dto = new UnitOfMeasureDefaultDto();
                    using (UnitOfMeasureDefaultService service = new UnitOfMeasureDefaultService())
                    {
                        Mapper.Map(unitOfMeasureDefault, dto);
                        if (unitOfMeasureDefault.Id > 0)
                            service.Update(dto);
                        else
                        {
                            dto.PlantID = CurrentPlantId;
                            dto.EnteredBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
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

        [HttpGet]
        public JsonResult GetWeightUoMs() 
        {
            return GetByType("W");
        }

        [HttpGet]
        public JsonResult GetLengthUoMs() 
        {
            return GetByType("L");
        }

        [HttpGet]
        public JsonResult GetWidthUoMs() 
        {
            return GetByType("D");
        }

        private JsonResult GetByType(string type = "") 
        {
            List<UnitOfMeasureModel> data = new List<UnitOfMeasureModel>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService()) 
            {
                var dtos = svc.GetByTypeCode(type);
                data.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

    }
}