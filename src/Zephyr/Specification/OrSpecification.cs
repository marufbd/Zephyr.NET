using System;
using System.Linq.Expressions;
using Zephyr.Domain;

namespace Zephyr.Specification
{
    public class OrSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _spec1;
        private readonly ISpecification<TEntity> _spec2;

        public OrSpecification(ISpecification<TEntity> s1, ISpecification<TEntity> s2)
        {
            this._spec1 = s1;
            this._spec2 = s2;
        }

        public bool IsSatisfiedBy(TEntity canditate)
        {
            return _spec1.IsSatisfiedBy(canditate) || _spec2.IsSatisfiedBy(canditate);
        }

        public Expression<Func<TEntity, bool>> Predicate
        {
            get { throw new NotImplementedException(); }
        }
    }
}