using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <inheritdoc />
    /// <summary>True specification</summary>
    /// <typeparam name="TValueObject">Type of entity in this specification</typeparam>
    public sealed class TrueSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        /// <summary>
        /// 覆写
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TAggregateRoot, bool>> SatisfiedBy()
        {
            bool result = true;
            return (Expression<Func<TAggregateRoot, bool>>)(t => result);
        }
    }
}
