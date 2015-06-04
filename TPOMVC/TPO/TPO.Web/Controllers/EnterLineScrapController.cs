using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Services.Application;
using TPO.Services.Scrim;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Scrap;
using TPO.Services.Production;

namespace TPO.Web.Controllers
{
    public class EnterLineScrapController : BaseController
    {
        //
        // GET: /EnterLineScrap/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetLineScrapResult()
        {
            List<LineScrapModel> model = new List<LineScrapModel>();
            using (TPOLineScrapService svc = new TPOLineScrapService())
            {
                var dtos = svc.GetAll();
                model.AddRange(Mapper.Map<List<TPOLineScrapDto>, List<LineScrapModel>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGroupResult()
        {
            List<TPOLineScrapCodeGroup> model = new List<TPOLineScrapCodeGroup>();
            using (TPOLineScrapCodeGroupService svc = new TPOLineScrapCodeGroupService())
            {
                var dtos = svc.GetAll();
                model.AddRange(Mapper.Map<List<TPOLineScrapCodeGroupDto>, List<TPOLineScrapCodeGroup>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetScrapCodeResult(int groupID)
        {
            List<TPOLineScrapCode> model = new List<TPOLineScrapCode>();
            using (TPOLineScrapCodeService svc = new TPOLineScrapCodeService())
            {
                var dtos = svc.GetByCodeGroup(groupID);
                model.AddRange(Mapper.Map<List<TPOLineScrapCodeDto>, List<TPOLineScrapCode>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDispositionResult()
        {
            List<TPOLineScrapTypeModel> model = new List<TPOLineScrapTypeModel>();
            using (TPOLineScrapTypeService svc = new TPOLineScrapTypeService())
            {
                var dtos = svc.GetAll();
                model.AddRange(Mapper.Map<List<TPOLineScrapTypeDto>, List<TPOLineScrapTypeModel>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllUoMResultByCode(string code)
        {
            UnitOfMeasureTypeModel uomType =
                Mapper.Map<UnitOfMeasureTypeDto, UnitOfMeasureTypeModel>((new UnitOfMeasureTypeService()).GetByCode(code));
            if (uomType != null)
            {
                // TODO: what if uomType is null?
                List<UnitOfMeasureModel> uoms = new List<UnitOfMeasureModel>();
                using (UnitOfMeasureService service = new UnitOfMeasureService())
                {
                    var dtos = service.GetAllByUoMTypeId(uomType.Id);
                    uoms.AddRange(Mapper.Map<List<UnitOfMeasureDto>, List<UnitOfMeasureModel>>(dtos));
                }
                return Json(uoms, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        
        [HttpGet]
        public JsonResult GetAllWeightUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("W");
        }
        [HttpGet]
        public JsonResult GetAllLengthUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("L");
        }

        [HttpGet]
        public JsonResult GetAllWidthUomResult()
        {
            // TODO: fix strings
            return GetAllUoMResultByCode("D");
        }

        [HttpGet]
        public JsonResult GetProductionShift()
        {

            List<ProductionShiftModel> model = new List<ProductionShiftModel>();
            using (ProductionShiftService svc = new ProductionShiftService())
            {
                var dtos = svc.GetAll();
                model.AddRange(Mapper.Map<List<ProductionShiftDto>, List<ProductionShiftModel>>(dtos));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult LineScrapAjaxCreate(LineScrapModel model)
        {
            TPO.Web.Core.ResponseMessage responseMessage;
            var dto = Mapper.Map<LineScrapModel, TPOLineScrapDto>(model);
            dto.BatchNumber = 1;
            dto.PlantID = CurrentPlantId;
            dto.LastModified = DateTime.Now;
            dto.ModifiedBy = CurrentUser;
            dto.DateEntered = DateTime.Now;
            dto.EnteredBy = CurrentUser;
            
            try
            {

                using (TPOLineScrapService svc = new TPOLineScrapService()) 
                {
                    var codeCheckDto = svc.GetByCode(dto.Code);
                    if (codeCheckDto != null && codeCheckDto.ID != dto.ID)
                    {
                        responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, "A roll with that code already exists.");
                    }
                    else 
                    {
                        if (dto.ID > 0)
                        {
                            svc.Update(dto);
                        }
                        else
                        {
                            dto.DateEntered = dto.LastModified;
                            dto.EnteredBy = CurrentUser;
                            dto.ID = svc.Add(dto);
                        }
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
	}
}