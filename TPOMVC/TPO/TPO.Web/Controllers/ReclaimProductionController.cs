using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Constants;
using TPO.Services.Reclaim;
using TPO.Services.Products;
using TPO.Services.Application;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using AutoMapper;
using TPO.Web.Core;
using Newtonsoft.Json;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ReclaimProductionController : BaseController
    {
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetReclaimMaterialResult()
        {
            List<TPOProduct> data = new List<TPOProduct>();
            using (TPOProductService svc = new TPOProductService())
            {
                var dtos = svc.GetAll().FindAll(p => p.IsREPEL == true);
                data.AddRange(Mapper.Map<List<TPOProductDto>, List<TPOProduct>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetMaterialProcessedByRCMaterial(string actionTypeCode)
        {
            List<TPOReclaimActionModel> data = new List<TPOReclaimActionModel>();
            using (TPOReclaimActionService svc = new TPOReclaimActionService())
            {
                var dtos = svc.GetAll().FindAll(p => p.TPOReclaimActionTypeCode.Equals(actionTypeCode));
                data.AddRange(Mapper.Map<List<TPOReclaimActionDto>, List<TPOReclaimActionModel>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}