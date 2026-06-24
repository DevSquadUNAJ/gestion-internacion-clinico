namespace Clinico.Aplicacion.DTOs.Respuestas.IA
{
    public class SugerenciaIADto
    {
        public bool Aplicar { get; set; }
        public string? MedicamentoAlternativo { get; set; }
        public decimal? Dosis { get; set; }
        public string? Unidad { get; set; }
        public string? Frecuencia { get; set; }
        public string Justificacion { get; set; } = string.Empty;
    }
}