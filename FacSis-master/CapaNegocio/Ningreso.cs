using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class Ningreso
    {

        #region Insertar

        public static string Insertar(int idTrabajador, int idProveedor, DateTime fecha, string tipoComprobante,
    string serie, string correlativo, string estado, DataTable dtDetalles)
        {
            Dingreso Ingreso = new Dingreso()
            {
                IdTrabajador = idTrabajador,
                IdProveedor = idProveedor,
                Fecha = fecha,
                TipoComprobante = tipoComprobante,
                Serie = serie,
                Correlativo = correlativo,
                Estado = estado
            };

            var ListaDetalles = new List<DdetalleIngreso>();

            foreach (DataRow filasDetalles in dtDetalles.Rows)
            {
                DdetalleIngreso detalleIngreso = new DdetalleIngreso();
                detalleIngreso.Articulo = Convert.ToString(filasDetalles["articulo"]);
                detalleIngreso.PrecioVenta = Convert.ToDecimal(filasDetalles["precio_venta"].ToString());
                detalleIngreso.StockActual = Convert.ToInt32(filasDetalles["Cantidad"].ToString());
                ListaDetalles.Add(detalleIngreso);
            }
            return Ingreso.Insertar(Ingreso, ListaDetalles);
        }
        #endregion


        #region Anular

        public static string Anular(int idIngreso)
        {
            Dingreso Ingreso = new Dingreso()
            {
                IdIngreso = idIngreso
            };

            return Ingreso.Anular(Ingreso);
        }
        #endregion


        #region Mostrar

        public static DataTable Mostrar()
        {
            return new Dingreso().Mostrar();
        }

        #endregion


        #region BuscarFecha

        public static DataTable BuscarFechas(string fechaInicio, string fechaFin)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.BuscarFechas(fechaInicio, fechaFin);
        }
        #endregion

        #region BuscarFechaMas30

        public static DataTable BuscarFechasMas30(string fechaInicio, string fechaFin, string estado)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.BuscarFechaMas30(fechaInicio, fechaFin, estado);
        }
        #endregion

        #region BuscarEstado

        public static DataTable BuscarEstados(string estado)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.BuscarEstado(estado);
        }
        #endregion

        #region BuscarCliente

        public static DataTable BuscarClientes(string idCliente)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.BuscarCliente(idCliente);
        }
        #endregion


        #region BuscarFactura

        public static DataTable BuscarFacturas(string serie, string factura)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.BuscarFactura(serie,factura);
        }
        #endregion

        #region MostrarDetalles

        public static DataTable MostrarDetalles(string textoBuscar)
        {
            Dingreso Ingreso = new Dingreso();

            return Ingreso.MostrarDetalles(textoBuscar);
        }
        #endregion

    }
}
