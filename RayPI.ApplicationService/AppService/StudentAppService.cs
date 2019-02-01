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
    public class StudentAppService : IStudentAppService
    {
        private readonly IStudentRepos _iStudentRepos;

        public StudentAppService(IStudentRepos iStudentRepos)
        {
            _iStudentRepos = iStudentRepos;
        }

        public PageResult<DtoStudentResponse> GetPage(int pageIndex, int pageSize, string name)
        {
            var spec = StudentSpec.GetPage(name);
            var pageResult = _iStudentRepos.GetListPaged(pageIndex, pageSize, spec, it => it.CreateTime, SortEnum.Desc);
            var result = AutoMapperHelper.Map<PageResult<StudentEntity>, PageResult<DtoStudentResponse>>(pageResult);
            return result;
        }

        public long Add(DtoStudentAddRequest addRequest)
        {
            var stuEntity = AutoMapperHelper.Map<DtoStudentAddRequest, StudentEntity>(addRequest);
            return _iStudentRepos.Add(stuEntity);
        }

        public DtoStudentResponse GetById(long id)
        {
            var stuEntity = _iStudentRepos.Find(id);
            var result = AutoMapperHelper.Map<StudentEntity, DtoStudentResponse>(stuEntity);
            return result;
        }
    }
}
