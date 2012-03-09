using Zephyr.Domain;

namespace Zephyr.Specification
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity canditate);
    }
}