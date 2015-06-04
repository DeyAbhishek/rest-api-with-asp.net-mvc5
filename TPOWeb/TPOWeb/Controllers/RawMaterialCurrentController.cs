using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.Domain.DTO;
using TPOWeb.Models;


namespace TPOWeb.Controllers.RawMaterialCurrent
{
    public class RawMaterialCurrentController : BaseController
    {
        #region Index

        //public void LoadLineDropdown(string lineId)
        //{
        //    var productionLines = ProductionLineRepo.GetProductionLines(CurrentPlantId).ToList();
        //    IEnumerable<SelectListItem> lineItems = from currentLine in productionLines
        //                                            select new SelectListItem { 
        //                                                Text = currentLine.Code,
        //                                                //TODO: Replace with ID after refactor in sprint 2
        //                                                Value = currentLine.Code,
        //                                                Selected = currentLine.Code==lineId,
        //                                            };
        //    ViewBag.productionLineSelectList = new SelectList(lineItems,"","",selectedValue);
        //}

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

        // GET: /RawMaterialCurrent/
        [HttpGet]
        [AcceptVerbs("Get")]
        public ActionResult Index(string lineId)
        {
            List<TPO.Model.Production.ProductionLineModel> productionLines = ProductionLineRepo.GetProductionLines(CurrentPlantId);
            ViewBag.productionLineSelectList = new SelectList(productionLines, "Code", "Code");
            ViewBag.lineId = lineId;

            List<CurrentRawMaterialDTO> currentDTOList = GetRawMaterailListing(lineId);
            List<CurrentRawMaterialViewModel> model = currentDTOList.Select(MapCurrentRawMaterialDTOToViewModel).ToList();

            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        [AcceptVerbs("Post")]
        public ActionResult IndexPost(string lineId)
        {
            List<TPO.Model.Production.ProductionLineModel> productionLines = ProductionLineRepo.GetProductionLines(CurrentPlantId);
            ViewBag.productionLineSelectList = new SelectList(productionLines, "Code", "Code");
            ViewBag.lineId = lineId;

            List<CurrentRawMaterialDTO> currentDTOList = GetRawMaterailListing(lineId);
            List<CurrentRawMaterialViewModel> model = currentDTOList.Select(MapCurrentRawMaterialDTOToViewModel).ToList();

            return View(model);
        }

        #endregion

        #region Details

        // GET: /RawMaterialCurrent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var current = CurrentRawMaterialRepo.GetById(id.Value);
            if (current == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(MapCurrentRawMaterialDTOToViewModel(current));
        }

        #endregion

        #region Create

        // GET: /RawMaterialCurrent/Create
        public ActionResult Create(int plantId)
        {
            var model = new CurrentRawMaterialViewModel
            {
                PlantId = plantId,
                LastModified = DateTime.Now,
                DateEntered = DateTime.Now,
                ModifiedBy = CurrentUser, 
                EnteredBy = CurrentUser
            };

            // MOCK DATA - PRODUCTION LINES
            var lines = ProductionLineRepo.GetProductionLines(plantId);
            ViewBag.LineID = new SelectList(lines, "Code", "Code");


            var rawMaterialCodes = RawMaterialReceivedRepo.GetAvailableRawMaterialIds(plantId);
            ViewBag.RawMaterialReceivedCode = new SelectList(rawMaterialCodes);


            return View(model);
        }

        // POST: /RawMaterialCurrent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PlantID,LineID,RawMaterialReceivedID,DateEntered,EnteredBy,LastModified,ModifiedBy")] CurrentRawMaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = MapCurrentRawMaterialViewModelToDTO(viewModel);
                    dto.ModifiedBy = dto.EnteredBy;
                    dto.LastModified = DateTime.Now;
                    dto.DateEntered = DateTime.Now;
                    CurrentRawMaterialRepo.Add(dto);

                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                    return RedirectToAction("Index", new { lineId = dto.LineId });
                }
                catch (Exception ex)
                {
                    ViewBag.ExceptionMessage = ex.Message;

                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                    return View(viewModel);
                }
            }

            return RedirectToAction("Create");
        }

        #endregion

        #region Edit

        // GET: /RawMaterialCurrent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var dto = CurrentRawMaterialRepo.GetById(id.Value);
                var vm = MapCurrentRawMaterialDTOToViewModel(dto);
                return ShowEditView(vm);
            }

            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return RedirectToAction("Index");

        }

        // POST: /RawMaterialCurrent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PlantID,LineID,RawMaterialReceivedID,DateEntered,EnteredBy,LastModified,ModifiedBy")] CurrentRawMaterialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = MapCurrentRawMaterialViewModelToDTO(viewModel);
                CurrentRawMaterialRepo.Update(dto);

                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                return RedirectToAction("Index", new { lineId = dto.LineId });
            }
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return ShowEditView(viewModel);
        }

        private ActionResult ShowEditView(CurrentRawMaterialViewModel vm)
        {
            var lines = ProductionLineRepo.GetProductionLines(vm.PlantId);
            ViewBag.LineID = new SelectList(lines, "Code", "Code");

            var rawMaterialCodes = RawMaterialReceivedRepo.GetAvailableRawMaterialIds(vm.PlantId);
            ViewBag.RawMaterialReceivedCode = new SelectList(rawMaterialCodes, vm.RawMaterialReceivedCode);

            var lots = RawMaterialReceivedRepo.GetAll(vm.PlantId, (int)(vm.RawMaterialReceivedId ?? 0));
            ViewBag.Lots = new SelectList(lots, "ID", "LotID", vm.RawMaterialReceivedId);

            return View(vm);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = CurrentRawMaterialRepo.GetById(id.Value);
            var viewModel = MapCurrentRawMaterialDTOToViewModel(dto);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string lineIdHolder = CurrentRawMaterialRepo.GetById(id).LineId;
            CurrentRawMaterialRepo.Delete(id);
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessDelete);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
            return RedirectToAction("Index", new { lineId = lineIdHolder });
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get list of Raw Materials Recieved for "Lot" dropdown list
        /// </summary>
        /// <param name="rawMaterialId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLotsForRawMaterial(int rawMaterialId)
        {
            var lots = RawMaterialReceivedRepo.GetAll(1, rawMaterialId);
            var retVal = Json(lots);
            return retVal;
        }

        #endregion

        #region Mapping

        private CurrentRawMaterialViewModel MapCurrentRawMaterialDTOToViewModel(CurrentRawMaterialDTO dto)
        {
            var vm = new CurrentRawMaterialViewModel
            {
                Id = dto.Id,
                LineId = dto.LineId,
                PlantId = dto.PlantId,
                RawMaterialReceivedId = dto.RawMaterialReceivedId,
                RawMaterialReceivedCode = dto.RawMaterialReceivedCode,
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

        private CurrentRawMaterialDTO MapCurrentRawMaterialViewModelToDTO(CurrentRawMaterialViewModel viewModel)
        {
            var dto = new CurrentRawMaterialDTO
            {
                Id = viewModel.Id,
                PlantId = viewModel.PlantId,
                LineId = viewModel.LineId,
                LotId = viewModel.LotId,
                RawMaterialReceivedId = viewModel.RawMaterialReceivedId,
                RawMaterialReceivedCode = viewModel.RawMaterialReceivedCode,
                LastModified = viewModel.LastModified,
                ModifiedBy = viewModel.ModifiedBy,
                DateEntered = viewModel.DateEntered,
                EnteredBy = viewModel.EnteredBy
            };
            if (dto.PlantId == 0)
            {
                dto.PlantId = CurrentPlantId;
            }
            return dto;
        }

        #endregion

    }
}
