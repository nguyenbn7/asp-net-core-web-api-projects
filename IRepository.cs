namespace GenericRepository;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    Task<List<TEntity>> FindAllAsync();
    Task<List<TEntity>> FindAllAsync(Specification<TEntity> specification);
    // Task<List<TEntity>> FindAllAsync(Specification<TEntity> specification, IOrderable);
    Task<TEntity?> FindOneByPrimaryKeyAsync(TKey key);
    Task<TEntity?> FindOneAsync(Specification<TEntity> specification);
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
