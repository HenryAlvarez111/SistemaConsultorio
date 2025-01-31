using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;
using SistemaSeguimientoPacientes.Logica;
using SistemaSeguimientoPacientes.Datos;
using SistemaSeguimientoPacientes.Datos.DataSetExtractorTableAdapters;

namespace SistemaSeguimientoPacientes.Presentacion.Reporte
{
    public partial class CUReporte : UserControl
    {
        public CUReporte()
        {
            InitializeComponent();
        }

        private void btnMostrarReporte_Click(object sender, EventArgs e)
        {
            if (cmbDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un doctor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int doctorID = (int)cmbDoctor.SelectedValue;
            string nombreDoctor = cmbDoctor.Text;
            MostrarReportePorDoctor(doctorID, nombreDoctor);

        }
        private void CargarCombos()
        {

            clsDoctores clsDoctores = new clsDoctores();

            cmbDoctor.DataSource = clsDoctores.ObtenerTodos();
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
            cmbDoctor.FormattingEnabled = true;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CUReporte_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }
        private void MostrarReportePorDoctor(int idDoctor, string NombreDoctor)
        {
            try
            {

                sp_ReporteCitasPorDoctorTableAdapter adapter = new sp_ReporteCitasPorDoctorTableAdapter();


                DataTable dt = adapter.GetData(idDoctor);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay citas disponibles para el doctor seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportViewer1.Clear();
                    return;
                }

                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSetInterno", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter paramDoctor = new ReportParameter("DoctorNombre", NombreDoctor);
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { paramDoctor });



                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDoctor_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is dtoDoctores doctor)
            {
                e.Value = $"{doctor.Nombre} {doctor.Apellido}";
            }
        }
    }
}
