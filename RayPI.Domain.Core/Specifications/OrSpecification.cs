using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    /// <summary>
    /// “或”规约
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        private readonly ISpecification<T> _rightSideSpecification;
        private readonly ISpecification<T> _leftSideSpecification;

        /// <summary>构造函数</summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            this._leftSideSpecification = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
            this._rightSideSpecification = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
        }

        /// <summary>Left side specification</summary>
        public override ISpecification<T> LeftSideSpecification => this._leftSideSpecification;

        /// <summary>Righ side specification</summary>
        public override ISpecification<T> RightSideSpecification => this._rightSideSpecification;

        /// <inheritdoc />
        /// <summary>
        /// 覆写
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return this._leftSideSpecification.SatisfiedBy().Or<T>(this._rightSideSpecification.SatisfiedBy());
        }
    }
}
