using BankIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.Infra.Persistance;

public class ApplicationDbContext :DbContext
{
    public virtual DbSet<NewPasargad_Product> NewPasargad_Product  { get; set; }
    public virtual DbSet<NewPasargad_ApiProductKey> NewPasargad_ApiProductKey { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}