using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Aplicacion.Mapeadores;
using Clinico.Infraestructura;
using Clinico.Infraestructura.Comandos;
using Clinico.Infraestructura.Consultas;
using Clinico.Infraestructura.Persistencia;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Clinico.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==========================================
            // 1. CONFIGURACIÓN BASE (Base de Datos)
            // ==========================================
            builder.Services.AddInfraestructura(builder.Configuration);

            // ==========================================
            // 2. DOMINIO: AUTH & USERS (Usuarios y Login)
            // ==========================================


            // ==========================================
            // 3. DOMINIO: Historia Clinica
            // ==========================================
            builder.Services.AddScoped<IHistoriaClinicaConsulta, HistoriaClinicaConsulta>();
            builder.Services.AddScoped<IHistoriaClinicaMapper, HistoriaClinicaMapper>();
            builder.Services.AddScoped<IObtenerHistoriaClinicaCasoDeUso, ObtenerHistoriaClinicaCasoDeUso>();

            // ==========================================
            // 4. DOMINIO: Medico
            // ==========================================
            builder.Services.AddScoped<IMedicoConsulta,MedicoConsulta>();

            // ==========================================
            // 5. DOMINIO: Catalogo CIE-10
            // ==========================================
            builder.Services.AddScoped<ICatalogoCie10Consulta,CatalogoCie10Consulta>();

            // ==========================================
            // 6. DOMINIO: Diagnostico
            // ==========================================
            builder.Services.AddScoped<IDiagnosticoComando,DiagnosticoComando>();
            builder.Services.AddScoped<IRegistrarDiagnosticoCasoDeUso,RegistrarDiagnosticoCasoDeUso>();

            // ==========================================
            // 7. DOMINIO: Evolucion Clinica
            // ==========================================
            builder.Services.AddScoped<IEvolucionClinicaComando, EvolucionClinicaComando>();
            builder.Services.AddScoped<IRegistrarEvolucionClinicaCasoDeUso, RegistrarEvolucionClinicaCasoDeUso>();

            // ==========================================
            // 7. DOMINIO: Evolucion Clinica
            // ==========================================
            builder.Services.AddScoped<IModificarTratamientoCasoDeUso,ModificarTratamientoCasoDeUso>();
            builder.Services.AddScoped<ITratamientoConsulta,TratamientoConsulta>();
            builder.Services.AddScoped<IFrecuenciaAdministracionConsulta,FrecuenciaAdministracionConsulta>();
            builder.Services.AddScoped<ITratamientoDosisConsulta,TratamientoDosisConsulta>();
            builder.Services.AddScoped<ITratamientoComando,TratamientoComando>();
            builder.Services.AddScoped<ITratamientoDosisComando,TratamientoDosisComando>();
            builder.Services.AddScoped<IObtenerSeguimientoTratamientoCasoDeUso, ObtenerSeguimientoTratamientoCasoDeUso>();


            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}