using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Services.Core;
using TPO.Data;

namespace TPO.Services.RawMaterials
{
    public class RawMaterialService: ServiceBase, ITpoService<RawMaterialDto>
    {
        public int Add(RawMaterialDto dto)
        {
            var entity = new RawMaterial();
            try
            {
                entity = AddToRepository(dto);
                CommitUnitOfWork();
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

        public RawMaterial AddToRepository(RawMaterialDto dto)
        {
            var entity = new RawMaterial();
            try
            {
                
                dto.LastModified = DateTime.Now;

                Mapper.Map(dto, entity);

                _repository.Repository<RawMaterial>().Insert(entity);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entity;
        }

        public List<RawMaterialDto> GetAll()
        {
            var entities = _repository.Repository<Data.RawMaterial>().GetAll();
            if (entities != null)
            {
                return AutoMapper.Mapper.Map<List<Data.RawMaterial>, List<RawMaterialDto>>(entities.ToList());
            }
            return new List<RawMaterialDto>();
        }

        public List<RawMaterialDto> GetAllByPlantId(int plantId)
        {
            Expression<Func<Data.RawMaterial, bool>> filterExpression = p => p.PlantID == plantId;

            var entities = _repository.Repository<Data.RawMaterial>().GetAllBy(filterExpression).ToList();

            return AutoMapper.Mapper.Map<List<Data.RawMaterial>, List<RawMaterialDto>>(entities);
        }

        public RawMaterialDto Get(int id)
        {
            var entity = _repository.Repository<RawMaterial>().GetById(id);
            return Mapper.Map<RawMaterial, RawMaterialDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<RawMaterial>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(RawMaterialDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = _repository.Repository<RawMaterial>().GetById(dto.Id);
            Mapper.Map(dto, entity);
            _repository.Repository<RawMaterial>().Update(entity);
            CommitUnitOfWork();
        }


    }
}
