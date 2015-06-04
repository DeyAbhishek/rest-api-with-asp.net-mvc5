using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Razor;
using Antlr.Runtime.Tree;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services.Application;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.LeadOperator, SecurityTask.SystemsAdministrator })]
    public class RawMaterialQCController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            RawMaterialQcDto dto;
            using (var svc = new RawMaterialsQcService())
            {
                dto = svc.CreateNew(id);
            }
            if (dto == null)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                return RedirectToAction("Index");
            }
            var qcTechList = new List<SelectListItem>();
            using (var svc = new SecurityService())
            {
                qcTechList.AddRange(svc.GetQCTechUsers().Select(u => new SelectListItem { Text = u.FullName, Value = u.Id.ToString(CultureInfo.InvariantCulture) }));
            }
            dto.DateEntered = DateTime.Now;
            ViewBag.QCTech = new SelectList(qcTechList, "Value", "Text");
            var model = Mapper.Map<RawMaterialQcDto, RawMaterialQc>(dto);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RawMaterialQc model)
        {
            model.PlantId = CurrentPlantId;
            model.EnteredBy = CurrentUser;
            model.DateEntered = DateTime.Now;
            model.ModifiedBy = CurrentUser;
            model.LastModified = DateTime.Now;
            var dto = Mapper.Map<RawMaterialQc, RawMaterialQcDto>(model);
            int id;
            using (var svc = new RawMaterialsQcService())
            {
                id = svc.Add(dto);
            }
            if (id > 0)
            {
                return RedirectToAction("Edit", new {id});
            }
            SetResponseMesssage(ActionTypeMessage.FailedSave);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0, bool success = false)
        {
            var qcTechList = new List<SelectListItem>();
            using (var svc = new SecurityService())
            {
                qcTechList.AddRange(svc.GetQCTechUsers().Select(u => new SelectListItem {Text = u.FullName, Value = u.Id.ToString(CultureInfo.InvariantCulture)}));
            }
            ViewBag.QCTech = new SelectList(qcTechList, "Value", "Text");
            RawMaterialQc model = null;

            using (var svc = new RawMaterialsQcService())
            {
                var dto = svc.Get(id);
                if (dto != null)
                {
                    model = Mapper.Map<RawMaterialQcDto, RawMaterialQc>(dto);
                }
            }

            //hack
            if (success)
            {
                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            if (model == null)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RawMaterialQc model)
        {
            var success = false;
            if (ModelState.IsValid)
            {
                var dto = Mapper.Map<RawMaterialQc, RawMaterialQcDto>(model);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;
                using (var svc = new RawMaterialsQcService())
                {
                    svc.Update(dto);
                }
                success = true;
            }

            if (success)
            {
                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            if (model == null)
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = model.Id, success });
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult RawMaterialQCsGridByReceived(int receivedId, int? rows, int? page)
        {
            rows = rows ?? DefaultPageSize;
            page = page ?? DefaultPage;

            var data = new List<RawMaterialQc>();
            int total;
            using (var svc = new RawMaterialsQcService())
            {
                var dtos = svc.GetByReceived(receivedId);
                total = dtos.Count;
                var currentPageDtos = dtos.OrderBy(r => r.DateEntered).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToList();
                data.AddRange(Mapper.Map<List<RawMaterialQcDto>, List<RawMaterialQc>>(currentPageDtos));
            }
            return BuildJsonResult(data, total);
        }

        [HttpGet]
        public JsonResult GetAllQCVisualInspectionResult()
        {
            List<QCVisualInspectionTypeModel> extruder = new List<QCVisualInspectionTypeModel>();
            using (QCVisualInspectionTypeService svc = new QCVisualInspectionTypeService())
            {
                var dtos = svc.GetAll();
                extruder.AddRange(Mapper.Map<List<QCVisualInspectionTypeDto>, List<QCVisualInspectionTypeModel>>(dtos));
            }
            return Json(extruder, JsonRequestBehavior.AllowGet);
        }
    }

    
}