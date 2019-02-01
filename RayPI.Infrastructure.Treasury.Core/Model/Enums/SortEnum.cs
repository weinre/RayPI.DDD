using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Model.Enums
{
    public enum SortEnum
    {
        /// <summary>不做任何改变，保留原有顺序</summary>
        Original = -1,
        /// <summary>升序</summary>
        Asc = 0,
        /// <summary>降序</summary>
        Desc = 1,
    }
}
