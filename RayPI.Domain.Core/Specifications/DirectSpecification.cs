using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public sealed class DirectSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        private Expression<Func<TAggregateRoot, bool>> _MatchingCriteria;

        /// <summary>Default constructor for Direct Specification</summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<TAggregateRoot, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
                throw new ArgumentNullException(nameof(matchingCriteria));
            this._MatchingCriteria = matchingCriteria;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            return this._MatchingCriteria;
        }
    }
}
