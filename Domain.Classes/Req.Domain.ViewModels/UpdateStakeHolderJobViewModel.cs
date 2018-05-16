using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Req.Enums;
using System.Web;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateStakeHolderJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public JobCategory JobType { get; set; }

        [DisplayFormat(DataFormatString = "{0}")]
        public int EstimatedTimeInHours { get; set; }

        public int? AssignedTakerId { get; set; }

        public HttpPostedFileBase[] Files { get; set; }

        public string[] FileNames { get; set; }

        public string ReleaseVersion { get; set; }
    }
}
