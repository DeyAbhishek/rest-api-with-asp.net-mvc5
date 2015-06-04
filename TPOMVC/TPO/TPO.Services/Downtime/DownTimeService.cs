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
    public class DownTimeService : ServiceBase, ITpoService<DownTimeDto>
    {
        public int Add(DownTimeDto dto)
        {
            dto.LastModified = DateTime.Now;
            dto.WorkOrderID = 2;
            var entity = Mapper.Map<DownTimeDto, DownTime>(dto);
            try
            {
                _repository.Repository<DownTime>().Insert(entity);
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
            return entity.ID;
        }

        public List<DownTimeDto> GetAll()
        {
            var entities = _repository.Repository<DownTime>().GetAll().ToList();
            return Mapper.Map<List<DownTime>, List<DownTimeDto>>(entities);
        }

        public List<DownTimeDto> GetByShift(int lineID, int shiftID, DateTime productionDate) 
        {
            var entities = _repository.Repository<DownTime>().GetAllBy(dt => dt.ShiftID == shiftID && dt.LineID == lineID && dt.ProductionDate == productionDate).ToList();
            return Mapper.Map<List<DownTime>, List<DownTimeDto>>(entities);
        }

        public DownTimeDto Get(int id)
        {
            var entity = _repository.Repository<DownTime>().GetById(id);
            return Mapper.Map<DownTime, DownTimeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<DownTime>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(DownTimeDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<DownTime>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<DownTime>().Update(entity);
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
