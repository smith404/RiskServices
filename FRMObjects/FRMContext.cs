using FRMObjects.model;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FRMObjects
{
    public class FRMContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<ToolDefinition> ToolDefinitions { get; set; }

        public DbSet<ToolStep> ToolSteps { get; set; }

        public DbSet<ToolExecutionLog> ToolExecutionLogs { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionBuilder.GetSQLConnectionString(), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);            
        }
    }
}
