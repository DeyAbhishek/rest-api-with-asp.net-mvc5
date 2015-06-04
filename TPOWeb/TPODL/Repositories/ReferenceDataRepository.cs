using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Models;
using TPO.DL.Framework;

namespace TPO.DL.Repositories
{
    public class ReferenceDataRepository : RepositoryBase
    {
        #region Variables
        
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ReferenceDataRepository() : base()
        {
            
        }
        #endregion

        #region Public Methods


        #region ScrimType
        public IEnumerable<ScrimType> GetScrimTypes()
        {
            return Entities.ScrimTypes.ToList();
        }
        #endregion

        #region TestLimitType
        public TestLimitType GetTestLimitTypeByID(int id)
        {
            return Entities.TestLimitTypes.FirstOrDefault(t => t.ID == id);
        }
        #endregion

        #region UnitOfMeasure
        public UnitOfMeasure GetUnitOfMeasureByID(int id) 
        {
            return Entities.UnitOfMeasures.FirstOrDefault(uom => uom.ID == id);
        }
        public IEnumerable<UnitOfMeasure> GetWeightUnitsOfMeasure() 
        {
            return (from r in Entities.UnitOfMeasures
                    join t in Entities.UnitOfMeasureTypes on r.TypeID equals t.ID
                    where t.Code == "W"
                    select r).ToList();
        }
        public IEnumerable<UnitOfMeasure> GetLengthUnitsOfMeasure() 
        {
            return (from r in Entities.UnitOfMeasures
                    join t in Entities.UnitOfMeasureTypes on r.TypeID equals t.ID
                    where t.Code == "L"
                    select r).ToList();
        }
        #endregion

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion

    }
}
