using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgrocomercioWEB.Reportes.Clases
{
    public class clsRepDetalleFactura
    {
            public int ArtCod;
            public Double ArtPeso;
            public int LotNro;
            public string ArtDescripcion;
            public string UniAbrev;
            public Double dtpCantidad;
            public Double dtpPrecioVen;
            public Double dtpDscto;
            public Double dtpSubTotal;

            public clsRepDetalleFactura()
            {
                 ArtCod = 0;
                 ArtPeso = 0.0;
                 LotNro = 0;
                 ArtDescripcion = "";
                 UniAbrev = "";
                 dtpCantidad = 0.0;
                 dtpPrecioVen = 0.0;
                 dtpDscto = 0.0;
                 dtpSubTotal = 0.0;
            }
    }
}