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
    public partial class wfrmVistaRepProveedoresMes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRepProveedores = null;
                Double nTipCam = 0.0;
                String cMes = "";
                dtRepProveedores = (DataTable)Session["dtRepProveedores"];
                nTipCam = Double.Parse((String)Session["nTipCam"]);
                cMes = (String)Session["cMes"];

                if (dtRepProveedores != null)
                {
                    if (dtRepProveedores.Rows.Count != 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath("crRepProveedoresMes.rpt"));
                        rpt.FileName = Server.MapPath("crRepProveedoresMes.rpt");
                        rpt.SetDataSource(dtRepProveedores);
                        rpt.SetParameterValue("@TipCam", nTipCam);
                        rpt.SetParameterValue("@Mes", cMes);
                        
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