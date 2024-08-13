using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

public class LocationContext : DbContext
{
    public LocationContext(DbContextOptions<LocationContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Coordinates)
                  .HasConversion(
                      v => v == null ? null : $"{v.X},{v.Y}",
                      v => v == null ? null : ParsePoint(v));

        });
    }

    private Point ParsePoint(string value)
    {
        var parts = value.Split(',');
        return new Point(double.Parse(parts[0]), double.Parse(parts[1]));
    }

}