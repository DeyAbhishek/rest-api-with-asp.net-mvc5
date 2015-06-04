using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services;
using TPO.Services.Application;
using TPO.Web.Helpers;

namespace TPO.Web.Controllers
{
    [Authorize]
    public class BaseController: Controller
    {
        // TODO: move to config?
        internal int DefaultPageSize = 15;
        internal int DefaultPage = 1;

       
        public int CurrentPlantId
        {
            get 
            { 
                if ( Session["CurrentPlantId"] == null )
                {
                    using ( SecurityService service = new SecurityService() )
                    {
                        int plantId = service.GetByUserName(CurrentUser).PlantId;
                        Session.Add("CurrentPlantId", plantId);
                    }
                }

                return (int)Session["CurrentPlantId"]; 
            }
            set
            {
                Session.Add("CurrentPlantId", value);
            }
        }

        public string CurrentUser
        {
            get {  return User.Identity.Name.ToUpper(); }
        }

        public JsonResult BuildJsonResult(object rows, int totalRows, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new DataGridResult
            {
                rows = rows,
                total = totalRows
            }, jsonRequestBehavior);
        }

        public Core.ResponseMessage SetResponseMesssage(ActionTypeMessage actionTypeMessage, string message = "")
        {
            var status = Common.Constants.ResponseMessage.OK;

            switch (actionTypeMessage)
            {
                case ActionTypeMessage.SuccessfulSave:
                    TempData[Common.Constants.ResponseMessage.ActionMessage] = General.ResponseMessageSuccessSave;
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeSuccess;
                    break;
                case ActionTypeMessage.SuccessfulDelete:
                    TempData[Common.Constants.ResponseMessage.ActionMessage] = General.ResponseMessageSuccessDelete;
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeSuccess;
                    break;
                case ActionTypeMessage.FailedSave:
                    TempData[Common.Constants.ResponseMessage.ActionMessage] = General.ResponseMessageFailSave;
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeError;
                    status = "error";
                    break;
                case ActionTypeMessage.FailedDelete:
                    TempData[Common.Constants.ResponseMessage.ActionMessage] = General.ResponseMessageFailDelete;
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeError;
                    status = "error";
                    break;
                case ActionTypeMessage.Warning: //TODO: Add default warning message
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeWarning;
                    status = "warning";
                    break;
                case ActionTypeMessage.Error:
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeError;
                    status = "error";
                    break;
                case ActionTypeMessage.Success:
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeSuccess;
                    break;
                default: // info as default, TODO: Add default info message
                    TempData[Common.Constants.ResponseMessage.ActionMessageType] = General.ResponseTypeInfo;
                    status = "info";
                    break;
            }

            if (message != "")
            {
                TempData[Common.Constants.ResponseMessage.ActionMessage] = message;
            }

            return new Core.ResponseMessage {ActionMessage = (string)TempData[Common.Constants.ResponseMessage.ActionMessage], ActionType = (string)TempData[Common.Constants.ResponseMessage.ActionMessageType], ActionStatus = status};
        }

        public static DateTime GetFirstDayOfWeek(DateTime theDate)
        {
            DateTime firstDay = theDate;

            while (firstDay.DayOfWeek != DayOfWeek.Sunday)
                firstDay = firstDay.AddDays(-1);
            return firstDay;
        }

        #region Select Lists
        

        protected SelectList GetThickUoMOptions()
        {
            List<UnitOfMeasureDto> dtos = new List<UnitOfMeasureDto>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                dtos.AddRange(svc.GetByTypeCode(UnitOfMeasureTypeDto.THICKNESS));
            }
            return new SelectList(dtos, "ID", "Code");
        }

        protected SelectList GetForceUoMOptions()
        {
            List<UnitOfMeasureDto> dtos = new List<UnitOfMeasureDto>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                dtos.AddRange(svc.GetByTypeCode(UnitOfMeasureTypeDto.FORCE));
            }
            return new SelectList(dtos, "ID", "Code");
        }

        protected SelectList GetTempUoMOptions()
        {
            List<UnitOfMeasureDto> dtos = new List<UnitOfMeasureDto>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                dtos.AddRange(svc.GetByTypeCode(UnitOfMeasureTypeDto.TEMPERATURE));
            }
            return new SelectList(dtos, "ID", "Code");
        }

        protected SelectList GetWeightUoMOptions()
        {
            List<UnitOfMeasureDto> dtos = new List<UnitOfMeasureDto>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                dtos.AddRange(svc.GetByTypeCode(UnitOfMeasureTypeDto.WEIGHT));
            }
            return new SelectList(dtos, "ID", "Code");
        }

        protected SelectList GetLengthUoMOptions()
        {
            List<UnitOfMeasureDto> dtos = new List<UnitOfMeasureDto>();
            using (UnitOfMeasureService svc = new UnitOfMeasureService())
            {
                dtos.AddRange(svc.GetByTypeCode(UnitOfMeasureTypeDto.LENGTH));
            }
            return new SelectList(dtos, "ID", "Code");
        }

        #endregion
    }
}