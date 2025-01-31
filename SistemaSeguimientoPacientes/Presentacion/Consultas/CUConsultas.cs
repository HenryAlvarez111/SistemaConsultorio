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

namespace SistemaSeguimientoPacientes.Presentacion.Consultas
{
    public partial class CUConsultas : UserControl
    {
        public CUConsultas()
        {
            InitializeComponent();
        }
        private void CargarDatos()
        {
            clsConsultas consultas = new clsConsultas();
            List<dtoConsultas> lista = consultas.LeerConsultas();

            dgvData.DataSource = lista;
        }
        private void CUConsultas_Load(object sender, EventArgs e)
        {
            CargarDatos(); CargarCombos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

        }
        private void CargarCombos()
        {
            clsPacientes clsPacientes = new clsPacientes();
            clsTratamientos clsTratamientos = new clsTratamientos();
            clsDoctores clsDoctores = new clsDoctores();

            cmbTratamiento.DataSource = clsPacientes.LeerPacientes();
            cmbTratamiento.DisplayMember = "Nombre";
            cmbTratamiento.ValueMember = "IdPaciente";

            cmbPaciente.DataSource = clsTratamientos.LeerTratamientos();
            cmbPaciente.DisplayMember = "NombreTratamiento";
            cmbPaciente.ValueMember = "IdTratamiento";

            cmbDoctor.DataSource = clsDoctores.ObtenerTodos();
            cmbDoctor.DisplayMember = "Nombre"; 
            cmbDoctor.ValueMember = "IdDoctor";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtoConsultas nuevaConsulta = new dtoConsultas
            {
                IdPaciente = (int)cmbTratamiento.SelectedValue,
                IdTratamiento = (int)cmbPaciente.SelectedValue,
                FechaConsulta = dtpFecha.Value,
                Observaciones = txtObservacion.Text,
                IdDoctor = (int)cmbDoctor.SelectedValue 
            };
            clsConsultas consultas = new clsConsultas();
            if (consultas.InsertarConsulta(nuevaConsulta))
            {
                MessageBox.Show("Registro guardado correctamente.");
                CargarDatos(); LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el registro.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idConsulta = (int)dgvData.SelectedRows[0].Cells["IdConsulta"].Value;
                dtoConsultas consultaActualizada = new dtoConsultas
                {
                    IdConsulta = idConsulta,
                    IdPaciente = (int)cmbTratamiento.SelectedValue,
                    IdTratamiento = (int)cmbPaciente.SelectedValue,
                    FechaConsulta = dtpFecha.Value,
                    Observaciones = txtObservacion.Text,
                    IdDoctor = (int)cmbDoctor.SelectedValue
                };
                clsConsultas consultas = new clsConsultas();
                if (consultas.ModificarConsulta(consultaActualizada))
                {
                    MessageBox.Show("Registro actualizado correctamente.");
                    CargarDatos(); LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el registro.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idConsulta = (int)dgvData.SelectedRows[0].Cells["IdConsulta"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    clsConsultas consultas = new clsConsultas();
                    if (consultas.EliminarConsulta(idConsulta))
                    {
                        MessageBox.Show("Registro eliminado correctamente.");
                        CargarDatos(); LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el registro.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para eliminar.");
            }
        }
        private void LimpiarCampos()
        {
            cmbTratamiento.SelectedIndex = -1; 
            cmbPaciente.SelectedIndex = -1; cmbDoctor.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;   
            txtObservacion.Clear();             
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
                cmbTratamiento.SelectedValue = dgvData.SelectedRows[0].Cells["IdPaciente"].Value;
                cmbPaciente.SelectedValue = dgvData.SelectedRows[0].Cells["IdTratamiento"].Value;
                dtpFecha.Value = (DateTime)dgvData.SelectedRows[0].Cells["FechaConsulta"].Value;
                txtObservacion.Text = dgvData.SelectedRows[0].Cells["Observaciones"].Value.ToString();
                cmbDoctor.SelectedValue = dgvData.SelectedRows[0].Cells["IdDoctor"].Value; // Asignar el ID del doctor
            }
            else
            {
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }
    }
}
