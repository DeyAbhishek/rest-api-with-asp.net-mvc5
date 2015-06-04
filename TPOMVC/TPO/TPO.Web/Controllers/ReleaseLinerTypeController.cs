using System.Web.Mvc;
using TPO.Common.Enums;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ReleaseLinerTypeController : BaseController
    {
        //
        // GET: /ReleaseLinerType/
        public ActionResult Index()
        {
            return View(new ScrimTypeModel { PlantID = CurrentPlantId, IsLiner = true});
        }
	}
}