using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class TakerRegistrationViewModel
    {
        [Required]
        public string TakerName { get; set; }
        [Required(ErrorMessage = "Please Enter a UserName")]
        public string TakerUserName { get; set; }
        [Required(ErrorMessage = "Please Enter a Password")]
        public string TakerPassword { get; set; }
        [Required(ErrorMessage = "Please Confirm your Password")]
        public string TakerConfirmPassword { get; set; }
    }
}
