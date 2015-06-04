using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Services.Production;
using TPO.Web.Models;
using AutoMapper;
using TPO.Common.Enums;
using TPO.Web.Core;

namespace TPO.Web.Controllers
{
    public class WorkOrderController : BaseController
    {
        //
        // GET: /WorkOrder/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult WorkOrdersByLine(int lineID = 0, bool viewCompleted = false) 
        {
            List<WorkOrderModel> data = new List<WorkOrderModel>();
            using (WorkOrderService svc = new WorkOrderService()) 
            {
                var dtos = svc.GetByLineID(lineID, viewCompleted);
                data.AddRange(Mapper.Map<List<WorkOrderDto>, List<WorkOrderModel>>(dtos));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateWorkOrder(WorkOrderModel model) 
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<WorkOrderModel, WorkOrderDto>(model);
                using (WorkOrderService svc = new WorkOrderService())
                {
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;
                    dto.PlantID = CurrentPlantId;
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.DateEntered = dto.LastModified;
                        dto.EnteredBy = CurrentUser;
                        dto.RunOrder = svc.GetWorkOrderCount(dto.LineID) + 1;
                        dto.ID = svc.Add(dto);
                    }
                    model = Mapper.Map<WorkOrderDto, WorkOrderModel>(dto, model);
                }

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
        public JsonResult DeleteWorkOrder(int id = 0) 
        {
            ResponseMessage responseMessage;

            try
            {
                using (WorkOrderService svc = new WorkOrderService())
                {
                    svc.Delete(id);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
            }
            catch (Exception ex) 
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedDelete, ex.Message);
            }
            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reorder(int workOrderID = 0, int newOrder = 1) 
        {
            ResponseMessage message;
            try
            {
                using (WorkOrderService svc = new WorkOrderService())
                {
                    svc.Reorder(workOrderID, newOrder);
                }
                message = SetResponseMesssage(ActionTypeMessage.SuccessfulSave, "Run order successfully changed.");
            }
            catch (Exception ex) 
            {
                message = SetResponseMesssage(ActionTypeMessage.Error, "Unable to change run order.");
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Count(int lineID = 0) 
        {
            int count = 0;
            using (WorkOrderService svc = new WorkOrderService()) 
            {
                count = svc.GetWorkOrderCount(lineID);
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
	}
}