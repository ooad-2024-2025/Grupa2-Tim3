using System.ComponentModel.DataAnnotations;

namespace BookMyStyle.Models
{
    public class EmailQrViewModel
    {
        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Molimo unesite ispravan email.")]
        [Display(Name = "Vaš email")]
        public string Email { get; set; }

        // Nakon uspješne POST akcije, ovdje ćemo pohraniti Base64 verziju QR koda
        // (npr. "data:image/png;base64,....").
        public string QrCodeImageBase64 { get; set; }
    }
}
