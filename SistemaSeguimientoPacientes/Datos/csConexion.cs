using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Datos
{
    internal class csConexion
    {
        private readonly string connectionString;

        public csConexion()
        {
            connectionString = "Server=.;Database=BDTratamientos;User Id=sa;Password=123456;";
        }

        public SqlConnection Conectar()
        {
            return new SqlConnection(connectionString);
        }
    }
}
