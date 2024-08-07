using BankIntegration.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOS.Domain.Entities;

namespace BankIntegration.Infra.Persistance;

public class ApplicationDbContext : IdentityDbContext<People, Role, long>
{
    public virtual DbSet<NewPasargad_Product> NewPasargad_Product { get; set; }
    public virtual DbSet<People> People { get; set; }

    public virtual DbSet<NewPasargad_ApiProductKey> NewPasargad_ApiProductKey { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<NewPasargad_ApiProductKey>()
        //     .HasOne(apk => apk.NewPasargad_Product)
        //     .WithMany(p => p.NewPasargad_ApiProductKey)
        //     .HasForeignKey(apk => apk.NewPasargad_ProductId)
        //     .HasConstraintName("FK_NewPasargad_ApiProductKey_ProductId");
        //
        // // Map NewPasargad_ProductId to ProductId column in database
        // builder.Entity<NewPasargad_ApiProductKey>()
        //     .Property(apk => apk.NewPasargad_ProductId)
        //     .HasColumnName("ProductId");
        base.OnModelCreating(builder);
    }
}