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
    public class TPOFormulationService : ServiceBase, ITpoService<TPOFormulationDto>
    {
        public int Add(TPOFormulationDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = Mapper.Map<TPOFormulationDto, TPOFormulation>(dto);
            try
            {
                _repository.Repository<TPOFormulation>().Insert(entity);
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

        public List<TPOFormulationDto> GetAll()
        {
            var entities = _repository.Repository<TPOFormulation>().GetAll().ToList();
            return Mapper.Map<List<TPOFormulation>, List<TPOFormulationDto>>(entities);
        }

        public TPOFormulationDto Get(int id)
        {
            var entity = _repository.Repository<TPOFormulation>().GetById(id);
            return Mapper.Map<TPOFormulation, TPOFormulationDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOFormulation>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOFormulationDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<TPOFormulation>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOFormulation>().Update(entity);
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
