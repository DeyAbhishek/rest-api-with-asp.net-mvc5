using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TPO.Data;
using TPO.Services;
using TPO.Services.Rework;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    public class CoatingProductionController : BaseController
    {
        // GET: CoatingProduction
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult FetchMasterRolls(int lineId, int? workOrderId)
        {
            List<TPOReworkRollModel> items = new List<TPOReworkRollModel>();
            using (TPOReworkRollService service = new TPOReworkRollService())
            {
                var dtos = service.GetMasterRollsByPlant(CurrentPlantId);
                AutoMapper.Mapper.Map(dtos, items);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}