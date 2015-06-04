using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Web.Models;
using TPO.Services.Rework;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Web.Core;
using TPO.Services.Rework;

namespace TPO.Web.Controllers
{
    public class ReworkProductionController : BaseController
    {



        #region JsonResult
        [HttpPost]
        public JsonResult UpdateRoll(TPOReworkRollModel roll1, TPOReworkRollModel roll2 = null) 
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                using (TPOReworkRollService svc = new TPOReworkRollService())
                {
                    var dto = Mapper.Map(roll1, svc.Get(roll1.Id));
                    svc.Update(dto);

                    if (roll2 != null)
                    {
                        var dto2 = Mapper.Map(roll2, svc.Get(roll2.Id));
                        svc.Update(dto2);
                    }
                }
                response = SetResponseMesssage(Common.Enums.ActionTypeMessage.SuccessfulSave, "Measurements saved successfully.");
            }
            catch (Exception ex) 
            {
                response = SetResponseMesssage(Common.Enums.ActionTypeMessage.FailedSave, "Failed to save measurements.");
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EnterPrimeScrap(EnterPrimeScrapModel model) 
        {
            ResponseMessage responseMessage = null;

            try
            {
                responseMessage = SetResponseMesssage(Common.Enums.ActionTypeMessage.SuccessfulSave, "Prime scrap entered successfully.");
            }
            catch (Exception ex) 
            {
                responseMessage = SetResponseMesssage(Common.Enums.ActionTypeMessage.FailedSave, ex.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EnterScrap() 
        {
            ResponseMessage response = null;

            try
            {
                response = SetResponseMesssage(Common.Enums.ActionTypeMessage.SuccessfulSave, "Rework scrap entered successfully.");
            }
            catch (Exception ex) 
            {
                response = SetResponseMesssage(Common.Enums.ActionTypeMessage.FailedSave, ex.Message);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult StartingRollsByPlant() 
        {
            List<TPOReworkRollModel> models = new List<TPOReworkRollModel>();
            using (TPOReworkRollService svc = new TPOReworkRollService()) 
            {
                var dtos = svc.GetStartingRollsByPlant(CurrentPlantId);
                models.AddRange(Mapper.Map<List<TPOReworkRollDto>, List<TPOReworkRollModel>>(dtos));
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult MasterRollsByPlant()
        {
            List<TPOReworkRollModel> models = new List<TPOReworkRollModel>();
            using (TPOReworkRollService svc = new TPOReworkRollService())
            {
                var dtos = svc.GetMasterRollsByPlant(CurrentPlantId);
                models.AddRange(Mapper.Map<List<TPOReworkRollDto>, List<TPOReworkRollModel>>(dtos));
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ProductionEntriesByLine(int lineID = 0) 
        {
            List<TPOReworkActionModel> models = new List<TPOReworkActionModel>();
            using (TPOReworkActionService svc = new Services.Rework.TPOReworkActionService())
            {
                var dtos = svc.GetProductionEntriesByLine(lineID);
                models.AddRange(Mapper.Map<List<TPOReworkActionDto>, List<TPOReworkActionModel>>(dtos));
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ScrapEntriesByLine(int lineID = 0)
        {
            List<TPOReworkActionModel> models = new List<TPOReworkActionModel>();
            using (TPOReworkActionService svc = new Services.Rework.TPOReworkActionService()) 
            {
                var dtos = svc.GetScrapEntriesByLine(lineID);
                models.AddRange(Mapper.Map<List<TPOReworkActionDto>, List<TPOReworkActionModel>>(dtos));
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}