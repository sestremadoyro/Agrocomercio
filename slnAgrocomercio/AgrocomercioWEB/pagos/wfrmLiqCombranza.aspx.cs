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
    public partial class wfrmLiqCombranza : BasePage
    {
        public String _click = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            String _selectedValue = "";

            if (Page.IsPostBack)
            {
                if (lblEstado.Value != "")
                {
                    switch (lblEstado.Value)
                    {
                        //case ("DGVLIS_SIMPLECLICK"):
                        //    _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());
                        //    dgvFacPend.SelectedIndex = _selectedIndex;
                        //    HabilitarBtn(btnNuevo, true);
                        //    lblEstado.Value = "EMPTY";
                        //    break;
                        //case ("DGVLET_SIMPLECLICK"):
                        //    _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());
                        //    dgvLetReg.SelectedIndex = _selectedIndex;
                        //    HabilitarBtn(btnEditar, true);
                        //    lblEstado.Value = "EMPTY";
                        //    break;
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
        #region FUNCIONES DEL FORMULARIO
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
                //HabilitarBtn(btnNuevo, true);
            }
        }
        protected void ddlListaVendedores_SelectedIndexChanged(object sender, EventArgs e) {
            int nVenCod = 0;
            nVenCod  = int.Parse(ddlListaVendedores.SelectedValue);
            if (nVenCod > 0)
            {
                HabilitarBtn(btnNuevo, true);
                clsPersonal form = new clsPersonal();
                Personal obj = new Personal();
                obj = form.GetPersonal(Convert.ToInt32(ddlListaVendedores.SelectedValue));
                lblnomVen.Text = obj.perNombres + " " + obj.perApellidoPat + " " + obj.perApellidoMat;
                lblOpeCodigo.Value = ddlListaVendedores.SelectedValue;
            }
            else
            {
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
        protected void txtmonto_TextChange(object sender, EventArgs e)
        {
            calcular();
        }
        protected void calcular()
        {
            double SumDol = 0.00, nSumDol = 0.00;
            double SumSoles = 0.00, nSumSol = 0.00;

            foreach (GridViewRow row in dgvPagoLetra.Rows)
            {
                TextBox txtMon = row.FindControl("txtmonto") as TextBox;
                TextBox txtFecPago = row.FindControl("txtFecPago") as TextBox;
                TextBox txtnumrecibo = row.FindControl("txtnumrecibo") as TextBox;
                CheckBox chktipPago = row.FindControl("chktipPago") as CheckBox;
                try
                {
                    if (txtMon.Text.Length > 0 && Convert.ToDouble(txtMon.Text) > 0.00)
                    {
                        txtMon.Text = Convert.ToDouble(txtMon.Text).ToString("N");
                    }
                    else
                        txtMon.Text = Convert.ToDouble("0.00").ToString("N");                    
                }
                catch 
                {
                    txtMon.Text = Convert.ToDouble("0.00").ToString("N"); 
                }
                if (txtnumrecibo.Text.Length > 0 && txtFecPago.Text.Length > 0)
                {
                    if (row.Cells[9].Text == "PEN")
                    {

                        SumSoles = SumSoles + Convert.ToDouble(txtMon.Text);
                        if (chktipPago.Checked == false)
                        {
                            nSumSol = nSumSol + Convert.ToDouble(txtMon.Text);
                        }
                    }
                    else
                    {
                        SumDol = SumDol + Convert.ToDouble(txtMon.Text);
                        if (chktipPago.Checked == false)
                        {
                            nSumDol = nSumDol + Convert.ToDouble(txtMon.Text);
                        }
                    }

                }

                txtMon.Text = Convert.ToDouble(txtMon.Text).ToString("N");
                                
            }
            lblSubDolares.Text = SumDol.ToString("N");
            lblSubSoles.Text = SumSoles.ToString("N");
            lblCobEfecSol.Text = SumSoles.ToString("N");
            lblCobEfecDol.Text = SumDol.ToString("N");
            lblCobSnefeSol.Text = nSumSol.ToString("N");
            lblCobSnefeDol.Text = nSumDol.ToString("N");

            ///////////////////////INGRESOS
            //viaticos
            try
            {                
                
                SumSoles = SumSoles + Convert.ToDouble(txtctaViasoles.Text);
                txtctaViasoles.Text = Convert.ToDouble(txtctaViasoles.Text).ToString("N"); 
            }
            catch (Exception ex)
            {
                
                    txtctaViasoles.Text = Convert.ToDouble("0.00").ToString("N");
                    var res = ex.Message;
            }
            try
            {
                SumDol = SumDol + Convert.ToDouble(txtctaViadolares.Text);
                txtctaViadolares.Text = Convert.ToDouble(txtctaViadolares.Text).ToString("N");
            }
            catch (Exception ex)
            {

                txtctaViadolares.Text = Convert.ToDouble("0.00").ToString("N");
                var res = ex.Message;
            }
            lblingsoles.Text = SumSoles.ToString("N");
            lblingdolares.Text = SumDol.ToString("N");
           
            //Gastos Varios
            try
            {

                SumSoles = SumSoles - Convert.ToDouble(txtGasVSol.Text);
                txtGasVSol.Text = Convert.ToDouble(txtGasVSol.Text).ToString("N"); 
            }
            catch (Exception ex)
            {

                txtGasVSol.Text = Convert.ToDouble("0.00").ToString("N");
                var res = ex.Message;
            }
            try
            {
                SumDol = SumDol - Convert.ToDouble(txtGasVDol.Text);
                txtGasVDol.Text = Convert.ToDouble(txtGasVDol.Text).ToString("N");
            }
            catch (Exception ex)
            {

                txtGasVDol.Text = Convert.ToDouble("0.00").ToString("N");
                var res = ex.Message;
            }
            // Cheques
            try
            {

                SumSoles = SumSoles - Convert.ToDouble(txtchqSol.Text);
                txtchqSol.Text = Convert.ToDouble(txtchqSol.Text).ToString("N");
            }
            catch (Exception ex)
            {

                txtchqSol.Text = Convert.ToDouble("0.00").ToString("N");
                var res = ex.Message;
            }
            try
            {
                SumDol = SumDol - Convert.ToDouble(txtchqDol.Text);
                txtchqDol.Text = Convert.ToDouble(txtchqDol.Text).ToString("N");
            }
            catch (Exception ex)
            {

                txtchqDol.Text = Convert.ToDouble("0.00").ToString("N");
                var res = ex.Message;
            }
            // Entrega Efectivo
            try
            {

                SumSoles = SumSoles - Convert.ToDouble(txtegrEfeSol.Text);
                txtegrEfeSol.Text = Convert.ToDouble(txtegrEfeSol.Text).ToString("N");
            }
            catch
            {

                txtegrEfeSol.Text = Convert.ToDouble("0.00").ToString("N");                
            }
            try
            {
                SumDol = SumDol - Convert.ToDouble(txtegrEfeDol.Text);
                txtegrEfeDol.Text = Convert.ToDouble(txtegrEfeDol.Text).ToString("N");
            }
            catch
            {

                txtegrEfeDol.Text = Convert.ToDouble("0.00").ToString("N");

            }
            SumSoles = SumSoles - Convert.ToDouble(lblCobSnefeSol.Text);
            SumDol = SumDol - Convert.ToDouble(lblCobSnefeDol.Text);

            lblegrSoles.Text = ((SumSoles - Convert.ToDouble(lblingsoles.Text))*(-1)).ToString("N");
            lblegrDolares.Text = ((SumDol - Convert.ToDouble(lblingdolares.Text))*(-1)).ToString("N");
            lblSalSoles.Text = SumSoles.ToString("N");
            lblSalDolares.Text = SumDol.ToString("N");

            if (SumSoles >= 0 && SumSoles >= 0 && (SumSoles >=0 || SumSoles > 0)) {
                HabilitarBtn(btnGuardar, true);
            }
            
        }
        #endregion
        #region FUNCIONES DE LOS BOTONES
        protected void btnNuevo_Click(object sender, EventArgs e) {
            pnBusqueda.Visible = false;
            pnLiquidacion.Visible = true;

            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnProcesar, true);
            HabilitarBtn(btnImprimir, true);
            HabilitarBtn(btnGuardar, false);
            btnProcesar.Text = "Procesar";
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

            for (int i = 20; i <= 29; i++) {
                dgvPagoLetra.Columns[i].Visible = true;
            }
                
            String _filtro = "", _estado = "", _codletra = "";
            DateTime fecMax;
            int _cod_cli = 0, _cod_vendedor = 0;
            clsdetalle_pagos_now formll = new clsdetalle_pagos_now();
            DataTable dtRegistro;
            if (ddlListaVendedores.SelectedValue == "0")//0 vendedor
                _filtro = _filtro + "1";
            else
            {
                _filtro = _filtro + "2";
                _cod_vendedor = Convert.ToInt32(ddlListaVendedores.SelectedValue);
            }

            if (txtCliente.Text.Length > 0 && ddlCliente.SelectedValue != "0")//1 cliente
            {
                _filtro = _filtro + "2";
                _cod_cli = Convert.ToInt32(ddlCliente.SelectedValue);
            }
            else
            {
                _filtro = _filtro + "1";
            }
            _filtro = _filtro + "211111";//2  estado,vencimiento desde, hasta cancelacion desde hasta, dias de atraso
            if (ddlMoneda.SelectedValue == "0")
                _filtro = _filtro + 1;
            else
                _filtro = _filtro + 2;

            _filtro = _filtro + "11";
            fecMax = DateTime.Today;
            
                     
            _filtro = _filtro + "1";//CLIENTE            
            dtRegistro = formll.GetList_letra(_filtro, "V", _cod_vendedor, _cod_cli, "1", DateTime.Today, DateTime.Today, DateTime.Today, DateTime.Today,ddlMoneda.SelectedValue,0,0);
            dgvPagoLetra.DataSource = dtRegistro;
            dgvPagoLetra.DataBind();
            foreach (GridViewRow row in dgvPagoLetra.Rows)
            {
                TextBox fecPag = row.FindControl("txtFecPago") as TextBox;
                fecPag.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            for (int i = 20; i <= 29; i++)
            {
                dgvPagoLetra.Columns[i].Visible = false;
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e) { }
        protected void btnAnular_Click(object sender, EventArgs e) { }
        protected void btnCancelar_Click(object sender, EventArgs e) {
            pnBusqueda.Visible = true;
            pnLiquidacion.Visible = false;
            pnLiquidacion.Enabled = true;
            ddlListaVendedores.SelectedValue = "0";
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnProcesar, true);
            HabilitarBtn(btnGuardar, false);

            btnProcesar.Text = "Buscar";
            btnCancelar.Text = "Cancelar";

            txtObservaciones.Text = "";
            txtFecMax.Text = "";
            txtFecMin.Text = "";
        }
        protected void btnGuardar_Click(object sender, EventArgs e) {
            for (int i = 20; i <= 29; i++)
            {
                dgvPagoLetra.Columns[i].Visible = true;
            }

            //primero guardar la cabecera tabla tbliquCob
            clstbliqCobranza formliq = new clstbliqCobranza();
            tbliqCobranza objliq = new tbliqCobranza();
            int idliquidacion = formliq.MaxOpeCod() + 1;
            objliq.idliqcob = idliquidacion;
            objliq.fecreg = Convert.ToDateTime( txtFecha.Text);

            objliq.efectivo = Convert.ToDecimal(lblCobEfecSol.Text);
            objliq.efectivodol = Convert.ToDecimal(lblCobEfecDol.Text);

            objliq.ctaViatico = Convert.ToDecimal(txtctaViasoles.Text);
            objliq.ctaViaticodol = Convert.ToDecimal(txtctaViadolares.Text);

            objliq.depositos = Convert.ToDecimal(lblCobSnefeSol.Text);
            objliq.depositosdol = Convert.ToDecimal(lblCobSnefeDol.Text);

            objliq.GastVarios = Convert.ToDecimal(txtGasVSol.Text);
            objliq.GastVariosdol = Convert.ToDecimal(txtGasVDol.Text);

            objliq.cheques = Convert.ToDecimal(txtchqSol.Text);
            objliq.chequesdol = Convert.ToDecimal(txtchqDol.Text);

            objliq.inefectivo = Convert.ToDecimal(txtegrEfeSol.Text);
            objliq.inefectivodol = Convert.ToDecimal(txtegrEfeDol.Text);

            objliq.totsoles = Convert.ToDecimal(lblSalSoles.Text);
            objliq.totdolares = Convert.ToDecimal(lblSalDolares.Text);
            objliq.observaciones = txtObservaciones.Text;
            // falta ingresar las observaiones y el recibidor
            int cod_det;
            int ultimo;
            int cod_ven;
            int cod_letra;
            int cod_cliente;
            int primero=0;
            decimal monto_total;
            decimal monto_cuota;
            decimal pago;
            DateTime fecha, fecha_min = DateTime.Now, fecha_max = DateTime.Now;

            int flag = 0;

            formliq.fnletraInsertar(objliq);
            foreach (GridViewRow row in dgvPagoLetra.Rows)
            {
                TextBox txtMon = row.FindControl("txtmonto") as TextBox;
                TextBox txtFecPago = row.FindControl("txtFecPago") as TextBox;
                TextBox txtnumrecibo = row.FindControl("txtnumrecibo") as TextBox;
                CheckBox chktipPago = row.FindControl("chktipPago") as CheckBox;

                try
                {
                    if (txtMon.Text.Length > 0 && Convert.ToDouble(txtMon.Text) > 0.00)
                    {
                        txtMon.Text = Convert.ToDouble(txtMon.Text).ToString("N");
                    }
                    else
                    {
                        txtMon.Text = Convert.ToDouble("0.00").ToString("N");
                        dgvPagoLetra.Rows[row.RowIndex].Visible= false;
                        //dgvPagoLetra.Columns[i].Visible = true;

                    }
                }
                catch
                {
                    txtMon.Text = Convert.ToDouble("0.00").ToString("N");
                    dgvPagoLetra.Rows[row.RowIndex].Visible = false;
                    
                }
             

                txtMon.Text = Convert.ToDouble(txtMon.Text).ToString("N");
            }

            clspag_Letra form = new clspag_Letra();
            int cod_mov = Convert.ToInt32(form.Maxcod_pago()) + 1;
            foreach (GridViewRow row in dgvPagoLetra.Rows)
            {
                flag = 0;
                fecha = Convert.ToDateTime(txtFecha.Text);
                TextBox pag = row.FindControl("txtmonto") as TextBox;
                TextBox txtFecPago = row.FindControl("txtFecPago") as TextBox;
                TextBox txtobsdet = row.FindControl("txtobsdetalle") as TextBox;
                TextBox txtnumrecibo = row.FindControl("txtnumrecibo") as TextBox;
                CheckBox chktipPago = row.FindControl("chktipPago") as CheckBox;

                cod_det = Convert.ToInt32(row.Cells[25].Text);
                cod_ven = Convert.ToInt32(row.Cells[22].Text);
                cod_letra = Convert.ToInt32(row.Cells[26].Text);
                cod_cliente = Convert.ToInt32(row.Cells[28].Text);
                ultimo = Convert.ToInt32(row.Cells[29].Text);
                pago = Convert.ToDecimal(pag.Text);
                if (primero == 0)
                {
                    fecha_min = Convert.ToDateTime(txtFecPago.Text);
                    fecha_max = Convert.ToDateTime(txtFecPago.Text);
                    primero = 1;
                }
                else {
                    if (Convert.ToDateTime(txtFecPago.Text) < fecha_min) {
                        fecha_min = Convert.ToDateTime(txtFecPago.Text);
                    }
                    if (Convert.ToDateTime(txtFecPago.Text) > fecha_max) {
                        fecha_max = Convert.ToDateTime(txtFecPago.Text);
                    }
                }
                if (pago > 0)
                    {
                        monto_total = Convert.ToDecimal(row.Cells[11].Text);
                        monto_cuota = Convert.ToDecimal(row.Cells[14].Text);
                        
                        while (flag == 0)
                        {
                            Pag_letras obj = new Pag_letras();
                            if (pago > monto_cuota)
                            {

                                clsdetletra update = new clsdetletra();
                                det_letra obj_upd = new det_letra();
                                obj_upd = update.GetDetLetra(cod_det);
                                obj_upd.cestado = "2";
                                obj_upd.dfecpago = Convert.ToDateTime(fecha);


                                update.fnDetLetraUpdate(obj_upd);

                                if (ultimo == 0)
                                {
                                    ////////////////////
                                    //crea la nota/////
                                    ////////////////////
                                    clsOpeNotas formnota = new clsOpeNotas();
                                    Notas objnt = new Notas();
                                    objnt.inota = Convert.ToInt32(formnota.MaxOpeCod()) + 1;

                                    objnt.dfecreg = Convert.ToDateTime(fecha);
                                    objnt.ccodnota = cod_letra.ToString();
                                    objnt.iprvcod = cod_cliente;
                                    objnt.nmontoNota = pago - monto_cuota;
                                    objnt.cestadoNota = "N";
                                    objnt.nmntutilizado = Convert.ToDecimal(0.00);
                                    objnt.cobservaciones = "pago excesivo de factura";
                                    objnt.ctipo = "V";
                                    formnota.fnNotaInsert(objnt);

                                    ////////////////////////////////////////
                                    obj.idetletra = obj_upd.idetletra;
                                    obj.idpagletra = Convert.ToInt32(form.Maxidpaglet()) + 1;
                                    obj.nmonto = monto_cuota;
                                    obj.dfecpago = Convert.ToDateTime(fecha);
                                    obj.dfecmodifi = DateTime.Today;
                                    obj.ipercod = cod_ven;
                                    obj.icodpago = cod_mov;
                                    obj.inumRecibo = txtnumrecibo.Text;
                                    obj.cobservaciones = txtobsdet.Text;
                                    obj.inumliq = idliquidacion;
                                    if (chktipPago.Checked)
                                        obj.ctipPag = "E";
                                    else
                                        obj.ctipPag = "D";
                                    clspag_Letra inserta = new clspag_Letra();
                                    inserta.fnpag_letraInsertar(obj);

                                    flag = 1;
                                }
                                else
                                {
                                    obj.idetletra = obj_upd.idetletra;
                                    obj.idpagletra = Convert.ToInt32(form.Maxidpaglet()) + 1;
                                    obj.nmonto = monto_cuota;
                                    obj.dfecpago = Convert.ToDateTime(fecha);
                                    obj.dfecmodifi = DateTime.Today;
                                    obj.ipercod = cod_ven;
                                    obj.icodpago = cod_mov;
                                    obj.inumRecibo = txtnumrecibo.Text;
                                    obj.cobservaciones = txtObservaciones.Text;
                                    obj.inumliq = idliquidacion;
                                    if(chktipPago.Checked)
                                        obj.ctipPag= "E";
                                    else
                                        obj.ctipPag = "D";
                                    clspag_Letra inserta = new clspag_Letra();
                                    inserta.fnpag_letraInsertar(obj);

                                    pago = pago - monto_cuota;
                                    cod_det = Convert.ToInt32(update.Mindetletpend_cod(cod_letra));
                                    monto_cuota = fnnextDeuda(cod_det);
                                }
                            }
                            else
                            {
                                obj.idetletra = cod_det;
                                obj.idpagletra = Convert.ToInt32(form.Maxidpaglet()) + 1;
                                obj.nmonto = pago;
                                obj.dfecpago = Convert.ToDateTime(fecha);
                                obj.dfecmodifi = DateTime.Today;
                                obj.ipercod = cod_ven;
                                obj.icodpago = cod_mov;
                                obj.inumRecibo = txtnumrecibo.Text;
                                obj.cobservaciones = txtObservaciones.Text;
                                obj.inumliq = idliquidacion;
                                if (chktipPago.Checked)
                                    obj.ctipPag = "E";
                                else
                                    obj.ctipPag = "D";
                                clspag_Letra inserta = new clspag_Letra();
                                inserta.fnpag_letraInsertar(obj);
                                if (pago == monto_cuota)
                                {
                                    clsdetletra update = new clsdetletra();
                                    det_letra obj_upd = new det_letra();
                                    obj_upd = update.GetDetLetra(cod_det);
                                    obj_upd.cestado = "2";
                                    obj_upd.dfecpago = Convert.ToDateTime(fecha);
                                    update.fnDetLetraUpdate(obj_upd);
                                }
                                flag = 1;

                            }
                        }
                    }
            }
            for (int i = 20; i <= 29; i++)
            {
                dgvPagoLetra.Columns[i].Visible = false;
            }
            txtFecMin.Text = fecha_min.ToString("yyyy-MM-dd");
            txtFecMax.Text = fecha_max.ToString("yyyy-MM-dd");
            pnLiquidacion.Enabled = false;
            btnCancelar.Text = "Salir";
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnProcesar, false);
           
        }
        protected void btnProcesar_Click(object sender, EventArgs e){

            if (btnProcesar.Text == "Buscar")
            {
                if (rbtResumido.Checked)
                {
                    pnResumindo.Visible = true;
                    pnDetallado.Visible = false;

                    for (int i = 10; i <= 18; i++)
                    {
                        dgvListCobranza.Columns[i].Visible = true;
                    }
                    String _filtro = "", _estado = "", _codletra = "";
                    DateTime fecMax;
                    int _cod_cli = 0, _cod_vendedor = 0;
                    clsList_letra formll = new clsList_letra();
                    DataTable dtRegistro;

                    _filtro = "1";//numero de letra
                    _filtro = _filtro + "1"; //Fecha
                    fecMax = DateTime.Today;
                    _filtro = _filtro + "2";// estado
                    _estado = "PEND";

                    if (ddlMoneda.SelectedValue == "0")
                        _filtro = _filtro + "1";
                    else
                        _filtro = _filtro + "2";
                    if (ddlListaVendedores.SelectedValue == "0")
                        _filtro = _filtro + "1";
                    else
                    {
                        _filtro = _filtro + "2";
                        _cod_vendedor = Convert.ToInt32(ddlListaVendedores.SelectedValue);
                    }
                    if (txtCliente.Text.Length > 0)
                    {
                        _filtro = _filtro + "2";
                        _cod_cli = Convert.ToInt32(ddlCliente.SelectedValue);
                    }
                    else
                    {
                        _filtro = _filtro + "1";
                    }
                    dtRegistro = formll.GetList_letra(_filtro, _codletra, fecMax, _estado, ddlMoneda.SelectedValue, _cod_vendedor, _cod_cli, "V");
                    dgvListCobranza.DataSource = dtRegistro;
                    dgvListCobranza.DataBind();

                    for (int i = 10; i <= 18; i++)
                    {
                        dgvListCobranza.Columns[i].Visible = false;
                    }
                }
                else
                {
                    pnResumindo.Visible = false;
                    pnDetallado.Visible = true;

                    String _filtro = "", _estado = "", _codletra = "";
                    DateTime fecMax;
                    int _cod_cli = 0, _cod_vendedor = 0;
                    clsvwList_let_detalle formld = new clsvwList_let_detalle();
                    DataTable dtRegistro;

                    _filtro = "1";//numero de letra
                    _filtro = _filtro + "1"; //Fecha
                    fecMax = DateTime.Today;
                    _filtro = _filtro + "2";// estado
                    _estado = "PEND";

                    if (ddlMoneda.SelectedValue == "0")
                        _filtro = _filtro + "1";
                    else
                        _filtro = _filtro + "2";
                    if (ddlListaVendedores.SelectedValue == "0")
                        _filtro = _filtro + "1";
                    else
                    {
                        _filtro = _filtro + "2";
                        _cod_vendedor = Convert.ToInt32(ddlListaVendedores.SelectedValue);
                    }
                    if (txtCliente.Text.Length > 0)
                    {
                        _filtro = _filtro + "2";
                        _cod_cli = Convert.ToInt32(ddlCliente.SelectedValue);
                    }
                    else
                    {
                        _filtro = _filtro + "1";
                    }
                    dtRegistro = formld.GetList_letra(_filtro, _codletra, fecMax, _estado, ddlMoneda.SelectedValue, _cod_vendedor, _cod_cli, "V");
                    dgvListCobDet.DataSource = dtRegistro;
                    dgvListCobDet.DataBind();
                    //dgvListCobDet
                }
            }
            else {
                calcular();
                if (Convert.ToDouble(lblSalSoles.Text) > 0 || Convert.ToDouble(lblSalDolares.Text) > 0)
                {
                    HabilitarBtn(btnGuardar, true);
                }
                else {
                    HabilitarBtn(btnGuardar, true);
                }
            }
            
        }
        protected void btnImprimir_Click(object sender, EventArgs e) { }
        protected void btnAgregar_Click(object sender, EventArgs e) { }
        protected void btnEliminar_Click(object sender, EventArgs e) { }
              
        #endregion
        #region FUNCIONES DEL
        private void IniciarCampos()
        {
            System.Text.StringBuilder sb = new StringBuilder();

            sb.AppendLine("Nueva");
            sb.AppendLine("Liquidacion");
            btnNuevo.Text=sb.ToString();
            btnNuevo.Visible = true;
            btnCancelar.Visible = true;
            btnEditar.Visible = true;
            btnAnular.Visible = false;
            btnImprimir.Visible = true;
            btnGuardar.Visible = true;
            btnProcesar.Visible = true;

            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnImprimir, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnProcesar, true);

            pnResumindo.Visible = false;
            pnDetallado.Visible = false;
            pnLiquidacion.Visible = false;

            CargarClientes();
            cargarMoneda();
            CargarVendedores();
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
        public void CargarVendedores()
        {
            clsPersonal lstPersonal = new clsPersonal();
            var lstVendedores = lstPersonal.GetPersonalPorTipo(2);

            ddlListaVendedores.DataSource = lstVendedores;
            ddlListaVendedores.DataBind();
            ddlListaVendedores.Items.Insert(0, new ListItem("Todos", "0"));



        }
        public decimal fnnextDeuda(int cod_Det)
        {
            clsdetletra form = new clsdetletra();
            det_letra obj = new det_letra();
            obj = form.GetDetLetra(cod_Det);

            return Convert.ToDecimal(obj.nmonto);
        }
        #endregion
    }
}