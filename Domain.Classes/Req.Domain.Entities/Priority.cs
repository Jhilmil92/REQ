using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Req.Enums;

namespace Domain.Classes
{
    public class Priority
    {
        public int PriorityID { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
    }
}
