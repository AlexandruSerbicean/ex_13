using System;
namespace InfraSim.Pages.Models.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : DbItem;

        void Begin();
        void Commit();
        void Rollback();
        void SaveChanges();
    }
}
