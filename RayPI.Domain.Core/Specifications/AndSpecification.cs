using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RayPI.Domain.Core.Specifications
{
    public sealed class AndSpecification<T> : CompositeSpecification<T> where T : class
    {
        private ISpecification<T> _RightSideSpecification;
        private ISpecification<T> _LeftSideSpecification;

        /// <summary>Default constructor for AndSpecification</summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException(nameof(leftSide));
            if (rightSide == null)
                throw new ArgumentNullException(nameof(rightSide));
            this._LeftSideSpecification = leftSide;
            this._RightSideSpecification = rightSide;
        }

        /// <summary>Left side specification</summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get
            {
                return this._LeftSideSpecification;
            }
        }

        /// <summary>Right side specification</summary>
        public override ISpecification<T> RightSideSpecification
        {
            get
            {
                return this._RightSideSpecification;
            }
        }

        /// <summary>
        /// <see cref="T:NLC.Domain.Specifications.ISpecification`1" />
        /// </summary>
        /// <returns><see cref="T:NLC.Domain.Specifications.ISpecification`1" /></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return this._LeftSideSpecification.SatisfiedBy().And<T>(this._RightSideSpecification.SatisfiedBy());
        }
    }
}
