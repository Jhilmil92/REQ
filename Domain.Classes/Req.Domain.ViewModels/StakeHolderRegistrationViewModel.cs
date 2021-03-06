﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class StakeHolderRegistrationViewModel
    {
        //[Required(ErrorMessage="Please Enter your Organization Name")]
        //public string StakeHolderOrganization { get; set; }

        [Required(ErrorMessage = "Please Enter a Client")]
        public int StakeholderClientId { get; set; }
        [Required(ErrorMessage = "Please Enter a UserName")]
        public string StakeHolderUserName { get; set; }
        [Required(ErrorMessage = "Please Enter a Password")]
        public string StakeHolderPassword { get; set; }
        [Required(ErrorMessage = "Please Confirm your Password")]
        public string StakeHolderConfirmPassword { get; set; }
    }
}
