﻿using System;
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

        [DataType(DataType.MultilineText)]
        public string JobDescription { get; set; }
        public JobCategory JobType { get; set; }
        public JobStatus JobStatus { get; set; }
        public PriorityLevel JobPriority { get; set; }
        public string ReportedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:d-MMM-yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedOn { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public decimal EstimatedTimeInHours { get; set; }
        public decimal ActualTimeTakenHrPart { get; set; }
        public int? AssignedTakerId { get; set; }

        public HttpPostedFileBase[] Files { get; set; }

        public string[] FileNames { get; set; }

        public string ReleaseVersion { get; set; }

        [DisplayFormat(DataFormatString = "{0:d-MMM-yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? LastUpdatedOn { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}
