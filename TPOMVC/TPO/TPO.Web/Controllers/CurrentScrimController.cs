using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPO.Common.DTOs;
using TPO.Common.Enums;
using TPO.Data;
using TPO.Services;
using TPO.Services.Production;
using TPO.Services.RawMaterials;
using TPO.Services.Scrim;
using TPO.Web.ActionFilters;
using TPO.Web.Core;
using TPO.Web.Models;
using RawMaterialReceived = TPO.Web.Models.RawMaterialReceived;
using SecurityTask = TPO.Common.Enums.SecurityTask;

namespace TPO.Web.Controllers
{
    [SecurityTaskAuthorization(RequiredSecurityTasks = new[] { SecurityTask.QcLabTech, SecurityTask.Supervisor, SecurityTask.SystemsAdministrator })]
    public class CurrentScrimController : BaseController
    {
        #region ActionResults
        [HttpGet]
        public ActionResult Edit() 
        {
            return View();
        }

        #endregion 

        #region JasonResults
        //TODO:  This should probably be moved to a production lines controller
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetProductionLinesResult()
        {
            ProductionLineService prodLineBL = new ProductionLineService();
            List<ProductionLinesDto> lines = prodLineBL.GetAll();
            return Json(lines, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GridByType(int lineID, int? rows, int? page)
        {
            rows = rows ?? DefaultPageSize;
            page = page ?? DefaultPage;

            int lineDescription;

            List<CurrentScrimViewModel> list = new List<CurrentScrimViewModel>();
            List<TPOCurrentRawMaterialDto> materialslist = new List<TPOCurrentRawMaterialDto>();

            if (lineID != null)
            {
                using (var service = new ProductionLineService())
                {
                    lineDescription = service.Get(lineID).ID;
                }

                using (var service = new TPOCurrentRawMaterialService())
                {
                    materialslist = service.GetAll().Where(q => q.LineID == lineDescription).ToList();
                }

                foreach (var dto in materialslist)
                {
                    RawMaterialReceivedDto rawMaterialReceivedDto = new RawMaterialReceivedDto();
                    RawMaterialDto rawMaterialDto = new RawMaterialDto();

                    CurrentScrimViewModel csvm = new CurrentScrimViewModel();

                    csvm.Id = dto.ID;
                    csvm.PlantId = CurrentPlantId;

                    using (var service = new RawMaterialReceivedService())
                    {
                        rawMaterialReceivedDto = service.Get(dto.RawMaterialReceivedID ?? 0);
                        csvm.LotNumber = rawMaterialReceivedDto.LotNumber;
                        csvm.RawMaterialID = rawMaterialReceivedDto.Id;
                    }

                    using (var service = new RawMaterialService())
                    {
                        rawMaterialDto = service.Get(Convert.ToInt32(rawMaterialReceivedDto.RawMaterialId));
                        csvm.RawMaterialCode = rawMaterialDto.Code;
                    }

                    csvm.EnteredBy = dto.EnteredBy;
                    csvm.ModifiedBy = dto.ModifiedBy;
                    csvm.DateEntered = dto.DateEntered;
                    csvm.LastModified = dto.LastModified;

                    list.Add(csvm);
                }
            }

            int total;
            total = list.Count();
            List<CurrentScrimViewModel> currentPageDtos = new List<CurrentScrimViewModel>();
            if (rows.HasValue)
            {
                currentPageDtos.AddRange(list.OrderByDescending(r => r.DateEntered).Skip((page.Value - 1) * rows.Value).Take(rows.Value).ToList());
            }
            else 
            {
                currentPageDtos.AddRange(list);
            }

            return BuildJsonResult(currentPageDtos, total);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetScrimRollResult()
        {
            ScrimRollService stBL = new ScrimRollService();
            List<ScrimRollDto> scrimTypeMlModel = stBL.GetAll();
            return Json(scrimTypeMlModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetRawMaterialCodeDropDown(string q)
        {
            List<RawMaterialDto> list = new List<RawMaterialDto>();
            using (var service = new RawMaterialService())
            {
                list = service.GetAll();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetCodeDropDown(string id = "")
        {
            List<RawMaterialReceivedDto> list = new List<RawMaterialReceivedDto>();
            RawMaterialDto rawMaterialDto = new RawMaterialDto();
            if (id != "")
            {
                using (var service = new RawMaterialService())
                {
                    rawMaterialDto = service.GetAll().Where(q => q.Code == id).ToList().FirstOrDefault();
                }

                using (var service = new RawMaterialReceivedService())
                {
                    list = service.GetAll().Where(q => q.RawMaterialId == rawMaterialDto.Id).ToList();
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CurrentScrimAjaxCreate(string id, int lineId)
        {
            ResponseMessage responseMessage;

            try
            {
                CurrentScrimViewModel currentSrimViewModel = JsonConvert.DeserializeObject<CurrentScrimViewModel>(id);

                if (currentSrimViewModel != null)
                {
                    TPOCurrentRawMaterialDto tpoCurrentRawMaterialdto = new TPOCurrentRawMaterialDto();

                    if (currentSrimViewModel.Id > 0)
                    {
                        using (TPOCurrentRawMaterialService service = new TPOCurrentRawMaterialService())
                        {
                            tpoCurrentRawMaterialdto = service.Get(currentSrimViewModel.Id);
                        }

                        using (RawMaterialReceivedService service = new RawMaterialReceivedService())
                        {
                            RawMaterialReceivedDto rawMaterialDto = new RawMaterialReceivedDto();
                            rawMaterialDto =
                                service.GetAll()
                                    .Where(q => q.Id == Convert.ToInt32(currentSrimViewModel.LotNumber))
                                    .ToList()
                                    .FirstOrDefault();

                            tpoCurrentRawMaterialdto.RawMaterialReceivedID = rawMaterialDto.Id;
                        }
                    }
                    else
                    {
                        tpoCurrentRawMaterialdto.PlantID = CurrentPlantId;

                        using (ProductionLineService service = new ProductionLineService())
                        {
                            tpoCurrentRawMaterialdto.LineID = service.Get(lineId).ID;
                        }

                        using (RawMaterialReceivedService service = new RawMaterialReceivedService())
                        {
                            RawMaterialReceivedDto rawMaterialDto = new RawMaterialReceivedDto();
                            rawMaterialDto =
                                service.GetAll()
                                    .Where(q => q.Id == Convert.ToInt32(currentSrimViewModel.LotNumber))
                                    .ToList()
                                    .FirstOrDefault();

                            tpoCurrentRawMaterialdto.RawMaterialReceivedID = rawMaterialDto.Id;
                        }

                    }

                    using (TPOCurrentScrimService service = new TPOCurrentScrimService())
                    {
                        if (currentSrimViewModel.Id > 0)
                        {
                            service.UpdateTPOCurrentRawMaterial(tpoCurrentRawMaterialdto);
                        }
                        else
                        {
                            service.AddTPOCurrentRawMaterial(tpoCurrentRawMaterialdto);
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

        [HttpPost]
        public JsonResult RawMaterialAjaxDelete(string id)
        {
            ResponseMessage responseMessage;

            try
            {
                TPOCurrentRawMaterial tpoCurrentRawMaterial = JsonConvert.DeserializeObject<TPOCurrentRawMaterial>(id);
                if (tpoCurrentRawMaterial != null)
                {
                    TPOCurrentRawMaterialDto dto = new TPOCurrentRawMaterialDto();
                    using (TPOCurrentRawMaterialService service = new TPOCurrentRawMaterialService())
                    {
                        Mapper.Map(tpoCurrentRawMaterial, dto);
                        if (tpoCurrentRawMaterial.ID > 0)
                            service.Delete(dto.ID);
                    }
                }

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulDelete);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OtherMethods

        ////needs work
        //public CurrentScrimViewModel CurrentScrimViewModelViewModel_Get(int? ID)
        //{
        //    ScrimTypeService scrimTypeBL = new ScrimTypeService();
        //    List<ScrimTypeDto> scrimTypes = scrimTypeBL.GetAll();
        //    scrimTypes.Insert(0, new ScrimTypeDto() { Code = "N/A" });
        //    GetRollTypesList();
        //    ViewBag.ScrimRoll = new SelectList(new List<TPOCurrentScrimModel>());

        //    ProductionLineService prodLineBL = new ProductionLineService();
        //    List<ProductionLinesDto> lines = prodLineBL.GetAll();
        //    ViewBag.ProductionLine = new SelectList(lines, "Code", "Code");

        //    TPOCurrentScrimService bl = new TPOCurrentScrimService();

        //    CurrentScrimViewModel model = new CurrentScrimViewModel();

        //    TPOCurrentScrimDto tpoCurrentModel = bl.Get(ID != null ? ID.Value : 0);

        //    if (tpoCurrentModel == null)
        //    {
        //        tpoCurrentModel = new TPOCurrentScrimDto();
        //        model.TPOCurrentScrim = tpoCurrentModel;
        //    }
        //    else
        //    {
        //        model.TPOCurrentScrim = tpoCurrentModel;
        //    }
        //    model.LineID = ID.ToString();







        //    // TODO: refactor this area






        //    //List<TPOCurrentRawMaterialDto> currentDTOList = GetRawMaterailListing(model.LineID);

        //    //if (!currentDTOList.Any())
        //    //{
        //    //    currentDTOList = new List<TPOCurrentRawMaterialDto>();
        //    //    model.CurrentRawMaterialList = currentDTOList;
        //    //}
        //    //else
        //    //{
        //    //    model.CurrentRawMaterialList = currentDTOList;
        //    //}

        //    //if (!model.CurrentRawMaterialList.Any())
        //    //{
        //    //    model.CurrentRawMaterial = new TPOCurrentRawMaterialDto();








        //    //    // TODO: Refactor this area


        //    //    //model.CurrentRawMaterial.LineID = "1";
        //    //    //model.TPOCurrentScrim.LineID = "1";
        //    //}
        //    //else
        //    //{
        //    //    model.CurrentRawMaterialList = currentDTOList;
        //    //}

        //    return model;
        //}

        private void GetRollTypesList()
        {
            ScrimTypeService stBL = new ScrimTypeService();
            ViewBag.ScrimType = new SelectList(stBL.GetAll().Select(s => new { ID = s.ID, Description = string.Format("{0} | {1}", s.Code, s.Description) }).ToList(), "ID", "Description");
        }

        public List<TPOCurrentRawMaterialDto> GetRawMaterailListing(int lineId)
        {
            List<TPOCurrentRawMaterialDto> currentRawMaterials;
            if (lineId != 0)
            {
                TPOCurrentRawMaterialService bl = new TPOCurrentRawMaterialService();
                currentRawMaterials = bl.GetAll().Where(q => q.LineID == lineId).ToList();
            }
            else
            {
                currentRawMaterials = new List<TPOCurrentRawMaterialDto>();
            }

            return currentRawMaterials;
        }

        #endregion

        #region Current Scrim

        [HttpGet]
        public JsonResult CurrentScrimByLineResult(int lineID = 0) 
        {
            TPOCurrentScrimModel model = new TPOCurrentScrimModel();
            using (TPOCurrentScrimService svc = new TPOCurrentScrimService()) 
            {
                var dto = svc.GetByLineID(lineID).FirstOrDefault();
                if (dto == null) 
                {
                    dto = new TPOCurrentScrimDto();
                }
                Mapper.Map<TPOCurrentScrimDto, TPOCurrentScrimModel>(dto, model);
                //Have to set these manually
                //AutoMapper is setting them to null every time
                model.DateEntered = dto.DateEntered;
                model.EnteredBy = dto.EnteredBy;
                model.LastModified = dto.LastModified;
                model.ModifiedBy = dto.ModifiedBy;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCurrentScrimResult(TPOCurrentScrimModel model) 
        {
            ResponseMessage responseMessage;

            try
            {
                var dto = Mapper.Map<TPOCurrentScrimModel, TPOCurrentScrimDto>(model);
                dto.PlantID = CurrentPlantId;
                dto.ModifiedBy = CurrentUser;
                dto.LastModified = DateTime.Now;
                using (TPOCurrentScrimService svc = new TPOCurrentScrimService())
                {
                    if (dto.ID > 0)
                    {
                        svc.Update(dto);
                    }
                    else
                    {
                        dto.DateEntered = dto.LastModified;
                        dto.EnteredBy = CurrentUser;
                        dto.PlantID = CurrentPlantId;
                        dto.ID = svc.Add(dto);
                    }
                }
                model = Mapper.Map<TPOCurrentScrimDto, TPOCurrentScrimModel>(dto);
                //Have to set these manually
                //AutoMapper is setting them to null every time
                model.DateEntered = dto.DateEntered;
                model.EnteredBy = dto.EnteredBy;
                model.LastModified = dto.LastModified;
                model.ModifiedBy = dto.ModifiedBy;

                responseMessage = SetResponseMesssage(ActionTypeMessage.SuccessfulSave);
            }
            catch (Exception exc)
            {
                responseMessage = SetResponseMesssage(ActionTypeMessage.FailedSave, exc.Message);
            }

            return Json(responseMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult ScrimRollsByTypeResult(int typeID = 0)
        {
            List<ScrimRollModel> items = new List<ScrimRollModel>();
            using (ScrimRollService svc = new ScrimRollService()) 
            {
                var dtos = svc.GetByType(typeID);
                items.AddRange(Mapper.Map<List<ScrimRollDto>, List<ScrimRollModel>>(dtos));
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}