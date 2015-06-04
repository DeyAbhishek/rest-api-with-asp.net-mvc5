using System;
using System.Collections.Generic;
using System.Linq;
using TPO.Data;
using TPO.Services.Core;
using TPO.Common.DTOs;
using AutoMapper;

namespace TPO.Services.Production
{
    public class ProdLinesPerformProdService : ServiceBase, ITpoService<ProdLinesPerformProdDto>
    {
        public int Add(ProdLinesPerformProdDto dto)
        {
            int key = AddToUnitOfWork(dto);
            CommitUnitOfWork();

            return key;
        }

        public int AddToUnitOfWork(ProdLinesPerformProdDto dto)
        {
            int key = -1;

            ProdLinesPerformProd entity = UpdateRepository(dto);
            if (entity != null)
                key = entity.ID;

            return key;
        }

        private ProdLinesPerformProd UpdateRepository(ProdLinesPerformProdDto dto)
        {
            ProdLinesPerformProd entity = null;
            try
            {
                if (dto.ID == 0)
                {
                    entity = new ProdLinesPerformProd();
                    Mapper.Map(dto, entity);
                    _repository.Repository<ProdLinesPerformProd>().Insert(entity);
                }
                else
                {
                    entity = GetById(dto.ID);
                    Mapper.Map(dto, entity);
                    _repository.Repository<ProdLinesPerformProd>().Update(entity);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity;
        }

        public List<ProdLinesPerformProdDto> GetAll()
        {
            return MapEntityList(_repository.Repository<ProdLinesPerformProd>().GetAll());
        }

        private List<ProdLinesPerformProdDto> MapEntityList(IEnumerable<ProdLinesPerformProd> entities)
        {
            return Mapper.Map<List<ProdLinesPerformProd>, List<ProdLinesPerformProdDto>>(entities.ToList());
        }

        private ProdLinesPerformProdDto MapEntity(ProdLinesPerformProd entity)
        {
            return Mapper.Map<ProdLinesPerformProd, ProdLinesPerformProdDto>(entity);
        }

        public ProdLinesPerformProdDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        private ProdLinesPerformProd GetById(int id)
        {
            return _repository.Repository<ProdLinesPerformProd>().GetById(id);
        }

        public List<ProdLinesPerformProdDto> GetByProdLineId(int prodLineId)
        {
            return
                MapEntityList(_repository.Repository<ProdLinesPerformProd>().GetAllBy(p => p.ProdLineID == prodLineId));
        }

        public void Delete(int id)
        {
            DeleteInUnitOfWork(id);
            CommitUnitOfWork();
        }

        public void DeleteInUnitOfWork(int id)
        {
            _repository.Repository<ProdLinesPerformProd>().Delete(GetById(id));
        }

        public void UpdateUnitOfWork(ProdLinesPerformProdDto dto)
        {
            UpdateRepository(dto);
        }

        public void Update(ProdLinesPerformProdDto dto)
        {
            UpdateUnitOfWork(dto);
            CommitUnitOfWork();
        }
    }
}
