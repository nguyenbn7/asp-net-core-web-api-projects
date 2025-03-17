using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public class Repository<TEntity, TKey>(DbContext dbContext) : IRepository<TEntity, TKey> where TEntity : class
{
    private readonly DbContext _dbContext = dbContext;
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public Task<int> AddOrUpdateAsync(TEntity entity)
    {
        _dbContext.Add(entity);
        return _dbContext.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(TEntity entity)
    {
        _dbContext.Remove(entity);
        return _dbContext.SaveChangesAsync();
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet.AsNoTracking().ToListAsync();
    }

    public Task<TEntity?> GetByIdAsync(TKey key)
    {
        var primaryKeyName = GetPrimaryKeyName();
        if (primaryKeyName is null)
            return Task.FromResult<TEntity?>(null);

        var predicate = FindByPrimaryKey(primaryKeyName, key);
        return _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    private Expression<Func<TEntity, bool>> FindByPrimaryKey(string primaryKeyName, TKey key)
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var left = Expression.Property(parameter, primaryKeyName);
        var right = Expression.Property(Expression.Constant(new { key }), nameof(key));
        var body = Expression.Equal(left, right);
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }

    private string? GetPrimaryKeyName()
    {
        return _dbSet.EntityType.FindPrimaryKey()?.Properties.Select(x => x.Name).Single();
    }
}
