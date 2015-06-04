using System;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Reclaim;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class WipAdjustmentController : BaseController
    {
        //
        // GET: /WipAdjustment/
        public ActionResult Index() {
            return View( new TPOReclaimWIPModel() );
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult FetchWIP(string reclaimType )
        {
            var model = new Models.TPOReclaimWIPModel();
            using( TPOReclaimWIPService svc = new TPOReclaimWIPService())
            {
                model = Mapper.Map<TPOReclaimWIPDto, TPOReclaimWIPModel>( 
                    svc.GetByPlantAndType(CurrentPlantId, reclaimType )
                );
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save (float Wip, string ReclaimType, string AdjustmentType)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (TPOReclaimWIPService svc = new TPOReclaimWIPService()) 
                {
                    svc.Update(CurrentPlantId, ReclaimType, AdjustmentType, CurrentUser, Wip.ToString());
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