using SistemaSeguimientoPacientes.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSeguimientoPacientes.Logica
{
    internal class clsPacientes
    {
        private csConexion conexion = new csConexion();

        public List<dtoPacientes> LeerPacientes()
        {
            List<dtoPacientes> listaPacientes = new List<dtoPacientes>();
            string consulta = "SELECT * FROM Pacientes";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaPacientes.Add(new dtoPacientes
                    {
                        IdPaciente = (int)reader["IdPaciente"],
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                        Telefono = reader["Telefono"].ToString(),
                        Email = reader["Email"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            return listaPacientes;
        }

        public bool InsertarPaciente(dtoPacientes paciente)
        {
            string consulta = "INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion) VALUES (" +
                              $"'{paciente.Nombre}', '{paciente.Apellido}', '{paciente.FechaNacimiento:yyyy-MM-dd}', " +
                              $"'{paciente.Telefono}', '{paciente.Email}', '{paciente.Direccion}')";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ModificarPaciente(dtoPacientes paciente)
        {
            string consulta = "UPDATE Pacientes SET " +
                              $"Nombre = '{paciente.Nombre}', " +
                              $"Apellido = '{paciente.Apellido}', " +
                              $"FechaNacimiento = '{paciente.FechaNacimiento:yyyy-MM-dd}', " +
                              $"Telefono = '{paciente.Telefono}', " +
                              $"Email = '{paciente.Email}', " +
                              $"Direccion = '{paciente.Direccion}' " +
                              $"WHERE IdPaciente = {paciente.IdPaciente}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool EliminarPaciente(int idPaciente)
        {
            string consulta = $"DELETE FROM Pacientes WHERE IdPaciente = {idPaciente}";

            using (SqlConnection con = conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
