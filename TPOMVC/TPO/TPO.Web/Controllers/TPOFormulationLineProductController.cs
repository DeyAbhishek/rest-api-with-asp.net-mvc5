using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Formulation;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOFormulationLineProductController : BaseController
    {
        //
        // GET: /TPOFormulationLineProduct/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxTypeUpdate(string row)
        {
            TPOFormulationLineProductModel formulationLineProduct = JsonConvert.DeserializeObject<TPOFormulationLineProductModel>(row);
            if (formulationLineProduct != null)
            {
                formulationLineProduct.LastModified = DateTime.Now;
                TPOFormulationLineProductDto dto = new TPOFormulationLineProductDto();
                using (TPOFormulationLineProductService service = new TPOFormulationLineProductService())
                {
                    Mapper.Map(formulationLineProduct, dto);
                    if (formulationLineProduct.Id > 0)
                    {
                        service.Update(dto);
                    }
                }
            }

            return RedirectToAction("Edit","TPOProduct");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllFormulationLineProductResult(int productId, int plantId)
        {
            List<TPOFormulationLineProductModel> formulations = new List<TPOFormulationLineProductModel>();
            using (TPOFormulationLineProductService svc = new TPOFormulationLineProductService())
            {
                var dtos = svc.GetAll().FindAll(p => p.TPOProductID == productId).FindAll(p => p.PlantID == plantId);
                formulations.AddRange(Mapper.Map<List<TPOFormulationLineProductDto>, List<TPOFormulationLineProductModel>>(dtos));
            }
            return Json(formulations, JsonRequestBehavior.AllowGet);
        }
	}
}