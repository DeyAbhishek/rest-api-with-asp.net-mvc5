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
using TPO.DL.Models;
using TPO.Domain.DTO;
using TPOWeb.Models;

namespace TPOWeb.Controllers.RawMaterialQCTest
{
    public class RawMaterialQCRedHoldController : BaseController
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /RawMaterialQCRedHold/
        public ActionResult Index()
        {
            var rawmaterialqcredholds = db.RawMaterialQCRedHolds.Include(r => r.FailProperty).Include(r => r.Plant).Include(r => r.User).Include(r => r.User1).Include(r => r.User2).Include(r => r.RawMaterialReceived).Include(r => r.User3);
            return View(rawmaterialqcredholds.ToList());
        }

        // GET: /RawMaterialQCRedHold/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
            if (rawmaterialqcredhold == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialqcredhold);
        }

        // GET: /RawMaterialQCRedHold/Create/QCID
        public ActionResult Create(DateTime? holddate, DateTime? managerdate, DateTime? reddate, int? id)
        {
            TPOWeb.Models.RawMaterialQCRedHoldViewModel model = new Models.RawMaterialQCRedHoldViewModel();

            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index","RawMaterialQC");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else 
            {
                //Sloppy error handling, have to fix the null ID variable. 
                RawMaterialQCRedHoldDTO lookupObjByQCId = RawMaterialQCRedHoldRepo.GetByQCID(id??-1);
                
                //test to see if record already exists, if it does, switch to edit view
                if (lookupObjByQCId != null && lookupObjByQCId.ID >= 0)
                {
                    return RedirectToAction("Edit","RawMaterialQCRedHold", new { ID = lookupObjByQCId.ID });
                }

                    //TODO: Rewrite after QC object has been refactored.
                    TPO.DL.Models.RawMaterialQC qcLookupObj = db.RawMaterialQCs.Find(id);

                    model.RawMaterialQCId = qcLookupObj.ID;
                    model.QCTechId = qcLookupObj.QCTechUserID;
                    model.PlantId = qcLookupObj.PlantID;
                    model.BoxCarTested = qcLookupObj.BoxCarTested;
                    model.RawMaterialReceived = qcLookupObj.RawMaterialReceived.RawMaterial.Code;
                    model.LotNumber = qcLookupObj.RawMaterialReceived.LotNumber;
                    model.RawMaterialReceivedId = qcLookupObj.RawMaterialReceivedID;
                  
                    model.ManagerDate = managerdate;
                    model.HoldDate = holddate;
                    model.RedDate = reddate;

                    PrepareSelectLists();

                    ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID");

            }
            return View(model);
        }

        // POST: /RawMaterialQCRedHold/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include="Id,RawMaterialQCId,PlantId,RawMaterialReceivedId,FailPropertyId,HoldLotId,QCTechId,LeadOperatorId,SupervisorId,ManagerId,RedDate,Zone,RedComments,RedCorrectionAction,HoldDate,HoldComments,ManagerDate,ManagerComments,PrimeBoxCar,PrimeUOM,ReworkBoxCar,ReworkUOM,ScrapBoxCar,ScrapUOM,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQCRedHoldViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //map objects for database
                    TPO.Domain.DTO.RawMaterialQCRedHoldDTO dto = MapRawMaterialQCRedHoldViewModeltoDTO(viewModel);
                    //update object records 
                    
                    dto.ModifiedBy = CurrentUser;
                    dto.EnteredBy = CurrentUser; 
                    dto.LastModified = DateTime.Now;
                    dto.DateEntered = DateTime.Now;

                    //save item, get item ID back
                    int itemId = RawMaterialQCRedHoldRepo.Add(dto);

                    //if item ID is valid redirect to edit page.
                    if (itemId > 0)
                    {
                        TempData["ActionMessage"] =
                            MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                        TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                        return RedirectToAction("Edit","RawMaterialQCRedHold", new { ID = itemId });
                    }
                }
                catch (Exception ex)
                {
                    //ViewBag.ExceptionMessage = ex.Message;
                    //return View(viewModel);
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                }
                finally
                {
                    
                    PrepareSelectLists();

                    ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID");
                }
            }
            return View(viewModel);
        }

        // GET: /RawMaterialQCRedHold/Edit/5
        public ActionResult Edit(DateTime? holddate, DateTime? managerdate, DateTime? reddate, int? id)
        {
            RawMaterialQCRedHoldViewModel model = new Models.RawMaterialQCRedHoldViewModel();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
                
                //Sloppy error handling, have to fix the null ID variable. 
                RawMaterialQCRedHoldDTO dto = RawMaterialQCRedHoldRepo.GetByID(id ?? -1);
                if (dto == null)
                {
                    return HttpNotFound();
                }
                else 
                {
                    model = MapRawMaterialQCRedHoldDTOtoViewModel(dto);

                    //TODO: Rewrite after QC object has been refactored.
                    GetQcFormDetail(ref model);


                    model.ManagerDate = managerdate;
                    model.HoldDate = holddate;
                    model.RedDate = reddate;

                    PrepareSelectLists();

                    ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID");                
                }

            }
            catch(Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return View(model);
            }

            return View(model);
        }

        private void GetQcFormDetail(ref TPOWeb.Models.RawMaterialQCRedHoldViewModel model)
        {
            //TODO: Rewrite after QC object has been refactored.
            TPO.DL.Models.RawMaterialQC qcLookupObj = db.RawMaterialQCs.Find(model.RawMaterialQCId);
            model.RawMaterialQCId = qcLookupObj.ID;
            model.QCTechId = qcLookupObj.QCTechUserID;
            model.PlantId = qcLookupObj.PlantID;
            model.BoxCarTested = qcLookupObj.BoxCarTested;

            TPO.BL.RawMaterials.RawMaterial RawMatList = new TPO.BL.RawMaterials.RawMaterial();

            model.RawMaterialReceived = qcLookupObj.RawMaterialReceived.RawMaterial.Code + " | " +
                                        qcLookupObj.RawMaterialReceived.RawMaterial.Description;
            model.LotNumber = qcLookupObj.RawMaterialReceived.LotNumber;

        }

        // POST: /RawMaterialQCRedHold/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,RawMaterialQCId,PlantId,RawMaterialReceivedId,FailPropertyId,HoldLotId,QCTechId,LeadOperatorId,SupervisorId,ManagerId,RedDate,Zone,RedComments,RedCorrectionAction,HoldDate,HoldComments,ManagerDate,ManagerComments,PrimeBoxCar,PrimeUOM,ReworkBoxCar,ReworkUOM,ScrapBoxCar,ScrapUOM,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQCRedHoldViewModel viewModel)
        {
            ViewBag.SuccessMessage = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //map objects for database
                    TPO.Domain.DTO.RawMaterialQCRedHoldDTO dto = MapRawMaterialQCRedHoldViewModeltoDTO(viewModel);
                    //update object records 
                    dto.EnteredBy = dto.EnteredBy ?? CurrentUser;
                    dto.DateEntered = dto.DateEntered ?? DateTime.Now;
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;
                    //save item, get item ID back
                    RawMaterialQCRedHoldRepo.Update(dto);
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageSuccessSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeSuccess);
                    //ViewBag.SuccessMessage = "Save completed successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.ExceptionMessage = ex.Message;
                    TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailSave);
                    TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                    //return View(viewModel);
                }
                finally
                {
                    
                    ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID");
                }
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Please enter required fields.");
                
            }

            PrepareSelectLists();
            GetQcFormDetail(ref viewModel);
            return View(viewModel);
        }

        private void PrepareSelectLists()
        {
            ViewBag.FailProperties = new SelectList(db.FailProperties, "ID", "Code");
            ViewBag.Plants = new SelectList(db.Plants, "ID", "Code");

            var qcTechItems = UserRepo.GetAllUsers().ToList();
            var leadOpItems = UserRepo.GetAllUsers().ToList();
            var supervisorItems = UserRepo.GetAllUsers().ToList();
            var managerItems = UserRepo.GetAllUsers().ToList();


            ViewBag.QCTechs = new SelectList(qcTechItems, "ID", "FullName");
            ViewBag.LeadOperators = new SelectList(leadOpItems, "ID", "FullName");
            ViewBag.Supervisors = new SelectList(supervisorItems, "ID", "FullName");
            ViewBag.Managers = new SelectList(managerItems, "ID", "FullName");
        }


        private RawMaterialQCRedHoldViewModel MapRawMaterialQCRedHoldDTOtoViewModel(RawMaterialQCRedHoldDTO dto)
        {
            var vm = new RawMaterialQCRedHoldViewModel
            {
                Id = dto.ID,
                RawMaterialQCId = dto.RawMaterialQCID,
                PlantId = dto.PlantID,
                RawMaterialReceivedId = dto.RawMaterialReceivedID,
                QCTechId = dto.QCTechID,
                SupervisorId = dto.SupervisorID,
                LeadOperatorId = dto.LeadOperatorID,
                RedDate = dto.RedDate,
                FailPropertyId = dto.FailPropertyID,
                Zone = dto.Zone,
                RedComments = dto.RedComments,
                RedCorrectionAction = dto.RedCorrectionAction,
                HoldDate = dto.HoldDate,
                HoldLotNumber = dto.HoldLotNumber,
                HoldComments = dto.HoldComments,
                ManagerID = dto.ManagerID,
                ManagerDate = dto.ManagerDate,
                ManagerComments = dto.ManagerComments,
                PrimeBoxCar = dto.PrimeBoxCar,
                PrimeUOM = dto.PrimeUOM,
                ReworkBoxCar = dto.ReworkBoxCar,
                ReworkUOM = dto.ReworkUOM,
                ScrapBoxCar = dto.ScrapBoxCar,
                ScrapUOM = dto.ScrapUOM,
                DateEntered = dto.DateEntered,
                EnteredBy = dto.EnteredBy,
                LastModified = dto.LastModified,
                ModifiedBy = dto.ModifiedBy
            };
            return vm;
        }

        private RawMaterialQCRedHoldDTO MapRawMaterialQCRedHoldViewModeltoDTO(RawMaterialQCRedHoldViewModel vm)
        {
            var dto = new RawMaterialQCRedHoldDTO
            {
                ID = vm.Id,
                RawMaterialQCID = vm.RawMaterialQCId,
                PlantID = vm.PlantId,
                RawMaterialReceivedID = vm.RawMaterialReceivedId,
                QCTechID = vm.QCTechId,
                SupervisorID = vm.SupervisorId,
                LeadOperatorID = vm.LeadOperatorId,
                RedDate = vm.RedDate,
                FailPropertyID = vm.FailPropertyId,
                Zone = vm.Zone,
                RedComments = vm.RedComments,
                RedCorrectionAction = vm.RedCorrectionAction,
                HoldDate = vm.HoldDate,
                HoldLotNumber = vm.HoldLotNumber,
                HoldComments = vm.HoldComments,
                ManagerID = vm.ManagerID,
                ManagerDate = vm.ManagerDate,
                ManagerComments = vm.ManagerComments,
                PrimeBoxCar = vm.PrimeBoxCar,
                PrimeUOM = vm.PrimeUOM,
                ReworkBoxCar = vm.ReworkBoxCar,
                ReworkUOM = vm.ReworkUOM,
                ScrapBoxCar = vm.ScrapBoxCar,
                ScrapUOM = vm.ScrapUOM,
                DateEntered = vm.DateEntered,
                EnteredBy = vm.EnteredBy,
                LastModified = vm.LastModified,
                ModifiedBy = vm.ModifiedBy
            };
            return dto;
        }



        [HttpGet]
        public JsonResult GridByType(string lineID)
        {
            var x = string.Empty;


            ConsolidatedCurrentRawMaterialsAndRollsViewModel gridInfo = new ConsolidatedCurrentRawMaterialsAndRollsViewModel();


            return Json(gridInfo.CurrentRawMaterialList, JsonRequestBehavior.AllowGet);
        }





        // GET: /RawMaterialQCRedHold/Delete/5
/*        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
            if (rawmaterialqcredhold == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialqcredhold);
        }

        // POST: /RawMaterialQCRedHold/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
            db.RawMaterialQCRedHolds.Remove(rawmaterialqcredhold);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }










        // GET: /RawMaterialQCRedHold/Edit/5
        public ActionResult HoldFormEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
            if (rawmaterialqcredhold == null)
            {
                return HttpNotFound();
            }
            ViewBag.FailPropertyID = new SelectList(db.FailProperties, "ID", "Code", rawmaterialqcredhold.FailPropertyID);
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqcredhold.PlantID);
            ViewBag.LeadOperatorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.LeadOperatorID);
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.ManagerID);
            ViewBag.QCTechID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.QCTechID);
            ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID", rawmaterialqcredhold.RawMaterialReceivedID);
            ViewBag.SupervisorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.SupervisorID);
            return View(rawmaterialqcredhold);
        }

        // POST: /RawMaterialQCRedHold/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoldFormEdit([Bind(Include = "ID,PlantID,RawMaterialReceivedID,FailPropertyID,HoldLotID,QCTechID,LeadOperatorID,SupervisorID,ManagerID,RedDate,Zone,RedComments,RedCorrectionAction,HoldDate,HoldComments,ManagerDate,ManagerComments,PrimeBoxCar,PrimeUOM,ReworkBoxCar,ReworkUOM,ScrapBoxCar,ScrapUOM,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQCRedHold rawmaterialqcredhold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialqcredhold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FailPropertyID = new SelectList(db.FailProperties, "ID", "Code", rawmaterialqcredhold.FailPropertyID);
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqcredhold.PlantID);
            ViewBag.LeadOperatorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.LeadOperatorID);
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.ManagerID);
            ViewBag.QCTechID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.QCTechID);
            ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID", rawmaterialqcredhold.RawMaterialReceivedID);
            ViewBag.SupervisorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.SupervisorID);
            return View(rawmaterialqcredhold);
        }




        // GET: /RawMaterialQCRedHold/Edit/5
        public ActionResult RedFormEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQCRedHold rawmaterialqcredhold = db.RawMaterialQCRedHolds.Find(id);
            if (rawmaterialqcredhold == null)
            {
                return HttpNotFound();
            }
            ViewBag.FailPropertyID = new SelectList(db.FailProperties, "ID", "Code", rawmaterialqcredhold.FailPropertyID);
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqcredhold.PlantID);
            ViewBag.LeadOperatorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.LeadOperatorID);
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.ManagerID);
            ViewBag.QCTechID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.QCTechID);
            ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID", rawmaterialqcredhold.RawMaterialReceivedID);
            ViewBag.SupervisorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.SupervisorID);
            return View(rawmaterialqcredhold);
        }

        // POST: /RawMaterialQCRedHold/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedFormEdit([Bind(Include = "ID,PlantID,RawMaterialReceivedID,FailPropertyID,HoldLotID,QCTechID,LeadOperatorID,SupervisorID,ManagerID,RedDate,Zone,RedComments,RedCorrectionAction,HoldDate,HoldComments,ManagerDate,ManagerComments,PrimeBoxCar,PrimeUOM,ReworkBoxCar,ReworkUOM,ScrapBoxCar,ScrapUOM,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQCRedHold rawmaterialqcredhold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialqcredhold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FailPropertyID = new SelectList(db.FailProperties, "ID", "Code", rawmaterialqcredhold.FailPropertyID);
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqcredhold.PlantID);
            ViewBag.LeadOperatorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.LeadOperatorID);
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.ManagerID);
            ViewBag.QCTechID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.QCTechID);
            ViewBag.RawMaterialReceivedID = new SelectList(db.RawMaterialReceiveds, "ID", "RawMaterialID", rawmaterialqcredhold.RawMaterialReceivedID);
            ViewBag.SupervisorID = new SelectList(db.Users, "ID", "Username", rawmaterialqcredhold.SupervisorID);
            return View(rawmaterialqcredhold);
        }
*/
    }
}
