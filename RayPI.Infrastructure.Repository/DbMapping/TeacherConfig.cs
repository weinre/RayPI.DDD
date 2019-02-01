using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entities;
using RayPI.Infrastructure.Repository.Core.Context;

namespace RayPI.Infrastructure.Repository.DbMapping
{
    public class TeacherConfig : EntityBaseTypeConfig<TeacherEntity>
    {
        public override void Configure(EntityTypeBuilder<TeacherEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Teacher");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Age).IsRequired();
        }
    }
}
