using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;

namespace TPO.Services.Production
{
    public class ProdLinesPerformService : ServiceBase, ITpoService<ProdLinesPerformDto>
    {
        public int Add(ProdLinesPerformDto dto)
        {
            var entity = Mapper.Map<ProdLinesPerformDto, ProdLinesPerform>(dto);
            try
            {
                var prodLine = _repository.Repository<ProdLine>().GetById(dto.ProdLineID);
                var typeCode = prodLine.ProdLineType.ProdLineTypeCode;
                if (typeCode == "TPO" || typeCode == "RW" || typeCode == "WI" || typeCode == "CO") 
                {
                    //Create ProdLinesPerform records for REPEL and GEOREPEL TPOProducts
                    var products = _repository.Repository<TPOProduct>().GetAllBy(p => p.ProductCode == "REPEL" || p.ProductCode == "GEOREPEL").ToList();
                    for (int i = 0; i < products.Count; i++) 
                    {
                        ProdLinesPerformProd perform = new ProdLinesPerformProd();
                        perform.ProdLineID = prodLine.ID;
                        perform.ProductID = products[i].ID;
                        perform.Throughput = entity.Throughput;
                        perform.LocID = entity.LocID;
                        perform.DateChange = DateTime.Now;
                        _repository.Repository<ProdLinesPerformProd>().Insert(perform);
                    }
                }
                _repository.Repository<ProdLinesPerform>().Insert(entity);
                _repository.Save();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException valEx)
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

        public List<ProdLinesPerformDto> GetAll()
        {
            return MapEntityList(_repository.Repository<ProdLinesPerform>().GetAll());
        }

        private List<ProdLinesPerformDto> MapEntityList(IEnumerable<ProdLinesPerform> entities)
        {
            return Mapper.Map<List<ProdLinesPerform>, List<ProdLinesPerformDto>>(entities.ToList());
        }

        private ProdLinesPerformDto MapEntity(ProdLinesPerform entity)
        {
            return Mapper.Map<ProdLinesPerform, ProdLinesPerformDto>(entity);
        }

        public ProdLinesPerformDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private ProdLinesPerform GetById(int id)
        {
            return _repository.Repository<ProdLinesPerform>().GetById(id);
        }

        public ProdLinesPerformDto GetByLineId(int lineId)
        {
            return
                MapEntity(
                    _repository.Repository<ProdLinesPerform>().GetAllBy(p => p.ProdLineID == lineId).FirstOrDefault());
        }

        public void Delete(int id)
        {
            DeleteInUnitOfWork(id);
            CommitUnitOfWork();
        }

        public void DeleteInUnitOfWork(int id)
        {
            _repository.Repository<ProdLinesPerform>().Delete(GetById(id));
        }

        public void Update(ProdLinesPerformDto dto)
        {
            var entity = _repository.Repository<ProdLinesPerform>().GetById(dto.ID);
            try
            {
                Mapper.Map(dto, entity);
                _repository.Repository<ProdLinesPerform>().Update(entity);
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
