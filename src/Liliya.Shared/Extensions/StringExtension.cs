using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断Guid是否为空或者为null
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <returns>返回true/false</returns>
        public static bool IsNullOrEmpty(this Guid value)
        {
            return value == null || value == Guid.Empty;
        }


        /// <summary>
        /// 是否为空或者为null
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <returns>返回true/false</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

    }
}
