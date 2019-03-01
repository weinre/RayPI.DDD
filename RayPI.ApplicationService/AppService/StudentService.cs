using RayPI.ApplicationService.Dto;
using RayPI.ApplicationService.IAppService;
using RayPI.Domain.Entities;
using RayPI.Domain.IRepository;
using RayPI.Domain.Specifications;
using RayPI.Infrastructure.Treasury.Core.Helpers;
using RayPI.Infrastructure.Treasury.Core.Model;
using RayPI.Infrastructure.Treasury.Core.Model.Enums;

namespace RayPI.ApplicationService.AppService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepos _studentRepos;

        public StudentService(IStudentRepos studentRepos)
        {
            _studentRepos = studentRepos;
        }

        public PageResult<DtoStudentResponse> GetPage(int pageIndex, int pageSize, string name)
        {
            var spec = StudentSpec.GetPage(name);
            var pageResult = _studentRepos.GetListPaged(pageIndex, pageSize, spec, it => it.CreateTime, SortEnum.Desc);
            var result = AutoMapperHelper.Map<PageResult<StudentEntity>, PageResult<DtoStudentResponse>>(pageResult);
            return result;
        }

        public long Add(DtoStudentAddRequest addRequest)
        {
            var stuEntity = AutoMapperHelper.Map<DtoStudentAddRequest, StudentEntity>(addRequest);
            return _studentRepos.Add(stuEntity);
        }

        public DtoStudentResponse GetById(long id)
        {
            var stuEntity = _studentRepos.Find(id);
            var result = AutoMapperHelper.Map<StudentEntity, DtoStudentResponse>(stuEntity);
            return result;
        }

        public bool Delete(long id)
        {
            _studentRepos.Delete(id);
            return true;
        }
    }
}
