using SistemaSeguimientoPacientes.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Logica
{
    internal class clsConsultas
    {
        private csConexion conexion = new csConexion();

        public List<dtoConsultas> LeerConsultas()
        {
            List<dtoConsultas> listaConsultas = new List<dtoConsultas>();
            string consulta = "SELECT * FROM Consultas";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaConsultas.Add(new dtoConsultas
                    {
                        IdConsulta = (int)reader["IdConsulta"],
                        IdPaciente = (int)reader["IdPaciente"],
                        IdTratamiento = reader["IdTratamiento"] as int?,
                        FechaConsulta = (DateTime)reader["FechaConsulta"],
                        Observaciones = reader["Observaciones"].ToString()
                    });
                }
            }
            return listaConsultas;
        }

        public bool InsertarConsulta(dtoConsultas consultaObj)
        {
            string consulta = "INSERT INTO Consultas (IdPaciente, IdTratamiento, FechaConsulta, Observaciones) VALUES (" +
                              $"{consultaObj.IdPaciente}, {consultaObj.IdTratamiento}, '{consultaObj.FechaConsulta:yyyy-MM-dd HH:mm:ss}', " +
                              $"'{consultaObj.Observaciones}')";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ModificarConsulta(dtoConsultas consultaObj)
        {
            string consulta = "UPDATE Consultas SET " +
                              $"IdPaciente = {consultaObj.IdPaciente}, " +
                              $"IdTratamiento = {consultaObj.IdTratamiento}, " +
                              $"FechaConsulta = '{consultaObj.FechaConsulta:yyyy-MM-dd HH:mm:ss}', " +
                              $"Observaciones = '{consultaObj.Observaciones}' " +
                              $"WHERE IdConsulta = {consultaObj.IdConsulta}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarConsulta(int idConsulta)
        {
            string consulta = $"DELETE FROM Consultas WHERE IdConsulta = {idConsulta}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
