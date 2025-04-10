using Microsoft.EntityFrameworkCore;
namespace Parcial.Models
{
    public class restauranteDbContext : DbContext
    {
        public restauranteDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<mesas> mesas { get; set; }
        public DbSet<pedidos_proceso> pedidos_proceso { get; set; }
        public DbSet<meseros> meseros { get; set; }
        public DbSet<platos> platos { get; set; }
        public DbSet<cocineros> cocineros { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
