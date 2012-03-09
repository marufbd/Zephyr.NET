using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zephyr.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Linq query extension for OrderBy using column name as string
        /// </summary>
        /// <param name="source"></param>        
        /// <param name="orderPropertyName"> </param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderPropertyName, params object[] values)
        {            
            return GeneralOrderBy(source, orderPropertyName, "ASC");
        }


        /// <summary>
        /// Linq query extension for OrderByDescending using column name as string
        /// </summary>
        /// <param name="source"></param>        
        /// <param name="orderPropertyName"> </param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string orderPropertyName, params object[] values)
        {
            return GeneralOrderBy(source, orderPropertyName, "DESC");
        }

        private static IQueryable<T> GeneralOrderBy<T>(IQueryable<T> source, string propertyName, string sortOrder)
        {
            //http://msdn.microsoft.com/en-us/library/bb882637.aspx

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new InvalidOperationException(string.Format("Could not find a property called '{0}' on type {1}",
                                                                  propertyName, type));
            }

            var methodToInvoke = sortOrder.Equals("ASC") ? "OrderBy" : "OrderByDescending";
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodToInvoke,
                                                             new Type[] { type, property.PropertyType }, source.Expression,
                                                             Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }


        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> sourceList, string propertyName)
        {
            return sourceList.AsQueryable<T>().OrderBy(propertyName);
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> sourceList, string propertyName)
        {
            return sourceList.AsQueryable<T>().OrderByDescending(propertyName);
        }

    }
}