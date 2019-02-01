using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public sealed class TrueSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            bool result = true;
            return (Expression<Func<TAggregateRoot, bool>>)(t => result);
        }
    }
}
