using System.Data.Entity.Validation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Users
{
    public class UserPlantService : ServiceBase, ITpoService<UserPlantDto>
    {
        public int Add(UserPlantDto dto)
        {
            int result = -1;

            var entity = new UserPlant();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<UserPlant>().Insert(entity);
                _repository.Save();

                if (entity != null)
                    result = entity.PlantId;
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

            return result;
        }

        public List<UserPlantDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserPlantDto> GetByUserId(int id)
        {
            var entities = _repository.Repository<UserPlant>().GetAllBy(u => u.UserId == id).ToList();
            return Mapper.Map<List<UserPlant>, List<UserPlantDto>>(entities);
        }
        public List<UserPlantDto> GetByPlantId(int id)
        {
            var entities = _repository.Repository<UserPlant>().GetAllBy(u => u.PlantId == id).ToList();
            return Mapper.Map<List<UserPlant>, List<UserPlantDto>>(entities);
        }

        public UserPlantDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserPlantDto entity)
        {
            try
            {
                UserPlant deleteEntity =
                    _repository.Repository<UserPlant>()
                        .GetAllBy(u => u.PlantId == entity.PlantId && u.UserId == entity.UserId)
                        .First();
                _repository.Repository<UserPlant>().Delete(deleteEntity);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [Obsolete]
        public void Delete(int id)
        {
            throw new NotImplementedException("These cannot be deleted with a single Id value. Try using 'Delete(UserPlantDTo entity)' instead");
        }

        public void Update(UserPlantDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
