using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayPI.ApplicationService.AppService;
using RayPI.ApplicationService.IAppService;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Repository;
using RayPI.Infrastructure.Repository.Repository;
using RayPI.Infrastructure.Treasury.Core.Adapters;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region 注入
        public IConfiguration Configuration { get; }//配置
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BizUnitOfWork>(options => options.UseSqlServer(Configuration.GetConnectionString("RayPIDbConnStr"), b => b.UseRowNumberForPaging()));//注入数据库上下文(参数2是为了兼容Sql2005)
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);//注入MVC
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Email = "2271272653@qq.com", Name = "RayWang", Url = "https://home.cnblogs.com/u/RayWang/" },
                    Description = "基于EntityFramworkCore的WebApi框架",
                    Title = "RayPI.DDD",
                    Version = "1.0"
                });
            });

            services.AddTransient<IBizUnitOfWork, BizUnitOfWork>();

            #region 注入Service
            services.AddTransient<IStudentAppService, StudentAppService>();
            #endregion

            #region 注入Repos
            services.AddTransient<IStudentRepos, StudentRepos>();
            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                c.RoutePrefix = "";//设置根目录访问swagger
            });
            #endregion
            app.UseMvc();
        }
    }
}
