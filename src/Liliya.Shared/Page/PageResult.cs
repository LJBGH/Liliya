using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public class PageResult<T> : IPageResult<T>
    {
        public PageResult(int total, IEnumerable<T> data)
        {
            Total = total;
            Data = data;
        }

        public int Total { get; set; }


        public IEnumerable<T> Data { get; set; }
    }
}
