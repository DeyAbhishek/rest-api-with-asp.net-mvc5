using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Services.Core;
using TPO.Common.DTOs;
using TPO.Data;
using AutoMapper;
using System.Data.Entity.Validation;

namespace TPO.Services.Downtime
{
    public class DownTimeReasonService : ServiceBase, ITpoService<DownTimeReasonDto>
    {
        public int Add(DownTimeReasonDto dto)
        {
            DownTimeReason entity = Mapper.Map<DownTimeReasonDto, DownTimeReason>(dto);

            try
            {
                _repository.Repository<DownTimeReason>().Insert(entity);
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
            return entity.ID;
        }

        public List<DownTimeReasonDto> GetAll()
        {
            var entities = _repository.Repository<DownTimeReason>().GetAll().ToList();
            return Mapper.Map<List<DownTimeReason>, List<DownTimeReasonDto>>(entities);
        }

        public List<DownTimeReasonDto> GetByLineAndType(int lineID, int typeID) 
        {
            var entities = _repository.Repository<DownTimeReason>().GetAllBy(dt => dt.LineID.HasValue && dt.LineID.Value == lineID && dt.DownTimeTypeID == typeID).ToList();
            return Mapper.Map<List<DownTimeReason>, List<DownTimeReasonDto>>(entities);
        }

        public DownTimeReasonDto Get(int id)
        {
            var entity = _repository.Repository<DownTimeReason>().GetById(id);
            return Mapper.Map<DownTimeReason, DownTimeReasonDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<DownTimeReason>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(DownTimeReasonDto dto)
        {
            try
            {
                var entity = _repository.Repository<DownTimeReason>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<DownTimeReason>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
    }
}
