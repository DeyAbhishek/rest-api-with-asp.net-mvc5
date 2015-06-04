using System.Collections.Generic;
using System.Linq;
using TPO.DL.Models;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly TPOMVCApplicationEntities _dbContext;

         public UserRepository()
        {
            _dbContext = new TPOMVCApplicationEntities();
        }

         public UserRepository(TPOMVCApplicationEntities dbContext)
        {
            _dbContext = dbContext;
        }

         public IEnumerable<UserDTO> GetAllUsers()
         {
             var users = (from r in _dbContext.Roles
                          join ur in _dbContext.RoleAssignments on r.ID equals ur.RoleID
                          join u in _dbContext.Users on ur.UserID equals u.ID
                          select u).ToList();
             return users.Select(MapUserToUserDto).ToList();
         }

         public IEnumerable<UserDTO> GetUsersByRole(string roleName)
         {
             var users = (from r in _dbContext.Roles
                          join ur in _dbContext.RoleAssignments on r.ID equals ur.RoleID
                          join u in _dbContext.Users on ur.UserID equals u.ID
                          where r.RoleName == roleName
                          select u).ToList();
             return users.Select(MapUserToUserDto).ToList();
         }

        public UserDTO GetUserByUserName(string userName)
        {
            UserDTO result = null;

            using (DL.Repositories.UserRepository repository = new DL.Repositories.UserRepository())
            { 
                result = MapUserToUserDto(repository.GetUserByUserName(userName));
            }

            return result;
        }


        private static UserDTO MapUserToUserDto(DL.Models.User user )
        {
            // TODO: Use mapper
            return new UserDTO
            {
                ID = user.ID,
                LastModified = user.LastModified,
                ModifiedBy = user.ModifiedBy,
                EnteredBy = user.EnteredBy,
                DateEntered = user.DateEntered,
                FullName = user.FullName,
                Username = user.Username,
                PlantID = user.PlantID
            };
            
        }

    }
}
