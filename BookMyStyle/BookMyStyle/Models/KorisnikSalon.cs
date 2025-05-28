using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyStyle.Models
{
    public class KorisnikSalon
    {
        [Key]
        public int korisniksalonId { get; set; }
        [ForeignKey("salonID")]
        public int salonID { get; set; }
        [ForeignKey("korisnikID")]
        public int korisnikID { get; set; }
    }
}