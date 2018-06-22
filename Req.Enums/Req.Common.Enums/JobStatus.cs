using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Req.Enums
{
    public enum JobStatus
    {
        Queued = 1, //New.
        Unassigned = 2, //Remove.
        Assigned = 3,
        InProcess = 4,//In Progress.
        //Incubate - new status to be added.
        Completed = 5, //ready
        UAT = 6,//remove.
        Released = 7
    }
}
