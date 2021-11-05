using System;
using System.Collections.Generic;
using System.Text;

namespace AkliaJob.Shared
{
    public class AjaxResult
    {
        public AjaxResult() : this(null)
        {

        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success) : this("", null, type)
        {

        }

        public AjaxResult(string message, AjaxResultType type = AjaxResultType.Success, object data = null) : this(message, data, type)
        {

        }

        public AjaxResult(string message, object data, AjaxResultType type)
        {
            this.Message = message;
            this.Data = data;
            this.Type = type;
            this.Success = type == AjaxResultType.Success;
        }

        /// <summary>
        /// 消息
        /// </summary>

        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>

        public object Data { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>

        public bool Success { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>

        public AjaxResultType Type { get; set; }
    }
}
