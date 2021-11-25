using Liliya.Shared.AppSetting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;


namespace Liliya.Swagger
{
    public static class SwaggerModule
    {
        private static string _title = string.Empty;
        private static string _version = string.Empty;
        private static string _url = string.Empty;
        public static void AddSwaggerService(this IServiceCollection services) 
        {
            _title = Appsettings.app(new[] { "Liliya", "Swagger", "Title"});
            _version = Appsettings.app(new[] { "Liliya", "Swagger", "Version" });
            _url = Appsettings.app(new[] { "Liliya", "Swagger", "Url" });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_version, new OpenApiInfo 
                { 
                    Title = _title + " 接口文档", 
                    Version = _version, 
                    Description = "基于.Net5.0和Sqlsugar的WebAPI框架模板",
                    Contact = new OpenApiContact 
                    {
                        Name = _title,
                        Email = "1983810978@qq.com",
                        Url = new System.Uri("https://github.com/LJBGH/Liliya")
                    }
                });

                // 获取xml文件路径
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                // 获取所有xml文件
                var files = Directory.GetFiles(basePath, "*.xml");

                foreach (var item in files)
                {
                    // 添加控制器层注释，true表示显示控制器注释
                    c.IncludeXmlComments(item, true);
                }
                //一定要返回true
                c.DocInclusionPredicate((docName, description) =>
                {
                    return true;
                });

                //swagger上配置授权
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2"}},
                        new []{ "readAccess", "writeAccess" }
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app) 
        {
            app.UseSwagger(x=> 
            {
                x.RouteTemplate = "doc/Liliya.Core.API/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint(_url, _title);
                c.RoutePrefix = string.Empty;

                //Swagger界面打开时自动折叠
                c.DocExpansion(DocExpansion.None); 
            } );
            return app;
        }
    }
}
