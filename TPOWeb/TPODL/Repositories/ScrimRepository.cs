using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Framework;
using TPO.DL.Models;

namespace TPO.DL.Repositories
{
    public class ScrimRepository : RepositoryBase
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        #region ScrimRoll
        public ScrimRoll GetScrimRollByID(int id)
        {
            return Entities.ScrimRolls.FirstOrDefault(r => r.ID == id);
        }
        public IEnumerable<ScrimRoll> GetScrimRolls() 
        {
            return Entities.ScrimRolls.ToList();
        }
        public IEnumerable<ScrimRoll> GetScrimRollsByPlantID(int plantID) 
        {
            return Entities.ScrimRolls.Where(r => r.PlantID == plantID).ToList();
        }
        public IEnumerable<ScrimRoll> GetScrimRollsByTypeID(int typeID) 
        {
            return Entities.ScrimRolls.Where(r => r.TypeID == typeID).ToList();
        }
        public int InsertScrimRoll(Models.ScrimRoll entity) 
        {
            Entities.ScrimRolls.Add(entity);
            return Entities.SaveChanges();
        }
        #endregion

        #region ScrimType
        
        #endregion

        #region TPOCurrentScrim
        public TPOCurrentScrim GetTPOCurrentScrimByID(int id) 
        {
            return Entities.TPOCurrentScrims.FirstOrDefault(r => r.ID == id);
        }
        public void InsertTPOCurrentScrim(TPOCurrentScrim entity) 
        {
            Entities.TPOCurrentScrims.Add(entity);
            Entities.SaveChanges();
        }
        public TPOCurrentScrim GetTPOCurrentScrimByLineID(string lineID) 
        {
            return Entities.TPOCurrentScrims.FirstOrDefault(r => r.LineID == lineID);
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
