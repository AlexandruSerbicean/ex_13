using System;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.State;

public class ServerBuilder : IServerBuilder
{
    private Guid _id = Guid.NewGuid();
    private ServerType _type = ServerType.Server;
    private IServerCapability _capability = new ServerCapability();
    private IServerState _state = new NormalState();

    public IServerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public IServerBuilder WithType(ServerType type)
    {
        _type = type;
        return this;
    }

    public IServerBuilder WithCapability(IServerCapability capability)
    {
        _capability = capability;
        return this;
    }

    public IServerBuilder WithState(IServerState state)
    {
        _state = state;
        return this;
    }

    public IServer Build()
    {
        return new Server(_id, _type, _capability, _state);
    }
}
