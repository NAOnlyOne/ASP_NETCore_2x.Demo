using BLL.Service;
using DAL;
using DAL.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVC.Middlewares;
using MVC.Validators;
using System.Globalization;
using System.Threading.Tasks;

namespace MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //添加依赖注入（可选）
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddDbContext<CustomDbContext>(options => options.UseLazyLoadingProxies());
            AddAppDI(services);

            ValidatorOptions.LanguageManager.Culture = new CultureInfo("zh-CN");

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddUserInputValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// 添加程序所需依赖
        /// </summary>
        /// <param name="services"></param>
        private void AddAppDI(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IMsgService, EmailService>();
        }

        //配置HTTP请求管道
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CustomDbContext dbContext, ILogger<Startup> logger)
        {
            //初始化数据库
            dbContext.Database.Migrate();
            dbContext.Seed();

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");

                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = async context =>
                    {
                        //获取全局异常
                        var error = context.Features.Get<IExceptionHandlerFeature>().Error;

                        //把异常信息记录到日志
                        logger.LogError(error.Message);

                        //根据请求类型返回响应结果
                        bool isApiRequest = context.Request.Path.StartsWithSegments("/api");
                        if (isApiRequest)
                        {
                            context.Response.StatusCode = 504;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync("{\"code\":-1,\"success\":false,\"message\":\"服务器异常\"}");
                        }
                        else
                        {
                            context.Response.Redirect("/home/error");
                            await Task.CompletedTask;
                        }
                    }
                });
            }

            //使用Map构建中间件
            app.Map("/unauthorizedPath", builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "text/plain;charset=utf-8";
                    await context.Response.WriteAsync("权限不足");
                });
            });

            //使用MapWhen构建中间件
            app.MapWhen(httpContext => httpContext.Request.Path.StartsWithSegments("/nonExistPath"), builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain;charset=utf-8";
                    await context.Response.WriteAsync("资源不存在");
                });
            });

            //使用Use构建中间件
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("X-Remote-IP"))
                    context.Request.Headers.Add("X-Remote-IP", context.Request.Host.Host);

                //执行下一个请求委托
                await next.Invoke();

                //在响应开始后修改HttpResponse会引发异常
                //context.Response.Headers.Add("X-Service-IP", "127.0.0.1");
            });

            //自定义中间件
            app.UseMiddleware<LogMiddleware>();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
