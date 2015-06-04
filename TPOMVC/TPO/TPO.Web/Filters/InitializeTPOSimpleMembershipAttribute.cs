using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;   
using System.Web.Mvc;
using TPO.Services.Application;
using WebMatrix.WebData;

namespace TPO.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeTPOSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static TPOSimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class TPOSimpleMembershipInitializer
        {
            public TPOSimpleMembershipInitializer()
            {   
                try
                {
                    using (var svc = new SecurityService())
                    {
                        svc.Initialize();
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("TPO Simple Membership Cannot be Initialized.", ex);
                }
            }
        }
    }
}