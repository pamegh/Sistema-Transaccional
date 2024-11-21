
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
namespace Proyecto.BD

{
    public class AppBDContext: DbContext
    {
        public AppBDContext(DbContextOptions<AppBDContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<CuentaModel> Cuentas { get; set; }
        public DbSet<TransaccionModel> Transacciones { get; set; }
        public DbSet<AuditoriaModel> Auditorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().HasKey(e => e.Id_Usuario);
            modelBuilder.Entity<CuentaModel>().HasKey(e => e.Id_Cuenta);
            modelBuilder.Entity<TransaccionModel>().HasKey(e => e.Id_Transaccion);
            modelBuilder.Entity<AuditoriaModel>().HasKey(e => e.Id_Auditoria);
        }
    }
}
