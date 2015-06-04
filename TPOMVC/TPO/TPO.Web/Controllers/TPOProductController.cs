using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Products;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using Newtonsoft.Json;
using TPO.Services.Formulation;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOProductController : BaseController
    {
        //
        // GET: /TPOProduct/Edit/5
        public ActionResult Edit(int id=0)
        {
            if (id == 0)
            {
                return View(new TPOProduct());
            }
            using (var service = new TPOProductService())
            {
                TPOProduct model =
                    Mapper.Map<TPOProductDto, TPOProduct>(service.Get(id));

                return View(model);
            }
        }

        //
        // POST: /TPOProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(TPOProduct model)
        {
            model.LastModified = DateTime.Now;
            model.ModifiedBy = CurrentUser;
            model.DateEntered = DateTime.Now;
            model.EnteredBy = CurrentUser;
            model.PlantId = CurrentPlantId;
            
            if (ModelState.IsValid)
            {
                TPOProductDto dto = Mapper.Map<TPOProduct, TPOProductDto>(model);
                
                //TODO: move to service
                using (var service = new TPOProductService())
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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllProductResult()
        {
            List<TPOProduct> products = new List<TPOProduct>();
            using (TPOProductService svc = new TPOProductService())
            {
                var dtos = svc.GetAll();
                products.AddRange(Mapper.Map<List<TPOProductDto>, List<TPOProduct>>(dtos));
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetByPlantResult()
        {
            List<TPOProduct> products = new List<TPOProduct>();
            using (TPOProductService svc = new TPOProductService())
            {
                var dtos = svc.GetByPlant(CurrentPlantId);
                products.AddRange(Mapper.Map<List<TPOProductDto>, List<TPOProduct>>(dtos));
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AjaxTypeUpdate(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
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

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetReworkProducts(int productInID = 0) 
        {
            List<TPOProduct> data = new List<TPOProduct>();
            using (TPOProductService svc = new TPOProductService()) 
            {
                var dtos = svc.GetReworkProducts(productInID);
                data.AddRange(Mapper.Map<List<TPOProductDto>, List<TPOProduct>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
