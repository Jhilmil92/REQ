using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Req.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }
        [DataType(DataType.Time)]
        public DateTime EstimatedTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime ActualTimeTaken { get; set; }
        public JobStatus JobStatus { get; set; }
    }
}
