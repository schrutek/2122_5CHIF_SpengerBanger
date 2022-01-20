using Spg.SpengerBanger.Domain.Model;
using System.Linq.Expressions;

namespace Spg.SpengerBanger.Infrastructure
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeNavigationProperty = "");
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeNavigationProperty = "");
        IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeNavigationProperty = "");
        void SaveChanges();
    }
}
