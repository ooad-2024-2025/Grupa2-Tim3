using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Termin
{
    [Key]
    public int ID { get; set; }


    [Required]
    [StringLength(50)]
    public string NazivSalona { get; set; }


    [Required]
    [StringLength(50)]
    public string AdresaSalona { get; set; }


    [Required]
    [StringLength(50)]
    public string NazivFrizera { get; set; }

    [Required]
    public Usluga Usluga { get; set; }


    [Required]
    public DateTime DatumIVrijeme { get; set; }
}