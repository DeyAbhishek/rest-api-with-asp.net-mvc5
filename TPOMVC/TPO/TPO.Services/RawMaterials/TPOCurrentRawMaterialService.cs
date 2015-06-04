using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class TPOCurrentRawMaterialService : ServiceBase, ITpoService<TPOCurrentRawMaterialDto>
    {
        public int Add(TPOCurrentRawMaterialDto dto)
        {
            var entity = new TPOCurrentRawMaterial();
            try
            {
                dto.DateEntered = DateTime.Now;
                dto.EnteredBy = CurrentUserName;
                dto.LastModified = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                Mapper.Map(dto, entity);
                _repository.Repository<TPOCurrentRawMaterial>().Insert(entity);
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

        public List<TPOCurrentRawMaterialDto> GetAll()
        {
            var entities = _repository.Repository<TPOCurrentRawMaterial>().GetAll().ToList();
            return Mapper.Map<List<TPOCurrentRawMaterial>, List<TPOCurrentRawMaterialDto>>(entities);
        }

        public TPOCurrentRawMaterialDto Get(int id)
        {
            var entity = _repository.Repository<TPOCurrentRawMaterial>().GetById(id);
            return Mapper.Map<TPOCurrentRawMaterial, TPOCurrentRawMaterialDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOCurrentRawMaterial>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOCurrentRawMaterialDto dto)
        {
            try
            {
                dto.DateEntered = DateTime.Now;
                dto.EnteredBy = CurrentUserName;
                dto.LastModified = DateTime.Now;
                dto.ModifiedBy = CurrentUserName;
                var entity = _repository.Repository<TPOCurrentRawMaterial>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<TPOCurrentRawMaterial>().Update(entity);

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

        public List<TPOCurrentRawMaterialDto> GetAllByLineID(int lineId)
        {
            var entities = _repository.Repository<TPOCurrentRawMaterial>().GetAllBy(c => c.LineID == lineId).ToList();
            return Mapper.Map<List<TPOCurrentRawMaterial>, List<TPOCurrentRawMaterialDto>>(entities);
        }
    }
}
