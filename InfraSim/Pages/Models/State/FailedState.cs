using InfraSim.Pages.Models.Health;

namespace InfraSim.Pages.Models.State
{
    public class FailedState : IServerState
    {
        public void Handle(IServer server)
        {
            var health = new ServerHealthCheck(server);

            if (health.IsIdle)
            {
                server.State = new IdleState();
            }
            else if (health.IsNormal)
            {
                server.State = new NormalState();
            }
            else if (health.IsOverloaded)
            {
                server.State = new OverloadedState();
            }
        }
    }
}
