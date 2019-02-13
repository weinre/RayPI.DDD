using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// 复合规约抽象基类
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public abstract class CompositeSpecification<TAggregateRoot> : Specification<TAggregateRoot> where TAggregateRoot : class
    {
        /// <summary>Left side specification for this composite element</summary>
        public abstract ISpecification<TAggregateRoot> LeftSideSpecification { get; }

        /// <summary>Right side specification for this composite element</summary>
        public abstract ISpecification<TAggregateRoot> RightSideSpecification { get; }
    }
}
