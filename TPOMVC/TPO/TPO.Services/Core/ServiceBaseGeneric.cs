using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using System.Linq.Expressions;

namespace TPO.Services.Core
{
    public abstract class ServiceBaseGeneric<TDto, T> : ServiceBase,  ITpoService<TDto>, IDisposable 
        where TDto : class
        where T : class, new()
    {
        #region ITpoService implementation
        // override this if T doesn't have an ID field
        public virtual int Add(TDto dto)
        {
            dynamic entity = UpdateUnitOfWork(dto, 0);
            if (entity != null)
            {
                CommitUnitOfWork();
                return entity.ID;
            }
            else
            {
                return -1;
            }
        }

        public List<TDto> GetAll()
        {
            return MapEntityList(_repository.Repository<T>().GetAll());
        }

        public void Delete(int id)
        {
            _repository.Repository<T>().Delete(GetById(id));
            CommitUnitOfWork();
        }

        public TDto Get(int id)
        {
            return MapEntity(GetById(id));
        }

        // override this if TDto doesn't have an ID field
        public virtual void Update(TDto dto)
        {
            int id = GetIDValue(dto);
            UpdateUnitOfWork(dto, id);
            CommitUnitOfWork();
        }
        #endregion // ITpoService implementation

        #region Public
        public List<TDto> GetAllByPlantId(int plantId)
        {
            return MapEntityList(FilterByProperty(_repository.Repository<T>().GetAll(), "PlantID", typeof(int), plantId));
        }

        public virtual TDto Save(TDto dto)
        {
            int id = 0;
            try
            {
                id = GetIDValue(dto);
            }
            catch
            {
                throw new InvalidOperationException("Entity doesn't have an ID field, must override save method");
            }
            if (id == 0)
                return Get(Add(dto));
            else
            {
                Update(dto);
                return dto;
            }
        }
        #endregion // Public

        #region Protected
        protected T UpdateUnitOfWork(TDto dto, int id)
        {
            T entity = null;
            try
            {
                if (id == 0)
                {
                    entity = new T();
                    Mapper.Map(dto, entity);
                    _repository.Repository<T>().Insert(entity);
                }
                else
                {
                    entity = GetById(id);
                    Mapper.Map(dto, entity);
                    _repository.Repository<T>().Update(entity);
                }
            }
            catch (DbEntityValidationException valEx)
            {
                HandleValidationException(valEx);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
            return entity;
        }

        protected int GetIDValue(TDto dto)
        {
            PropertyInfo pi = dto.GetType().GetProperty("ID");
            return (int)pi.GetValue(dto);
        }

        // maps an Enumerable set of entities back to a List of DTOs
        protected List<TDto> MapEntityList(IEnumerable<T> entities)
        {
            return Mapper.Map<List<T>, List<TDto>>(entities.ToList());
        }

        // maps an entity back to a DTO
        protected TDto MapEntity(T entity)
        {
            return Mapper.Map<T, TDto>(entity);
        }

        protected T GetById(int id)
        {
            return _repository.Repository<T>().GetById(id);
        }

        // creates an expression tree to filter by a property value.
        // GetAllByPlantId is implemented using this:
        //         public List<TDto> GetAllByPlantId(int plantId)
        //{
        //    return MapEntityList(FilterByProperty(_repository.Repository<T>().GetAll(), "PlantID", typeof(int), plantId));
        //}
        protected IQueryable<T> FilterByProperty(IQueryable<T> ps, string propertyName, Type propertyType, object propertyValue)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "t");
            Expression left = Expression.Property(pe, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Constant(propertyValue, propertyType);
            Expression predicate = Expression.Equal(left, right);
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { ps.ElementType },
                ps.Expression,
                Expression.Lambda<Func<T, bool>>(predicate, new ParameterExpression[] { pe }));

            return ps.Provider.CreateQuery<T>(whereCallExpression);

        }
        #endregion
    }
}
