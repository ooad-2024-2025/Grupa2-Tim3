using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Termin
{
    [Key]
    public int ID { get; set; }

    [StringLength(50)]
    public string NazivSalona { get; set; }

    [StringLength(50)]
    public string AdresaSalona { get; set; }

    [StringLength(50)]
    public string NazivFrizera { get; set; }

    public Usluga Usluga { get; set; }

    public DateTime DatumIVrijeme { get; set; }
}