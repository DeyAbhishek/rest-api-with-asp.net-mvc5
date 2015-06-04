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

namespace TPO.Services.EnterLineScrap
{
   //public class EnterLineScrapService : ServiceBase, ITpoService<TPOLineScrapDto>
   // {

   //     //public int Add(TPOLineScrapDto dto)
   //     //{
   //     //    var entity = new TPOLineScrap();
   //     //    try
   //     //    {
   //     //        Mapper.Map(dto, entity);
   //     //        _repository.Repository<TPOLineScrap>().Insert(entity);
   //     //        _repository.Save();
   //     //    }
   //     //    catch (DbEntityValidationException valEx)
   //     //    {
   //     //        HandleValidationException(valEx);
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogException(ex);
   //     //        throw;
   //     //    }
   //     //    return entity.ID;
   //     //}


   //     //public List<TPOLineScrapDto> GetAll()
   //     //{
   //     //    var entities = _repository.Repository<TPOLineScrap>().GetAll().ToList();
   //     //    return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
   //     //}

   //     //public List<TPOLineScrapDto> GetByShift(int shiftID) 
   //     //{
   //     //    var entities = _repository.Repository<TPOLineScrap>().GetAllBy(r => r.ShiftID == shiftID).ToList();
   //     //    return Mapper.Map<List<TPOLineScrap>, List<TPOLineScrapDto>>(entities);
   //     //}

   //     //public TPOLineScrapDto Get(int id)
   //     //{
   //     //    var entity = _repository.Repository<TPOLineScrap>().GetById(id);
   //     //    return Mapper.Map<TPOLineScrap, TPOLineScrapDto>(entity);
   //     //}

   //     //public TPOLineScrapDto GetByCode(string code) 
   //     //{
   //     //    var entity = _repository.Repository<TPOLineScrap>().GetAllBy(r => r.Code == code).FirstOrDefault();
   //     //    return Mapper.Map<TPOLineScrap, TPOLineScrapDto>(entity);
   //     //}

   //     //public void Delete(int id)
   //     //{
   //     //    throw new NotImplementedException();
   //     //}


   //     //public void Update(TPOLineScrapDto dto)
   //     //{
   //     //    var entity = _repository.Repository<TPOLineScrap>().GetById(dto.ID);
   //     //    try
   //     //    {
   //     //        Mapper.Map(dto, entity);
   //     //        _repository.Repository<TPOLineScrap>().Update(entity);
   //     //        _repository.Save();
   //     //    }
   //     //    catch (DbEntityValidationException valEx)
   //     //    {
   //     //        HandleValidationException(valEx);
   //     //    }
   //     //    catch (Exception ex)
   //     //    {
   //     //        LogException(ex);
   //     //        throw;
   //     //    }

   //     //}


   // }
}
