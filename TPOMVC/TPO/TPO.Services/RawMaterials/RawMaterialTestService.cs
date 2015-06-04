using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialTestService : ServiceBase, ITpoService<RawMaterialTestDto>
    {
        public int Add(RawMaterialTestDto dto)
        {
            var entity = new RawMaterialTest();
            try
            {
                dto.LastModified = DateTime.Now;
                

                Mapper.Map(dto, entity);

                _repository.Repository<RawMaterialTest>().Insert(entity);

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

        public List<RawMaterialTestDto> GetAll()
        {
            var entities = _repository.Repository<RawMaterialTest>().GetAll().ToList();
            return Mapper.Map<List<RawMaterialTest>, List<RawMaterialTestDto>>(entities);
        }

        public RawMaterialTestDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterialTest>().GetById(id);
            return Mapper.Map<RawMaterialTest, RawMaterialTestDto>(entity);
        }

        public RawMaterialTestDto GetByRawMatId(int id)
        {
            Expression<Func<RawMaterialTest, bool>> filter = t => t.RawMaterialID == id;
            var entity = _repository.Repository<RawMaterialTest>().GetAllBy(filter).ToList().FirstOrDefault();
            return Mapper.Map<RawMaterialTest, RawMaterialTestDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterialTest>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialTestDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = _repository.Repository<RawMaterialTest>().GetById(dto.Id);
            Mapper.Map(dto, entity);

            try
            {
                _repository.Repository<RawMaterialTest>().Update(entity);
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
