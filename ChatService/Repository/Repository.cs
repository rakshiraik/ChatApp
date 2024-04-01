using ChatService.Entity;
using ChatService.Repository.Contracts;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Reflection;

namespace ChatService.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }



        public IQueryable<TEntity> GetAsQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
        }

        public IEnumerable<TEntity> FromSqlInterpolated(FormattableString sql)
        {
            return Context.Set<TEntity>().FromSqlInterpolated(sql);
        }


        //private T DetachEntity<T>(T entity) where T : class
        //{
        //    Context.Entry(entity).State = EntityState.Detached;
        //    if (entity.GetType().GetProperty("Id") != null)
        //    {
        //        entity.GetType().GetProperty("Id").SetValue(entity, 0);
        //    }
        //    return entity;
        //}
        //public void Remove(TEntity entity)
        //{
        //    Context.Set<TEntity>().Remove(entity);

        //}

        //public void RemoveRange(IEnumerable<TEntity> entities)
        //{
        //    Context.Set<TEntity>().RemoveRange(entities);
        //}

        /// <summary>
        /// Gets a table
        /// </summary>
        //public virtual IQueryable<TEntity> Table
        //{
        //    get
        //    {
        //        return Entities;
        //    }
        //}

        //protected virtual DbSet<TEntity> Entities
        //{
        //    get
        //    {
        //        if (entities == null)
        //            entities = Context.Set<TEntity>();
        //        return entities;
        //    }
        //}

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        //public virtual IQueryable<TEntity> TableNoTracking
        //{
        //    get
        //    {
        //        return Entities.AsNoTracking();
        //    }
        //}

        //public int Insert(TEntity entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    entities.Add(entity);
        //    return SaveChange();
        //}

        //public int UpdateOld(TEntity entity)
        //{//rakshith context chnaged
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    return SaveChange();
        ////}
        //private int SaveChange()
        //{
        //    return Context.SaveChanges();
        //}

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();

        }
    }
}
