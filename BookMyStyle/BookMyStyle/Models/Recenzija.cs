using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Recenzija
    {
        [Key]
        public int ID { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int Ocjena { get; set; }

        [Required]
        public DateTime DatumObjave { get; set; }

        [Required]
        [StringLength(1000)]
        public string Komentar { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [ForeignKey("Salon")]
        public int SalonID { get; set; }
        

    }
}
