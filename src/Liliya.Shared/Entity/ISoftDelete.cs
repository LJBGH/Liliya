using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
