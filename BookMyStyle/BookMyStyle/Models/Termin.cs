using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace BookMyStyle.Models
{
    public class Termin
    {
        [Key]
        public int terminID { get; set; }


        
        [StringLength(30,ErrorMessage = "Naziv salona moze imati najviše 30 karaktera!")]
        [DisplayName("Naziv salona:")]
        public string? NazivSalona { get; set; }


        
        [StringLength(50,ErrorMessage = "Adresa salona može imati najviše 50 karaktera!")]
        [RegularExpression(@"[0-9| |a-z|A-Z]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        [DisplayName("Adresa salona:")]
        public string? AdresaSalona { get; set; }


        
        [StringLength(20,ErrorMessage ="Naziv frizera može imati najviše 20 karaktera!")]
        [RegularExpression(@"^[A-ZŽĆČŠĐ][a-zžćčšđ]+ [A-ZŽĆČŠĐ][a-zžćčšđ]+$", ErrorMessage = "Unesite ime i prezime u formatu: Ime Prezime.")]
        [DisplayName("Ime i prezime frizera: ")]
        public string? NazivFrizera { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Datum i vrijeme moraju biti u budućnosti.")]
        [DisplayName("Datum i vrijeme termina: ")]
        public DateTime DatumIVrijeme { get; set; }


        [Required]
        [ForeignKey("salonID")]
        public int salonID { get; set; }


        [Required]
        [ForeignKey("uslugaID")]
        public int uslugaID { get; set; }

        // ➕ Korisnik koji je zakazao termin
        public string? KorisnikID { get; set; }

        [ForeignKey("KorisnikID")]
        public Korisnik? Korisnik { get; set; }

        // ➕ Frizer koji je kreirao termin
        public string? FrizerID { get; set; }

        [ForeignKey("FrizerID")]
        public Korisnik? Frizer { get; set; }

    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime > DateTime.Now;
            }
            return false;
        }
    }

}