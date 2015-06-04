using System;
using TPO.DL.Models;

namespace TPO.DL.Framework
{
    public class RepositoryBase : IDisposable
    {
        #region Variables

        #endregion

        #region Properties

        protected TPOMVCApplicationEntities Entities { get; private set; }

        #endregion

        #region Constructors
        public RepositoryBase() 
        {
            Entities = new TPOMVCApplicationEntities();
        }
        #endregion

        #region Public Methods
        public void Dispose()
        {
            if (Entities != null) { Entities.Dispose(); }
        }
        public int SaveChanges() 
        {
            return Entities.SaveChanges();
        }
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
