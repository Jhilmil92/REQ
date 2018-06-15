using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Display(Name = "Username")]
        [Index("IX_UserName", 1, IsUnique = true)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public UserType UserType{ get; set; }

        public int TargetUserID { get; set; }

    }

    public enum UserType
    {
        StakeHolder,
        Taker
    }
}
