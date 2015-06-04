using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Data;
using TPO.Services.Core;

namespace TPO.Services.Production
{
    public class TPOBatchService : ServiceBaseGeneric<TPOBatchDto, TPOBatch>, ITpoService<TPOBatchDto>
    {
        public List<TPOBatchDto> GetByLineID(int productionLineId)
        {
            return MapEntityList(_repository.Repository<TPOBatch>().GetAllBy(p => p.LineID == productionLineId));
        }

        public int GetTPOProdBatchNumber(int lineID, int productID, string userName, int? masterRollID) 
        {
            int batchNo = 0;
            var lineEntity = _repository.Repository<ProdLine>().GetById(lineID);
            if (masterRollID.HasValue) 
            {
                var masterRoll = _repository.Repository<TPOCProductRoll>().GetById(masterRollID.Value);
                if (masterRoll != null) 
                {
                    return masterRoll.BatchNumber;
                }
            }
            else 
            {
                bool newBatch = false;
                var batches = GetByLineID(lineID);
                if (batches.Count == 0)
                {
                    newBatch = true;
                }
                else 
                {
                    batchNo = batches[0].BatchNumber;
                }

                if (!newBatch) 
                {
                    TPO.Services.Formulation.TPOFormulationLineProductService formSvc = new Formulation.TPOFormulationLineProductService();
                    var formulations = formSvc.GetByPlantIdProductId(lineEntity.PlantID, productID);
                    newBatch = CheckNewBatchRequirements(lineEntity.PlantID, lineID, formulations.TPOFormulationID, batches);

                    if (newBatch) 
                    {
                        batchNo = GetNewBatch(lineEntity.PlantID, lineID, ++batchNo, formulations.TPOFormulationID, userName);
                    }
                }

            }
            return batchNo;
        }

        private bool CheckNewBatchRequirements(int plantID, int lineID, int tpoFormulationID, List<TPOBatchDto> batch)
        {
            var newBatch = CheckFormulationForBatch(tpoFormulationID, batch);

            if (!newBatch)
                newBatch = CheckScrimRollCountsForBatch(lineID, batch);

            if (!newBatch)
                newBatch = CheckScrimRollsForBatch(lineID);

            if (!newBatch)
                newBatch = CheckRawMaterialCountsForBatch(plantID, lineID, batch);

            if (!newBatch)
                newBatch = CheckRawMaterialsForBatch(lineID);
            return newBatch;
        }
        private bool CheckFormulationForBatch(int formulationID, List<TPOBatchDto> batch)
        {
            return batch.Count > 0 ?
                batch[0].FormulationID != formulationID :
                true;
        }
        private bool CheckScrimRollCountsForBatch(int lineID, List<TPOBatchDto> batch) 
        {
            int batchScrim = batch.Count(s => s.IsScrim == true);
            int currentScrim = _repository.Repository<TPOCurrentScrim>().GetAllBy(c => c.ProdLineID == lineID && c.ScrimPos != "NA").Count();
            return batchScrim != currentScrim;
        }
        private bool CheckScrimRollsForBatch(int lineID) 
        {
            TPOEntities context = new TPOEntities();
            var check =  (from b in context.TPOBatches
                            join cs in context.TPOCurrentScrims
                            on b.ScrimRollID equals cs.Scrim1RollID
                            where cs.ProdLineID == lineID
                            select cs).Count() > 0;
            if (!check)
            {
                check = (from b in context.TPOBatches
                         join cs in context.TPOCurrentScrims
                         on b.ScrimRollID equals cs.Scrim2RollID
                         where cs.ProdLineID == lineID
                         select cs).Count() > 0;
                if (!check) 
                {
                    check = (from b in context.TPOBatches
                             join cs in context.TPOCurrentScrims
                             on b.ScrimRollID equals cs.FleeceRollID
                             where cs.ProdLineID == lineID
                             select cs).Count() > 0;
                }
            }
            return check;     
        }
        private bool CheckRawMaterialCountsForBatch(int plantID, int lineID, List<TPOBatchDto> batch) 
        {
            int oldRawMatCount = batch.Count(s => s.IsScrim == false);
            //TODO:  Check TPOCurrentRawMaterial for batch number
            int newRawMatCount = _repository.Repository<TPOCurrentRawMaterial>().GetAllBy(r => r.PlantID == plantID && r.LineID == lineID).Count();
            return oldRawMatCount != newRawMatCount;
        }
        private bool CheckRawMaterialsForBatch(int lineID) 
        {
            TPOEntities context = new TPOEntities();
            int rawMats = (from b in context.TPOBatches
                           join cr in context.TPOCurrentRawMaterials
                               on new { l = b.LineID, rr = b.RawMaterialReceivedID.Value }
                               equals new { l = cr.LineID, rr = cr.RawMaterialReceivedID.Value }
                           where cr.LineID == lineID
                           select cr).Count();
            return rawMats == 0;
        }
        private int GetNewBatch(int plantID, int lineID, int batchNumber, int formulationID, string userName)
        {
            
            List<TPOCurrentRawMaterial> rmList = _repository.Repository<TPOCurrentRawMaterial>().GetAllBy(r => r.LineID == lineID).ToList();

            DateTime now = DateTime.Now;
            foreach (TPOCurrentRawMaterial rm in rmList)
            {
                TPOBatch batch = new TPOBatch()
                {
                    BatchNumber = batchNumber,
                    DateEntered = now,
                    EnteredBy = userName,
                    FormulationID = formulationID,
                    IsScrim = false,
                    LastModified = now,
                    LineID = lineID,
                    ModifiedBy = userName,
                    PlantID = plantID,
                    RawMaterialID = rm.RawMaterialReceived.RawMaterialID,
                    RawMaterialReceivedID = rm.RawMaterialReceivedID.Value,
                    ScrimRollID = null
                };
                _repository.Repository<TPOBatch>().Insert(batch);
            }

            List<TPOCurrentScrim> scrimList = _repository.Repository<TPOCurrentScrim>().GetAllBy(s => s.ProdLineID == lineID && s.ScrimPos != "NA").ToList();

            foreach (TPOCurrentScrim scrim in scrimList)
            {
                if (scrim.Scrim1RollID != null)
                {
                    TPOBatch batch = new TPOBatch()
                    {
                        BatchNumber = batchNumber,
                        DateEntered = now,
                        EnteredBy = userName,
                        FormulationID = formulationID,
                        IsScrim = true,
                        LastModified = now,
                        LineID = lineID,
                        ModifiedBy = userName,
                        PlantID = plantID,
                        ScrimRollID = scrim.Scrim1RollID.Value,
                        RawMaterialID = null,
                        RawMaterialReceivedID = null
                    };
                    _repository.Repository<TPOBatch>().Insert(batch);
                }
                if (scrim.Scrim2RollID != null)
                {
                    TPOBatch batch = new TPOBatch()
                    {
                        BatchNumber = batchNumber,
                        DateEntered = now,
                        EnteredBy = userName,
                        FormulationID = formulationID,
                        IsScrim = true,
                        LastModified = now,
                        LineID = lineID,
                        ModifiedBy = userName,
                        PlantID = plantID,
                        ScrimRollID = scrim.Scrim2RollID.Value,
                        RawMaterialID = null,
                        RawMaterialReceivedID = null
                    };
                    _repository.Repository<TPOBatch>().Insert(batch);
                }
            }
            return batchNumber;
        }
    }
}
