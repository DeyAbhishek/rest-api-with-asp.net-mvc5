using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Formulation
{
    public class TPOFormulationRawMaterialService : ServiceBase, ITpoService<TPOFormulationRawMaterialDto>
    {
        public int Add(TPOFormulationRawMaterialDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = Mapper.Map<TPOFormulationRawMaterialDto, TPOFormulationRawMaterial>(dto);
            try
            {
                _repository.Repository<TPOFormulationRawMaterial>().Insert(entity);
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

        public List<TPOFormulationRawMaterialDto> GetAll()
        {
            var entities = _repository.Repository<TPOFormulationRawMaterial>().GetAll().ToList();
            return Mapper.Map<List<TPOFormulationRawMaterial>, List<TPOFormulationRawMaterialDto>>(entities);
        }

        public TPOFormulationRawMaterialDto Get(int id)
        {
            var entity = _repository.Repository<TPOFormulationRawMaterial>().GetById(id);
            return Mapper.Map<TPOFormulationRawMaterial, TPOFormulationRawMaterialDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOFormulationRawMaterial>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOFormulationRawMaterialDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOFormulationRawMaterial>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOFormulationRawMaterial>().Update(entity);
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
