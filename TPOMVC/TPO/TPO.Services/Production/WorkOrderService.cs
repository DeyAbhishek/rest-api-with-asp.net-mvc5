using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;
using System.Data.Entity.Validation;
using AutoMapper;

namespace TPO.Services.Production
{
    public class WorkOrderService : ServiceBase, ITpoService<WorkOrderDto>
    {
        public int Add(WorkOrderDto dto)
        {
            var entity = Mapper.Map<WorkOrderDto, WorkOrder>(dto);
            try
            {
                _repository.Repository<WorkOrder>().Insert(entity);
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

        public List<WorkOrderDto> GetAll()
        {
            var entities = _repository.Repository<WorkOrder>().GetAll().ToList();
            return Mapper.Map<List<WorkOrder>, List<WorkOrderDto>>(entities);
        }

        public List<WorkOrderDto> GetByLineID(int lineID, bool viewCompleted = false) 
        {
            var entities = _repository.Repository<WorkOrder>().GetAllBy(w => w.LineID == lineID).ToList();
            if (!viewCompleted) 
            {
                entities = entities.Where(w => w.Complete == false).ToList();
            }
            return Mapper.Map<List<WorkOrder>, List<WorkOrderDto>>(entities.OrderBy(wo => wo.RunOrder).ToList());
        }

        public WorkOrderDto Get(int id)
        {
            var entity = _repository.Repository<WorkOrder>().GetById(id);
            return Mapper.Map<WorkOrder, WorkOrderDto>(entity);
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _repository.Repository<WorkOrder>().GetById(id);
                var workOrders = _repository.Repository<WorkOrder>().GetAllBy(wo => wo.LineID == entity.LineID && wo.RunOrder > entity.RunOrder).ToList();
                foreach (var wo in workOrders) 
                {
                    wo.RunOrder--;
                    _repository.Repository<WorkOrder>().Update(wo);
                }
                _repository.Repository<WorkOrder>().Delete(id);
                _repository.Save();
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Update(WorkOrderDto dto)
        {
            try
            {
                var entity = _repository.Repository<WorkOrder>().GetById(dto.ID);
                Mapper.Map(dto, entity);
                _repository.Repository<WorkOrder>().Update(entity);
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

        public void Reorder(int workOrderID, int newOrder) 
        {
            var workOrder = _repository.Repository<WorkOrder>().GetById(workOrderID);
            var originalOrder = workOrder.RunOrder;
            workOrder.RunOrder = newOrder;
            var otherWorkOrder = _repository.Repository<WorkOrder>().GetAllBy(wo => wo.LineID == workOrder.LineID && wo.ID != workOrderID && wo.RunOrder == newOrder).FirstOrDefault();

            if (otherWorkOrder != null) 
            {
                otherWorkOrder.RunOrder = originalOrder;
                _repository.Repository<WorkOrder>().Update(otherWorkOrder);
            }

            _repository.Repository<WorkOrder>().Update(workOrder);
            _repository.Save();
        }

        public int GetWorkOrderCount(int lineID)
        {
            return _repository.Repository<WorkOrder>().GetAllBy(wo => wo.LineID == lineID).Count();
        }

        public double GetCompletionPercentage(int plantID, int lineID, int workOrderID) 
        {
            double returnValue = 0.0;

            WorkOrder workOrder = _repository.Repository<WorkOrder>().GetById(workOrderID);

            ProdLine line = _repository.Repository<ProdLine>().GetById(lineID);
            ProdLineType lineType =  _repository.Repository<ProdLineType>().GetById(line.LineTypeID);

            double ran = 0;
            if ( lineType.ProdLineTypeCode == "IM" )
            {
                ran = 0;
                /*
                    _repository.Repository<IMProd>().GetAllBy(
                        p => p.ProdLineID == lineID && 
                                p.WorkOrderID == workOrder.ID && 
                                p.IMProductID == workOrder.IMProductID && 
                                p.PlantID == plantID).Sum(p => p.PartsCarton);
                 */
            }
            else
            {
                ran = 0;
                /*
                ran = _repository.Repository<TPOCProductRoll>().GetAllBy(
                        p => p.LineID == lineID &&
                                p.WorkOrderID == workOrder.ID &&
                                p.ProductID == workOrder.TPOProductID &&
                                p.PlantID == plantID).Sum(
                                    p =>  (p.Length * p.TPOProduct.Width) == null ? 0 : (p.Length * p.TPOProduct.Width)
                                );
                */
            }

            if ( workOrder.RunArea > 0 )
            {
                returnValue = ( ran / workOrder.RunArea );
            }
            
            return returnValue;
        }
    }
}
