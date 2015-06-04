using System;
using System.Linq;
using System.Web.Mvc;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services;
using TPO.Services.Application;
using TPO.Services.FailProperties;
using TPO.Services.RawMaterials;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.LeadOperator, SecurityTask.SystemsAdministrator, SecurityTask.Manager })]
    public class RawMaterialQCRedHoldController : BaseController
    {
        #region ActionResults

        // GET: RawMaterialQCRedHold
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int qcID = 0) 
        {
            if (qcID == 0) 
            {
                SetResponseMesssage(ActionTypeMessage.Error,General.ResponseMessageFailNoId);
                return RedirectToAction("Index", "RawMaterialQC");
            }
            using (RawMaterialQCRedHoldService rhSvc = new RawMaterialQCRedHoldService()) 
            {
                var dto = rhSvc.GetByQC(qcID);
                if (dto != null) 
                {
                    return RedirectToAction("Edit", new { id = dto.Id });
                }
            }
            RawMaterialQCRedHoldViewModel model = new RawMaterialQCRedHoldViewModel();
            model.RawMaterialQCId = qcID;

            using (RawMaterialsQcService svc = new RawMaterialsQcService()) 
            {
                RawMaterialQcDto qcDto = svc.Get(qcID);
                model.RawMaterialReceivedId = qcDto.RawMaterialReceivedId;
                using (RawMaterialReceivedService receiveSvc = new RawMaterialReceivedService()) 
                {
                    RawMaterialReceivedDto receivedDto = receiveSvc.Get(qcDto.RawMaterialReceivedId);
                    model.BoxCarTested = receivedDto.LotNumber;
                    model.RawMaterialReceived = receivedDto.RawMaterialCode;
                    ViewBag.RawMaterialReceivedID = new SelectList(receiveSvc.GetAll(), "ID", "RawMaterialID");
                }
            }
            PrepareSelectLists();
            return View(model);

        }
        //public ActionResult Create(DateTime? holddate, DateTime? managerdate, DateTime? reddate, int? id)
        //{
        //    RawMaterialQCRedHoldViewModel model = new RawMaterialQCRedHoldViewModel();

        //    if (id == null)
        //    {
                
        //    }
        //    else
        //    {



        //        //model.RawMaterialQCId = qcLookupObj.ID;
        //        //model.QCTechId = qcLookupObj.QCTechUserID;
        //        //model.PlantId = qcLookupObj.PlantID;
        //        //model.BoxCarTested = qcLookupObj.BoxCarTested;
        //        //model.RawMaterialReceived = qcLookupObj.RawMaterialReceived.RawMaterial.Code;
        //        //model.LotNumber = qcLookupObj.RawMaterialReceived.LotNumber;
        //        //model.RawMaterialReceivedId = qcLookupObj.RawMaterialReceivedID;

        //        model.ManagerDate = managerdate;
        //        model.HoldDate = holddate;
        //        model.RedDate = reddate;
        //        model.RawMaterialQCId = id ?? -1;

                
        //        RawMaterialReceivedService rmsrv = new RawMaterialReceivedService();
                

        //    }
        //    return View(model);
        //}

        // POST: /RawMaterialQCRedHold/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(
        //    [Bind(Include = "Id,RawMaterialQCId,PlantId,RawMaterialReceivedId,FailPropertyId,HoldLotId,QCTechId,LeadOperatorId,SupervisorId,ManagerId,RedDate,Zone,RedComments,RedCorrectionAction,HoldDate,HoldComments,ManagerDate,ManagerComments,PrimeBoxCar,PrimeUOM,ReworkBoxCar,ReworkUOM,ScrapBoxCar,ScrapUOM,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQCRedHoldViewModel viewModel)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RawMaterialQCRedHoldViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //separate view model data into dtos

                    //var msrv = new MessageService();
                    using (var rhsrv = new RawMaterialQCRedHoldService())
                    {
                        var dto = AutoMapper.Mapper.Map<RawMaterialQCRedHoldViewModel, RawMaterialQcRedHoldDto>(viewModel);
                        dto.PlantId = CurrentPlantId;
                        dto.EnteredBy = CurrentUser;
                        dto.DateEntered = DateTime.Now;
                        dto.ModifiedBy = CurrentUser;
                        dto.LastModified = DateTime.Now;

                        //save item, get item ID back
                        int itemId = rhsrv.Add(dto);

                        //if item ID is valid redirect to edit page.
                        if (itemId > 0)
                        {
                            SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                            return RedirectToAction("Edit", "RawMaterialQCRedHold", new {ID = itemId});
                        }
                    }
                }
                catch (Exception ex)
                {
                    SetResponseMesssage(ActionTypeMessage.FailedSave);
                }
                finally
                {

                    PrepareSelectLists();
                    RawMaterialReceivedService rmsrv = new RawMaterialReceivedService();
                    ViewBag.RawMaterialReceivedID = new SelectList(rmsrv.GetAll(), "ID", "RawMaterialID");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0) 
            {
                SetResponseMesssage(ActionTypeMessage.Error, General.ResponseMessageFailNoId);
                return RedirectToAction("Index", "RawMaterialQC");
            }

            RawMaterialQCRedHoldViewModel model = null;
            using (RawMaterialQCRedHoldService svc = new RawMaterialQCRedHoldService()) 
            {
                var dto = svc.Get(id);
                model = AutoMapper.Mapper.Map<RawMaterialQcRedHoldDto, RawMaterialQCRedHoldViewModel>(dto);
            }
            PrepareSelectLists();
            return View(model ?? new RawMaterialQCRedHoldViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RawMaterialQCRedHoldViewModel viewModel)
        {
            try
            {
                using (RawMaterialQCRedHoldService svc = new RawMaterialQCRedHoldService()) 
                {
                    var dto = svc.Get(viewModel.Id);
                    dto = AutoMapper.Mapper.Map<RawMaterialQCRedHoldViewModel, RawMaterialQcRedHoldDto>(viewModel, dto);
                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;
                    svc.Update(dto);
                    SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
                }
            }
            catch (Exception ex)
            {
                SetResponseMesssage(ActionTypeMessage.FailedSave);
            }
            finally 
            {
                using (RawMaterialReceivedService svc = new RawMaterialReceivedService()) 
                {
                    ViewBag.RawMaterialReceivedID = new SelectList(svc.GetAll(), "ID", "RawMaterialID");
                }
            }
            PrepareSelectLists();
            return View(viewModel);
        }

        #endregion

        #region JsonResults



        #endregion

        #region OtherMethods

        private void PrepareSelectLists()
        {
            using (var fpsrv = new FailPropertiesService())
            {
                using (var plantService = new PlantService())
                {
                    using (var securityService = new SecurityService())
                    {

                        ViewBag.FailProperties = new SelectList(fpsrv.GetAll(), "ID", "Code");
                        ViewBag.Plants = new SelectList(plantService.GetAll(), "ID", "Code");

                        var qcTechItems = securityService.GetAll().ToList();
                        var leadOpItems = securityService.GetAll().ToList();
                        var supervisorItems = securityService.GetAll().ToList();
                        var managerItems = securityService.GetAll().ToList();


                        ViewBag.QCTechs = new SelectList(qcTechItems, "ID", "FullName");
                        ViewBag.LeadOperators = new SelectList(leadOpItems, "ID", "FullName");
                        ViewBag.Supervisors = new SelectList(supervisorItems, "ID", "FullName");
                        ViewBag.Managers = new SelectList(managerItems, "ID", "FullName");
                    }
                }
            }
        }

        #endregion
    }
}