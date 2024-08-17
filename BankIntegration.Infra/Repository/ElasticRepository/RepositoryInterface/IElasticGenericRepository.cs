namespace BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;

public interface IElasticGenericRepository<T>
{
    Task<IEnumerable<string>> Index(IEnumerable<T> documents);
    Task<string> SingleDocument(T document);
    Task<T> Get(string id);
    Task<bool> Update(T document, string id);
    Task<bool> Delete(string id);
}