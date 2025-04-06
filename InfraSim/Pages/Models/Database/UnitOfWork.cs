using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;

namespace InfraSim.Pages.Models.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction? Transaction { get; set; }

        private InfraSimContext Context { get; set; }
        private IRepositoryFactory RepositoryFactory { get; set; }

        private IDictionary<Type, object> Repositories { get; } = new Dictionary<Type, object>();

        public UnitOfWork(InfraSimContext context, IRepositoryFactory repositoryFactory)
        {
            Context = context;
            RepositoryFactory = repositoryFactory;
        }

        public void Begin()
        {
            Transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            Transaction?.Commit();
            Transaction = null;
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Transaction = null;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : DbItem
        {
            if (!Repositories.ContainsKey(typeof(TEntity)))
            {
            Repositories[typeof(TEntity)] = RepositoryFactory.Create<TEntity>();
            }

        return (IRepository<TEntity>)Repositories[typeof(TEntity)];
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Context.Dispose();
        }
    }
}
