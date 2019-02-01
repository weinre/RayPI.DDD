using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Interfaces
{
    public class OperateSetter : IEntityBaseAutoSetter
    {
        /// <summary>The _operate content</summary>
        private readonly IOperateContent _operateContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLC.Treasury.Interfaces.OperateSetter" /> class.
        /// </summary>
        /// <param name="operateContent">Content of the operate.</param>
        public OperateSetter(IOperateContent operateContent)
        {
            this._operateContent = operateContent;
        }

        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        public string CreateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._operateContent?.AuthBase?.Name))
                    return string.Empty;
                return this._operateContent?.AuthBase?.Name;

            }
        }

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        public long CreateId
        {
            get
            {
                return this._operateContent?.AuthBase?.Id ?? -1L;
            }
        }

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        public DateTime CreateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        public string UpdateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._operateContent?.AuthBase?.Name))
                    return string.Empty;
                return this._operateContent?.AuthBase?.Name;
            }
        }

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        public long UpdateId
        {
            get
            {
                return this._operateContent?.AuthBase?.Id ?? -1L;
            }
        }

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        public DateTime UpdateTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
