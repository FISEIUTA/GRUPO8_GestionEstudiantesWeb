using GestionEstudiantesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantesWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrera>()
                .HasMany(c => c.Estudiantes)
                .WithOne(e => e.oCarrera)
                .HasForeignKey(e => e.IdCarrera)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carrera>()
                .HasMany(c => c.Niveles)
                .WithOne(n => n.oCarrera)
                .HasForeignKey(n => n.IdCarrera)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nivel>()
                .HasMany(n => n.Materias)
                .WithOne(m => m.oNivel)
                .HasForeignKey(m => m.IdNivel)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Docente>()
                .HasMany(d => d.Materias)
                .WithOne(m => m.oDocente)
                .HasForeignKey(m => m.IdDocente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Matriculas)
                .WithOne(m => m.oEstudiante)
                .HasForeignKey(m => m.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Materia>()
                .HasMany(m => m.Matriculas)
                .WithOne(mt => mt.oMateria)
                .HasForeignKey(mt => mt.IdMateria)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasMany(m => m.Notas)
                .WithOne(n => n.oMatricula)
                .HasForeignKey(n => n.IdMatricula)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nota>()
                .Property(n => n.Calificacion)
                .HasPrecision(5, 2);
        }

    }
}
