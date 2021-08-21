using DMS.Redis;
using DMSN.Common.BaseResult;
using DMSN.Common.Helper;
using Freesql.Template.Contracts;
using Freesql.Template.Contracts.Param;
using Freesql.Template.Contracts.Result;
using Freesql.Template.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freesql.Template.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class SysJobLogService : ISysJobLogService
    {
        public RedisManager redisManager = null;
        public IFreeSql fsql;
        public SysJobLogService(IFreeSql freeSql)
        {
            fsql = freeSql;
            if (redisManager == null)
            {
                redisManager = new RedisManager(0);
            }
        }
        public async Task<ResponseResult> AddAsync(AddJobLogParam param)
        {
            ResponseResult result = new ResponseResult();

            if (param == null
                || string.IsNullOrEmpty(param.Name)
                || string.IsNullOrEmpty(param.Message))
            {
                result.errno = 1;
                result.errmsg = "参数错误";
                return result;
            }
            SysJobLog jobLogEntity = new SysJobLog()
            {
                Name = param.Name,
                JobLogType = param.JobLogType,
                ServerIP = IPHelper.GetWebClientIp(),
                TaskLogType = param.TaskLogType,
                Message = param.Message,
                CreateTime = DateTime.Now,
            };

            //var t1 = _fsql.Insert(jobLogEntity).ExecuteAffrows();
            //方法1：(原始)
            //var l = _fsql.Insert(jobLogEntity).ExecuteIdentity();
            //方法2：(依赖 FreeSql.Repository)
            //var repo = _fsql.GetRepository<SysJobLog>();
            //var r = repo.Insert(jobLogEntity);

            var t1 = await fsql.Insert(jobLogEntity).ExecuteAffrowsAsync();
            result.data = t1;
            return result;
        }

        public async Task<ResponseResult> AddTranAsync(AddJobLogParam param)
        {
            ResponseResult result = new ResponseResult();
            if (param == null
                || string.IsNullOrEmpty(param.Name)
                || string.IsNullOrEmpty(param.Message))
            {
                result.errno = 1;
                result.errmsg = "参数错误";
                return result;
            }
            SysJobLog jobLogEntity = new SysJobLog()
            {
                Name = param.Name,
                JobLogType = param.JobLogType,
                ServerIP = IPHelper.GetWebClientIp(),
                TaskLogType = param.TaskLogType,
                Message = param.Message,
                CreateTime = DateTime.Now,
            };
            SysLog jobEntity = new SysLog()
            {
                Logger = "测试数据",
                Level = "测试等级",
                IP = "::",
                DeleteFlag = 0,
                LogType = 1,
                Message = "测试数据",
                SubSysID = 1,
                SubSysName = "测试子名称",
                Thread = "测试数据",
                Url = "http://www.yuxunwang.com/",
                MemberName = "18802727803",
                CreateTime = DateTime.Now,
                Exception = "测试异常信息",
            };
            fsql.Transaction(() =>
            {
                var tran = fsql.Ado.TransactionCurrentThread; //获得当前事务对象
                var t1 = fsql.Insert(jobLogEntity).ExecuteAffrows();
                var t2 = fsql.Insert(jobEntity).ExecuteAffrows();
            });
            return await Task.FromResult(result);
        }

        public async Task<ResponseResult> DeleteAsync(long jobLogID)
        {
            ResponseResult result = new ResponseResult();
            if (jobLogID <= 0)
            {
                result.errno = 1;
                result.errmsg = "参数不合法";
                return result;
            }

            //var d = _fsql.Delete<SysJobLog>(new[] { 1, 2 }).ExecuteAffrows();
            //var t4 = _fsql.Delete<SysJobLog>(new { JobLogID = 3 }).ExecuteAffrows();
            //var t5 = _fsql.Delete<SysJobLog>().Where(a => a.JobLogID == 1).ExecuteAffrows();


            var t1 = await fsql.Delete<SysJobLog>()
                .Where(a => a.JobLogID == jobLogID)
                .ExecuteAffrowsAsync();
            result.data = t1;
            return result;
        }
        public async Task<ResponseResult> UpdateAsync(long jobLogID)
        {
            ResponseResult result = new ResponseResult();
            if (jobLogID <= 0)
            {
                result.errno = 1;
                result.errmsg = "参数不合法";
                return result;
            }
            var t1 = await fsql.Update<SysJobLog>()
                   .Set(a => a.Message, "新标题")
                   .Set(a => a.CreateTime, DateTime.Now)
                   .Where(a => a.JobLogID == jobLogID)
                   .ExecuteAffrowsAsync();
            result.data = t1;
            return result;

        }
        public async Task<ResponseResult<JobLogResult>> GetJobLogAsync(long jobLogID)
        {
            ResponseResult<JobLogResult> result = new ResponseResult<JobLogResult>() { data = new JobLogResult() };
            var entity = await fsql.Select<SysJobLog>()
                .Where(q => q.JobLogID == jobLogID)
                .FirstAsync<JobLogResult>();
            if (entity == null)
            {
                result.errno = 1;
                result.errmsg = "未找到相关数据";
                return result;
            }
            result.data = entity;
            return result;
        }

        public async Task<ResponseResult<List<JobLogResult>>> GetJobLogListAsync(long jobLogType)
        {
            ResponseResult<List<JobLogResult>> result = new ResponseResult<List<JobLogResult>>()
            {
                data = new List<JobLogResult>()
            };
            if (jobLogType <= 0)
            {
                result.errno = 1;
                result.errmsg = "参数不合法";
                return result;
            }
            var list = await fsql.Select<SysJobLog>()
                .Where(q => q.JobLogType == jobLogType)
                .ToListAsync<JobLogResult>();
            if (list == null || list.Count <= 0)
            {
                result.errno = 2;
                result.errmsg = "未找到相关数据";
                return result;
            }
            result.data = list;
            return result;
        }

        public async Task<ResponsePageResult<JobLogResult>> SearchJobLogAsync(SearchJobLogParam param)
        {
            ResponsePageResult<JobLogResult> result = new ResponsePageResult<JobLogResult>()
            {
                data = new DataResultList<JobLogResult>()
            };
            if (param == null || param.JobLogType <= 0)
            {
                result.errno = 1;
                result.errmsg = "参数不合法";
                return result;
            }

            Expression<Func<SysJobLog, bool>> where = q => q.JobLogType == 1;
            where = where.And(q => q.TaskLogType == 1);

            var list = await fsql.Select<SysJobLog>()
                .Where(where)
                .Count(out var total) //总记录数量
                .Page(param.PageIndex, param.PageSize)
                .ToListAsync<JobLogResult>();
            if (list == null || list.Count <= 0)
            {
                result.errno = 2;
                result.errmsg = "未找到相关数据";
                return result;
            }
            result.data.ResultList = list;
            result.data.PageIndex = param.PageIndex;
            result.data.PageSize = param.PageSize;
            result.data.TotalRecord = (int)total;
            return result;
        }


    }


}