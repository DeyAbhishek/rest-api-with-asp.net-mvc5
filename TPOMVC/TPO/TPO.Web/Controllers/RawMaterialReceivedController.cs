using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Core;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    public class RawMaterialReceivedController : BaseController
    {
        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly })]
        private IEnumerable<SelectListItem> GetProductionLines()
        {
            var data = new List<SelectListItem>();

            using (var service = new RawMaterialService())
            {
                var dtos = service.GetAllByPlantId(CurrentPlantId).ToList();

                data.AddRange(dtos.Select(d => new SelectListItem {Text = d.Description, Value = d.Id.ToString()}));
            }

            return data;
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly })]
        [HttpGet]
        public ActionResult Index(int id=0)
        {
            using (var service = new RawMaterialReceivedService())
            {
                List<RawMaterialReceived> model =
                    Mapper.Map<List<RawMaterialReceivedDto>, List<RawMaterialReceived>>(service.GetAllByPlantIdMaterialId(CurrentPlantId, id));

                ViewBag.RawMaterial = new SelectList(GetProductionLines(), "Value", "Text");

                return View(model);
            }
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                using (var service = new RawMaterialReceivedService())
                {
                   RawMaterialReceived model =
                   Mapper.Map<RawMaterialReceivedDto, RawMaterialReceived>(service.Get(id.Value));

                   model.ProductionLines = GetProductionLines();
                   return View(model);
                } 
            }

            SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoId);
            return RedirectToAction("Index");
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpPost]
        public ActionResult AjaxTypeUpdate(string row, string rawMaterialId)
        {
            ResponseMessage responseMessage;

            RawMaterialReceived rawMaterialReceived = JsonConvert.DeserializeObject<RawMaterialReceived>(row);

            try
            {
                if (rawMaterialReceived != null)
                {
                    rawMaterialReceived.LastModified = DateTime.Now;
                    RawMaterialReceivedDto dto = new RawMaterialReceivedDto();
                    using (RawMaterialReceivedService service = new RawMaterialReceivedService())
                    {
                        Mapper.Map(rawMaterialReceived, dto);
                        if (rawMaterialReceived.Id > 0)
                        {
                            service.Update(dto);
                        }
                        else
                        {
                            dto.RawMaterialId = Convert.ToInt32(rawMaterialId);
                            dto.PlantId = CurrentPlantId;
                            service.Add(dto);
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

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpPost]
        public JsonResult AjaxTypeDelete(string row)
        {
            ResponseMessage responseMessage;

            RawMaterialReceived rawMaterialReceived = JsonConvert.DeserializeObject<RawMaterialReceived>(row);

            try
            {
                if (rawMaterialReceived != null)
                {
                    RawMaterialReceivedDto dto = new RawMaterialReceivedDto();
                    using (RawMaterialReceivedService service = new RawMaterialReceivedService())
                    {
                        Mapper.Map(rawMaterialReceived, dto);
                        if (rawMaterialReceived.Id > 0)
                            service.DeleteComplete(dto.Id);
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

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpPost]
        public ActionResult Edit(int id, string action, RawMaterialReceived rawMaterialReceived)
        {
            if (action == "Edit")
            {
                if (ModelState.IsValid)
                {
                    var dto = Mapper.Map<RawMaterialReceived, RawMaterialReceivedDto>(rawMaterialReceived);
                    
                    //TODO: move to service
                    dto.ModifiedBy = CurrentUser;

                    using (var service = new RawMaterialReceivedService())
                    {
                        service.Update(dto);
                    }
                    SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                }
                else
                {
                    ViewBag.PlantId = CurrentPlantId;

                    rawMaterialReceived.ProductionLines = GetProductionLines();
                    ModelState.AddModelError(string.Empty, "Please enter required fields.");
                    SetResponseMesssage(ActionTypeMessage.FailedSave);
                    return View(rawMaterialReceived);
                }
            }

            return RedirectToAction("Index");
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (var service = new RawMaterialReceivedService())
                {
                    RawMaterialReceived model =
                    Mapper.Map<RawMaterialReceivedDto, RawMaterialReceived>(service.Get(id.Value));

                    model.ProductionLines = GetProductionLines();
                    return View(model);
                } 
            }

            SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoId);
            return RedirectToAction("Index");
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpPost]
        public ActionResult Delete(int id, string action)
        {
            try
            {
                using (var service = new RawMaterialReceivedService())
                {
                    service.Delete(id);
                }

                SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
            }
            catch (Exception ex)
            {
                //TODO: Move to service
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("FK_RawMaterialQCRedHold_RawMaterialReceived") )
                {
                    using (var service = new RawMaterialReceivedService())
                    {
                        RawMaterialReceived model =
                        Mapper.Map<RawMaterialReceivedDto, RawMaterialReceived>(service.Get(id));

                        model.ProductionLines = GetProductionLines();
                        SetResponseMesssage(ActionTypeMessage.Error,General.ResponseMessageMaterialAssociated);
                        return View(model);
                    } 

                }
            }
            return RedirectToAction("Index");
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RawMaterial = GetProductionLines();

            ViewBag.PlantId = CurrentPlantId;

            var model = new RawMaterialReceived
            {
                ProductionLines = GetProductionLines(),
                DateEntered = DateTime.Now,
                ModifiedBy = CurrentUser,
                EnteredBy = CurrentUser,
                UrlReferrer = System.Web.HttpContext.Current.Request.UrlReferrer.LocalPath
            };
            return View(model);
        }

        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts })]
        [HttpPost]
        public ActionResult Create(RawMaterialReceived rawMaterialReceived, string action)
        {
            if (action == "create")
            {
                if (ModelState.IsValid)
                {
                    var dto = Mapper.Map<RawMaterialReceived, RawMaterialReceivedDto>(rawMaterialReceived);

                    //TODO: move to service
                    dto.ModifiedBy = CurrentUser;
                    using (var service = new RawMaterialReceivedService())
                    {
                        service.Add(dto);
                    }

                    SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                }
                else
                {
                    ViewBag.PlantId = CurrentPlantId;
                    rawMaterialReceived.ProductionLines = GetProductionLines();
                    ModelState.AddModelError(string.Empty, "Please enter required fields.");
                    SetResponseMesssage(ActionTypeMessage.FailedSave);
                    return View(rawMaterialReceived);
                }
            }

            if (rawMaterialReceived.UrlReferrer != null && rawMaterialReceived.UrlReferrer.Contains("RawMaterialQC"))
            {
                return RedirectToAction("Index", "RawMaterialQC");
            }

            return RedirectToAction("Index");
        }


        //[SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator, SecurityTask.QcLabTech })]
        [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.RawMaterialReceipts, SecurityTask.RawMaterialReceiptsReadOnly })]
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult GetRawMatReceivedResult(int rawMatID, int? rows, int? page)
        {
            rows = rows ?? DefaultPageSize;
            page = page ?? DefaultPage;

            int total;
            List<RawMaterialReceived> rawMaterialReceiveds = new List<RawMaterialReceived>();
            using (RawMaterialReceivedService svc = new RawMaterialReceivedService())
            {
                var dtos = svc.GetAllByPlantIdMaterialId(CurrentPlantId, rawMatID);
                total = dtos.Count;
                var currentPageDtos = dtos.OrderBy(r => r.LotNumber).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToList();
                rawMaterialReceiveds.AddRange(Mapper.Map<List<RawMaterialReceivedDto>, List<RawMaterialReceived>>(currentPageDtos));
            }


            return BuildJsonResult(rawMaterialReceiveds, total);
        }
    }
}