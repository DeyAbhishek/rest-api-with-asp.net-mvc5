using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Microsoft.Ajax.Utilities;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.BL.Repositories.RawMaterial.RawMaterialQCSpecificGravity;
using TPO.DL.Models;
using System.Globalization;
using TPO.Domain.DTO;
using TPO.Model.RawMaterials;
using TPOWeb.Models;
using TPO.BL.RawMaterials;
using RawMaterialQCSpecificGravity = TPO.BL.RawMaterials.RawMaterialQCSpecificGravity;
using TPO.Model.Scrim;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace TPOWeb.Controllers
{
    public class RawMaterialQCController : TPOWeb.Controllers.BaseController
    {
        //
        // GET: /RawMaterialQC/
        public ActionResult Index(int? id, bool unlocked = false)
        {
            TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
            //
            var RawMat = db.RawMaterialQCs.Where(s => s.RawMaterialReceivedID != null).ToList();

            TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();
            ViewBag.RawMaterialQCList = RawMatList.GetRawMaterials();

            //TODO: Refactor after RawMaterialModel is completed to use intID
            List<RawMaterialModel> rawMatListFromBL = RawMatList.GetRawMaterials();
            var QCList = db.Users.Where(s => s.ID != null).ToList();


            IEnumerable<SelectListItem> rawMaterialSelectList = from s in rawMatListFromBL
                select new SelectListItem
                {
                    Value = s.RawMaterialCode,
                    Text = (s.RawMaterialCode + " | " + s.RawMaterialName)
                };
            //IEnumerable<SelectListItem> selectList = from s in RawMat
            //                                         select new SelectListItem
            //                                         {
            //                                             Value = s.RawMaterialID,
            //                                             Text = s.RawMaterialID + " | " + s.RawMaterialLotID.ToString()
            //                                         };

            IEnumerable<SelectListItem> lotSelectList = from s in RawMaterialReceivedRepo.GetAll()
                select new SelectListItem
                {
                    Value = s.LotNumber,
                    Text = s.LotNumber + " | " + s.LastModified.ToShortDateString()
                };


            //IEnumerable<SelectListItem> lotSelectList = from s in RawMat
            //                                            select new SelectListItem
            //                                            {
            //                                                Value = s.RawMaterialLotID,
            //                                                Text = (s.RawMaterialLotID.Length != 11 ? s.RawMaterialLotID : s.RawMaterialLotID) + " | " +
            //                                                s.DateEntered.ToShortDateString() +
            //                                                " | " + s.LastModified.ToShortDateString()
            //                                            };

            IEnumerable<SelectListItem> lotTestList = from s in RawMat
                select new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.DateEntered.ToShortDateString() + " | " + s.BoxCarTested
                };


            //new SelectList()
            ViewBag.RawMaterialQC = new SelectList(rawMaterialSelectList, "Value", "Text");
            ViewBag.RawMaterialLotQC = new SelectList(lotSelectList, "Value", "Text");
            ViewBag.RawMaterialQCLotTest = new SelectList(lotTestList, "Value", "Text");
            ViewBag.RawMaterialQCLastModified = new SelectList(RawMat, "ID", "LastModified");
            ViewBag.RawMaterialEnteredBy = new SelectList(QCList, "ID", "FullName");

            TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();

            //if (id != null)
            //{
            //    RawMaterialQCModel model = qcBL.GetRawMaterialQCModelByRawMaterialQCID(id ?? Convert.ToInt32(id));
            //}
            //else
            //{
            //    RawMaterialQCModel model = new RawMaterialQCModel();
            //}

            RawMaterialQCModel model = qcBL.GetRawMaterialQCModelByRawMaterialQCID(id ?? Convert.ToInt32(id));

            if (model == null)
            {
                model = new RawMaterialQCModel();
            }

            var tests = (from t in db.RawMaterialQCs
                where t.RawMaterialReceived.ID == t.RawMaterialReceivedID
                select t).ToList();

            ViewBag.QCItems = tests;

            return View(model);
        }

        // GET: /RawMaterialQC/RawMaterialID
        public ActionResult GetLotIDs(int ID)
        {
            List<SelectListItem> lotIds = new List<SelectListItem>();
            TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
            var LotID = db.RawMaterialReceiveds.Where(s => s.RawMaterialID == ID).ToList();


            lotIds.Add(new SelectListItem {Value = "", Text = "-- Select Lot Number --"});
            foreach (var item in LotID)
            {
                lotIds.Add(new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.RawMaterialID.ToString() + " - " + item.LotNumber + " | " +
                           item.DateEntered.ToShortDateString()
                });
            }

            return Json(new SelectList(lotIds, "Value", "Text"));
        }


        // GET: /RawMaterialQC/LotID
        public ActionResult GetTestIDs(int ID)
        {
            TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

            db.Configuration.ProxyCreationEnabled = false;

            var testRecords =
                db.RawMaterialQCs.Where(x => x.RawMaterialReceivedID == ID)
                    .OrderByDescending(x => x.DateEntered)
                    .ToList();


            List<TPO.DL.Models.RawMaterialQC> list = new List<TPO.DL.Models.RawMaterialQC>();

            list.AddRange(testRecords);

            return Json(list);
        }


        //// GET: /RawMaterialQC/LotID
        //public ActionResult GetTestIDs(int ID)
        //{
        //    List<SelectListItem> testItems = new List<SelectListItem>();
        //    TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
        //    var testRecords =
        //        db.RawMaterialQCs.Where(x => x.RawMaterialReceivedID == ID)
        //            .OrderByDescending(x => x.DateEntered)
        //            .ToList();

        //    if (testRecords.Count > 0)
        //    {
        //        testItems.Add(new SelectListItem {Value = "", Text = "-- Select Test --"});
        //    }
        //    else
        //    {
        //        testItems.Add(new SelectListItem {Value = "", Text = "-- No Tests for Lot: " + ID + " --"});
        //    }
        //    foreach (var item in testRecords)
        //    {
        //        testItems.Add(new SelectListItem
        //        {
        //            Value = item.ID.ToString(),
        //            Text = item.DateEntered.ToString("MM/dd/yy hh:mm") + " | " + item.BoxCarTested
        //        });
        //    }
        //    return Json(testItems);
        //}

        public ActionResult Create(string rm = "", string lot = "")
        {
            TPO.BL.Security.User userBL = new TPO.BL.Security.User();
            ViewBag.QCTech = new SelectList(userBL.GetQCTechUsers(), "ID", "FullName");

            TPO.Model.RawMaterials.RawMaterialQCModel model = new TPO.Model.RawMaterials.RawMaterialQCModel();
            int tmpRawMaterialReceivedId = 0;
            if (!int.TryParse(lot, out tmpRawMaterialReceivedId))
            {
                tmpRawMaterialReceivedId = 0;
            }
            model.RawMaterialReceivedID = tmpRawMaterialReceivedId;
            TPO.DL.Models.RawMaterialReceived rawMaterialLookup =
                new TPOMVCApplicationEntities().RawMaterialQCs.Where(
                    w => w.RawMaterialReceivedID == tmpRawMaterialReceivedId).First().RawMaterialReceived;
            if (rawMaterialLookup == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                //string.Format("Raw Material Test configuration not found for raw material {0}.", rm);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound(string.Format("Raw Material Test configuration not found for raw material {0}.", rm)); 
            }
            model.RawMaterialCode = rawMaterialLookup.RawMaterial.Code;
            model.LotCode = rawMaterialLookup.LotNumber;
            model.QCTechID = CurrentUserID;
            TPO.BL.RawMaterials.RawMaterialTest rmtBL = new TPO.BL.RawMaterials.RawMaterialTest();
            model.QCConfiguration = rmtBL.GetQCConfigurationByRawMaterial(tmpRawMaterialReceivedId);
            if (model.QCConfiguration == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                //string.Format("Raw Material Test configuration not found for raw material {0}.", rm);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound(string.Format("Raw Material Test configuration not found for raw material {0}.", rm)); 
            }
            TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();
            model = qcBL.InsertRawMaterialQCModel(model);
            model.LastModified = DateTime.Now;
            // return View(model);
            return RedirectToAction("Edit", new {id = model.ID});
        }

        //[HttpPost]
        //public ActionResult Create(TPO.Model.RawMaterials.RawMaterialQCModel model) 
        //{
        //   TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();
        //  model = qcBL.InsertRawMaterialQCModel(model);
        //   return RedirectToAction("Edit", new { id = model.ID, unlocked = false });
        //}


        public ActionResult Edit(int id = 0, bool isNew = false, bool success = false)
        {
            TPO.BL.Security.User userBL = new TPO.BL.Security.User();
            ViewBag.QCTech = new SelectList(userBL.GetQCTechUsers(), "ID", "FullName");

            TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();
            TPO.Model.RawMaterials.RawMaterialQCModel model = qcBL.GetRawMaterialQCModelByRawMaterialQCID(id);
            //model.IsEditMode = !isNew;

            //hack
            if (success)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            }
            if (model == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                //string.Format("Raw Material QC with ID {0} not found.", id);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound(string.Format("Raw Material QC with ID {0} not found.", id));
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Edit(TPO.Model.RawMaterials.RawMaterialQCModel model) 
        //{
        //    TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();
        //    qcBL.UpdateRawMaterialQCModel(model);
        //    return View(model);
        //}

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(TPO.Model.RawMaterials.RawMaterialQCModel qc, int id = 0)
            //TPO.Model.RawMaterials.RawMaterialQCModel model
        {
            //TPO.BL.RawMaterials.RawMaterialQC qcBL = new TPO.BL.RawMaterials.RawMaterialQC();
            //qcBL.UpdateRawMaterialQCModel(model);
            //return View(model);
            bool success = false;
            if (ModelState.IsValid)
            {
                TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
                ViewBag.RawMaterial = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID");
                qc.DateEntered = Convert.ToDateTime(System.DateTime.Now);
                qc.EnteredBy = CurrentUser;
                qc.LastModified = Convert.ToDateTime(System.DateTime.Now);
                qc.ModifiedBy = CurrentUser;

                TPO.BL.RawMaterials.RawMaterialQC layeredit = new TPO.BL.RawMaterials.RawMaterialQC();
                layeredit.UpdateRawMaterialQCModel(qc);
                success = true;
            }

            return RedirectToAction("Edit", new {id = id, success = success});

        }

        [HttpPost]
        [ActionName("Index")]


        public ActionResult Index_Post(string Action, FormCollection formCollection, string RawMaterial, string LotID)
        {

            if (Action == "Lot Not In List")
            {
                return RedirectToAction("Create", "RawMaterial");
            }
            if (ModelState.IsValid)
            {
                TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();
                ViewBag.RawMaterial = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID");

                if (Action == "New Test")
                {
                    TPO.DL.Models.RawMaterialQC rawmaterialqc = new TPO.DL.Models.RawMaterialQC();

                    //rawmaterialqc.RawMaterialID = RawMaterial;
                    rawmaterialqc.PlantID = CurrentPlantId;

                    string string1 = formCollection["RawMaterialQC"];
                    string string2 = formCollection["RawMaterialLotQC"];
                    /*
                    if (!(string.IsNullOrEmpty(string1)))
                    {
                        try
                        {
                            rawmaterialqc.RawMaterialReceived.RawMaterialID = int.Parse(string1);
                        }
                        catch
                        {

                        }
                    }
                    */
                    if (!(string.IsNullOrEmpty(string2)))
                    {

                        try
                        {

                            rawmaterialqc.RawMaterialReceivedID = int.Parse(string2);
                        }

                        catch
                        {

                        }
                    }

                    rawmaterialqc.DateEntered = Convert.ToDateTime(System.DateTime.Now);
                    rawmaterialqc.EnteredBy = "";
                    rawmaterialqc.LastModified = Convert.ToDateTime(System.DateTime.Now);
                    rawmaterialqc.ModifiedBy = "";


                    TryUpdateModel(rawmaterialqc);



                    TPO.BL.RawMaterials.RawMaterialQC layer1 = new TPO.BL.RawMaterials.RawMaterialQC();
                    string ID;

                    TryUpdateModel(rawmaterialqc);
                    if (ModelState.IsValid)
                    {


                        ID = layer1.AddRawMaterialTest(rawmaterialqc);



                        return RedirectToAction("Edit", new {id = ID, isNew = true});

                    }
                    else
                    {
                        return RedirectToAction("index");
                    }

                }
                else
                {

                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        #region QCDetail

        [HttpGet]
        //[ChildActionOnly]
        public ActionResult GravityDetail(int rawMaterialQCId)
        {
            var rawMaterialQCSpecificGravity = new RawMaterialQCSpecificGravity();
            var m = rawMaterialQCSpecificGravity.GetByRawMaterialQcId(rawMaterialQCId);
            var model = MapViewModelFromDTO(m);
            return RedirectToAction("Edit", "RawMaterialQCSpecificGravity", new {id = model.ID});

            //RawMaterialSpecificGravity
            //TPO.BL.RawMaterials.RawMaterialQC rmBL = new TPO.BL.RawMaterials.RawMaterialQC();
            //model = rmBL.GetQCDetailModelByRawMaterialQCID(model.ID, model.FieldsUnlocked);
            //return PartialView("_QCDetail", model);
        }

        //[HttpPost]
        //[ChildActionOnly]
        //public ActionResult UpdateQCDetail(TPO.Model.RawMaterials.RawMaterialQCModel model)
        //{
        //    TPO.BL.RawMaterials.RawMaterialQC rmBL = new TPO.BL.RawMaterials.RawMaterialQC();
        //    rmBL.UpdateRawMaterialQCModel(model);
        //    return PartialView("_QCDetail", model);
        //}

        #endregion

        private RawMaterialQCSpecificGravityViewModel MapViewModelFromDTO(RawMaterialQCSpecificGravityDTO dto)
        {
            var retVal = new RawMaterialQCSpecificGravityViewModel
            {
                AverageGravity = dto.AverageGravity,
                DenIso = dto.DenIso,
                EnteredBy = dto.EnteredBy,
                ID = dto.ID,
                LabTechUserID =
                    dto.LabTechUserID.HasValue
                        ? dto.LabTechUserID.Value.ToString(CultureInfo.InvariantCulture)
                        : string.Empty,
                ModifiedBy = dto.ModifiedBy,
                RawMaterialCode = dto.RawMaterialCode,
                RawMaterialLotCode = dto.RawMaterialLotCode,
                RawMaterialQCId = dto.RawMaterialQCID,
                Details = new List<RawMaterialQCSpecificGravityDetailViewModel>()
            };
            if (dto.LastModified.HasValue)
            {
                retVal.LastModified = dto.LastModified.Value;
            }
            if (dto.DateEntered.HasValue)
            {
                retVal.DateEntered = dto.DateEntered.Value;
            }

            foreach (var rawMaterialQCSpecificGravityDetailDTO in dto.RawMaterialSpecificGravityDetails)
            {
                var detail = new RawMaterialQCSpecificGravityDetailViewModel
                {
                    Id = rawMaterialQCSpecificGravityDetailDTO.ID,
                    rawMaterialQCSpecificGravityId = rawMaterialQCSpecificGravityDetailDTO.RawMaterialSpecGravID,
                    Order = rawMaterialQCSpecificGravityDetailDTO.Order,
                    Submerged = rawMaterialQCSpecificGravityDetailDTO.Submerged,
                    Value = rawMaterialQCSpecificGravityDetailDTO.Value,
                    EnteredBy = rawMaterialQCSpecificGravityDetailDTO.EnteredBy,
                    ModifiedBy = rawMaterialQCSpecificGravityDetailDTO.ModifiedBy,

                };
                if (rawMaterialQCSpecificGravityDetailDTO.DateEntered.HasValue)
                {
                    detail.DateEntered = rawMaterialQCSpecificGravityDetailDTO.DateEntered.Value;
                }
                if (rawMaterialQCSpecificGravityDetailDTO.LastModified.HasValue)
                {
                    detail.LastModified = rawMaterialQCSpecificGravityDetailDTO.LastModified.Value;
                }
            }

            return retVal;

        }

        [HttpGet]
        public JsonResult RawMaterialsGridByType(string typeID, int rows, int page)
        {
            var newTypeID = Convert.ToInt32(typeID);

            TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

            db.Configuration.ProxyCreationEnabled = false;

            var testRecords =
                db.RawMaterialQCs.Where(x => x.RawMaterialReceivedID == newTypeID)
                    .OrderByDescending(x => x.DateEntered)
                    .ToList();


            //add logic for pagination
            //var z = testRecords.Skip((page - 1) * rows).Take(rows);

            //object[] results = new []
            //{
                
            //}

            return Json(testRecords, JsonRequestBehavior.AllowGet);
        }

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

            return model;
        }

        //Temporary as no assiociated method to retrieve LotIDs
        [HttpGet]
        public JsonResult GetLotIdsResult(int ID)
        {
            List<TPO.Domain.DTO.RawMaterialReceivedDTO> all = new List<TPO.Domain.DTO.RawMaterialReceivedDTO>();
            if (ID == 0)
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

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}