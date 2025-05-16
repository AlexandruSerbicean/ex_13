using System;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models
{
    public class Server : BaseServer, IServer
    {
    public Server(ServerType type, IServerCapability capability, IValidatorStrategy validator)
        : base(type, capability, validator)
    {
        State = new NormalState(); 
    }
}
}
