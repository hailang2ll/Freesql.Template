using Autofac;
using DMS.Autofac;
using DMS.NLogs.Filters;
using DMS.Redis.Configurations;
using DMS.Swagger;
using DMSN.Common.Configurations;
using DMSN.Common.JsonHandler.JsonConverters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freesql.Template
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IFreeSql Fsql { get; }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            var path = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddRedisFile($"Configs/redis.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"Configs/domain.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
            .AddAppSettingsFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();


            Fsql = new FreeSql.FreeSqlBuilder()
               //.UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=192.168.31.201;User Id=devuser;Password=yxw-88888;Initial Catalog=trydou_sys;Pooling=true;Min Pool Size=1")
               .UseConnectionString(FreeSql.DataType.MySql, Configuration.GetConnectionString("trydou_sys_master"))
              //.UseAutoSyncStructure(true)
              .Build();

            Fsql.Aop.CurdAfter += (s, e) =>
            {
                if (e.ElapsedMilliseconds > 200)
                {
                    //记录日志
                    //发送短信给负责人
                    DMS.NLogs.Logger.Error($"Exception={e.Exception.Message},StackTrace={e.Exception.StackTrace},sql={e.Sql}");
                    throw new NotImplementedException();
                }
            };

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(option =>
            {
                option.Filters.Add<GlobalExceptionFilter>();

            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#if DEBUG
            services.AddSwaggerGenV2();
#endif

            #region AddDbContext
            services.AddSingleton<IFreeSql>(Fsql);
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
#if DEBUG
            app.UseSwaggerUIV2();
#endif
            app.UseStaticHttpContext();


            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAutofac31();
        }
    }
}
