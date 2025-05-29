using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Obavijest
    {
        [Key]
        public int obavijestID { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 3, ErrorMessage ="Obavijest treba imati između 3 i 1000 karaktera, kako bi klijentu bila jasna poruka")]
        public string Tekst { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Future(ErrorMessage = "Datum i vrijeme moraju biti u budućnosti.")]
        [DisplayName("Datum i vrijeme: ")]
        public DateTime DatumIVrijeme { get; set; }

    

        [Required]
        [ForeignKey("terminID")]
        public int terminID { get; set; }
    }

    public class FutureAttribute : ValidationAttribute
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