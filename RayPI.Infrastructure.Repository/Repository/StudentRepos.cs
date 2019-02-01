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
        private IBizUnitOfWork _bizUnitOfWork;

        public StudentRepos(IBizUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _bizUnitOfWork = unitOfWork;
        }
    }
}
