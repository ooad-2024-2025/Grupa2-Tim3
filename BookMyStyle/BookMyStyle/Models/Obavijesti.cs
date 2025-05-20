using System;
using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class Obavijest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Tekst { get; set; }

        [Required]
        public DateTime DatumIVrijeme { get; set; }
    }
}
