using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Treasury.Core.Interfaces;
using RayPI.Infrastructure.Treasury.Core.Model;

namespace RayPI.ApplicationService.Dto
{
    /// <summary>
    /// 基类Dto
    /// </summary>
    public class DtoBaseStudent : IValidate
    {
        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        public virtual void Valid()
        {
            
        }
    }

    /// <summary>
    /// 添加Dto
    /// </summary>
    public class DtoStudentAddRequest: DtoBaseStudent
    {

    }

    /// <summary>
    /// 编辑Dto
    /// </summary>
    public class DtoStudentUpdateRequest : DtoStudentAddRequest
    {
        public long Id { get; set; }
    }

    /// <summary>
    /// 单个返回Dto
    /// </summary>
    public class DtoStudentResponse : DtoStudentUpdateRequest
    {

    }
}
