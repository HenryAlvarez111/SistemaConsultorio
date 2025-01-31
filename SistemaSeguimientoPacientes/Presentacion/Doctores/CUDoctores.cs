using SistemaSeguimientoPacientes.Datos;
using SistemaSeguimientoPacientes.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaSeguimientoPacientes.Presentacion.Doctores
{
    public partial class CUDoctores : UserControl
    {
        public CUDoctores()
        {
            InitializeComponent();
        }

        private void CUDoctores_Load(object sender, EventArgs e)
        {
            List<dtoDoctores> doctores = new clsDoctores().ObtenerTodos();
            dgvData.DataSource = doctores;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value.ToString();
                txtEspecialidad.Text = row.Cells["Especialidad"].Value.ToString();
                txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtoDoctores doctor = new dtoDoctores
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Especialidad = txtEspecialidad.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            bool resultado = new clsDoctores().Insertar(doctor);
            if (resultado)
            {
                MessageBox.Show("Doctor guardado exitosamente.");
                // Actualizar el DataGridView con la nueva lista de doctores
                List<dtoDoctores> doctores = new clsDoctores().ObtenerTodos();
                dgvData.DataSource = doctores;
            }
            else
            {
                MessageBox.Show("Error al guardar el doctor.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idDoctor = Convert.ToInt32(dgvData.SelectedRows[0].Cells["IdDoctor"].Value);

                dtoDoctores doctor = new dtoDoctores
                {
                    IdDoctor = idDoctor,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Especialidad = txtEspecialidad.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };

                bool resultado = new clsDoctores().Actualizar(doctor);
                if (resultado)
                {
                    MessageBox.Show("Doctor actualizado exitosamente.");
                    List<dtoDoctores> doctores = new clsDoctores().ObtenerTodos();
                    dgvData.DataSource = doctores;
                }
                else
                {
                    MessageBox.Show("Error al actualizar el doctor.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un doctor para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idDoctor = Convert.ToInt32(dgvData.SelectedRows[0].Cells["IdDoctor"].Value);

                bool resultado = new clsDoctores().Eliminar(idDoctor);
                if (resultado)
                {
                    MessageBox.Show("Doctor eliminado exitosamente.");
                    List<dtoDoctores> doctores = new clsDoctores().ObtenerTodos();
                    dgvData.DataSource = doctores;
                }
                else
                {
                    MessageBox.Show("Error al eliminar el doctor.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un doctor para eliminar.");
            }
        }
    }
}
