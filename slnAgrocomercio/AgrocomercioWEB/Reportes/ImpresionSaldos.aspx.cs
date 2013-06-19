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
    public partial class ImpresionSaldos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable _dtSaldos = null;
                //Int32 nPrvCod = 0;
                _dtSaldos = (DataTable)Session["dtSaldos"];
                //nPrvCod = Int32.Parse((String)Session["nPrvCod"]);

                if (_dtSaldos != null )
                {
                    if (_dtSaldos.Rows.Count != 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath("rpt/crSaldos.rpt"));
                        rpt.FileName = Server.MapPath("rpt/crSaldos.rpt");
                        rpt.SetDataSource(_dtSaldos);
                        
                        CrystalReportViewer8.ReportSource = rpt;
                        CrystalReportViewer8.ReuseParameterValuesOnRefresh = true;
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