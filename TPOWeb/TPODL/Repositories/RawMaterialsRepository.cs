using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Models;
using TPO.DL.Framework;

namespace TPO.DL.Repositories
{
    public class RawMaterialsRepository : RepositoryBase
    {
        #region Variables
        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        public RawMaterialsRepository() : base()
        {
            
        }
        #endregion

        #region Public Methods

        #region RawMaterialQC
        public RawMaterialQC GetRawMaterialQCByID(int id)
        {
            return Entities.RawMaterialQCs.FirstOrDefault(r => r.ID == id);
        }
        public int CreateRawMaterialQC(Models.RawMaterialQC item) 
        {
            Entities.RawMaterialQCs.Add(item);
            Entities.SaveChanges();
            return item.ID;
        }
        #endregion

        #region RawMaterialReceived
        public IEnumerable<RawMaterialReceived> GetRawMaterialReceivedByRawMaterialCode(Int32 rawMaterialCode) 
        {
            return Entities.RawMaterialReceiveds.Where(r => r.RawMaterialID == rawMaterialCode).ToList();
        }
        #endregion

        #region RawMaterialTest
        public RawMaterialTest GetRawMaterialTestByRawMaterial(Int32 rawMaterialID) 
        {
            return Entities.RawMaterialTests.FirstOrDefault(r => r.RawMaterialID == rawMaterialID);
        }
        #endregion

        #region RawMaterialVendor

        public RawMaterialVendor GetRawMaterialVendorByID(Int32 vendorID)
        {
            return Entities.RawMaterialVendors.FirstOrDefault(v => v.ID == vendorID);
        }

        public IEnumerable<RawMaterialVendor> GetRawMaterialVendorByPlantID(Int32 plantID)
        {
            return Entities.RawMaterialVendors.Where(v => v.PlantID == plantID).ToList();
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
