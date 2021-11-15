using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Asp.NetCore.Filter
{
    /// <summary>
    /// 记录审计日志
    /// </summary>
    public class AuditLogFilter : IActionFilter
    {

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("方法执行前");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("方法执行后");
        }

    }
}
