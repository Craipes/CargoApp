using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Data;

public class CargoAppContext : IdentityDbContext<User>
{
    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<CarRequest> CarRequests { get; set; } = null!;
    public DbSet<CarResponse> CarResponses { get; set; } = null!;
    public DbSet<CargoRequest> CargoRequests { get; set; } = null!;
    public DbSet<CargoResponse> CargoResponses { get; set; } = null!;

    public CargoAppContext(DbContextOptions<CargoAppContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Review>()
            .HasOne(r => r.Receiver)
            .WithMany(r => r.Reviews)
            .HasForeignKey(r => r.ReceiverId);

        builder.Entity<User>()
            .HasIndex(u => u.PhoneNumber)
            .IsUnique();

        base.OnModelCreating(builder);
    }
}
