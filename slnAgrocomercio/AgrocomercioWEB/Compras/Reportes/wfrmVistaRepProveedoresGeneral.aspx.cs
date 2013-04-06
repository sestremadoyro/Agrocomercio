using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace AgrocomercioWEB.Reportes
{
    public partial class wfrmVistaRepProveedoresGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRepProveedores = null;
                Double nTipCam = 0.0;
                dtRepProveedores = (DataTable)Session["dtRepProveedores"];
                nTipCam = Double.Parse((String)Session["nTipCam"]);
                
                if (dtRepProveedores != null)
                {
                    if (dtRepProveedores.Rows.Count != 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath("crRepProveedoresGeneral.rpt"));
                        rpt.FileName = Server.MapPath("crRepProveedoresGeneral.rpt");
                        rpt.SetDataSource(dtRepProveedores);
                        rpt.SetParameterValue("@TipCam", nTipCam);
                        
                        CrystalReportViewer1.ReportSource = rpt;
                        CrystalReportViewer1.ReuseParameterValuesOnRefresh = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}