using SistemaSeguimientoPacientes.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Logica
{
    internal class clsTratamientos
    {
        private csConexion conexion = new csConexion();

        public List<dtoTratamientos> LeerTratamientos()
        {
            List<dtoTratamientos> listaTratamientos = new List<dtoTratamientos>();
            string consulta = "SELECT * FROM Tratamientos";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaTratamientos.Add(new dtoTratamientos
                    {
                        IdTratamiento = (int)reader["IdTratamiento"],
                        NombreTratamiento = reader["NombreTratamiento"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        DuracionDias = (int)reader["DuracionDias"],
                        Costo = (decimal)reader["Costo"]
                    });
                }
            }
            return listaTratamientos;
        }

        public bool InsertarTratamiento(dtoTratamientos tratamiento)
        {
            string consulta = "INSERT INTO Tratamientos (NombreTratamiento, Descripcion, DuracionDias, Costo) VALUES (" +
                              $"'{tratamiento.NombreTratamiento}', '{tratamiento.Descripcion}', {tratamiento.DuracionDias}, {tratamiento.Costo})";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ModificarTratamiento(dtoTratamientos tratamiento)
        {
            string consulta = "UPDATE Tratamientos SET " +
                              $"NombreTratamiento = '{tratamiento.NombreTratamiento}', " +
                              $"Descripcion = '{tratamiento.Descripcion}', " +
                              $"DuracionDias = {tratamiento.DuracionDias}, " +
                              $"Costo = {tratamiento.Costo.ToString(System.Globalization.CultureInfo.InvariantCulture)} " +
                              $"WHERE IdTratamiento = {tratamiento.IdTratamiento}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool EliminarTratamiento(int idTratamiento)
        {
            string consulta = $"DELETE FROM Tratamientos WHERE IdTratamiento = {idTratamiento}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
