namespace InfraSim.Pages.Models.Validator
{
    public class DataValidator : IValidatorStrategy
    {
        public bool Validate(IServer server)
        {
            return server.ServerType == ServerType.Database;
        }
    }
}
