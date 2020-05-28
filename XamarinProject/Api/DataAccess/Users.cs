using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataAccess
{
    [Table("Users")]
    public class Users  
    {
        [Column("User_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int User_ID { get; set; }


        [Column("Username")]
        [Required]
        [StringLength(40)]
        public string Username { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(40)]
        public string Password { get; set; }

        [Column("IsOnline")]
        [Required]
        [StringLength(7)]
        public string IsOnline { get; set; }

        [ForeignKey("User_ID")]
        public virtual List<Messages> Messages { get; set; }
    }
}
