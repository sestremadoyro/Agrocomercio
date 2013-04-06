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
    public partial class ImpresionBoleta : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable _dtCabeceraOper = null;
                DataTable _dtDetalleOper = null;
                _dtCabeceraOper = (DataTable)Session["dtCabeceraOperacion"];
                _dtDetalleOper = g_dtDetOperacion;
                if (_dtDetalleOper != null && _dtCabeceraOper != null)
                {
                    if (_dtDetalleOper.Rows.Count != 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath("rpt/crBoleta.rpt"));
                        rpt.FileName = Server.MapPath("rpt/crBoleta.rpt");
                        rpt.SetDataSource(_dtCabeceraOper);
                        rpt.Subreports[0].SetDataSource(_dtDetalleOper);
                        
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