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

namespace SistemaSeguimientoPacientes.Presentacion.Tratamientos
{
    public partial class CUTratamientos : UserControl
    {
        public CUTratamientos()
        {
            InitializeComponent();
        }
        private void CargarDatosTratamientos()
        {
            clsTratamientos clsTratamientos = new clsTratamientos();
            dgvData.DataSource = clsTratamientos.LeerTratamientos();
        }
        private void CUTratamientos_Load(object sender, EventArgs e)
        {
            CargarDatosTratamientos();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dtoTratamientos nuevoTratamiento = new dtoTratamientos
            {
                NombreTratamiento = txtTratamiento.Text,
                Descripcion = txtDescripcion.Text,
                DuracionDias = int.Parse(txtDuracionDias.Text),
                Costo = decimal.Parse(txtCosto.Text)
            };

            clsTratamientos clsTratamientos = new clsTratamientos();
            if (clsTratamientos.InsertarTratamiento(nuevoTratamiento))
            {
                MessageBox.Show("Tratamiento guardado correctamente.");
                CargarDatosTratamientos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el tratamiento.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idTratamiento = (int)dgvData.SelectedRows[0].Cells["IdTratamiento"].Value;

                dtoTratamientos tratamientoActualizado = new dtoTratamientos
                {
                    IdTratamiento = idTratamiento,
                    NombreTratamiento = txtTratamiento.Text,
                    Descripcion = txtDescripcion.Text,
                    DuracionDias = int.Parse(txtDuracionDias.Text),
                    Costo = decimal.Parse(txtCosto.Text)
                };

                clsTratamientos clsTratamientos = new clsTratamientos();
                if (clsTratamientos.ModificarTratamiento(tratamientoActualizado))
                {
                    MessageBox.Show("Tratamiento actualizado correctamente.");
                    CargarDatosTratamientos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el tratamiento.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un tratamiento para actualizar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                int idTratamiento = (int)dgvData.SelectedRows[0].Cells["IdTratamiento"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este tratamiento?", "Confirmar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    clsTratamientos clsTratamientos = new clsTratamientos();
                    if (clsTratamientos.EliminarTratamiento(idTratamiento))
                    {
                        MessageBox.Show("Tratamiento eliminado correctamente.");
                        CargarDatosTratamientos();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el tratamiento.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un tratamiento para eliminar.");
            }
        }
        private void LimpiarCampos()
        {
            txtTratamiento.Clear();
            txtDuracionDias.Clear();
            txtCosto.Clear();
            txtDescripcion.Clear();
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow filaSeleccionada = dgvData.Rows[e.RowIndex];

                txtTratamiento.Text = filaSeleccionada.Cells["NombreTratamiento"].Value.ToString();
                txtDuracionDias.Text = filaSeleccionada.Cells["DuracionDias"].Value.ToString();
                txtCosto.Text = filaSeleccionada.Cells["Costo"].Value.ToString();
                txtDescripcion.Text = filaSeleccionada.Cells["Descripcion"].Value.ToString();

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
