using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookMyStyle.Models;

namespace BookMyStyle.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<KorisnikSalon> KorisnikSalon { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Termin> Termin { get; set; }
        public DbSet<TerminUsluga> TerminUsluga { get; set; }
        public DbSet<Usluga> Usluga { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Obavijest>().ToTable("Obavijest");
            modelBuilder.Entity<Recenzija>().ToTable("Recenzija");
            modelBuilder.Entity<Salon>().ToTable("Salon");
            modelBuilder.Entity<Termin>().ToTable("Termin");
            modelBuilder.Entity<Usluga>().ToTable("Usluga");
            modelBuilder.Entity<KorisnikSalon>().ToTable("KorisnikSalon");
            modelBuilder.Entity<TerminUsluga>().ToTable("TerminUsluga");

            modelBuilder.Entity<Korisnik>(b =>
            {
                b.Property(u => u.Ime);
                b.Property(u => u.Prezime);            
                b.Property(u => u.tipFrizera);
                b.Property(u => u.terminID);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
