using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// 规约抽象基类
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public abstract class Specification<TAggregateRoot> : ISpecification<TAggregateRoot> where TAggregateRoot : class
    {
        /// <summary>IsSatisFied Specification pattern method,</summary>
        /// <returns>Expression that satisfy this specification</returns>
        public abstract Expression<Func<TAggregateRoot, bool>> SatisfiedBy();

        /// <summary>“与”运算符</summary>
        /// <param name="leftSideSpecification">left operand in this AND operation</param>
        /// <param name="rightSideSpecification">right operand in this AND operation</param>
        /// <returns>New specification</returns>
        public static Specification<TAggregateRoot> operator &(Specification<TAggregateRoot> leftSideSpecification, Specification<TAggregateRoot> rightSideSpecification)
        {
            return (Specification<TAggregateRoot>)new AndSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)leftSideSpecification, (ISpecification<TAggregateRoot>)rightSideSpecification);
        }

        /// <summary>“或”运算符</summary>
        /// <param name="leftSideSpecification">left operand in this OR operation</param>
        /// <param name="rightSideSpecification">left operand in this OR operation</param>
        /// <returns>New specification </returns>
        public static Specification<TAggregateRoot> operator |(Specification<TAggregateRoot> leftSideSpecification, Specification<TAggregateRoot> rightSideSpecification)
        {
            return (Specification<TAggregateRoot>)new OrSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)leftSideSpecification, (ISpecification<TAggregateRoot>)rightSideSpecification);
        }

        /// <summary>“非”运算符</summary>
        /// <param name="specification">Specification to negate</param>
        /// <returns>New specification</returns>
        public static Specification<TAggregateRoot> operator !(Specification<TAggregateRoot> specification)
        {
            return (Specification<TAggregateRoot>)new NotSpecification<TAggregateRoot>((ISpecification<TAggregateRoot>)specification);
        }

        /// <summary>
        /// 重载false运算符, 仅支持AND和OR运算符
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<TAggregateRoot> specification)
        {
            return false;
        }

        /// <summary>
        /// 重载true运算符, 仅支持AND和OR运算符
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See True operator in C#</returns>
        public static bool operator true(Specification<TAggregateRoot> specification)
        {
            return true;
        }
    }
}
