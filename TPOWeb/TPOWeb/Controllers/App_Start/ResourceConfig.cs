using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TPO.BL.Repositories.Message;

namespace TPOWeb.Controllers.App_Start
{
    public class ResourceConfig
    {
        public static void LoadResources(System.Web.HttpApplication application, string resourceFileList)
        {
            string[] resourceFiles = resourceFileList.Split(new char[]{' ', ','});
            int priority = 1;

            foreach (string resourceFile in resourceFiles)
                MessageRepository.AddResourceFile(application.Server.MapPath("bin"), resourceFile, priority++);
        }

    }
}