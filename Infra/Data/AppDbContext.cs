using apiserasa.infra.entities;
using Microsoft.EntityFrameworkCore;

namespace apiserasa.infra.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.OwnsOne(u => u.Email, emailOwned =>
            {
                emailOwned.Property(e => e._email).HasColumnName("Email").IsRequired();
                emailOwned.HasIndex(e => e._email).IsUnique();
            });

            entity.OwnsOne(u => u.Password, passwordOwned =>
            {
                passwordOwned.Property<string>(e => e._password).HasColumnName("Password").IsRequired();
            });
        });
    }
}
