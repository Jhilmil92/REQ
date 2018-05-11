using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class StakeHolder
    {
        [Key]
        public int StakeHolderId { get; set; }

        [Display(Name = "Stakeholder Organization")]
        public string StakeHolderOrganization { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
