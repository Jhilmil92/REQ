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
        Approved = 2,
        InProcess = 3,
        Completed = 4,
        UAT = 5,
        Released = 6
    }
}
