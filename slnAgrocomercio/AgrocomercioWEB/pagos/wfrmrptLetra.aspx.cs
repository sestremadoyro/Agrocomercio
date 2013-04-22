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
    public partial class wfrmrptLetra : BasePage
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
                            dgvFacPend.SelectedIndex = _selectedIndex;
                            HabilitarBtn(btnNuevo, false);    
                            lblEstado.Value = "EMPTY";
                        break;
                        case ("DGVLET_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());
                            dgvLetReg.SelectedIndex = _selectedIndex;
                            HabilitarBtn(btnEditar, true);
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
        protected void rbtPendiente_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbtPendiente.Checked == true)
            {
                pnFilPend.Visible = true;
                pnFilReg.Visible = false;                
            }
            else {
                DateTime Fechafin = DateTime.Today;
                Fechafin= Fechafin.AddMonths(1);
                Fechafin = new DateTime(Fechafin.Year, Fechafin.Month, 1);
                Fechafin = Fechafin.AddDays(-1);

                TxtFecPAgo.Text = Fechafin.ToString("yyyy-MM-dd");
                
                pnFilPend.Visible = false;
                pnFilReg.Visible = true;
            }
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnEditar, false);
            dgvLetReg.DataBind();
            dgvFacPend.DataBind();
            
        }
       
        protected void dgvFacPend_RowDataBound(object sender, GridViewRowEventArgs e) {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvFacPend.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvFacPendClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvFacPendClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
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
        protected void dgvLetReg_RowDataBound(object sender, GridViewRowEventArgs e) {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvLetReg.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvLetRegClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvLetRegClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
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
       
        protected void dgvListFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvFacPend.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvListFactClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvListFactClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
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
                hdcodper.Value = nPrvCod.ToString();
                //lblPersona.Text = Proveedor.PrvRazon.ToString();
                //hdcodper.Value = nPrvCod.ToString();
                //txtDocCli.Text = Proveedor.PrvNumDoc.ToString();
                //txtDireccion.Text = Proveedor.PrvDireccion.ToString();
                //txtTelefono.Text = Proveedor.PrvTelefono.ToString();

                lstProveedores = null;
                Proveedor = null;
                ///////////////////////////////////
                HabilitarBtn(btnNuevo, false);

            }
        }
        
        protected void ddlMoneda_new_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            String _moneda = ddlMoneda_new.SelectedValue;
            dgvFacPend.Columns[8].Visible = true;
            dgvListFact.Columns[2].Visible = true;
            dgvNotas.Columns[3].Visible = true;
            dgvNotas.Columns[5].Visible = true;

            int nPrvCod = int.Parse(dgvFacPend.Rows[dgvFacPend.SelectedIndex].Cells[8].Text);
            hdcodper.Value = nPrvCod.ToString();
            lblPersona.Text = (dgvFacPend.Rows[dgvFacPend.SelectedIndex].Cells[6].Text);

            clsLetra lstLetras = new clsLetra();
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            lblNroDocumento.Text = Rellenartexto("0", (lstLetras.MaxOpeCod()).ToString(), 8, 0);

            //////////////////////CARGA FACTURAS PENDIENTES
            clsFac_pnd_let Form = new clsFac_pnd_let();
            DataTable dtPendientes;

            //btgenCuotas.Visible = false;

            dtPendientes = Form.GetList(nPrvCod, "C", _moneda);
            dgvListFact.DataSource = dtPendientes;
            dgvListFact.DataBind();

            //////////////////////CARGA NOTAS DISPONIBLES
            clsListNotas lstOperaciones = new clsListNotas();
            DataTable dtNotasPen;
            dtNotasPen = lstOperaciones.GetLista("1222111", "0", "N", _moneda, nPrvCod, DateTime.Today, DateTime.Today, "C",0);
            dgvNotas.DataSource = dtNotasPen;
            dgvNotas.DataBind();

            dgvFacPend.Columns[8].Visible = false;

            dgvListFact.Columns[2].Visible = false;

            dgvNotas.Columns[3].Visible = false;
            dgvNotas.Columns[5].Visible = false;
        }
        
        protected void grdchknota_CheckedChanged(object sender, EventArgs e)
        {
            string _cError = "";
            Double total_acum = 0.0;
            int multiplica;
            
            
            try
            {
                dgvNotas.Columns[3].Visible = true;

                foreach (GridViewRow row in dgvNotas.Rows)
                {
                    //CheckBox chk_Publicar = (CheckBox)row.Cells[1].Controls[0];
                    CheckBox check = row.FindControl("grdchknota") as CheckBox;
                    if (check.Checked)
                    {
                        if (row.Cells[3].Text.Substring(0, 1) == "1")
                            multiplica = 1;
                        else
                            multiplica = -1;

                        total_acum = total_acum + (Convert.ToDouble(row.Cells[7].Text)*multiplica);
                    }
                }
                lbresta.Text = (Math.Round(total_acum,2)).ToString();
                CalculartotalPago();

                dgvNotas.Columns[3].Visible = false;
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }
        }
        protected void grdchk_CheckedChanged(object sender, EventArgs e)
        {
            string _cError = "";
            Double total_acum = 0.0;
            try
            {
                foreach (GridViewRow row in dgvListFact.Rows)
                {
                    //CheckBox chk_Publicar = (CheckBox)row.Cells[1].Controls[0];
                    CheckBox check = row.FindControl("CheckBox1") as CheckBox;
                    if (check.Checked)
                    {

                        total_acum = total_acum + Convert.ToDouble(row.Cells[5].Text);
                    }
                }
                lbSuma.Text = total_acum.ToString();
                CalculartotalPago();
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }
        }

        protected void txtnumCuotas_TextChange(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(txtNumCuotas.Text);
                if (a > 0 && lbpago.Text != "0.00")
                {
                    btgenCuotas.Visible = true;
                    btgenCuotas.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                txtNumCuotas.Text = "";
                var res = ex.Message;
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
       
        #region FUNCIONES DE LOS BOTONES
        protected void btnNuevo_Click(object sender, EventArgs e) {
            pnBusqueda.Visible = false;
            pnNuevo.Visible = true;
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnNuevo, false);
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            cargarMoneda_new();

            dgvFacPend.Columns[8].Visible =true;
            dgvListFact.Columns[2].Visible = true;
            dgvNotas.Columns[3].Visible = true;
            dgvNotas.Columns[5].Visible = true;
            ddlMoneda_new.SelectedValue = dgvFacPend.Rows[dgvFacPend.SelectedIndex].Cells[9].Text;
             String _moneda = ddlMoneda_new.SelectedValue;
             int nPrvCod = int.Parse(dgvFacPend.Rows[dgvFacPend.SelectedIndex].Cells[8].Text);
             hdcodper.Value= nPrvCod.ToString() ;
             lblPersona.Text = (dgvFacPend.Rows[dgvFacPend.SelectedIndex].Cells[6].Text);

            clsLetra lstLetras = new clsLetra();
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            lblNroDocumento.Text = Rellenartexto("0", (lstLetras.MaxOpeCod()).ToString(), 8, 0);
                     
            //////////////////////CARGA FACTURAS PENDIENTES
            clsFac_pnd_let Form = new clsFac_pnd_let();
            DataTable dtPendientes;
            
            //btgenCuotas.Visible = false;

            dtPendientes = Form.GetList(nPrvCod, "C", _moneda);
            dgvListFact.DataSource = dtPendientes;
            dgvListFact.DataBind();
                        
            //////////////////////CARGA NOTAS DISPONIBLES
            clsListNotas lstOperaciones = new clsListNotas();
            DataTable dtNotasPen;
            dtNotasPen = lstOperaciones.GetLista("1222111", "0", "N", _moneda, nPrvCod, DateTime.Today, DateTime.Today, "C",0);
            dgvNotas.DataSource = dtNotasPen;
            dgvNotas.DataBind();

            dgvFacPend.Columns[8].Visible =false;

            dgvListFact.Columns[2].Visible = false;
            
            dgvNotas.Columns[3].Visible = false;
            dgvNotas.Columns[5].Visible = false;
            
            lblProceso.Value = "Nuevo";
            HabilitarBtn(btnProcesar, false);

        }
        protected void btnEditar_Click(object sender, EventArgs e){
            lblPaso.Value="Detalle";
            cargarMoneda_new();

            pnNuevo.Visible = true;
            dgvLetReg.Columns[15].Visible = true;

            int cod_letra = int.Parse(dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[15].Text);
            txtFecha.Text = dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[6].Text;
            lblPersona.Text = dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[5].Text;
            ddlMoneda_new.SelectedValue = dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[10].Text;
            txtNumCuotas.Text = dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[7].Text;
            lblNroDocumento.Text = dgvLetReg.Rows[dgvLetReg.SelectedIndex].Cells[3].Text;
            //////////////////////CARGA FACTURAS
            clsfac_x_letra formFac = new clsfac_x_letra();
            DataTable dtPendientes;

            dtPendientes = formFac.GetList(cod_letra);
            dgvListFact.DataSource = dtPendientes;
            dgvListFact.DataBind();
            
            //////////////////////CARGA NOTAS 
            clsListNotas lstOperaciones = new clsListNotas();
            DataTable dtNotasPen;
            dtNotasPen = lstOperaciones.GetLista("1111112", "0", "N", "", 0, DateTime.Today, DateTime.Today, "C", cod_letra);
            dgvNotas.DataSource = dtNotasPen;
            dgvNotas.DataBind();
            ////////////////////CALCULOS
            Double total_acum = 0.0;
            int multiplica;

            foreach (GridViewRow row in dgvListFact.Rows)
            {
                //CheckBox chk_Publicar = (CheckBox)row.Cells[1].Controls[0];
                CheckBox check = row.FindControl("CheckBox1") as CheckBox;
                check.Checked = true;
                total_acum = total_acum + Convert.ToDouble(row.Cells[5].Text);
            }
            lbSuma.Text = total_acum.ToString();
            //////////////////////////////
            dgvNotas.Columns[3].Visible = true;
            total_acum = 0.0;
            /////////////////////////////
            foreach (GridViewRow rown in dgvNotas.Rows)
            {

                CheckBox check = rown.FindControl("grdchknota") as CheckBox;
                check.Checked = true;

                if (rown.Cells[3].Text.Substring(0, 1) == "1")
                    multiplica = 1;
                else
                    multiplica = -1;

                total_acum = total_acum + (Convert.ToDouble(rown.Cells[7].Text) * multiplica);
                
            }
            lbresta.Text = (Math.Round(total_acum, 2)).ToString();
            CalculartotalPago();
            ///////////////DETALLE DE LETRAS
            clsdetletra formDet = new clsdetletra();
            DataTable dtDetalle;

            dtDetalle = formDet.GetList(cod_letra);
            int valor = dtDetalle.Rows.Count;
            dgvcuotas.DataSource = dtDetalle;
            dgvcuotas.DataBind();
            int cont = 0;
            foreach (GridViewRow row in dgvcuotas.Rows)
            {
                row.Cells[0].Text = dtDetalle.Rows[cont][1].ToString();
                TextBox txtFecVen = row.FindControl("txtFecVen") as TextBox;
                txtFecVen.Enabled = false;
                txtFecVen.Text = Convert.ToDateTime(dtDetalle.Rows[cont][6]).ToString("yyyy-MM-dd");
                TextBox num_let = row.FindControl("num_let") as TextBox;
                num_let.Text = dtDetalle.Rows[cont][10].ToString();
                num_let.Enabled = false;
                TextBox cod_unic = row.FindControl("cod_unic") as TextBox;
                cod_unic.Text = dtDetalle.Rows[cont][2].ToString();
                cod_unic.Enabled = false;
                TextBox monto = row.FindControl("monto") as TextBox;
                monto.Text = dtDetalle.Rows[cont][3].ToString();
                monto.Enabled = false;
                cont++;
            }


            dgvNotas.Columns[3].Visible = false;

            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnProcesar, true);

            HabilitarBtn(btnImprimir, true);
            pnNuevo.Enabled = false;
            btgenCuotas.Visible = false;
            dgvLetReg.Columns[15].Visible = false;
            lblPaso.Value = "Detalle";
        }
        protected void btnAnular_Click(object sender, EventArgs e) { }
        protected void btnCancelar_Click(object sender, EventArgs e){
            if(lblPaso.Value == "Generacion"){
                pnaddfactura.Enabled = true;
                pnNotas.Enabled = true;                
                pnCuotas.Visible = false;
                HabilitarBtn(btnGuardar, false);
                ddlMoneda_new.Enabled = true;
                lblPaso.Value = "Nuevo";
            }
            else{                
                IniciarCampos();
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e){
            String cfecLetra;
            cfecLetra = txtFecha.Text;
            int nPrvCod = Convert.ToInt32(hdcodper.Value);

            int cuotas = int.Parse(txtNumCuotas.Text);
            int icodigo, idetcodigo;
            Double monto_total = Double.Parse(lbpago.Text);
            Double monto_nota = Double.Parse(lbresta.Text);
            String moneda;
            moneda = ddlMoneda_new.SelectedValue;
            try
            {
                clsLetra _Letra = new clsLetra();
                letra obj = new letra();
                icodigo = Convert.ToInt32(_Letra.MaxOpeCod() + 1);
                obj.icodigo = icodigo;
                obj.itotcuota = cuotas;
                obj.nmontocuota = Convert.ToDecimal(monto_total);
                obj.cmoneda = moneda;
                obj.nintpag = Convert.ToDecimal(0.00);
                obj.iestado = "1";
                obj.dfeccreacion = Convert.ToDateTime(cfecLetra);
                obj.dfecmod = DateTime.Today;
                obj.ctippago = "L";
                obj.nmntnota = Convert.ToDecimal(monto_nota);
                _Letra.fnletraInsertar(obj);
                clsdetletra _detletra = new clsdetletra();

                foreach (GridViewRow row in dgvcuotas.Rows)
                {
                    det_letra _detobj = new det_letra();
                    _detobj.icodletra = icodigo;
                    _detobj.inumletra = Convert.ToInt32(row.Cells[0].Text);
                    TextBox num_let = row.FindControl("num_let") as TextBox;
                    _detobj.cnumletra = num_let.Text;
                    TextBox ccodletra = row.FindControl("cod_unic") as TextBox;
                    _detobj.ccodletra = ccodletra.Text;
                    TextBox monto = row.FindControl("monto") as TextBox;
                    _detobj.nmonto = Convert.ToDecimal(monto.Text);
                    _detobj.cestado = "1";
                    _detobj.ninteres = Convert.ToDecimal(0.00);
                    TextBox txtFecVen = row.FindControl("txtFecVen") as TextBox;
                    _detobj.dfecvenc = Convert.ToDateTime(txtFecVen.Text);
                    _detobj.dfecmod = DateTime.Today;
                    idetcodigo = Convert.ToInt32(_detletra.Maxdetletra_cod()) + 1;
                    _detobj.idetletra = idetcodigo;
                    _detletra.fndet_letraInsertar(_detobj);
                }
                clsDocumenOperacion _docobj = new clsDocumenOperacion();
                DocumenOperacion _docope = new DocumenOperacion();
                Int64 cod_fact;
                foreach (GridViewRow row in dgvListFact.Rows)
                {

                    CheckBox check = row.FindControl("CheckBox1") as CheckBox;
                    if (check.Checked)
                    {
                        cod_fact = Convert.ToInt64(row.Cells[2].Text);
                        //_docope = _docobj.GetListOperaciones(cod_fact );
                        _docope = _docobj.GetDocumenOperaciona(cod_fact);

                        _docope.icodletra = icodigo;
                        _docobj.fnDocOpeUpdate(_docope);
                    }
                }
                Int32 cod_not;
                clsOpeNotas _formnot = new clsOpeNotas();
                Notas _docopenot = new Notas();
                rel_notfactura _relacion = new rel_notfactura();
                clsrel_notfactura _formrel = new clsrel_notfactura();
                dgvNotas.Columns[3].Visible = true;
                dgvNotas.Columns[5].Visible = true;
                foreach (GridViewRow row in dgvNotas.Rows)
                {
                    

                    CheckBox check = row.FindControl("grdchknota") as CheckBox;
                    if (check.Checked)
                    {
                        cod_not = Convert.ToInt32(row.Cells[5].Text);
                        //clsOpeNotas
                        _docopenot = _formnot.GetNota(cod_not);
                        _docopenot.cestadoNota = "C";
                        _docopenot.nmntutilizado = _docopenot.nmontoNota;
                        _formnot.fnNotaUpdate(_docopenot);

                        _relacion.idtpcod = icodigo;
                        _relacion.inota = cod_not;
                        _relacion.nmonto = _docopenot.nmontoNota;
                        _relacion.dfecmod = DateTime.Today;
                        //_relacion .iusrcrc
                        _relacion.irelnotfac = Convert.ToInt32(_formrel.MaxOpeCod()) + 1;
                        _formrel.fnNotaInsert(_relacion);
                    }
                }
                dgvNotas.Columns[3].Visible = false;
                dgvNotas.Columns[5].Visible = false;
                IniciarCampos();

                obj = _Letra.GetLetra(icodigo);
                obj.iestado = "1";
                _Letra.fnletraupdate(obj);
                pnBusqueda.Visible = true;
                pnNuevo.Visible = false;
                IniciarCampos();
                btnProcesar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnProcesar_Click(object sender, EventArgs e) {
            if (rbtPendiente.Checked == true)
            {
                dgvFacPend.Columns[8].Visible = true;
                HabilitarBtn(btnNuevo, false);
                String _filtro = "", _factura = "";
                int _cod_prov;
                DateTime _fecha_factura;
                clsFac_pnd_let form = new clsFac_pnd_let();
                DataTable dtPendientes;

                //filtro filtro proveedor factura fecha_factura valor
                if ((txtProveedor.Text.ToString().Length) == 0)
                {
                    _filtro = "1";
                    _cod_prov = 0;
                }
                else
                {
                    _filtro = "2";
                    _cod_prov = int.Parse(ddlProveedor.SelectedValue);
                }
                if ((txtNumDoc.Text.ToString().Length) == 0)
                {
                    _filtro = _filtro + "1";

                }
                else
                {
                    _filtro = _filtro + "2";
                    _factura = txtNumDoc.Text;
                }
                if ((txtflfecpro.Text.ToString().Length) == 0)
                {
                    _filtro = _filtro + "1";
                    _fecha_factura = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd"));

                }
                else
                {
                    _filtro = _filtro + "2";
                    _fecha_factura = Convert.ToDateTime(txtflfecpro.Text);
                }
                if (ddlMoneda.SelectedValue == "0")
                    _filtro = _filtro + "1";
                else
                    _filtro = _filtro + "2";
                dtPendientes = form.GetLista(_filtro+"1", _cod_prov, _factura, _fecha_factura, ddlMoneda.SelectedValue,0, "C");
                dgvFacPend.DataSource = dtPendientes;
                dgvFacPend.DataBind();
                //dgvFacPend.Columns[8].Visible = false;
            }
            else {
                dgvLetReg.Columns[15].Visible = true;
                String _filtro = "", _estado="", _codletra="";
                int _cod_prove;
                DateTime fecMax;
                clsList_letra formll = new clsList_letra();
                DataTable dtRegistro;
                if (txtNumLetra.Text.Length > 0)
                {
                    _codletra = txtNumLetra.Text;
                    _filtro = "2";
                }
                else {
                    _filtro = "1";
                   
                }
                if (TxtFecPAgo.Text.Length > 0)
                {
                    fecMax = Convert.ToDateTime(TxtFecPAgo.Text);
                    _filtro = _filtro + "2";
                }
                else {
                    _filtro = _filtro + "1";
                    fecMax = DateTime.Today;
                }
                if (dllEstado.SelectedValue == "A")
                {
                    _filtro = _filtro + "1";
                }
                else {
                    _filtro = _filtro + "2";
                    _estado = dllEstado.SelectedValue.ToString();
                }
                if (ddlMoneda.SelectedValue == "0")
                    _filtro = _filtro + "1";
                else
                    _filtro = _filtro + "2";
                _filtro = _filtro + "1"; //no tiene vendedor
                if (txtProveedor.Text.Length > 0)
                {
                    _filtro = _filtro + "2";
                    _cod_prove = Convert.ToInt32(ddlProveedor.SelectedValue);
                }
                else
                {
                    _filtro = _filtro + "1";
                    _cod_prove = 0;
                }
                dtRegistro = formll.GetList_letra(_filtro, _codletra, fecMax, _estado, ddlMoneda.SelectedValue, 0,_cod_prove, "C");
                dgvLetReg.DataSource = dtRegistro;
                dgvLetReg.DataBind();
                dgvLetReg.Columns[15].Visible = false;
            }
        }
        protected void btnImprimir_Click(object sender, EventArgs e) { }
        protected void btnAgregar_Click(object sender, EventArgs e) { }
        protected void btnEliminar_Click(object sender, EventArgs e) { }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtCuotas;
            DataRow newRow;
            dtCuotas = CreatDTListaCuotas();
            double acum = 0.0;
            int cont = 1;
            int num_cuotas = Convert.ToInt16(this.txtNumCuotas.Text);
            double monto_cuota = Convert.ToDouble(this.lbpago.Text);
            monto_cuota = Math.Round(monto_cuota / num_cuotas, 2);

            for (int i = dtCuotas.Rows.Count; i < num_cuotas; i++)
            {
                newRow = dtCuotas.NewRow();

                for (int j = 0; j < dtCuotas.Columns.Count; j++)
                {
                    if (j == 0)
                        newRow[j] = i + 1;
                    newRow[j] = DBNull.Value;
                }
                dtCuotas.Rows.Add(newRow);

            }
            dgvcuotas.DataSource = dtCuotas;
            dgvcuotas.DataBind();
            DateTime ahora = DateTime.Today;
            ahora = ahora.AddMonths(1);
            foreach (GridViewRow row in dgvcuotas.Rows)
            {
                row.Cells[0].Text = cont.ToString();
                TextBox txtFecVen = row.FindControl("txtFecVen") as TextBox;
                txtFecVen.Text = ahora.ToString("yyyy-MM-dd");

                TextBox num_let = row.FindControl("num_let") as TextBox;
                num_let.Text = " -";
                TextBox cod_unic = row.FindControl("cod_unic") as TextBox;
                cod_unic.Text = " -";
                TextBox monto = row.FindControl("monto") as TextBox;
                monto.Text = monto_cuota.ToString();
                ahora = ahora.AddMonths(1);
                acum = acum + monto_cuota;
                if (cont == num_cuotas)
                {
                    if (acum != Convert.ToDouble(this.lbpago.Text))
                    {
                        monto.Text = (monto_cuota + (Convert.ToDouble(this.lbpago.Text) - acum)).ToString();
                    }
                }
                cont++;
            }
            pnaddfactura.Enabled = false;
            pnNotas.Enabled = false;
            lblPaso.Value = "Generacion";
            pnCuotas.Visible = true;
            HabilitarBtn(btnGuardar, false);
            ddlMoneda_new.Enabled = false;
            
        }
        #endregion
        #region FUNCIONES DEL
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
                        
            CargarProveedores();
            cargarMoneda();

            rbtPendiente.Checked = true;
           
            pnNuevo.Visible = false;
            pnFilPend.Visible = true;
            pnFilReg.Visible = false;
            pnBusqueda.Visible = true;
            
            dgvFacPend.Columns[8].Visible = true;
            lblPaso.Value = "";
            ddlMoneda_new.Enabled = true;
        }
              
        private void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();

            ddlProveedor.DataSource = lstProveedores.GetAll();
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("", "000"));

            lstProveedores = null;
        }
        private void cargarMoneda_new()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlMoneda_new.DataSource = Atributos.ListAtributos(1);
            ddlMoneda_new.DataBind();
        }
        private void cargarMoneda()
        {
            clsAtributos Atributos = new clsAtributos();

            ddlMoneda.DataSource = Atributos.ListAtributos(1);
            ddlMoneda.DataBind();
            ddlMoneda.Items.Insert(0, new ListItem("Ambas", "0"));
        }
        protected String Rellenartexto(String _valor, String _Texto, int _tam, int _pos)
        {
            String cadena = _Texto;
            int tam = _Texto.Length;
            if (tam > _tam)
                return _Texto;
            else
            {
                for (int i = 0; i < _tam - tam; i++)
                {
                    if (_pos == 0)
                        cadena = _valor + cadena;
                    else
                        cadena = cadena + _valor;
                }

                return cadena;
            }

        }
        protected DataTable CreatDTListaCuotas()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("ArtCod", typeof(int)));
            return dtResult;
        }
        
        protected void CalculartotalPago()
        {
            Double debe, haber;
            debe = Convert.ToDouble(lbSuma.Text);
            haber = Convert.ToDouble(lbresta.Text);
            lbpago.Text = (debe + haber).ToString();

            try
            {
                int a = int.Parse(txtNumCuotas.Text);
                if (a > 0 && Convert.ToDouble(lbpago.Text )>0)
                {
                    btgenCuotas.Visible = true;
                    btgenCuotas.Enabled = true;
                }
                else {                    
                    btgenCuotas.Visible = false;
                    btgenCuotas.Enabled = false;
                }
            }
            catch
            {
                txtNumCuotas.Text = "";
                btgenCuotas.Visible = false;
                btgenCuotas.Enabled = false;
            }
        }
        #endregion
    }
}