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
    public class RoleAssignmentService : ServiceBase, ITpoService<RoleAssignmentDto>
    {
        public int Add(RoleAssignmentDto dto)
        {
            int result = -1;

            var entity = new RoleAssignment();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<RoleAssignment>().Insert(entity);
                _repository.Save();

                if (entity != null)
                    result = entity.ID;
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

        public List<RoleAssignmentDto> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<RoleAssignmentDto> GetByUserId(int id)
        {
            var entities = _repository.Repository<RoleAssignment>().GetAllBy(u => u.UserID == id).ToList();
            return Mapper.Map<List<RoleAssignment>, List<RoleAssignmentDto>>(entities);
        }

        public RoleAssignmentDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RoleAssignment>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RoleAssignmentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
