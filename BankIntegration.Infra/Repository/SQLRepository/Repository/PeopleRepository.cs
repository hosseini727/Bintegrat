using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.Repository;
using SOS.Domain.Entities;
using SOS.Infrastructure.Repository.Interface;

public class PeopleRepository : GenericRepository<People> , IPeopleRepository
{
    public PeopleRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}