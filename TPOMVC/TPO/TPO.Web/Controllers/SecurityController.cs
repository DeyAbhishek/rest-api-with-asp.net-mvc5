using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Data;
using TPO.Services.Application;
using TPO.Web.Filters;
using TPO.Web.Models;
using WebMatrix.WebData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPO.Web.Controllers
{
    [InitializeTPOSimpleMembership]
    public class SecurityController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            var model = new Login();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var service = new SecurityService())
                {
                    var loginResult = service.Login(model.UserName, model.Password);
                    if (loginResult != null)
                    {
                        //var loggedInUser = service.GetByUserName(model.UserName);
                        //var loggedInUser = service.Get(WebSecurity.CurrentUserId);
                        //System.Web.HttpContext.Current.Session["CurrentUserContext"] = loggedInUser;
                        System.Web.HttpContext.Current.Session["CurrentUserContext"] = loginResult;
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index","Home");
                    }
                }
            }

            ModelState.AddModelError("", "The username and/or password are incorrect.");

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            using (var service = new SecurityService())
            {
                service.Logout();
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult TimeOut()
        {
            Session.Abandon();
            using (var service = new SecurityService())
            {
                service.Logout();
            }
            return View("TimeOut");
        }

        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View("Unauthorized");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SecurityEditModel model;
            UserDto user;
            using (var service = new SecurityService())
            {
                user = service.Get(id);
                model = Mapper.Map<UserDto, SecurityEditModel>(user);
            }

            //TODO: refactor
            using (Services.PlantService service = new Services.PlantService())
            {
                var dtos = service.GetAll().OrderBy(p => p.Name).ToList();
                model.PlantList = new List<SelectListItem>(); ;
                model.PlantList.AddRange(dtos.Select(d => new SelectListItem { Text = d.Name, Value = d.ID.ToString() }));
            }

            using (Services.Users.RoleService service = new Services.Users.RoleService())
            {
                var dtos = service.GetAll().OrderBy(p => p.RoleName).ToList();
                model.RoleList = new List<SelectListItem>();
                model.RoleList.AddRange(dtos.Select(d => new SelectListItem { Text = d.RoleName, Value = d.RoleId.ToString() }));
            }

            model.RoleAssignmentList = new List<SelectListItem>();
            model.RoleAssignmentList.AddRange(user.Roles.Select(d => new SelectListItem { Text = d.RoleName, Selected = true, Value = d.RoleName}));

            model.RelatedPlantList = new List<SelectListItem>();

            model.RelatedPlantList.AddRange(user.UserPlants.Select(d => new SelectListItem { Text = d.Plant.Name, Selected = true, Value = d.Plant.ID.ToString() }));

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(SecurityEditModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: move to service
                model.ModifiedBy = CurrentUser;
                model.LastModified = DateTime.Now;

                var dto = new UserDto();
                Mapper.Map(model, dto);

                try
                {
                    using (var service = new SecurityService())
                    {
                        List<int> selectedPlants = model.SelectedPlants.Plants.Select(s => int.Parse(s)).ToList();
                        service.Update(dto, selectedPlants, new List<string>(model.SelectedRoles.Roles));
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception exc)
                {
                    SetResponseMesssage(ActionTypeMessage.FailedSave);
                }
            }
            return View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult FetchUsers()
        {
            var results = new List<SecurityAddModel>();
            using (var service = new SecurityService())
            {
                var dtos = service.GetAll();
                results.AddRange(Mapper.Map<List<UserDto>, List<SecurityAddModel>>(dtos));
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Remove(int id)
        {
            string status;
            string msg;

            try
            {
                using (var service = new SecurityService())
                {
                    service.Delete(id);
                }

                status = "ok";
                msg = string.Empty;
            }
            catch (Exception exc)
            {
                status = "error";
                msg = exc.Message;
            }
            return Json(new { Status = status, Message = msg }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Add()
        {
            var model = new SecurityAddModel
            {
                EnteredBy = CurrentUser,
                DateEntered = DateTime.Now,
                ModifiedBy = CurrentUser,
                LastModified = DateTime.Now
            };

            //TODO: refactor
            using (Services.PlantService service = new Services.PlantService())
            {
                var dtos = service.GetAll().OrderBy(p => p.Name).ToList();
                model.PlantList = new List<SelectListItem>(); ;
                model.PlantList.AddRange(dtos.Select(d => new SelectListItem { Text = d.Name, Value = d.ID.ToString() }));
            }

            using (Services.Users.RoleService service = new Services.Users.RoleService())
            {
                var dtos = service.GetAll().OrderBy(p => p.RoleName).ToList();
                model.RoleList = new List<SelectListItem>();
                model.RoleList.AddRange(dtos.Select(d => new SelectListItem { Text = d.RoleName, Value = d.RoleId.ToString() }));
            }

            model.RoleAssignmentList = new List<SelectListItem>();
            model.RelatedPlantList = new List<SelectListItem>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SecurityAddModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new UserDto();
                Mapper.Map(model, dto);

                try
                {
                    using (var service = new SecurityService())
                    {
                        List<int> selectedPlants = model.SelectedPlants.Plants.Select(s => int.Parse(s)).ToList();
                        service.Add(dto, selectedPlants, new List<string>(model.SelectedRoles.Roles));
                    }

                    SetResponseMesssage(ActionTypeMessage.SuccessfulSave);

                    return RedirectToAction("Index");
                }
                catch (Exception exc)
                {
                   //TODO: refactor
                    using (Services.PlantService service = new Services.PlantService())
                    {
                        var dtos = service.GetAll().OrderBy(p => p.Name).ToList();
                        model.PlantList = new List<SelectListItem>(); ;
                        model.PlantList.AddRange(dtos.Select(d => new SelectListItem { Text = d.Name, Value = d.ID.ToString() }));
                    }

                    using (Services.Users.RoleService service = new Services.Users.RoleService())
                    {
                        var dtos = service.GetAll().OrderBy(p => p.RoleName).ToList();
                        model.RoleList = new List<SelectListItem>();
                        model.RoleList.AddRange(dtos.Select(d => new SelectListItem { Text = d.RoleName, Value = d.RoleId.ToString() }));
                    }
                    model.RoleAssignmentList = new List<SelectListItem>();
                    model.RelatedPlantList = new List<SelectListItem>();

                    SetResponseMesssage(ActionTypeMessage.FailedSave);
                }
            }
            return View(model);
        }
    }
}
