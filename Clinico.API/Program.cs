using Clinico.Aplicacion;
using Clinico.Infraestructura;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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
            // 1. INYECCI�N DE DEPENDENCIAS (CAPAS)
            // ==========================================
            builder.Services.AddAplicacion();
            builder.Services.AddInfraestructura(builder.Configuration);

            // ==========================================
            // 2. CONFIGURACI�N DE AUTENTICACI�N JWT
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

            // ==========================================
            // 3. SEGURIDAD ADICIONAL Y SERVICIOS API
            // ==========================================
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<Aplicacion.Interfaces.ISeguridad.ITokenUsuarioActual, Servicios.TokenUsuarioActual>();

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // ==========================================
            // 4. SWAGGER
            // ==========================================
            builder.Services.AddSwaggerGen(opciones =>
            {
                opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Pega tu token JWT directamente aqu�. (Nota: NO escribas la palabra 'Bearer')."
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

            // ==========================================
            // 5. PIPELINE HTTP (MIDDLEWARES)
            // ==========================================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<Middlewares.ManejadorGlobalExcepcionesMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}