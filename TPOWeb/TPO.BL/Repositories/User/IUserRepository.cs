using System.Collections.Generic;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.User
{
    public interface IUserRepository
    {
        IEnumerable<UserDTO> GetAllUsers();
        IEnumerable<UserDTO> GetUsersByRole(string roleName);
        UserDTO GetUserByUserName(string userName);
    }
}
