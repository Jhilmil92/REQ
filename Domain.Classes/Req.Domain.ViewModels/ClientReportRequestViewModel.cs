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
    public class ClientReportRequestViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int StaffId { get; set; }

        public int StakeholderClientId { get; set; }

        [Required]
        [MaxLength(255)]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string JobDescription { get; set; }
        [Required]
        public JobCategory JobType { get; set; }
        //public string ReportedBy { get; set; }
        [Required]
        public PriorityLevel JobPriority { get; set; }

        public JobStatus JobStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal EstimatedTimeInHours { get; set; }

        public int JobTakerId { get; set; }

        // [DataType(DataType.Upload)]
        public HttpPostedFileBase[] Files { get; set; }

        [Required]
        public string ReleaseVersion { get; set; }
    }
}
