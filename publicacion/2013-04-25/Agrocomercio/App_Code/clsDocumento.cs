using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrocomercioWEB
{
    public class clsDocumento
    {
        private string m_cRazonSocial;
        private string m_cDireccion;
        private string m_cRuc;
        private string m_cNroGuia;
        private string m_cFormaPago;
        private string m_cAnio;
        private string m_cMes;
        private string m_cDia;
        private string m_cValorVenta;
        private string m_cFlete;
        private string m_cIGV;
        private string m_cTotal;
        private string m_cTotalLetras;
        private string m_dCantidad;
        private string m_dDescripcion;
        private string m_dPrecioUnitario;
        private string m_dDescuento;
        private string m_dMonto;
        // para la guia de remision
        private string m_Destinatario;
        private string m_PLlegada;
        private string m_NroFactura;
        private string m_Unidad;

        public string Unidad
        {
            get { return m_Unidad; }
            set { m_Unidad = value; }
        }
        

        public string NroFactura
        {
            get { return m_NroFactura; }
            set { m_NroFactura = value; }
        }
        

        public string PLlegada
        {
            get { return m_PLlegada; }
            set { m_PLlegada = value; }
        }
        

        public string Destinatario
        {
            get { return m_Destinatario; }
            set { m_Destinatario = value; }
        }
        


        public string cRazonSocial
        {
            get { return m_cRazonSocial; }
            set { m_cRazonSocial = value; }
        }
        public string cDireccion
        {
            get { return m_cDireccion; }
            set { m_cDireccion = value; }
        }
        public string cRuc
        {
            get { return m_cRuc; }
            set { m_cRuc = value; }
        }
        public string cNroGuia
        {
            get { return m_cNroGuia; }
            set { m_cNroGuia = value; }
        }
        public string cFormaPago
        {
            get { return m_cFormaPago; }
            set { m_cFormaPago = value; }
        }
        public string cAnio
        {
            get { return m_cAnio; }
            set { m_cAnio = value; }
        }
        public string cMes
        {
            get { return m_cMes; }
            set { m_cMes = value; }
        }
        public string cDia
        {
            get { return m_cDia; }
            set { m_cDia = value; }
        }
        public string cValorVenta
        {
            get { return m_cValorVenta; }
            set { m_cValorVenta = value; }
        }
        public string cFlete
        {
            get { return m_cFlete; }
            set { m_cFlete = value; }
        }
        public string cIGV
        {
            get { return m_cIGV; }
            set { m_cIGV = value; }
        }
        public string cTotal
        {
            get { return m_cTotal; }
            set { m_cTotal = value; }
        }
        public string cTotalLetras
        {
            get { return m_cTotalLetras; }
            set { m_cTotalLetras = value; }
        }
        public string dCantidad
        {
            get { return m_dCantidad; }
            set { m_dCantidad = value; }
        }
        public string dDescripcion
        {
            get { return m_dDescripcion; }
            set { m_dDescripcion = value; }
        }
        public string dPrecioUnitario
        {
            get { return m_dPrecioUnitario; }
            set { string m_dPrecioUnitario = value; }
        }
        public string dDescuento
        {
            get { return m_dDescuento; }
            set { m_dDescuento = value; }
        }
        public string dMonto
        {
            get { return m_dMonto; }
            set { m_dMonto = value; }
        }
        public clsDocumento()
        {
            
        }
    }
}