using RayPI.Domain.Core.Specifications;
using RayPI.Domain.Entities;

namespace RayPI.Domain.Specifications
{
    public class StudentSpec
    {
        public static Specification<StudentEntity> GetPage(string name)
        {
            Specification<StudentEntity> spec = new TrueSpecification<StudentEntity>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                spec = new DirectSpecification<StudentEntity>(x => x.Name.Contains(name));
            }

            return spec;
        }
    }
}
