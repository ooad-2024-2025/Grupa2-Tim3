using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Recenzija
    {
        [Key]
        public int recenzijaID { get; set; }


       
        [Range(1, 5, ErrorMessage = "Unesena ocjena mora biti između 1 i 5.")]
        [DisplayName("Ocjena:")]
        public int Ocjena { get; set; }


       
        [DataType(DataType.DateTime)]
        [TestDate(ErrorMessage = "Datum i vrijeme moraju biti u sadašnjosti ili prošlosti.")]
        [DisplayName("Datum i vrijeme objave: ")]
        public DateTime DatumObjave { get; set; }

        [Required(ErrorMessage = "Komentar je obavezan.")]
        [StringLength(maximumLength:150,MinimumLength =10, ErrorMessage = "Komentar može imati između 10 i 150 znakova!")]
        [RegularExpression(@"[0-9| |a-z|A-Z]*", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        [DisplayName("Komentar:")]
        public string Komentar { get; set; }


      

        //  Korisnik koji je napisao recenziju
        public string? KorisnikID { get; set; }

        [ForeignKey("KorisnikID")]
        public Korisnik? Korisnik { get; set; }

        // salon za koji je napisana recenzija
        public int? SalonID { get; set; }

        [ForeignKey("SalonID")]
        public Salon? Salon { get; set; }

    }


    public class TestDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {

                return dateTime <= DateTime.Now;
            }
            return false;
        }
    }

}



