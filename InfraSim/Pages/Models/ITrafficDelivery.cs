namespace InfraSim.Pages.Models.Traffic
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
}