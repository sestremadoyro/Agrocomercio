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

namespace AgrocomercioWEB.Compras
{

    public partial class wfrmReportesCompras : BasePage
    {
        public String _click = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            String _selectedValue = "";
            SetEstado("INI");
            if (Page.IsPostBack)
            {
                if (lblEstado.Value != "")
                {
                    switch (lblEstado.Value)
                    {
                        case ("PRV_SELECT"):
                            _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                            ddlProveedor.SelectedValue = _selectedValue;
                            ddlProveedor_SelectedIndexChanged(sender, e);
                            break;
                    }
                    lblEstado.Value = "";
                }
            }
            else
            {
                ddlReportes_SelectedIndexChanged(sender, e);
                ddlProveedor.SelectedValue = "9999";
                ddlProveedor_SelectedIndexChanged(sender, e);
            }
            
        }

#region FUNCIONES DEL FORMULARIO
        protected void ddlReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSubTitulo.Text = "REPORTE " + ddlReportes.SelectedItem.Text;
            switch (ddlReportes.SelectedValue)
            {
                case ("PG"):
                    chkPorMes.Visible = false;
                    chkPorFecha.Visible = true;
                    ddlMeses.Visible = false;
                    divFechas.Visible = true;
                    btnImprimir.OnClientClick = "AbrirVentanaProveedoresGeneral()";
                    break;
                case ("CPM"):
                    chkPorMes.Visible = true;
                    chkPorFecha.Visible = false;
                    ddlMeses.Visible = true;
                    divFechas.Visible = false;
                    btnImprimir.OnClientClick = "AbrirVentanaProveedoresMes()";
                    break;                
            }

            
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nPrvCod = 0;
            nPrvCod = int.Parse(ddlProveedor.SelectedValue);

            txtProveedor.Text = ddlProveedor.SelectedItem.Text;
        }
        
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            DataTable dtResultado = null;
            clsOperaciones lstOperaciones = new clsOperaciones();
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;
            int prvCod = 0;
            try
            {
                if (ValidarDatos(ref cMensaje))
                {
                    prvCod = int.Parse(ddlProveedor.SelectedValue);

                    switch (ddlReportes.SelectedValue)
                    {
                        case ("PG"):
                            if (chkPorFecha.Checked)
                            {
                                dFecIni = DateTime.Parse(txtFecIni.Text);
                                dFecFin = DateTime.Parse(txtFecFin.Text);
                            }
                            dtResultado = lstOperaciones.RepComprasProveedorGeneral(prvCod, chkPorFecha.Checked, dFecIni, dFecFin);
                            break;
                        case ("CPM"):
                            if (chkPorMes.Checked)
                            {
                                dFecIni = DateTime.Parse(ddlMeses.SelectedValue);
                            }
                            dtResultado = lstOperaciones.RepComprasProveedorXMes(prvCod, chkPorMes.Checked, dFecIni);
                            break;
                    }

                    if (dtResultado.Rows.Count > 0)
                    {
                        SetEstado("PRO");

                        string cMes = "TODOS";
                        if (chkPorMes.Checked)
                            cMes = ddlMeses.SelectedItem.Text;

                        AgregarVariableSession("dtRepProveedores", dtResultado);
                        AgregarVariableSession("nTipCam", txtTipCam.Text);
                        AgregarVariableSession("cMes", cMes);
                    }
                    else
                        SetEstado("ERR");
                }
                else
                {
                    MessageBox(cMensaje);
                }
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
        protected void chkPorFecha_CheckedChanged(object sender, EventArgs e)
        {
            txtFecIni.Enabled = chkPorFecha.Checked;
            txtFecFin.Enabled = chkPorFecha.Checked;
            if (!chkPorFecha.Checked)
            {
                txtFecIni.Text = "";
                txtFecFin.Text = "";
            }
        }

        protected void chkPorMes_CheckedChanged(object sender, EventArgs e)
        {
            ddlMeses.Enabled = chkPorMes.Checked;

        }

#endregion

#region FUNCIONES DE CARGA DE LISTAS DE DATOS
        
        public void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();
            ddlProveedor.DataSource = lstProveedores.GetAll();
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("[TODOS]", "9999"));
            ddlProveedor.Items.Add(new ListItem("<< SIN RESULTADOS >>", "8888"));
            lstProveedores = null;
        }
        public void CargarMeses()
        {
            int limite = 12;
            string cFecPro = "";
            string cDescri = "";
            DateTime dFecPro = DateTime.Today;

            for (int i = limite; i > 0; i--) {
                cFecPro = dFecPro.ToString("yyyy-MM-") + "01";
                cDescri = GetMes(dFecPro.Month) + " - " + dFecPro.Year.ToString();
                ddlMeses.Items.Add(new ListItem(cDescri, cFecPro));
                dFecPro = dFecPro.AddMonths(-1);
            }   
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
            
                    txtFecIni.Text = "";
                    txtFecFin.Text = "";
                    txtFecIni.Enabled = false;
                    txtFecFin.Enabled = false;
                    ddlMeses.Enabled = false;

                    lblExito.Visible = false;
                    lblError.Visible = false;
                    CargarProveedores();
                    CargarMeses();

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
                    lblExito.Visible = true;
                    lblError.Visible = false;
                    break;
                case ("ERR"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                    lblExito.Visible = false;
                    lblError.Visible = true;
                    break;
            }

            
        }
        
        private Boolean ValidarDatos(ref string cMensaje)
        {
            Boolean bRes = true;

            switch (ddlReportes.SelectedValue)
            {
                case "PG":                    
                    break;
                case "CPM":
                    if (chkPorFecha.Checked && (txtFecFin.Text == "" || txtFecIni.Text == ""))
                    {
                        cMensaje = "Debe Escoger una Fecha de Inicio y Fin";
                        txtFecIni.Focus();
                        return false;
                    }
                    if (ddlProveedor.SelectedValue == "8888")
                    {
                        cMensaje = "Debe Escoger un Proveedor";
                        txtProveedor.Focus();
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return bRes;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static String[] BuscarProveedores(string prefixText)
        {
            String[] sList = null;
            List<string> sProveedorList = new List<string>();
            clsProveedores lstProveedores = new clsProveedores();

            try
            {
                DataTable dtProveedores = lstProveedores.BuscarProveedores(prefixText);

                if (dtProveedores.Rows.Count > 0)
                {
                    sProveedorList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem("[TODOS]", "9999"));

                    for (int i = 0; i < dtProveedores.Rows.Count; i++)
                    {
                        sProveedorList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem(dtProveedores.Rows[i]["PrvRazon"].ToString(), dtProveedores.Rows[i]["PrvCod"].ToString()));
                    }
                    sList = new String[10];
                    sList = sProveedorList.ToArray();
                }
                else
                {
                    sProveedorList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem("<< SIN RESULTADOS >>", "8888"));
                    sList = new String[1];
                    sList = sProveedorList.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lstProveedores = null;
            }
            return sList;
        }

#endregion

       

        
    }
}

