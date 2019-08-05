using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DBContext;

namespace ProductCatalog.Data.Repositories
{
    public interface IRepository<ContextType, EntityType> : IDisposable where ContextType : IBasicContext where EntityType : class
    {
        Expression<Func<EntityType, bool>> BaseFilter { set; }
        IQueryable<EntityType> DataSet { get; }
        void Insert(EntityType entity);
        void InsertRange(IEnumerable<EntityType> entities);
        void Delete(EntityType entity);
        void DeleteRange(IEnumerable<EntityType> entities);
        void Update(EntityType entity);
        void UpdateRange(IEnumerable<EntityType> entities);
        EntityType FirstOrDefault(Expression<Func<EntityType, bool>> predicate);
        SelectType FirstOrDefault<SelectType>(Expression<Func<EntityType, bool>> predicate, Expression<Func<EntityType, SelectType>> selectPredicate);
        IEnumerable<EntityType> SearchFor(Expression<Func<EntityType, bool>> predicate);
        IEnumerable<SelectType> SearchFor<SelectType>(Expression<Func<EntityType, bool>> predicate, Expression<Func<EntityType, SelectType>> selectPredicate);
        IRepository<ContextType, EntityType> Include(Expression<Func<EntityType, object>> includeProperty);
    }

    public class Repository<ContextType, EntityType> : IRepository<ContextType, EntityType> where ContextType : IBasicContext where EntityType : class
    {
        private ContextType _context;
        private List<Expression<Func<EntityType, object>>> _includeProperties = new List<Expression<Func<EntityType, object>>>();
        private DbSet<EntityType> _dbSet;
        private Expression<Func<EntityType, bool>> _baseFilter = (x => true);

        public Expression<Func<EntityType, bool>> BaseFilter
        {
            set
            {
                _baseFilter = value;
            }
        }

        public IQueryable<EntityType> DataSet
        {
            get
            {
                return BaseDBSet;
            }
        }

        private IQueryable<EntityType> BaseDBSet
        {
            get
            {
                return _includeProperties.Aggregate<Expression<Func<EntityType, object>>, IQueryable<EntityType>>(_dbSet, (current, expression) => current.Include(expression)).Where(_baseFilter).AsNoTracking();
            }
        }

        public Repository(ContextType context)
        {
            _context = context;
            _dbSet = context.Set<EntityType>();
        }

        public void Insert(EntityType entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void InsertRange(IEnumerable<EntityType> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void Delete(EntityType entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void DeleteRange(IEnumerable<EntityType> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void Update(EntityType entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void UpdateRange(IEnumerable<EntityType> entities)
        {
            _dbSet.UpdateRange(entities);
            _context.SaveChanges();
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public EntityType FirstOrDefault(Expression<Func<EntityType, bool>> predicate)
        {
            return BaseDBSet.Where(predicate).FirstOrDefault();
        }

        public SelectType FirstOrDefault<SelectType>(Expression<Func<EntityType, bool>> predicate, Expression<Func<EntityType, SelectType>> selectPredicate)
        {
            return BaseDBSet.Where(predicate).Select(selectPredicate).FirstOrDefault();
        }

        public IEnumerable<EntityType> SearchFor(Expression<Func<EntityType, bool>> predicate)
        {
            return BaseDBSet.Where(predicate).AsEnumerable();
        }

        public IEnumerable<SelectType> SearchFor<SelectType>(Expression<Func<EntityType, bool>> predicate, Expression<Func<EntityType, SelectType>> selectPredicate)
        {
            return BaseDBSet.Where(predicate).Select(selectPredicate).AsEnumerable();
        }

        public IRepository<ContextType, EntityType> Include(Expression<Func<EntityType, object>> IncludeProperty)
        {
            _includeProperties.Add(IncludeProperty);
            return this;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
