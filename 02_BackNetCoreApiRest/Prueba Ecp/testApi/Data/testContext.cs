using Microsoft.EntityFrameworkCore;
using testEcpApi.Models;

namespace testEcpApi.Data
{
    public class testContext : DbContext
    {
        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public DbSet<Categorias> DbCategorias { get; set; } //Nombre representación del objeto en la BD
        public DbSet<Usuarios>  DbUsuarios { get; set; } //Nombre representación del objeto en la BD
        public DbSet<Preguntas> DbPreguntas{ get; set; } //Nombre representación del objeto en la BD

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>().ToTable("Categorias"); //Nombre objeto en la BD
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios"); //Nombre objeto en la BD
            modelBuilder.Entity<Preguntas>().ToTable("Preguntas"); //Nombre objeto en la BD

        }

    }

}
