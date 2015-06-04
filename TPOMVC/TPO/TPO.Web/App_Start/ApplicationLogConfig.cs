using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPO.Services.Application;
using TPO.Services.Core;

namespace TPO.Web
{
    public class ApplicationLogConfig
    {
        public static void EnableLogging(Type callingType)
        {
            IApplicationLogService log = ApplicationLogService.InitializeLogger(log4net.LogManager.GetLogger(callingType), null);
        }
    }
}