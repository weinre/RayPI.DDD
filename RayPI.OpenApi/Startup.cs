using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayPI.ApplicationService.AppService;
using RayPI.ApplicationService.IAppService;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Repository;
using RayPI.Infrastructure.Repository.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Module = Autofac.Module;
using RayPI.Infrastructure.Treasury.Core.Extensions;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        #region 注入
        public IConfiguration Configuration { get; }//配置
        public IContainer AutoFacContainer { get; private set; }//容器
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var controllerXmlPath = Path.Combine(basePath, "RayPI.OpenApi.xml");
                var dtoXmlPath = Path.Combine(basePath, "RayPI.ApplicationService.xml");
                c.IncludeXmlComments(controllerXmlPath, true);
                c.IncludeXmlComments(dtoXmlPath);
            });

            services.AddTransient<IBizUnitOfWork, BizUnitOfWork>();//注册数据库上下文

            #region 注册Service
            //services.AddTransient<IStudentAppService, StudentAppService>();
            services.RegisterAssembly("RayPI.ApplicationService");
            #endregion

            #region 注册Repos
            //services.AddTransient<IStudentRepos, StudentRepos>();
            services.RegisterAssembly("RayPI.Domain", "RayPI.Infrastructure.Repository");
            #endregion

            #region AutoFac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var module = new DefaultModuleRegister();
            builder.RegisterModule(module);
            AutoFacContainer = builder.Build();
            #endregion
            return new AutofacServiceProvider(AutoFacContainer);
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

    /// <summary>
    /// AutoFac注册
    /// </summary>
    public class DefaultModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*
            var assemblys = System.Web.Compilation.BuildManager.GetReferencedAssemblies()
                .Where(m =>
                    m.FullName.Contains(".ApplicationService") ||
                    m.FullName.Contains(".Infrastructure.Repository"))
                .ToArray();
            builder.RegisterAssemblyTypes(assemblys)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblys)
                .Where(t => t.Name.EndsWith("Repos"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
                */
        }
    }
}
