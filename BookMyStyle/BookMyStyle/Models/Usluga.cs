using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Usluga
    {
        [Key]
        public int uslugaID { get; set; }

        [Required]
        public double Cijena { get; set; }

        [StringLength(20)]
        public string Naziv { get; set; }
        [Required]
        public double Popust { get; set; }

        [StringLength(200)]
        public string Opis { get; set; }
        
        [Required]

        public int Trajanje { get; set; }

        [StringLength(20)]
        public string Tip { get; set; }

        [ForeignKey("salonID")]
        public int salonID { get; set; }
      
       

        
    }
}
