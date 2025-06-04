using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class Salon
    {
        [Key]
        public int salonID { get; set; }

        [Required(ErrorMessage = "Naziv salona je obavezan.")]
        [StringLength(30, ErrorMessage = "Naziv salona može imati najviše 30 karaktera.")]
        [MinLength(3, ErrorMessage = "Naziv salona mora imati najmanje 3 karaktera.")]
        public string Naziv { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Adresa salona može imati najviše 50 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        [MinLength(3, ErrorMessage = "Adresa salona mora imati najmanje 3 karaktera.")]
        public string Adresa { get; set; }

        [MinLength(11, ErrorMessage = "Radno vrijeme mora imati minimalno 11 karaktera (npr. 08:00 - 21:00).")]
        [RegularExpression(@"^([01]\d|2[0-3]|24):([0-5]\d) - ([01]\d|2[0-3]|24):([0-5]\d)$",
            ErrorMessage = "Radno vrijeme mora biti u formatu HH:mm - HH:mm (24-satno vrijeme).")]
        public string RadnoVrijeme { get; set; }

        
        public ICollection<Usluga> Usluga { get; set; }
        public ICollection<Termin> Termin { get; set; }
        
    }
}
