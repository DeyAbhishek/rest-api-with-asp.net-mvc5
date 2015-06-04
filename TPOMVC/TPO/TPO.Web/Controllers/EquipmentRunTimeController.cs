using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.Common.Enums;
using TPO.Services.Production;
using TPO.Common.DTOs;
using TPO.Web.Core;
using TPO.Web.Models;


namespace TPO.Web.Controllers
{
    public class EquipmentRunTimeController : BaseController
    {
        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = false)]
        public ActionResult Index()
        {
            EquipmentRunTimeModel model = new EquipmentRunTimeModel();
            using (ProductionLineService service = new ProductionLineService()) {
                model.ProductionLineList = new SelectList(service.GetAll().OrderBy(l => l.LineDesc), "ID", "LineDesc");
            }

            // should only show 15 weeks starting from 5 weeks ago
            DateTime theWeek = GetFirstDayOfWeek(DateTime.Now).AddDays((5 * 7 * -1));
            List<SelectListItem> weeks = new List<SelectListItem>();
            for (int weekIdx = -5; weekIdx <= 10; weekIdx++ )
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = theWeek.ToShortDateString();
                listItem.Text = theWeek.ToShortDateString();
                weeks.Add(listItem);

                theWeek = theWeek.AddDays(7);
            }
            model.WeekStartingList = new SelectList(weeks, "Value", "Text", GetFirstDayOfWeek(DateTime.Now).ToShortDateString());
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(EquipmentRunTimeModel model)
        {
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveGridInformation(int productionLineId, DateTime week, IEnumerable<ProductionLineScheduleWeekly> rows)
        {
            ResponseMessage responseMessage;

            try
            {
                //now wee need to go through each day of the of each row and save it
                foreach (ProductionLineScheduleWeekly row in rows)
                {
                    for (int idx = 0; idx <= 6; idx++)
                    {
                        ProductionLineScheduleDto item = new ProductionLineScheduleDto();
                        item.PlantID = CurrentPlantId;
                        item.LineID = productionLineId;
                        item.ShiftID = row.ShiftId;
                        item.ProductionDate = week.AddDays(idx);
                        item.ModifiedBy = CurrentUser;
                        item.LastModified = DateTime.Now;
                        
                        switch (idx)
                        {
                            case 0:
                                item.ID = row.Day1Id;
                                item.MinutesScheduled = row.Day1;
                                break;
                            case 1:
                                item.ID = row.Day2Id;
                                item.MinutesScheduled = row.Day2;
                                break;
                            case 2:
                                item.ID = row.Day3Id;
                                item.MinutesScheduled = row.Day3;
                                break;
                            case 3:
                                item.ID = row.Day4Id;
                                item.MinutesScheduled = row.Day4;
                                break;
                            case 4:
                                item.ID = row.Day5Id;
                                item.MinutesScheduled = row.Day5;
                                break;
                            case 5:
                                item.ID = row.Day6Id;
                                item.MinutesScheduled = row.Day6;
                                break;
                            case 6:
                                item.ID = row.Day7Id;
                                item.MinutesScheduled = row.Day7;
                                break;
                        }
                        using (ProductionLineScheduleService service = new ProductionLineScheduleService())
                        {
                            if ( item.ID == 0)
                            {
                                item.EnteredBy = CurrentUser;
                                item.DateEntered = DateTime.Now;
                                service.Add(item);
                            }
                            else
                            {
                                service.Update(item);
                            }
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = false)]
        public JsonResult FetchGridInfo(int productionLineId, DateTime week)
        {
            List<ProductionLineScheduleWeekly> items = new List<ProductionLineScheduleWeekly>();
            List<ProductionLineScheduleDto> entities;

            List<ProductionShiftDto> shifts;
            using (ProductionShiftService service = new ProductionShiftService())
                shifts = service.GetAllByPlantId(CurrentPlantId);

            foreach (ProductionShiftDto shift in shifts)
            { 
                using (ProductionLineScheduleService service = new ProductionLineScheduleService())
                    entities = service.GetWeekly(CurrentPlantId, shift.ID, productionLineId, GetFirstDayOfWeek(week));
                
                if (entities.Count == 0)
                {
                    items.Add(CreateWeeklyInfo(shift));
                }
                else
                { 
                    items.Add(PopulateWeeklyInfo(shift, entities));
                }
            }
            
           

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        protected ProductionLineScheduleWeekly PopulateWeeklyInfo(ProductionShiftDto shift, List<ProductionLineScheduleDto> entities)
        {
            ProductionLineScheduleWeekly weeklyItem = new ProductionLineScheduleWeekly();
            weeklyItem.ShiftId = shift.ID;
            weeklyItem.ShiftCode = shift.Code;

            foreach (ProductionLineScheduleDto entity in entities)
            {    
                ProductionLineScheduleModel item = new ProductionLineScheduleModel();
                AutoMapper.Mapper.Map(entity, item);

                switch (entity.ProductionDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        weeklyItem.Day1 = item.MinutesScheduled;
                        weeklyItem.Day1Id = item.Id;
                        break;
                    case DayOfWeek.Monday:
                        weeklyItem.Day2 = item.MinutesScheduled;
                        weeklyItem.Day2Id = item.Id;
                        break;
                    case DayOfWeek.Tuesday:
                        weeklyItem.Day3 = item.MinutesScheduled;
                        weeklyItem.Day3Id = item.Id;
                        break;
                    case DayOfWeek.Wednesday:
                        weeklyItem.Day4 = item.MinutesScheduled;
                        weeklyItem.Day4Id = item.Id;
                        break;
                    case DayOfWeek.Thursday:
                        weeklyItem.Day5 = item.MinutesScheduled;
                        weeklyItem.Day5Id = item.Id;
                        break;
                    case DayOfWeek.Friday:
                        weeklyItem.Day6 = item.MinutesScheduled;
                        weeklyItem.Day6Id = item.Id;
                        break;
                    case DayOfWeek.Saturday:
                        weeklyItem.Day7 = item.MinutesScheduled;
                        weeklyItem.Day7Id = item.Id;
                        break;
                }
            }

            return weeklyItem;
        }
        protected ProductionLineScheduleWeekly CreateWeeklyInfo(ProductionShiftDto shift)
        {
            ProductionLineScheduleWeekly weeklyItem = new ProductionLineScheduleWeekly();
            weeklyItem.ShiftId = shift.ID;
            weeklyItem.ShiftCode = shift.Code;
            weeklyItem.Day1 = 0;
            weeklyItem.Day2 = 0;
            weeklyItem.Day3 = 0;
            weeklyItem.Day4 = 0;
            weeklyItem.Day5 = 0;
            weeklyItem.Day6 = 0;

            weeklyItem.Day1Id = 0;
            weeklyItem.Day2Id = 0;
            weeklyItem.Day3Id = 0;
            weeklyItem.Day4Id = 0;
            weeklyItem.Day5Id = 0;
            weeklyItem.Day6Id = 0;

            return weeklyItem;
        }
    }
}