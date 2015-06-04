using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class ProductionLineScheduleService : ServiceBase, ITpoService<ProductionLineScheduleDto>
    {
        public int Add(ProductionLineScheduleDto dto)
        {
            int result = -1;
            var entity = new ProductionLineSchedule();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<ProductionLineSchedule>().Insert(entity);
                _repository.Save();
                
                if ( entity != null )
                    result = entity.ID;
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception exc)
            {
                throw new Exception("Error occurred while adding item.", exc);
            }

            return result;
        }

        public List<ProductionLineScheduleDto> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<ProductionLineScheduleDto> GetWeekly(int plantId, int shiftId, int lineId, DateTime startOfWeek)
        {
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var entities = _repository.Repository<ProductionLineSchedule>().GetAll()
                .Where(s => s.PlantID == plantId && s.LineID == lineId && s.ShiftID == shiftId && s.ProductionDate >= startOfWeek && s.ProductionDate <= endOfWeek)
                .OrderBy(s => s.ProductionDate).ToList();
            return Mapper.Map<List<ProductionLineSchedule>, List<ProductionLineScheduleDto>>(entities);
        }
        public List<ProductionLineScheduleDto> GetByShift(int lineID, int shiftID, DateTime productionDate) 
        {
            var entities = _repository.Repository<ProductionLineSchedule>().GetAllBy(pl => pl.LineID == lineID && pl.ShiftID == shiftID && pl.ProductionDate == productionDate).ToList();
            return Mapper.Map<List<ProductionLineSchedule>, List<ProductionLineScheduleDto>>(entities);
        }

        public ProductionLineScheduleDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductionLineScheduleDto dto)
        {
            var entity = _repository.Repository<ProductionLineSchedule>().GetById(dto.ID);
            entity.MinutesScheduled = dto.MinutesScheduled;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.LastModified = dto.LastModified;

            try
            {
                _repository.Repository<ProductionLineSchedule>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception exc)
            {
                throw new Exception("Error occurred while updating item.", exc);
            }
        }

        public List<ProductionLineScheduleDto> GetAllByPlantId(int plantId)
        {
            var entities = _repository.Repository<ProductionLineSchedule>().GetAll()
                           .Where(s => s.PlantID == plantId)
                           .OrderBy(s => s.ProductionDate).ToList();
            return Mapper.Map<List<ProductionLineSchedule>, List<ProductionLineScheduleDto>>(entities);
        }

        public List<ProductionLineScheduleDto> GetAllByLineId(int lineId)
        {
            var entities = _repository.Repository<ProductionLineSchedule>().GetAll()
                           .Where(s => s.LineID == lineId)
                           .OrderBy(s => s.ProductionDate).ToList();
            return Mapper.Map<List<ProductionLineSchedule>, List<ProductionLineScheduleDto>>(entities);
        }

        public void ResetShiftDefaults(ProductionLineScheduleDto dto)
        {
            var entity = _repository.Repository<ProductionLineSchedule>().GetById(dto.ID);
            foreach (var shiftUse in entity.ProductionShift.ProductionShiftUses.Where(u => u.LineID == dto.LineID))
            {
                shiftUse.Day1Minutes =
                    ((int)(entity.ProductionShift.EndTime - entity.ProductionShift.StartTime + new TimeSpan(1, 0, 0, 0))
                        .TotalMinutes) % 1440;
                shiftUse.Day2Minutes = shiftUse.Day1Minutes;
                shiftUse.Day3Minutes = shiftUse.Day1Minutes;
                shiftUse.Day4Minutes = shiftUse.Day1Minutes;
                shiftUse.Day5Minutes = shiftUse.Day1Minutes;
                shiftUse.Day6Minutes = shiftUse.Day1Minutes;
                shiftUse.Day7Minutes = shiftUse.Day1Minutes;
                shiftUse.LastModified = DateTime.Now;
                shiftUse.ModifiedBy = CurrentUserName;

                _repository.Repository<ProductionShiftUse>().Update(shiftUse);
            }
            CommitUnitOfWork();
        }
    }
}
