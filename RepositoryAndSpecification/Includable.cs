using System.Linq.Expressions;

namespace Demo;

public class Includable<TEntity> where TEntity : class
{
    public List<Expression<Func<TEntity, dynamic>>> Expressions { get; private set; } = [];

    public Includable<TEntity> Include(Expression<Func<TEntity, dynamic>> expression)
    {
        Expressions.Add(expression);
        return this;
    }
}
