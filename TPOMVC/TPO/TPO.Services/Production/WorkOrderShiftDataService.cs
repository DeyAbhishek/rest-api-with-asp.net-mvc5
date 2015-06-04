using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class WorkOrderShiftDataService : ServiceBase, ITpoService<WorkOrderShiftDataDto>
    {
        public int Add(WorkOrderShiftDataDto workOrderShiftDataDto)
        {
            var newWorkOrderShiftDataDto = new WorkOrderShiftData();
            try
            {
                newWorkOrderShiftDataDto.LastModified = DateTime.Now;
                newWorkOrderShiftDataDto.ModifiedBy = CurrentUserName;
                newWorkOrderShiftDataDto.EnteredBy = CurrentUserName;
                newWorkOrderShiftDataDto.DateEntered = DateTime.Now;
                Mapper.Map(workOrderShiftDataDto, newWorkOrderShiftDataDto);

                _repository.Repository<WorkOrderShiftData>().Insert(newWorkOrderShiftDataDto);
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
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return newWorkOrderShiftDataDto.ID;
        }

        public List<WorkOrderShiftDataDto> GetAll()
        {

            var entities = _repository.Repository<WorkOrderShiftData>().GetAll().ToList();

            return Mapper.Map<List<WorkOrderShiftData>, List<WorkOrderShiftDataDto>>(entities);

        }

        public List<WorkOrderShiftDataDto> GetByShift(int lineID, int shiftID, DateTime productionDate) 
        {
            var entities = _repository.Repository<WorkOrderShiftData>().GetAllBy(wo => wo.LineID == lineID && wo.ShiftID == shiftID && wo.ProductionDate == productionDate).ToList();

            return Mapper.Map<List<WorkOrderShiftData>, List<WorkOrderShiftDataDto>>(entities);
        }

        public WorkOrderShiftDataDto Get(int id)
        {
            var entity = _repository.Repository<WorkOrderShiftData>().GetById(id);
            return Mapper.Map<WorkOrderShiftData, WorkOrderShiftDataDto>(entity);

        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<WorkOrderShiftData>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(WorkOrderShiftDataDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                var entity = _repository.Repository<WorkOrderShiftData>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<WorkOrderShiftData>().Update(entity);
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
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        
    }
}
