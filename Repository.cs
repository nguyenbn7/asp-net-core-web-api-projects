
namespace GenericRepository;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    
    public Task<IReadOnlyCollection<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
