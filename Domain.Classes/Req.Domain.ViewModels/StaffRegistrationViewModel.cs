using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class StaffRegistrationViewModel
    {
        [Required]
        public string StaffName { get; set; }
        [Required(ErrorMessage = "Please Enter a UserName")]
        public string StaffUserName { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        public string StaffPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm your Password")]
        public string StaffConfirmPassword { get; set; }
    }
}
