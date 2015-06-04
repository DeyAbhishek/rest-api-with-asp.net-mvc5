using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TPO.Common.Enums;

namespace TPO.Web.Controllers
{
    public class MessageController : BaseController
    {
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetMessage(string message, string result)
        {
            string responseMessage = string.Empty;
            string responseType = string.Empty;
            string[] response = new string[2];

            switch (message)
            {
                case "save":
                    if (result == "true")
                    {
                        SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                        responseMessage = TempData["ActionMessage"].ToString();
                        responseType = TempData["ActionMessageType"].ToString();

                        response[0] = responseMessage;
                        response[1] = responseType;
                    }
                    else
                    {
                        SetResponseMesssage(ActionTypeMessage.FailedSave);
                        responseMessage = TempData["ActionMessage"].ToString();
                        responseType = TempData["ActionMessageType"].ToString();

                        response[0] = responseMessage;
                        response[1] = responseType;
                    }
                    break;
                case "delete":
                    if (result == "true")
                    {
                        SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
                        responseMessage = TempData["ActionMessage"].ToString();
                        responseType = TempData["ActionMessageType"].ToString();

                        response[0] = responseMessage;
                        response[1] = responseType;
                    }
                    else
                    {
                        SetResponseMesssage(ActionTypeMessage.FailedDelete);
                        responseMessage = TempData["ActionMessage"].ToString();
                        responseType = TempData["ActionMessageType"].ToString();

                        response[0] = responseMessage;
                        response[1] = responseType;
                    }
                    break;
                default:
                    //ask about default statement
                    break;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}