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

namespace AgrocomercioWEB.Compras
{

    public partial class wfrmRepResumenCliente : BasePage
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
                        case ("CLI_SELECT"):
                            _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                            ddlClientes.SelectedValue = _selectedValue;
                            ddlClientes_SelectedIndexChanged(sender, e);
                            break;
                    }
                    lblEstado.Value = "";
                }
            }
            else
            {
                ddlClientes.SelectedValue = "9999";
                ddlClientes_SelectedIndexChanged(sender, e);
            }
            
        }

#region FUNCIONES DEL FORMULARIO

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nPrvCod = 0;
            nPrvCod = int.Parse(ddlClientes.SelectedValue);            
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
                    if (txtCliente.Text  != "")
                        prvCod = int.Parse(ddlClientes.SelectedValue);
                    else
                        prvCod = 9999;

                    if (chkPorFecha.Checked)
                    {
                        dFecIni = DateTime.Parse(txtFecIni.Text);
                        dFecFin = DateTime.Parse(txtFecFin.Text);
                    }
                    dtResultado = lstOperaciones.RepResumenCliente(prvCod, chkPorFecha.Checked, dFecIni, dFecFin);
                    
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

        

#endregion

#region FUNCIONES DE CARGA DE LISTAS DE DATOS

        public void CargarClientes()
        {
            clsClientes lstClientes = new clsClientes();

            ddlClientes.DataSource = lstClientes.GetAll();
            ddlClientes.DataBind();
            ddlClientes.Items.Add(new ListItem("[TODOS]", "9999"));
            ddlClientes.Items.Add(new ListItem("<< SIN RESULTADOS >>", "8888"));

            lstClientes = null;
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
        
                    lblExito.Visible = false;
                    lblError.Visible = false;
                    CargarClientes();

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

            if (chkPorFecha.Checked && (txtFecFin.Text == "" || txtFecIni.Text == ""))
            {
                cMensaje = "Debe Escoger una Fecha de Inicio y Fin";
                txtFecIni.Focus();
                return false;
            }
            if (ddlClientes.SelectedValue == "8888")
            {
                cMensaje = "Debe Escoger un Cliente";
                txtCliente.Focus();
                return false;
            }
            return bRes;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static String[] BuscarClientes(string prefixText)
        {
            String[] sList = null;
            List<string> sClienteList = new List<string>();
            clsClientes lstClientes = new clsClientes();

            try
            {
                DataTable dtClientes = lstClientes.BuscarClientes(prefixText);

                if (dtClientes.Rows.Count > 0)
                {
                    for (int i = 0; i < dtClientes.Rows.Count; i++)
                    {
                        sClienteList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem(dtClientes.Rows[i]["CliNombre"].ToString(), dtClientes.Rows[i]["CliCod"].ToString()));
                    }
                    sList = new String[10];
                    sList = sClienteList.ToArray();
                }
                else
                {
                    sClienteList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem("[NUEVO CLIENTE]", "999"));

                    sList = new String[1];
                    sList = sClienteList.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lstClientes = null;
            }
            return sList;
        }

#endregion

        protected void gridVentasxCobrar_ColumnsCreated(object sender, EventArgs e)
        {
            //int width = 100;
            //int count = gridVentasxCobrar.Columns.Count;
            //int average = 0;
            //if (count > 0)
            //    average = width / count;
            //int i = 0;

            //foreach (Column column in gridVentasxCobrar.Columns)
            //{
            //    if (i < count - 1)
            //    {
            //        column.Width = average.ToString() + "%";
            //    }
            //    else
            //    {
            //        column.Width = width.ToString() + "%";
            //    }

            //    width -= average;
            //    i++;
            //}
        }

       

        
    }
}

