using Liliya.Shared;
using Liliya.Shared.AppSetting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Core.API.Startups
{
    public static class AuthorizationModule
    {
        public static void AddAuthService(this IServiceCollection service, IConfiguration configuration)
        {
            //var secretKey = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "SecretKey" });
            //var issuer = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "Issuer" });
            //var audience = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "Audience" });
            //var expireMins = int.Parse(Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "ExpireMins" }));

            AuthrizeToken authrizeToken = new AuthrizeToken
            {
                SecretKey = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "SecretKey" }),
                Issuer = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "Issuer" }),
                Audience = Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "Audience" }),
                ExpireMins = int.Parse(Appsettings.app(new string[] { "Liliya", "AuthrizeToken", "ExpireMins" }))
            };
            //注入配置类
            service.Configure<AuthrizeToken>(configuration.GetSection("Liliya:AuthrizeToken"));

            //Http上下文和用户信息注入
            service.AddHttpContextAccessor();
            service.AddSingleton<IUserAuth, UserAuth>();

            service.AddAuthorization();
            service.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证颁发者
                    ValidateIssuer = true,
                    //是否验证接收方
                    ValidateAudience = true,
                    //是否验证秘钥
                    ValidateIssuerSigningKey = true,
                    //是否验证Token有效期 使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    //过期时间 是否要求Token的Claims中必须包含Expires
                    RequireExpirationTime = true,
                    //Token颁发机构
                    ValidIssuer = authrizeToken.Issuer,
                    //颁发给谁
                    ValidAudience = authrizeToken.Audience,

                    //这里的key要进行加密
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authrizeToken.SecretKey)),

                    //允许服务器时间偏移量300秒，即我们配置的过期时间加上这个允许偏移的时间值，
                    //才是真正过期的时间(过期时间 + 偏移值)你也可以设置为0，ClockSkew = TimeSpan.Zero
                    ClockSkew = TimeSpan.Zero,
                };

                //jwt自带事件
                x.Events = new JwtBearerEvents
                {
                    //在处理请求期间引发异常时调用
                    OnAuthenticationFailed = async context =>
                    {
                        await Task.CompletedTask;
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                    },

                    //此处为权限验证失败后触发的事件
                    OnChallenge = async context =>
                    {
                        //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                        context.HandleResponse();
                        //自定义自己想要返回的数据结果
                        var result = JsonConvert.SerializeObject(new AjaxResult("未经授权", AjaxResultType.Unauthorized));
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //输出Json数据结果
                        await context.Response.WriteAsync(result);
                    },

                    ////在安全令牌已通过验证并生成 ClaimsIdentity 后调用
                    //OnTokenValidated = async context =>
                    //{
                    //    //var user = context.HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
                    //    //await Task.CompletedTask;
                    //    //Console.WriteLine("权限验证通过");
                    //}
                };
            });
        }  
    }
}
