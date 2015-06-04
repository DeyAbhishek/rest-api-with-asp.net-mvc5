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

namespace TPO.Services.Rework
{
    public class TPOReworkRollService : ServiceBaseGeneric<TPOReworkRollDto, TPOReworkRoll>, ITpoService<TPOReworkRollDto>
    {
        public List<TPOReworkRollDto> GetStartingRollsByPlant(int plantID)
        {
            List<TPOReworkRollDto> data = new List<TPOReworkRollDto>();
            var entities = _repository.Repository<TPOReworkRoll>().GetAllBy(r => r.Processed == false && r.Code.Substring(0, 1) == "S" && r.PlantID == plantID).ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = MapEntity(entities[i]);
                dto.ProductCode = entities[i].TPOProduct.ProductCode;
                dto.ThicknessDisplay = string.Format("{0} {1}",
                                                        entities[i].TPOProduct.Thick,
                                                        entities[i].TPOProduct.ThickUnitOfMeasure.Code);
                dto.WidthDisplay = string.Format("{0} {1}",
                                                    entities[i].Width,
                                                    entities[i].WidthUnitOfMeasure.Code);
                dto.LengthDisplay = string.Format("{0} {1}",
                                                    entities[i].Length,
                                                    entities[i].LengthUnitOfMeasure.Code);
                dto.WeightDisplay = string.Format("{0} {1}",
                                                    entities[i].Weight,
                                                    entities[i].WeightUnitOfMeasure.Code);
                data.Add(dto);
            }
            return data;
        }

        public List<TPOReworkRollDto> GetMasterRollsByPlant(int plantID)
        {
            List<TPOReworkRollDto> data = new List<TPOReworkRollDto>();
            var entities = _repository.Repository<TPOReworkRoll>().GetAllBy(r => r.Processed == false && r.Code.Substring(0, 1) != "S" && r.PlantID == plantID).ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = MapEntity(entities[i]);
                dto.ProductCode = entities[i].TPOProduct.ProductCode;
                dto.ThicknessDisplay = string.Format("{0} {1}",
                                                        entities[i].TPOProduct.Thick,
                                                        entities[i].TPOProduct.ThickUnitOfMeasure.Code);
                dto.WidthDisplay = string.Format("{0} {1}",
                                                    entities[i].Width,
                                                    entities[i].WidthUnitOfMeasure.Code);
                dto.LengthDisplay = string.Format("{0} {1}",
                                                    entities[i].Length,
                                                    entities[i].LengthUnitOfMeasure.Code);
                dto.WeightDisplay = string.Format("{0} {1}",
                                                    entities[i].Weight,
                                                    entities[i].WeightUnitOfMeasure.Code);
                data.Add(dto);
            }
            return data;
        }

        public void CheckRollProcessed(int reworkRollID) 
        {
            var roll = _repository.Repository<TPOReworkRoll>().GetById(reworkRollID);
            if (roll != null) 
            {
                var actions = _repository.Repository<TPOReworkAction>().GetAllBy(a => a.StartRollID == reworkRollID).ToList();
                var lengthSum = actions.Sum(a => a.Length);
                var weightSum = actions.Sum(a => a.Weight);

                bool processed = (roll.Length * .9) <= lengthSum || (roll.Weight * .9) <= weightSum;
                if (processed != roll.Processed)
                {
                    roll.Processed = processed;
                    _repository.Repository<TPOReworkRoll>().Update(roll);
                    _repository.Save();
                }
            }
        }

        
    }
}
