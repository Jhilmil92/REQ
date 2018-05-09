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
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public JobCategory JobCategory { get; set; }

        public DateTime? CreatedOn { get; set; }
        public virtual StakeHolder ReportedBy { get; set; }

        [ForeignKey("ReportedBy")]
        public int ReportedById { get; set; }

        public string EstimatedTime { get; set; }

        [NotMapped]
        public int EstimatedTimeHour 
        {
            get
            {
                if(string.IsNullOrWhiteSpace(EstimatedTime))
                {
                    return 0;
                }
                else
                {
                    var hourPart = EstimatedTime.Split(':')[0];
                    return Convert.ToInt32(hourPart);
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(EstimatedTime))
                {
                    EstimatedTime = string.Format("{0}:0", value);
                }
                else
                {
                    var estimatedTimeSplit = EstimatedTime.Split(':');
                    EstimatedTime = string.Format("{0}:{1}", value, estimatedTimeSplit[1]);
                }

            }
        }

        [NotMapped]
        public int EstimatedTimeMinute 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EstimatedTime))
                {
                    return 0;
                }
                else
                {
                    var minPart = EstimatedTime.Split(':')[1];
                    return Convert.ToInt32(minPart);
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(EstimatedTime))
                {
                    EstimatedTime = string.Format("0:{0}", value);
                }
                else
                {
                    var estimatedTimeSplit = EstimatedTime.Split(':');
                    EstimatedTime = string.Format("{0}:{1}", estimatedTimeSplit[0], value);
                }
            }
        }
     
        public string ActualTimeTaken { get; set; }

        [NotMapped]
        public int ActualTimeTakenHour
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ActualTimeTaken))
                {
                    return 0;
                }
                else
                {
                    var hourPart = ActualTimeTaken.Split(':')[0];
                    return Convert.ToInt32(hourPart);
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(ActualTimeTaken))
                {
                    ActualTimeTaken = string.Format("{0}:0", value);
                }
                else
                {
                    var actualTimeTakenSplit = ActualTimeTaken.Split(':');
                    ActualTimeTaken = string.Format("{0}:{1}", value, actualTimeTakenSplit[1]);
                }
            }
        }

        [NotMapped]
        public int ActualTimeTakenMinute
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ActualTimeTaken))
                {
                    return 0;
                }
                else
                {
                    var minPart = ActualTimeTaken.Split(':')[1];
                    return Convert.ToInt32(minPart);
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(ActualTimeTaken))
                {
                    ActualTimeTaken = string.Format("0:{0}", value);
                }
                else
                {
                    var actualTimeTakenSplit = ActualTimeTaken.Split(':');
                    ActualTimeTaken = string.Format("{0}:{1}", actualTimeTakenSplit[0], value);
                }
            }
        }

        public virtual Taker AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public int? AssignedToId { get; set; }

        public PriorityLevel JobPriority { get; set; }

        public JobStatus Status { get; set; }

        public string ReleaseVersion { get; set; }

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
