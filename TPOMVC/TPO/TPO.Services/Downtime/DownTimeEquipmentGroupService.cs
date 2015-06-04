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
    public class DownTimeEquipmentGroupService : ServiceBase, ITpoService<DownTimeEquipmentGroupDto>
    {
        public int Add(DownTimeEquipmentGroupDto dto)
        {
            var entity = Mapper.Map<DownTimeEquipmentGroupDto, DownTimeEquipmentGroup>(dto);

            try
            {
                _repository.Repository<DownTimeEquipmentGroup>().Insert(entity);
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

        public List<DownTimeEquipmentGroupDto> GetAll()
        {
            var entities = _repository.Repository<DownTimeEquipmentGroup>().GetAll().ToList();
            return Mapper.Map<List<DownTimeEquipmentGroup>, List<DownTimeEquipmentGroupDto>>(entities);
        }

        public List<DownTimeEquipmentGroupDto> GetByLineAndType(int lineID, int typeID) 
        {
            var entities = _repository.Repository<DownTimeEquipmentGroup>().GetAllBy(dt => dt.LineID == lineID && dt.DownTimeTypeID == typeID).ToList();
            return Mapper.Map<List<DownTimeEquipmentGroup>, List<DownTimeEquipmentGroupDto>>(entities);
        }

        public DownTimeEquipmentGroupDto Get(int id)
        {
            var entity = _repository.Repository<DownTimeEquipmentGroup>().GetById(id);
            return Mapper.Map<DownTimeEquipmentGroup, DownTimeEquipmentGroupDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<DownTimeEquipmentGroup>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(DownTimeEquipmentGroupDto dto)
        {
            try
            {
                var entity = _repository.Repository<DownTimeEquipmentGroup>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<DownTimeEquipmentGroup>().Update(entity);
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
