using BookMyStyle.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookMyStyle.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        
       
        
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<KorisnikSalon> KorisnikSalon { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Termin> Termin { get; set; }
        public DbSet<TerminUsluga> TerminUsluga { get; set; }
        public DbSet<Usluga> Usluga { get; set; }


    }
}
