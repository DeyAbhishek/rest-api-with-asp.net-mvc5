using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.Constants;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Application
{
    public class UoMConversionService : ServiceBase, ITpoService<UoMConversionDto>
    {
        public int Add(UoMConversionDto dto)
        {
            // TODO: Verify this is okay, as UnitOfMeasureConversion table has a composite primary key
           throw new NotImplementedException();
        }

        public List<UoMConversionDto> GetAll()
        {
            var entities = _repository.Repository<UnitOfMeasureConversion>().GetAll().ToList();

            return Mapper.Map<List<UnitOfMeasureConversion>, List<UoMConversionDto>>(entities);
        }

        public UoMConversionDto Get(int id)
        {
            return Map(GetById(id));
        }

        private UoMConversionDto Map(UnitOfMeasureConversion entity)
        {
            return Mapper.Map<UnitOfMeasureConversion, UoMConversionDto>(entity);
        }

        private UnitOfMeasureConversion GetById(int id)
        {
            return _repository.Repository<UnitOfMeasureConversion>().GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Repository<UnitOfMeasureConversion>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public void Update(UoMConversionDto dto)
        {
            try
            {
                var entity = _repository.Repository<UnitOfMeasureConversion>().GetAllBy(c => c.UoMID1 == dto.SourceUnitOfMeasureId && c.UoMID2 == dto.TargetUnitOfMeasureId).FirstOrDefault();
                Mapper.Map(dto, entity);
                _repository.Repository<UnitOfMeasureConversion>().Update(entity);
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

        private decimal GetConversionRateByIds(int sourceUnitOfMeasureId, int targetUnitOfMesaureId)
        {
            decimal conversionRate = 1;
            if (sourceUnitOfMeasureId != targetUnitOfMesaureId)
            {
                Expression<Func<UnitOfMeasureConversion, bool>> filter =
                    ra => ra.UoMID1 == sourceUnitOfMeasureId && ra.UoMID2 == targetUnitOfMesaureId;
                var conversion = _repository.Repository<UnitOfMeasureConversion>().GetAllBy(filter).ToList();
                if (conversion.Count > 0) 
                {
                    var rate = conversion.Select(ra => ra.ConversionRate).First().ToString(CultureInfo.InvariantCulture);
                if (!decimal.TryParse(rate, out conversionRate))
                {
                    conversionRate = 0;
                }
            }
            }
            return conversionRate;
        }

        public decimal ConvertUoM(int sourceUnitOfMeasureId, decimal sourceValue, int targetUnitOfMeasureId)
        {
            decimal conversionRate = GetConversionRateByIds(sourceUnitOfMeasureId, targetUnitOfMeasureId);
            return sourceValue * conversionRate;
        }
    }
}
