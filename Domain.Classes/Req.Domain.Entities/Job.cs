using Req.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobDescription { get; set; }
        public JobCategory JobType { get; set; }

        [DataType(DataType.Date,ErrorMessage = "Add a valid Date")]
        public DateTime CreatedOn { get; set; }
        public StakeHolder ReportedBy { get; set; }
        [DataType(DataType.Time, ErrorMessage = "Add a valid Time")]
        public DateTime EstimatedTime { get; set; }
        [DataType(DataType.Time, ErrorMessage = "Enter a valid Time")]
        public DateTime ActualTimeTaken { get; set; }
        public string AssignedTo { get; set; }
        public PriorityLevel JobPriority { get; set; }
        
    }
}
