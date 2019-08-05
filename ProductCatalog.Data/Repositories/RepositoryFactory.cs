using ProductCatalog.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProductCatalog.Data.Repositories
{
    public interface IRepositoryFactory<ContextType> where ContextType : IBasicContext
    {
        IRepository<ContextType, EntityType> CreateRepository<EntityType>(List<Expression<Func<EntityType, object>>> includeProperties = null, Expression<Func<EntityType, bool>> baseFilter = null) where EntityType : class;
    }

    public class RepositoryFactory<ContextType> : IRepositoryFactory<ContextType> where ContextType : IBasicContext
    {
        private ContextType _context;

        public RepositoryFactory(ContextType context)
        {
            _context = context;
        }

        public IRepository<ContextType, EntityType> CreateRepository<EntityType>() where EntityType : class
        {
            return new Repository<ContextType, EntityType>(_context);
        }

        public IRepository<ContextType, EntityType> CreateRepository<EntityType>(
            List<Expression<Func<EntityType, object>>> includeProperties = null,
            Expression<Func<EntityType, bool>> baseFilter = null) where EntityType : class
        {
            var repository = new Repository<ContextType, EntityType>(_context);

            if (includeProperties != null)
                includeProperties.ForEach(i => repository.Include(i));
            if (baseFilter != null)
                repository.BaseFilter = baseFilter;

            return repository;
        }
    }
}
