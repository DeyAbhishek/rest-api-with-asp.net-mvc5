using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.DL.Models;
using TPO.BL.RawMaterials;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using TPO.BL;
using TPO.Domain.DTO;
using TPO.Model;
using TPO.Model.Scrim;
using Newtonsoft.Json.Linq;

namespace TPOWeb.Controllers
{
    public class RawMaterialReceivedController : BaseController
    {
        // GET: /RawMaterial Create/
        [HttpGet]
        [ActionName("Create")]
        [AcceptVerbs("Get")]
        public ActionResult Create()
        {
           TPOMVCApplicationEntities db = new TPOMVCApplicationEntities(); 
           TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
           
           ViewBag.PlantId = CurrentPlantId; 
           TPO.Model.RawMaterials.RawMaterialReceived model = new TPO.Model.RawMaterials.RawMaterialReceived();
           model.DateEntered = DateTime.Now;
           model.Users = new SelectList(this.UserRepo.GetAllUsers(), "UserName", "UserName");
           model.ModifiedBy = CurrentUser;
           model.EnteredBy = CurrentUser;
           model.RawMaterials = from s in RawMatList.GetRawMaterials()
                                select new SelectListItem
                                {
                                    Value = s.RawMaterialId.ToString(CultureInfo.InvariantCulture),
                                    Text = (s.RawMaterialCode + " | " + s.RawMaterialName)
                                };
            
            model.UrlReferrer = System.Web.HttpContext.Current.Request.UrlReferrer.LocalPath;
           return View(model);
        }


        // POST: /RawMaterial Create/
        [HttpPost]
        [ActionName("Create")]
        [AcceptVerbs("Post")]
        public ActionResult Create(DateTime? dateentered, TPO.Model.RawMaterials.RawMaterialReceived rawMaterialReceived, string Action)
        {
            if (Action.Equals("create"))
            {
                if (ModelState.IsValid)
                {
                    //TODO: add mapper
                    var dto = new TPO.Domain.DTO.RawMaterialReceivedDTO();
                    //dto.DateEntered = rawMaterialReceived.DateEntered.Value;
                    dto.DateEntered = DateTime.Now;
                    dto.EnteredBy = rawMaterialReceived.EnteredBy;
                    dto.LotNumber = rawMaterialReceived.LotNumber;
                    dto.PlantID = rawMaterialReceived.PlantID;
                    dto.RawMaterialID = rawMaterialReceived.RawMaterialID;
                    dto.ModifiedBy = CurrentUser;
                    dto.CoA = rawMaterialReceived.CoA;
                    dto.QuantityShipped = rawMaterialReceived.QuantityShipped;
                    dto.QuantityReceived = rawMaterialReceived.QuantityReceived;
                    dto.QuantityNotReceived = rawMaterialReceived.QuantityNotReceived;
                    TPO.BL.RawMaterials.RawMaterialReceived.Add(dto);
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                }
                else
                {
                    ViewBag.PlantId = CurrentPlantId;
                    TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
                    rawMaterialReceived.RawMaterials = from s in RawMatList.GetRawMaterials()
                                                       select new SelectListItem
                                                       {
                                                           Value = s.RawMaterialId.ToString(CultureInfo.InvariantCulture),
                                                           Text = (s.RawMaterialCode + " | " + s.RawMaterialName)
                                                       };
                    ModelState.AddModelError(string.Empty, "Please enter required fields.");
                    rawMaterialReceived.Users = new SelectList(this.UserRepo.GetAllUsers(), "UserName", "UserName");
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                    return View(rawMaterialReceived);
                }
            }

            if (rawMaterialReceived.UrlReferrer != null && rawMaterialReceived.UrlReferrer.Contains("RawMaterialQC"))
            {
                return RedirectToAction("Index", "RawMaterialQC");
            }

            return RedirectToAction("Index");
        }

        // GET: /RawMaterial Edit/
        [HttpGet]
        [ActionName("Edit")]
        [AcceptVerbs("Get")]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var dto = TPO.BL.RawMaterials.RawMaterialReceived.Get(id.Value);
                var model = MapToModel(dto);
                
                TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
                model.RawMaterials = from s in RawMatList.GetRawMaterials()
                                     select new SelectListItem
                                     {
                                         Value = s.RawMaterialId.ToString(CultureInfo.InvariantCulture),
                                         Text = (s.RawMaterialCode + " | " + s.RawMaterialName)
                                     };
                
                model.Users = new SelectList(this.UserRepo.GetAllUsers(), "UserName", "UserName");
                return View(model);
            }

            else
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
            }  
        }

        // POST: /RawMaterial Edit/
        [HttpPost]
        [ActionName("Edit")]
        [AcceptVerbs("Post")]
        public ActionResult Edit(int id, string Action, TPO.Model.RawMaterials.RawMaterialReceived rawMaterialReceived)
        {
            if (Action == "Edit")
            {
                if (ModelState.IsValid)
                {
                    //TODO: add mapper
                    var dto = new TPO.Domain.DTO.RawMaterialReceivedDTO();
                    dto.DateEntered = rawMaterialReceived.DateEntered.Value;
                    dto.EnteredBy = rawMaterialReceived.EnteredBy;
                    dto.LotNumber = rawMaterialReceived.LotNumber;
                    dto.PlantID = rawMaterialReceived.PlantID;
                    dto.RawMaterialID = rawMaterialReceived.RawMaterialID;
                    dto.ID = rawMaterialReceived.ID;
                    dto.ModifiedBy = CurrentUser;
                    TPO.BL.RawMaterials.RawMaterialReceived.Update(dto);
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                }
                else
                {
                    ViewBag.PlantId = CurrentPlantId;
                    TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
                    rawMaterialReceived.RawMaterials = from s in RawMatList.GetRawMaterials()
                                                       select new SelectListItem
                                                       {
                                                           Value = s.RawMaterialId.ToString(CultureInfo.InvariantCulture),
                                                           Text = (s.RawMaterialCode + " | " + s.RawMaterialName)
                                                       };
                    ModelState.AddModelError(string.Empty, "Please enter required fields.");
                    rawMaterialReceived.Users = new SelectList(this.UserRepo.GetAllUsers(), "UserName", "UserName");
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                    return View(rawMaterialReceived);
                }
            }

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Index()
        //{

            

        //    List<TPO.Model.RawMaterials.RawMaterialReceived> model = new List<TPO.Model.RawMaterials.RawMaterialReceived>();

        //    foreach (var dto in all)
        //    {
        //        model.Add(MapToModel(dto));
        //    }

        //    TPO.BL.Production.ProductionLine1 db = new TPO.BL.Production.ProductionLine1();
        //    ViewBag.RawMaterial = new SelectList(db.GetProductionLines(), "Code", "Code");

        //    return View(model);

        //}

        private TPO.Model.RawMaterials.RawMaterialReceived MapToModel(TPO.Domain.DTO.RawMaterialReceivedDTO dto)
        {
            //TODO: Move to common
            //TODO: Add mapper
            var model = new TPO.Model.RawMaterials.RawMaterialReceived();
            
            model.DateEntered = dto.DateEntered;
            model.EnteredBy = dto.EnteredBy;
            model.ID = dto.ID;
            model.LastModified = dto.LastModified;
            model.ModifiedBy = dto.ModifiedBy;
            model.PlantID = dto.PlantID;
            model.PlantCode = dto.PlantCode;
            model.RawMaterialID = dto.RawMaterialID;
            model.RawMaterialCode = dto.RawMaterialCode;
            model.LotNumber = dto.LotNumber;
            model.CoA = dto.CoA;
            model.QuantityShipped = dto.QuantityShipped;
            model.QuantityReceived = dto.QuantityReceived;
            model.QuantityNotReceived = dto.QuantityNotReceived;
            model.QuantityUsedThisLot = dto.QuantityUsedThisLot;
            model.UoMID = dto.UoMID;
            model.UoMCode = dto.UoMCode;
            return model;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            List<RawMaterialReceivedDTO> all = new List<RawMaterialReceivedDTO>();
            //if (id == null)
            //{
            //    all.AddRange(TPO.BL.RawMaterials.RawMaterialReceived.GetAll());
            //}
            //else
            //{
            //    all.AddRange(TPO.BL.RawMaterials.RawMaterialReceived.GetAll(CurrentPlantId, id.Value));
            //}

            if (id != null)
            {
                all.AddRange(TPO.BL.RawMaterials.RawMaterialReceived.GetAll(CurrentPlantId, id.Value));
            }


            List<TPO.Model.RawMaterials.RawMaterialReceived> model = new List<TPO.Model.RawMaterials.RawMaterialReceived>();

            foreach (var dto in all)
            {
                model.Add(MapToModel(dto));
            }

            TPO.BL.Production.ProductionLine1 db = new TPO.BL.Production.ProductionLine1();
            ViewBag.RawMaterial = new SelectList(db.GetProductionLines(), "Code", "Code");

            return View(model);

        }

        [HttpGet]
        [ActionName("Delete")]
        [AcceptVerbs("Get")]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var dto = TPO.BL.RawMaterials.RawMaterialReceived.Get(id.Value);
                var model = MapToModel(dto);
                return View(model);
            }

            else
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
            }  
        
        }

        [HttpPost]
        [ActionName("Delete")]
        [AcceptVerbs("Post")]
        public ActionResult Delete(int id, string Action)
        {
            try 
            { 
                TPO.BL.RawMaterials.RawMaterialReceived.Delete(id);
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessDelete);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("FK_RawMaterialQCRedHold_RawMaterialReceived"))
                        { 
                            var dto = TPO.BL.RawMaterials.RawMaterialReceived.Get(id);
                            var model = MapToModel(dto);
                            TempData["ActionMessage"] =
                                MessageRepository.GetStringValue(MessageKeys.ResponseMessageMaterialAssociated);
                            TempData["ActionMessageType"] =
                                MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                            return View(model);
                        }
                    } 
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Scrim()
        {
            TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
            ViewBag.RawMaterial = new SelectList(db.ScrimRolls, "ID", "Code");
            return View();
        }



        [HttpGet]
        public JsonResult RawMaterialsRecievedGridByType(string ID, int rows, int page)
        {
            List<TPO.Domain.DTO.RawMaterialReceivedDTO> all = new List<TPO.Domain.DTO.RawMaterialReceivedDTO>();
            if (ID == null)
            {
                all.AddRange(TPO.BL.RawMaterials.RawMaterialReceived.GetAll());
            }
            else
            {
                all.AddRange(TPO.BL.RawMaterials.RawMaterialReceived.GetAll(CurrentPlantId, Convert.ToInt32(ID)));
            }

            List<TPO.Model.RawMaterials.RawMaterialReceived> model = new List<TPO.Model.RawMaterials.RawMaterialReceived>();

            foreach (var dto in all)
            {
                model.Add(MapToModel(dto));
            }


            //add logic for pagination
            //var z = testRecords.Skip((page - 1) * rows).Take(rows);

            //object[] results = new []
            //{

            //}

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRawMaterialResult()
        {
            TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
            List<TPO.Model.RawMaterials.RawMaterialModel> rawMatListFromBL = RawMatList.GetRawMaterials();
            return Json(rawMatListFromBL, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RawMaterialAjaxCreate(string id)
        {
            dynamic d = JObject.Parse(id);

            //TODO: add mapper
            var dto = new TPO.Domain.DTO.RawMaterialReceivedDTO();
            TPO.Model.RawMaterials.RawMaterialReceived materials = new TPO.Model.RawMaterials.RawMaterialReceived();

            if (d["ID"] != null)
            {
                dto.ID = d["ID"];
            }

            dto.PlantCode = d["PlantCode"];
            dto.LotNumber = d["LotNumber"];
            dto.DateEntered = d["DateEntered"];
            dto.EnteredBy = d["EnteredBy"];
            dto.LastModified = d["LastModified"];
            dto.ModifiedBy = d["ModifiedBy"];
            dto.QuantityShipped = d["QuantityShipped"];
            dto.QuantityReceived = d["QuantityReceived"];
            dto.QuantityNotReceived = d["QuantityNotReceived"];
            dto.CoA = d["CoA"];

            // find values
            dto.PlantID = 1;
            dto.RawMaterialID = 1;
            dto.ReceivedSizeLimitID = 1;
            dto.UoMID = 1;
            //dto.RawMaterialCode = string.Empty;
            //dto.QuantityUsedThisLot = 1;


            if (dto.ID == 0)
            {
                TPO.BL.RawMaterials.RawMaterialReceived.Add(dto);
            }
            else
            {
                TPO.BL.RawMaterials.RawMaterialReceived.Update(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RawMaterialAjaxDelete(string id)
        {
            dynamic d = JObject.Parse(id);

            //TODO: add mapper
            var dto = new TPO.Domain.DTO.RawMaterialReceivedDTO();
            TPO.Model.RawMaterials.RawMaterialReceived materials = new TPO.Model.RawMaterials.RawMaterialReceived();

            if (d["ID"] != null)
            {
                dto.ID = d["ID"];
            }

            dto.PlantCode = d["PlantCode"];
            dto.LotNumber = d["LotNumber"];
            dto.DateEntered = d["DateEntered"];
            dto.EnteredBy = d["EnteredBy"];
            dto.LastModified = d["LastModified"];
            dto.ModifiedBy = d["ModifiedBy"];
            dto.QuantityShipped = d["QuantityShipped"];
            dto.QuantityReceived = d["QuantityReceived"];
            dto.QuantityNotReceived = d["QuantityNotReceived"];
            dto.CoA = d["CoA"];

            // find values
            dto.PlantID = 1;
            dto.RawMaterialID = 1;
            dto.ReceivedSizeLimitID = 1;
            dto.UoMID = 1;
            //dto.RawMaterialCode = string.Empty;
            //dto.QuantityUsedThisLot = 1;


            TPO.BL.RawMaterials.RawMaterialReceived.Delete(dto.ID);

            return RedirectToAction("Index");
        }

    }

}