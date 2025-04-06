namespace InfraSim.Pages.Models.Database
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity : DbItem;
    }
}
