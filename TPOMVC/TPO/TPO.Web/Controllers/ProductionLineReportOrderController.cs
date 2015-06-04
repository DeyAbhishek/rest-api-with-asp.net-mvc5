using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Production;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ProductionLineReportOrderController : BaseController
    {
        //
        // GET: /ProductionLineReportOrder/
        [HttpGet]
        public ActionResult Index()
        {
            //  List<ProdLineReportOrderModel> model;
            List<ProductionLinesModel> model;
            using (var service = new ProductionLineService())
            {
                model = Mapper.Map<List<ProductionLinesDto>, List<ProductionLinesModel>>(service.GetByPlant(CurrentPlantId)).OrderBy(x => x.RepOrder).ToList();

            }
            
            return View(model);
        }


        [HttpPost]
       public ActionResult Index(ProductionLinesModel model, int newOrder, int originalOrder)
       {
            using (var svc = new ProductionLineService())
            {
                //List<ProductionLinesModel> m = Mapper.Map<List<ProductionLinesDto>, List<ProductionLinesModel>>(svc.GetAll());
                var dto = Mapper.Map<ProductionLinesModel, ProductionLinesDto>(model);
                dto.PlantID = CurrentPlantId;
               svc.Reorder(dto, newOrder, originalOrder);
            }

            return RedirectToAction("Index");
        }

       
	}
}