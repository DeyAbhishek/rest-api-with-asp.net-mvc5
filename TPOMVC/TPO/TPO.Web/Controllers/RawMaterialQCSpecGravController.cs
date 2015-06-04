using System;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Constants;
using TPO.Services.Application;
using TPO.Services.RawMaterials;
using TPO.Web.Models;
using TPO.Web.ActionFilters;
using TPO.Common.Enums;
using TPO.Common.Resources;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator})]
    public class RawMaterialQCSpecGravController : BaseController
    {
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoId);
                return RedirectToAction("Index", "RawMaterialQC");
            }
            RawMaterialQcSpecificGravity model = null;
            using (var svc = new RawMaterialQcSpecificGravityService())
            {
                var dto = svc.Get(id);
                if (dto != null)
                {
                    model = Mapper.Map<RawMaterialSpecificGravityDto, RawMaterialQcSpecificGravity>(dto);
                    using (var secSvc = new SecurityService())
                    {
                        var userDtos = secSvc.GetQCTechUsers();
                        ViewBag.QCTech = new SelectList(userDtos, "Id", "FullName");
                    }
                }
            }
            if (model == null)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                return RedirectToAction("Index", "RawMaterialQC");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RawMaterialQcSpecificGravity model, double? dryWeight1, double? subWeight1,
            double? dryWeight2, double? subWeight2, double? dryWeight3, double? subWeight3, double? dryWeight4,
            double? subWeight4, double? dryWeight5, double? subWeight5)
        {
            if (ModelState.IsValid)
            {
                RawMaterialSpecificGravityDto dto;
                using (var svc = new RawMaterialQcSpecificGravityService())
                {
                    dto = svc.Get(model.Id);
                }
                if (dto != null)
                {
                    dto.DenIso = model.DenIso;
                    dto.AverageGravity = model.AverageGravity;
                    dto.LabTechUserId = model.LabTechUserId;
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;

                    foreach (var detail in dto.RawMaterialSpecificGravityDetails)
                    {
                        switch (detail.Order)
                        {
                            case 1:
                                if (detail.Submerged)
                                    detail.Value = subWeight1 ?? 0.0;
                                else
                                    detail.Value = dryWeight1 ?? 0.0;
                                break;
                            case 2:
                                if (detail.Submerged)
                                    detail.Value = subWeight2 ?? 0.0;
                                else
                                    detail.Value = dryWeight2 ?? 0.0;
                                break;
                            case 3:
                                if (detail.Submerged)
                                    detail.Value = subWeight3 ?? 0.0;
                                else
                                    detail.Value = dryWeight3 ?? 0.0;
                                break;
                            case 4:
                                if (detail.Submerged)
                                    detail.Value = subWeight4 ?? 0.0;
                                else
                                    detail.Value = dryWeight4 ?? 0.0;
                                break;
                            case 5:
                                if (detail.Submerged)
                                    detail.Value = subWeight5 ?? 0.0;
                                else
                                    detail.Value = dryWeight5 ?? 0.0;
                                break;
                        }
                    }

                    using (var svc = new RawMaterialQcSpecificGravityService())
                    {
                        svc.Update(dto);
                    }
                    SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                    return RedirectToAction("Edit", "RawMaterialQC", new { id = model.RawMaterialQcId });
                }
            }
            SetResponseMesssage(ActionTypeMessage.FailedSave);
            return View(model);
        }

        [HttpGet]
        public ActionResult SpecificGravityDetail(int id = 0)
        {
            ActionResult result = null;
            if (id == 0)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                result = RedirectToAction("Index", "RawMaterialQC");
            }
            using (var svc = new RawMaterialQcSpecificGravityService())
            {
                var dto = svc.GetByQcId(id);
                if (dto == null)
                {
                    //New specific gravity, need to create now
                    dto = svc.CreateNew(id);
                    dto.EnteredBy = CurrentUser;
                    dto.DateEntered = DateTime.Now;
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;
                    foreach (var detail in dto.RawMaterialSpecificGravityDetails)
                    {
                        detail.EnteredBy = CurrentUser;
                        detail.DateEntered = DateTime.Now;
                        detail.ModifiedBy = CurrentUser;
                        detail.LastModified = DateTime.Now;
                    }
                    svc.Add(dto);
                    dto = svc.GetByQcId(id);
                }
                if (dto != null)
                {
                    result = RedirectToAction("Edit", "RawMaterialQCSpecGrav", new { id = dto.Id });
                }
            }
            return result;
        }

    }

}