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


namespace TPO.Services.Production
{
    public class ProdDateChangeService : ServiceBase, ITpoService<ProdDateChangeDto>
    {
        public int Add(ProdDateChangeDto dto)
        {
            dto.LastModified = DateTime.Now;
            var entity = Mapper.Map<ProdDateChangeDto, ProdDateChange>(dto);
            try
            {
                _repository.Repository<ProdDateChange>().Insert(entity);
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

        public List<ProdDateChangeDto> GetAll()
        {
            var entities = _repository.Repository<ProdDateChange>().GetAll().ToList();
            return Mapper.Map<List<ProdDateChange>, List<ProdDateChangeDto>>(entities);
        }

        public ProdDateChangeDto Get(int id)
        {
            var entity = _repository.Repository<ProdDateChange>().GetById(id);
            return Mapper.Map<ProdDateChange, ProdDateChangeDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<ProdDateChange>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(ProdDateChangeDto dto)
        {
            try
            {
                dto.LastModified = DateTime.Now;
                var entity = _repository.Repository<ProdDateChange>().GetById(dto.ID);
                if(dto.ShiftTypeID==1)
                {
                    dto.DateChange = entity.DateChange;
                    dto.RotationStart = entity.RotationStart;
                }
                else if (dto.ShiftTypeID == 2 || dto.ShiftTypeID == 3)
                {
                    dto.RotationStart = entity.RotationStart;
                }
                entity = Mapper.Map(dto, entity);
                _repository.Repository<ProdDateChange>().Update(entity);
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

        public ProdDateChangeDto GetCurrentProductionDate(int lineID) 
        {
            var entity = _repository.Repository<ProdDateChange>().GetAllBy(pdc => pdc.LineID == lineID).FirstOrDefault();
            var dto = Mapper.Map<ProdDateChange, ProdDateChangeDto>(entity);
            if (dto != null) 
            {
                if (entity.ProductionShiftType.Code == "0")
                {
                    dto.CurrentProductionDate = DateTime.Today;
                }
                else 
                {
                    if (entity.ProductionShiftType.Code == "1")
                    {
                        if (DateTime.Now.TimeOfDay >= dto.DateChange.TimeOfDay)
                        {
                            dto.CurrentProductionDate = DateTime.Today.AddDays(1);
                        }
                        else
                        {
                            dto.CurrentProductionDate = DateTime.Today;
                        }
                    }
                    else 
                    {
                        if (DateTime.Now.TimeOfDay >= dto.DateChange.TimeOfDay)
                        {
                            dto.CurrentProductionDate = DateTime.Today;
                        }
                        else
                        {
                            dto.CurrentProductionDate = DateTime.Today.AddDays(-1);
                        }
                    }
                }
            }
            return dto;
        }
    }
}
