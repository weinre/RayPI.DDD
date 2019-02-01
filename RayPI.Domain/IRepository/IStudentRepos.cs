using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Domain.Core;
using RayPI.Domain.Entities;

namespace RayPI.Domain.IRepository
{
    public interface IStudentRepos : IRepository<long, StudentEntity>
    {
    }
}
