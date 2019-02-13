using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// “非”规约
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public sealed class NotSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        private readonly Expression<Func<TAggregateRoot, bool>> _originalCriteria;

        /// <summary>构造函数</summary>
        /// <param name="originalSpecification">Original specification</param>
        public NotSpecification(ISpecification<TAggregateRoot> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException(nameof(originalSpecification));
            this._originalCriteria = originalSpecification.SatisfiedBy();
        }

        /// <summary>构造函数</summary>
        /// <param name="originalSpecification">Original specificaiton</param>
        public NotSpecification(Expression<Func<TAggregateRoot, bool>> originalSpecification)
        {
            this._originalCriteria = originalSpecification ?? throw new ArgumentNullException(nameof(originalSpecification));
        }

        /// <summary>
        /// 覆写
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TAggregateRoot, bool>>((Expression)Expression.Not(this._originalCriteria.Body), new ParameterExpression[1]
            {
                this._originalCriteria.Parameters.Single<ParameterExpression>()
            });
        }
    }
}
