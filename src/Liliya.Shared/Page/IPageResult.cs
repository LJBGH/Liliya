using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public interface IPageResult<T>
    {
        int Total { get; }

        IEnumerable<T> Data { get; set; }
    }
}
