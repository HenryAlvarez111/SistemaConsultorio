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

            
            cmbProveedor.DataSource = clsPacientes.LeerPacientes();
            cmbProveedor.DisplayMember = "Nombre"; 
            cmbProveedor.ValueMember = "IdPaciente";

            cmbProducto.DataSource = clsTratamientos.LeerTratamientos();
            cmbProducto.DisplayMember = "NombreTratamiento"; 
            cmbProducto.ValueMember = "IdTratamiento"; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtoConsultas nuevaConsulta = new dtoConsultas
            {
                IdPaciente = (int)cmbProveedor.SelectedValue,
                IdTratamiento = (int)cmbProducto.SelectedValue,
                FechaConsulta = dtpFecha.Value,
                Observaciones = txtCantidad.Text
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
                    IdPaciente = (int)cmbProveedor.SelectedValue,
                    IdTratamiento = (int)cmbProducto.SelectedValue,
                    FechaConsulta = dtpFecha.Value,
                    Observaciones = txtCantidad.Text
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
            cmbProveedor.SelectedIndex = -1; 
            cmbProducto.SelectedIndex = -1;  
            dtpFecha.Value = DateTime.Now;   
            txtCantidad.Clear();             
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;

                cmbProveedor.SelectedValue = dgvData.SelectedRows[0].Cells["IdPaciente"].Value;
                cmbProducto.SelectedValue = dgvData.SelectedRows[0].Cells["IdTratamiento"].Value;
                dtpFecha.Value = (DateTime)dgvData.SelectedRows[0].Cells["FechaConsulta"].Value;
                txtCantidad.Text = dgvData.SelectedRows[0].Cells["Observaciones"].Value.ToString();
            }
            else
            {
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }
    }
}
