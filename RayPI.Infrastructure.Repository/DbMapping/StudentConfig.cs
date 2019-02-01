using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entities;
using RayPI.Infrastructure.Repository.Core.Context;

namespace RayPI.Infrastructure.Repository.DbMapping
{
    public class StudentConfig : EntityBaseTypeConfig<StudentEntity>
    {
        public override void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Student");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Age).IsRequired();
        }
    }
}
