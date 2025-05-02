using InfraSim.Pages.Models.Capabilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models.Database
{
    public class ServerDataMapper : IServerDataMapper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICapabilityFactory _capabilityFactory;

        public ServerDataMapper(IUnitOfWork unitOfWork, ICapabilityFactory capabilityFactory)
        {
            _unitOfWork = unitOfWork;
            _capabilityFactory = capabilityFactory;
        }

        public List<IServer> GetAll()
        {
            var dbServers = _unitOfWork.GetRepository<DbServer>().GetAll();
            return dbServers.Select(db =>
                new ServerBuilder()
                    .WithId(db.Id)
                    .WithType(db.ServerType)
                    .WithCapability(_capabilityFactory.Create(db.ServerType))
                    .WithState(new State.NormalState())
                    .Build()
            ).ToList();
        }

        public IServer? Get(Guid id)
        {
            var db = _unitOfWork.GetRepository<DbServer>().Get(id);
            if (db == null) return null;

            return new ServerBuilder()
                .WithId(db.Id)
                .WithType(db.ServerType)
                .WithCapability(_capabilityFactory.Create(db.ServerType))
                .WithState(new State.NormalState())
                .Build();
        }

        public void Insert(IServer server)
        {
            var dbServer = new DbServer
            {
                Id = server.Id,
                ServerType = server.ServerType
            };

            _unitOfWork.GetRepository<DbServer>().Insert(dbServer);
            _unitOfWork.SaveChanges();
        }

        public void Remove(IServer server)
        {
            var repo = _unitOfWork.GetRepository<DbServer>();
            var dbServer = repo.Get(server.Id);

            if (dbServer != null)
            {
                repo.Delete(dbServer);
                _unitOfWork.SaveChanges();
            }
        }
    }
}
