using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class Salon
    {
        [Key]
        public int salonID { get; set; }

        [Required, StringLength(30)]
        public string Naziv { get; set; }

        [Required, StringLength(50)]
        public string Adresa { get; set; }

        [StringLength(20)]
        public string RadnoVrijeme { get; set; }

       

        

        

       
    }
}
