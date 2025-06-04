using System.ComponentModel.DataAnnotations;

public class SmtpSettings
{
    [Key]
    public int SmtpSettingsId { get; set; }

    [Required(ErrorMessage = "Host is required")]
    public string Host { get; set; } = string.Empty;

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    [Required(ErrorMessage = "Port is required")]
    public int Port { get; set; }

    [Required(ErrorMessage = "EnableSsl is required")]
    public bool EnableSsl { get; set; } = true;

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "From is required")]
    public string From { get; set; } = string.Empty;

    [Required(ErrorMessage = "DisplayName is required")]
    public string DisplayName
    {
        get; set;
    }
}
