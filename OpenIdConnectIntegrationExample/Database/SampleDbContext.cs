using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OpenIdConnectIntegrationExample.Models;
using System.Text;

namespace OpenIdConnectIntegrationExample.Database
{
    public class SampleDbContext : DbContext
    {
        private readonly string _connectionString = null;

        public DbSet<OIDCConfiguration> OIDCConfigurations { get; set; }

        public SampleDbContext() { }

        public SampleDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OIDCConfiguration>().HasKey(c => c.Id);
            builder.Entity<OIDCConfiguration>().Property(c => c.Id).HasColumnType("varchar(36)");
            builder.Entity<OIDCConfiguration>().Property(c => c.authority).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.audience).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.callback_uri).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.certificatePassword).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.certificateSerial).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.client_id).HasColumnType("varchar(36)");
            builder.Entity<OIDCConfiguration>().Property(c => c.response_type).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.redirect_uri).HasColumnType("varchar(256)");
            builder.Entity<OIDCConfiguration>().Property(c => c.scope).HasColumnType("varchar(256)");

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                var connection = new SqliteConnection(_connectionString);
                optionsBuilder.UseSqlite(connection)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
            }
        }
    }
}
