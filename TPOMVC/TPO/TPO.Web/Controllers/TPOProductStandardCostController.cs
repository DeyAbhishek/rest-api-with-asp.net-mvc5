using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Products;
using AutoMapper;
using TPO.Common.DTOs;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOProductStandardCostController : BaseController
    {
        [HttpGet]
        public ActionResult Index() 
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetByProductIDResult(int id = 0) 
        {
            List<TPOProductStandardCost> data = new List<TPOProductStandardCost>();
            using (TPOProductStandardCostService svc = new TPOProductStandardCostService()) 
            {
                var dtos = svc.GetByProductID(id);
                data.AddRange(Mapper.Map<List<TPOProductStandardCostDto>, List<TPOProductStandardCost>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateStandardCostResult(TPOProductStandardCost model) 
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<TPOProductStandardCost, TPOProductStandardCostDto>(model);
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;

                using (TPOProductStandardCostService svc = new TPOProductStandardCostService())
                {
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }
                }

                model = Mapper.Map<TPOProductStandardCostDto, TPOProductStandardCost>(dto);

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            model.ResponseMessage = responseMessage;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteStandardCost(int id = 0) 
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOProductStandardCostService svc = new TPOProductStandardCostService())
                {
                    svc.Delete(id);
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