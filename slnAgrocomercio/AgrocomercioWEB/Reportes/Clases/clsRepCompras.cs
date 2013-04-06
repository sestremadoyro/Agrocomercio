using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrocomercioWEB.Reportes.Clases
{
    public class clsRepCompras
    {
            public int OpeCod;
            public int PrvCod;
            public String PrvRazon;
            public DateTime OpeFecEmision;
            public String NroFactura;
            public String letraTip;
            public String LetraCant;
            public DateTime dFecVenc;
            public String cEstado;
            public Double nCompraUSD;
            public Double nCompraPEN;
            public Double nDsctoUSD;
            public Double nDsctoPEN;
            public Double nPagoUSD;
            public Double nPagoPEN;
            public Double nSaldoUSD;
            public Double nSaldoPEN;
            public Double nTotalUSD;
            public Double nTotalPEN;

            public clsRepCompras()
            {
                OpeCod = 0;
                PrvCod = 0;
                PrvRazon = "";
                OpeFecEmision = DateTime.Today;
                NroFactura = "";
                letraTip = "";
                LetraCant = "";
                dFecVenc = DateTime.Today;
                cEstado = "";
                nCompraUSD = 0.0;
                nCompraPEN = 0.0;
                nDsctoUSD = 0.0;
                nDsctoPEN = 0.0;
                nPagoUSD = 0.0;
                nPagoPEN = 0.0;
                nSaldoUSD = 0.0;
                nSaldoPEN = 0.0;
                nTotalUSD = 0.0;
                nTotalPEN = 0.0; 
            }
    }
}