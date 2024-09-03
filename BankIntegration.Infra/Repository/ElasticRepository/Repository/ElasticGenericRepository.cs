using BankIntegration.Infra.Repository.ElasticRepository.RepositoryInterface;
using Nest;

namespace BankIntegration.Infra.Repository.ElasticRepository.Repository;

public class ElasticGenericRepository<T> : IElasticGenericRepository<T> where T : class
{
    private readonly IElasticClient _elasticClient;

    public ElasticGenericRepository(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<IEnumerable<string>> Index(IEnumerable<T> documents)
    {
        var indexName = typeof(T).Name.ToLower();
        var indexResponse = await _elasticClient.Indices.ExistsAsync(indexName);
        if (!indexResponse.Exists)
            await _elasticClient.Indices.CreateAsync(indexName, i => i.Map<T>(x => x.AutoMap()));
        var response = await _elasticClient.IndexManyAsync(documents);
        return response.Items.Select(x => x.Id);
    }

    public async Task<string> SingleDocument(T document)
    {
        var indexName = typeof(T).Name.ToLower();
        var indexResponse = await _elasticClient.Indices.ExistsAsync(indexName);
        if (!indexResponse.Exists)
            await _elasticClient.Indices.CreateAsync(indexName, i => i.Map<T>(x => x.AutoMap()));
        var response = await _elasticClient.IndexAsync(document, i => i.Index(indexName));
        return response.Id;
    }

    public async Task<T> Get(string id)
    {
        var response = await _elasticClient.GetAsync<T>(id, i => i.Index(typeof(T).Name.ToLower()));
        return response.Source;
    }

    public async Task<bool> Update(T document, string id)
    {
        var response = await _elasticClient.UpdateAsync<T>(id, u => u.Doc(document));
        return response.IsValid;
    }

    public async Task<bool> Delete(string id)
    {
        var response = await _elasticClient.DeleteAsync<T>(id);
        return response.IsValid;
    }

    public async Task<IEnumerable<T>> SearchByField(string fieldName, string fieldValue)
    {
        var indexName = typeof(T).Name.ToLower();
        var searchResponse = await _elasticClient.SearchAsync<T>(search =>
            search.Index(indexName)
                .Query(query =>
                    query.Term(term =>
                        term.Field(fieldName)
                            .Value(fieldValue)))
        );
        return searchResponse.Documents;
    }

    public async Task<IEnumerable<T>> FullTextSearch(string searchText)
    {
        var indexName = typeof(T).Name.ToLower();
        var searchResponse = await _elasticClient.SearchAsync<T>(
            search => search.Index(indexName)
                .Query(query => query.MultiMatch(
                    m => m.Query(searchText)
                        .Fuzziness(Fuzziness.Auto)))
        );
        return searchResponse.Documents;
    }
    
    public async Task<IEnumerable<T>> SearchWithFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filter)
    {
        var indexName = typeof(T).Name.ToLower();
        var searchResponse = await _elasticClient.SearchAsync<T>(s => s
            .Index(indexName)
            .Query(filter)
        );

        return searchResponse.Documents;
    }
}