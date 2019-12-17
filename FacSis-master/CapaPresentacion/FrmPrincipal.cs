using System;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmPrincipal : Form
    {
        
        public string estado;
        public string fechaInicio;
        public string fechaFin;
        public string idTrabajador;
        public string nombre;
        public string apellido;
        public string acceso;



        private void indicadores()
        {//mas de 60 dias
            
            estado = "Adeuda";
            var Fecha = DateTime.Today.AddYears(-50);
            fechaInicio = Fecha.ToString("yyyy-MM-dd");
            var Fecha2 = DateTime.Today.AddDays(-61);
            fechaFin = Fecha2.ToString("yyyy-MM-dd");
            datalistado.DataSource = Ningreso.BuscarFechasMas30(fechaInicio, fechaFin, estado);
            if (datalistado.Rows.Count.Equals(0))
            {
                lblAlto.Text = "0";
            }
            else {
                lblAlto.Text = Convert.ToString((datalistado.Rows.Count) - 1);
            }
            //mas de 15 dias
            estado = "Adeuda";
            var Fecha3 = DateTime.Today.AddDays(-30);
            fechaInicio = Fecha3.ToString("yyyy-MM-dd");
            var Fecha4 = DateTime.Today.AddDays(-15);
            fechaFin = Fecha4.ToString("yyyy-MM-dd");
            datalistado.DataSource = Ningreso.BuscarFechasMas30(fechaInicio, fechaFin, estado);
            if (datalistado.Rows.Count.Equals(0))
            {
                lblBajo.Text = "0";
            }
            else
            {
                lblBajo.Text = Convert.ToString((datalistado.Rows.Count) - 1);
            }
            //mas de 30 dias
            estado = "Adeuda";
            var Fecha5 = DateTime.Today.AddDays(-60);
            fechaInicio = Fecha5.ToString("yyyy-MM-dd");
            var Fecha6 = DateTime.Today.AddDays(-31);
            fechaFin = Fecha6.ToString("yyyy-MM-dd");
            datalistado.DataSource = Ningreso.BuscarFechasMas30(fechaInicio, fechaFin, estado);
            if (datalistado.Rows.Count.Equals(0))
            {
                lblMedio.Text = "0";
            }
            else
            {
                lblMedio.Text = Convert.ToString((datalistado.Rows.Count) - 1);
            }
        }

        public FrmPrincipal()
        {
            InitializeComponent();
        }


        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            indicadores();
            lblidUsuario.Text = idTrabajador;
        }


        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void abrirFormHijo(object formhijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }


        private void BtnClientes_Click(object sender, EventArgs e)
        {

            abrirFormHijo(new FrmCliente());
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);
            FrmIngreso fh = new FrmIngreso();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.txtIdusuario.Text = lblidUsuario.Text;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
            //abrirFormHijo(new FrmIngreso());

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {

            abrirFormHijo(new FrmTrabajador());
        }

        private void HoraFecha_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
            indicadores();
        }



        private void BtnCerrar2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimizar2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}