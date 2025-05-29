using System.ComponentModel.DataAnnotations;

public enum TipUsluge
{
    [Display(Name = "Šišanje")]
    Sisanje,

    [Display(Name = "Brijanje")]
    Brijanje,

    [Display(Name = "Pranje kose")]
    PranjeKose,

    [Display(Name = "Farbanje")]
    Farbanje,

    [Display(Name = "Feniranje")] 
    Feniranje
}