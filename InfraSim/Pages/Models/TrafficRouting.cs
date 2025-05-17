using System.Collections.Generic;
using System.Linq;

namespace InfraSim.Pages.Models
{
    public interface ITrafficDelivery
    {
        void SetNext(ITrafficDelivery nextHandler);
        void DeliverRequests(long requestCount);
    }

    public abstract class TrafficDelivery : ITrafficDelivery
    {
        protected ITrafficDelivery? NextHandler;

        public void SetNext(ITrafficDelivery nextHandler)
        {
            NextHandler = nextHandler;
        }

        public abstract void DeliverRequests(long requestCount);
    }

    public interface ITrafficRouting
    {
        void RouteTraffic(long requestCount);
    }

    public abstract class TrafficRouting : TrafficDelivery, ITrafficRouting
    {
        protected List<IServer> _servers;

        public TrafficRouting(List<IServer> servers)
        {
            _servers = servers;
        }

        public void RouteTraffic(long requestCount)
        {
            long totalRequests = CalculateRequests(requestCount);
            List<IServer> availableServers = ObtainServers();
            SendRequestsToServers(totalRequests, availableServers);
        }

        public abstract long CalculateRequests(long requestCount);
        public abstract List<IServer> ObtainServers();

        public void SendRequestsToServers(long requests, List<IServer> servers)
        {
            if (servers.Count == 0) return;

            long requestsPerServer = requests / servers.Count;
            long remainder = requests % servers.Count;

            foreach (var server in servers)
            {
                long assignedRequests = requestsPerServer + (remainder > 0 ? 1 : 0);
                server.HandleRequests((int)assignedRequests); 
                remainder--;
            }
        }

        public override void DeliverRequests(long requestCount)
        {
            RouteTraffic(requestCount);
            long remainingRequests = requestCount - CalculateRequests(requestCount);
            if (remainingRequests > 0)
            {
                NextHandler?.DeliverRequests(remainingRequests);
            }
            else
            {
                NextHandler?.DeliverRequests(requestCount);
            }
        }
    }
}
