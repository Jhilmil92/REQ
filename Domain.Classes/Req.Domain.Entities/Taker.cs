using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Taker
    {
        public int TakerId { get; set; }
        public string TakerName { get; set; }
        public ICollection<Job> Jobs { get; set; }

    }
}
