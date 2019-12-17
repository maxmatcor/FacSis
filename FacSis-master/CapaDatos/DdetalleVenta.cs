using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DdetalleVenta
    {
        #region Contructores
        public DdetalleVenta()
        {

        }

        public DdetalleVenta( int idVenta, int cantidad,
            decimal precioVenta)
        {
            IdVenta = idVenta;
            Cantidad = cantidad;
            PrecioVenta = precioVenta;
        }
        #endregion


        #region Propiedades
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }

        #endregion


        #region MetodoInsertar
        //Metodo Insertar
        public string Insertar(DdetalleVenta DetalleVenta, ref SqlConnection conexionSql, ref SqlTransaction transaccionSql)
        {

            string respuesta = "";

            try
            {


                //Establecer el comando SQL
                var comandoSql = new SqlCommand("[spinsertar_detalle_venta]", conexionSql, transaccionSql);
                comandoSql.CommandType = CommandType.StoredProcedure;

                //Parametros para el comandoSql (StoreProcedure)
                var parIdDetalleVenta = new SqlParameter("@iddetalle_venta", SqlDbType.Int);
                parIdDetalleVenta.Direction = ParameterDirection.Output;
                comandoSql.Parameters.Add(parIdDetalleVenta);

                var parIdVenta = new SqlParameter("@idventa", SqlDbType.Int);
                parIdVenta.Value = DetalleVenta.IdVenta;
                comandoSql.Parameters.Add(parIdVenta);


                var parCantidad = new SqlParameter("@cantidad", SqlDbType.Int);
                parCantidad.Value = DetalleVenta.Cantidad;
                comandoSql.Parameters.Add(parCantidad);

                SqlParameter parPrecioVenta = new SqlParameter("@precio_venta", SqlDbType.Money);
                parPrecioVenta.Value = DetalleVenta.PrecioVenta;
                comandoSql.Parameters.Add(parPrecioVenta);


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
