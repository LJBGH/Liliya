using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Liliya.Asp.NetCore.Authorization
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

        /// <summary>
        /// 上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PolicyHandler(IAuthenticationSchemeProvider schemes, IJwtApp jwtAppService,IHttpContextAccessor httpContextAccessor)
        {
            Schemes = schemes;
            _jwtAppService = jwtAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        //授权处理
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            //AuthorizationFilterContext filterContext =  context.Resource as AuthorizationFilterContext;

            //获取上下文
            HttpContext httpContext = _httpContextAccessor.HttpContext; 

            //判断请求是否拥有凭据，即有没有登录
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();

            if (defaultAuthenticate != null) 
            {
                // 验证签发的用户信息
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                //Success为True则登陆成功
                if (result.Succeeded) 
                {
                    //判断是否为已停用的Token(即缓存黑名单中是否存在该Token)
                    if (!await _jwtAppService.IsCurrentActiveTokenAsync())
                    {
                        //ajaxResult = new AjaxResult("授权失败，请重新登录", AjaxResultType.Fail);
                        //httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        //filterContext.Result = new JsonResult(ajaxResult);

                        ////自定义返回的数据类型
                        //httpContext.Response.ContentType = "application/json; charset=UTF-8";
                        //var response = JsonConvert.SerializeObject(new AjaxResult("授权失败", AjaxResultType.Unauthorized));
                        ////自定义返回状态码，默认为401 我这里改成 200
                        //httpContext.Response.StatusCode = StatusCodes.Status200OK;
                        ////输出Json数据结果
                        //await httpContext.Response.WriteAsync(response);
                        //await httpContext.Response.Body.FlushAsync();
                        context.Fail();
                        return;
                    }
                }
            }
            context.Succeed(requirement);
        }
    }
}
