using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Production;
using TPO.Services.Products;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ProdLinePerformanceTargetController : BaseController
    {
        //
        // GET: /ProdLinePerformanceTarget/
        [HttpGet]
        public ActionResult Index(int lineId = -1)
        {
            ProdLinePerformanceTargetModel model = new ProdLinePerformanceTargetModel() {CanShow = false};
            if (lineId >= 0)
            {
                model.ProdLineID = lineId;
                model.TPTUse = "L";
                
                using (ProdLinesPerformService service = new ProdLinesPerformService())
                {
                    var dto = service.GetByLineId(lineId);
                    if (dto != null)
                    {
                        model = Mapper.Map<ProdLinesPerformDto, ProdLinePerformanceTargetModel>(dto);
                    }
                }
                ProductionLinesModel productionLine = GetProductionLine(lineId);
                ProdLineTypeModel prodLineType = GetProductionLineType(productionLine.LineTypeID);
                model.ProdLineDescCode = prodLineType.ProdLineTypeCode;
                model.CanShow = true;
                
            }
            return View(model);
        }

        private ProdLineTypeModel GetProductionLineType(int lineTypeId)
        {
            ProdLineTypeModel lineType = new ProdLineTypeModel();
            using (ProdLineTypeService service = new ProdLineTypeService())
            {
                var dto = service.Get(lineTypeId);
                lineType = Mapper.Map<ProdLineTypeDto, ProdLineTypeModel>(dto);
            }
            return lineType;
        }

        [HttpPost]
        public ActionResult Index(int lineId, ProdLinePerformanceTargetModel model)
        {
            using (ProdLinesPerformService service = new ProdLinesPerformService())
            {
                ProdLinesPerformDto dto;
                model.DateChange = DateTime.Now;
                ProductionLinesModel productionLine = GetProductionLine(lineId);

                model.LocID = productionLine.PlantId;
                if (string.IsNullOrEmpty(model.LineCode))
                    model.LineCode = lineId.ToString();

                if (string.IsNullOrEmpty(model.TPTUse))
                    model.TPTUse = "L";

                model.ModifiedBy = CurrentUser;
                model.LastModified = DateTime.Now;

                if (model.Id > 0)
                {
                    dto = service.Get(model.Id);
                    model.EnteredBy = dto.EnteredBy;
                    model.DateEntered = dto.DateEntered;

                    Mapper.Map(model, dto);
                    service.Update(dto);
                }
                else
                {
                    model.EnteredBy = CurrentUser;
                    model.DateEntered = DateTime.Now;

                    dto = new ProdLinesPerformDto();
                    Mapper.Map(model, dto);
                    service.Add(dto);
                }
                
                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            return RedirectToAction("Index", new {lineId = lineId});
        }
        
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetLines()
        {
            List<ProductionLinesModel> productionLines = new List<ProductionLinesModel>();
            using (ProductionLineService service = new ProductionLineService())
            {
                var dto = service.GetAll().OrderBy(r => r.LineDesc).ToList();
                productionLines.AddRange(Mapper.Map<List<ProductionLinesDto>, List<ProductionLinesModel>>(dto));
            }
            return Json(productionLines, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetProdLinePerformanceProd(int prodLineId)
        {
            ProductionLinesModel productionLine = GetProductionLine(prodLineId);
            List<ProdLinesPerformanceTargetProductModel> prodLineProducts = new List<ProdLinesPerformanceTargetProductModel>();
            using (ProdLinesPerformProdService service = new ProdLinesPerformProdService())
            {
                var dtos = service.GetByProdLineId(prodLineId);
                prodLineProducts.AddRange(Mapper.Map<List<ProdLinesPerformProdDto>, List<ProdLinesPerformanceTargetProductModel>>(dtos));
            }
            using (TPOProductService productService = new TPOProductService())
            {
                var dtos = productService.GetAllByProdLineId(prodLineId);
                foreach (var dto in dtos)
                {
                    if (prodLineProducts.FirstOrDefault(p => p.ProductID == dto.ID) != null) continue;
                    prodLineProducts.Add(
                        new ProdLinesPerformanceTargetProductModel()
                        {
                            LocID = productionLine.PlantId,
                            ProdLineID = prodLineId,
                            ProductID = dto.ID,
                            ProductName = dto.ProductCode,
                            Throughput = 0,
                        }
                        );
                }
            }
            return Json(prodLineProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxProductUpdate(string row)
        {
            ProdLinesPerformanceTargetProductModel model = JsonConvert.DeserializeObject<ProdLinesPerformanceTargetProductModel>(row);
            if (model != null)
            {
                model.DateChange = DateTime.Now;
                ProdLinesPerformProdDto dto = new ProdLinesPerformProdDto();
                using (ProdLinesPerformProdService service = new ProdLinesPerformProdService())
                {
                    Mapper.Map(model, dto);
                    if (model.Id > 0)
                        service.Update(dto);
                    else
                    {
                        service.Add(dto);
                    }
                }
                return RedirectToAction("Index", new { lineId = model.ProdLineID });
            }
            return RedirectToAction("Index");
        }
        private ProductionLinesModel GetProductionLine(int prodLineId)
        {
            ProductionLinesModel productionLine = new ProductionLinesModel();
            using (ProductionLineService service = new ProductionLineService())
            {
                var dto = service.Get(prodLineId);
                productionLine = Mapper.Map<ProductionLinesDto, ProductionLinesModel>(dto);
            }
            return productionLine;
        }
	}
}