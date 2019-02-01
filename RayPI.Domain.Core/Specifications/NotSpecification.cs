using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public sealed class NotSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        private Expression<Func<TAggregateRoot, bool>> _OriginalCriteria;

        /// <summary>Constructor for NotSpecificaiton</summary>
        /// <param name="originalSpecification">Original specification</param>
        public NotSpecification(ISpecification<TAggregateRoot> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException(nameof(originalSpecification));
            this._OriginalCriteria = originalSpecification.SatisfiedBy();
        }

        /// <summary>Constructor for NotSpecification</summary>
        /// <param name="originalSpecification">Original specificaiton</param>
        public NotSpecification(Expression<Func<TAggregateRoot, bool>> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException(nameof(originalSpecification));
            this._OriginalCriteria = originalSpecification;
        }

        /// <summary>
        /// <see cref="T:NLC.Domain.Specifications.ISpecification`1" />
        /// </summary>
        /// <returns><see cref="T:NLC.Domain.Specifications.ISpecification`1" /></returns>
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TAggregateRoot, bool>>((Expression)Expression.Not(this._OriginalCriteria.Body), new ParameterExpression[1]
            {
                this._OriginalCriteria.Parameters.Single<ParameterExpression>()
            });
        }
    }
}
