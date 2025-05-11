using System;                             
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models.UI
{
    public sealed class ServerInfoAdapter : IServerInfo
    {
        private static readonly string ImgBase = "/images/";

        public IServer Server { get; }

        public ServerInfoAdapter(IServer server) => Server = server;



        public string Name => Server.ServerType switch
        {
            ServerType.Server       => "Server",
            ServerType.Cache        => "Cache",
            ServerType.LoadBalancer => "Load Balancer",
            ServerType.CDN          => "CDN",
            _ => throw new NotImplementedException($"Unknown server type {Server.ServerType}")
        };

        public string ImageUrl => Server.ServerType switch
        {
            ServerType.Server       => $"{ImgBase}server.png",
            ServerType.Cache        => $"{ImgBase}Cache.webp",
            ServerType.LoadBalancer => $"{ImgBase}LB.png",
            ServerType.CDN          => $"{ImgBase}CDN.png",
            _ => throw new NotImplementedException($"No icon mapped for {Server.ServerType}")
        };

        public string StatusColor => Server.State switch
        {
            IdleState       => "gray",
            NormalState     => "blue",
            OverloadedState => "orange",
            FailedState     => "red",
            _               => "gray"
        };
    }
}
