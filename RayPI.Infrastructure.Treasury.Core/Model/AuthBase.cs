using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Treasury.Core.Model.Enums;

namespace RayPI.Infrastructure.Treasury.Core.Model
{
    public class AuthBase
    {
        /// <summary>登录ID</summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>登录人名称</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>登录账号</summary>
        /// <value>The name of the login.</value>
        public string LoginName { get; set; }

        /// <summary>登录类型</summary>
        /// <value>The type of the login.</value>
        public LoginTypeEnum LoginType { get; set; }
    }
}
