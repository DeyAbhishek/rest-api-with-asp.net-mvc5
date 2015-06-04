using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Products;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class IMProductTypeController : BaseController
    {
        [HttpGet]
        public JsonResult GetAllProductTypeResult()
        {
            List<IMProductTypeModel> productType = new List<IMProductTypeModel>();
            using (IMProductTypeService svc = new IMProductTypeService())
            {
                var dtos = svc.GetAll();
                productType.AddRange(Mapper.Map<List<IMProductTypeDto>, List<IMProductTypeModel>>(dtos));
            }
            return Json(productType, JsonRequestBehavior.AllowGet);
        }
	}
}