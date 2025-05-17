using System;
using InfraSim.Pages.Models.Visitor;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public interface IServer : IServerStateHandler, IServerAcceptVisit
    {
        Guid Id { get; set; }
        ServerType ServerType { get; }
        IServerCapability Capability { get; }
        long RequestsCount { get; set; }
        void HandleRequests(long requests);
        IValidatorStrategy Validator { get; set; }

    }
}
