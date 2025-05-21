using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string Username { get; set; } 

        [Required]
        public string Password { get; set; }

        [Required]
        public bool DaLiDolaziNaAdresu { get; set; }  

        [ForeignKey("Salon")]
        public int SalonID { get; set; }

        public virtual Salon Salon { get; set; }  
    }
}
