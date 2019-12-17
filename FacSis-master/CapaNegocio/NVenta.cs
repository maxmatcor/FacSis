using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class NVenta
    {

        #region Insertar
        public static string Insertar(int idCliente, int idTrabajador, DateTime fecha, string tipoComprobante, DataTable dtDetalles)
        {
            Dventa Venta = new Dventa()
            {
                IdCliente = idCliente,
                IdTrabajador = idTrabajador,
                Fecha = fecha,
                TipoComprobante = tipoComprobante,
            };


            var ListaDetalles = new List<DdetalleVenta>();

            foreach (DataRow filasDetalles in dtDetalles.Rows)
            {
                DdetalleVenta detalleIngreso = new DdetalleVenta();
                detalleIngreso.Cantidad = Convert.ToInt32(filasDetalles["cantidad"]);
                detalleIngreso.PrecioVenta = Convert.ToDecimal(filasDetalles["precio_venta"]);

                ListaDetalles.Add(detalleIngreso);
            }
            return Venta.Insertar(Venta, ListaDetalles);
        }
        #endregion


        #region Eliminar

        public static string Eliminar(int idIngreso)
        {
            Dventa Venta = new Dventa()
            {
                IdVenta = idIngreso
            };

            return Venta.Eliminar(Venta);
        }
        #endregion


        #region Mostrar

        public static DataTable Mostrar()
        {
            return new Dventa().Mostrar();
        }
        #endregion


        #region BuscarFechas

        public static DataTable BuscarFechas(string fechaInicio, string fechaFin)
        {
            Dventa Venta = new Dventa();

            return Venta.BuscarFechas(fechaInicio, fechaFin);
        }
        #endregion


        #region MostrarDetalles
        public static DataTable MostrarDetalles(string textoBuscar)
        {
            Dventa Venta = new Dventa();
            return Venta.MostrarDetalles(textoBuscar);
        }

        #endregion


        #region BuscarArticuloVentaNombre

        public static DataTable BuscarArticuloVentaNombre(string textoBuscar)
        {
            Dventa Venta = new Dventa();

            return Venta.BuscarAritculoVentaNombre(textoBuscar);
        }
        #endregion


        #region BuscarAritculoVentaCodigo
        public static DataTable BuscarAritculoVentaCodigo(string textoBuscar)
        {
            Dventa Venta = new Dventa();

            return Venta.BuscarAritculoVentaCodigo(textoBuscar);
        }
        #endregion
    }
}
