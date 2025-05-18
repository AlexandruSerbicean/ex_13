namespace InfraSim.Pages.Models
{
    public interface IServerFactory
    {
        IServer CreateServer(ServerType type);
        IServer CreateServer();
        IServer CreateCache();
        IServer CreateCDN();
        IServer CreateLoadBalancer();
        ICluster CreateCluster();
        ICluster CreateGatewayCluster();
        ICluster CreateProcessorsCluster();
        ICluster CreateDataCluster();
}
}