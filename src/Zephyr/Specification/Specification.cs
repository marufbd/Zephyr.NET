using System;
using System.Linq;
using System.Linq.Expressions;
using Zephyr.DesignByContract;

namespace Zephyr.Specification
{
    public class Spec<TEntity> : ISpecification<TEntity>
    {
        private Expression<Func<TEntity, bool>> _predicate;

        /// <summary>
        /// Gets the predicate representing the current Spec
        /// </summary>
        public Expression<Func<TEntity, bool>> Predicate { get { return _predicate; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spec&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public Spec(Expression<Func<TEntity, bool>> predicate)
        {
            Check.Require(predicate!=null, "Predicate expression for Spec cannot be null");
            _predicate = predicate; 
        }


        #region Operator overloads

        /// <summary>
        /// Overloads the &amp; operator and combines two <see cref="Spec{TEntity}"/> in a Boolean And expression
        /// and returns a new see cref="Spec{TEntity}"/>.
        /// </summary>
        /// <param name="leftHand">The left hand <see cref="Spec{TEntity}"/> to combine.</param>
        /// <param name="rightHand">The right hand <see cref="Spec{TEntity}"/> to combine.</param>
        /// <returns>The combined <see cref="Spec{TEntity}"/> instance.</returns>
        public static Spec<TEntity> operator &(Spec<TEntity> leftHand, Spec<TEntity> rightHand)
        {
            InvocationExpression rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                                 leftHand.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftHand.Predicate.Body,
                                                                   rightInvoke);
            return new Spec<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

        /// <summary>
        /// Overloads the &amp; operator and combines two <see cref="Spec{TEntity}"/> in a Boolean Or expression
        /// and returns a new see cref="Spec{TEntity}"/>.
        /// </summary>
        /// <param name="leftHand">The left hand <see cref="Spec{TEntity}"/> to combine.</param>
        /// <param name="rightHand">The right hand <see cref="Spec{TEntity}"/> to combine.</param>
        /// <returns>The combined <see cref="Spec{TEntity}"/> instance.</returns>
        public static Spec<TEntity> operator |(Spec<TEntity> leftHand, Spec<TEntity> rightHand)
        {
            InvocationExpression rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                                 leftHand.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftHand.Predicate.Body,
                                                                   rightInvoke);
            return new Spec<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

        #endregion

        #region Implementation of ISpec<TEntity>

        public bool IsSatisfiedBy(TEntity canditate)
        {
            return Predicate.Compile().Invoke(canditate);
        }

        #endregion
    }
}