using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Text;
using pryAgrocomercioBLL.EntityCollection;
//using pryAgrocomercioBLL.Clases;

using pryAgrocomercioDAL;
using System.Globalization;
using System.Configuration;


namespace AgrocomercioWEB.pagos
{
    public partial class wfrmComprasProveedores : BasePage
    {
        public String _click = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
            }
            else
            {
                //IniciarCampos();
            }

        }

        protected void dgvListOperLetras_RowDataBound(object sender, GridViewRowEventArgs e) { 
           
        }
    }
}