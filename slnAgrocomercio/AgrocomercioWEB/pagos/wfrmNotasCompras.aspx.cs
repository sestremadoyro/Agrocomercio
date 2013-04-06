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
    public partial class wfrmNotasCompras : BasePage
    {
        public String _click = "";
        
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

                            if (dgvNotas.Rows[_selectedIndex].Cells[11].Text.Trim() != "Utilizada")
                            {
                                dgvNotas.SelectedIndex = _selectedIndex;
                                HabilitarBtn(btnEditar, true);
                                btnEditar.Visible = true;
                            }
                            else
                            {
                                dgvNotas.SelectedIndex = -1;
                                HabilitarBtn(btnEditar, false);
                            }
                            lblEstado.Value = "EMPTY";
                        break;
                        case ("PRV_SELECT"):
                            _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                            ddlProveedor.SelectedValue = _selectedValue;
                            ddlProveedor_SelectedIndexChanged(sender, e);
                            
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
        
        #region FUNCIONES DEL FORMULARIO

        protected void btnNuevo_Click(object sender, EventArgs e) {
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnGuardar, true);
            HabilitarBtn(btnProcesar, false);

            pnBusqueda.Visible = false;
            pnNuevo.Visible = true;

            CargarTipos_new();
            CargarMonedas_new();
            txtfecpro.Text = DateTime.Today.ToString("yyyy-MM-dd");
            lblProceso.Value = "Nuevo";
        }
        protected void btnEditar_Click(object sender, EventArgs e) {     
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnGuardar, true);
            HabilitarBtn(btnProcesar, false);
            pnBusqueda.Visible = false;
            pnNuevo.Visible = true;

            CargarTipos_new();
            CargarMonedas_new();
            lblProceso.Value = "Editar";

            dgvNotas.Columns[3].Visible = true;
            dgvNotas.Columns[13].Visible = true;
            dgvNotas.Columns[15].Visible = true;
            
            int nCodNota = int.Parse(dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[3].Text);
            dgvNotas.Columns[3].Visible = false;
            lblOpeCodigo.Value = nCodNota.ToString();

            lblPersona.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[7].Text;
            hdcodper.Value = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[6].Text;
            txtfecpro.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[5].Text;
            ddlTipNot_new.SelectedValue = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[13].Text;
            txtCodNota.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[4].Text;
            ddlMoneda_new.SelectedValue = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[12].Text;
            txtmonto.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[8].Text;
            txtmontoutl.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[10].Text;
            txtobservaciones.Text = dgvNotas.Rows[dgvNotas.SelectedIndex].Cells[15].Text;
            ddlMoneda_new.Enabled = false;
            dgvNotas.Columns[3].Visible = false;
            dgvNotas.Columns[13].Visible = false;
            dgvNotas.Columns[15].Visible = false;

        }
        protected void btnAnular_Click(object sender, EventArgs e) { }
        protected void btnCancelar_Click(object sender, EventArgs e) {
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnProcesar, true);
            pnBusqueda.Visible = true;
            pnNuevo.Visible = false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e) {
        clsOpeNotas form = new clsOpeNotas();
            Notas obj = new Notas();
            if (lblProceso.Value == Convert.ToString("Nuevo"))
            {
                if (int.Parse(hdcodper.Value) == 0 || txtCodNota.Text.Length == 0
                   || txtmonto.Text.Length == 0)
                {
                    lbmensaje.Visible = true;
                }
                else {
                    lbmensaje.Visible = false;
                    obj.inota = Convert.ToInt32(form.MaxOpeCod()) + 1;
                    obj.dfecreg = Convert.ToDateTime(txtfecpro.Text);
                    obj.ccodnota = txtCodNota.Text;
                    obj.iprvcod = int.Parse(hdcodper.Value);
                    obj.nmontoNota = Convert.ToDecimal(txtmonto.Text);
                    obj.cestadoNota = "N";
                    obj.nmntutilizado = Convert.ToDecimal(0.00);
                    obj.cobservaciones = txtobservaciones.Text;
                    obj.ctipo = "C";
                    obj.OpeMoneda = ddlMoneda_new.SelectedValue.Trim();
                    obj.ctipNota = ddlTipNot_new.SelectedValue.Trim();
                    form.fnNotaInsert(obj);
                    IniciarCampos();
                    ddlProveedor.SelectedValue = hdcodper.Value;
                    btnProcesar_Click(sender, e);
                    
                }
            }
            else{
                if ( txtCodNota.Text.Length == 0|| txtmonto.Text.Length == 0)
                {
                    lbmensaje.Visible = true;
                }
                else
                {
                    double monto= Convert.ToDouble(txtmonto.Text);
                    double mnt_util = Convert.ToDouble(txtmontoutl.Text);
                    if (mnt_util > monto)
                    {
                        lbmensaje.Text = "El monto a actualizar no puede ser menor al ya utilizado";
                        lbmensaje.Visible = true;

                    }
                    else
                    {
                        lbmensaje.Visible = false;
                        obj = form.GetNota(Convert.ToInt32(lblOpeCodigo.Value));
                        obj.dfecreg = Convert.ToDateTime(txtfecpro.Text);
                        obj.ccodnota = txtCodNota.Text;                       
                        obj.nmontoNota = Convert.ToDecimal(txtmonto.Text);
                        obj.cestadoNota = "N";                        
                        obj.cobservaciones = txtobservaciones.Text;
                        obj.ctipo = "C";                        
                        obj.ctipNota = ddlTipNot_new.SelectedValue.Trim();
                        form.fnNotaUpdate(obj);
                        IniciarCampos();

                        dgvNotas.Columns[3].Visible = false;
                        dgvNotas.Columns[13].Visible = false;
                        dgvNotas.Columns[15].Visible = false;
                    }

                }

            }
            btnProcesar_Click(sender, e);
        }
        protected void btnProcesar_Click(object sender, EventArgs e) {
            dgvNotas.Columns[3].Visible = true;
            dgvNotas.Columns[13].Visible = true;
            dgvNotas.Columns[15].Visible = true;
            clsListNotas form = new clsListNotas();
            DataTable dtNotas;
            String _filtro;
            String _Tipo = "", _estado = "", _moneda = "";
            int _codProv;
            DateTime _desde = Convert.ToDateTime("1990-01-01"), _hasta = Convert.ToDateTime("3000-01-01"); ;
            //pnBusqueda :tipo estado Moneda Proveedor
            //1111 (String _bus, String _Tipo, String _estado, String _moneda, int _codProv, D)
            if ((ddlNotas.SelectedValue).ToString() == "0")            
                _filtro = "1";
            
            else {
                _filtro = "2";
                _Tipo = (ddlNotas.SelectedValue).ToString();
            }
            if ((ddlEstado.SelectedValue).ToString() == "0")
                _filtro = _filtro + "1";

            else
            {
                _filtro = _filtro+ "2";
                _estado = (ddlEstado.SelectedValue).ToString();
            }
            if ((ddlMoneda.SelectedValue).ToString() == "0")
                _filtro = _filtro + "1";

            else
            {
                _filtro = _filtro + "2";
                _moneda = (ddlMoneda.SelectedValue).ToString();
            }
            //if (int.Parse(ddlProveedor.SelectedValue) == 0)
            if ((txtProveedor.Text.ToString().Length) == 0)
            {
                _filtro = _filtro + "1";
                _codProv = 0;
            }
            else
            {
                _filtro = _filtro + "2";
                _codProv = int.Parse(ddlProveedor.SelectedValue);
            }
            int ifecdesde = txtFecDesde.Text.ToString().Length;
            int ifechasta = txtFechaHasta.Text.ToString().Length;
            if (ifecdesde > 0)
            {
                _filtro = _filtro + "2";
                _desde = Convert.ToDateTime(txtFecDesde.Text.ToString());
            }
            else            
                _filtro = _filtro + "1";
            if (ifechasta > 0)
            {
                _filtro = _filtro + "2";
                _hasta = Convert.ToDateTime(txtFechaHasta.Text.ToString());
            }
            else
                _filtro = _filtro + "1";
            dtNotas= form.GetLista(_filtro+"1", _Tipo, _estado, _moneda, _codProv, _desde, _hasta, "C", 0);
            dgvNotas.DataSource = dtNotas;
            dgvNotas.DataBind();
            HabilitarBtn(btnEditar, false);
            dgvNotas.Columns[3].Visible = false;
            dgvNotas.Columns[13].Visible = false;
            dgvNotas.Columns[15].Visible = false;

        }
        protected void btnImprimir_Click(object sender, EventArgs e) { }
        protected void btnAgregar_Click(object sender, EventArgs e) { }
        protected void btnEliminar_Click(object sender, EventArgs e) { }

        protected void dgvNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvNotas.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvNotasClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvNotasVentaClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nPrvCod = 0;
            nPrvCod = int.Parse(ddlProveedor.SelectedValue);

            if (nPrvCod == 0)
            {
                //txtDocCli.Text = "";
                //txtDireccion.Text = "";
                //txtTelefono.Text = "";
            }
            else
            {
                clsProveedores lstProveedores = new clsProveedores();
                Proveedores Proveedor = new Proveedores();

                Proveedor = lstProveedores.GetProveedor(nPrvCod);

                txtProveedor.Text = Proveedor.PrvRazon.ToString();
                lblPersona.Text = Proveedor.PrvRazon.ToString();
                hdcodper.Value = nPrvCod.ToString ();
                //txtDocCli.Text = Proveedor.PrvNumDoc.ToString();
                //txtDireccion.Text = Proveedor.PrvDireccion.ToString();
                //txtTelefono.Text = Proveedor.PrvTelefono.ToString();

                lstProveedores = null;
                Proveedor = null;
                ///////////////////////////////////
                HabilitarBtn(btnNuevo, true);

            }
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
                          CreateAutoCompleteItem("[NUEVO PROVEEDOR]", "999"));

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
        #region FUNCIONES DEL 
        private void IniciarCampos()
        {
            btnNuevo.Visible = true;
            btnCancelar.Visible = true;
            btnEditar.Visible = true;
            btnAnular.Visible = false;
            btnImprimir.Visible = true;
            btnGuardar.Visible = true;
            btnProcesar.Visible = true;

            HabilitarBtn (btnNuevo , false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn (btnAnular , false);
            HabilitarBtn(btnImprimir, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn (btnProcesar , true);
            
            pnBusqueda.Visible = true;
            pnNuevo.Visible = false;
            CargarTipos();
            CargarMonedas();
            CargarProveedores();
            //dgvNotas.Rows.Clear();
            dgvNotas.Columns[3].Visible = true;
            dgvNotas.Columns[13].Visible = true;
            dgvNotas.Columns[15].Visible = true;

            ddlMoneda_new.Enabled = true;

            lbmensaje.Text = "Es necesario llenar todos los campos solicitados (*)";
        }
        public void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();

            ddlProveedor.DataSource = lstProveedores.GetAll();
            ddlProveedor.DataBind();           
            ddlProveedor.Items.Insert(0, new ListItem("", "000"));

            lstProveedores = null;
        }
        public void CargarTipos()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlNotas.DataSource = Atributos.ListAtributos(9);
            ddlNotas.DataBind();
            ddlNotas.Items.Insert(0, new ListItem("Todos", "0"));
        }
        public void CargarTipos_new()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlTipNot_new.DataSource = Atributos.ListAtributos(9);
            ddlTipNot_new.DataBind();            
        }
        public void CargarMonedas()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlMoneda.DataSource = Atributos.ListAtributos(1);
            ddlMoneda.DataBind();
            ddlMoneda.Items.Insert(0, new ListItem("Ambas", "0"));
        }
        public void CargarMonedas_new()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlMoneda_new.DataSource = Atributos.ListAtributos(1);
            ddlMoneda_new.DataBind();
        }    
        #endregion
    }
}