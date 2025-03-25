namespace InfraSim.Pages.Models.Health
{
    public interface IServerHealthCheck
    {
        bool IsIdle { get; }
        bool IsNormal { get; }
        bool IsOverloaded { get; }
        bool IsFailed { get; }
    }
}
