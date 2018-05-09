using Req.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class ReportRequestViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int StakeHolderId { get; set; }

        public string StakeHolderOrganization { get; set; }

        [Required]
        [MaxLength(255)]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }
        [Required]
        public JobCategory JobType { get; set; }
        //public string ReportedBy { get; set; }
        [Required]
        public PriorityLevel JobPriority { get; set; }

        public int EstimatedTimeInHours { get; set; }

        public int JobTakerId { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public string ReleaseVersion { get; set; }
    }
}
