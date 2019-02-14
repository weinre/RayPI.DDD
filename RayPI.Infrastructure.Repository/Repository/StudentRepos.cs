using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Domain.Entities;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Repository.Core;

namespace RayPI.Infrastructure.Repository.Repository
{
    public class StudentRepos: Repository<long, StudentEntity>,IStudentRepos
    {
        #region 容器注入
        private IBizUnitOfWork _bizUnitOfWork;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public StudentRepos(IBizUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _bizUnitOfWork = unitOfWork;
        }
    }
}
