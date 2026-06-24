using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IExternos
{
    public interface IValidadorClinicoIA
    {
        Task<AnalisisIAResultado> AnalizarAsync(
            ContextoClinicoIADto contexto,
            CancellationToken cancellationToken);
    }

    public class AnalisisIAResultado
    {
        public AnalisisIADto? Analisis { get; set; }
        public string? PayloadJsonCrudo { get; set; } 
        public bool Exitoso { get; set; }
        public bool FueTimeout { get; set; }
        public string? ErrorMensaje { get; set; }
    }
}