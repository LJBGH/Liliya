using AkliaJob.Shared;
using AkliaJob.Shared.AppSetting;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AkliaJob.Consul
{
    public static class ConsulModule
    {
        /// <summary>
        /// Consul服务地址
        /// </summary>
        private static string _consulIp = string.Empty;

        /// <summary>
        /// Consul服务端口
        /// </summary>
        private static int _consulPort = 80;

        /// <summary>
        /// 服务地址
        /// </summary>
        private static string _serviceName = string.Empty;

        /// <summary>
        /// 服务Ip
        /// </summary>
        private static string _ip = string.Empty;

        /// <summary>
        /// 服务端口 docker容器内部端口
        /// </summary>
        private static int _Prot = 80;

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app) 
        {
            _consulIp = Appsettings.app(new string[] { "Consul", "IP" });
            _consulPort = Convert.ToInt32(Appsettings.app(new string[] { "Consul", "Port" }));
            _ip = Appsettings.app(new string[] { "Service", "IP" });
            _Prot = Convert.ToInt32(Appsettings.app(new string[] { "Service", "Port" }));
            _serviceName = Appsettings.app(new string[] { "Service", "Name" });

            ConsulServiceEntity serviceEntity = new ConsulServiceEntity
            {
                //IP = NetworkHelper.LocalIPAddress,
                IP = _ip,
                Port = _Prot,//如果使用的是docker 进行部署这个需要和dockerfile中的端口保证一致
                ServiceName = _serviceName,
                ConsulIP = _consulIp,
                ConsulPort = _consulPort
            };


            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"));//请求注册的 Consul 地址

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}/api/health",//健康检查地址
                //HTTP = $"http://{"192.168.88.74"}:{serviceEntity.Port}/api/health",//健康检查地址  如果是本地则IP为localhost ，如果是发布到服务端，则使用服务端IP
                Timeout = TimeSpan.FromSeconds(1)
            };

            Console.WriteLine($"我是服务IP和端口：{serviceEntity.IP}:{serviceEntity.Port}");

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceEntity.ServiceName,
                Address = serviceEntity.IP,
                Port = serviceEntity.Port,
                Tags = new[] { $"urlprefix-/{serviceEntity.ServiceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };


            //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            consulClient.Agent.ServiceRegister(registration).Wait();
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            //应用程序终止时，服务取消注册
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });

            return app;
        }

    }
}
