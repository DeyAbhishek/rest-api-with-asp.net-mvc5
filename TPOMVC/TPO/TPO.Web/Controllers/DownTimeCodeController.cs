using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Downtime;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using AutoMapper;
using TPO.Web.Core;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class DownTimeCodeController : BaseController
    {
        //
        // GET: /DownTimeCode/
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult TypesResult() 
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
        public JsonResult ReasonsByLineAndTypeResult(int lineID = 0, int typeID = 0) 
        {
            List<DownTimeReason> data = new List<DownTimeReason>();
            using (DownTimeReasonService svc = new DownTimeReasonService()) 
            {
                var dtos = svc.GetByLineAndType(lineID, typeID);
                data.AddRange(Mapper.Map<List<DownTimeReasonDto>, List<DownTimeReason>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GroupsByLineResult(int lineID = 0, int typeID = 0) 
        {
            List<DownTimeEquipmentGroup> data = new List<DownTimeEquipmentGroup>();
            using (DownTimeEquipmentGroupService svc = new DownTimeEquipmentGroupService()) 
            {
                var dtos = svc.GetByLineAndType(lineID, typeID);
                data.AddRange(Mapper.Map<List<DownTimeEquipmentGroupDto>, List<DownTimeEquipmentGroup>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult EquipmentByGroupResult(int groupID = 0) 
        {
            List<DownTimeEquipment> data = new List<DownTimeEquipment>();
            using (DownTimeEquipmentService svc = new DownTimeEquipmentService()) 
            {
                var dtos = svc.GetByGroup(groupID);
                data.AddRange(Mapper.Map<List<DownTimeEquipmentDto>, List<DownTimeEquipment>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateReasonResult(DownTimeReason model) 
        {
            ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<DownTimeReason, DownTimeReasonDto>(model);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;

                using (DownTimeReasonService svc = new DownTimeReasonService())
                {
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }
                }
                model = Mapper.Map<DownTimeReasonDto, DownTimeReason>(dto);

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            model.ResponseMessage = responseMessage;
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateGroupResult(DownTimeEquipmentGroup model) 
        {
            ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<DownTimeEquipmentGroup, DownTimeEquipmentGroupDto>(model);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;

                using (DownTimeEquipmentGroupService svc = new DownTimeEquipmentGroupService())
                {
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }
                }
                model = Mapper.Map<DownTimeEquipmentGroupDto, DownTimeEquipmentGroup>(dto);

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            model.ResponseMessage = responseMessage;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateEquipmentResult(DownTimeEquipment model) 
        {
            ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<DownTimeEquipment, DownTimeEquipmentDto>(model);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;

                using (DownTimeEquipmentService svc = new DownTimeEquipmentService())
                {
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }
                }
                model = Mapper.Map<DownTimeEquipmentDto, DownTimeEquipment>(dto);

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            model.ResponseMessage = responseMessage;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteReason(int id = 0) 
        {
            ResponseMessage responseMessage;

            try
            {
                using (DownTimeReasonService svc = new DownTimeReasonService())
                {
                    svc.Delete(id);
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
        public JsonResult DeleteGroup(int id = 0) 
        {
            ResponseMessage responseMessage;

            try
            {
                using (DownTimeEquipmentGroupService svc = new DownTimeEquipmentGroupService())
                {
                    svc.Delete(id);
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
        public JsonResult DeleteEquipment(int id = 0) 
        {
            ResponseMessage responseMessage;

            try
            {
                using (DownTimeEquipmentService svc = new DownTimeEquipmentService())
                {
                    svc.Delete(id);
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