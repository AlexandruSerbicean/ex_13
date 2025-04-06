using System;

namespace InfraSim.Pages.Models.Database
{
    public interface IDbItem
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
