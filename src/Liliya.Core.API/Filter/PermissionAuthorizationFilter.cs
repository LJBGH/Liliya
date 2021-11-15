using Liliya.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Liliya.Core.API.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtApp _jwtApp;

        public PermissionAuthorizationFilter(IHttpContextAccessor httpContextAccessor, IJwtApp jwtApp)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtApp = jwtApp;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.CompletedTask;
            var action = context.ActionDescriptor as ControllerActionDescriptor;
           
            //是否包含AllowAnonymous特性
            if (!action.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
            {
                //是否授权
                if (!(bool)_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated)
                {
                    var result = new AjaxResult(ResultMessage.Unauthorized, AjaxResultType.Unauthorized);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                    context.Result = new JsonResult(result);
                    return;
                }

                //判断是否为已停用的Token(即缓存黑名单中是否存在该Token)
                if (!await _jwtApp.IsCurrentActiveTokenAsync())
                {
                    var result = new AjaxResult(ResultMessage.Unauthorized, AjaxResultType.Uncertified);
                    context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                    context.Result = new JsonResult(result);
                    return;
                }
            }
        }
    }
}
