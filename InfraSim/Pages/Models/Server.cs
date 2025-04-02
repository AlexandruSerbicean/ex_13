public class Server : BaseServer
{
    public Server(ServerType type, IServerCapability capability, IServerState state) 
        : base(type, capability)
    {
        State = state;
    }
}