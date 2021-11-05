using System;
using System.Collections.Generic;
using System.Text;

namespace AkliaJob.Shared
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
