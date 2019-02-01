﻿using RayPI.Infrastructure.Treasury.Core.Interfaces;
using System;

namespace RayPI.Domain.Core.Entity
{
    /// <summary>实体基类</summary>
    public class EntityBase
    {
        /// <summary>主键</summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }
        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        public string CreateName { get; set; } = string.Empty;
        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        public long? CreateId { get; set; }
        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        public DateTime? CreateTime { get; set; }
        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        public string UpdateName { get; set; } = string.Empty;
        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        public long? UpdateId { get; set; }
        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        public DateTime? UpdateTime { get; set; }
        /// <summary>数据状态</summary>
        /// <value>The state of the data.</value>
        public bool IsDeleted { get; set; }
        /// <summary>基础字段设置方法</summary>
        /// <value>The automatic setter.</value>
        public IEntityBaseAutoSetter AutoSetter { get; set; }

    }
}
