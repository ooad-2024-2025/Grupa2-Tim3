﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
   

    public class Usluga
    {
        [Key]
        public int uslugaID { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Cijena usluge ne može biti negativna!")]
        [DisplayName("Cijena usluge:")]
        public double Cijena { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Naziv usluge može imati najviše 30 karaktera!")]
        [RegularExpression(@"^[A-Za-zČĆŽŠĐčćžšđ\s]+$", ErrorMessage = "Naziv usluge može sadržavati samo slova i razmake.")]
        [DisplayName("Naziv usluge:")]
        public string Naziv { get; set; }

        [Required]
        [DisplayName("Popust [%]:")]
        [Range(0.0, 100.00, ErrorMessage = "Popust mora biti u opsegu od 0% - 100%")]
        public double Popust { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Opis usluge smije imati između 3 i 200 karaktera!")]
        [DisplayName("Opis:")]
        public string Opis { get; set; }

        [Required]
        [DisplayName("Trajanje [min]:")]
        public int Trajanje { get; set; }

        [EnumDataType(typeof(TipUsluge))]
        [DisplayName("Tip usluge:")]
        public TipUsluge Tip { get; set; }

        
        [ForeignKey("Salon")]
        public int salonID { get; set; }

        public Salon Salon { get; set; }

    }
}
