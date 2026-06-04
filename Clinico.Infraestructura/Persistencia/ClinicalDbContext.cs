using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clinico.Dominio.Entidades;

namespace Clinico.Infraestructura.Persistencia
{

    public class ClinicalDbContext : DbContext
    {
        public ClinicalDbContext(
            DbContextOptions<ClinicalDbContext> options)
            : base(options)
        {
        }
        // Agregando DbSet para cada entidad
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
        /*public DbSet<Sector> Sectores => Set<Sector>();*/
        public DbSet<TratamientoDosis> TratamientosDosis => Set<TratamientoDosis>();
        public DbSet<AuditoriaIA> AuditoriasIA => Set<AuditoriaIA>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ClinicalDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
