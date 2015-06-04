using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using System.Data.Entity.Validation;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialQCRedHoldService : ServiceBase, ITpoService<RawMaterialQcRedHoldDto>
    {
        public int Add(RawMaterialQcRedHoldDto dto)
        {
            var entity = Mapper.Map<RawMaterialQcRedHoldDto, RawMaterialQCRedHold>(dto);
            try
            {
                _repository.Repository<RawMaterialQCRedHold>().Insert(entity);
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

        public List<RawMaterialQcRedHoldDto> GetAll()
        {
            var entities = _repository.Repository<RawMaterialQCRedHold>().GetAll().ToList();
            
            return Mapper.Map<List<RawMaterialQCRedHold>, List<RawMaterialQcRedHoldDto>>(entities);
        }


        public RawMaterialQcRedHoldDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterialQCRedHold>().GetById(id);
            var dto = Mapper.Map<RawMaterialQCRedHold, RawMaterialQcRedHoldDto>(entity);
            dto.RawMaterialReceived = entity.RawMaterialReceived.RawMaterial.Code;
            dto.BoxCarTested = entity.RawMaterialReceived.LotNumber;
            return dto;
        }

        public RawMaterialQcRedHoldDto GetByQC(int qcID) 
        {
            var entities = _repository.Repository<RawMaterialQCRedHold>().GetAllBy(r => r.RawMaterialQCID == qcID).ToList();
            return Mapper.Map<RawMaterialQCRedHold, RawMaterialQcRedHoldDto>(entities.FirstOrDefault());
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialQCRedHold>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialQcRedHoldDto dto)
        {
            try
            {
                var entity = _repository.Repository<RawMaterialQCRedHold>().GetById(dto.Id);
                Mapper.Map(dto, entity);
                _repository.Repository<RawMaterialQCRedHold>().Update(entity);
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
