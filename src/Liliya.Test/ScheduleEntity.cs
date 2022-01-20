using Liliya.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Test
{
    /// <summary>
    /// 任务计划表
    /// </summary>
    [SugarTable("Schedule")]
    //[SplitTable(SplitType.Year)]
    public class ScheduleEntity : IFullAuditedEntity
    {
        /// <summary>
        /// 主键(任务Id)
        /// </summary>
        [SugarColumn(IsPrimaryKey = true/*, IsIdentity = true*/)]
        [DisplayName("主键(任务Id)")]
        public Guid Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [DisplayName("任务名称")]
        public string JobName { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        [DisplayName("任务分组")]
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DisplayName("任务状态")]
        public JobStatus JobStatus { get; set; }

        /// <summary>
        /// 任务运行状态
        /// </summary>
        [DisplayName("任务运行状态")]
        public RunStatus RunStatus { get; set; }

        /// <summary>
        /// 任务运行时间表达式
        /// </summary>
        [DisplayName("任务运行时间表达式")]
        public string Cron { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        [DisplayName("任务描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 任务所在DLL对应的程序集名称
        /// </summary>
        [DisplayName("任务所在DLL对应的程序集名称")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 任务所在类
        /// </summary>                                                  0
        [DisplayName("任务所在类名称")]
        public string ClassName { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        [DisplayName("执行次数")]
        public int RunTimes { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextTime { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        [DisplayName("触发器类型")]
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 执行间隔时间, 秒为单位
        /// </summary>
        [DisplayName("执行间隔时间, 秒为单位")]
        public int IntervalSecond { get; set; }






        #region   通用字段
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改人Id
        /// </summary>
        [DisplayName("最后修改人Id")]
        public Guid? LastModifyUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        #endregion
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stoped = 0,

        /// <summary>
        /// 启用
        /// </summary>
        Enabled = 1

    }

    /// <summary>
    /// 任务运行状态
    /// </summary>
    public enum RunStatus
    {
        /// <summary>
        /// 待运行
        /// </summary>
        StayRun = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Runing = 1
    }

    /// <summary>
    /// 触发器类型
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// 简单类型触发器
        /// </summary>
        Simple = 0,

        /// <summary>
        /// Cron表达式
        /// </summary>
        Cron = 1
    }
}
