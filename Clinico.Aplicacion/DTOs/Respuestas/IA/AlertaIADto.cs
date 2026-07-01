using Clinico.Dominio.Constantes;

namespace Clinico.Aplicacion.DTOs.Respuestas.IA
{
    public class AlertaIADto
    {
        public TipoAlertaIA Tipo { get; set; }
        public SeveridadAlertaIA Severidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}