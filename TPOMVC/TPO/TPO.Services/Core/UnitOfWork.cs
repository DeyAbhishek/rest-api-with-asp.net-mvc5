using System;
using System.Collections.Generic;
using TPO.Data;

namespace TPO.Services.Core
{
    public sealed class UnitOfWork: IUnitOfWork
    {
        private bool _disposed = false;
        private IDictionary<Type, object> _repositories;
        private TPOEntities _dbContext;

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork()
        {
            _dbContext = new TPOEntities();
            _repositories = new Dictionary<Type, object>();
            AutoMapperConfig.Initialize();
        }

        /// <summary>
        /// IOC
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="repositories"></param>
        //public UnitOfWork(IDbContext dbContextFactory, IDictionary<Type,object> repositories)
        //{
        //    _dbContext = IDbContext;
        //    _repositories = repositories;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}