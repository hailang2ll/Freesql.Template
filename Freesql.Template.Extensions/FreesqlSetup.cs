using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freesql.Template.Extensions
{
    public static class FreesqlSetup
    {
        public static void AddFreesqlSetup(this IServiceCollection services, IConfiguration configuration)
        {
            IFreeSql Fsql = new FreeSql.FreeSqlBuilder()
               //.UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=192.168.31.201;User Id=devuser;Password=yxw-88888;Initial Catalog=trydou_sys;Pooling=true;Min Pool Size=1")
               .UseConnectionString(FreeSql.DataType.MySql, configuration.GetConnectionString("trydou_sys_master"))
              //.UseAutoSyncStructure(true)
              .Build();

            Fsql.Aop.CurdAfter += (s, e) =>
            {
                if (e.ElapsedMilliseconds > 200)
                {
                    //记录日志
                    //发送短信给负责人
                    //DMS.NLogs.Logger.Error($"Exception={e.Exception.Message},StackTrace={e.Exception.StackTrace},sql={e.Sql}");
                    throw new NotImplementedException();
                }
            };

            services.AddSingleton<IFreeSql>(Fsql);
        }
    }
}
