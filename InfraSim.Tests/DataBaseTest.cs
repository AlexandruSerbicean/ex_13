using System;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Database;
using InfraSim.Pages.Models.Capabilities;
using Xunit;

namespace InfraSim.Tests
{
    public class DataBaseTest
    {
        DbServer Server;
        InfraSimContext Context;
        IRepositoryFactory Factory;
        IUnitOfWork UnitOfWork;
        IRepository<DbServer> ServerRepository;

        public DataBaseTest()
        {
            Server = new DbServer
            {
                Id = Guid.NewGuid(),
                ServerType = ServerType.Server
            };

            Context = new MemoryInfraSimContext();
            Context.Database.EnsureCreated();

            Factory = new RepositoryFactory(Context);
            UnitOfWork = new UnitOfWork(Context, Factory);

            ServerRepository = UnitOfWork.GetRepository<DbServer>();

            UnitOfWork.Begin();
            ServerRepository.Insert(Server);
            
            UnitOfWork.SaveChanges();
        }

        [Fact]
        public void WhenAddingServersInDatabase_TheyAreStoredIfSuccess()
        {
            UnitOfWork.Commit();

            var servers = ServerRepository.GetAll();
            Assert.Single(servers);
        }

        [Fact]
        public void WhenAddingServersInDatabase_TheyAreNotStoredIfFailed()
        {
            UnitOfWork.Rollback();

            var servers = ServerRepository.GetAll();
            Assert.Empty(servers);
        }
    }
}
