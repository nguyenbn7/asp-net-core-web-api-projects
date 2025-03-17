using System.Linq.Expressions;

namespace GenericRepository;

// https://en.wikipedia.org/wiki/Specification_pattern
public abstract class Specification<TEntity> where TEntity : class
{
    public abstract Expression<Func<TEntity, bool>> IsSatisfiedBy();
    public Specification<TEntity> And(Specification<TEntity> other) => new AndSpecification<TEntity>(this, other);
    public Specification<TEntity> AndNot(Specification<TEntity> other) => new AndNotSpecification<TEntity>(this, other);
    public Specification<TEntity> Or(Specification<TEntity> other) => new OrSpecification<TEntity>(this, other);
    public Specification<TEntity> OrNot(Specification<TEntity> other) => new OrNotSpecification<TEntity>(this, other);
    public Specification<TEntity> Not() => new NotSpecification<TEntity>(this);
}

public sealed class AndSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    private readonly Specification<TEntity> _left;
    private readonly Specification<TEntity> _right;

    public AndSpecification(Specification<TEntity> left,
                            Specification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
    {
        var @param = Expression.Parameter(typeof(TEntity), "x");
        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.AndAlso(
                Expression.Invoke(_left.IsSatisfiedBy(), @param),
                Expression.Invoke(_right.IsSatisfiedBy(), @param)),
            @param
        );
    }
}

public sealed class AndNotSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    private readonly Specification<TEntity> _left;
    private readonly Specification<TEntity> _right;

    public AndNotSpecification(Specification<TEntity> left,
                               Specification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
    {
        var @param = Expression.Parameter(typeof(TEntity), "x");
        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.AndAlso(
                Expression.Invoke(_left.IsSatisfiedBy(), @param),
                Expression.Invoke(Expression.Not(_right.IsSatisfiedBy()), @param)),
            @param
        );
    }
}

public sealed class OrSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    private readonly Specification<TEntity> _left;
    private readonly Specification<TEntity> _right;

    public OrSpecification(Specification<TEntity> left,
                           Specification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
    {
        var @param = Expression.Parameter(typeof(TEntity), "x");
        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.OrElse(
                Expression.Invoke(_left.IsSatisfiedBy(), @param),
                Expression.Invoke(_right.IsSatisfiedBy(), @param)),
            @param
        );
    }
}

public sealed class OrNotSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    private readonly Specification<TEntity> _left;
    private readonly Specification<TEntity> _right;

    public OrNotSpecification(Specification<TEntity> left,
                              Specification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
    {
        var @param = Expression.Parameter(typeof(TEntity), "x");
        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.OrElse(
                Expression.Invoke(_left.IsSatisfiedBy(), @param),
                Expression.Invoke(Expression.Not(_right.IsSatisfiedBy()), @param)),
            @param
        );
    }
}

public sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
{
    private readonly Specification<TEntity> _other;

    public NotSpecification(Specification<TEntity> other)
    {
        _other = other;
    }

    public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
    {
        return Expression.Lambda<Func<TEntity, bool>>(
            Expression.Not(_other.IsSatisfiedBy()), _other.IsSatisfiedBy().Parameters[0]
        );
    }
}
