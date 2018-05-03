using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class StakeHolder
    {
        [Key]
        public int StakeHolderId { get; set; }
        public string StakeHolderOrganization { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
