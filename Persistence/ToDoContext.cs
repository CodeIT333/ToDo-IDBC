using Domain.ToDoItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class ToDoContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // for runtime config
        public ToDoContext(DbContextOptions<ToDoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStr = _configuration.GetConnectionString("DefaultConnection") ??
                    "Server=localhost;Database=ToDoDB;Trusted_Connection=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionStr);
            }
        }

        public DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetEntityKeys(modelBuilder);
            ConfigureEntities(modelBuilder);
        }

        private void SetEntityKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasKey(i => i.Id); // add pk
            });
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .Property(i => i.Priority)
                .HasConversion<byte>();  // enum <=> byte
        }
    }
}
