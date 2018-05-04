using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Taker
    {
        [Key]
        public int TakerId { get; set; }
        public string TakerName { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual Role TakerRole { get; set; }

        [ForeignKey("TakerRole")]
        public int? TakerRoleId { get; set; }
    }
}
