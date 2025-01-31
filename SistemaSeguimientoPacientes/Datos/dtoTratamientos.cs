using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Datos
{
    internal class dtoTratamientos
    {
        public int IdTratamiento { get; set; }
        public string NombreTratamiento { get; set; }
        public string Descripcion { get; set; }
        public int DuracionDias { get; set; }
        public decimal Costo { get; set; }
    }
}
