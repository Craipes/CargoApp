using CargoApp.Models;

namespace CargoApp.Data;

public class CargoAppContext : IdentityDbContext<User>
{
    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<CarRequest> CarRequests { get; set; } = null!;
    public DbSet<CarResponse> CarResponses { get; set; } = null!;
    public DbSet<CargoRequest> CargoRequests { get; set; } = null!;
    public DbSet<CargoResponse> CargoResponses { get; set; } = null!;

    public DbSet<Settlement> Settlements { get; set; } = null!;

    public CargoAppContext(DbContextOptions<CargoAppContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Base options
        builder.Entity<Review>(review =>
        {
            review
                .HasOne(r => r.Receiver)
                .WithMany(r => r.ReviewsReceived)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            review
                .HasOne(r => r.Sender)
                .WithMany(r => r.ReviewsSent)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CarResponse>(response =>
        {
            response.HasKey(r => new { r.CarRequestId, r.DriverId });

            response
                .HasOne(r => r.CarRequest)
                .WithMany(r => r.Responses)
                .HasForeignKey(r => r.CarRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            response
                .HasOne(r => r.Driver)
                .WithMany(r => r.CarResponses)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CargoResponse>(response =>
        {
            response.HasKey(r => new { r.CargoRequestId, r.SenderId });

            response
                .HasOne(r => r.CargoRequest)
                .WithMany(r => r.Responses)
                .HasForeignKey(r => r.CargoRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            response
                .HasOne(r => r.Sender)
                .WithMany(r => r.CargoResponses)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CarRequest>(request =>
        {
            request
                .HasOne(r => r.DeparturePlace)
                .WithMany()
                .HasForeignKey(r => r.DeparturePlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            request
                .HasOne(r => r.DestinationPlace)
                .WithMany()
                .HasForeignKey(r => r.DestinationPlaceId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CargoRequest>(request =>
        {
            request
                .HasOne(r => r.DeparturePlace)
                .WithMany()
                .HasForeignKey(r => r.DeparturePlaceId)
                .OnDelete(DeleteBehavior.NoAction);

            request
                .HasOne(r => r.DestinationPlace)
                .WithMany()
                .HasForeignKey(r => r.DestinationPlaceId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<User>(user =>
        {
            user.HasIndex(u => u.PhoneNumber).IsUnique();
            user.ToTable("Users");
        });

        builder.Entity<Settlement>(settlement =>
        {
            settlement.HasIndex(l => l.Region);
            settlement.HasIndex(l => l.District);
            settlement.HasIndex(l => l.City);
            settlement.HasIndex(l => l.CityRegion);
        });
    }
}
