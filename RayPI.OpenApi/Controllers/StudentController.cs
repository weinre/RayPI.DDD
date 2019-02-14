using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RayPI.ApplicationService.Dto;
using RayPI.ApplicationService.IAppService;
using RayPI.Infrastructure.Treasury.Core.Model;

namespace RayPI.OpenApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 学生模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _iStudentAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iStudentAppService"></param>
        public StudentController(IStudentService iStudentAppService)
        {
            _iStudentAppService = iStudentAppService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="name">学生姓名</param>
        /// <returns></returns>
        [HttpGet]
        public PageResult<DtoStudentResponse> GetPage(int pageIndex = 1, int pageSize = 10, string name = null)
        {
            return _iStudentAppService.GetPage(pageIndex, pageSize, name);
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="addRequest">学生添加Dto</param>
        /// <returns></returns>
        [HttpPost]
        public long Add(DtoStudentAddRequest addRequest)
        {
            return _iStudentAppService.Add(addRequest);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id">学生Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public DtoStudentResponse GetById(long id)
        {
            return _iStudentAppService.GetById(id);
        }
    }
}
