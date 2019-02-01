using System;

namespace RayPI.Domain.Entities
{
    /// <summary>
    /// 学生实体
    /// </summary>
    public class StudentEntity : RayPI.Domain.Core.Entity.EntityBase
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

    }
}
