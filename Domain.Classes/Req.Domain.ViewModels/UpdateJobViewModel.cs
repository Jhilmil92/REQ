using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class UpdateJobViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int takerId { get; set; }
        [DataType(DataType.Time)]
        public DateTime EstimatedTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime ActualTimeTaken { get; set; }
    }
}
