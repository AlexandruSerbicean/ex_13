using System;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public class Server : BaseServer, IServer
    {
        public Guid Id { get; set; } 

        public Server(Guid id, ServerType type, IServerCapability capability, IServerState state)
            : base(type, capability)
        {
            Id = id;
            State = state;
        }

    }
}
