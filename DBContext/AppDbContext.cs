using MicroServicioCM.Entidades.Context;
using Microsoft.EntityFrameworkCore;

namespace MicroServicioCM.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CuentaContext> Cuenta { get; set; }
        public DbSet<MovimientoContext> Movimiento { get; set; }
    }
}
