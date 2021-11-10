using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public static class ListExtensions
    {
        public static bool IsNotNull<T>(this List<T> list)
        {
            return list != null && list.Count > 0;
        }
    }
}
