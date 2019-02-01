using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public abstract class Specification<TAggregateRoot> : ISpecification<TAggregateRoot> where TAggregateRoot : class
    {
        /// <summary>IsSatisFied Specification pattern method,</summary>
        /// <returns>Expression that satisfy this specification</returns>
        public abstract Expression<Func<TAggregateRoot, bool>> SatisfiedBy();

        /// <summary>And operator</summary>
        /// <param name="leftSideSpecification">left operand in this AND operation</param>
        /// <param name="rightSideSpecification">right operand in this AND operation</param>
        /// <returns>New specification</returns>
        public static Specification<TAggregateRoot> operator &(Specification<TAggregateRoot> leftSideSpecification, Specification<TAggregateRoot> rightSideSpecification)
        {
            return (Specification<TAggregateRoot>)new AndSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)leftSideSpecification, (ISpecification<TAggregateRoot>)rightSideSpecification);
        }

        /// <summary>Or operator</summary>
        /// <param name="leftSideSpecification">left operand in this OR operation</param>
        /// <param name="rightSideSpecification">left operand in this OR operation</param>
        /// <returns>New specification </returns>
        public static Specification<TAggregateRoot> operator |(Specification<TAggregateRoot> leftSideSpecification, Specification<TAggregateRoot> rightSideSpecification)
        {
            return (Specification<TAggregateRoot>)new OrSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)leftSideSpecification, (ISpecification<TAggregateRoot>)rightSideSpecification);
        }

        /// <summary>Not specification</summary>
        /// <param name="specification">Specification to negate</param>
        /// <returns>New specification</returns>
        public static Specification<TAggregateRoot> operator !(Specification<TAggregateRoot> specification)
        {
            return (Specification<TAggregateRoot>)new NotSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)specification);
        }

        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<TAggregateRoot> specification)
        {
            return false;
        }

        /// <summary>
        /// Override operator True, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See True operator in C#</returns>
        public static bool operator true(Specification<TAggregateRoot> specification)
        {
            return true;
        }
    }
}
