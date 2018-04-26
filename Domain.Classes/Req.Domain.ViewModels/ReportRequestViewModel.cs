using Req.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class ReportRequestViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int StakeHolderId { get; set; }

        [Required]
        public string JobDescription { get; set; }
        [Required]
        public JobCategory JobType { get; set; }
        [Required]
        public string ReportedBy { get; set; }
        [Required]
        public PriorityLevel JobPriority { get; set; }
    }
}
