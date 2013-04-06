using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgrocomercioWEB.Maestros.Reportes
{
    public partial class wfrmVistaReportes : BasePage 
    {

        #region "Atributos y Propiedades"
        private string _lstCodProveedor = string.Empty;
        public string CodProveedor
        {
            get
            {
                _lstCodProveedor = ExtraeQueryStringCadena("CodProvee").ToString();
                return _lstCodProveedor;
            }

            
        }

        private string _lstNomProveedor = string.Empty;
        public string NomProveedor
        {
            get
            {
                _lstNomProveedor = ExtraeQueryStringCadena("nProvee").ToString();
                return _lstNomProveedor;
            }

            
        }
        private DateTime  _ldtFecDes = DateTime.Today ;
        public DateTime  FecDes
        {
            get
            {
                int linFecIni = ExtraeQueryStringEntero("fDes");
                return _ldtFecDes;
            }
        }

        private DateTime _ldtFecHas = DateTime.Today;
        public DateTime FecHas
        {
            get
            {
                int linFecHas = ExtraeQueryStringEntero("fHas");
                return _ldtFecHas;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}