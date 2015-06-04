using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Application;
using TPO.Services.Core;
using AutoMapper;
using System.Data.Entity.Validation;
using System.Text;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialReceivedService : ServiceBase, ITpoService<RawMaterialReceivedDto>
    {

        public int Add(RawMaterialReceivedDto rawMaterialReceivedDto)
        {
            var newRawMaterialReceived = new RawMaterialReceived();
            try
            {
                //rawMaterialReceivedDto.RawMaterialId = 1;

                rawMaterialReceivedDto.LastModified = DateTime.Now;
                rawMaterialReceivedDto.ModifiedBy = CurrentUserName;
                rawMaterialReceivedDto.EnteredBy = CurrentUserName;
                rawMaterialReceivedDto.DateEntered = DateTime.Now;
                Mapper.Map(rawMaterialReceivedDto, newRawMaterialReceived);

                _repository.Repository<RawMaterialReceived>().Insert(newRawMaterialReceived);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return newRawMaterialReceived.ID;
        }

        public void AdjustStock(int plantId, int rawMaterialId, int materialReceivedId, int actionTypeId, decimal quantity, int uomId, string reason, string actionBy)
        {
            using (var rmService = new RawMaterialReceivedService())
            {
                var rawMaterial = rmService.Get(materialReceivedId);
                if (rawMaterial.UoMId != uomId)
                    quantity = new TPO.Services.Application.UoMConversionService().ConvertUoM(uomId, quantity,
                        rawMaterial.UoMId);

                // create the action    
                var action = new RawMaterialActionDto();
                action.PlantID = plantId;
                action.RawMaterialID = rawMaterialId;
                action.LotNumber = materialReceivedId;
                action.TypeID = actionTypeId;
                action.Quantity = (double)quantity;
                action.UserID = new SecurityService().GetByUserName(actionBy).Id;

                // explicitly set nullable columns
                action.ProductionDate = null;
                action.ReasonID = null;
                action.LineID = null;
                action.ShiftID = null;
                action.WorkOrderID = null;
                action.ActionReasonText = string.Format("{0} {1}",
                    actionTypeId == 1 ? "Adjust Stock By" : "Set Stock To", quantity);


                // audit information
                action.EnteredBy = actionBy;
                action.DateEntered = DateTime.Now;
                action.ModifiedBy = action.EnteredBy;
                action.LastModified = action.DateEntered;


                // now update the Raw Material
                var entity = _repository.Repository<RawMaterialReceived>().GetById(materialReceivedId);


                entity.QuantityUsedThisLot = action.TypeID == 1
                    ? entity.QuantityUsedThisLot - action.Quantity // Adjust by a specific amount
                    : (entity.QuantityReceived + action.Quantity); // Set to specific level
                entity.LastModified = DateTime.Now;
                entity.ModifiedBy = action.ModifiedBy;



                // now save the stuff
                _repository.Repository<RawMaterialReceived>().Update(entity);
                _repository.Repository<RawMaterialAction>()
                    .Insert(Mapper.Map<RawMaterialActionDto, RawMaterialAction>(action));
                try
                {
                    _repository.Save();
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    throw;
                }
            }
        }

        public List<RawMaterialReceivedDto> GetAll()
        {

            var entities = _repository.Repository<RawMaterialReceived>().GetAll().ToList();

            return Mapper.Map<List<RawMaterialReceived>, List<RawMaterialReceivedDto>>(entities);

        }
        public List<RawMaterialReceivedDto> GetAllByPlantIdMaterialId(int plantId, int materialId)
        {
            Expression<Func<RawMaterialReceived, bool>> filterExpression;

            if (materialId > 0)
            {
                filterExpression = p => p.PlantID == plantId && p.RawMaterialID == materialId;
            }
            else
            {
                filterExpression = p => p.PlantID == plantId;
            }

            var entities = _repository.Repository<Data.RawMaterialReceived>().GetAllBy(filterExpression).ToList();

            return Mapper.Map<List<Data.RawMaterialReceived>, List<RawMaterialReceivedDto>>(entities);
        }

        public List<RawMaterialReceivedDto> GetAvailable(int plantId, int materialId, bool showUsed)
        {
            Expression<Func<RawMaterialReceived, bool>> filterExpression;
            filterExpression = p => p.PlantID == plantId &&
                                    p.RawMaterialID == materialId &&
                                    p.QuantityReceived > (showUsed ? -1 : p.QuantityUsedThisLot);

            var entities = _repository.Repository<Data.RawMaterialReceived>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<Data.RawMaterialReceived>, List<RawMaterialReceivedDto>>(entities);

        }
        public RawMaterialReceivedDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterialReceived>().GetById(id);
            return Mapper.Map<RawMaterialReceived, RawMaterialReceivedDto>(entity);

        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialReceived>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialReceivedDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                var entity = _repository.Repository<RawMaterialReceived>().GetById(dto.Id);
                Mapper.Map(dto, entity);
                _repository.Repository<RawMaterialReceived>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void DeleteComplete(int id)
        {
            try
            {
                //TPOFormulationRawMaterials handle

                List<int> specificGravityList = new List<int>();
                List<int> specificGravityDetailsList = new List<int>();

                List<RawMaterialSpecificGravity> finalSpecificGravitys = new List<RawMaterialSpecificGravity>();
                List<RawMaterialSpecificGravityDetail> finalSpecificGravitysDetails = new List<RawMaterialSpecificGravityDetail>();
                List<RawMaterialQCRedHold> finalRedHolds = new List<RawMaterialQCRedHold>();


                List<RawMaterialQC> rawMaterialQCList =
                    _repository.Repository<RawMaterialQC>()
                    .GetAll()
                    .Where(q => q.RawMaterialReceivedID == id)
                    .ToList();

                List<TPOCurrentRawMaterial> tpoCurrentRawMaterialsList =
                    _repository.Repository<TPOCurrentRawMaterial>()
                    .GetAll().Where(q => q.RawMaterialReceivedID == id)
                    .ToList();

                foreach (RawMaterialQC rawMaterialQC in rawMaterialQCList)
                {
                    specificGravityList.Add(rawMaterialQC.ID);
                }

                foreach (int specific in specificGravityList)
                {
                    RawMaterialQCRedHold redholdmodel =
                        _repository.Repository<RawMaterialQCRedHold>()
                            .GetAll()
                            .Where(q => q.RawMaterialQCID == specific && q.RawMaterialReceivedID == id)
                            .ToList()
                            .FirstOrDefault();

                    if (redholdmodel != null)
                    {
                        finalRedHolds.Add(redholdmodel);
                    }

                    RawMaterialSpecificGravity specificGravitymodel =
                        _repository.Repository<RawMaterialSpecificGravity>()
                            .GetAll()
                            .Where(q => q.RawMaterialQCID == specific)
                            .ToList()
                            .FirstOrDefault();

                    if (specificGravitymodel != null)
                    {
                        finalSpecificGravitys.Add(specificGravitymodel);
                    }
                }

                foreach (RawMaterialSpecificGravity specificGravity in finalSpecificGravitys)
                {
                    specificGravityDetailsList.Add(specificGravity.ID);
                }

                foreach (int detail in specificGravityDetailsList)
                {
                    RawMaterialSpecificGravityDetail specificGravityDetailmodel =
                        _repository.Repository<RawMaterialSpecificGravityDetail>()
                            .GetAll()
                            .Where(q => q.RawMaterialSpecGravID == detail)
                            .ToList()
                            .FirstOrDefault();

                    if (specificGravityDetailmodel != null)
                    {
                        finalSpecificGravitysDetails.Add(specificGravityDetailmodel);
                    }
                }

                foreach (RawMaterialSpecificGravityDetail specificGravityDetail in finalSpecificGravitysDetails)
                {
                    Mapper.Map<RawMaterialSpecificGravityDetail, RawMaterialQcSpecificGravityDetailDto>(specificGravityDetail);
                    _repository.Repository<RawMaterialSpecificGravityDetail>().Delete(specificGravityDetail);
                }

                foreach (RawMaterialSpecificGravity specificGravity in finalSpecificGravitys)
                {
                    Mapper.Map<RawMaterialSpecificGravity, RawMaterialSpecificGravityDto>(specificGravity);
                    _repository.Repository<RawMaterialSpecificGravity>().Delete(specificGravity);
                }

                foreach (RawMaterialQCRedHold redhold in finalRedHolds)
                {
                    Mapper.Map<RawMaterialQCRedHold, RawMaterialQcRedHoldDto>(redhold);
                    _repository.Repository<RawMaterialQCRedHold>().Delete(redhold);
                }

                foreach (RawMaterialQC rawMaterialQC in rawMaterialQCList)
                {
                    Mapper.Map<RawMaterialQC, RawMaterialQcDto>(rawMaterialQC);
                    _repository.Repository<RawMaterialQC>().Delete(rawMaterialQC);
                }

                foreach (TPOCurrentRawMaterial tpoCurrentRawMaterial in tpoCurrentRawMaterialsList)
                {
                    Mapper.Map<TPOCurrentRawMaterial, TPOCurrentRawMaterialDto>(tpoCurrentRawMaterial);
                    _repository.Repository<TPOCurrentRawMaterial>().Delete(tpoCurrentRawMaterial);
                }

                _repository.Repository<RawMaterialReceived>().Delete(id);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public List<RawMaterialReceivedDto> GetAllByPlantId(int plantId)
        {
            return Mapper.Map<List<RawMaterialReceived>, List<RawMaterialReceivedDto>>(_repository.Repository<RawMaterialReceived>().GetAllBy(r => r.PlantID == plantId).ToList());
        }

        public RawMaterialReceivedDto GetByPlantIdLotNumber(int plantId, string lotNumber)
        {
            return Mapper.Map<RawMaterialReceived, RawMaterialReceivedDto>(_repository.Repository<RawMaterialReceived>().GetAllBy(r => r.PlantID == plantId && r.LotNumber == lotNumber).FirstOrDefault());
        }
    }
}
