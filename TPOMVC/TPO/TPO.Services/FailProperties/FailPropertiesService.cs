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

namespace TPO.Services.FailProperties
{
    public class FailPropertiesService : ServiceBase, ITpoService<FailPropertyDto>
    {
        public int Add(FailPropertyDto dto)
        {
            var entity = new FailProperty();
            try
            {
                dto.LastModified = DateTime.Now;

                Mapper.Map(dto, entity);

                _repository.Repository<FailProperty>().Insert(entity);

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

        public List<FailPropertyDto> GetAll()
        {
            var dtos = new List<FailPropertyDto>();

            var entities = _repository.Repository<FailProperty>().GetAll();

            foreach (var entity in entities)
            {
                var dto = Mapper.Map<FailProperty, FailPropertyDto>(entity);
                dtos.Add(dto);
            }

            return dtos;
        }

        public FailPropertyDto Get(int id)
        {
            var entity = _repository.Repository<FailProperty>().GetById(id);
            FailPropertyDto dto = null;
            if (entity != null)
            {
                dto = Mapper.Map<FailProperty, FailPropertyDto>(entity);
            }
            return dto;
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<FailProperty>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(FailPropertyDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<FailProperty>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<FailProperty>().Update(entity);

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
