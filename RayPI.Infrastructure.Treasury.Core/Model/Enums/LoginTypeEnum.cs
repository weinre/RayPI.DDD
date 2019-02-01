using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Model.Enums
{
    public enum LoginTypeEnum
    {
        /// <summary>用户(PC端)</summary>
        PcUser,
        /// <summary>用户(移动端)</summary>
        MobileUser,
        /// <summary>设备</summary>
        Device,
        /// <summary>子系统</summary>
        SubSystem,
    }
}
