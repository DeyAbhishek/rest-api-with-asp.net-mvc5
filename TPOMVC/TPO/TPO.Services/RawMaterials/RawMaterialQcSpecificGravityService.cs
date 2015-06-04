using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialQcSpecificGravityService : ServiceBase, ITpoService<RawMaterialSpecificGravityDto>
    {
        public int Add(RawMaterialSpecificGravityDto dto)
        {
            try
            {
                var entity = new RawMaterialSpecificGravity();
                Mapper.Map(dto, entity);
                _repository.Repository<RawMaterialSpecificGravity>().Insert(entity);
                _repository.Save();
                return entity.ID;
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
                return -1;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public List<RawMaterialSpecificGravityDto> GetAll()
        {
            var entities = _repository.Repository<RawMaterialSpecificGravity>().GetAll();
            var dtos =
                Mapper.Map<IEnumerable<RawMaterialSpecificGravity>, IEnumerable<RawMaterialSpecificGravityDto>>(entities);
            return dtos.ToList();
        }

        public RawMaterialSpecificGravityDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterialSpecificGravity>().GetById(id);
            var dto = Mapper.Map<RawMaterialSpecificGravity, RawMaterialSpecificGravityDto>(entity);
            dto.RawMaterialCode = entity.RawMaterialQC.RawMaterialReceived.RawMaterial.Code;
            dto.RawMaterialLotCode = entity.RawMaterialQC.RawMaterialReceived.LotNumber;
            return dto;
        }

        public RawMaterialSpecificGravityDto GetByQcId(int id)
        {
            RawMaterialSpecificGravityDto dto = null;
            var entity = _repository.Repository<RawMaterialQC>().GetById(id);
            if (entity.RawMaterialSpecificGravities != null)
            {
                var entityGravity = entity.RawMaterialSpecificGravities.FirstOrDefault();
                dto = Mapper.Map<RawMaterialSpecificGravity, RawMaterialSpecificGravityDto>(entityGravity);
            }
            return dto;
        }

        public RawMaterialSpecificGravityDto CreateNew(int rawMatID)
        {
            RawMaterialSpecificGravityDto dto = new RawMaterialSpecificGravityDto
            {
                RawMaterialQcId = rawMatID,
                DenIso = 0.7851,
                RawMaterialSpecificGravityDetails =  new Collection<RawMaterialQcSpecificGravityDetailDto>()
            };
            for (int i = 0; i < 5; i++)
            {
                var dry = new RawMaterialQcSpecificGravityDetailDto() 
                {
                    Order = (i + 1),
                    Submerged = false
                };
                var submerged = new RawMaterialQcSpecificGravityDetailDto() 
                {
                    Order = (i + 1),
                    Submerged = true
                };
                dto.RawMaterialSpecificGravityDetails.Add(dry);
                dto.RawMaterialSpecificGravityDetails.Add(submerged);
            }
            return dto;
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialSpecificGravity>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialSpecificGravityDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = _repository.Repository<RawMaterialSpecificGravity>().GetById(dto.Id);
            //Mapper.Map<RawMaterialSpecificGravityDto, RawMaterialSpecificGravity>(dto, entity);
            //AutoMapper is not playing nice with the RawMaterialSpecificGravity property on the RawMaterialSpecificGravityDetail record
            //It keeps overwriting it, even thought that property doesn't exist on the DTO
            //So, we're doing this by hand for now
            entity.LabTechUserID = dto.LabTechUserId;
            entity.LastModified = dto.LastModified;
            entity.ModifiedBy = dto.ModifiedBy;
            foreach (var detail in dto.RawMaterialSpecificGravityDetails) 
            {
                var entityDetail = _repository.Repository<RawMaterialSpecificGravityDetail>().GetById(detail.Id);
                entityDetail.LastModified = dto.LastModified;
                entityDetail.ModifiedBy = dto.ModifiedBy;
                entityDetail.Value = detail.Value;
                _repository.Repository<RawMaterialSpecificGravityDetail>().Update(entityDetail);
            }
            entity.RawMaterialQC.SpecGrav = dto.AverageGravity;
            try
            {
                _repository.Repository<RawMaterialSpecificGravity>().Update(entity);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);                
                throw;
            }
        }
    }
}
