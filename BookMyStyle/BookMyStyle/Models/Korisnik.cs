using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookMyStyle.Models
{
    public class Korisnik : IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage =
            "Ime smije imati između 3 i 20 karaktera")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Dozvoljeno je korištenje velikih i malih slova i razmaka")]
        [DisplayName("Ime korisnika")]
        public string Ime { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage =
            "Prezime smije imati između 3 i 20 karaktera")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Dozvoljeno je korištenje velikih i malih slova i razmaka")]
        [DisplayName("Prezime korisnika")]
        public string Prezime { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage =
            "Username smije imati između 3 i 20 karaktera")]
        [RegularExpression(@"^[^\s]*$", ErrorMessage = "Nije dozvoljeno korištenje razmaka")]
        [DisplayName("Username korisnika")]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage =
            "Šifra smije imati između 5 i 20 karaktera")]
        [DisplayName("Šifra korisnika")]
        public string Password { get; set; }

        [EnumDataType(typeof(TipFrizera))] 
        public string tipFrizera { get; set; }

        [ForeignKey("terminID")]
        public int terminID { get; set; }
    }
}