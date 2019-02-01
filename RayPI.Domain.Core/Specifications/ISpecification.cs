using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public interface ISpecification<TAggregateRoot> where TAggregateRoot : class
    {
        Expression<Func<TAggregateRoot, bool>> SatisfiedBy();
    }
}
