using System;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public interface IServer : IServerStateHandler
    {
        Guid Id { get; set; }
        ServerType ServerType { get; }
        IServerCapability Capability { get; }
        int RequestsCount { get; set; }
        void HandleRequests(int requests);
    }
}
