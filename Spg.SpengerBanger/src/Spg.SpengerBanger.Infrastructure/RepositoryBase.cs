using Microsoft.EntityFrameworkCore;
using Spg.SpengerBanger.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.Infrastructure
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        public SpengerBangerContext _db { get; }

        public RepositoryBase(SpengerBangerContext db)
        {
            _db = db;
        }

        public IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sortOrder = null,
            string? includeNavigationProperty = null)
        {
            IQueryable<TEntity> result = _db.Set<TEntity>();
            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (sortOrder != null)
            {
                result = sortOrder(result);
            }
            includeNavigationProperty = includeNavigationProperty ?? String.Empty;
            foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(item);
            }
            return result;
        }

        public TEntity? GetSingle(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "")
        {
            return GetQueryable(filter: filter, sortOrder: orderBy, includeNavigationProperty: includeNavigationProperty)
                .SingleOrDefault();
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "")
        {
            return GetQueryable(filter: filter, sortOrder: orderBy, includeNavigationProperty: includeNavigationProperty);
        }

        public IQueryable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "")
        {
            return GetQueryable(sortOrder: orderBy, includeNavigationProperty: includeNavigationProperty);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}