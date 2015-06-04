using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using AutoMapper;


namespace TPO.Services.Reclaim
{
    public class TPOReclaimWIPService : ServiceBase, ITpoService<TPOReclaimWIPDto>
    {
        public int Add(TPOReclaimWIPDto dto)
        {
            throw new NotImplementedException();
        }

        public List<TPOReclaimWIPDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TPOReclaimWIPDto Get(int id)
        {
            throw new NotImplementedException();
        }
        public TPOReclaimWIPDto GetByPlantAndType(int plantId, string reclaimType)
        {
            Expression<Func<TPOReclaimWIP, bool>> filterExpression;
            filterExpression = w => w.PlantID == plantId && w.ReclaimType == reclaimType;

            var entity = _repository.Repository<TPOReclaimWIP>().GetAllBy(filterExpression).FirstOrDefault();
            return Mapper.Map<TPOReclaimWIP, TPOReclaimWIPDto>(entity);
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TPOReclaimWIPDto dto)
        {
            throw new NotImplementedException();
        }
        public void Update(int plantId, string reclaimType, string adjustementType, string actionBy, string adjustementAmount = "0")
        {
            Expression<Func<TPOReclaimWIP, bool>> filterExpression;
            filterExpression = w => w.PlantID == plantId && w.ReclaimType == reclaimType;

            TPOReclaimWIP wip = _repository.Repository<TPOReclaimWIP>().GetAllBy(filterExpression).FirstOrDefault();
            if ( wip == null )
            {
                wip = new TPOReclaimWIP();
                wip.PlantID = plantId;
                wip.ReclaimType = reclaimType;
                wip.DateEntered = DateTime.Now;
                wip.EnteredBy = actionBy;
            }

            wip.Wip = adjustementType == "SS"
                ? double.Parse(adjustementAmount)
                : wip.Wip += double.Parse(adjustementAmount);
                        
            wip.LastModified = DateTime.Now;
            wip.ModifiedBy = actionBy;

            var action = new TPOReclaimAction();
            action.ActionAmount = double.Parse(adjustementAmount);
            action.AssocAction = "";            //TODO: need to figure this out
            action.CompAmount = 0;
            action.PlantID = plantId;
            action.ProdLineId = 1;              //TODO: figure current prod line
            action.ProductionShiftId = 1;       //TODO: figure current prod shift;
            action.ReclaimActionTypeId = 4;     //TODO: get the correct type here
            action.ReclaimType = reclaimType;
            
            action.EnteredBy = actionBy;
            action.DateEntered = DateTime.Now;
            action.ModifiedBy = actionBy;
            action.LastModified = DateTime.Now;

            try
            {
                if (wip.ID > 0)
                    _repository.Repository<TPOReclaimWIP>().Update(wip);
                else
                    _repository.Repository<TPOReclaimWIP>().Insert(wip);
                _repository.Repository<TPOReclaimAction>().Insert(action);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
    }
}
