using System.Linq;
using InfraSim.Pages.Models;
using InfraSim.Pages.Models.State;

namespace InfraSim.Pages.Models.Validator
{
    public class ServerValidator : IValidatorStrategy
    {
        public bool Validate(IServer server)
        {
            return server.State is not FailedState;
        }
    }
}