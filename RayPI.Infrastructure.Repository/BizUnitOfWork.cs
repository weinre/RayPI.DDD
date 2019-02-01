using RayPI.Infrastructure.Repository.Core.Context;
using Microsoft.EntityFrameworkCore;
using RayPI.Infrastructure.Repository.DbMapping;

namespace RayPI.Infrastructure.Repository
{
    public class BizUnitOfWork : EntityFrameworkUnitOfWork, IBizUnitOfWork
    {
        public BizUnitOfWork(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new TeacherConfig());
        }
    }
}
