using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T:class
{
    
    public Task<T> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
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