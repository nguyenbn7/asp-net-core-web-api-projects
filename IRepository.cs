namespace GenericRepository;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();
}
