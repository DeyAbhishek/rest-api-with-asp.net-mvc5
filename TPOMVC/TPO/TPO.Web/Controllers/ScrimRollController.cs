using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Scrim;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Downtime;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ScrimRollController : BaseController
    {
        //
        // GET: /ScrimRoll/
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /ScrimRoll/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            return View();
            //   ScrimRollService  svc = new ScrimRollService();

            ////   ViewBag.ScrimRoll = svc.GetAll();
            //   List<ScrimRollModel> model1 = new List<ScrimRollModel>();
            //   using (ScrimRollService svc1 = new ScrimRollService())
            //   {
            //       var dtos = svc1.GetAll();
            //       model1.AddRange(Mapper.Map<List<ScrimRollDto>, List<ScrimRollModel>>(dtos));
            //   }
            //   ViewBag.ScrimRoll = model1;

            //   if (id == null)
            //   {
            //  //     List<ScrimRollDto> dto = svc.GetAll();
            //         //  ((List<ScrimRollModel>)ViewBag.ScrimRoll).Insert(0, new ScrimRollModel());

            //       return View(new List<ScrimRollModel>());
            //   }
            //   else
            //   {
            //       GetRollTypesList();
            //       //GetUoMList();

            //       ////ScrimRollModel model = bl.GetScrimRollModelByTypeID((int)id);

            //       var model = new List<ScrimRollModel>();


            //       List<ScrimRollDto> dto = svc.GetByType((int) id);
            //      model.AddRange(Mapper.Map<List<ScrimRollDto>, List<ScrimRollModel>>(dto));
            //       if (dto == null)
            //       {
            //           TempData["ActionMessage"] = MessageService.GetStringValue(MessageKeys.ResponseMessageFailNoId);
            //           TempData["ActionMessageType"] = MessageService.GetStringValue(MessageKeys.ResponseTypeError);
            //           return RedirectToAction("Details");
            //      }

            //       return View(model);
            //   }


        }


        [HttpPost]
        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", new { id = id });
        }


        //
        // GET: /ScrimRoll/Create
        public ActionResult Create()
        {
            GetRollTypesList();
            //GetUoMList();
            ViewBag.LengthUoM = GetLengthUoMOptions();
            ViewBag.WeightUoM = GetWeightUoMOptions();
            ScrimRollModel model = new ScrimRollModel();
            return View(model);
        }


        //
        // POST: /ScrimRoll/Create
        [HttpPost]
        public ActionResult Create(ScrimRollModel model)
        {

            if (ModelState.IsValid)
            {

                var dto = Mapper.Map<ScrimRollModel, ScrimRollDto>(model);
                dto.EnteredBy = CurrentUser;
                dto.ModifiedBy = CurrentUser;
                dto.DateEntered = DateTime.Now;
                dto.LastModified = DateTime.Now;
                //dto.DateReceived = date ?? DateTime.Now;
                using (var service = new ScrimRollService())
                {
                    service.Add(dto);
                }

                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);

                return RedirectToAction("Details", new { id = model.Id });

            }
            else
            {

                ////  ModelState.AddModelError(string.Empty, "Please enter required fields.");

                GetRollTypesList();
                //GetUoMList();

                return View(model);

            }
        }

        //
        // GET: /ScrimRoll/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                ////removed to keep from initial load of scrim page from displaying error.
                ////TempData["ActionMessage"] = Responses.ResponseMessages["FailNoId"];
                ////TempData["ActionMessageType"] = Responses.ResponseTypes["error"]; 
                return RedirectToAction("Details");
            }
            GetRollTypesList();
            //GetUoMList();
            //ScrimRoll bl = new ScrimRoll();
            //ViewBag.ScrimRoll = bl.GetScrimRollModels();
            //ScrimRollModel model = bl.GetScrimRollModelByID(id);
            ScrimRollModel model;
            using (var service = new ScrimRollService())
            {
                model = Mapper.Map<ScrimRollDto, ScrimRollModel>(service.Get(id));
            }

            if (model == null) { model = new ScrimRollModel(); }

            return View(model);
        }

        //
        // POST: /ScrimRoll/Edit/5
        [HttpPost]
        public ActionResult Edit(DateTime? date, DateTime? wovendate, int id, ScrimRollModel model)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                GetRollTypesList();
                //GetUoMList();

                return View(model);
            }
            var dto = Mapper.Map<ScrimRollModel, ScrimRollDto>(model);
            dto.EnteredBy = CurrentUser;
            dto.ModifiedBy = CurrentUser;
            dto.DateEntered = DateTime.Now;
            dto.LastModified = DateTime.Now;
            dto.DateReceived = date ?? DateTime.Now;
            dto.WovenDate = wovendate ?? DateTime.Now;
            //ScrimRoll bl = new ScrimRoll();
            //bl.UpdateScrimRoll(model);
            using (var service = new ScrimRollService())
            {
                service.Update(dto);
            }
            // TempData["ActionMessage"] = Responses.ResponseMessages["SuccessfulSave"];
            //TempData["ActionMessageType"] = Responses.ResponseTypes["success"];
            return RedirectToAction("Details", new { id = model.Id });
        }

        private void GetRollTypesList()
        {
            List<ScrimTypeDto> dtos = new List<ScrimTypeDto>();
            using (ScrimTypeService service = new ScrimTypeService())
            {
                dtos.AddRange(service.GetAll());
            }
            ViewBag.ScrimType = new SelectList(dtos.Select(s => new
            {

                ID = s.ID,
                Description = string.Format("{0} | {1}", s.Code, s.Description)
            }).ToList(), "ID", "Description");
            // return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public JsonResult GetScrimTypeResult()
        {
            List<ScrimTypeModel> model = new List<ScrimTypeModel>();
            using (ScrimTypeService svc = new ScrimTypeService())
            {
                var dtos = svc.GetAll();
                model.AddRange(Mapper.Map<List<ScrimTypeDto>, List<ScrimTypeModel>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult ScrimRollGridByType(int typeID, int? rows, int? page)
        {
            List<ScrimRollModel> model = new List<ScrimRollModel>();
            using (ScrimRollService svc = new ScrimRollService())
            {
                var dtos = svc.GetByType(typeID);
                model.AddRange(Mapper.Map<List<ScrimRollDto>, List<ScrimRollModel>>(dtos));
            }

            int total;
            total = model.Count();
            var currentPageDtos = model.OrderBy(r => r.DateEntered).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToList();

            return BuildJsonResult(currentPageDtos, total);


        }

        [HttpPost]
        public JsonResult AjaxUpdateScrimRoll(ScrimRollModel model)
        {
            var dto = Mapper.Map<ScrimRollModel, ScrimRollDto>(model);
            dto.ModifiedBy = CurrentUser;
            dto.LastModified = DateTime.Now;

            using (ScrimRollService svc = new ScrimRollService())
            {
                if (dto.ID > 0)
                {
                    svc.Update(dto);
                }
                else
                {
                    dto.EnteredBy = CurrentUser;
                    dto.DateEntered = DateTime.Now;
                    dto.PlantID = CurrentPlantId;
                    dto.ID = svc.Add(dto);
                }
            }
            model = Mapper.Map<ScrimRollDto, ScrimRollModel>(dto);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteScrimRoll(int id = 0)
        {
            using (ScrimRollService svc = new ScrimRollService())
            {
                svc.Delete(id);
            }
            return RedirectToAction("Details");
        }



        [HttpPost]
        public JsonResult ScrimRollAjaxCreate(string id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                ScrimRollService srsrv = new ScrimRollService();

                ScrimRollModel model = JsonConvert.DeserializeObject<ScrimRollModel>(id);
                if (model != null)
                {
                    var x = srsrv.GetAll().Where(q => q.Code == model.Code).ToList();

                    if (!x.Any())
                    {
                        ScrimRollDto dto = new ScrimRollDto();

                        using (ScrimRollService service = new ScrimRollService())
                        {
                            Mapper.Map(model, dto);

                            dto.WovenDate = null;

                            dto.PlantID = CurrentPlantId;
                            dto.LastModified = DateTime.Now;
                            dto.ModifiedBy = CurrentUser;
                            

                            if (model.Id > 0)
                            {
                                service.Update(dto);
                            }
                            else
                            {
                                dto.DateEntered = dto.LastModified;
                                dto.EnteredBy = CurrentUser;
                                service.Add(dto);
                            }
                        }
                    }
                    else
                    {
                        ScrimRollDto dto = new ScrimRollDto();

                        using (ScrimRollService service = new ScrimRollService())
                        {
                            Mapper.Map(model, dto);

                            dto.WovenDate = null;

                            dto.PlantID = CurrentPlantId;
                            dto.LastModified = DateTime.Now;
                            dto.ModifiedBy = CurrentUser;
                            dto.DateEntered = DateTime.Now;
                            dto.EnteredBy = CurrentUser;

                            service.Update(dto);
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void MarkRollAsUsed(int scrimRollID = 0, int lineID = 0) 
        {
            using (ScrimRollService svc = new ScrimRollService()) 
            {
                svc.MarkRollAsUsed(scrimRollID, lineID, CurrentUser);
            }
        }


    }

}

