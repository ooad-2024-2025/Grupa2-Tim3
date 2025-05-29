using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace BookMyStyle.Models
{
    public class Usluga
    {
        [Key]
        public int uslugaID { get; set; }

       
        [Required]
        [Range(1.0, 200.0, ErrorMessage = "Cijena usluge ne moze biti manja od 1.0 i veća od 200.0")]  
        [DisplayName("Cijena usluge:")]
        public double Cijena { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = "Naziv usluge moze imati najviše 30 karaktera!")]
        [DisplayName("Naziv usluge:")]
        public string Naziv { get; set; }


        [Required]
        [DisplayName("Popust:")]
        [Range(0.0, 20.00, ErrorMessage = "Popust ne može biti veći za Vašu uslugu")]
        public double Popust { get; set; }

        
        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage ="Opis usluge smije imati između 3 i 200 karaktera!")]
        [DisplayName("Opis:")]
        public string Opis { get; set; }


        [Required]
        [DisplayName("Trajanje:")]
       
        public int Trajanje { get; set; }


        [EnumDataType(typeof(TipUsluge))]   public TipUsluge Tip
        { get; set; }
      



        [Required]
        [ForeignKey("salonID")]
        public int salonID { get; set; }




    }
}