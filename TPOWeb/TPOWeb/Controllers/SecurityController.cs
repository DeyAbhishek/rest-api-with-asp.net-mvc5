using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Model.Security;
using TPO.BL.System;

namespace TPOWeb.Controllers
{
    public class SecurityController : Controller
    {
        //
        // GET: /Security/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormUnlock() 
        {
            FormUnlockModel model = new FormUnlockModel();
            return PartialView("/Views/Security/_FormUnlock.cshtml", model);
        }

        [HttpPost]
        public JsonResult FormUnlock(FormUnlockModel model) 
        {
            model.TriedUnlock = true;
            model.Unlocked = SystemSettings.CheckFormPassword(model.Password);
            return Json(model);
        }
        //public ActionResult FormUnlock(FormUnlockModel model)
        //{
        //    model.TriedUnlock = true;
        //    model.Unlocked = SystemSettings.CheckFormPassword(model.Password);
        //    return PartialView("\\Shared\\_FormUnlock", model);
        //}
	}
}