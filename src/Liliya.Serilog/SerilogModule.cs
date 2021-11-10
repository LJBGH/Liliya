using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Liliya.Serilog
{
    public static class SerilogModule
    {
        /// <summary>
        /// 添加Serilog日志
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((webHost, configuration) =>
                {
                    //得到配置文件
                    var serilog = webHost.Configuration.GetSection("Serilog");
                    //最小级别
                    var minimumLevel = serilog["MinimumLevel:Default"];
                    //日志事件级别
                    var logEventLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLevel);

                    configuration.ReadFrom
                    .Configuration(webHost.Configuration.GetSection("Serilog"))
                    .Enrich.FromLogContext()//使用Serilog.Context.LogContext中的属性丰富日志事件
                    .WriteTo.Console(logEventLevel)//输出到控制台
                    .WriteTo.File(Path.Combine("Logs", $@"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}\.txt"), rollingInterval: RollingInterval.Day); //输出到文件

                    ////写入本地txt
                    //configuration.WriteTo.Map(le => MapData(le), (key, log) =>
                    // log.Async(o => o.File(Path.Combine("Logs", @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"), logEventLevel)));

                    //(DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
                    //{
                    //    return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
                    //}
                });
            return hostBuilder;
        }
    }
}
