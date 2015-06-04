using TPO.Common.Enums;
using TPO.Web.ActionFilters;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class TPOLineScrapCodeController : BaseController
    {
    }
}