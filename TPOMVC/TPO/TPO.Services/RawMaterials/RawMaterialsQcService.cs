using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using log4net.Filter;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialsQcService : ServiceBase, ITpoService<RawMaterialQcDto>
    {
        public int Add(RawMaterialQcDto dto)
        {
            var entity = new RawMaterialQC();
            try
            {
                dto.LastModified = DateTime.Now;
                dto.QCVisualInspectionID = 1;
                Mapper.Map(dto, entity);

                _repository.Repository<RawMaterialQC>().Insert(entity);

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

        public List<RawMaterialQcDto> GetAll()
        {
            var dtos = new List<RawMaterialQcDto>();

            var entities = _repository.Repository<RawMaterialQC>().GetAll();

            foreach (var entity in entities)
            {
                var dto = Mapper.Map<RawMaterialQC, RawMaterialQcDto>(entity);
                dtos.Add(dto);
            }

            return dtos;
        }

        public List<RawMaterialQcDto> GetByReceived(int rawMaterialReceivedID) 
        {
            Expression<Func<Data.RawMaterialQC, bool>> filterExpression = p => p.RawMaterialReceivedID == rawMaterialReceivedID;

            var entities = _repository.Repository<Data.RawMaterialQC>().GetAllBy(filterExpression).ToList();
            return Mapper.Map<List<Data.RawMaterialQC>, List<RawMaterialQcDto>>(entities);
        }

        public RawMaterialQcDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterialQC>().GetById(id);
            RawMaterialQcDto dto = null;
            if (entity != null)
            {
                dto = Mapper.Map<RawMaterialQC, RawMaterialQcDto>(entity);
                dto.RawMaterialCode = entity.RawMaterialReceived.RawMaterial.Code;
                dto.LotId = entity.RawMaterialReceived.LotNumber;
                using (RawMaterialTestService svc2 = new RawMaterialTestService())
                {
                    dto.RawMaterialTest = svc2.GetByRawMatId(entity.RawMaterialReceived.RawMaterialID);
                }
            }
            return dto;
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialQC>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);                
                throw;
            }
        }

        public void Update(RawMaterialQcDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<RawMaterialQC>().GetById(dto.Id);
                Mapper.Map(dto, entity);
                _repository.Repository<RawMaterialQC>().Update(entity);

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

        public RawMaterialQcDto CreateNew(int rawMatReceivedID)
        {
            RawMaterialQcDto dto = null;
            var rawMatEntity = _repository.Repository<RawMaterialReceived>().GetById(rawMatReceivedID);
            if (rawMatEntity != null)
            {
                dto = new RawMaterialQcDto
                {
                    RawMaterialReceivedId = rawMatEntity.ID,
                    RawMaterialCode = rawMatEntity.RawMaterial.Code,
                    LotId = rawMatEntity.LotNumber
                };
            }
            return dto;
        }
    }
}
