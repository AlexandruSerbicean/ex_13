namespace InfraSim.Pages.Models.UI
{
    /// <summary>
    /// Plain DTO the Blazor UI binds to.
    /// </summary>
    public interface IServerInfo
    {
        string Name        { get; }
        string ImageUrl    { get; }
        string StatusColor { get; }
    }
}
