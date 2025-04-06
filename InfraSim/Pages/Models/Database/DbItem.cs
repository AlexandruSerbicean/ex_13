using System;

namespace InfraSim.Pages.Models.Database
{
    public abstract class DbItem : IDbItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
