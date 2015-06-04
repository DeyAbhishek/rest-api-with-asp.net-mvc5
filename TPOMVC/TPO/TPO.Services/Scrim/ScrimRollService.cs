using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;
using System.Data.Entity.Validation;

namespace TPO.Services.Scrim
{
    public class ScrimRollService : ServiceBase, ITpoService<ScrimRollDto>
    {
        public int Add(ScrimRollDto dto)
        {
            var entity = new ScrimRoll();
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<ScrimRoll>().Insert(entity);
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
            return entity.ID;
        }

        public List<ScrimRollDto> GetAll()
        {
            var entities = _repository.Repository<ScrimRoll>().GetAll().ToList();

            return Mapper.Map<List<ScrimRoll>, List<ScrimRollDto>>(entities);
        }

        public ScrimRollDto Get(int id)
        {
            return Mapper.Map<ScrimRoll, ScrimRollDto>(GetById(id));
        }

        private ScrimRoll GetById(int id)
        {
            return _repository.Repository<ScrimRoll>().GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Repository<ScrimRoll>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(ScrimRollDto dto)
        {
            try
            {
                var entity = GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<ScrimRoll>().Update(entity);
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

        public void MarkRollAsUsed(int scrimRollID, int lineID, string userName) 
        {
            //Get scrim roll and prod line
            var entity = _repository.Repository<ScrimRoll>().GetById(scrimRollID);
            var prodLine = _repository.Repository<ProdLine>().GetById(lineID);

            //Get current production date
            TPO.Services.Production.ProdDateChangeService dateSvc = new Production.ProdDateChangeService();
            var dateChange = dateSvc.GetCurrentProductionDate(lineID);

            //Get current production shift
            TPO.Services.Production.ProductionShiftService shiftSvc = new Production.ProductionShiftService();
            var prodShift = shiftSvc.GetCurrentProductionShift(lineID);

            //Get appropriate action type
            var actionType = _repository.Repository<ScrimActionType>().GetAllBy(at => at.Code == "PR").FirstOrDefault();

            //Get appropriate user
            TPO.Services.Application.SecurityService secSvc = new Application.SecurityService();
            var user = secSvc.GetByUserName(userName);

            //Create ScrimActionDto
            ScrimActionDto actionDto = new ScrimActionDto();
            actionDto.ScrimRollID = scrimRollID;
            actionDto.TypeID = actionType.ID;
            actionDto.ActionLength = (entity.Length - entity.LengthUsed) * -1;
            actionDto.ActionWeight = (entity.Weight - entity.WeightUsed) * -1;
            actionDto.StartLength = entity.Length - entity.LengthUsed;
            actionDto.StartWeight = entity.Weight - entity.WeightUsed;
            actionDto.EndLength = entity.Length - actionDto.StartLength;
            actionDto.EndWeight = entity.Weight - actionDto.StartWeight;
            actionDto.ActionReasonText = "Production Used Roll";
            actionDto.UserID = user.Id;
            actionDto.ActionDate = dateChange.CurrentProductionDate;
            actionDto.ShiftID = prodShift != null ? prodShift.ID : (int?)null;
            actionDto.LineID = lineID;
            actionDto.WorkOrderID = prodLine.CurrentWorkOrderID;
            actionDto.PlantID = prodLine.PlantID;
            actionDto.FleeceProd = false;
            actionDto.DateEntered = DateTime.Now;
            actionDto.EnteredBy = userName;
            actionDto.LastModified = actionDto.DateEntered;
            actionDto.ModifiedBy = actionDto.EnteredBy;

            ScrimActionService actionSvc = new ScrimActionService();
            actionSvc.Add(actionDto);

        }


        public void Update(ScrimRollDto scrimRollDto, ScrimActionDto scrimActionDto)
        {
            try
            {
                var scrimRoll = new ScrimRoll();
                Mapper.Map(scrimRollDto, scrimRoll);
                _repository.Repository<ScrimRoll>().Update(scrimRoll);
                //CommitUnitOfWork();

                var scrimAction = new ScrimAction();
                Mapper.Map(scrimActionDto, scrimAction);
                if (scrimAction.ID > 0)
                    _repository.Repository<ScrimAction>().Update(scrimAction);
                else
                {
                    _repository.Repository<ScrimAction>().Insert(scrimAction);
                }
                CommitUnitOfWork();
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

        public List<ScrimRollDto> GetByType(int typeId)
        {
            var entities = _repository.Repository<ScrimRoll>().GetAll().Where(q=>q.TypeID == typeId).ToList();

            return Mapper.Map<List<ScrimRoll>, List<ScrimRollDto>>(entities);
        }
    }
}
