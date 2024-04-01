using ChatService.Entity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatService.Repository.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAsQuery(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAllAsNoTracking();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FromSqlInterpolated(FormattableString sql);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);

        //IQueryable<TEntity> Table { get; }
        //IQueryable<TEntity> TableNoTracking { get; }

        //int Insert(TEntity entity);

        //int UpdateOld(TEntity entity);

        void Delete(TEntity entity);

        void SaveChanges();
    }
}
