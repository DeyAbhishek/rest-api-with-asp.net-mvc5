using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.BL.Repositories.RawMaterial.RawMaterialQCSpecificGravity;
using TPO.DL.Models;
using TPO.Domain.DTO;
using TPOWeb.Models;

namespace TPOWeb.Controllers.RawMaterialQCTest
{
    public class RawMaterialQCSpecificGravityController : BaseController
    {
        private RawMaterialQCSpecificGravityRepository repository = new RawMaterialQCSpecificGravityRepository();

        //TODO: is there a need for this method?
        // GET: /RawMaterialSpecificGravityDetail/
        //public ActionResult Index()
        //{
            //var rawmaterialspecificgravitydetails = db.RawMaterialSpecificGravityDetails.Include(r => r.RawMaterialSpecificGravity);
            //return View(rawmaterialspecificgravitydetails.ToList());
        //}

        // GET: /RawMaterialSpecificGravityDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index","RawMaterialQC");
            }
            var rawMaterialSpecificGravityObject = repository.GetByID(id.Value);
            
            if (rawMaterialSpecificGravityObject != null)
            {
                return View(rawMaterialSpecificGravityObject);
            }
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return RedirectToAction("Index", "RawMaterialQC");
            
        }

        //TODO: is there a need for this method?
        //// GET: /RawMaterialSpecificGravityDetail/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RawMaterialSpecGravID = new SelectList(db.RawMaterialSpecificGravities, "ID", "ID");
        //    return View();
        //}

        //TODO: is there a need for this method?
        //// POST: /RawMaterialSpecificGravityDetail/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="ID,RawMaterialSpecGravID,Submerged,Value,Order,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialSpecificGravityDetail rawmaterialspecificgravitydetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.RawMaterialSpecificGravityDetails.Add(rawmaterialspecificgravitydetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RawMaterialSpecGravID = new SelectList(db.RawMaterialSpecificGravities, "ID", "EnteredBy", rawmaterialspecificgravitydetail.RawMaterialSpecGravID);
        //    return View(rawmaterialspecificgravitydetail);
        //}

        
        [HttpGet]
        public ActionResult GravityDetail(int id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index", "RawMaterialQC");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var specificGravityDto = repository.GetByQCID(id);
            if (specificGravityDto != null)
            {
                return RedirectToAction("Edit", "RawMaterialQCSpecificGravity", new { id = specificGravityDto.ID });
            }

            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return RedirectToAction("Index", "RawMaterialQC");
            //return HttpNotFound();// TODO: correct messaging
        }

        // GET: /RawMaterialSpecificGravityDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index", "RawMaterialQC");
            }
            var specificGravityDto = repository.GetByID(id.Value);
            if (specificGravityDto != null)
            {
                RawMaterialQCSpecificGravityViewModel viewModel = MapDtoToViewModel(specificGravityDto);

                var userBL = new TPO.BL.Security.User();
                ViewBag.QCTech = new SelectList(userBL.GetQCTechUsers(), "ID", "FullName");

                return View(viewModel);
            }
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return RedirectToAction("Index", "RawMaterialQC");
            // TODO: correct messaging

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(RawMaterialQCSpecificGravityViewModel model, double? dryWeight1, double? subWeight1, double? dryWeight2, double? subWeight2, double? dryWeight3, double? subWeight3, double? dryWeight4, double? subWeight4, double? dryWeight5, double? subWeight5)
        {
            if (ModelState.IsValid)
            {
                // Get Original and update dto
                var toUpdate = repository.GetByID(model.ID);
                toUpdate.DenIso = model.DenIso;
                toUpdate.AverageGravity = model.AverageGravity;
                
                if (!string.IsNullOrWhiteSpace(model.LabTechUserID))
                {
                    toUpdate.LabTechUserID = int.Parse(model.LabTechUserID);
                }
                
                
                toUpdate.ModifiedBy = string.IsNullOrWhiteSpace(model.ModifiedBy) ? toUpdate.ModifiedBy : model.ModifiedBy;
                toUpdate.RawMaterialCode = model.RawMaterialCode;
                toUpdate.RawMaterialLotCode = model.RawMaterialLotCode;

                toUpdate.LastModified = DateTime.Now;

                toUpdate.RawMaterialQCID = model.RawMaterialQCId;

                foreach (var detail in toUpdate.RawMaterialSpecificGravityDetails)
                {
                    switch (detail.Order)
                    {
                        case 1:
                            if (detail.Submerged)
                            {
                                detail.Value = subWeight1 ?? 0.0;
                            }
                            else {detail.Value = dryWeight1 ?? 0.0;}
                            break;

                        case 2:
                            if (detail.Submerged)
                            {
                                detail.Value = subWeight2 ?? 0.0;
                            }
                            else {detail.Value = dryWeight2 ?? 0.0;}
                            break;
                            
                        case 3:
                            if (detail.Submerged)
                            {
                                detail.Value = subWeight3 ?? 0.0;
                            }
                            else {detail.Value = dryWeight3 ?? 0.0;}
                            break;
                            
                        case 4:
                            if (detail.Submerged)
                            {
                                detail.Value = subWeight3 ?? 0.0;
                            }
                            else {detail.Value = dryWeight4 ?? 0.0;}
                            break;
                            
                        case 5:
                            if (detail.Submerged)
                            {
                                detail.Value = subWeight5 ?? 0.0;
                            }
                            else {detail.Value = dryWeight5 ?? 0.0;}
                            break;

                    }
                }

                repository.Update(toUpdate);
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                return RedirectToAction("Edit", "RawMaterialQC", new { id = model.RawMaterialQCId });

            }
            return View(model);
        }

        //TODO: is this method needed? do business rules allow deletion of these records?
        // GET: /RawMaterialSpecificGravityDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index", "RawMaterialQC");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var specificGravityDto = repository.GetByID(id.Value);
            if (specificGravityDto != null)
            {
                RawMaterialQCSpecificGravityViewModel viewModel = MapDtoToViewModel(specificGravityDto);

                var userBL = new TPO.BL.Security.User();
                ViewBag.QCTech = new SelectList(userBL.GetQCTechUsers(), "ID", "FullName");

                return View(viewModel);
            }
            TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
            TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
            return RedirectToAction("Index", "RawMaterialQC");
            //return HttpNotFound();
        }

        //TODO: is this method needed? do business rules allow deletion of these records?
        // POST: /RawMaterialSpecificGravityDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var specificGravityDto = repository.GetByID(id);
            if (specificGravityDto == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index", "RawMaterialQC");
                //return HttpNotFound();
            }
            else
            {
                //TODO: wire up delete in the repository if needed.                
            }
            return RedirectToAction("Index");
        }

        #region DTO ViewModel Mapping
        //TODO: move to common
        public RawMaterialQCSpecificGravityViewModel MapDtoToViewModel(RawMaterialQCSpecificGravityDTO dto)
        {
            var model = new RawMaterialQCSpecificGravityViewModel
            {
                ID = dto.ID,
                RawMaterialQCId = dto.RawMaterialQCID,
                DenIso = dto.DenIso,
                EnteredBy = dto.EnteredBy,
                RawMaterialLotCode = dto.RawMaterialLotCode,
                RawMaterialCode = dto.RawMaterialCode,
                AverageGravity = dto.AverageGravity,
                DateEntered = dto.DateEntered ?? DateTime.Now,
                LastModified = dto.LastModified ?? DateTime.Now,
                ModifiedBy = dto.ModifiedBy
            };

            if (dto.LabTechUserID.HasValue)
            {
                model.LabTechUserID = dto.LabTechUserID.Value.ToString();
            }

            model.Details = new List<RawMaterialQCSpecificGravityDetailViewModel>();
            if (dto.RawMaterialSpecificGravityDetails != null)
            {
                foreach (
                    var detailLine in
                        dto.RawMaterialSpecificGravityDetails)
                {
                    model.Details.Add(
                        new RawMaterialQCSpecificGravityDetailViewModel()
                        {
                            Order = detailLine.Order,
                            Value = detailLine.Value,
                            Submerged = detailLine.Submerged
                        });
                }
            }

            return model;
        }

        //TODO: move to common
        public RawMaterialQCSpecificGravityDTO MapToDto(RawMaterialQCSpecificGravityViewModel viewModel)
        {

            var retVal = new RawMaterialQCSpecificGravityDTO
            {
                ID = viewModel.ID,
                RawMaterialQCID = viewModel.RawMaterialQCId,
                DenIso = viewModel.DenIso,
                EnteredBy = viewModel.EnteredBy,
                RawMaterialLotCode = viewModel.RawMaterialLotCode,
                RawMaterialCode = viewModel.RawMaterialCode,
                AverageGravity = viewModel.AverageGravity,
                DateEntered = viewModel.DateEntered > DateTime.MinValue ? viewModel.DateEntered : DateTime.Now,
                LastModified = viewModel.LastModified > DateTime.MinValue ? viewModel.LastModified : DateTime.Now
            };

            int LabTechId;
            if (int.TryParse(viewModel.LabTechUserID, out LabTechId))
            {
                retVal.LabTechUserID = LabTechId;
            }
            retVal.ModifiedBy = viewModel.ModifiedBy;

            retVal.RawMaterialSpecificGravityDetails = new List<RawMaterialQCSpecificGravityDetailDTO>();
            if (viewModel.Details != null)
            {
                foreach (var detail in viewModel.Details)
                {
                    retVal.RawMaterialSpecificGravityDetails.Add(
                        new RawMaterialQCSpecificGravityDetailDTO()
                        {
                            Order = detail.Order,
                            Value = detail.Value,
                            Submerged = detail.Submerged
                        });
                }
            }

            return retVal;
        }

        //TODO: move to common
        public TPO.Model.RawMaterials.RawMaterialQCSpecificGravity MapToModel(TPO.Domain.DTO.RawMaterialQCSpecificGravityDTO dto)
        {

            var model = new TPO.Model.RawMaterials.RawMaterialQCSpecificGravity();
            model.ID = dto.ID;
            model.RawMaterialQCID = dto.RawMaterialQCID;
            model.LabTechUserID = dto.LabTechUserID;
            model.DenIso = dto.DenIso;
            model.EnteredBy = dto.EnteredBy;

            model.RawMaterialLotCode = dto.RawMaterialLotCode;
            model.RawMaterialCode = dto.RawMaterialCode;
            model.AverageGravity = dto.AverageGravity;

            model.DateEntered = dto.DateEntered ?? DateTime.Now;
            model.LastModified = dto.LastModified ?? DateTime.Now;
            model.ModifiedBy = dto.ModifiedBy;
            model.RawMaterialSpecificGravityDetails = new List<TPO.Model.RawMaterials.RawMaterialQCSpecificGravityDetail>();
            foreach (TPO.Domain.DTO.RawMaterialQCSpecificGravityDetailDTO detailLine in dto.RawMaterialSpecificGravityDetails)
            {
                model.RawMaterialSpecificGravityDetails.Add(new TPO.Model.RawMaterials.RawMaterialQCSpecificGravityDetail() { Order = detailLine.Order, Value = detailLine.Value, Submerged = detailLine.Submerged });
            }

            return model;
        }
        #endregion
    }
}
