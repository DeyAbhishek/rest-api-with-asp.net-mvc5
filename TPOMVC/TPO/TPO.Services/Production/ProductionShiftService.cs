using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class ProductionShiftService : ServiceBaseGeneric<ProductionShiftDto, ProductionShift>, ITpoService<ProductionShiftDto>
    {
        public ProductionShiftDto GetCurrentProductionShift(int lineID) 
        {
            ProductionShiftDto dto = null;
            ProdDateChangeService dateSvc = new ProdDateChangeService();
            ProdDateChangeDto dateChange = dateSvc.GetCurrentProductionDate(lineID);
            var currentTime = DateTime.Now.TimeOfDay;
            var shiftType = _repository.Repository<ProductionShiftType>().GetById(dateChange.ShiftTypeID);
            switch (shiftType.Code) 
            {
                case "0":
                case "1":
                case "2": 
                    {
                        dto = GetProductionShiftForTypes012(currentTime, lineID);
                    }break;
                case "3": 
                    {
                        dto = GetProductionShiftForType3(dateChange, currentTime, lineID);
                    }break;
                case "4": 
                    {
                        dto = GetProductionShiftForType4(dateChange, currentTime, lineID);
                    }break;
                case "5": 
                    {
                        dto = GetProductionShiftForType5(dateChange, currentTime, lineID);
                    }break;
            }
            return dto;
        }
        private ProductionShiftDto GetProductionShiftForTypes012(TimeSpan currentTime, int lineID) 
        {
            Expression<Func<ProductionShiftUse, bool>> expression = psu => psu.LineID == lineID && psu.UseShift == true && 
                ((psu.ProductionShift.StartTime <= psu.ProductionShift.EndTime && psu.ProductionShift.StartTime <= currentTime && psu.ProductionShift.EndTime > currentTime) ||
                (psu.ProductionShift.StartTime > psu.ProductionShift.EndTime && (psu.ProductionShift.StartTime <= currentTime || psu.ProductionShift.EndTime > currentTime)));
            var entity = _repository.Repository<ProductionShiftUse>().GetAllBy(expression).Select(psu => psu.ProductionShift).FirstOrDefault();
            return Mapper.Map<ProductionShift, ProductionShiftDto>(entity);
        }
        private ProductionShiftDto GetProductionShiftForType3(ProdDateChangeDto dateChange, TimeSpan currentTime, int lineID)
        {
            var maxEnd = _repository.Repository<ProductionShiftUse>().GetAllBy(psu => psu.LineID == lineID && psu.UseShift == true).Select(psu => psu.ProductionShift.EndTime).Max();
            var timeChange = dateChange.DateChange.TimeOfDay;
            ProductionShift shift1 = null;
            ProductionShift shift2 = null;
            Expression<Func<ProductionShiftUse, bool>> expression = null;
            if (currentTime < timeChange || currentTime > maxEnd)
            {
                expression = psu => psu.ProductionShift.StartTime > psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            else 
            {
                expression = psu => psu.ProductionShift.StartTime < psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            shift1 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression).Select(psu => psu.ProductionShift).FirstOrDefault();
            shift2 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression).Select(psu => psu.ProductionShift).LastOrDefault();
            var daysDiff = (dateChange.RotationStart - dateChange.CurrentProductionDate).Days % 14;
            ProductionShiftDto dto = null;
            switch (daysDiff) 
            {
                case 0:
                case 1:
                case 4:
                case 5:
                case 6:
                case 9:
                case 10: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift1);
                    } break;
                default: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift2);
                    }break;
            }

            return dto;
        }
        private ProductionShiftDto GetProductionShiftForType4(ProdDateChangeDto dateChange, TimeSpan currentTime, int lineID)
        {
            var maxEnd = _repository.Repository<ProductionShiftUse>().GetAllBy(psu => psu.LineID == lineID && psu.UseShift == true).Select(psu => psu.ProductionShift.EndTime).Max();
            var timeChange = dateChange.DateChange.TimeOfDay;
            ProductionShift shift1 = null;
            ProductionShift shift2 = null;
            Expression<Func<ProductionShiftUse, bool>> expression = null;
            if (currentTime < timeChange || currentTime > maxEnd)
            {
                expression = psu => psu.ProductionShift.StartTime > psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            else
            {
                expression = psu => psu.ProductionShift.StartTime < psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            shift1 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression).Select(psu => psu.ProductionShift).FirstOrDefault();
            shift2 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression).Select(psu => psu.ProductionShift).LastOrDefault();
            var daysDiff = (dateChange.RotationStart - dateChange.CurrentProductionDate).Days % 14;
            ProductionShiftDto dto = null;
            switch (daysDiff)
            {
                case 0:
                case 1:
                case 2:
                case 7:
                case 8:
                case 9:
                case 10:
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift1);
                    } break;
                default:
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift2);
                    } break;
            }

            return dto;
        }
        private ProductionShiftDto GetProductionShiftForType5(ProdDateChangeDto dateChange, TimeSpan currentTime, int lineID)
        {
            var maxEnd = _repository.Repository<ProductionShiftUse>().GetAllBy(psu => psu.LineID == lineID && psu.UseShift == true).Select(psu => psu.ProductionShift.EndTime).Max();
            var timeChange = dateChange.DateChange.TimeOfDay;
            ProductionShift shift1 = null;
            ProductionShift shift2 = null;
            ProductionShift shift3 = null;
            ProductionShift shift4 = null;
            Expression<Func<ProductionShiftUse, bool>> expression1 = null;
            Expression<Func<ProductionShiftUse, bool>> expression2 = null;
            if (currentTime < timeChange || currentTime >= maxEnd)
            {
                expression1 = psu => psu.ProductionShift.StartTime > psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
                expression2 = psu => psu.ProductionShift.StartTime < psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            else
            {
                expression1 = psu => psu.ProductionShift.StartTime < psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
                expression2 = psu => psu.ProductionShift.StartTime > psu.ProductionShift.EndTime && psu.LineID == lineID && psu.UseShift == true;
            }
            shift1 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression1).Select(psu => psu.ProductionShift).FirstOrDefault();
            shift2 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression1).Select(psu => psu.ProductionShift).LastOrDefault();
            shift3 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression2).Select(psu => psu.ProductionShift).FirstOrDefault();
            shift4 = _repository.Repository<ProductionShiftUse>().GetAllBy(expression2).Select(psu => psu.ProductionShift).LastOrDefault();

            var daysDiff = (dateChange.RotationStart - dateChange.CurrentProductionDate).Days % 28;
            ProductionShiftDto dto = null;
            switch (daysDiff)
            {
                case 0:
                case 1:
                case 4:
                case 5:
                case 6:
                case 9:
                case 10: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift1);
                    }break;
                case 7:
                case 8:
                case 11:
                case 12:
                case 13:
                case 16:
                case 17: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift2);
                    }break;
                case 14:
                case 15:
                case 18:
                case 19:
                case 20:
                case 23:
                case 24: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift3);
                    } break;
                default: 
                    {
                        dto = Mapper.Map<ProductionShift, ProductionShiftDto>(shift4);
                    }break;
            }

            return dto;
        }


        public double GetScrapPercent(int plantID, int lineID, int shiftID, DateTime productionDate)
        {
            double returnValue = 0;

            Plant plant = _repository.Repository<Plant>().GetById(plantID);
            UnitOfMeasure defaultUom = plant.UnitOfMeasureDefaults.First(m => m.UnitOfMeasureType.Code == "W").UnitOfMeasure;

            ProdLine line = _repository.Repository<ProdLine>().GetById(lineID);
            ProdLineType lineType = _repository.Repository<ProdLineType>().GetById(line.LineTypeID);
            ProductionShift shift = _repository.Repository<ProductionShift>().GetById(shiftID);

            Application.UoMConversionService convert = new Application.UoMConversionService();

            var scrap = 0;
            /*
            var scrap = _repository.Repository<TPOLineScrap>().GetAllBy(
                p => p.PlantID == plantID &&
                        p.ShiftID == shiftID &&
                        p.WorkOrder.LineID == lineID &&
                        p.ProductionDate == productionDate).Sum(
                            p => convert.ConvertUoM(p.WeightUoMID, (decimal)p.Weight, defaultUom.ID)
                        );
            */
            if (lineType.ProdLineTypeCode == "IM")
            {
                var prod = 0;
                /*
                var prod = _repository.Repository<IMProd>().GetAllBy(
                        p => p.ProdLineID == lineID &&
                                p.ProdShift == shift.Code &&
                                p.PlantID == plantID &&
                                p.ProdDate == productionDate).Sum(
                                    p => convert.ConvertUoM(p.WeightUOM, (decimal)p.CartonWeight, defaultUom.ID)
                                );
                 */
                 if (prod > 0)
                    returnValue = (double)(scrap / prod);

            }
            else
            {
                var prod = 0;
                /*
                var prod = _repository.Repository<TPOCProductRoll>().GetAllBy(
                        p => p.LineID == lineID &&
                                p.ShiftID == shiftID &&
                                p.PlantID == plantID &&
                                p.ProductionDate == productionDate).Sum(
                                    p => convert.ConvertUoM(p.WeightUoMID, (decimal)p.Weight, defaultUom.ID)
                                );
                */
                if (prod > 0)
                    returnValue = (double)( scrap / prod );
            }

            return returnValue;
        }
        public double GetScrapWeight(int plantID, int lineID, int shiftID, DateTime productionDate)
        {
            double returnValue = 0;

            Plant plant = _repository.Repository<Plant>().GetById(plantID);
            UnitOfMeasure defaultUom = plant.UnitOfMeasureDefaults.First(m => m.UnitOfMeasureType.Code == "W").UnitOfMeasure;

            ProdLine line = _repository.Repository<ProdLine>().GetById(lineID);
            ProdLineType lineType = _repository.Repository<ProdLineType>().GetById(line.LineTypeID);

            Application.UoMConversionService convert = new Application.UoMConversionService();
            double result = 100;
            /*
            var result = _repository.Repository<TPOLineScrap>().GetAllBy(
                            p => p.PlantID == plantID &&
                                 p.ShiftID == shiftID &&
                                 p.WorkOrder.LineID == lineID &&
                                 p.ProductionDate == productionDate).Sum(
                                    p => convert.ConvertUoM(p.WeightUoMID, (decimal)p.Weight, defaultUom.ID)
                                 );
            
            returnValue = (double)Math.Round(result, 0);
            */
            returnValue = result;

            return returnValue;
        }
    }
}
