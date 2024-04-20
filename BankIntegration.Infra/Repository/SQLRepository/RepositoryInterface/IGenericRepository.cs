namespace BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(long id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Add(T entity);
    T Update(T entity);
    Task<T> Delete(long id);
}