using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class HeroiContext : DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<Arma> Armas { get; set; }
        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadeSecretas { get; set; }

        public HeroiContext(DbContextOptions<HeroiContext> options) : base(options)
        {

        }

    /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
             //O BD pode ser referenciado aqui, ou na classe Startup em configuration services.
            optionsBuilder.UseSqlServer("Password=senha123;Persist Security Info=True;User ID=sa;Initial Catalog=master;Data Source=DESKTOP-JKR5P22");
            
        }
     */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity => 
            {
                entity.HasKey(e => new
                {
                    e.BatalhaId, e.HeroiId
                });
            });
        }
    }
}
