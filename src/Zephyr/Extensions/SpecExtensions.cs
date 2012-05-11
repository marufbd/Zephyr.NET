using System;
using System.Linq.Expressions;
using Zephyr.Domain;
using Zephyr.Specification;

namespace Zephyr.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ISpecification{TEntity}"/>.
    /// </summary>
    public static class SpecExtensions
    {
        /// <summary>
        /// Retuns a new specification adding this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns></returns>
        public static ISpecification<T> And<T>(this ISpecification<T> rightHand, ISpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate, leftHand.Predicate.Parameters);
            var newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftHand.Predicate.Body, rightInvoke);
            return new Spec<T>(Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters));
        }

        /// <summary>
        /// Retuns a new specification or'ing this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns></returns>
        public static ISpecification<T> Or<T>(this ISpecification<T> rightHand, ISpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate, leftHand.Predicate.Parameters);
            var newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftHand.Predicate.Body, rightInvoke);
            return new Spec<T>(Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters));
        }
    }
}