using System.Linq.Expressions;
using Nest;

namespace BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;

public interface IElasticGenericRepository<T> where T : class
{
    Task<IEnumerable<string>> Index(IEnumerable<T> documents);
    Task<string> SingleDocument(T document);
    Task<T> Get(string id);
    Task<bool> Update(T document, string id);
    Task<bool> Delete(string id);
    Task<IEnumerable<T>> SearchByField(string fieldName, string fieldValue);
    Task<IEnumerable<T>> FullTextSearch(string searchText);
    Task<IEnumerable<T>> SearchWithFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filter);
}