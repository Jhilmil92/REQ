using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Req.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;


namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string JobDescription { get; set; }
        public JobCategory JobType { get; set; }

        [DisplayFormat(DataFormatString="{0}")]
        public decimal EstimatedTimeHrPart { get; set; }
        public decimal ActualTimeTakenHrPart { get; set; }
        public string ReportedBy { get; set; }
        public string AssignedTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public JobStatus JobStatus { get; set; }
        public PriorityLevel JobPriority { get; set; }
        public string ReleaseVersion { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public string[] FileNames { get; set; }
    }
}
