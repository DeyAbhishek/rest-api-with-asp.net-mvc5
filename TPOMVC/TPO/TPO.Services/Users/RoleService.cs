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
    public class RoleService : ServiceBase, ITpoService<RoleDto>
    {
        public int Add(RoleDto dto)
        {
            throw new NotImplementedException();
        }

        public List<RoleDto> GetAll()
        {
            var entities = _repository.Repository<webpages_Roles>().GetAllBy().ToList();
            return Mapper.Map<List<webpages_Roles>, List<RoleDto>>(entities);
        }

        public RoleDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RoleDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
