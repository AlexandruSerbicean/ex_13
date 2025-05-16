using System.Linq;
using InfraSim.Pages.Models;
public interface IValidatorStrategy
{
    bool Validate(IServer server);
}
