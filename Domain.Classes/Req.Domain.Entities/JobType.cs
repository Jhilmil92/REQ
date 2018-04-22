using Req.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class JobType
    {
        public int JobTypeID { get; set; }
        public JobCategory CategoryType { get; set; }
    }
}
