using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
namespace TPO.Services.Reclaim
{
    public class TPOReclaimActionService : ServiceBase, ITpoService<TPOReclaimActionDto>
    {
        public int Add(TPOReclaimActionDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = Mapper.Map<TPOReclaimActionDto, TPOReclaimAction>(dto);
            try
            {
                _repository.Repository<TPOReclaimAction>().Insert(entity);
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
            return entity.ID;
        }

        public List<TPOReclaimActionDto> GetAll()
        {
            var entities = _repository.Repository<TPOReclaimAction>().GetAll().ToList();
            return Mapper.Map<List<TPOReclaimAction>, List<TPOReclaimActionDto>>(entities);
        }

        public TPOReclaimActionDto Get(int id)
        {
            var entity = _repository.Repository<TPOReclaimAction>().GetById(id);
            return Mapper.Map<TPOReclaimAction, TPOReclaimActionDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOReclaimAction>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOReclaimActionDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<TPOReclaimAction>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOReclaimAction>().Update(entity);
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

        public void EnterReclaimScrap(int plantID, int lineID, int tpoProductID, double weight, DateTime productionDate, string scrapID, string userName) 
        {
            //First get the product
            var tpoProduct = _repository.Repository<TPOProduct>().GetById(tpoProductID);

            //Set the reclaim type
            string reclaimType = string.Empty;
            string reclaim = string.Empty;
            if (tpoProduct.ProductCode.Substring(0, 4) == "W56V")
            {
                reclaimType = "GEORECLAIM";
                reclaim = "GEOREPEL";
            }
            else
            {
                reclaimType = "RECLAIM";
                reclaim = "REPEL";
            }

            TPOReclaimWIPService wipSvc = new TPOReclaimWIPService();
            TPOReclaimWIPDto wipDto = wipSvc.GetByPlantAndType(plantID, reclaimType);
            var actionType = _repository.Repository<TPOReclaimActionType>().GetAllBy(t => t.Code == "PW").FirstOrDefault();

            DateTime now = DateTime.Now;

            wipDto.Wip = wipDto.Wip + (float)weight;
            wipDto.LastModified = now;
            wipDto.ModifiedBy = userName;
            wipSvc.Update(wipDto);

            TPOReclaimActionDto rcActionDto = new TPOReclaimActionDto()
            {
                PlantID = plantID,
                ProductionDate = productionDate,
                ProdLineId = lineID,
                RecalimActionTypeId = actionType.ID,
                ActionAmount = (float)weight,
                AssocAction = scrapID,
                RecalimType = reclaim,
                DateEntered = now,
                EnteredBy = userName,
                LastModified = now,
                ModifiedBy = userName
            };
            rcActionDto.ID = Add(rcActionDto);
        }
    }
}
