using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Services.Core;
using TPO.Common.DTOs;
using TPO.Data;
using AutoMapper;
using System.Data.Entity.Validation;

namespace TPO.Services.Downtime
{
    public class DownTimeTypeService : ServiceBase, ITpoService<DownTimeTypeDto>
    {
        public int Add(DownTimeTypeDto dto)
        {
            DownTimeType entity = Mapper.Map<DownTimeTypeDto, DownTimeType>(dto);

            try
            {
                _repository.Repository<DownTimeType>().Insert(entity);
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

        public List<DownTimeTypeDto> GetAll()
        {
            var entities = _repository.Repository<DownTimeType>().GetAll().ToList();
            return Mapper.Map<List<DownTimeType>, List<DownTimeTypeDto>>(entities);
        }

        public DownTimeTypeDto Get(int id)
        {
            var entity = _repository.Repository<DownTimeType>().GetById(id);
            return Mapper.Map<DownTimeType, DownTimeTypeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<DownTimeType>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(DownTimeTypeDto dto)
        {
            try
            {
                var entity = _repository.Repository<DownTimeType>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<DownTimeType>().Update(entity);
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
