using Domain.Classes.Req.Domain.Entities;
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
    public class Job : IComparable<Job>
    {
        public Job()
        {
            JobPriority = PriorityLevel.Medium;
        }
        public int JobId { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Display(Name = "Job Category")]
        public JobCategory JobCategory { get; set; }

        [Display(Name = "Created On")]
        //[Column(TypeName="datetime")]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Last Updated On")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Reported By")]
        public virtual Client ReportedBy { get; set; } 

        [ForeignKey("ReportedBy")]
        public int ReportedById { get; set; }

        [Display(Name = "Created By")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }

        [Display(Name = "Estimated Time")]
        public string EstimatedTime { get; set; }

        [NotMapped]
        public decimal EstimatedTimeHour 
        {
            get
            {
                if(string.IsNullOrWhiteSpace(EstimatedTime))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(EstimatedTime);
                }
            }
            set
            {
                    EstimatedTime = string.Format("{0}", value);

            }
        }

        [Display(Name = "Actual Time")]
        public string ActualTimeTaken { get; set; }

        [NotMapped]
        public decimal ActualTimeTakenHour
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ActualTimeTaken))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(ActualTimeTaken);
                }
            }
            set
            {
                    ActualTimeTaken = string.Format("{0}", value);
            }
        }

        [Display(Name = "Assigned To")]
        public virtual Taker AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public int? AssignedToId { get; set; }

        [Display(Name = "Job Priority")]
        public PriorityLevel JobPriority { get; set; }

        [Display(Name = "Job Status")]
        public JobStatus Status { get; set; }

        [Display(Name = "Release Version")]
        public string ReleaseVersion { get; set; }

        public string Comments { get; set; }

        public int CompareTo(Job other)
        {
            if(this.JobPriority < other.JobPriority)
            {
                return 1;
            }
            else if (this.JobPriority.Equals(other.JobPriority))
            {
                return 0;
            }
            else 
            {
                return -1;
            }
        }
    }
}
