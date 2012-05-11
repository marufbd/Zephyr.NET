using System;
using System.Linq.Expressions;
using Zephyr.Domain;

namespace Zephyr.Specification
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity canditate);

        Expression<Func<TEntity, bool>> Predicate { get; }
    }
}