using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace STC
{
    public class AppContext : DbContext
    {
        public DbSet<User> UserSet { get; set; }
        public DbSet<Post> PostSet { get; set; }
        public DbSet<Thread> ThreadSet { get; set; }

        public AppContext() : base()
        {
            // Config
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "database.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}