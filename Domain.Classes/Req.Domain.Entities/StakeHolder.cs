using Domain.Classes.Req.Domain.Entities;
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

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client ClientDetails { get; set; }

        //[Display(Name = "Username")]
        //public string UserName { get; set; }
        //public string Password { get; set; }
    }
}
