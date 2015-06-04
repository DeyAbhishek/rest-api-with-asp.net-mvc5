using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TPO.DL.Models;
using TPO.DL.Repositories;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.Model.Scrim;
using ScrimRoll = TPO.BL.Scrim.ScrimRoll;
using Newtonsoft.Json.Linq;

namespace TPOWeb.Controllers
{
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
            ScrimRoll bl = new ScrimRoll();
            ViewBag.ScrimRoll = bl.GetScrimRollModels();

            if (id == null)
            {
                ((List<ScrimRollModel>)ViewBag.ScrimRoll).Insert(0, new ScrimRollModel());

                GetRollTypesList();
                GetUoMList();
                //return View(bl.GetScrimRollModelsByTypeID(1));
                return View(new List<ScrimRollModel>());
            }
            else
            {

                GetRollTypesList();
                GetUoMList();
                //ScrimRollModel model = bl.GetScrimRollModelByID((int)id);
                List<ScrimRollModel> model = bl.GetScrimRollModelsByTypeID((int)id);
                if (model == null)
                {
                    //TempData["ActionMessage"] = string.Format("ScrimRoll with ID {0} not found.", id);
                    TempData["ActionMessage"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                    TempData["ActionMessageType"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                    return RedirectToAction("Details");
                    //return HttpNotFound(string.Format("ScrimRoll with ID {0} not found.", id)); 
                }
                return View(model);
            }


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
            GetUoMList();
            ScrimRollModel model = new ScrimRollModel();
            return View(model);
        }

        //
        // POST: /ScrimRoll/Create
        [HttpPost]
        public ActionResult Create(DateTime? date, ScrimRollModel model)
        {
            if (date == null)
            {
                ModelState.AddModelError(string.Empty, "The Received Date field is required");
                
            } 
            if (ModelState.IsValid)
                {
                    ScrimRoll bl = new ScrimRoll();
                    model.EnteredBy = CurrentUser;
                    model.ModifiedBy = CurrentUser;
                    model.DateEntered = DateTime.Now;
                    model.LastModified = DateTime.Now;
                    model.DateReceived = date ?? DateTime.Now;
                    model = bl.InsertScrimRoll(model);
                    TempData["ActionMessage"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                    TempData["ActionMessageType"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);

                    return RedirectToAction("Details", new { id = model.ID });
                    //return RedirectToAction("Edit", new { id = model.ID });
                }
                else
                {

                  //  ModelState.AddModelError(string.Empty, "Please enter required fields.");
                    GetRollTypesList();
                    GetUoMList();

                    return View(model);

                }
        }

        //
        // GET: /ScrimRoll/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                //removed to keep from initial load of scrim page from displaying error.
                //TempData["ActionMessage"] = TPO.BL.Constants.Responses.ResponseMessages["FailNoId"];
                //TempData["ActionMessageType"] = TPO.BL.Constants.Responses.ResponseTypes["error"];
                return RedirectToAction("Details");
            }
            GetRollTypesList();
            GetUoMList();
            ScrimRoll bl = new ScrimRoll();
            ViewBag.ScrimRoll = bl.GetScrimRollModels();
            ScrimRollModel model = bl.GetScrimRollModelByID(id);
            if (model == null) { model = new ScrimRollModel(); }

            return View(model);
        }

        private void GetUoMList()
        {
            TPO.BL.Reference.UnitOfMeasure uomBL = new TPO.BL.Reference.UnitOfMeasure();
            ViewBag.WeightUoM = new SelectList(uomBL.GetWeightUnitsOfMeasure(), "ID", "Code");
            ViewBag.LengthUoM = new SelectList(uomBL.GetLengthUnitsOfMeasure(), "ID", "Code");
        }

        private void GetRollTypesList()
        {
            TPO.BL.Reference.ScrimType stBL = new TPO.BL.Reference.ScrimType();
            ViewBag.ScrimType = new SelectList(stBL.GetScrimTypeModels().Select(s => new
            {

                ID = s.ID,
                Description = string.Format("{0} | {1}", s.Code, s.Description)
            }).ToList(), "ID", "Description");

            //  var x = ViewBag.ScrimType;
        }

        //
        // POST: /ScrimRoll/Edit/5
        [HttpPost]
        public ActionResult Edit(DateTime? date, int id, ScrimRollModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                GetRollTypesList();
                GetUoMList();

                return View(model);
            }

            model.EnteredBy = CurrentUser;
            model.ModifiedBy = CurrentUser;
            model.DateEntered = DateTime.Now;
            model.LastModified = DateTime.Now;
            model.DateReceived = date ?? DateTime.Now;
            ScrimRoll bl = new ScrimRoll();
            bl.UpdateScrimRoll(model);
            TempData["ActionMessage"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
            TempData["ActionMessageType"] = TPO.BL.Repositories.Message.MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            return RedirectToAction("Details", new { id = model.ID });
        }

        [HttpGet]
        public JsonResult ListByType(int typeID)
        {
            ScrimRoll scrimBL = new ScrimRoll();
            List<ScrimRollModel> list = scrimBL.GetScrimRollModelsByTypeID(typeID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ScrimRollGridByType(string typeID, int rows, int page)
        {
            int testtypeID = Convert.ToInt32(typeID);
            ScrimRoll bl = new ScrimRoll();
            //ScrimRollModel model = bl.GetScrimRollModelByID(testtypeID);
            List<ScrimRollModel> list = bl.GetScrimRollModelsByTypeID(testtypeID);

            //add logic for pagination
            //var z = testRecords.Skip((page - 1) * rows).Take(rows);

            //object[] results = new []
            //{

            //}

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetScrimRollResult()
        {
            TPO.BL.Reference.ScrimType stBL = new TPO.BL.Reference.ScrimType();
            List<TPO.Model.Reference.ScrimTypeModel> scrimTypeMlModel = stBL.GetScrimTypeModels();
            return Json(scrimTypeMlModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ScrimRollAjaxCreate(string id)
        {
            dynamic d = JObject.Parse(id);

            ScrimRollModel model = new ScrimRollModel();
            ScrimRoll bl = new ScrimRoll();

            if (d["ID"] != null)
            {
                model.ID = d["ID"];
            }

            model.ScrimRollCode = d["ScrimRollCode"]; //required
            model.ScrimRollTypeID = d["ScrimRollTypeID"];
            model.Length = d["Length"];
            model.DateReceived = d["DateReceived"]; //required
            model.ReceivedLength = d["ReceivedLength"];
            

            // TODO: set values
            model.WeightUnitOfMeasureID = 1; //required
            model.LengthUnitOfMeasureID = 1; //required

            //model.LotCode = string.Empty;
            //model.WovenLotCode = string.Empty;
            //model.Weight = 0;
            //model.TareWeight = 0;
            //model.ReceivedWeight = 0;
            //model.ReceivedTareWeight = 0;
            //model.LengthUsed = 0;
            //model.WeightUsed = 0;

            model.EnteredBy = CurrentUser;
            model.ModifiedBy = CurrentUser;
            model.DateEntered = DateTime.Now;
            model.LastModified = DateTime.Now;


            if (model.ID == -1)
            {
                model = bl.InsertScrimRoll(model);
            }
            else
            {
                bl.UpdateScrimRoll(model);
            }

            return RedirectToAction("Details");
        }
    }
}
