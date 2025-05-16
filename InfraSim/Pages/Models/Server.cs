using System;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;
using InfraSim.Pages.Models.Validator;

namespace InfraSim.Pages.Models
{
    public class Server : BaseServer, IServer
    {
        public Guid Id { get; set; }

        public Server(Guid id, ServerType type, IServerCapability capability, IServerState state, IValidatorStrategy validator)
            : base(type, capability, validator)
        {
            Id = id;
            State = state;
        }
    }
}
