using System;
using InfraSim.Pages.Models.Iterator;
using System.Collections.Generic;

namespace InfraSim.Pages.Models.Iterator
{
    public sealed class ServerIterator : IServerIterator
    {
        private readonly List<IServer> _servers;
        private int _position; 

        public ServerIterator(ICluster root)
        {
            _servers  = GetServers(root);
            _position = 0;
        }

        public bool HasNext => _position < _servers.Count;

        public IServer Next()
        {
            if (!HasNext)
                throw new InvalidOperationException("Iterator has reached the end.");

            return _servers[_position++];
        }


        private static List<IServer> GetServers(ICluster cluster)
        {
            var result = new List<IServer>();

            foreach (var server in cluster.Servers)
            {
                if (server is ICluster nested)
                    result.AddRange(GetServers(nested));   
                else
                    result.Add(server);                    
            }
            return result;
        }
    }
}
