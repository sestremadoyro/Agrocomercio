using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace AgrocomercioWEB.Ventas
{
    public partial class ImpresionDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable _dtResultado = null;
                _dtResultado = (DataTable)Session["dtFactura"];
                if (_dtResultado.Rows.Count != 0)
                {
                    ReportDocument rpt = new ReportDocument();
                    rpt.Load(Server.MapPath("ImpresionFactura.rpt"));
                    rpt.FileName = Server.MapPath("ImpresionFactura.rpt");

                    rpt.SetDataSource(_dtResultado);
                    CrystalReportViewer1.ReportSource = rpt;
                    CrystalReportViewer1.ReuseParameterValuesOnRefresh = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}