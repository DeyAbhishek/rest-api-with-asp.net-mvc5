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
    public class TPOExtruderService : ServiceBase, ITpoService<TPOExtruderDto>
    {
        public int Add(TPOExtruderDto dto)
        {
            var entity = Mapper.Map<TPOExtruderDto, TPOExtruder>(dto);
            try
            {
                _repository.Repository<TPOExtruder>().Insert(entity);
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

        public List<TPOExtruderDto> GetAll()
        {
            var entities = _repository.Repository<TPOExtruder>().GetAll().ToList();
            return Mapper.Map<List<TPOExtruder>, List<TPOExtruderDto>>(entities);
        }

        public TPOExtruderDto Get(int id)
        {
            var entity = _repository.Repository<TPOExtruder>().GetById(id);
            return Mapper.Map<TPOExtruder, TPOExtruderDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOExtruder>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOExtruderDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOExtruder>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<TPOExtruder>().Update(entity);
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
