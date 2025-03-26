using InfraSim.Pages.Models.Health;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models.State
{
    public class IdleState : IServerState
    {
        public void Handle(IServer server)
        {
            var health = new ServerHealthCheck(server);

            if (health.IsNormal)
            {
                server.State = new NormalState();
            }
            else if (health.IsOverloaded)
            {
                server.State = new OverloadedState();
            }
            else if (health.IsFailed)
            {
                server.State = new FailedState();
            }
        }
    }
}
