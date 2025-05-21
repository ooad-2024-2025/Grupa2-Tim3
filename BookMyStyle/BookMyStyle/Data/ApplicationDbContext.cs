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
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<Termin> Termin { get; set; }

        
    }
}
