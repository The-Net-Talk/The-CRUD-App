using Microsoft.EntityFrameworkCore;
using TheCrudApp.Database.Entities;

namespace TheCrudApp.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(car =>
        {
            car.HasKey(c => c.Id);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}