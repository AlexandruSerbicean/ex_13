using System;
using System.Collections.Generic;

public class TrafficRouting : ITrafficRouting
{
    private List<IServer> _servers;

    public TrafficRouting(List<IServer> servers)
    {
        _servers = servers;
    }

    public void RouteTraffic(int requestsCount)
    {
        int requests = CalculateRequests(requestsCount);
        List<IServer> servers = ObtainServers();
        SendRequestsToServers(requests, servers);
    }

    private int CalculateRequests(int requestsCount)
    {
        return requestsCount; // Returns the total number of incoming requests
    }

    private List<IServer> ObtainServers()
    {
        return _servers; // Returns the list of available servers
    }

    private void SendRequestsToServers(int requests, List<IServer> servers)
    {
        if (servers.Count == 0)
        {
            Console.WriteLine("No servers available to handle traffic.");
            return;
        }

        int requestsPerServer = requests / servers.Count;
        int remainingRequests = requests % servers.Count;

        for (int i = 0; i < servers.Count; i++)
        {
            int requestsToSend = requestsPerServer + (i < remainingRequests ? 1 : 0);
            servers[i].HandleRequests(requestsToSend);
        }
    }
}
