using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Redis.Cache
{
    public class RedisCache : IRedisCache
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value)
        {
            return await RedisHelper.SetAsync(key, value);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await RedisHelper.GetAsync<T>(key);
        }
    }
}
