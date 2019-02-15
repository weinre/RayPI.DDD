using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 项目启动配置
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
        /// 运行时被调用
        /// 用于向Ioc容器注册服务
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //注册Swagger
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
            //注册数据库上下文(参数2是为了兼容Sql2005)
            services.AddDbContext<BizUnitOfWork>(options => options.UseSqlServer(Configuration.GetConnectionString("RayPIDbConnStr"), b => b.UseRowNumberForPaging()));
            services.AddTransient<IBizUnitOfWork, BizUnitOfWork>();

            //注册Service
            //services.AddTransient<IStudentAppService, StudentAppService>();
            //services.RegisterAssembly("RayPI.ApplicationService");
            //改为交由AutoFac注册

            //注册Repos
            //services.AddTransient<IStudentRepos, StudentRepos>();
            //services.RegisterAssembly("RayPI.Domain", "RayPI.Infrastructure.Repository");
            //改为交由AutoFac注册

            #region 引入AutoFac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var module = new DefaultModuleRegister();
            builder.RegisterModule(module);
            AutoFacContainer = builder.Build();
            #endregion
            return new AutofacServiceProvider(AutoFacContainer);
        }

        /// <summary>
        /// 运行时被调用
        /// 用于配置HTTP请求管道
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
                c.RoutePrefix = "swagger";//设置根目录访问swagger
            });
            #endregion

            app.UseMvc();
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// AutoFac注册
    /// </summary>
    public class DefaultModuleRegister : Autofac.Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            //1.获取程序集
            //var allAssemblys = System.Web.Compilation.BuildManager.GetReferencedAssemblies();
            IEnumerable<Assembly> allAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);

            Assembly[] assemblies = allAssemblies.Where(m =>
                      m.FullName.Contains(".ApplicationService") ||
                      m.FullName.Contains(".Infrastructure.Repository"))
                .ToArray();

            //2.注册service
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            //3.注册repository
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Repos"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
