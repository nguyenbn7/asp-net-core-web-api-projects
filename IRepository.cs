namespace GenericRepository;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey key);
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Number of entity has been saved to database</returns>
    Task<int> AddOrUpdateAsync(TEntity entity);
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Number of entity has been removed from database</returns>
    Task<int> DeleteAsync(TEntity entity);
}
