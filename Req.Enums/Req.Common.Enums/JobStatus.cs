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
        Unassigned = 2,
        Assigned = 3,
        InProcess = 4,
        Completed = 5,
        UAT = 6,
        Released = 7
    }
}
