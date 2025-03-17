using System.Linq.Expressions;

namespace Demo;

public sealed class Ordered<TEntity> where TEntity : class
{
    public List<OrderedWrapper<TEntity>> Expressions { get; private set; } = [];

    public Ordered<TEntity> By(Expression<Func<TEntity, dynamic>> expression, bool ascending = true)
    {
        Expressions.Add(new(expression, ascending));
        return this;
    }
}

public sealed class OrderedWrapper<TEntity>(Expression<Func<TEntity, dynamic>> orderExpression, bool ascending = true)
{
    public bool Ascending { get; private set; } = ascending;
    public Expression<Func<TEntity, dynamic>> OrderExpression { get; private set; } = orderExpression;
}
