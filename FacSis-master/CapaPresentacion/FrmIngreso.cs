using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace CapaPresentacion
{
    public partial class FrmIngreso : Form
    {   public String fechaFin;
        public string fechaInicio;
        public string estado;
        public string factura;
        public string serie;
        public string idCliente;
        public string idTrabajador;
        public string busquedaTabFactura = "0";
        private bool isNuevo;
        private DataTable dtDetalles;
        private decimal totalPagado = 0;

        //Obtener instancia del formulario
        #region Instancia
        private static FrmIngreso _instancia;

        public static FrmIngreso GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FrmIngreso();
            }

            return _instancia;
        }
        #endregion

        public FrmIngreso()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(btnBuscarProveedor, "Haga clic para seleccionar el Cliente");
            ttMensaje.SetToolTip(cbTipoComprobante, "Seleccione el tipo de comprobante");
            ttMensaje.SetToolTip(txtSerie, "Indique la serie de Facturas");
            ttMensaje.SetToolTip(txtCorrelativo, "Indique el Numero de Factura");
            ttMensaje.SetToolTip(btnAgregar, "Haga clic para agregar la Factura seleccionada");
            ttMensaje.SetToolTip(btnQuitar, "Haga clic para eliminar la Factura seleccionada");

            txtProveedor.ReadOnly = true;
        }


        public void SetProveedor(string idCliente, string nombreApellido)
        {
            txtIdProveedor.Text = idCliente;
            txtProveedor.Text = nombreApellido;
            txtProveedor2.Text = nombreApellido;
        }


        private void FrmIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void FrmIngreso_Load(object sender, System.EventArgs e)
        {   
            Mostrar();
            HabilitarBotones();
            HabilitarControles(false);
            CrearTablaDetalle();
        }

        private void btnBuscarProveedor_Click(object sender, System.EventArgs e)
        {
            MostrarClientes();
            dataCliente.Visible = true;
            busquedaTabFactura = "1";
            area.SelectedIndex = 1;

        }

        //Limpiar contoles
        private void Limpiar()
        {
            cbTipoComprobante.Text = "";
            btnBusquedaClientes2.Enabled = false;
            btnBusquedaPorCliente.Enabled = false;
            lblFactura.Visible = false;
            txtTomo.Enabled = false;
            txtProveedor2.Enabled = false;
            btnBusquedaPorCliente.Enabled = false;
            busquedaTabFactura = "0";
            txtIdIngreso.Clear();
            txtIdProveedor.Clear();
            txtProveedor.Clear();
            txtProveedor2.Clear();
            txtTomo.Clear();
            txtSerie.Clear();
            txtCorrelativo.Clear();
            lblTotalPagado.Text = "0.0";
            txtArticulo.Clear();
            CrearTablaDetalle();
            totalPagado = 0;
        }

        //limpiar detalles
        private void limpiarDetalle()
        {
            cmbTipoBusqueda.SelectedIndex = -1;
            txtStock.Clear();
            txtArticulo.Clear();
            txtPrecioVenta.Clear();
        }


        //Habilitar los controles del formulario
        private void HabilitarControles(bool valor)
        {
            txtIdIngreso.ReadOnly = !valor;
            txtSerie.ReadOnly = !valor;
            txtCorrelativo.ReadOnly = !valor;
            dtFechaIngresoAlmacen.Enabled = valor;
            cbTipoComprobante.Enabled = valor;
            txtPrecioVenta.ReadOnly = !valor;

            btnBuscarProveedor.Enabled = valor;
            btnAgregar.Enabled = valor;
            btnQuitar.Enabled = valor;
        }


        //Habilitar los botones
        private void HabilitarBotones()
        {
            if (isNuevo)
            {
                HabilitarControles(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;

            }
            else
            {
                HabilitarControles(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        //Ocultar Columnas
        private void OcultarColumnas()
        {
            dataListado.Columns[0].Visible = false;
            dataListado.Columns[1].Visible = false;
            dataListado.Columns[2].HeaderText = "Usuario";
            dataListado.Columns[3].HeaderText = "Cliente";
            dataListado.Columns[4].HeaderText = "Fecha";
            dataListado.Columns[5].HeaderText = "Tipo De Comprobante";
            dataListado.Columns[6].HeaderText = "Num. De Serie";
            dataListado.Columns[7].HeaderText = "Num. De Factura";
            dataListado.Columns[8].HeaderText = "Estado";
            dataListado.Columns[9].HeaderText = "Total";
            // Le da el formato a las columnas para que solo muestren 2 decimales
            dataListado.Columns[9].DefaultCellStyle.Format = "N2";
        }
        //Metodo Mostrar
        private void Mostrar()
        {
            dataListado.DataSource = Ningreso.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }

        //Metodo Buscar Por Fechas
        private void BuscarFechas()
        {
            
            dataListado.DataSource = Ningreso.BuscarFechas(dtFechaInicio.Value.ToString("yyyy-MM-dd"), dtFechaFin.Value.ToString("yyyy-MM-dd"));
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }
        //Metodo Buscar Por Estados
        private void BuscarEstados()
        {
            dataListado.DataSource = Ningreso.BuscarEstados(estado);
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }
        //Metodo Buscar Por Cantidad de dias de deudas
        
        private void BuscarFechasMas30()
        {

            dataListado.DataSource = Ningreso.BuscarFechasMas30(fechaInicio, fechaFin, estado);
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }

        //Metodo Buscar Por Cliente
        private void BuscarClientes()
        {
            dataListado.DataSource = Ningreso.BuscarClientes(idCliente);
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }


        //Metodo Buscar Por Cliente
        private void BuscarFacturas()
        {
            dataListado.DataSource = Ningreso.BuscarFacturas(serie, factura);
            OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
            CalculoDeuda();
        }


        //Metodo MostrarDetalles
        private void MostrarDetalles()
        {
            dataListadoDetalle.DataSource = Ningreso.MostrarDetalles(txtIdIngreso.Text);
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            BuscarFechas();
            CalculoDeuda();
        }

        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            try
            {
                DialogResult opcion =
                    MessageBox.Show("¿Realmente desea marcar como pagadas la/las deudas seleccionadas?",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    int IdIngreso = 0;
                    string respuesta = "";

                    foreach (DataGridViewRow fila in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            IdIngreso = Convert.ToInt32(fila.Cells[1].Value);
                            respuesta = Ningreso.Anular(IdIngreso);

                            if (respuesta.Equals("Ok"))
                            {
                                Utilidades.MensajeOK("El ingreso se marco como pagado correctamente.");
                            }
                            else
                            {
                                Utilidades.MensajeError(respuesta);
                            }
                        }
                    }
                    Mostrar();
                    chkAnular.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnular.Checked)
            {
                dataListado.Columns[0].Visible = true;
                btnAnular.Enabled = true;
            }
            else
            {
                dataListado.Columns[0].Visible = false;
                btnAnular.Enabled = false;
            }
        }

        private void CalculoDeuda()
        {
            double deuda = 0;
            foreach (DataGridViewRow row in dataListado.Rows)
            {
                deuda += Convert.ToDouble(row.Cells[9].Value);

            }
            lblDeuda.Text = Convert.ToString(deuda);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Anular"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar =
                (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Anular"];

                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void CrearTablaDetalle()
        {
            dtDetalles = new DataTable("Detalles");
            dtDetalles.Columns.Add("articulo", Type.GetType("System.String"));
            dtDetalles.Columns.Add("precio_venta", Type.GetType("System.Decimal"));
            dtDetalles.Columns.Add("Cantidad", Type.GetType("System.Int32"));
            dtDetalles.Columns.Add("subtotal", Type.GetType("System.Decimal"));

            dataListadoDetalle.DataSource = dtDetalles;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            isNuevo = true;
            Limpiar();
            HabilitarBotones();
            HabilitarControles(true);
            limpiarDetalle();
            txtSerie.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            isNuevo = false;
            HabilitarControles(false);
            HabilitarBotones();
            Limpiar();
            limpiarDetalle();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";

                if (txtIdProveedor.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtProveedor, "Seleccione un Cliente");
                }
                else if (txtSerie.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtSerie, "Ingrese el numero de tomo");
                }
                else if (txtCorrelativo.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtCorrelativo, "Ingrese el numero de comprobante");
                }
                else
                {

                    if (isNuevo)
                    {
                        respuesta = Ningreso.Insertar(Convert.ToInt32(txtIdusuario.Text), Convert.ToInt32(txtIdProveedor.Text), dtFechaIngresoAlmacen.Value,
                            cbTipoComprobante.Text, txtSerie.Text, txtCorrelativo.Text, "Adeuda", dtDetalles
                            );
                    }

                    if (respuesta.Equals("Ok"))
                    {
                        if (isNuevo)
                        {
                            Utilidades.MensajeOK("El comprobante se agregó correctamente");
                        }
                    }
                    else
                    {
                        Utilidades.MensajeError(respuesta);
                    }

                    isNuevo = false;
                    HabilitarBotones();
                    Limpiar();
                    limpiarDetalle();
                    Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPrecioVenta.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese el valor del articulo");
                }
                else if (txtArticulo.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtArticulo, "Ingrese una descripcion del articulo");
                }
                else if (txtStock.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtStock, "Ingrese la cantidad de articulos");
                }
                else
                {
                    bool registrar = true;

                    

                    if (registrar)
                    {
                        var subTotal = Convert.ToDecimal(txtStock.Text) * Convert.ToDecimal(txtPrecioVenta.Text);
                        totalPagado = totalPagado + subTotal;
                        lblTotalPagado.Text = totalPagado.ToString("#0.00#");

                        //Agregar detalle a datalistadodetalle
                        var fila = dtDetalles.NewRow();
                        fila["articulo"] = txtArticulo.Text;
                        fila["Precio_venta"] = Convert.ToDecimal(txtPrecioVenta.Text);
                        fila["Cantidad"] = Convert.ToInt32(txtStock.Text);
                        fila["subtotal"] = subTotal;
                        lblTotalPagado.Text = Convert.ToString(totalPagado);
                        dtDetalles.Rows.Add(fila);
                        limpiarDetalle();


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = dataListadoDetalle.CurrentCell.RowIndex;
                DataRow fila = dtDetalles.Rows[indiceFila];

                //Disminuir Total Pagado
                totalPagado = totalPagado - Convert.ToDecimal(fila["subtotal"].ToString());
                lblTotalPagado.Text = totalPagado.ToString("#0.00#");
                //Remover fila
                dtDetalles.Rows.Remove(fila);
            }
            catch (Exception)
            {
                Utilidades.MensajeError("No hay fila para remover");
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {   //limita a dos decimales
            decimal varTotal = Convert.ToDecimal(dataListado.CurrentRow.Cells["Total"].Value);
            varTotal = Math.Round(varTotal, 2);

            //carga datos para ver
            txtIdIngreso.Text = Convert.ToString(dataListado.CurrentRow.Cells["idingreso"].Value);
            txtProveedor.Text = Convert.ToString(dataListado.CurrentRow.Cells["Cliente"].Value);
            dtFechaIngresoAlmacen.Value = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha"].Value);
            cbTipoComprobante.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            txtSerie.Text = Convert.ToString(dataListado.CurrentRow.Cells["serie"].Value);
            txtCorrelativo.Text = Convert.ToString(dataListado.CurrentRow.Cells["correlativo"].Value);
            lblTotalPagado.Text = Convert.ToString(varTotal);

            MostrarDetalles();
            // Le da el formato a las columnas para que solo muestren 2 decimales
            dataListadoDetalle.Columns[1].DefaultCellStyle.Format = "N2";
            dataListadoDetalle.Columns[3].DefaultCellStyle.Format = "N2";
            area.SelectTab(0);
        }


        private void BtnPorDeudas_Click(object sender, EventArgs e)
        {
            estado = "Adeuda";
            BuscarEstados();
        }

        private void BtnPorPagados_Click(object sender, EventArgs e)
        {
            estado = "PAGADO";
            BuscarEstados();
        }

        private void BtnMas60_Click(object sender, EventArgs e)
        {
            estado = "Adeuda";
            var Fecha = DateTime.Today.AddYears(-50);
            fechaInicio =  Fecha.ToString("yyyy-MM-dd");
            var Fecha2 = DateTime.Today.AddDays(-60);
            fechaFin = Fecha2.ToString("yyyy-MM-dd");
            BuscarFechasMas30();
        }

        private void BtnMas15_Click(object sender, EventArgs e)
        {

            estado = "Adeuda";
            var Fecha = DateTime.Today.AddDays(-30);
            fechaInicio = Fecha.ToString("yyyy-MM-dd");
            var Fecha2 = DateTime.Today.AddDays(-15);
            fechaFin = Fecha2.ToString("yyyy-MM-dd");
            BuscarFechasMas30();

        }

        private void BtnMas30_Click(object sender, EventArgs e)
        {
            estado = "Adeuda";
            var Fecha = DateTime.Today.AddDays(-60);
            fechaInicio = Fecha.ToString("yyyy-MM-dd");
            var Fecha2 = DateTime.Today.AddDays(-30);
            fechaFin = Fecha2.ToString("yyyy-MM-dd");
            
            BuscarFechasMas30();
            
        }

        private void BtnBusquedaClientes_Click(object sender, EventArgs e)
        {
            MostrarClientes();
            dataCliente.Visible = true;
        }



        private void MostrarClientes()
        {
            dataCliente.DataSource = Ncliente.Mostrar();
            OcultarColumnas();
        }
        private void BtnBusquedaPorCliente_Click(object sender, EventArgs e)
        {
            
            if (cmbTipoBusqueda.Text.Equals("Factura"))
            {
                if (txtTomo.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtTomo, "Ingrese el numero de tomo a buscar");
                }
                else if (txtProveedor2.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(txtProveedor2, "Ingrese el numero de comprobante a buscar");
                }else
                { 
                factura = txtProveedor2.Text;
                serie = txtTomo.Text;
                BuscarFacturas();
                }
            }
            else if (cmbTipoBusqueda.Text.Equals("Cliente"))
            {
                if (txtProveedor2.Text == string.Empty)
                {
                    Utilidades.MensajeError("Falta ingresar algunos datos.");
                    errorIcono.SetError(btnBusquedaClientes2, "Ingrese selleccione el cliente");
                }
                else
                {
                    idCliente = txtIdProveedor.Text;
                    BuscarClientes();
                }
            }
            
            
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            isNuevo = false;
            HabilitarControles(false);
            HabilitarBotones();
            Limpiar();
            limpiarDetalle();
            area.SelectedIndex = 0;
        }

        private void BtnConsultas_Click(object sender, EventArgs e)
        {
            isNuevo = false;
            HabilitarControles(false);
            HabilitarBotones();
            Limpiar();
            limpiarDetalle();
            area.SelectedIndex = 1;
            
        }

        private void DataCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdProveedor.Text = Convert.ToString(dataCliente.CurrentRow.Cells["idcliente"].Value);
            txtProveedor.Text = Convert.ToString(dataCliente.CurrentRow.Cells["nombre"].Value) + " " + Convert.ToString(dataCliente.CurrentRow.Cells["apellidos"].Value);
            txtProveedor2.Text = txtProveedor.Text;
            dataCliente.Visible = false;

            if (busquedaTabFactura.Equals("1"))
            {

                busquedaTabFactura = "0";
                area.SelectedIndex = 0;
            }
        }

        private void CmbTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoBusqueda.Text.Equals("Factura"))
            {
                Limpiar();
                lblFactura.Visible = true;
                txtTomo.Enabled = true;
                txtProveedor2.Enabled = true;
                btnBusquedaPorCliente.Enabled = true;
            }
            else if (cmbTipoBusqueda.Text.Equals("Cliente"))
            {
                Limpiar();
                btnBusquedaClientes2.Enabled = true;
                btnBusquedaPorCliente.Enabled = true;
            }
        }

        private void BtnImprimir_Click_1(object sender, EventArgs e)
        {
            SetupThePrinting();
            printDocument1.Print();

        }
        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDocument1.DocumentName = this.Text;
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.DefaultPageSettings.Margins = new Margins(15, 15, 15, 15);

            MyDataGridViewPrinter = new DataGridViewPrinter(dataListado,
            printDocument1, true, true, this.Text + DateTime.Now.ToShortTimeString() + "  "+DateTime.Now.ToShortDateString()+"  "+"Reporte Facturas" + "  " + "La Casa del Corsa", new Font("Tahoma", 8,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }
        DataGridViewPrinter MyDataGridViewPrinter;

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void BtnImprimirDetalles_Click(object sender, EventArgs e)
        {
            SetupThePrintingDetalles();
            printDocument1.Print();

        }
        private bool SetupThePrintingDetalles()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDocument1.DocumentName = this.Text;
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.DefaultPageSettings.Margins = new Margins(15, 15, 15, 15);
            MyDataGridViewPrinter = new DataGridViewPrinter(dataListadoDetalle,
            printDocument1, true, true, this.Text + DateTime.Now.ToShortDateString() + "  " +"  "+txtProveedor.Text + "  " + "Fac Num.:" +txtSerie.Text+"-"+txtCorrelativo.Text + "  " + "Total:" + lblTotalPagado.Text + "  " + "La Casa del Corsa", new Font("Tahoma", 8,
                FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }
    }
}
