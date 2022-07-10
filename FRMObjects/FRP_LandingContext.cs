using FRMObjects.model;
using Microsoft.EntityFrameworkCore;

namespace FRMObjects
{
    public partial class FRP_LandingContext : DbContext
    {
        public FRP_LandingContext()
        {
        }

        public FRP_LandingContext(DbContextOptions<FRP_LandingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HubPage> HubPages { get; set; } = null!;
        public virtual DbSet<HubPageSection> HubPageSections { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<SearchItem> SearchItems { get; set; } = null!;
        public virtual DbSet<ToolDefinition> ToolDefinitions { get; set; } = null!;
        public virtual DbSet<ToolExecutionLog> ToolExecutionLogs { get; set; } = null!;
        public virtual DbSet<ToolStep> ToolSteps { get; set; } = null!;
        public virtual DbSet<ToolStepConfig> ToolStepConfigs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionBuilder.GetSQLConnectionString(), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HubPage>(entity =>
            {
                entity.ToTable("HubPage");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasMaxLength(36);

                entity.Property(e => e.Headline).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<HubPageSection>(entity =>
            {
                entity.ToTable("HubPageSection");

                entity.Property(e => e.Headline).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(255);

                entity.Property(e => e.Owner).HasMaxLength(255);

                entity.Property(e => e.ProcessedOn).HasColumnType("datetime");

                entity.Property(e => e.Sender).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<SearchItem>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Target });

                entity.ToTable("SearchItem");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Target).HasMaxLength(255);

                entity.Property(e => e.Domain).HasMaxLength(10);

                entity.Property(e => e.Summary).HasMaxLength(90);

                entity.Property(e => e.TargetType).HasMaxLength(12);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<ToolDefinition>(entity =>
            {
                entity.ToTable("ToolDefinition");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))")
                    .IsFixedLength();

                entity.Property(e => e.ToolName).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<ToolExecutionLog>(entity =>
            {
                entity.ToTable("ToolExecutionLog");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasMaxLength(36);

                entity.Property(e => e.RequestedTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Requestor)
                    .HasMaxLength(4000)
                    .HasComputedColumnSql("(original_login())", false);

                entity.Property(e => e.RunEndTimestamp).HasColumnType("datetime");

                entity.Property(e => e.RunStartTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ToolStep>(entity =>
            {
                entity.ToTable("ToolStep");

                entity.Property(e => e.DatasetName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Format)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Message).HasMaxLength(255);

                entity.Property(e => e.StepType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ToolStepConfig>(entity =>
            {
                entity.HasKey(e => new { e.ToolDefinitionId, e.ToolStepId });

                entity.ToTable("ToolStepConfig");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
