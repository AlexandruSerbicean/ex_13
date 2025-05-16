using InfraSim.Pages.Models.State;

public class ServerValidator : IValidatorStrategy
{
    public bool Validate(IServer server)
    {
        return server.State is not FailedState;
    }
}