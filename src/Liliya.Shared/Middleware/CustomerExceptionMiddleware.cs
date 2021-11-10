using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    /// <summary>
    /// 自定义异常中间件记录日志
    /// </summary>
    public class CustomerExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<CustomerExceptionMiddleware> _logger;

        public CustomerExceptionMiddleware(RequestDelegate next, ILogger<CustomerExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/problem+json";

                var title = "出现异常错误:" + ex.Message + ex.ToString();

                _logger.LogError(title);

                var ajaxResult = new AjaxResult
                {
                    Success = false,
                    Message = title,
                    Type = AjaxResultType.Error
                };

                var stream = context.Response.Body;
                await JsonSerializer.SerializeAsync(stream, ajaxResult);
            }
        }
    }
}
