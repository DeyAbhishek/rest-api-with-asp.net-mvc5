using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Formulation;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOFormulationRawMaterialController : BaseController
    {
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllFormulationRawMaterialResult(int formulationId, int extruderId)
        {
            List<TPOFormulationRawMaterialModel> formulations = new List<TPOFormulationRawMaterialModel>();
            using (TPOFormulationRawMaterialService svc = new TPOFormulationRawMaterialService())
            {
                var dtos = svc.GetAll().FindAll(p => p.TPOFormulationID == formulationId).FindAll(p => p.TPOExtruderID == extruderId);
                formulations.AddRange(Mapper.Map<List<TPOFormulationRawMaterialDto>, List<TPOFormulationRawMaterialModel>>(dtos));
            }
            return Json(formulations, JsonRequestBehavior.AllowGet);
        }
	}
}