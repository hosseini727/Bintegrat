using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T:class
{
    private readonly ApplicationDbContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetById(long id)
    {
        var result = await _dbSet.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var result = await _dbSet.ToListAsync();
        return result;
    }

    public Task<T> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public T Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(long id)
    {
        throw new NotImplementedException();
    }
}