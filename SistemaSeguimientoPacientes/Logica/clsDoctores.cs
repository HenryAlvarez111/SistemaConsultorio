using SistemaSeguimientoPacientes.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Logica
{
    internal class clsDoctores
    {
        private csConexion conexion = new csConexion();

        // Método para insertar un nuevo doctor
        public bool Insertar(dtoDoctores doctor)
        {
            try
            {
                using (SqlConnection conn = conexion.Conectar())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarDoctor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", doctor.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", doctor.Apellido);
                    cmd.Parameters.AddWithValue("@Especialidad", doctor.Especialidad);
                    cmd.Parameters.AddWithValue("@Telefono", doctor.Telefono);
                    cmd.Parameters.AddWithValue("@Email", doctor.Email);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar doctor: " + ex.Message);
                return false;
            }
        }

        // Método para obtener todos los doctores
        public List<dtoDoctores> ObtenerTodos()
        {
            List<dtoDoctores> doctores = new List<dtoDoctores>();
            try
            {
                using (SqlConnection conn = conexion.Conectar())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerDoctores", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dtoDoctores doctor = new dtoDoctores
                        {
                            IdDoctor = Convert.ToInt32(reader["IdDoctor"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Especialidad = reader["Especialidad"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                        doctores.Add(doctor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener doctores: " + ex.Message);
            }
            return doctores;
        }

        // Método para actualizar un doctor existente
        public bool Actualizar(dtoDoctores doctor)
        {
            try
            {
                using (SqlConnection conn = conexion.Conectar())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarDoctor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDoctor", doctor.IdDoctor);
                    cmd.Parameters.AddWithValue("@Nombre", doctor.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", doctor.Apellido);
                    cmd.Parameters.AddWithValue("@Especialidad", doctor.Especialidad);
                    cmd.Parameters.AddWithValue("@Telefono", doctor.Telefono);
                    cmd.Parameters.AddWithValue("@Email", doctor.Email);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar doctor: " + ex.Message);
                return false;
            }
        }

        // Método para eliminar un doctor
        public bool Eliminar(int idDoctor)
        {
            try
            {
                using (SqlConnection conn = conexion.Conectar())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarDoctor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDoctor", idDoctor);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar doctor: " + ex.Message);
                return false;
            }
        }
    }
}
