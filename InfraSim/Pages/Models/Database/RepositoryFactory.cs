namespace InfraSim.Pages.Models.Database
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly InfraSimContext _context;

        public RepositoryFactory(InfraSimContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Create<TEntity>() where TEntity : DbItem
        {
            return new Repository<TEntity>(_context);
        }
    }
}
