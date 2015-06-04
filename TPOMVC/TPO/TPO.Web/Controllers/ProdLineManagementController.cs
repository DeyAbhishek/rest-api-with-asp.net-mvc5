using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Common.Resources;
using TPO.Services.Application;
using TPO.Services.Production;
using TPO.Web.ActionFilters;
using TPO.Web.Models;
using TPO.Services.Scrim;
using AutoMapper;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class ProdLineManagementController : BaseController
    {
        // GET: ProdLineManagement
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfigLineRoll(int lineTypeId)
        {
            ProdLineRollConfigViewModel model = new ProdLineRollConfigViewModel();
            
            model.TypeID = lineTypeId;

            return View(model);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GridByType(int typeID = 0, int rows = 0, int page = 0)
        {
            List<ProductionLineManagementModel> data = new List<ProductionLineManagementModel>();
            using (ProductionLineService svc = new ProductionLineService()) 
            {
                var dtos = svc.GetByType(typeID).OrderBy(t => t.LineDescCode).ToList();
                for (int i = 0; i < dtos.Count; i++) 
                {
                    var model = AutoMapper.Mapper.Map<ProductionLinesDto, ProductionLineManagementModel>(dtos[i]);
                    model.Adhesive = dtos[i].RCComp;
                    model.Compatibilizer = dtos[i].RCComp;
                    model.TPOLineRolls = dtos[i].TPOMorC;
                    model.RollsProcessed = dtos[i].TPOMorC;
                    data.Add(model);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public JsonResult GetTypeByLineID(int lineID = 0) 
        {
            ProdLineTypeModel type = null;
            using (ProdLineTypeService svc = new ProdLineTypeService()) 
            {
                var dto = svc.GetByLineID(lineID);
                type = Mapper.Map<ProdLineTypeDto, ProdLineTypeModel>(dto);
            }
            return Json(type, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductionLineTypesResult()
        {
            List<ProdLineTypeDto> data = new List<ProdLineTypeDto>();
            using (ProdLineTypeService svc = new ProdLineTypeService())
            {
                data.AddRange(svc.GetAll().OrderBy(r => r.ProdLineTypeDesc).ToList());
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetProdLineRollConfigs(int lineTypeId)
        {
            ProdLineRollConfigService plrcsrv = new ProdLineRollConfigService();

            List<ProdLineRollConfigDto> list = plrcsrv.GetAll().Where(q => q.TypeID == lineTypeId).OrderBy(q=>q.Order).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProdLineResult(ProductionLineManagementModel model) 
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionLineService svc = new ProductionLineService())
                {
                    var dto = svc.Get(model.Id);
                    dto = AutoMapper.Mapper.Map<ProductionLineManagementModel, ProductionLinesDto>(model, dto);

                    using (ProdLineTypeService typeSvc = new ProdLineTypeService())
                    {
                        ProdLineTypeDto typeDto = typeSvc.Get(dto.LineTypeID);
                        if (typeDto != null)
                        {
                            switch (typeDto.ProdLineTypeCode)
                            {
                                case "TPO":
                                case "RW":
                                    {
                                        switch (typeDto.ProdLineTypeCode)
                                        {
                                            case "TPO":
                                                {
                                                    dto.TPOMorC = model.TPOLineRolls;
                                                }
                                                break;
                                            case "RW":
                                                {
                                                    dto.TPOMorC = model.RollsProcessed;
                                                }
                                                break;
                                        }
                                        dto.RCComp = "NA";
                                    }
                                    break;
                                case "RC":
                                case "CO":
                                    {
                                        switch (typeDto.ProdLineTypeCode)
                                        {
                                            case "RC":
                                                {
                                                    dto.RCComp = model.Compatibilizer;
                                                }
                                                break;
                                            case "CO":
                                                {
                                                    dto.RCComp = model.Adhesive;
                                                }
                                                break;
                                        }
                                        dto.TPOMorC = "NA";
                                    }
                                    break;
                                default:
                                    {
                                        dto.TPOMorC = "NA";
                                        dto.RCComp = "NA";
                                    }
                                    break;
                            }
                        }
                    }

                    dto.ModifiedBy = CurrentUser;
                    dto.LastModified = DateTime.Now;

                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        
                        dto.RepOrder = 1;

                        dto.DateEntered = DateTime.Now;
                        dto.EnteredBy = CurrentUser;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }

                    model = AutoMapper.Mapper.Map<ProductionLinesDto, ProductionLineManagementModel>(dto);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            model.ResponseMessage = responseMessage;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProdLineRollConfigDelete(int id)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProdLineRollConfigService service = new ProdLineRollConfigService())
                {
                    service.Delete(id);
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProdLineRollConfigSave(string row)
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            ProdLineRollConfigViewModel prodLineRollConfig =
                JsonConvert.DeserializeObject<ProdLineRollConfigViewModel>(row);

            try
            {
                if (prodLineRollConfig != null)
                {
                    ProdLineRollConfigDto dto = new ProdLineRollConfigDto();
                    using (ProdLineRollConfigService service = new ProdLineRollConfigService())
                    {
                        Mapper.Map(prodLineRollConfig, dto);
                        if (prodLineRollConfig.Id > 0)
                            service.Update(dto);
                        else
                        {
                            service.Add(dto);
                        }
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            prodLineRollConfig.ResponseMessage = responseMessage;

            return Json(prodLineRollConfig, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLine(int id = 0) 
        {
            TPO.Web.Core.ResponseMessage responseMessage;

            try
            {
                using (ProductionLineService svc = new ProductionLineService())
                {
                    svc.Delete(id);
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