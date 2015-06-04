using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TPO.Common.DTOs;

using TPO.Services;
using TPO.Services.Products;
using TPO.Services.Production;
using TPO.Web.Models;
using AutoMapper;
using TPO.Common.Enums;
using TPO.Services.RawMaterials;

namespace TPO.Web.Controllers
{
    public class IMProductionEntryController : BaseController
    {

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult CreateSingleEntry(int lineId, int shiftId, int workOrderId, DateTime productionDate, int parts, double weight, int weightUoMID, int? batchId, DateTime? adhesiveManufacturesDate, string comment)
        {
            IMProdModel model = new IMProdModel();
            try
            {
                using (IMProductionService service = new IMProductionService())
                {
                    int id = service.CreateSingleEntry(CurrentPlantId, lineId, shiftId, workOrderId, productionDate, parts, weight, weightUoMID, batchId, adhesiveManufacturesDate, comment, CurrentUser);
                    AutoMapper.Mapper.Map(service.Get(id), model);
                }

                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = "ok";
                model.ResponseMessage.ActionStatus = "ok";
            }
            catch (Exception exc)
            { 
                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = exc.Message;
                model.ResponseMessage.ActionStatus = "error";

            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult EditSingleEntry(int id, int lineId, int shiftId, int workOrderId, DateTime productionDate, int parts, double weight, int weightUoMID, int? batchId, DateTime? adhesiveManufacturesDate, string comment)
        {
            IMProdModel model = new IMProdModel();
            try
            {
                using (IMProductionService service = new IMProductionService())
                {
                    IMProdDto dto = service.Get(id);
                    dto.PlantID = CurrentPlantId;
                    dto.LineID = lineId;
                    dto.ShiftID = shiftId;
                    dto.WorkOrderID = workOrderId;
                    dto.ProductionDate = productionDate;
                    dto.PartsCarton = parts;
                    dto.CartonWeight = weight;
                    dto.WeightUoMID = weightUoMID;
                    dto.BatchID = batchId;
                    dto.AdhesionManufacturesDate = adhesiveManufacturesDate;
                    dto.ModifiedBy = CurrentUser;

                    service.EditSingleEntry(dto);
                    AutoMapper.Mapper.Map(service.Get(id), model);
                }

                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = "ok";
                model.ResponseMessage.ActionStatus = "ok";
            }
            catch (Exception exc)
            {
                model.ResponseMessage = new Core.ResponseMessage();
                model.ResponseMessage.ActionMessage = exc.Message;
                model.ResponseMessage.ActionStatus = "error";

            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetAllByLineShiftDate(int lineId, int shiftId, DateTime productionDate)
        {
            List<IMProdModel> models = new List<IMProdModel>();
            try
            {
                using (IMProductionService service = new IMProductionService())
                {
                    AutoMapper.Mapper.Map(service.GetByPlantLineShiftDate(CurrentPlantId, lineId, shiftId, productionDate), models);
                }

            }
            catch (Exception exc)
            {

            }

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProdEntry(IMProdModel model)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                model.PlantId = CurrentPlantId;
                model.LastModified = DateTime.Now;
                model.DateEntered = DateTime.Now;
                model.EnteredBy = CurrentUser;
                model.ModifiedBy = CurrentUser;
                model.RawMaterialReceivedID = GetRawMaterialReceivedId(CurrentPlantId, model.LotNumber);

                using (IMProductionService service = new IMProductionService())
                {
                    service.CreateEntries(Mapper.Map<IMProdModel, IMProdDto>(model));
                }
                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception ex)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, ex.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        private int GetRawMaterialReceivedId(int plantId, string lotNumber)
        {
            return (new RawMaterialReceivedService()).GetByPlantIdLotNumber(plantId, lotNumber).Id;
        }


    }
}