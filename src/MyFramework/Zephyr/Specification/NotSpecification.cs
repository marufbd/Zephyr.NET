namespace Zephyr.Specification
{
    public class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _spec;

        public NotSpecification(ISpecification<TEntity> s)
        {
            this._spec = s;
        }

        public bool IsSatisfiedBy(TEntity canditate)
        {
            return !_spec.IsSatisfiedBy(canditate);
        }
    }
}