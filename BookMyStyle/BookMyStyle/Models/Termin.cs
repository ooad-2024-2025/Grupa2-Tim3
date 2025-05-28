using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class Termin
    {
        [Key]
        public int terminID { get; set; }

        [StringLength(30)]
        public string NazivSalona { get; set; }

        [StringLength(50)]
        public string AdresaSalona { get; set; }

        [StringLength(20)]
        public string NazivFrizera { get; set; }
        [Required]

        public DateTime Datum { get; set; }

        [Required]
        public DateTime Vrijeme { get; set; }

        [ForeignKey("salonID")]
        public int salonID { get; set; }


        [ForeignKey("uslugaID")]
        public int uslugaID { get; set; }

    }

}