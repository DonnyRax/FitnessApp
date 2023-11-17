using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<WeightHistory> WeightHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole{ Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN" }
            );
    }
}
