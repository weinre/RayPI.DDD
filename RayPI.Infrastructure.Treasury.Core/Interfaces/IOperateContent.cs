using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Treasury.Core.Model;

namespace RayPI.Infrastructure.Treasury.Core.Interfaces
{
    public interface IOperateContent
    {
        /// <summary>验证权限</summary>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckBaseAuth(string token);

        /// <summary>登录人信息</summary>
        /// <value>The authentication base.</value>
        AuthBase AuthBase { get; }

        /// <summary>登录token</summary>
        /// <value>The token.</value>
        string Token { get; }
    }
}
