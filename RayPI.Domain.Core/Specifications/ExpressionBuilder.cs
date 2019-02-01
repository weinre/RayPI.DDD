using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Compose two expression and merge all in a new expression
        /// </summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Expression instance</param>
        /// <param name="second">Expression to merge</param>
        /// <param name="merge">Function to merge</param>
        /// <returns>New merged expressions</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            Expression expression = ParameterRebinder.ReplaceParameters(first.Parameters.Select((f, i) => new
            {
                f = f,
                s = second.Parameters[i]
            }).ToDictionary(p => p.s, p => p.f), second.Body);
            return Expression.Lambda<T>(merge(first.Body, expression), (IEnumerable<ParameterExpression>)first.Parameters);
        }

        /// <summary>And operator</summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Right Expression in AND operation</param>
        /// <param name="second">Left Expression in And operation</param>
        /// <returns>New AND expression</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose<Func<T, bool>>(second, new Func<Expression, Expression, Expression>(Expression.And));
        }

        /// <summary>Or operator</summary>
        /// <typeparam name="T">Type of param in expression</typeparam>
        /// <param name="first">Right expression in OR operation</param>
        /// <param name="second">Left expression in OR operation</param>
        /// <returns>New Or expressions</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose<Func<T, bool>>(second, new Func<Expression, Expression, Expression>(Expression.Or));
        }
    }
}
