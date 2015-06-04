using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Scrim;
using TPO.Web.ActionFilters;
using TPO.Web.Models;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ScrimStockAdjustmentController : BaseController
    {
        //
        // GET: /ScrimStockAdjustment/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NestedGrid()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrimTypeList()
        {
            var scrimTypes = new List<ScrimTypeModel>();
            using (var svc = new ScrimTypeService())
            {
                var dtos = svc.GetAllByPlantId(CurrentPlantId, false);
                scrimTypes.AddRange(Mapper.Map<List<ScrimTypeDto>, List<ScrimTypeModel>>(dtos));
            }
            return Json(scrimTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrimActionTypeList()
        {
            var scrimTypes = new List<ScrimActionTypeModel>();
            using (var svc = new ScrimActionTypeService())
            {
                var dtos = svc.GetAllByPlantId(CurrentPlantId);
                scrimTypes.AddRange(Mapper.Map<List<ScrimActionTypeDto>, List<ScrimActionTypeModel>>(dtos));
            }
            return Json(scrimTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrimRolls(int typeId)
        {
            var scrimRolls = new List<ScrimRollModel>();
            using (var service = new ScrimRollService())
            {
                var dtos = service.GetByType(typeId);
                scrimRolls.AddRange(Mapper.Map<List<ScrimRollDto>, List<ScrimRollModel>>(dtos));
            }

            return Json(scrimRolls, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrimRollDetails(int scrimRollId)
        {
            return Json(new ScrimRollDetailsModel(scrimRollId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetUomsByType(string type)
        {
            var scrimRolls = new List<UnitOfMeasureModel>();
            using (var service = new UnitOfMeasureService())
            {
                var dtos = service.GetByTypeCode(type);
                scrimRolls.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dtos));
            }

            return Json(scrimRolls, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAdjustment(int scrimRollId, int actionTypeId, double adjustment, int uomId, string adjustmentReason)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                var model = new ScrimActionModel();
                {
                    model.ActionDate = DateTime.Now;
                    model.PlantId = CurrentPlantId;
                    model.ScrimRollID = scrimRollId;
                    var uomModel = GetUomModel(uomId);
                    var uomTypeModel = GetUomTypeModel(uomModel.TypeID);
                    var scrimActionTypeModel = GetScrimActionTypeModel(actionTypeId);
                    var scrimRollModel = GetScrimRollModel(scrimRollId);
                    scrimRollModel.IsLoaded = true;
                    model.StartLength = (double)scrimRollModel.Length;
                    model.StartWeight = (double)scrimRollModel.Weight;
                    var uoMConversionService = new UoMConversionService();
                    string adjustmentReasonText;
                    if (uomTypeModel.Code == "L")
                    {
                        model.ActionLength = adjustment;
                        decimal adjustmentLength = uoMConversionService.ConvertUoM(uomId, (decimal)adjustment,
                            scrimRollModel.LengthUoMID);
                        if (scrimActionTypeModel.Code == "SA")
                        {
                            scrimRollModel.LengthUsed -= adjustmentLength;
                            scrimRollModel.Length += adjustmentLength;
                            adjustmentReasonText = "(Adjusted length by " + adjustment + " " + uomModel.Code + ")";
                        }
                        else
                        {
                            scrimRollModel.Length = adjustmentLength;
                            scrimRollModel.LengthUsed = scrimRollModel.ReceivedLength - scrimRollModel.Length;
                            adjustmentReasonText = "(Set length to " + adjustment + " " + uomModel.Code + ")";
                        }
                    }
                    else
                    {
                        model.ActionWeight = adjustment;
                        decimal adjustmentWeight = uoMConversionService.ConvertUoM(uomId, (decimal)adjustment,
                            scrimRollModel.WeightUoMID);
                        if (scrimActionTypeModel.Code == "SA")
                        {
                            scrimRollModel.WeightUsed -= adjustmentWeight;
                            scrimRollModel.Weight += adjustmentWeight;
                            adjustmentReasonText = "(Adjusted weight by " + adjustment + " " + uomModel.Code + ")";
                        }
                        else
                        {
                            scrimRollModel.Weight = adjustmentWeight;
                            scrimRollModel.WeightUsed = scrimRollModel.ReceivedWeight - scrimRollModel.Weight;
                            adjustmentReasonText = "(Set weight to " + adjustment + " " + uomModel.Code + ")";
                        }
                    }
                    if (adjustmentReason == null)
                        adjustmentReason = string.Empty;
                    else
                    {
                        adjustmentReason += " ";
                    }
                    model.DateEntered = DateTime.Now;
                    model.EndLength = (double)scrimRollModel.Length;
                    model.EndWeight = (double)scrimRollModel.Weight;
                    model.EnteredBy = CurrentUser;
                    model.LastModified = DateTime.Now;
                    model.ModifiedBy = CurrentUser;
                    model.RollID = null;
                    model.TypeID = actionTypeId;
                    model.UserID = GetUserModel(CurrentUser).Id;
                    model.ActionReasonText = adjustmentReason + adjustmentReasonText;
                    using (var scrimRollService = new ScrimRollService())
                    {
                        var scrimActionDto = new ScrimActionDto();
                        Mapper.Map(model, scrimActionDto);
                        var scrimRollDto = new ScrimRollDto();
                        Mapper.Map(scrimRollModel, scrimRollDto);
                        scrimRollService.Update(scrimRollDto, scrimActionDto);
                        SetResponseMesssage(ActionTypeMessage.SuccessfulSave);

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

        private UserModel GetUserModel(string user)
        {
            using (var service = new SecurityService())
            {
                var dto = service.GetByUserName(user);
                return Mapper.Map<UserDto, UserModel>(dto);
            }
        }

        private ScrimRollModel GetScrimRollModel(int scrimRollId)
        {
            using (var service = new ScrimRollService())
            {
                var dto = service.Get(scrimRollId);
                return Mapper.Map<ScrimRollDto, ScrimRollModel>(dto);
            }
        }

        private ScrimActionTypeModel GetScrimActionTypeModel(int actionTypeId)
        {
            using (var service = new ScrimActionTypeService())
            {
                var dto = service.Get(actionTypeId);
                return Mapper.Map<ScrimActionTypeDto, ScrimActionTypeModel>(dto);
            }
        }

        private UnitOfMeasureTypeModel GetUomTypeModel(int typeId)
        {
            using (var service = new UnitOfMeasureTypeService())
            {
                var dto = service.Get(typeId);
                return Mapper.Map<UnitOfMeasureTypeDto, UnitOfMeasureTypeModel>(dto);
            }
        }

        private UnitOfMeasureModel GetUomModel(int uomId)
        {
            UnitOfMeasureModel uomModel;
            using (var uomService = new UnitOfMeasureService())
            {
                var uomDto = uomService.Get(uomId);
                uomModel = Mapper.Map<UnitOfMeasureDto, UnitOfMeasureModel>(uomDto);
            }
            return uomModel;
        }
    }
}