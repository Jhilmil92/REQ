using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Please Enter the UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage="Please Enter the Password")]
        public string Password { get; set; }
    }
}
