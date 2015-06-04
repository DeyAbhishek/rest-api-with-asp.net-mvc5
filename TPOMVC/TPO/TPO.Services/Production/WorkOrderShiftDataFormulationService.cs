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
using TPO.Services.RawMaterials;

namespace TPO.Services.Production
{
    public class WorkOrderShiftDataFormulationService : ServiceBase, ITpoService<WorkOrderShiftDataFormulationDto>
    {
        public int Add(WorkOrderShiftDataFormulationDto workOrderShiftDataFormulationDto)
        {
            var newWorkOrderShiftDataFormulationDto = new WorkOrderShiftDataFormulation();
            try
            {
                newWorkOrderShiftDataFormulationDto.ModifiedDate = DateTime.Now;
                newWorkOrderShiftDataFormulationDto.ModifiedBy = CurrentUserName;
                newWorkOrderShiftDataFormulationDto.EnteredBy = CurrentUserName;
                newWorkOrderShiftDataFormulationDto.EnteredDate = DateTime.Now;
                Mapper.Map(workOrderShiftDataFormulationDto, newWorkOrderShiftDataFormulationDto);

                _repository.Repository<WorkOrderShiftDataFormulation>().Insert(newWorkOrderShiftDataFormulationDto);
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
            return newWorkOrderShiftDataFormulationDto.ID;
        }

        public List<WorkOrderShiftDataFormulationDto> GetAll()
        {

            var entities = _repository.Repository<WorkOrderShiftDataFormulation>().GetAll().ToList();

            return Mapper.Map<List<WorkOrderShiftDataFormulation>, List<WorkOrderShiftDataFormulationDto>>(entities);

        }

        public WorkOrderShiftDataFormulationDto Get(int id)
        {
            var entity = _repository.Repository<WorkOrderShiftDataFormulation>().GetById(id);
            return Mapper.Map<WorkOrderShiftDataFormulation, WorkOrderShiftDataFormulationDto>(entity);

        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<WorkOrderShiftDataFormulation>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(WorkOrderShiftDataFormulationDto dto)
        {
            try
            {
                dto.ModifiedDate = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                var entity = _repository.Repository<WorkOrderShiftDataFormulationDto>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<WorkOrderShiftDataFormulationDto>().Update(entity);
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
