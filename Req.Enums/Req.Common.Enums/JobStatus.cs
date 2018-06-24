using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Req.Enums
{
    public enum JobStatus
    {
        Queued = 1, 
        Assigned = 2,
        InProcess = 3,
        Incubate = 4, 
        Completed = 5,
        Released = 6
    }
}
