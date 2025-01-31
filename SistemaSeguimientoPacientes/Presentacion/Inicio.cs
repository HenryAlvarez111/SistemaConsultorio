using SistemaSeguimientoPacientes.Presentacion.Consultas;
using SistemaSeguimientoPacientes.Presentacion.Pacientes;
using SistemaSeguimientoPacientes.Presentacion.Tratamientos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaSeguimientoPacientes
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            CUConsultas frmPrueba = new CUConsultas();
            pnlContenedor.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(frmPrueba);
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            CUPacientes frmPrueba = new CUPacientes();
            pnlContenedor.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(frmPrueba);
        }

        private void btnTratamientos_Click(object sender, EventArgs e)
        {
            CUTratamientos frmPrueba = new CUTratamientos();
            pnlContenedor.Controls.Clear();
            frmPrueba.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(frmPrueba);
        }
    }
}
