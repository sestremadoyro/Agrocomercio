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

namespace AgrocomercioWEB.pagos
{
    public partial class wfrmrptventacredito : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             int _selectedIndex = 0;
            String _selectedValue = "";
            if (Page.IsPostBack)
            {
                if (lblEstado.Value != "")
                {
                    switch (lblEstado.Value)
                    {
                        case ("DGVLIS_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());                           
                            lblEstado.Value = "EMPTY";
                            break;
                        case ("DGVLET_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());                           
                            lblEstado.Value = "EMPTY";
                            break;
                        case ("CLI_SELECT"):
                            _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                            ddlCliente.SelectedValue = _selectedValue;
                            ddlCliente_SelectedIndexChanged(sender, e);

                            break;
                    }
                }
                lblEstado.Value = "";
            }
            else
            {
                IniciarCampos();
            }
        }
        #region FUNCIONES del formulario
        protected void dgvLista_RowDataBound(object sender, GridViewRowEventArgs e) { }
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nPrvCod = 0;
            nPrvCod = int.Parse(ddlCliente.SelectedValue);

            if (nPrvCod == 0)
            {
                //txtDocCli.Text = "";
                //txtDireccion.Text = "";
                //txtTelefono.Text = "";
            }
            else
            {
                clsClientes lstClientes = new clsClientes();
                Clientes Cliente = new Clientes();

                Cliente = lstClientes.GetCliente(nPrvCod);

                txtCliente.Text = Cliente.CliNombre.ToString();
                //lblPersona.Text = Cliente.CliNombre.ToString();
                //hdcodper.Value = nPrvCod.ToString();


                lstClientes = null;
                Cliente = null;
                ///////////////////////////////////
                HabilitarBtn(btnNuevo, false);

            }
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
        #region FUNCIONES DE LOS BOTONES
        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            dgvLista.Columns[2].Visible = true;
            dgvLista.Columns[3].Visible = true;
            dgvLista.Columns[7].Visible = true;
            dgvLista.Columns[8].Visible = true;
            dgvLista.Columns[12].Visible = true;
            dgvLista.Columns[15].Visible = true;
            dgvLista.Columns[17].Visible = true;
            String _filtro = "", _moneda = "", _estado = "";
            int _cod_prove;
            DateTime fregdesde = DateTime.Today, freghasta = DateTime.Today,
                fvendesde = DateTime.Today, fvenhasta = DateTime.Today,fultdesde = DateTime.Today, fulthasta = DateTime.Today;
            clsvwtodo_movimiento formulario = new clsvwtodo_movimiento();
            DataTable dtRegistro;
            String _tip_pago="";
            int _tip_doc=0, _cod_tra=0;
            if (txtCliente.Text.Length > 0)
            {
                _filtro = _filtro + "2";
                _cod_prove = Convert.ToInt32(ddlCliente.SelectedValue);
            }
            else
            {
                _filtro = _filtro + "1";
                _cod_prove = 0;
            }
            if (ddlMoneda.SelectedValue == "0")
                _filtro = _filtro + "1";
            else
            {
                _filtro = _filtro + "2";
                _moneda = ddlMoneda.SelectedValue;
            }
            if (TxtFecRegDesde.Text.Length > 0)
            {
                fregdesde = Convert.ToDateTime(TxtFecRegDesde.Text);
                _filtro = _filtro + "2";
            }
            else            
                _filtro = _filtro + "1";
            if (TxtFecRegHasta.Text.Length > 0)
            {
                freghasta = Convert.ToDateTime(TxtFecRegHasta.Text);
                _filtro = _filtro + "2";
            }
            else
                _filtro = _filtro + "1";
            if (TxtFecPagDesde.Text.Length > 0)
            {
                fultdesde= Convert.ToDateTime(TxtFecPagDesde.Text);
                _filtro = _filtro + "2";
            }
            else
                _filtro = _filtro + "1";
            if (TxtFecPagHasta.Text.Length > 0)
            {
                fulthasta = Convert.ToDateTime(TxtFecPagHasta.Text);
                _filtro = _filtro + "2";
            }
            else
                _filtro = _filtro + "1";
             if (TxtFecVenDesde.Text.Length > 0)
            {
                fvendesde = Convert.ToDateTime(TxtFecVenDesde.Text);
                _filtro = _filtro + "2";
            }
            else
                _filtro = _filtro + "1";
            if (TxtFecVenHasta.Text.Length > 0)
            {
                fvenhasta = Convert.ToDateTime(TxtFecVenHasta.Text);
                _filtro = _filtro + "2";
            }
            else
                _filtro = _filtro + "1";
            if (dllEstado.SelectedValue == "A")
            {
                _filtro = _filtro + "1";
            }
            else
            {
                _filtro = _filtro + "2";
                _estado = dllEstado.SelectedValue.ToString();
            }
            if (ddlListaVendedores.SelectedValue == "0")
                _filtro = _filtro + "1";
            else
            {
                _filtro = _filtro + "2";
                _cod_tra = Convert.ToInt32( ddlListaVendedores.SelectedValue.ToString());
            }
            if (ddlTipPago.SelectedValue == "Ambos")
                _filtro = _filtro + "1";
            else
            {
                _filtro = _filtro + "2";
                _tip_pago = ddlTipPago.SelectedValue.ToString();
            }
            if (ddlTipoDocu.SelectedValue == "000")
                _filtro = _filtro + "1";
            else
            {
                _filtro = _filtro + "2";
                _tip_doc = Convert.ToInt32( ddlTipoDocu.SelectedValue.ToString());
            }
            dtRegistro = formulario.GetList_factura(
                _filtro, "V", _cod_prove,
                _moneda, fregdesde, freghasta, 
                fultdesde, fulthasta, fvendesde,
                fvenhasta, _estado, _cod_tra,  
                _tip_pago,  _tip_doc);
            dgvLista.DataSource = dtRegistro;
            dgvLista.DataBind();
            dgvLista.Columns[2].Visible = false;
            dgvLista.Columns[3].Visible = false;
            dgvLista.Columns[7].Visible = false;
            dgvLista.Columns[8].Visible = false;
            dgvLista.Columns[12].Visible = false;
            dgvLista.Columns[15].Visible = false;
            dgvLista.Columns[17].Visible = false;
            CalcularSumatorias();
            pntotales.Visible = true;

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {

        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region FUNCIONES 
        private void IniciarCampos()
        {
            btnNuevo.Visible = false;
            btnCancelar.Visible = true;
            btnEditar.Visible = true;
            btnAnular.Visible = false;
            btnImprimir.Visible = true;
            btnGuardar.Visible = false;
            btnProcesar.Visible = true;

            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnImprimir, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnProcesar, true);

            pntotales.Visible = false;

            CargarClientes();
            cargarMoneda();
            CargarTipoCambio();
            CargarTipDocumento();
            CargarVendedores();
            //rbtPendiente.Checked = true;

           
        }

        private void CargarTipDocumento()
        {
            clsAtributos Atributos = new clsAtributos();
            ddlTipoDocu.DataSource = Atributos.ListAtributos(3);
            ddlTipoDocu.DataBind();
            ddlTipoDocu.Items.Insert(0, new ListItem("", "000"));

        }
        private void CargarClientes()
        {
            clsClientes lstClientes = new clsClientes();

            ddlCliente.DataSource = lstClientes.GetAll();
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("", "000"));

            lstClientes = null;
        }
        private void cargarMoneda()
        {
            clsAtributos Atributos = new clsAtributos();

            ddlMoneda.DataSource = Atributos.ListAtributos(1);
            ddlMoneda.DataBind();
            ddlMoneda.Items.Insert(0, new ListItem("Ambas", "0"));
        }
        public void CargarTipoCambio()
        {
            clsTipoCambios oTipCamp = new clsTipoCambios();

            var oTip = oTipCamp.GetLastTipoCambio("USD");
            if (oTip != null)            
                lbltc.Text = oTip.tcmCambio.ToString();
            
        }
        public void CargarVendedores()
        {
            clsPersonal lstPersonal = new clsPersonal();
            var lstVendedores = lstPersonal.GetPersonalPorTipo(2);

            ddlListaVendedores.DataSource = lstVendedores;
            ddlListaVendedores.DataBind();
            ddlListaVendedores.Items.Insert(0, new ListItem("Todos", "0"));



        }
        private void CalcularSumatorias(){
            Double totalComprasol, totalCompradol,
                totalSalsol, totalSaldol, 
                totalsalpagsol, totalsalpagdol,
                totalsalvensol, totalsalvendol,
                tip_cambio;
            DateTime fechoy = DateTime.Today, fecven;

            totalComprasol= totalCompradol =
            totalSalsol= totalSaldol= 
            totalsalpagsol= totalsalpagdol= totalsalvensol= totalsalvendol= 0.0;
            tip_cambio=Convert.ToDouble( lbltc.Text.ToString());
            //
            
            foreach (GridViewRow row in dgvLista.Rows){
                if(row.Cells[17].Text != "A")
                {
                if (row.Cells[16].Text == "PEN")
                {
                    totalComprasol = totalComprasol + Convert.ToDouble(row.Cells[11].Text);
                    totalSalsol = totalSalsol + Convert.ToDouble(row.Cells[13].Text);
                    totalsalpagsol = totalsalpagsol + Convert.ToDouble(row.Cells[14].Text);
                    if(row.Cells[6].Text.ToString().Length==10){
                        fecven = Convert.ToDateTime(row.Cells[6].Text.ToString());
                        if (fecven < fechoy) {
                            totalsalvensol = totalsalvensol + Convert.ToDouble(row.Cells[14].Text);
                        }
                    }
                    
                }
                else
                {
                    totalCompradol = totalCompradol + Convert.ToDouble(row.Cells[11].Text);
                    totalSaldol = totalSaldol + Convert.ToDouble(row.Cells[13].Text);
                    totalsalpagdol = totalsalpagdol + Convert.ToDouble(row.Cells[14].Text);
                    if (row.Cells[6].Text.ToString().Length ==10)
                    {
                        fecven = Convert.ToDateTime(row.Cells[6].Text.ToString());
                        if (fecven < fechoy)
                        {
                            totalsalvendol = totalsalvendol + Convert.ToDouble(row.Cells[14].Text);
                        }
                    }
                }
            }
            lblcompras.Text = totalComprasol.ToString("N");
            lblcomprad.Text = totalCompradol.ToString("N");
            lblcompra.Text =( totalComprasol+ (totalCompradol*tip_cambio)).ToString("N");
            //---------------------------//
            lblsaltots.Text = totalSalsol.ToString("N");
            lblsaltotd.Text = totalSaldol.ToString("N");
            lblsaltot.Text = (totalSalsol + (totalSaldol * tip_cambio)).ToString("N");
            //---------------------------//
            lblsalxvens.Text = totalsalpagsol.ToString("N");
            lblsalxvend.Text = totalsalpagdol.ToString("N");
            lblsalxven.Text = (totalsalpagsol + (totalsalpagdol * tip_cambio)).ToString("N");
            //---------------------------//
            lblsalvens.Text = totalsalvensol.ToString("N");
            lblsalvend.Text = totalsalvendol.ToString("N");
            lblsalven.Text = (totalsalvensol + (totalsalvendol * tip_cambio)).ToString("N");
            //---------------------------//
            }

        }
        #endregion
    }
}
