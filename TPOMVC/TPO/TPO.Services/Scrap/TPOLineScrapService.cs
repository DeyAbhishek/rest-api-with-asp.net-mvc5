using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Scrap
{
    public class TPOLineScrapService : ServiceBase, ITpoService<TPOLineScrapDto>
    {
        public int Add(TPOLineScrapDto dto)
        {
            TPOLineScrap entity = Mapper.Map<TPOLineScrapDto, TPOLineScrap>(dto);
            try
            {
                _repository.Repository<TPOLineScrap>().Insert(entity);
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

        public List<TPOLineScrapDto> GetAll()
        {
            var entities = _repository.Repository<TPOLineScrap>().GetAll().ToList();
            return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
        }

        //public List<TPOLineScrapDto> GetByCodeGroup(int id)
        //{
        //    Expression<Func<TPOLineScrap, bool>> filterExpression = p => p.GroupID == id;
        //    var entities = _repository.Repository<TPOLineScrap>().GetAllBy(filterExpression).ToList();
        //    return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
        //}

        public TPOLineScrapDto Get(int id)
        {
            var entity = _repository.Repository<TPOLineScrap>().GetById(id);
            return Mapper.Map<TPOLineScrap, TPOLineScrapDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<TPOLineScrap>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(TPOLineScrapDto dto)
        {
            try
            {
                var entity = _repository.Repository<TPOLineScrap>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<TPOLineScrap>().Update(entity);
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

        public List<TPOLineScrapDto> GetByShift(int shiftID, DateTime productionDate) 
        {
            var entities = _repository.Repository<TPOLineScrap>().GetAllBy(s => s.ShiftID == shiftID && s.ProductionDate == productionDate).ToList();
            return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
        }
        public TPOLineScrapDto GetByCode(string code)
        {
            var entity = _repository.Repository<TPOLineScrap>().GetAllBy(r => r.Code == code).FirstOrDefault();
            return Mapper.Map<TPOLineScrap, TPOLineScrapDto>(entity);
        }
        public List<TPOLineScrapDto> GetByReworkRollID(int rwRollID) 
        {
            var entities = _repository.Repository<TPOLineScrap>().GetAllBy(s => s.ReworkRollID == rwRollID).ToList();
            return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
        }

        public string GetNewScrapCode(int lineID, DateTime productionDate) 
        {
            var newCode = string.Empty;
            var lineEntity = _repository.Repository<ProdLine>().GetById(lineID);
            if (lineEntity != null) 
            {
                switch (lineEntity.ProdLineType.ProdLineTypeCode) 
                {
                    case "IM": 
                        {
                            newCode = GetScrapCodeIM(lineEntity, productionDate);
                        } break;
                    case "TPO": 
                        {
                            newCode = GetScrapCodeTPO(lineEntity, productionDate);
                        } break;
                    default: 
                        {
                            newCode = GetScrapCodeTPO(lineEntity, productionDate);
                        } break;
                }
            }
            return newCode;
        }

        private string GetScrapCodeTPO(ProdLine prodLine, DateTime productionDate) 
        {
            string newCode = string.Empty;

            var labelCode = string.Format("S{0}{1}{2}",
                prodLine.LabelID.ToString("00"),
                (productionDate.Year % 100).ToString("00"),
                productionDate.DayOfYear.ToString("000"));
            var lastEntity = _repository.Repository<TPOLineScrap>().GetAllBy(s => s.ProdLinesID == prodLine.ID && s.Code.StartsWith(labelCode)).OrderByDescending(s => s.ID).FirstOrDefault();
            int max = 0;
            if (lastEntity != null) 
            {
                int.TryParse(lastEntity.Code.Substring(lastEntity.Code.Length - 4), out max);
            }
            newCode = string.Format("{0}{1}", labelCode, max.ToString().PadLeft(3, '0'));
            return newCode;
        }

        private string GetScrapCodeIM(ProdLine prodLine, DateTime productionDate) 
        {
            string newCode = string.Empty;
            
            var labelCode = string.Format("S{0}{1}{2}",
                prodLine.LabelID.ToString("00"),
                (productionDate.Year % 100).ToString("00"),
                productionDate.DayOfYear.ToString("000"));

            return newCode;
        }


    }
}
