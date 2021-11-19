using Liliya.Dto.Sys.Audit;
using Liliya.Services.Sys.Audit;
using Liliya.Shared;
using Liliya.Shared.Attributes.Audit;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Liliya.AspNetCore.Filter
{
    /// <summary>
    /// 记录审计日志
    /// </summary>
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ILogger<AuditActionFilter> _logger;
        private readonly IJwtApp _jwtApp;

        public AuditActionFilter(IAuditLogService auditLogService, ILogger<AuditActionFilter> logger, IJwtApp jwtApp)
        {
            _auditLogService = auditLogService;
            _logger = logger;
            _jwtApp = jwtApp;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //判断是否写入审计日志
            if (!IsSaveAudit(context)) 
            {
                await next();
                return;
            }

            //开始计时
            var stopwatch = Stopwatch.StartNew();

            //接口类型
            var type = (context.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo.AsType();
            //方法信息
            var method = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;

            AuditLogInputDto auditLogInfo = new AuditLogInputDto();
            auditLogInfo.UserId = _jwtApp.Id;
            auditLogInfo.UserName = _jwtApp.Name;
            auditLogInfo.ServiceName = type.FullName;
            auditLogInfo.MethodName = method.Name;
            auditLogInfo.RoutePath = context.HttpContext.Request.Path;
            auditLogInfo.Parameters = JsonConvert.SerializeObject(context.ActionArguments);
            auditLogInfo.ExecutionTime = DateTime.Now;
            auditLogInfo.ClientIpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            auditLogInfo.BrowserInfo = context.HttpContext.Request.Headers["User-Agent"].ToString();


            ActionExecutedContext result = null;
            try
            {
                result = await next();
                if (result.Exception != null && !result.ExceptionHandled)
                {
                    auditLogInfo.Exception = result.Exception.Message;
                }
            }
            catch (Exception ex)
            {
                auditLogInfo.Exception = ex.Message;
                throw;
            }
            finally 
            {
                stopwatch.Stop();
                auditLogInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                await _auditLogService.AddAsync(auditLogInfo);
            }
        }

        /// <summary>
        /// 是否写入审计日志
        /// </summary>
        /// <param name="context"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool IsSaveAudit(ActionExecutingContext context, bool defaultValue = false)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return false;

            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.HasAttribute<AuditedLogAttribute>())
            {
                return true;
            }

            //if (methodInfo.HasAttribute<DisableAuditingAttribute>())
            //{
            //    return false;
            //}

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().HasAttribute<AuditedLogAttribute>())
                {
                    return true;
                }

                //if (classType.GetTypeInfo().HasAttribute<DisableAuditingAttribute>())
                //{
                //    return false;
                //}
            }
            return defaultValue;
        }
    }
}
