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

namespace SistemaSeguimientoPacientes.Presentacion.Pacientes
{
    public partial class CUPacientes : UserControl
    {
        public CUPacientes()
        {
            InitializeComponent();
        }
        private void CargarDatosPacientes()
        {
            clsPacientes ff = new clsPacientes();
            dgvData.DataSource = ff.LeerPacientes();
        }
        private void CUPacientes_Load(object sender, EventArgs e)
        {
            CargarDatosPacientes();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtoPacientes nuevoPaciente = new dtoPacientes
            {
                Nombre = txtNombres.Text,
                Apellido = txtApellidos.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Telefono = txtTelefono.Text,
                Email = txtMail.Text,
                Direccion = txtDireccion.Text
            };

            clsPacientes clsPacientes = new clsPacientes();
            if (clsPacientes.InsertarPaciente(nuevoPaciente))
            {
                MessageBox.Show("Paciente guardado correctamente.");
                CargarDatosPacientes();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el paciente.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idPaciente = (int)dgvData.SelectedRows[0].Cells["IdPaciente"].Value;

                dtoPacientes pacienteActualizado = new dtoPacientes
                {
                    IdPaciente = idPaciente,
                    Nombre = txtNombres.Text,
                    Apellido = txtApellidos.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Telefono = txtTelefono.Text,
                    Email = txtMail.Text,
                    Direccion = txtDireccion.Text
                };

                clsPacientes clsPacientes = new clsPacientes();
                if (clsPacientes.ModificarPaciente(pacienteActualizado))
                {
                    MessageBox.Show("Paciente actualizado correctamente.");
                    CargarDatosPacientes();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el paciente.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idPaciente = (int)dgvData.SelectedRows[0].Cells["IdPaciente"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este paciente?", "Confirmar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    clsPacientes clsPacientes = new clsPacientes();
                    if (clsPacientes.EliminarPaciente(idPaciente))
                    {
                        MessageBox.Show("Paciente eliminado correctamente.");
                        CargarDatosPacientes();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el paciente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para eliminar.");
            }
        }
        private void LimpiarCampos()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtMail.Clear();
            txtDireccion.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvData.Rows[e.RowIndex];

                txtNombres.Text = filaSeleccionada.Cells["Nombre"].Value.ToString();
                txtApellidos.Text = filaSeleccionada.Cells["Apellido"].Value.ToString();
                dtpFechaNacimiento.Value = Convert.ToDateTime(filaSeleccionada.Cells["FechaNacimiento"].Value);
                txtTelefono.Text = filaSeleccionada.Cells["Telefono"].Value.ToString();
                txtMail.Text = filaSeleccionada.Cells["Email"].Value.ToString();
                txtDireccion.Text = filaSeleccionada.Cells["Direccion"].Value.ToString();

                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }
    }
}
