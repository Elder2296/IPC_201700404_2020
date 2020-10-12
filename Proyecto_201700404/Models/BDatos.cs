namespace Proyecto_201700404.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BDatos : DbContext
    {
        public BDatos()
            : base("name=BDatos")
        {
        }

        public virtual DbSet<CIUDAD> CIUDAD { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CIUDAD>()
                .Property(e => e.nombre_pais)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.nombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.tipoUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.contrasenia)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.email)
                .IsUnicode(false);
        }
    }
}
