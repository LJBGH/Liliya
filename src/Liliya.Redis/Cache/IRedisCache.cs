using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Redis.Cache
{
    public interface IRedisCache
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value);



        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

    }
}
