using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class Salon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(200)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(50)]
        public string RadnoVrijeme { get; set; }

       
    }
}
