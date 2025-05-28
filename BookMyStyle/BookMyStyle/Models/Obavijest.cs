using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Obavijest
    {
        [Key]
        public int obavijestID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Tekst { get; set; }

        [Required]
        public DateTime DatumIVrijeme { get; set; }


        [ForeignKey("terminID")]
        public int terminID { get; set; }
    }
}