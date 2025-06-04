using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    
    
        public class QRCodeModel
        {
            [Display]
            [Required]
            [Key]
            public int Id { get; set; }
        [Display]
            [Required]
            [StringLength(100, ErrorMessage = "Unesite e-mail u ispravnom formatu!")]
        public string QRCodeText { get; set; }
        }
    
}
