using System;
using System.Collections.Generic;

namespace InfraSim.Pages.Models.Database
{
    public interface IRepository<TEntity> where TEntity : DbItem
    {
        List<TEntity> GetAll();
        TEntity? Get(Guid id);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
    }
}
