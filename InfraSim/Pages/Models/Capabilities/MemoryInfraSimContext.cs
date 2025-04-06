using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace InfraSim.Pages.Models.Database
{
    public class MemoryInfraSimContext : InfraSimContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            SqliteConnection connection = new("DataSource=:memory:");
            connection.Open();

            options.UseSqlite(connection);
        }
    }
}
