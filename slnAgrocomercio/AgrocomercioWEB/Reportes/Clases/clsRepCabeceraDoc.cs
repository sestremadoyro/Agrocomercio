using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrocomercioWEB.Reportes.Clases
{
    public class clsRepCabeceraDoc
    {
        private string m_cRazonSocial;
        private string m_cDireccion;
        private string m_cTelefono;
        private string m_cRuc;
        private string m_cNroGuia;
        private string m_cNroPedido;        
        private string m_cFormaPago;
        private string m_cAnio;
        private string m_cMes;
        private string m_cDia;
        private string m_cValorVenta;
        private string m_cFlete;
        private string m_cIGV;
        private string m_cTotal;
        private string m_cTotalSinFlete;
        private string m_cTotalLetras;
        private string m_dCantidad;
        private string m_dDescripcion;
        private string m_dPrecioUnitario;
        private string m_dDescuento;
        private string m_dMonto;
        private string m_OpeTipCiclo;
        private string m_OpeCiclo;

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
        public string cTelefono
        {
            get { return m_cTelefono; }
            set { m_cTelefono = value; }
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
        public string cNroPedido
        {
            get { return m_cNroPedido; }
            set { m_cNroPedido = value; }
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
        public string cTotalSinFlete
        {
            get { return m_cTotalSinFlete; }
            set { m_cTotalSinFlete = value; }
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

        public string dOpeTipCiclo
        {
            get { return m_OpeTipCiclo; }
            set { m_OpeTipCiclo = value; }
        }
        public string dOpeCiclo
        {
            get { return m_OpeCiclo; }
            set { m_OpeCiclo = value; }
        }

        public clsRepCabeceraDoc()
        {
            m_cRazonSocial="";
            m_cDireccion="";
            m_cRuc="";
            m_cNroGuia="";
            m_cFormaPago="";
            m_cAnio="";
            m_cMes="";
            m_cDia="";
            m_cValorVenta="";
            m_cFlete="";
            m_cIGV="";
            m_cTotal="";
            m_cTotalLetras="";
            m_dCantidad="";
            m_dDescripcion="";
            m_dPrecioUnitario="";
            m_dDescuento="";
            m_dMonto="";
            // para la guia de remision
            m_Destinatario="";
            m_PLlegada="";
            m_NroFactura="";
            m_Unidad="";
        }
    }
}