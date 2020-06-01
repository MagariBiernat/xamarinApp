using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataAccess
{
    [Table("Msgs")]
    public class Msgs
    {
        [Column("Message_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Message_ID { get; set; }

        [Column("Value")]
        [Required]
        [StringLength(1000)]
        public string Value { get; set; }

        [Required]
        public int User_ID_From { get; set; }

        [Required]
        public int User_ID_To { get; set; }

        [Required]
        public bool Read { get; set; }
    }
}
