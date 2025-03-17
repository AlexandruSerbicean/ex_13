namespace InfraSim.Pages.Models;

public interface IServer
{
    ServerType ServerType { get; }
    void HandleRequests(int requestsCount);  
}
