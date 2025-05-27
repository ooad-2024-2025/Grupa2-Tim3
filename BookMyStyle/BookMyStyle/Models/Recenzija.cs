using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Recenzija
    {
        [Key]
        public int recenzijaID { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int Ocjena { get; set; }

        [Required]
        public DateTime DatumObjave { get; set; }

        [Required, StringLength(100)]
        public string Komentar { get; set; }

        [ForeignKey("korisnikID")]
        public int korisnikID { get; set; }


        [ForeignKey("salonID")]
        public int salonID { get; set; }
      
        
    }
}
