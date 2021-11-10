﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Shared
{
    public enum AjaxResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("失败")]
        Fail = 201,

        /// <summary>
        /// 未经授权
        /// </summary>
        [Description("未经授权")]
        Unauthorized = 401,

        /// <summary>
        /// 已登录但权限不足
        /// </summary>
        [Description("当前用户权限不足")]
        Uncertified = 403,

        /// <summary>
        /// 功能资源找不到
        /// </summary>
        [Description("当前功能资源找不到")]
        NoFound = 404,


        /// <summary>
        ///  系统出现异常
        /// </summary>
        [Description("系统出现异常")]
        Error = 500
    }
}
