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
    public partial class ImpresionNotaGeneral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int tipo_rep;
                DataTable _dtcontenido = null;
                DataTable _dtDetalleOper = null;
                tipo_rep = (int)Session["tiprep"];
                if(tipo_rep ==1)

                    _dtcontenido = (DataTable)Session["dtRepNotasGen"];
                else
                    _dtcontenido = (DataTable)Session["dtRepNotasDet"];

                if (_dtcontenido != null)
                {
                    if (_dtcontenido.Rows.Count != 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Server.MapPath("rpt/crREpNotaGeneral.rpt"));
                        rpt.FileName = Server.MapPath("rpt/crREpNotaGeneral.rpt");
                        rpt.SetDataSource(_dtcontenido);
                      //  rpt.Subreports[0].SetDataSource(_dtDetalleOper);
                        
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