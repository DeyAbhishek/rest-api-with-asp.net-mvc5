using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Scrap;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOLineScrapCodeGroupController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrapCodeGroupResult()
        {
            List<TPOLineScrapCodeGroup> groups = new List<TPOLineScrapCodeGroup>();
            using (TPOLineScrapCodeGroupService svc = new TPOLineScrapCodeGroupService())
            {
                var dtos = svc.GetByPlant(CurrentPlantId);
                groups.AddRange(Mapper.Map<List<TPOLineScrapCodeGroupDto>, List<TPOLineScrapCodeGroup>>(dtos));
            }

            //using (TPOLineScrapCodeService svc = new TPOLineScrapCodeService())
            //{
            //    foreach (var tpoLineScrapCodeGroup in groups)
            //    {
            //        var codeDtos = svc.GetByCodeGroup(tpoLineScrapCodeGroup.Id);
            //        tpoLineScrapCodeGroup.TPOLineScrapCodes.AddRange(Mapper.Map<List<TPOLineScrapCodeDto>, List<TPOLineScrapCode>>(codeDtos));
            //    }
            //}
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetScrapCodesResult(int groupId = 0)
        {
            List<TPOLineScrapCode> codes = new List<TPOLineScrapCode>();
            using (TPOLineScrapCodeService svc = new TPOLineScrapCodeService())
            {
                var dtos = svc.GetByCodeGroup(groupId);
                codes.AddRange(Mapper.Map<List<TPOLineScrapCodeDto>, List<TPOLineScrapCode>>(dtos));
            }
            return Json(codes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCodeGroupResult(TPOLineScrapCodeGroup model)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOLineScrapCodeGroupService svc = new TPOLineScrapCodeGroupService())
                {
                    var dto = Mapper.Map<TPOLineScrapCodeGroup, TPOLineScrapCodeGroupDto>(model);
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;

                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.PlantID = CurrentPlantId;
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.ID = svc.Add(dto);
                    }
                    model = Mapper.Map<TPOLineScrapCodeGroupDto, TPOLineScrapCodeGroup>(dto);
                }

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
        public JsonResult DeleteCodeGroup(int id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOLineScrapCodeGroupService svc = new TPOLineScrapCodeGroupService())
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
        public JsonResult UpdateCodeResult(TPOLineScrapCode model)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOLineScrapCodeService svc = new TPOLineScrapCodeService())
                {
                    var dto = Mapper.Map<TPOLineScrapCode, TPOLineScrapCodeDto>(model);
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;

                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.ID = svc.Add(dto);
                    }

                    model = Mapper.Map<TPOLineScrapCodeDto, TPOLineScrapCode>(dto);
                }

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
        public JsonResult DeleteCode(int id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOLineScrapCodeService svc = new TPOLineScrapCodeService())
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