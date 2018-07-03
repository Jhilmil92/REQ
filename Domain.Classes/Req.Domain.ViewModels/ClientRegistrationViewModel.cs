using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.ViewModels
{
    public class ClientRegistrationViewModel
    {
        [Required]
        public string ClientOrganization { get; set; }

        [Required]
        public DateTime? JoinDate { get; set; }
    }
}
