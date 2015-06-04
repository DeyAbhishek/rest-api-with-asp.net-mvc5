using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;

namespace TPO.Services.Production
{
    public class ProductionShiftUseService : ServiceBaseGeneric<ProductionShiftUseDto, ProductionShiftUse>, ITpoService<ProductionShiftUseDto>
    {
        public List<ProductionShiftUseDto> GetAllByLineId(int lineId)
        {
            return MapEntityList(_repository.Repository<ProductionShiftUse>()
                .GetAllBy(s => s.LineID == lineId).ToList());
        }

        public ProductionShiftUseDto GetByPlantIdLineIdShiftId(int plantId, int lineId, int shiftId)
        {
            return MapEntity(_repository.Repository<ProductionShiftUse>()
                .GetAllBy(s => s.PlantID == plantId && s.LineID == lineId && s.ShiftID == shiftId).FirstOrDefault());
        }

        public void SetProductionLineSchedule(ProductionShiftUseDto dto, string startDateString)
        {
            DateTime startDate = DateTime.Parse(startDateString);
            var entity = _repository.Repository<ProductionShiftUse>().GetById(dto.ID);
            if (entity != null)
            {
                DateTime endDate = entity.ProductionShift.ProductionLineSchedules.Max(s => s.ProductionDate);
                DateTime currentDate = startDate;
                while (currentDate <= endDate)
                {
                    var schedule = GetSchedule(dto, entity, currentDate);

                    var prodDateChange =
                        entity.ProductionShift.ProductionShiftType.ProdDateChanges.FirstOrDefault(
                            d => d.LineID == dto.LineID);
                    if (prodDateChange != null)
                    {
                        ProcessShiftRotation(entity, currentDate, prodDateChange, schedule);

                        AddScheduleToRepository(schedule);
                    }
                    currentDate = currentDate.AddDays(1);
                }
                CommitUnitOfWork();
            }
        }

        private void AddScheduleToRepository(ProductionLineSchedule schedule)
        {
            if (schedule.ID == 0)
                _repository.Repository<ProductionLineSchedule>().Insert(schedule);
            else
                _repository.Repository<ProductionLineSchedule>().Update(schedule);
        }

        private ProductionLineSchedule GetSchedule(ProductionShiftUseDto dto, ProductionShiftUse entity, DateTime currentDate)
        {
            var schedule =
                entity.ProductionShift.ProductionLineSchedules.FirstOrDefault(
                    s => s.ProductionDate == currentDate);

            if (schedule == null)
                schedule = new ProductionLineSchedule()
                {
                    PlantID = dto.PlantID,
                    LineID = dto.LineID,
                    ShiftID = dto.ShiftID,
                    ProductionDate = currentDate,
                    DateEntered = DateTime.Now,
                    LastModified = DateTime.Now
                };

            schedule.LastModified = DateTime.Now;
            schedule.ModifiedBy = CurrentUserName;
            return schedule;
        }

        private void ProcessShiftRotation(ProductionShiftUse entity, DateTime currentDate, ProdDateChange prodDateChange,
            ProductionLineSchedule schedule)
        {
            switch (entity.ProductionShift.ProductionShiftType.Code)
            {
                case "3":
                    ProcessShiftType3(currentDate, prodDateChange, schedule, entity);
                    break;
                case "4":
                    ProcessShiftType4(currentDate, prodDateChange, schedule, entity);
                    break;
                case "5":
                    ProcessShiftType5(currentDate, prodDateChange, schedule, entity);
                    break;
                default:
                    ProcessBasicShiftTypes(currentDate, schedule, entity);
                    break;
            }
        }

        private void ProcessBasicShiftTypes(DateTime currentDate, ProductionLineSchedule schedule,
            ProductionShiftUse entity)
        {
            switch (currentDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    schedule.MinutesScheduled = entity.Day1Minutes;
                    break;
                case DayOfWeek.Monday:
                    schedule.MinutesScheduled = entity.Day2Minutes;
                    break;
                case DayOfWeek.Tuesday:
                    schedule.MinutesScheduled = entity.Day3Minutes;
                    break;
                case DayOfWeek.Wednesday:
                    schedule.MinutesScheduled = entity.Day4Minutes;
                    break;
                case DayOfWeek.Thursday:
                    schedule.MinutesScheduled = entity.Day5Minutes;
                    break;
                case DayOfWeek.Friday:
                    schedule.MinutesScheduled = entity.Day6Minutes;
                    break;
                case DayOfWeek.Saturday:
                    schedule.MinutesScheduled = entity.Day7Minutes;
                    break;
            }
        }

        private void ProcessShiftType5(DateTime currentDate, ProdDateChange prodDateChange,
            ProductionLineSchedule schedule, ProductionShiftUse entity)
        {
            switch (Math.Abs((currentDate - prodDateChange.RotationStart).Days)%14)
            {
                case 0:
                case 1:
                case 5:
                case 6:
                case 7:
                case 10:
                case 11:
                    switch (currentDate.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            schedule.MinutesScheduled = entity.Day1Minutes;
                            break;
                        case DayOfWeek.Monday:
                            schedule.MinutesScheduled = entity.Day2Minutes;
                            break;
                        case DayOfWeek.Tuesday:
                            schedule.MinutesScheduled = entity.Day3Minutes;
                            break;
                        case DayOfWeek.Wednesday:
                            schedule.MinutesScheduled = entity.Day4Minutes;
                            break;
                        case DayOfWeek.Thursday:
                            schedule.MinutesScheduled = entity.Day5Minutes;
                            break;
                        case DayOfWeek.Friday:
                            schedule.MinutesScheduled = entity.Day6Minutes;
                            break;
                        case DayOfWeek.Saturday:
                            schedule.MinutesScheduled = entity.Day7Minutes;
                            break;
                    }
                    break;
                default:
                    schedule.MinutesScheduled = 0;
                    break;
            }
        }

        private void ProcessShiftType4(DateTime currentDate, ProdDateChange prodDateChange,
            ProductionLineSchedule schedule, ProductionShiftUse entity)
        {
            switch (Math.Abs((currentDate - prodDateChange.RotationStart).Days)%14)
            {
                case 0:
                case 1:
                case 2:
                case 8:
                case 9:
                case 10:
                case 11:
                    switch (currentDate.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            schedule.MinutesScheduled = entity.Day1Minutes;
                            break;
                        case DayOfWeek.Monday:
                            schedule.MinutesScheduled = entity.Day2Minutes;
                            break;
                        case DayOfWeek.Tuesday:
                            schedule.MinutesScheduled = entity.Day3Minutes;
                            break;
                        case DayOfWeek.Wednesday:
                            schedule.MinutesScheduled = entity.Day4Minutes;
                            break;
                        case DayOfWeek.Thursday:
                            schedule.MinutesScheduled = entity.Day5Minutes;
                            break;
                        case DayOfWeek.Friday:
                            schedule.MinutesScheduled = entity.Day6Minutes;
                            break;
                        case DayOfWeek.Saturday:
                            schedule.MinutesScheduled = entity.Day7Minutes;
                            break;
                    }
                    break;
                default:
                    schedule.MinutesScheduled = 0;
                    break;
            }
        }

        private void ProcessShiftType3(DateTime currentDate, ProdDateChange prodDateChange,
            ProductionLineSchedule schedule, ProductionShiftUse entity)
        {
            switch (Math.Abs((currentDate - prodDateChange.RotationStart).Days)%14)
            {
                case 0:
                case 1:
                case 4:
                case 5:
                case 6:
                case 9:
                case 10:
                    switch (currentDate.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            schedule.MinutesScheduled = entity.Day1Minutes;
                            break;
                        case DayOfWeek.Monday:
                            schedule.MinutesScheduled = entity.Day2Minutes;
                            break;
                        case DayOfWeek.Tuesday:
                            schedule.MinutesScheduled = entity.Day3Minutes;
                            break;
                        case DayOfWeek.Wednesday:
                            schedule.MinutesScheduled = entity.Day4Minutes;
                            break;
                        case DayOfWeek.Thursday:
                            schedule.MinutesScheduled = entity.Day5Minutes;
                            break;
                        case DayOfWeek.Friday:
                            schedule.MinutesScheduled = entity.Day6Minutes;
                            break;
                        case DayOfWeek.Saturday:
                            schedule.MinutesScheduled = entity.Day7Minutes;
                            break;
                    }
                    break;
                default:
                    schedule.MinutesScheduled = 0;
                    break;
            }
        }
    }
}
