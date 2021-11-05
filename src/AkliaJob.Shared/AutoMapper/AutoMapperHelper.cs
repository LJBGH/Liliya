using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AkliaJob.Shared
{
    /// <summary>
    /// AutoMapper对象映射拓展
    /// </summary>
    public static class AutoMapperHelper
    {
        private static IMapper _mapper = null;

        public static void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 检查传入的实例
        /// </summary>
        private static void CheckMapper()
        {
            _mapper.NotNull(nameof(_mapper));
        }

        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget MapTo<TTarget>(this object source)
        {
            CheckMapper();
            source.NotNull(nameof(source));
            return _mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 批量将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TTarget> MapToList<TTarget, TSource>(this IEnumerable<TSource> source)
        {
            CheckMapper();
            source.NotNull(nameof(source));
            return _mapper.Map<IEnumerable<TTarget>>(source);
        }


        /// <summary>
        /// 将源对象映射为指定目标对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget MapTo<TSource, TTarget>(this TSource source)
        {
            CheckMapper();
            source.NotNull(nameof(source));
            return _mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// 批量将源对象映射为指定目标对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TTarget> MapToList<TSource, TTarget>(this IEnumerable<TSource> source)
        {
            CheckMapper();
            source.NotNull(nameof(source));
            return _mapper.Map<IEnumerable<TTarget>>(source);
        }

        /// <summary>
        ///  将数据源映射为指定<typeparamref name="TTarget"/>的集合
        /// </summary>
        /// <typeparam name="TTarget">动态实体</typeparam>
        /// <param name="sources">数据源</param>
        /// <returns></returns>
        public static IEnumerable<TTarget> MapToList<TTarget>(this IEnumerable<object> sources)
        {
            CheckMapper();
            sources.NotNull(nameof(sources));
            return _mapper.Map<IEnumerable<TTarget>>(sources);
        }

        /// <summary>
        ///  将数据源映射为指定<typeparamref name="TTarget"/>的集合
        /// </summary>
        /// <typeparam name="TTarget">动态实体</typeparam>
        /// <param name="sources">数据源</param>
        /// <returns></returns>
        public static IQueryable<TTarget> MapToList<TTarget>(this IQueryable<object> sources)
        {
            CheckMapper();
            sources.NotNull(nameof(sources));
            return _mapper.Map<IQueryable<TTarget>>(sources);
        }


        /// <summary>
        /// 将数据源映射为指定<typeparamref name="TOutputDto"/>的集合
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="membersToExpand">成员展开</param>
        public static IQueryable<TOutputDto> ToOutput<TOutputDto>(this IQueryable source, params Expression<Func<TOutputDto, object>>[] membersToExpand)
        {
            CheckMapper();
            return _mapper.ProjectTo<TOutputDto>(source, membersToExpand);
        }
    }
}
