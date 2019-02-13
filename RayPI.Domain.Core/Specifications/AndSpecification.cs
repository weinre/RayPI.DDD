using System;
using System.Linq.Expressions;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// “与”规约
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AndSpecification<T> : CompositeSpecification<T> where T : class
    {
        private readonly ISpecification<T> _rightSideSpecification;
        private readonly ISpecification<T> _leftSideSpecification;

        /// <summary>构造函数</summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            this._leftSideSpecification = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
            this._rightSideSpecification = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
        }

        /// <inheritdoc />
        /// <summary>Left side specification</summary>
        public override ISpecification<T> LeftSideSpecification => this._leftSideSpecification;

        /// <inheritdoc />
        /// <summary>Right side specification</summary>
        public override ISpecification<T> RightSideSpecification => this._rightSideSpecification;

        /// <summary>
        /// 覆写
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return this._leftSideSpecification.SatisfiedBy().And<T>(this._rightSideSpecification.SatisfiedBy());
        }
    }
}
