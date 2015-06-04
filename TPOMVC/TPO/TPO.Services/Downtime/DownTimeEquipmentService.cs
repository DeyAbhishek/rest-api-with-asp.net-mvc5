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
    public class DownTimeEquipmentService : ServiceBase, ITpoService<DownTimeEquipmentDto>
    {
        public int Add(DownTimeEquipmentDto dto)
        {
            var entity = Mapper.Map<DownTimeEquipmentDto, DownTimeEquipment>(dto);

            try
            {
                _repository.Repository<DownTimeEquipment>().Insert(entity);
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

        public List<DownTimeEquipmentDto> GetAll()
        {
            var entities = _repository.Repository<DownTimeEquipment>().GetAll().ToList();
            return Mapper.Map<List<DownTimeEquipment>, List<DownTimeEquipmentDto>>(entities);
        }

        public List<DownTimeEquipmentDto> GetByGroup(int groupID) 
        {
            var entities = _repository.Repository<DownTimeEquipment>().GetAllBy(dt => dt.DownTimeEquipmentGroupID == groupID).ToList();
            return Mapper.Map<List<DownTimeEquipment>, List<DownTimeEquipmentDto>>(entities);
        }

        public DownTimeEquipmentDto Get(int id)
        {
            var entity = _repository.Repository<DownTimeEquipment>().GetById(id);
            return Mapper.Map<DownTimeEquipment, DownTimeEquipmentDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<DownTimeEquipment>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(DownTimeEquipmentDto dto)
        {
            try
            {
                var entity = _repository.Repository<DownTimeEquipment>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<DownTimeEquipment>().Update(entity);
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
