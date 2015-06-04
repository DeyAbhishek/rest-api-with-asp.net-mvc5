using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Constants;
using TPO.Services.Downtime;
using TPO.Services.Application;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using AutoMapper;
using TPO.Web.Core;
using Newtonsoft.Json;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPODownTimeEntryController : BaseController
    {
        //
        // GET: /TPODownTimeEntry/
        public ActionResult Index()
        {
            return View();
        }   

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllDownTimeTypeResult()
        {
            List<DownTimeTypeDto> data = new List<DownTimeTypeDto>();
            using (DownTimeTypeService svc = new DownTimeTypeService())
            {
                data.AddRange(svc.GetAll().OrderBy(t => t.SortOrder));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetEquimentGroupResult()
        {
            List<DownTimeEquipmentGroup> data = new List<DownTimeEquipmentGroup>();
            using (DownTimeEquipmentGroupService svc = new DownTimeEquipmentGroupService())
            {
                var dtos = svc.GetAll();
                data.AddRange(Mapper.Map<List<DownTimeEquipmentGroupDto>, List<DownTimeEquipmentGroup>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetReasonsByTypeResult(int typeID = 0)
        {
            List<DownTimeReason> data = new List<DownTimeReason>();
            using (DownTimeReasonService svc = new DownTimeReasonService())
            {
                var dtos = svc.GetAll().FindAll(p => p.DownTimeTypeID == typeID);
                data.AddRange(Mapper.Map<List<DownTimeReasonDto>, List<DownTimeReason>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetEquipmentComponentByGroupResult(int groupID = 0)
        {
            List<DownTimeEquipment> data = new List<DownTimeEquipment>();
            using (DownTimeEquipmentService svc = new DownTimeEquipmentService())
            {
                var dtos = svc.GetAll().FindAll(p => p.DownTimeEquipmentGroupID == groupID);
                data.AddRange(Mapper.Map<List<DownTimeEquipmentDto>, List<DownTimeEquipment>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetDownTimeByLineResult(int lineID = 0)
        {
            List<DownTimeModel> data = new List<DownTimeModel>();
            using (DownTimeService svc = new DownTimeService())
            {
                var dtos = svc.GetAll().FindAll(p => p.LineID == lineID);
                data.AddRange(Mapper.Map<List<DownTimeDto>, List<DownTimeModel>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxTypeUpdate(DownTimeModel downTime)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                if (downTime != null)
                {
                    downTime.LastModified = DateTime.Now;
                    DownTimeDto dto = new DownTimeDto();
                    using (DownTimeService service = new DownTimeService())
                    {
                        Mapper.Map(downTime, dto);
                        
                        if (downTime.ID > 0)
                        {
                            dto.ModifiedBy = CurrentUser;
                            service.Update(dto);
                        }
                        else
                        {
                            dto.ModifiedBy = CurrentUser;
                            dto.EnteredBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
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
        public JsonResult AjaxTypeDelete(DownTimeModel downTime)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                if (downTime != null)
                {
                    DownTimeDto dto = new DownTimeDto();
                    using (DownTimeService service = new DownTimeService())
                    {
                        Mapper.Map(downTime, dto);
                        if (downTime.ID > 0)
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
	}
}