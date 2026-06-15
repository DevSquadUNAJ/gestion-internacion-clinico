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
using Clinico.Aplicacion;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System;


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
            builder.Services.AddScoped<IMedicoConsulta, MedicoConsulta>();

            // ==========================================
            // 5. DOMINIO: Catalogo CIE-10
            // ==========================================
            builder.Services.AddScoped<ICatalogoCie10Consulta, CatalogoCie10Consulta>();

            // ==========================================
            // 6. DOMINIO: Diagnostico
            // ==========================================
            builder.Services.AddScoped<IDiagnosticoComando, DiagnosticoComando>();
            builder.Services.AddScoped<IRegistrarDiagnosticoCasoDeUso, RegistrarDiagnosticoCasoDeUso>();

            // ==========================================
            // 7. DOMINIO: Evolucion Clinica
            // ==========================================
            builder.Services.AddScoped<IEvolucionClinicaComando, EvolucionClinicaComando>();
            builder.Services.AddScoped<IRegistrarEvolucionClinicaCasoDeUso, RegistrarEvolucionClinicaCasoDeUso>();

            // ==========================================
            // 7. DOMINIO: Evolucion Clinica
            // ==========================================
            builder.Services.AddScoped<IModificarTratamientoCasoDeUso, ModificarTratamientoCasoDeUso>();
            builder.Services.AddScoped<ITratamientoConsulta, TratamientoConsulta>();
            builder.Services.AddScoped<IFrecuenciaAdministracionConsulta, FrecuenciaAdministracionConsulta>();
            builder.Services.AddScoped<ITratamientoDosisConsulta, TratamientoDosisConsulta>();
            builder.Services.AddScoped<ITratamientoComando, TratamientoComando>();
            builder.Services.AddScoped<ITratamientoDosisComando, TratamientoDosisComando>();
            builder.Services.AddScoped<IObtenerSeguimientoTratamientoCasoDeUso, ObtenerSeguimientoTratamientoCasoDeUso>();

            // ==========================================
            // 8. DOMINIO: Auditoría
            // ==========================================
            builder.Services.AddScoped<IObtenerHistorialAuditoriaCasoDeUso, ObtenerHistorialAuditoriaCasoDeUso>();
            builder.Services.AddScoped<IHistorialAuditoriaConsulta, HistorialAuditoriaConsulta>();
            builder.Services.AddScoped<IHistorialAuditoriaMapper, HistorialAuditoriaMapper>();


            // ==========================================
            // CONFIGURACIÓN DE AUTENTICACIÓN JWT
            // ==========================================
            var configuracionJwt = builder.Configuration.GetSection("Jwt");
            var claveSecreta = Encoding.UTF8.GetBytes(configuracionJwt["Key"]!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones =>
                {
                    opciones.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuracionJwt["Issuer"],
                        ValidAudience = configuracionJwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(claveSecreta)
                    };
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();


            // Add services to the container.
            builder.Services.AddControllers();
            /*
             * 
            //builder.Services.AddInfraestructura(builder.Configuration);
            builder.Services.AddDbContext<ContextoBaseDeDatos>(opciones =>opciones.UseSqlServer(builder.Configuration.GetConnectionString("ClinicoDb")));
            //builder.Services.AddAplicacion(builder.Configuration);
            builder.Services.AddAplicacion();
            builder.Services.AddInfraestructura(builder.Configuration);

*/


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opciones =>
            {
                opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Pega tu token JWT directamente aquí. (Nota: NO escribas la palabra 'Bearer', Swagger lo agregará por ti automáticamente)."
                });

                opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<Middlewares.ManejadorGlobalExcepcionesMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}