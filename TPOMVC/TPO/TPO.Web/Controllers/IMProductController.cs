using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services.Application;
using TPO.Services.Products;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class IMProductController : BaseController
    {
        //
        // GET: /IMProduct/Edit/5
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            if (id == 0) 
            {
                return View(new IMProduct());
            }
            using (var service = new IMProductService())
            {
                IMProduct model =
                    Mapper.Map<IMProductDto, IMProduct>(service.Get(id));

                return View(model);
            }
        }

        //
        // POST: /IMProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(IMProduct model)
        {
            //Static value used because CurrentPlantId is throwing exception
            model.PlantId = CurrentPlantId;
            if (ModelState.IsValid)
            {
                IMProductDto dto = Mapper.Map<IMProduct, IMProductDto>(model);
                
                //TODO: move to service
                using (var service = new IMProductService())
                {
                    if (model.Id > 0)
                    {
                        service.Update(dto);
                    }
                    else
                    {
                        service.Add(dto);
                    }
                }
                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                SetResponseMesssage(ActionTypeMessage.FailedSave);

                return View(model);
            }
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public JsonResult GetAllForceUomResult()
        {
            ScrimTypeController scrimType = new ScrimTypeController();
            return scrimType.GetAllUoMResultByCode("F");
        }
        
        [HttpGet]
        public JsonResult GetAllThicknessUomResult()
        {
            ScrimTypeController scrimType = new ScrimTypeController();
            return scrimType.GetAllUoMResultByCode("T");
        }

        [HttpGet]
        public JsonResult GetAllWeightUomResult()
        {
            ScrimTypeController scrimType = new ScrimTypeController();
            return scrimType.GetAllUoMResultByCode("W");
        }

        [HttpGet]
        public JsonResult GetAllWidthUomResult()
        {
            ScrimTypeController scrimType = new ScrimTypeController();
            return scrimType.GetAllUoMResultByCode("L");
        }

        [HttpGet]
        public JsonResult GetAllAreaUomResult()
        {
            ScrimTypeController scrimType = new ScrimTypeController();
            return scrimType.GetAllUoMResultByCode("A");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllProductResult()
        {
            List<IMProduct> products = new List<IMProduct>();
            using (IMProductService svc = new IMProductService())
            {
                var dtos = svc.GetAll();
                products.AddRange(Mapper.Map<List<IMProductDto>, List<IMProduct>>(dtos));
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetByPlantResult()
        {
            List<IMProduct> products = new List<IMProduct>();
            using (IMProductService svc = new IMProductService())
            {
                var dtos = svc.GetByPlant(CurrentPlantId);
                products.AddRange(Mapper.Map<List<IMProductDto>, List<IMProduct>>(dtos));
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}
