using RayPI.ApplicationService.Dto;
using RayPI.Infrastructure.Treasury.Core.Model;

namespace RayPI.ApplicationService.IAppService
{
    public interface IStudentAppService
    {
        long Add(DtoStudentAddRequest addRequest);
        PageResult<DtoStudentResponse> GetPage(int pageIndex, int pageSize, string name);
        DtoStudentResponse GetById(long id);
    }
}
