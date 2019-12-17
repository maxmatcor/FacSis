using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteCategorias : Form
    {
        public FrmReporteCategorias()
        {
            InitializeComponent();
        }

        private void FrmReporteCategorias_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsPrincipal.spmostrar_categoria' table. You can move, or remove it, as needed.
            try
            {
                this.spmostrar_categoriaTableAdapter.Fill(this.dsPrincipal.spmostrar_categoria);
                var configuracionPagina = new PageSettings();
                configuracionPagina.Margins.Top = 0;
                configuracionPagina.Margins.Right = 0;
                configuracionPagina.Margins.Bottom = 0;
                configuracionPagina.Margins.Left = 0;

                reportViewer1.SetPageSettings(configuracionPagina);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                this.reportViewer1.RefreshReport();
                Utilidades.MensajeError(ex.Message);
            }

        }
    }
}
