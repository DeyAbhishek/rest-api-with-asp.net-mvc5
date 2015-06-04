using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TPO.Services.Core
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        IQueryable<TEntity> GetAllBy(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}