using AkliaJob.Shared;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkliaJob.Center.Web.StartupModule
{
    /// <summary>
    /// 自定义中间件拓展
    /// </summary>
    public static class MiddlewareModule
    {

        public static IApplicationBuilder CustomerMiddleware (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomerExceptionMiddleware>();
        }
    }
}
