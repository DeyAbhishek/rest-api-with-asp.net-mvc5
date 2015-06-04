using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Web.Models;
using TPO.Services.TPOMonthlyProductionBudget;
using TPO.Common.Enums;

namespace TPO.Web.Controllers
{
    public class TPOMonthlyProductionBudgetController : BaseController
    {
        //
        // GET: /TPOMonthlyProductionBudget/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetYears()
        {
            List<KeyValuePair<int, int>> years = new List<KeyValuePair<int, int>>();
            int currentYear = DateTime.Now.Year;
            years.Add(new KeyValuePair<int, int>(currentYear - 2, currentYear - 2));
            years.Add(new KeyValuePair<int, int>(currentYear - 1, currentYear - 1));
            years.Add(new KeyValuePair<int, int>(currentYear, currentYear));
            years.Add(new KeyValuePair<int, int>(currentYear + 1, currentYear + 1));
            years.Add(new KeyValuePair<int, int>(currentYear + 2, currentYear + 2));
            return Json(years, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult BudgetTotalResult(int typeID)
        {
            TPOMonthlyProductionBudgetModel m = new TPOMonthlyProductionBudgetModel();
            double budgetTotal = 0;
            using (TPOMonthlyProductionBudgetService svc = new TPOMonthlyProductionBudgetService())
            {
                budgetTotal = svc.GetByYear(typeID).Sum(s => s.Budget);
            }
            return Json(budgetTotal, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult BudgetGrid(int typeID)
        {


            int total;
            List<TPOMonthlyProductionBudgetModel> model = new List<TPOMonthlyProductionBudgetModel>();
            TPOMonthlyProductionBudgetModel m = new TPOMonthlyProductionBudgetModel();
            using (TPOMonthlyProductionBudgetService svc = new TPOMonthlyProductionBudgetService())
            {
                for (int i = 1; i <= 12; i++)
                {
                    var d = svc.GetByYear(typeID).Count(q => q.Month == i);
                    if (d == 0)
                    {
                        ProductionBudgetDto dto = new ProductionBudgetDto();

                        using (TPOMonthlyProductionBudgetService service = new TPOMonthlyProductionBudgetService())
                        {
                            Mapper.Map(m, dto);

                            dto.TypeID = 1;
                            dto.Year = typeID;
                            dto.Month = i;
                            dto.PlantID = CurrentPlantId;
                            dto.LastModified = DateTime.Now;
                            dto.ModifiedBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
                            dto.EnteredBy = CurrentUser;
                            service.Add(dto);
                        }
                    }
                }

                var dtos = svc.GetByYear(typeID);
                total = dtos.Count;
                var currentDtos = dtos.OrderBy(r => r.Month).ToList();
                model.AddRange(Mapper.Map<List<ProductionBudgetDto>, List<TPOMonthlyProductionBudgetModel>>(currentDtos));
            }


            return BuildJsonResult(model, total);

        }

        [HttpPost]
        public JsonResult BudgetAjaxUpdate(string id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                TPOMonthlyProductionBudgetService srsrv = new TPOMonthlyProductionBudgetService();

                TPOMonthlyProductionBudgetModel model =
                    JsonConvert.DeserializeObject<TPOMonthlyProductionBudgetModel>(id);
                if (model != null)
                {
                    var x = srsrv.GetAll().Where(q => q.Year == model.Year).ToList();

                    if (!x.Any())
                    {
                        ProductionBudgetDto dto = new ProductionBudgetDto();

                        using (TPOMonthlyProductionBudgetService service = new TPOMonthlyProductionBudgetService())
                        {
                            Mapper.Map(model, dto);

                            dto.TypeID = 1;

                            dto.PlantID = CurrentPlantId;
                            dto.LastModified = DateTime.Now;
                            dto.ModifiedBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
                            dto.EnteredBy = CurrentUser;

                            if (model.Id > 0)
                            {
                                service.Update(dto);
                            }
                            else
                            {
                                service.Add(dto);
                            }
                        }
                    }
                    else
                    {
                        ProductionBudgetDto dto = new ProductionBudgetDto();

                        using (TPOMonthlyProductionBudgetService service = new TPOMonthlyProductionBudgetService())
                        {
                            Mapper.Map(model, dto);

                            dto.TypeID = 1;

                            dto.PlantID = CurrentPlantId;
                            dto.LastModified = DateTime.Now;
                            dto.ModifiedBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
                            dto.EnteredBy = CurrentUser;

                            service.Update(dto);
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

    }
}