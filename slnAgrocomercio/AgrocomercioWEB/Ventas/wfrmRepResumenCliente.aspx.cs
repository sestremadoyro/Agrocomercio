using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Text;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;

using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Configuration;
using AgrocomercioWEB;
using System.Threading;
using Obout.Grid;

namespace AgrocomercioWEB.Reportes
{

    public partial class wfrmRepResumenCliente : BasePage
    {
        public String _click = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           SetEstado("INI");
            if (Page.IsPostBack)
            {
                
            }
            
        }

#region FUNCIONES DEL FORMULARIO

      
        
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataTable dtResultado = null;
            clsOperaciones colOperaciones = new clsOperaciones();
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;
            try
            {
                dtResultado = colOperaciones.RepResumenCliente();

                if (dtResultado.Rows.Count > 0)
                {
                    SetEstado("PRO");

                    gridVentasxCobrar.DataSource = dtResultado;
                    gridVentasxCobrar.DataBind();

                    AgregarVariableSession("dtRepClientes", dtResultado);
                    AgregarVariableSession("nTipCam", txtTipCam.Text);

                    
                }
                else
                    SetEstado("ERR");
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }


        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            SetEstado("PRO");
        }
     

        

#endregion


#region FUNCIONES GENERALES
        private void SetEstado(string cTipo)
        {
            switch (cTipo)
            {
                case ("INI"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                    HabilitarBtn(btnExcel, false);
                    HabilitarBtn(btnPdf, false);
                    lblExito.Visible = false;
                    lblError.Visible = false;

                    clsAtributos Atributos = new clsAtributos();
                    var oTip = ((List<Atributos>)Atributos.ListAtributos(8)).FirstOrDefault();
                    if (oTip == null) 
                        txtTipCam.Text = "2.56";
                    else
                        txtTipCam.Text = oTip.AtrDescripcion;
                    Atributos = null;

                    break;
                case ("PRO"):
                    HabilitarBtn(btnProcesar, false);
                    HabilitarBtn(btnImprimir, true);
                    HabilitarBtn(btnExcel, true);
                    HabilitarBtn(btnPdf, true);
                    lblExito.Visible = true;
                    lblError.Visible = false;
                    break;
                case ("ERR"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                    HabilitarBtn(btnExcel, false);
                    HabilitarBtn(btnPdf, false);
                    lblExito.Visible = false;
                    lblError.Visible = true;
                    break;
            }

            
        
            
        }

        protected void gridVentasxCobrar_Filtering(object sender, EventArgs e)
        {
            // filter for OrderDate
            Column orderDateColumn = gridVentasxCobrar.Columns[1];

            if (orderDateColumn.FilterCriteria.Option is CustomFilterOption)
            {
                CustomFilterOption filterOption = orderDateColumn.FilterCriteria.Option as CustomFilterOption;

                
            }

        }
        

#endregion


        
    }
}

