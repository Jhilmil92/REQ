﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Req.Enums;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateStakeHolderJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public JobCategory JobType { get; set; }

        public int EstimatedTimeInHours { get; set; }

        public int? AssignedTakerId { get; set; }

        public string ReleaseVersion { get; set; }
    }
}
