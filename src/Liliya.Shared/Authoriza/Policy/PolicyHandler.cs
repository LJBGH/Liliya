using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    /// <summary>
    /// 自定义授权策略
    /// </summary>
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// Jwt服务
        /// </summary>
        private readonly IJwtApp _jwtAppService;

        public PolicyHandler(IAuthenticationSchemeProvider schemes, IJwtApp jwtAppService)
        {
            Schemes = schemes;
            _jwtAppService = jwtAppService;
        }

        //授权处理
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var filterContext = (context.Resource as AuthorizationFilterContext);
            //获取上下文
            var httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;
            //获取授权方式
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();

            var ajaxResult = new AjaxResult();

            if (defaultAuthenticate != null) 
            {
                // 验证签发的用户信息
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result.Succeeded) 
                {
                    //判断是否为已停用的 Token
                    if (!await _jwtAppService.IsCurrentActiveTokenAsync())
                    {
                        //ajaxResult = new AjaxResult("授权失败，请重新登录", AjaxResultType.Fail);
                        //httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        //filterContext.Result = new JsonResult(ajaxResult);
                        context.Fail();
                        return;
                    }

                    httpContext.User = result.Principal;
                }

            }
            context.Fail();
        }
    }
}
