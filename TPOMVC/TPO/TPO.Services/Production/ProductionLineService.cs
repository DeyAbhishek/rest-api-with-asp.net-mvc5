using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using System.Data.Entity.Validation;

namespace TPO.Services.Production
{
    public class ProductionLineService : ServiceBase, ITpoService<ProductionLinesDto>
    {
        public int Add(ProductionLinesDto dto)
        {
            var entity = Mapper.Map<ProductionLinesDto, ProdLine>(dto);


            //TODO:  Set fields on ProdLinesPerform based on values in ProdLine
            //ProdLinesPerform plp = new ProdLinesPerform();
            //plp.LocID = entity.LocID;

            //TODO:  Handle setting current work order to Line based on its type (see usp_WO_UpdateRCRWWorkOrders in old DB for reference)

            #region Line Type Checks
            //Get the type of production line
            ProdLineType type = _repository.Repository<ProdLineType>().GetById(entity.LineTypeID);
            if (type != null)
            {
                //If type is not reclaim or rework, make sure a WOLineUse record is present
                if (type.ProdLineTypeCode != "RW" && type.ProdLineTypeCode != "RC")
                {
                    //TODO:  Once WOLineUse table has been created, create a new instance of that Entity here and assign to newly created ProdLine
                }

                if (type.ProdLineTypeCode == "TPO" || type.ProdLineTypeCode == "RW" || type.ProdLineTypeCode == "WI" || type.ProdLineTypeCode == "CO")
                {


                    //Create TPOCurrentScrim record
                    if (type.ProdLineTypeCode == "TPO" || type.ProdLineTypeCode == "CO")
                    {
                        TPOCurrentScrim currScrim = new TPOCurrentScrim();
                        entity.TPOCurrentScrims.Add(currScrim);
                        currScrim.PlantID = entity.PlantID;
                        currScrim.DateEntered = entity.DateEntered;
                        currScrim.EnteredBy = entity.EnteredBy;
                        currScrim.LastModified = entity.LastModified;
                        currScrim.ModifiedBy = entity.ModifiedBy;
                        currScrim.ScrimPos = "NA";

                        if (type.ProdLineTypeCode == "TPO")
                        {
                            //TODO:  Create TPOFormLineProd for this line for all products for the 
                            //current plant in TPOProducts where IsRepel is false
                            //See usp_SA_LineProdFormCheck in old database
                        }

                        //TODO:  Once TPOCurrentBatch table has been created, create a new instance of that Entity here
                    }
                }

            }
            #endregion

            //Create ProductionShiftUse records for each available shift
            var prodShifts = _repository.Repository<ProductionShiftDto>().GetAllBy(s => s.PlantID == entity.PlantID).ToList();
            for (int i = 0; i < prodShifts.Count; i++)
            {
                var minutes = 0;
                if (prodShifts[i].StartTime > prodShifts[i].EndTime)
                {
                    minutes = prodShifts[i].StartTime.Subtract(prodShifts[i].EndTime).Minutes + 1440;
                }
                else
                {
                    minutes = minutes = prodShifts[i].StartTime.Subtract(prodShifts[i].EndTime).Minutes;
                }
                ProductionShiftUse use = new ProductionShiftUse()
                {
                    ShiftID = prodShifts[i].ID,
                    PlantID = entity.PlantID,
                    Day1Minutes = minutes,
                    Day2Minutes = minutes,
                    Day3Minutes = minutes,
                    Day4Minutes = minutes,
                    Day5Minutes = minutes,
                    Day6Minutes = minutes,
                    Day7Minutes = minutes,
                    
                    DateEntered = entity.DateEntered,
                    EnteredBy = entity.EnteredBy,
                    LastModified = entity.LastModified,
                    ModifiedBy = entity.ModifiedBy
                };
                entity.ProductionShiftUses.Add(use);

                //Need to create config line now since FK is non-nullable
                ProdLineRollConfig config = new ProdLineRollConfig()
                {
                    RollName = type.ProdLineTypeCode,
                    TypeID = type.ID,
                    Order = 0
                };
                entity.ProdLineRollConfig = config;
            }

            //TODO:  Create new ProdDteChng record
            ProdDateChange change = new ProdDateChange()
            {
                DateChange = DateTime.Now,
                RotationStart = DateTime.Now,
                PlantID = entity.PlantID,
                DateEntered = entity.DateEntered,
                EnteredBy = entity.EnteredBy,
                LastModified = entity.LastModified,
                ModifiedBy = entity.ModifiedBy

            };
            entity.ProdDateChanges.Add(change);


            try
            {
                _repository.Repository<ProdLine>().Insert(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity.ID;
        }

        public List<ProductionLinesDto> GetAll()
        {
            var entities = _repository.Repository<ProdLine>().GetAll().ToList();
            return Mapper.Map<List<ProdLine>, List<ProductionLinesDto>>(entities);
        }

        public List<ProductionLinesDto> GetByPlant(int plantID) 
        {
            var entities = _repository.Repository<ProdLine>().GetAllBy(p => p.PlantID == plantID).ToList();
            return Mapper.Map<List<ProdLine>, List<ProductionLinesDto>>(entities);
        }

        public List<ProductionLinesDto> GetByType(int typeID) 
        {
            var entities = _repository.Repository<ProdLine>().GetAllBy(l => l.LineTypeID == typeID).ToList();
            return Mapper.Map<List<ProdLine>, List<ProductionLinesDto>>(entities);
        }

        public ProductionLinesDto Get(int id)
        {
            var entity = _repository.Repository<ProdLine>().GetById(id);
            return Mapper.Map<ProdLine, ProductionLinesDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Repository<ProdLine>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(ProductionLinesDto dto)
        {
            try
            {
                var entity = _repository.Repository<ProdLine>().GetById(dto.ID);
                entity = Mapper.Map(dto, entity);
                _repository.Repository<ProdLine>().Update(entity);
                _repository.Save();
            }
            catch (DbEntityValidationException valEx)
            {
                var sb = new StringBuilder();

                foreach (var failure in valEx.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), valEx
                    ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Reorder(ProductionLinesDto dto, int newOrder, int originalOrder)
        {

            var prodLine = _repository.Repository<Data.ProdLine>().GetAllBy(p => p.PlantID == dto.PlantID).ToList();
            foreach (var v in prodLine)
            {
                if (v.RepOrder == originalOrder) v.RepOrder = -1;
                _repository.Repository<ProdLine>().Update(v);
            }
            var changedProdLine = _repository.Repository<Data.ProdLine>().GetAllBy(p => p.PlantID == dto.PlantID).ToList();

            foreach (var q in changedProdLine)
            {
                if (newOrder > originalOrder)
                {

                    if ((q.RepOrder > originalOrder) && (q.RepOrder <= newOrder))
                    {
                        q.RepOrder = q.RepOrder - 1;
                    }
                }
                else
                {
                    if ((q.RepOrder < originalOrder) && (q.RepOrder >= newOrder))
                    {
                        q.RepOrder = q.RepOrder + 1;
                        _repository.Repository<ProdLine>().Update(q);
                    }
                }
                if (q.RepOrder == -1) q.RepOrder = newOrder;
                _repository.Repository<ProdLine>().Update(q);
            }
            try
            {
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
