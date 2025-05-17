using System;
using InfraSim.Pages.Models.Visitor;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public abstract class BaseServer : IServer
    {
        private long Requests = 0;

        public Guid Id { get; set; } 

        public ServerType ServerType { get; private set; }
        public IServerCapability Capability { get; private set; }
        public void Accept(IServerVisitor visitor) => visitor.Visit(this);
        public IValidatorStrategy Validator { get; set; }

        public long RequestsCount
        {
            get => Requests;
            set
            {
                Requests = value;
                State?.Handle(this);
            }
        }

        public IServerState State { get; set; }

        public virtual void HandleRequests(long requests)
        {
            RequestsCount = requests;
        }

        public BaseServer(ServerType type, IServerCapability capability, IValidatorStrategy validator)
        {
            ServerType = type;
            Capability = capability;
            Validator   = validator;
        }
    }
}
