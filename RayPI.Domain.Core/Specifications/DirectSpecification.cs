using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <inheritdoc />
    /// <summary>
    /// A Direct Specification is a simple implementation of specification that acquire this from a lambda expression in constructor
    /// </summary>
    /// <typeparam name="TValueObject">Type of entity that check this specification</typeparam>
    public sealed class DirectSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        private readonly Expression<Func<TAggregateRoot, bool>> _matchingCriteria;

        /// <summary>Default constructor for Direct Specification</summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<TAggregateRoot, bool>> matchingCriteria)
        {
            this._matchingCriteria = matchingCriteria ?? throw new ArgumentNullException(nameof(matchingCriteria));
        }

        /// <inheritdoc />
        /// <summary>
        /// 覆写
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            return this._matchingCriteria;
        }
    }
}
