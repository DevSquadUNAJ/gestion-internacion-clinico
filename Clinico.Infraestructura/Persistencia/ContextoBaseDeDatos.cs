using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Clinico.Infraestructura.Persistencia
{

    public class ContextoBaseDeDatos : DbContext
    {
        public ContextoBaseDeDatos(
            DbContextOptions<ContextoBaseDeDatos> options)
            : base(options)
        {
        }
        public DbSet<HistoriaClinica> HistoriasClinicas => Set<HistoriaClinica>();
        public DbSet<Diagnostico> Diagnosticos => Set<Diagnostico>();
        public DbSet<EvolucionClinica> EvolucionesClinicas => Set<EvolucionClinica>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<CatalogoCie10> CatalogosCie10 => Set<CatalogoCie10>();
        public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
        public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();
        public DbSet<UnidadMedida> UnidadesMedida => Set<UnidadMedida>();
        public DbSet<FrecuenciaAdministracion> FrecuenciasAdministracion => Set<FrecuenciaAdministracion>();
        public DbSet<Enfermera> Enfermeras => Set<Enfermera>();
        public DbSet<TratamientoDosis> TratamientosDosis => Set<TratamientoDosis>();
        public DbSet<AuditoriaIA> AuditoriasIA => Set<AuditoriaIA>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ContextoBaseDeDatos).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
