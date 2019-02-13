using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// 规约Interface
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public interface ISpecification<TAggregateRoot> where TAggregateRoot : class
    {
        Expression<Func<TAggregateRoot, bool>> SatisfiedBy();
    }
}
