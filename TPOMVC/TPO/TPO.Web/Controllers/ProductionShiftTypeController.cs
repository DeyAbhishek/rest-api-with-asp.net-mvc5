using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Production;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ProductionShiftTypeController : BaseController
    {
        //
        // GET: /ProductionShiftType/Edit/5
        public ActionResult Edit(int id=0)
        {
            if (id == 0)
            {
                return View(new ProdDateChangeModel());
            }
            using (var service = new ProdDateChangeService())
            {
                ProdDateChangeModel model =
                    Mapper.Map<ProdDateChangeDto, ProdDateChangeModel>(service.GetAll().Find(p => p.LineID == id));

                return View(model);
            }
        }

        //
        // POST: /ProductionShiftType/Edit/5
        [HttpPost]
        public ActionResult Edit(ProdDateChangeModel model)
        {
            if (ModelState.IsValid)
            {
                ProdDateChangeDto dto = Mapper.Map<ProdDateChangeModel, ProdDateChangeDto>(model);

                //TODO: move to service
                using (var service = new ProdDateChangeService())
                {
                    dto.ID = service.GetAll().Find(p => p.LineID == model.LineID).ID;
                    if (model.Id > 0)
                    {
                        service.Update(dto);
                    }
                }
                SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                SetResponseMesssage(ActionTypeMessage.FailedSave);

                return View(model);
            }

            return RedirectToAction("Edit");


            //string status = "ok";
            //string msg = string.Empty;

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        ProdDateChangeDto dto = Mapper.Map<ProdDateChangeModel, ProdDateChangeDto>(model);

            //        //TODO: move to service
            //        using (var service = new ProdDateChangeService())
            //        {
            //            dto.ID = service.GetAll().Find(p => p.LineID == model.LineID).ID;
            //            if (model.ID > 0)
            //            {
            //                service.Update(dto);
            //            }
            //        }
            //        SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Please enter required fields.");
            //        SetResponseMesssage(ActionTypeMessage.FailedSave);
            //    }
            //}
            //catch (Exception exc)
            //{
            //    status = "error";
            //    msg = exc.Message;
            //}

            //model.ResponseStatus = status;
            //model.ResponseMessage = msg;

            //return View(model);
        }

        [HttpGet]
        public JsonResult GetAllShiftTypeResult()
        {
            List<ProductionShiftTypeModel> shiftType = new List<ProductionShiftTypeModel>();
            using (ProductionShiftTypeService svc = new ProductionShiftTypeService())
            {
                var dtos = svc.GetAll();
                shiftType.AddRange(Mapper.Map<List<ProductionShiftTypeDto>, List<ProductionShiftTypeModel>>(dtos));
            }
            return Json(shiftType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllProductionLineResult()
        {
            List<ProdDateChangeModel> shiftType = new List<ProdDateChangeModel>();
            using (ProdDateChangeService svc = new ProdDateChangeService())
            {
                var dtos = svc.GetAll();
                shiftType.AddRange(Mapper.Map<List<ProdDateChangeDto>, List<ProdDateChangeModel>>(dtos));
            }
            return Json(shiftType, JsonRequestBehavior.AllowGet);
        }
    }
}
