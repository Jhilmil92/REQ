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
        [Display(Name="Client Organization")]
        public string ClientOrganization { get; set; }

        [Display(Name = "Join Date")]
        public DateTime? JoinDate { get; set; }
    }
}
