using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TPO.Domain.DTO;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.Model.Scrim;
using TPO.Model.Reference;
using TPO.BL.Scrim;
using TPO.BL.Reference;
using TPO.BL.Production;
using TPOWeb.Models;
using System.Web.Script.Serialization;

namespace TPOWeb.Controllers
{
    public class CurrentScrimController : BaseController
    {
        //
        // GET: /CurrentScrim/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CurrentScrim/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CurrentScrim/Create
        public ActionResult Create()
        {
            ScrimType scrimTypeBL = new ScrimType();
            List<ScrimTypeModel> scrimTypes = scrimTypeBL.GetScrimTypeModels();
            scrimTypes.Insert(0, new ScrimTypeModel() { Code="N/A" });
            GetRollTypesList();
            ViewBag.ScrimRoll = new SelectList(new List<TPO.Model.Scrim.TPOCurrentScrimModel>());
            ProductionLine prodLineBL = new ProductionLine();
            ViewBag.ProductionLine = new SelectList(prodLineBL.GetProductionLines(), "Code", "Code");
            TPOCurrentScrimModel model = new TPOCurrentScrimModel();
            return View(model);
        }


        private void GetRollTypesList()
        {
            TPO.BL.Reference.ScrimType stBL = new TPO.BL.Reference.ScrimType();
            ViewBag.ScrimType = new SelectList(stBL.GetScrimTypeModels().Select(s => new { ID = s.ID, Description = string.Format("{0} | {1}", s.Code, s.Description) }).ToList(), "ID", "Description");
        }

        //
        // POST: /CurrentScrim/Create
        [HttpPost]
        public ActionResult Create(TPOCurrentScrimModel model)
        {
            TPOCurrentScrim bl = new TPOCurrentScrim();
            model = bl.InsertTPOCurrentScrimModel(model);
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            return RedirectToAction("Edit", model.ID);
        }


        // GET: /CurrentScrim/Edit/5
        public ActionResult Edit(string lineID)
        {
            var model = ConsolidatedCurrentRawMaterialsAndRollsViewModel_Get(lineID);
            model.LineID = lineID;
            return View(model);
        }

        ////
        //// POST: /CurrentScrim/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, TPOCurrentScrimModel model)
        //{
        //    TPOCurrentScrim bl = new TPOCurrentScrim();
        //    string lineID = model.LineID;
        //    if (id == TPOCurrentScrimModel.INVALID_ID)
        //    {
        //        model = bl.InsertTPOCurrentScrimModel(model);
        //    }
        //    else 
        //    {
        //        bl.UpdateTPOCurrentScrimModel(model);
        //    }
        //    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
        //    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
        //    return RedirectToAction("Edit", new { lineID = lineID });
            
        //}

        [HttpPost]
        public ActionResult Edit(ConsolidatedCurrentRawMaterialsAndRollsViewModel model) 
        {
            TPOCurrentScrim bl = new TPOCurrentScrim();
            string lineID = model.LineID;
            model.TPOCurrentScrim.LineID = lineID;
            if (model.TPOCurrentScrim.ID > 0)
                bl.UpdateTPOCurrentScrimModel(model.TPOCurrentScrim);
            else
                bl.InsertTPOCurrentScrimModel(model.TPOCurrentScrim);
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            return RedirectToAction("Edit", new { lineID = lineID });
        }

        //
        // GET: /CurrentScrim/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CurrentScrim/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public List<CurrentRawMaterialDTO> GetRawMaterailListing(string lineId)
        {
            List<CurrentRawMaterialDTO> currentRawMaterials;
            if (lineId != null)
            {
                currentRawMaterials = CurrentRawMaterialRepo.GetAll(CurrentPlantId, lineId);
            }
            else
            {
                currentRawMaterials = new List<CurrentRawMaterialDTO>();
            }

            return currentRawMaterials;
        }

        private CurrentRawMaterialViewModel MapCurrentRawMaterialDTOToViewModel(CurrentRawMaterialDTO dto)
        {
            var vm = new CurrentRawMaterialViewModel
            {
                Id = dto.Id,
                LineId = dto.LineId,
                PlantId = dto.PlantId,
                RawMaterialReceivedId = dto.RawMaterialReceivedId,
                RawMaterialReceivedCode = dto.RawMaterialReceivedCode,
                QuantityReceived = dto.QuantityReceived,
                QuantityUsed = dto.QuantityUsed,
                LotId = dto.LotId,
                DateEntered = dto.DateEntered,
                EnteredBy = dto.EnteredBy,
                LastModified = dto.LastModified,
                ModifiedBy = dto.ModifiedBy
            };
            if (vm.PlantId == 0)
            {
                vm.PlantId = CurrentPlantId;
            }
            return vm;
        }

        public ConsolidatedCurrentRawMaterialsAndRollsViewModel ConsolidatedCurrentRawMaterialsAndRollsViewModel_Get(string lineID)
        {
            ScrimType scrimTypeBL = new ScrimType();
            List<ScrimTypeModel> scrimTypes = scrimTypeBL.GetScrimTypeModels();
            scrimTypes.Insert(0, new ScrimTypeModel() { Code = "N/A" });
            GetRollTypesList();
            ViewBag.ScrimRoll = new SelectList(new List<TPO.Model.Scrim.TPOCurrentScrimModel>());
            ProductionLine prodLineBL = new ProductionLine();

            List<TPO.Model.Production.ProductionLineModel> lines = prodLineBL.GetProductionLines();
            //lines.Insert(0, new TPO.Model.Production.ProductionLineModel() { Code = "Select Line" });
            ViewBag.ProductionLine = new SelectList(lines, "Code", "Code");
            //ViewBag.productionLineSelectList = new SelectList(lines, "Code", "Code");
            TPOCurrentScrim bl = new TPOCurrentScrim();

            ConsolidatedCurrentRawMaterialsAndRollsViewModel model = new ConsolidatedCurrentRawMaterialsAndRollsViewModel();

            TPOCurrentScrimModel tpoCurrentModel = bl.GetTPOCurrentScrimModelByLineID(lineID);

            if (tpoCurrentModel == null)
            {
                tpoCurrentModel = new TPOCurrentScrimModel();
                model.TPOCurrentScrim = tpoCurrentModel;
            }
            else
            {
                model.TPOCurrentScrim = tpoCurrentModel;
            }

            List<CurrentRawMaterialDTO> currentDTOList = GetRawMaterailListing(lineID);
            List<CurrentRawMaterialViewModel> model2 = currentDTOList.Select(MapCurrentRawMaterialDTOToViewModel).ToList();

            if (!model2.Any())
            {
                model2 = new List<CurrentRawMaterialViewModel>();
                model.CurrentRawMaterialList = model2;
            }
            else
            {
                model.CurrentRawMaterialList = model2;
            }

            if (!model.CurrentRawMaterialList.Any() && model.TPOCurrentScrim.LineID == null)
            {
                model.CurrentRawMaterial = new CurrentRawMaterialViewModel();
                model.CurrentRawMaterial.LineId = "1";
                model.TPOCurrentScrim.LineID = "1";
            }
            else
            {
                model.CurrentRawMaterialList = model2;
            }

            return model;
        }

        [HttpGet]
        public JsonResult GridByType(string lineID, int rows, int page)
        {
            ConsolidatedCurrentRawMaterialsAndRollsViewModel gridInfo = new ConsolidatedCurrentRawMaterialsAndRollsViewModel();
            gridInfo = ConsolidatedCurrentRawMaterialsAndRollsViewModel_Get(lineID);

            //add logic for pagination
            //var z = testRecords.Skip((page - 1) * rows).Take(rows);

            //object[] results = new []
            //{

            //}


            return Json(gridInfo.CurrentRawMaterialList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductionLinesResult()
        {
            ProductionLine prodLineBL = new ProductionLine();
            List<TPO.Model.Production.ProductionLineModel> lines = prodLineBL.GetProductionLines();
            return Json(lines, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void CurrentScrimAjaxCreate(object id)
        {
            var x = true;
        }

    }
}
