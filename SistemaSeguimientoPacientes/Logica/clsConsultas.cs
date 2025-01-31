using SistemaSeguimientoPacientes.Datos;
using System;
using System.Collections.Generic;
using System.Data;
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
            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerConsultas", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
                        Observaciones = reader["Observaciones"].ToString(),
                        IdDoctor = reader["IdDoctor"] as int? // Añadir el ID del doctor
                    });
                }
            }
            return listaConsultas;
        }
        public bool InsertarConsulta(dtoConsultas consultaObj)
        {
            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarConsulta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPaciente", consultaObj.IdPaciente);
                cmd.Parameters.AddWithValue("@IdTratamiento", consultaObj.IdTratamiento);
                cmd.Parameters.AddWithValue("@FechaConsulta", consultaObj.FechaConsulta);
                cmd.Parameters.AddWithValue("@Observaciones", consultaObj.Observaciones);
                cmd.Parameters.AddWithValue("@IdDoctor", consultaObj.IdDoctor); // Añadir el ID del doctor

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool ModificarConsulta(dtoConsultas consultaObj)
        {
            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarConsulta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdConsulta", consultaObj.IdConsulta);
                cmd.Parameters.AddWithValue("@IdPaciente", consultaObj.IdPaciente);
                cmd.Parameters.AddWithValue("@IdTratamiento", consultaObj.IdTratamiento);
                cmd.Parameters.AddWithValue("@FechaConsulta", consultaObj.FechaConsulta);
                cmd.Parameters.AddWithValue("@Observaciones", consultaObj.Observaciones);
                cmd.Parameters.AddWithValue("@IdDoctor", consultaObj.IdDoctor); // Añadir el ID del doctor

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
