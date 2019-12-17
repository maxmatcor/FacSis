using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DdetalleIngreso
    {
        #region Constructores
        public DdetalleIngreso()
        {

        }

        public DdetalleIngreso(int idDetalleIngreso, int idIngreso, int idArticulo, String articulo, decimal precioCompra,
            decimal precioVenta, int stockInicial, int stockActual, DateTime fechaProduccion, DateTime fechaVecimiento)
        {
            IdDetalleIngreso = idDetalleIngreso;
            IdIngreso = idIngreso;
            IdArticulo = idArticulo;
            Articulo = articulo;
            PrecioCompra = precioCompra;
            PrecioVenta = precioVenta;
            StockInicial = stockInicial;
            StockActual = stockActual;
            FechaProduccion = fechaProduccion;
            FechaVencimiento = fechaVecimiento;
        }
        #endregion


        #region Propiedades
        public int IdDetalleIngreso { get; set; }
        public int IdIngreso { get; set; }
        public int IdArticulo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockInicial { get; set; }
        public int StockActual { get; set; }
        public DateTime FechaProduccion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public String Articulo { get; set; }


        #endregion


        #region MetodoInsertar
        //Metodo Insertar
        public string Insertar(DdetalleIngreso DetalleArticulo, ref SqlConnection conexionSql, ref SqlTransaction transaccionSql)
        {

            string respuesta = "";

            try
            {


                //Establecer el comando SQL
                var comandoSql = new SqlCommand("[spinsertar_detalle_ingreso]", conexionSql, transaccionSql);
                comandoSql.CommandType = CommandType.StoredProcedure;

                //Parametros para el comandoSql (StoreProcedure)
                var parIdDetalleIngreso = new SqlParameter("@iddetalle_ingreso", SqlDbType.Int);
                parIdDetalleIngreso.Direction = ParameterDirection.Output;
                comandoSql.Parameters.Add(parIdDetalleIngreso);

                var parIdIngreso = new SqlParameter("@idingreso", SqlDbType.Int);
                parIdIngreso.Value = DetalleArticulo.IdIngreso;
                comandoSql.Parameters.Add(parIdIngreso);

                var parPrecioVenta = new SqlParameter("@precio_venta", SqlDbType.Money);
                parPrecioVenta.Value = DetalleArticulo.PrecioVenta;
                comandoSql.Parameters.Add(parPrecioVenta);

                var parStockActual = new SqlParameter("@stock_actual", SqlDbType.Int);
                parStockActual.Value = DetalleArticulo.StockActual;
                comandoSql.Parameters.Add(parStockActual);

                var parArticulo = new SqlParameter("@articulo", SqlDbType.VarChar, 50);
                parArticulo.Value = DetalleArticulo.Articulo;
                comandoSql.Parameters.Add(parArticulo);


                //Ejecucion del comando
                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";


            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }
        #endregion
    }
}