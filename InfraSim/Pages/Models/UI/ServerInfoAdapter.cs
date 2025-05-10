using System;                             // <- ADD THIS
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models.UI
{
    /// <summary>
    /// Adapter — converts <see cref="IServer"/> into a view‑model
    /// </summary>
    public sealed class ServerInfoAdapter : IServerInfo
    {
        private static readonly string ImgBase = "/images/";

        public IServer Server { get; }

        public ServerInfoAdapter(IServer server) => Server = server;

        /* ---------- IServerInfo implementation ---------- */

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
            ServerType.Cache        => $"{ImgBase}cache.png",
            ServerType.LoadBalancer => $"{ImgBase}loadbalancer.png",
            ServerType.CDN          => $"{ImgBase}cdn.png",
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
