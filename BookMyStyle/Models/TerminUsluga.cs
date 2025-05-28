using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class TerminUsluga
    {
        [Key]
        public int terminuslugaId { get; set; }
        [ForeignKey("terminID")]
        public int terminID { get; set; }
        [ForeignKey("uslugaID")]
        public int uslugaID { get; set; }
    }

}