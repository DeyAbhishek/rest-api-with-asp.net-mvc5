using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPO.BL.ApplicationLog;

namespace TPOWeb.Controllers.App_Start
{
    public class ApplicationLogConfig
    {
        public static void EnableLogging(Type callingType)
        {
            IApplicationLog log = ApplicationLog.GetLogger(log4net.LogManager.GetLogger(callingType), null);

            if (log.IsInfoEnabled) log.Info("Started Logging");

        }

    }
}