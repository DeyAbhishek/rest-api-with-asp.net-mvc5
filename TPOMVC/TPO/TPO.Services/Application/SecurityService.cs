using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Web.Security;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using TPO.Services.Users;
using WebMatrix.WebData;
using Roles = TPO.Common.Constants.Roles;
using SecurityTask = TPO.Common.Enums.SecurityTask;

namespace TPO.Services.Application
{
    public class SecurityService: ServiceBase, ITpoService<UserDto>
    {
        private MembershipProvider _membershipProvider;
        private RoleProvider _roleProvider;

        public SecurityService()
        {
            _membershipProvider = Membership.Provider;
            _roleProvider = System.Web.Security.Roles.Provider;
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public int Add(UserDto dto)
        {
            int result = -1;

            var entity = new User();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<User>().Insert(entity);
                _repository.Save();

                if (entity != null)
                {
                    result = entity.ID;
                }
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

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public bool ChangePassword(UserDto dto)
        {
            if (dto.Password != null &&  dto.Password != string.Empty)
            {
                var token = WebSecurity.GeneratePasswordResetToken(dto.Username, 1);
                WebSecurity.ResetPassword(token, dto.Password);
                return true;
            }
            return false;
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public int Add(UserDto dto, List<int> relatedPlants, List<string> assignRoles)
        {
            if (WebSecurity.UserExists(dto.Username))
            {
                throw new Exception("Username exists");
            }

            WebSecurity.CreateUserAndAccount(dto.Username, dto.Password, new
            {
                PlantId = dto.PlantId,
                FullName = dto.FullName,
                DateEntered = DateTime.UtcNow,
                EnteredBy = CurrentUserName,
                LastModified = DateTime.UtcNow,
                ModifiedBy = CurrentUserName
            }, requireConfirmationToken: false);

            var entity = _repository.Repository<User>().GetAllBy(null, null, "webpages_Roles").Where(u => u.Username == dto.Username);
            dto.Id = entity.SingleOrDefault().ID;

            SaveRelatedInfo(dto, relatedPlants, assignRoles);

            return dto.Id;
        }

        public List<UserDto> GetAll()
        {
            var entities = _repository.Repository<User>().GetAll().OrderBy( u => u.FullName );
            if (entities != null)
            {
                return Mapper.Map<List<User>, List<UserDto>>(entities.ToList());
            }
            return new List<UserDto>();           
        }

        public UserDto Get(int id)
        {
            var userEntity = _repository.Repository<User>().GetById(id);
            var userDTO =  Mapper.Map<User, UserDto>(userEntity);

            userDTO.Roles = Mapper.Map<List<webpages_Roles>, List<RoleDto>>(userEntity.webpages_Roles.ToList());

            if (userEntity.webpages_Roles.Any())
            {
                userDTO.SecurityTasks = new List<SecurityTask>();
                foreach (var r in userEntity.webpages_Roles.ToList())
                {
                    foreach (var task in r.SecurityTasks)
                    {
                        var t = (SecurityTask)task.SecurityTaskId;
                        userDTO.SecurityTasks.Add(t);
                    }
                }
            }
            
            var userPlants = _repository.Repository<UserPlant>().GetAllBy(u => u.UserId == id);

            userDTO.UserPlants = Mapper.Map<List<UserPlant>, List<UserPlantDto>>(userPlants.ToList());
           

            return userDTO;
        }

        public UserDto GetByUserName(string userName)
        {
            var entity = _repository.Repository<User>().GetAllBy(null, null, "webpages_Roles").Where(u => u.Username == userName);

            if (entity.Any())
            {
                var user = entity.FirstOrDefault();
                var userDto = Mapper.Map<User, UserDto>(user);

                userDto.SecurityTasks = new List<SecurityTask>();
                if (user != null && user.webpages_Roles != null)
                {
                    foreach (var role in user.webpages_Roles)
                    {
                        var rolesQuery = _repository.Repository<webpages_Roles>().GetAllBy(null, null, "SecurityTasks").Where(u => u.RoleId == role.RoleId);
                        if (rolesQuery.Any())
                        {
                            foreach (var r in rolesQuery.ToList())
                            {
                                foreach (var task in r.SecurityTasks)
                                {
                                    var t = (SecurityTask)task.SecurityTaskId;
                                    userDto.SecurityTasks.Add(t);
                                }
                            }
                        }
                    }
                }
                return userDto;
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public void Delete(int id)
        {
            var entity = _repository.Repository<User>().GetAllBy(null, null, "webpages_Roles").Where(u => u.ID == id).SingleOrDefault();
            
            var roles = System.Web.Security.Roles.GetRolesForUser(entity.Username);
            if (roles.Any())
            {
                System.Web.Security.Roles.RemoveUserFromRoles(entity.Username, roles);
            }

            try
            {
                foreach (var p in entity.UserPlants)
                {
                    _repository.Repository<UserPlant>().Delete(p);
                }
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }

            Membership.DeleteUser(entity.Username);

        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public void Update(UserDto dto)
        {
            var entity = _repository.Repository<User>().GetById(dto.Id);
            try
            {
                entity.FullName = dto.FullName;
                entity.Username = dto.Username;
                entity.PlantID = dto.PlantId;
                entity.ModifiedBy = dto.ModifiedBy;
                entity.LastModified = dto.LastModified;
               
                _repository.Repository<User>().Update(entity);
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

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = Roles.SystemAdministrator)]
        public void Update(UserDto dto, List<int> relatedPlants, List<string> assignRoles)
        {
            Update(dto);
            //to fix all users without accounts
            if (!WebSecurity.UserExists(dto.Username))
            {
                if (dto.Password == null || dto.Password == string.Empty)
                {
                    dto.Password = ConfigurationManager.AppSettings[SettingKeys.DefaultPassword];
                }
                WebSecurity.CreateUserAndAccount(dto.Username, dto.Password, new
                {
                    PlantId = dto.PlantId,
                    FullName = dto.FullName,
                    DateEntered = DateTime.UtcNow,
                    EnteredBy = CurrentUserName,
                    LastModified = DateTime.UtcNow,
                    ModifiedBy = CurrentUserName
                }, requireConfirmationToken: false);
            }
            else
            {
                ChangePassword(dto);
            }
            SaveRelatedInfo(dto, relatedPlants, assignRoles);
        }

        public UserDto Login(string userName, string password)
        {
            // WebSecurity does not accept null or empty
            if (string.IsNullOrEmpty(userName)) return null;
            if (string.IsNullOrEmpty(password)) return null;

            if (WebSecurity.Login(userName, password))
            {
                var entity = _repository.Repository<User>().GetAllBy(null, null, "webpages_Roles").Where(u => u.Username == userName);

                if (entity.Any())
                {
                    var user = entity.FirstOrDefault();
                    var userDto = Mapper.Map<User, UserDto>(user);

                    userDto.SecurityTasks = new List<SecurityTask>();
                    if (user != null && user.webpages_Roles != null)
                    {
                        foreach (var role in user.webpages_Roles)
                        {
                            var rolesQuery = _repository.Repository<webpages_Roles>().GetAllBy(null, null, "SecurityTasks").Where(u => u.RoleId == role.RoleId);
                            if (rolesQuery.Any())
                            {
                                foreach (var r in rolesQuery.ToList())
                                {
                                    foreach (var task in r.SecurityTasks)
                                    {
                                        var t = (SecurityTask)task.SecurityTaskId;
                                        userDto.SecurityTasks.Add(t);
                                    }
                                }
                            }
                        }
                    }
                    return userDto;
                }
            }
            return null;
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public List<UserDto> GetQCTechUsers()
        {
            return GetByRole(Roles.QcLabTechnician);
        }

        public List<UserDto> GetByRole(string role)
        {
            Expression<Func<webpages_Roles, bool>> filter = ra => ra.RoleName == role;
            var entities = _repository.Repository<webpages_Roles>().GetAllBy(filter).SelectMany(ra => ra.Users).ToList();
            return Mapper.Map<List<User>, List<UserDto>>(entities);
        }

        public bool UserCanDoTask(UserDto user, TPO.Common.Enums.SecurityTask task)
        {
            var canDoTask = false;
            try
            {
                canDoTask = user.SecurityTasks.Contains(task);
            }
            catch (Exception ex)
            {
                // LOG
                canDoTask = false;
            }
            return canDoTask;
        }

        public bool UserCanOneOrMoreDoTasks(UserDto user, List<TPO.Common.Enums.SecurityTask> tasks)
        {
            var canOneOrMoreDoTasks = false;
            try
            {
                canOneOrMoreDoTasks = tasks.Any(t => user.SecurityTasks.Contains(t));
            }
            catch (Exception ex)
            {
                // LOG
                canOneOrMoreDoTasks = false;
            }

            return canOneOrMoreDoTasks;
        }

        public void Initialize()
        {
            //Map to User table
            WebSecurity.InitializeDatabaseConnection("SecurityService", "User", "Id", "UserName", autoCreateTables: true);

            var usersToCreate = new Dictionary<string, string>
                {
                    {DefaultUsers.LeadOperator, Roles.LeadOperator},
                    {DefaultUsers.Manager, Roles.Manager},
                    {DefaultUsers.Operator, Roles.Operator},
                    {DefaultUsers.QcLabTechnician, Roles.QcLabTechnician},
                    {DefaultUsers.ShiftSupervisor, Roles.ShiftSupervisor},
                    {DefaultUsers.SystemAdministrator, Roles.SystemAdministrator}
                };

            CreateRoles(usersToCreate);

            if (ConfigurationManager.AppSettings[SettingKeys.LoadDefaultUsers] != null &&
                ConfigurationManager.AppSettings[SettingKeys.LoadDefaultUsers].ToUpper() == "TRUE")
            {
                CreateDefaultUsers(usersToCreate);
            }
        }

        #region PRIVATE METHODS

        private void CreateRoles(Dictionary<string, string> usersToCreate)
        {
            var roles = _roleProvider.GetAllRoles();

            foreach (var u in usersToCreate)
            {
                if (!roles.Contains(u.Value))
                {
                    _roleProvider.CreateRole(u.Value);
                }
            }
        }

        private void CreateDefaultUsers(Dictionary<string,string> usersToCreate)
        {
            var defaultPassword = ConfigurationManager.AppSettings[SettingKeys.DefaultPassword];
            var defaultPlantId = Convert.ToInt32(ConfigurationManager.AppSettings[SettingKeys.DefaultPlantId]);
            foreach (var u in usersToCreate)
            {
                if (!WebSecurity.UserExists(u.Key))
                {
                    WebSecurity.CreateUserAndAccount(u.Key, defaultPassword, new
                    {
                        PlantId = defaultPlantId,
                        FullName = u.Key,
                        DateEntered = DateTime.UtcNow,
                        EnteredBy = "System",
                        LastModified = DateTime.UtcNow,
                        ModifiedBy = "System"
                    }, requireConfirmationToken: false);
                }

                if (!System.Web.Security.Roles.IsUserInRole(u.Key,u.Value))
                {
                    System.Web.Security.Roles.AddUserToRole(u.Key, u.Value);
                }
            }
        }

        private void SaveRelatedInfo(UserDto dto, List<int> relatedPlants, List<string> assignRoles)
        {
            try
            {
                SaveRelatedPlant(dto, relatedPlants);
                SaveRelatedRoles(dto, assignRoles);
            }
            catch (Exception exc)
            {
                throw new ApplicationException("An exception occurred during Plants/Roles sync.", exc);
            }
        }

        private void SaveRelatedPlant(UserDto dto, List<int> relatedPlants)
        {
 
            var plants = _repository.Repository<UserPlant>().GetAllBy(f => f.User.ID == dto.Id).ToList();

            #region Save New Plants

            // find all the plants to add
            foreach (var plantId in relatedPlants)
            {
                if (!plants.FindAll(p => p.PlantId == plantId).Any())
                {
                    // add it
                    var newPlant = new UserPlant()
                    {
                        PlantId = plantId,
                        UserId = dto.Id,
                        EnteredBy = dto.ModifiedBy,
                        DateEntered = dto.LastModified,
                        ModifiedBy = dto.ModifiedBy,
                        LastModified = dto.LastModified
                    };

                    try
                    {
                        _repository.Repository<UserPlant>().Insert(newPlant);
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

            #endregion

            #region Remove Plants

            // find all the plants to remove
            foreach (var existPlant in plants)
            {
                if (!relatedPlants.FindAll(p => p == existPlant.PlantId).Any())
                {
                    try
                    {
                        _repository.Repository<UserPlant>().Delete(existPlant);
                        _repository.Save();
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                        throw;
                    }
                }
            }

            #endregion
        }
        
        private void SaveRelatedRoles(UserDto dto, List<string> assignRoles)
        {
            var roles = System.Web.Security.Roles.GetRolesForUser(dto.Username);
            if (roles.Any())
            {
                System.Web.Security.Roles.RemoveUserFromRoles(dto.Username, roles);
            }
            foreach (var r in assignRoles)
            {
                System.Web.Security.Roles.AddUserToRole(dto.Username, r);
            }
        }

        #endregion
    }
}
