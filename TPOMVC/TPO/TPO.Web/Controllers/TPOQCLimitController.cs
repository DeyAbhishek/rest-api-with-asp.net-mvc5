using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Products;
using TPO.Services.Application;
using TPO.Common.Constants;
using TPO.Common.Resources;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOQCLimitController : BaseController
    {
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0) 
            {
                return View(new TPOQCLimit());
            }
            TPOQCLimit model = null;
            using (TPOQCLimitService svc = new TPOQCLimitService()) 
            {
                var dto = svc.Get(id);
                model = AutoMapper.Mapper.Map<TPOQCLimitDto, TPOQCLimit>(dto);
            }
            if (model == null) 
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoRecord);
                return View();
            }
            ViewBag.UseSecThick = GetUseSecThickOptions();
            ViewBag.ThickUoM = GetThickUoMOptions();
            ViewBag.ForceUoM = GetForceUoMOptions();
            ViewBag.TempUoM = GetTempUoMOptions();
            ViewBag.WeightUoM = GetWeightUoMOptions();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TPOQCLimit model) 
        {
            model.LastModified = DateTime.Now;
            model.ModifiedBy = CurrentUser;
            TPOQCLimitDto dto = AutoMapper.Mapper.Map<TPOQCLimit, TPOQCLimitDto>(model);
            using (TPOQCLimitService svc = new TPOQCLimitService()) 
            {
                svc.Update(dto);
            }
            SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            return RedirectToAction("Edit", new { id = model.Id });
        }

        public JsonResult AllProductsResult() 
        {
            List<TPOProductDto> dtos = new List<TPOProductDto>();
            using (TPOProductService svc = new TPOProductService()) 
            {
                dtos.AddRange(svc.GetByPlant(CurrentPlantId));
            }
            return Json(dtos, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetUseSecThickOptions()
        {
            List<KeyValuePair<string, bool>> useSecThick = new List<KeyValuePair<string, bool>>();
            useSecThick.Add(new KeyValuePair<string, bool>("Yes", true));
            useSecThick.Add(new KeyValuePair<string, bool>("No", false));
            return new SelectList(useSecThick, "Value", "Key");
        }
    }
}