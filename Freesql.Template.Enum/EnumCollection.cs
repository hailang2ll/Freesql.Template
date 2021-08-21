
using System;
using System.ComponentModel;

namespace Freesql.Template.Enum
{
    #region EnumStatusFlag 通用状态,取消，禁用
    /// <summary>
    /// 通用状态
    /// </summary>
    public enum EnumStatusFlag
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        None = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Pending = 1,

        /// <summary>
        /// 回收站
        /// </summary>
        [Description("回收站")]
        Delete = 2,

        /// <summary>
        /// 不通过
        /// </summary>
        [Description("不通过")]
        UnPassed = 3,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Passed = 4,
    }

    /// <summary>
    /// 取消，禁用
    /// </summary>
    public enum EnumStatusFlagPass
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("<span class='g_red'>禁用</span>")]
        None = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Passed = 4,
    }
    #endregion

    /// <summary>
    /// 前后台用户标识
    /// </summary>
    public enum EnumSysUserType
    {
        /// <summary>
        /// 系统用户
        /// </summary>
        [Description("系统用户")]
        Sys = -1,

        /// <summary>
        /// 后台用户
        /// </summary>
        [Description("后台用户")]
        Admin = 1,

        /// <summary>
        /// 前台用户
        /// </summary>
        [Description("前台用户")]
        Member = 2,

    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumWorkQueueType
    {
        /// <summary>
        ///  邮件
        /// </summary>
        [Description("邮件")]
        Email = 1,

        /// <summary>
        ///  短信
        /// </summary>
        [Description("短信")]
        SMS = 2,

        /// <summary>
        ///  微信
        /// </summary>
        [Description("微信")]
        Weixin = 3,
    }
 

    /// <summary>
    /// 发送短信类型
    /// </summary>
    public enum EnumSMSType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("用户注册")]
        Register = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("2")]
        SellerApply = 2,
    }

}
