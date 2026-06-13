using Clinico.Infraestructura;
using Clinico.Infraestructura.Persistencia;
using Clinico.Aplicacion;
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

            // Add services to the container.
            builder.Services.AddControllers();
            //builder.Services.AddInfraestructura(builder.Configuration);
            builder.Services.AddDbContext<ContextoBaseDeDatos>(opciones =>opciones.UseSqlServer(builder.Configuration.GetConnectionString("ClinicoDb")));
            //builder.Services.AddAplicacion(builder.Configuration);
            builder.Services.AddAplicacion();
            builder.Services.AddInfraestructura(builder.Configuration);


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