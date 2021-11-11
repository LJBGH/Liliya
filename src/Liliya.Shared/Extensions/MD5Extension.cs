using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 安全拓展
    /// </summary>
    public static class SecurityExtension
    {

        /// <summary>
        /// 使用加密服务提供程序(CSP)计算输入数据的MD5哈希值。
        /// </summary>
        /// <param name="value">要加密的字符串。</param>
        /// <returns>经过MD5加密的字符串。</returns>
        public static string ToMD5(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byteHash = null;
            byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            md5.Clear();
            string strTemp = byteHash.Aggregate("", (current, t) => current + t.ToString("x").PadLeft(2, '0'));
            return strTemp.ToLower();
        }
    }
}
