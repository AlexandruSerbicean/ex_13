using System;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;
using InfraSim.Pages.Models.Validator;

namespace InfraSim.Pages.Models
{
    public interface IServerBuilder
    {
        IServerBuilder WithId(Guid id);
        IServerBuilder WithType(ServerType type);
        IServerBuilder WithCapability(IServerCapability capability);
        IServerBuilder WithState(IServerState state);
        IServerBuilder WithValidator(IValidatorStrategy validator);
        IServer Build(); 
    }
}
