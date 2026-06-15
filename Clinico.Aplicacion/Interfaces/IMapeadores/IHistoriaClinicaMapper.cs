using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IMapeadores
{
    public interface IHistoriaClinicaMapper
    {
        ObtenerHistoriaClinicaRespuesta Mapear(HistoriaClinica historiaClinica);
    }
}
