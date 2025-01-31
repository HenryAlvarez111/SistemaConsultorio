using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Datos
{
    internal class dtoConsultas
    {
        public int IdConsulta { get; set; }
        public int IdPaciente { get; set; }
        public int? IdTratamiento { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string Observaciones { get; set; }
    }
}
