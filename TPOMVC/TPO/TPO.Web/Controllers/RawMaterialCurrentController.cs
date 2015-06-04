using System.Web.Mvc;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class RawMaterialCurrentController : Controller
    {
        //
        // GET: /RawMaterialCurrent/
        public ActionResult Index()
        {
            return View();
        }
	}
}