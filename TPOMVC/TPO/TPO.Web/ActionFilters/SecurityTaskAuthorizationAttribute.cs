using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Services.Application;
using TPO.Web.Controllers;
using TPO.Web.Filters;
using TPO.Web.Models;
using WebMatrix.WebData;

namespace TPO.Web.ActionFilters
{
    public class SecurityTaskAuthorizationAttribute : AuthorizeAttribute
    {

        public SecurityTask[] RequiredSecurityTasks { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (RequiredSecurityTasks == null || RequiredSecurityTasks.Count() == 0)
            {
                return true;
            }
            bool retVal = false;

            var user = HttpContext.Current.Session["CurrentUserContext"];
            if (user != null)
            {
                try
                {
                    using (var svc = new SecurityService())
                    {
                        retVal = svc.UserCanOneOrMoreDoTasks((UserDto) user, RequiredSecurityTasks.ToList());
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Log
                    retVal = false;
                }
            }
            return retVal;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpContext context = HttpContext.Current;

            //session timed out
            if (context.Session["CurrentUserContext"] == null && context.Session.IsNewSession && context.Request.Cookies["TPOWeb_SessionId"] != null)
            {
                context.Request.Cookies.Clear();
                //TODO: Log?
                var controller = (BaseController)filterContext.Controller;      
                controller.SetResponseMesssage(ActionTypeMessage.Warning, General.ResponseSessionTimeout);

                filterContext.Result = new RedirectToRouteResult("",
                    new RouteValueDictionary(new {controller = "Security", action = "TimeOut"}), false);
            }
            else if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                //TODO: LOG?
                var controller = (BaseController)filterContext.Controller;  
                controller.SetResponseMesssage(ActionTypeMessage.Warning, General.ResponseUnauthorized);
                filterContext.Result = new RedirectToRouteResult("",
                    new RouteValueDictionary(new {controller = "Security", action = "Unauthorized"}), false);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}