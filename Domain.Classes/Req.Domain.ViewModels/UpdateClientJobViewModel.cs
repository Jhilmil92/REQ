using Req.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateClientJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string JobDescription { get; set; }
        public JobCategory JobType { get; set; }
        public JobStatus JobStatus { get; set; }
        public PriorityLevel JobPriority { get; set; }
        public string ReportedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal EstimatedTimeInHours { get; set; }
        public decimal ActualTimeTakenHrPart { get; set; }
        public int? AssignedTakerId { get; set; }

        public HttpPostedFileBase[] Files { get; set; }

        public string[] FileNames { get; set; }

        public string ReleaseVersion { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}
