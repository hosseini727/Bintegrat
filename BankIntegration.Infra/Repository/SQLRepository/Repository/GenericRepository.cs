using System.Diagnostics;
using BankIntegration.Domain.Entities;
using BankIntegration.Infra.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_dbSet));
    }

    public async Task<T?> GetById(long id)
    {
        var result = await _dbSet.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        try
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<T> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }
    public T Update(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> Delete(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return null!;
        }

        _dbSet.Remove(entity);
        return entity;
    }
}