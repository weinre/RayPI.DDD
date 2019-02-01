﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RayPI.ApplicationService.Dto;
using RayPI.ApplicationService.IAppService;
using RayPI.Infrastructure.Treasury.Core.Model;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 学生模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentAppService _iStudentAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iStudentAppService"></param>
        public StudentController(IStudentAppService iStudentAppService)
        {
            _iStudentAppService = iStudentAppService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public PageResult<DtoStudentResponse> GetPage(int pageIndex = 1, int pageSize = 10, string name = null)
        {
            return _iStudentAppService.GetPage(pageIndex, pageSize, name);
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="addRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public long Add(DtoStudentAddRequest addRequest)
        {
            return _iStudentAppService.Add(addRequest);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public DtoStudentResponse GetById(long id)
        {
            return _iStudentAppService.GetById(id);
        }
    }
}