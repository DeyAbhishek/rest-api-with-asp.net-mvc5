using System;

namespace TPO.Services.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Save();
    }
}