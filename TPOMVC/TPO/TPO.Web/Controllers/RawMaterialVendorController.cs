using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class RawMaterialVendorController : BaseController
    {
        //
        // GET: /RawMaterialVendor/
        
        public ActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult FetchVendors()
        {
            List<RawMaterialVendorModel> results = new List<RawMaterialVendorModel>();
            using (Services.RawMaterials.RawMaterialVendorService service = new Services.RawMaterials.RawMaterialVendorService())
            {
                var dtos = service.GetByPlantId(CurrentPlantId);
                results.AddRange(Mapper.Map<List<RawMaterialVendorDto>, List<RawMaterialVendorModel>>(dtos));
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult Save(int? id, string vendor)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                RawMaterialVendorModel model = new RawMaterialVendorModel();
                if (id.HasValue)
                {
                    using (Services.RawMaterials.RawMaterialVendorService service = new Services.RawMaterials.RawMaterialVendorService())
                    {
                        Mapper.Map(service.Get(id.Value), model);
                    }
                }
                else
                {
                    model.PlantId = CurrentPlantId;
                    model.EnteredBy = CurrentUser;
                    model.DateEntered = DateTime.Now;
                }

                model.Vendor = vendor;
                model.ModifiedBy = CurrentUser;
                model.LastModified = DateTime.Now;

                using (Services.RawMaterials.RawMaterialVendorService service = new Services.RawMaterials.RawMaterialVendorService())
                {
                    RawMaterialVendorDto dto = new RawMaterialVendorDto();
                    Mapper.Map(model, dto);
                    if (id.HasValue)
                    {
                        service.Update(dto);
                    }
                    else
                    {
                        service.Add(dto);
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
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult Remove(int id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (Services.RawMaterials.RawMaterialVendorService service = new Services.RawMaterials.RawMaterialVendorService()) 
                {
                    service.Delete(id);
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
