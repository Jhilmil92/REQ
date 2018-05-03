using Req.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DateTime? CreatedOn { get; set; }
        public virtual StakeHolder ReportedBy { get; set; }

        [ForeignKey("ReportedBy")]
        public int ReportedById { get; set; }

        public DateTime? EstimatedTime { get; set; }
     
        public DateTime? ActualTimeTaken { get; set; }
        public Taker AssignedTo { get; set; }
        public PriorityLevel JobPriority { get; set; }
        public JobStatus Status { get; set; }
    }
}
