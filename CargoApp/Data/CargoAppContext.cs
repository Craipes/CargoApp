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
    public DbSet<Cargo> Cargoes { get; set; } = null!;

    public CargoAppContext(DbContextOptions<CargoAppContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Review>(review =>
        {
            review.HasKey(r => new { r.ReceiverId, r.SenderId });

            review
                .HasOne(r => r.Receiver)
                .WithMany(r => r.ReviewsReceived)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.ClientCascade);

            review
                .HasOne(r => r.Sender)
                .WithMany(r => r.ReviewsSent)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });

        builder.Entity<CarResponse>(response =>
        {
            response.HasKey(r => new { r.CarRequestId, r.UserId });

            response
                .HasOne(r => r.CarRequest)
                .WithMany(r => r.Responses)
                .HasForeignKey(r => r.CarRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            response
                .HasOne(r => r.User)
                .WithMany(r => r.CarResponses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CargoResponse>(response =>
        {
            response.HasKey(r => new { r.CargoRequestId, r.UserId });

            response
                .HasOne(r => r.CargoRequest)
                .WithMany(r => r.Responses)
                .HasForeignKey(r => r.CargoRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            response
                .HasOne(r => r.User)
                .WithMany(r => r.CargoResponses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CargoRequest>().HasOne(r => r.Car).WithOne().OnDelete(DeleteBehavior.NoAction);
        builder.Entity<CarResponse>().HasOne(r => r.Car).WithMany().OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Cargo>(cargo =>
        {
            cargo
                .HasOne<CarRequest>()
                .WithOne(r => r.Cargo)
                .HasPrincipalKey<CarRequest>(r => r.CargoId)
                .OnDelete(DeleteBehavior.ClientCascade);

            cargo
                .HasOne<CargoResponse>()
                .WithOne(r => r.Cargo)
                .HasPrincipalKey<CargoResponse>(r => r.CargoId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}
