using Microsoft.EntityFrameworkCore;
using MovieRentApp.Models;

namespace MovieRentApp.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Rental> Rentals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Rental>()
            .HasKey(k => new { k.MovieId, k.ClientId });

        builder.Entity<Client>()
            .HasMany(c => c.RentMovies)
            .WithOne(rm => rm.Client)
            .HasForeignKey(rm => rm.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Movie>()
            .HasMany(m => m.Clients)
            .WithOne(rm => rm.Movie)
            .HasForeignKey(rm => rm.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
