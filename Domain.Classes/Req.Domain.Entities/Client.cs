using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Display(Name = "Client Organization")]
        public string ClientOrganization { get; set; }
        public DateTime? JoinDate { get; set; }

    }
}
