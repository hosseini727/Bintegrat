using BankIntegration.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOS.Domain.Entities;

namespace BankIntegration.Infra.Repository.SQLRepository.Persistance;

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
        builder.Entity<NewPasargad_Product>(entity => entity.HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(e => e.ParentProductId));

        builder.Entity<NewPasargad_ApiProductKey>(entity => entity.HasOne(e => e.NewPasargad_Product)
            .WithMany(e => e.NewPasargad_ApiProductKey)
            .HasForeignKey(e => e.ProductId));

        base.OnModelCreating(builder);
    }
}