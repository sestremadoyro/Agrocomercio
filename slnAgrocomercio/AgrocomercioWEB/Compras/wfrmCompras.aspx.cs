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
    public partial class wfrmCompras : BasePage
    {
        public String _click = "";
        public String gcOpeTipo = "C";
        private Thread oThread;

        #region PROPIEDADES FORMULARIO

        /// PROPIEDADES DE LA OPERACION
        public string cProceso
        {
            get { return lblProceso.Value; }
        }
        public long nOpeCod
        {
            get { return lblNroPedido.Text.Trim() == "" ? 0 : Convert.ToInt64(lblNroPedido.Text); }
        }
        public DateTime dOpeFecEmision
        {
            get { return DateTime.Parse(txtFecha.Text); }
        }
        public string cZonCod
        {
            get { return ddlZonas.SelectedValue; }
        }
        public int nOpePrvCod
        {
            get { return int.Parse(ddlProveedor.SelectedValue); }
        }
        public string cOpeMoneda
        {
            get { return ddlMoneda.SelectedValue.Trim(); }
        }
        public string cOpeTipPago
        {
            get { return ddlTipoVenta.SelectedValue.Trim(); }
        }
        public Decimal nOpeSubTotal
        {
            get { return (Decimal)GetNumero(txtPrecioCompra.Text); }
            set { txtPrecioCompra.Text = SetFormatNum((double)value); }
        }
        public Decimal nOpeDscto
        {
            get { return (Decimal)GetNumero(txtDescuento.Text); }
            set { txtDescuento.Text = SetFormatNum((double)value); }
        }
        public Decimal nOpeImpuesto
        {
            get { return (Decimal)GetNumero(txtIgv.Text); }
            set { txtIgv.Text = SetFormatNum((double)value); }
        }
        public Decimal nOpeFlete
        {
            get { return (Decimal)GetNumero(txtFlete.Text); }
            set { txtFlete.Text = SetFormatNum((double)value); }
        }
        public Decimal nOpeTotal
        {
            get { return (Decimal)GetNumero(txtCostoTotal.Text); }
            set { txtCostoTotal.Text = SetFormatNum((double)value); }
        }
        public string cOpeTipCiclo
        {
            get { return ddlTipCiclo.SelectedValue; }
        }
        public int nOpeCiclo
        {
            get { return txtCiclo.Text == "" ? 0 : Convert.ToInt32(txtCiclo.Text); }
        }
        public int ntcmCod
        {
            get { return lbltcmCod.Value == "" ? -1 : Convert.ToInt32(lbltcmCod.Value); }
        }
        public double nTipoCambioOri
        {
            get { return lblTipCambio.Value == "" ? 0.0 : Convert.ToDouble(lblTipCambio.Value); }
        }
        public double nTipoCambio
        {
            get { return txtTipCambio.Text == "" ? 0.0 : Convert.ToDouble(txtTipCambio.Text); }
        }

        /// PROPIEDADES DE LA DOCUMENTOS OPERACION
        public string cdopNroSerie
        {
            get { return lblNroDocumento.Text.Split('-')[0].Trim(); }
        }
        public string cdopNumero
        {
            get { return lblNroDocumento.Text.Split('-')[1].Trim(); }
        }
        public int ntdoCod
        {
            get { return int.Parse(lblTipoDoc.Value); }
        }
        public long ndopCod
        {
            get { return lbldopCod.Value.Trim() == "" ? 0 : Convert.ToInt64(lbldopCod.Value); }
        }
        public DateTime? ddopFecTraslado
        {
            get
            {
                if (txtFecTraslado.Text == "") return null;
                else return Convert.ToDateTime(txtFecTraslado.Text);
            }
        }
        public String cdopPunPartida
        {
            get { return txtPuntoPartida.Text; }
        }
        public String cdopDocComple
        {
            get { return txtNotaPedidoVen.Text; }
        }

        



        /// PROPIEDADES DE PROVEEDORES
        public int nPrvCod
        {
            get { return txtCliCod.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCliCod.Text); }
        }
        public string cPrvNumDoc
        {
            get { return txtNroDoc.Text.Trim(); }
        }
        public string cPrvRazon
        {
            get { return txtNombre.Text.Trim(); }
        }
        public string cPrvDireccion
        {
            get { return txtCliDireccion.Text.Trim(); }
        }
        public string cPrvContacto
        {
            get { return txtRepresentante.Text.Trim(); }
        }
        public string cPrvTelefono
        {
            get { return lblTelefono.Text.Trim(); }
        }
        public DateTime dPrvFecRegis
        {
            get { return (txtFecRegis.Text == "" ? DateTime.Today : DateTime.Parse(txtFecRegis.Text)); }
        }

        #endregion

        #region FUNCIONES DEL FORMULARIO
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
                        case ("DGVCOM_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            if (dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            {
                                dgvDetalleVenta.SelectedIndex = _selectedIndex;
                                HabilitarBtn(btnEliminar, true);
                            }
                            else
                            {
                                dgvDetalleVenta.SelectedIndex = -1;
                                HabilitarBtn(btnEliminar, false);
                            }

                            break;
                        case ("DGVCOM_DOUBLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            if (dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            {
                                if (double.Parse(dgvDetalleVenta.Rows[_selectedIndex].Cells[7].Text) == 0)
                                {
                                    dgvDetalleVenta.SelectedIndex = _selectedIndex;
                                    IniciarCamposArticulos("ESPECIAL");
                                    lsbArticulos.SelectedValue = dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text;
                                    lsbArticulos_SelectedIndexChanged(sender, e);
                                    txtArtCant.Text = dgvDetalleVenta.Rows[_selectedIndex].Cells[6].Text;
                                    txtArtDescuento.Text = dgvDetalleVenta.Rows[_selectedIndex].Cells[8].Text;
                                    ModalPopupAgregar.Show();
                                }
                            }
                            else
                            {
                                dgvDetalleVenta.SelectedIndex = -1;
                            }

                            break;
                        case ("DGVLIS_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            if (dgvListOperCompras.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvListOperCompras.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            {
                                dgvListOperCompras.SelectedIndex = _selectedIndex;
                                HabilitarBtn(btnAnular, dgvListOperCompras.Rows[_selectedIndex].Cells[7].Text != "Cerrado");
                                HabilitarBtn(btnEditar, true);
                            }
                            else
                            {
                                dgvListOperCompras.SelectedIndex = -1;
                                HabilitarBtn(btnAnular, false);
                                HabilitarBtn(btnEditar, false);
                            }

                            break;
                        case ("DGVDOC_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            if (dgvDocumentos.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvDocumentos.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            {
                                dgvDocumentos.SelectedIndex = _selectedIndex;
                                dgvDocumentos_SelectedIndexChanged(sender, e);
                            }
                            else
                            {
                                dgvDocumentos.SelectedIndex = -1;
                            }

                            break;
                        case ("ART_SELECT"):
                            _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                            lsbArticulos.SelectedValue = _selectedValue;
                            lsbArticulos_SelectedIndexChanged(sender, e);
                            break;
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
                IniciarCampos();
            }

        }
        protected void dgvDetalleVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            AddCLickToGridView(ref dgvDetalleVenta);
        }
        protected void dgvListOperCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            AddCLickToGridView(ref dgvListOperCompras);
        }
        protected void dgvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            AddCLickToGridView(ref dgvDocumentos);
        }
        protected void dgvDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            long ndopCod = 0;
            if (lblOpeEstado.Value.Trim() == "R")
            {
                clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();
                lstDocumenOpe.Guardar(this, ref ndopCod);
                lbldopCod.Value = ndopCod.ToString();
            }

            int nDopCod = int.Parse(dgvDocumentos.Rows[dgvDocumentos.SelectedIndex].Cells[1].Text);
            string cDopDescri = dgvDocumentos.Rows[dgvDocumentos.SelectedIndex].Cells[2].Text;

            CargarCamposDocumento(nDopCod, cDopDescri);
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nPrvCod = 0;
            nPrvCod = int.Parse(ddlProveedor.SelectedValue);

            if (nPrvCod == 999)
            {
                txtProveedor.Text = "";
                ModalPopupNuevoPrv.Show();
                IniciarCamposProveedor();
                btnNuevoProveedor_Click(sender, e);
            }
            else if (nPrvCod == 0)
            {
                txtDocCli.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
            }
            else
            {
                if (int.Parse(lblTipoDoc.Value) > 3)
                {
                    clsPersonal lstPersonal = new clsPersonal();
                    Personal oVendedor = lstPersonal.GetPersonal(nPrvCod);

                    txtProveedor.Text = oVendedor.perNombres + " " + oVendedor.perApellidoPat + " " + oVendedor.perApellidoMat;
                    txtDocCli.Text = "";
                    txtDireccion.Text = oVendedor.perDireccion == null ? "" : oVendedor.perDireccion.ToString();
                    txtTelefono.Text = oVendedor.perTelefono == null ? "" : oVendedor.perTelefono.ToString();

                    lstPersonal = null;
                    oVendedor = null;
                }
                else
                {
                    clsProveedores lstProveedores = new clsProveedores();
                    Proveedores Proveedor = lstProveedores.GetProveedor(nPrvCod);

                    txtProveedor.Text = Proveedor.PrvRazon.ToString();
                    txtDocCli.Text = Proveedor.PrvNumDoc.ToString();
                    txtDireccion.Text = Proveedor.PrvDireccion.ToString();
                    txtTelefono.Text = Proveedor.PrvTelefono.ToString();

                    lstProveedores = null;
                    Proveedor = null;
                }
            }
        }
        protected void chkPorEstado_CheckedChanged(object sender, EventArgs e)
        {
            ddlEstados.Enabled = chkPorEstado.Checked;
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void chkPorFecha_CheckedChanged(object sender, EventArgs e)
        {
            txtFecIni.Enabled = chkPorFecha.Checked;
            txtFecFin.Enabled = chkPorFecha.Checked;
            if (chkPorFecha.Checked)
            {
                txtFecIni.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtFecFin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFecIni.Text = "";
                txtFecFin.Text = "";
            }
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void txtFecIni_TextChanged(object sender, EventArgs e)
        {
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void txtFecFin_TextChanged(object sender, EventArgs e)
        {
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void ddlProveedoresFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void chkPorProveedor_CheckedChanged(object sender, EventArgs e)
        {
            ddlProveedorFiltro.Enabled = chkPorProveedor.Checked;
            CargarListaOpeCompra(nNroDetPed);
        }
        protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoneda.SelectedValue == "USD")
            {
                pnTipCam.Visible = true;
                lblArtPreUnitario.Text = lblArtPreUnitario.Text.Replace("S/.", "$");
                lblImpTotal.Text = lblImpTotal.Text.Replace("S/.", "$");
            }
            else {
                pnTipCam.Visible = false;
                lblArtPreUnitario.Text = lblArtPreUnitario.Text.Replace("$", "S/.");
                lblImpTotal.Text = lblImpTotal.Text.Replace("$", "S/.");
            }            

            CargarTipoCambio();
            DataTable dtDetOper = CopiarDT(g_dtDetOperacion);
            dtDetOper = CambiarMonedaDetOperacion(dtDetOper);
            RellenarGrilla(ref dgvDetalleVenta, dtDetOper, this.nNroDetPed);
            CalcularPago(dtDetOper, false);
        }
        protected void txtTipCambio_TextChanged(object sender, EventArgs e)
        {
            g_nTipoCambio = double.Parse(txtTipCambio.Text);
            CalcularPago(g_dtDetOperacion);
        }
        protected void txtDesEspec_TextChanged(object sender, EventArgs e)
        {
            CalcularPago(g_dtDetOperacion);
            ModalPopupGirarCompra.Show();
        }

        protected void txtFleteTra_TextChanged(object sender, EventArgs e)
        {
            CalcularPago(g_dtDetOperacion);
            ModalPopupGirarCompra.Show();
        }

        protected void ddlTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTipCiclo.Visible = ddlTipoVenta.SelectedValue == "CR";
            txtCiclo.Visible = ddlTipoVenta.SelectedValue == "CR";
        }
        #endregion

        #region FUNCIONES DE BOTONES FORMULARIO
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int nMainTipo = 0;
            try
            {
                clsDocumenOperacion lstDocumentos = new clsDocumenOperacion();

                if (((Button)sender).ToolTip == btnNuevo.ToolTip)
                {
                    HabilitarCampos(true);
                    CargarCombos();
                    nMainTipo = 2;
                    lblProceso.Value = "NEW";
                    ddlTipoDocu.Enabled = true;
                    txtProveedor.Text = "";
                    ddlTipCiclo.Visible = false;
                    txtCiclo.Visible = false;
                }
                else
                {
                    DataRow[] Filas = g_dtDetOperacion.Select("dtpSubTotal = 0");
                    if (Filas.Count() > 0)
                    {
                        MessageBox("Tiene " + Filas.Count().ToString() + " Detalles con Precio 0, No se puede Canjear la Factura.");
                        return;
                    }
                    else
                    {
                        txtNroDocu.Enabled = true;
                        txtNroSerie.Enabled = true;
                        nMainTipo = 3;
                        lblProceso.Value = "NEWDOC";
                        ddlTipoDocu.Enabled = false;
                    }
                }

                if (ddlTipoDocu.Items.Count > 1)
                {
                    if (ddlTipoDocu.Items.Count > nMainTipo - 1)
                        ddlTipoDocu.SelectedIndex = nMainTipo;
                    else
                        ddlTipoDocu.SelectedIndex = 0;

                    txtNroSerie.Text = lstDocumentos.MaxDopNroSerie(Convert.ToInt32(ddlTipoDocu.SelectedValue));
                    //txtNroSerie.Text = string.Empty;
                    txtNroSerie.Focus();
                    HabilitarBtn(btnAgregar, true);
                    ModalPopupNuevoDocumento.Show();
                }
                else
                    MessageBox("No hay ningun Tipo de Documento registrado. Por favor registrar alguno antes de Comenzar.");

            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            oThread = new Thread(delegate() { Esperar(1000); });
            oThread.Start();

            clsOperaciones lstOperaciones = new clsOperaciones();
            clsDetOperacion lstDetOperacion = new clsDetOperacion();

            Operaciones Operacion = new Operaciones();
            DocumenOperacion DocumenOpe = new DocumenOperacion();
            DataTable dtDetOperacion;
            int nOpeCod = 0;
            int nDopCod = 0;
            string cDopDescri = "";

            try
            {
                HabilitarCampos(true);
                CargarCombos();
                
                txtDireccion.Enabled = false;
                txtDocCli.Enabled = false;
                txtTelefono.Enabled = false;

                nOpeCod = int.Parse(dgvListOperCompras.Rows[dgvListOperCompras.SelectedIndex].Cells[2].Text);

                SetPanelDocumento(nOpeCod);
                nDopCod = int.Parse(dgvDocumentos.Rows[0].Cells[1].Text);
                cDopDescri = dgvDocumentos.Rows[0].Cells[2].Text;

                CargarCamposDocumento(nDopCod, cDopDescri);
                CargarProveedores();
                CargarCamposOperacion(nOpeCod);
                ddlProveedor_SelectedIndexChanged(sender, e);

                dtDetOperacion = lstDetOperacion.GetListDetOperacion(nOpeCod);
                g_dtDetOperacion = dtDetOperacion;

                DataTable dtDetOper = CopiarDT(g_dtDetOperacion);
                dtDetOper = CambiarMonedaDetOperacion(dtDetOper);
                RellenarGrilla(ref dgvDetalleVenta, dtDetOper, this.nNroDetPed);

                lblProceso.Value = lblOpeEstado.Value == "C" ? "CLOSE" : "EDIT";
                SetBotones(lblProceso.Value);

                ConfigurarDocumento();

                GuardarDtCabeceraDocumento();

                oThread.Join();
            }
            catch (Exception ex)
            {
                oThread.Abort();
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            oThread = new Thread(delegate() { Esperar(1000); });
            oThread.Start();

            clsOperaciones lstOperaciones = new clsOperaciones();
            int nOpeCod = 0;

            try
            {
                nOpeCod = int.Parse(dgvListOperCompras.Rows[dgvListOperCompras.SelectedIndex].Cells[2].Text);

                if (lstOperaciones.ValidarAnulacion(nOpeCod))
                {
                    lstOperaciones.Anular(nOpeCod);

                    CargarListaOpeCompra(nNroDetPed);
                    SetBotones("ANULAR");
                    oThread.Join();
                    MessageBox("La Operacion Fue Anulada Correctamente.");
                }
                else
                {
                    MessageBox("Esta compra no se puede ANULAR, El lote Comprado fue Afectado por una Venta. Debe Anular Primero la Venta.");
                }
            }
            catch (Exception ex)
            {
                oThread.Abort();
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            IniciarCampos();
            ddlMoneda.SelectedValue = "000";
            ddlTipoVenta.SelectedValue = "000";
            ddlTipoDocu.SelectedValue = "000";
            ddlProveedor.SelectedValue = "000";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            try
            {
                if (ValidarDatos("OPE", ref cMensaje))
                {
                    txtFleteTra.Text = Math.Round(GetNumero(txtSubTotal.Text) / 100, 2).ToString();
                    txtFlete.Text = SetFormatNum(Math.Round(GetNumero(txtSubTotal.Text) / 100, 2));

                    if (lblProceso.Value == "NEW")
                    {
                        clsOperaciones lstOperaciones = new clsOperaciones();
                        long nOpeCod = lstOperaciones.MaxOpeCod() + 1;
                        lblNroPedido.Text = nOpeCod.ToString().PadLeft(10, '0');
                        txtDesEspec.Text = "0.0";

                        ModalPopupGirarCompra.Show();
                    }
                    else if (lblProceso.Value == "EDIT")
                    {
                        btnGuardarDocu_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox(cMensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message + ex.InnerException.Message);
            }
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            clsOperaciones lstOperaciones = new clsOperaciones();

            long ndopCod = 0;
            int ntcmCod = 0;
            try
            {
                if (ValidarDatos("OPE", ref cMensaje, "PROC"))
                {
                    if (lblProceso.Value == "NEW")
                    {
                        txtFleteTra.Text = Math.Round(GetNumero(txtSubTotal.Text) / 100, 2).ToString();
                        txtFlete.Text = SetFormatNum(Math.Round(GetNumero(txtSubTotal.Text) / 100, 2));
                        long nOpeCod = lstOperaciones.MaxOpeCod() + 1;
                        lblNroPedido.Text = nOpeCod.ToString().PadLeft(10, '0');
                        txtDesEspec.Text = "0.0";
                    }

                    lstOperaciones.Procesar(this, gcOpeTipo, ref ndopCod, ref ntcmCod);
                    lbldopCod.Value = ndopCod.ToString();
                    lbltcmCod.Value = ntcmCod.ToString();
                    lblOpeEstado.Value = "P";

                    GuardarDtCabeceraDocumento();
                    MessageBox("La Operacion fue Procesada con Exito, Ahora puede imprimir.");
                    //NUEVO:  asigna funcion a botón IMPRIMIR
                    AsignarFuncionBotonImprimir();

                    lblProceso.Value = "EDIT";
                    SetBotones(lblProceso.Value);

                    ConfigurarDocumento();
                }
                else
                {
                    MessageBox(cMensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message + ex.InnerException.Message );
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            clsOperaciones lstOperaciones = new clsOperaciones();

            try
            {
                if (ValidarDatos("OPE", ref cMensaje))
                {
                    lstOperaciones.Cerrar(this);
                    lblOpeEstado.Value = "C";

                    GuardarDtCabeceraDocumento();
                    MessageBox("La Operacion fue Cerrada.");
                    lblProceso.Value = "CLOSE";
                    SetBotones(lblProceso.Value);
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
            clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();
            int nDopCod = 0;
            try
            {
                nDopCod = int.Parse(lbldopCod.Value);
                lstDocumenOpe.ActualizarImpresion(nDopCod);
            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            IniciarCamposArticulos();
            if (lsbArticulos.Items.Count > 0 && lsbArticulos.Items[0].Value != "0")
            {
                if (ddlProveedor.SelectedValue != "000" && ddlProveedor.SelectedValue != "999" && lblTipoDoc.Value != "4" && lblTipoDoc.Value != "5")
                {
                    ddlLaboratorios.SelectedValue = ddlProveedor.SelectedValue;
                    ddlLaboratorios_SelectedIndexChanged(sender, e);
                }
                ModalPopupAgregar.Show();
            }
            else
                MessageBox("Por Favor Primero debe Registrar articulos antes de Continuar!");

        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string ArtCod = dgvDetalleVenta.Rows[dgvDetalleVenta.SelectedIndex].Cells[2].Text;

            DataTable dtDetalleCompra;
            try
            {
                dtDetalleCompra = g_dtDetOperacion;
                if (dtDetalleCompra.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDetalleCompra.Rows.Count; i++)
                    {
                        if (dtDetalleCompra.Rows[i]["ArtCod"].ToString() == ArtCod.ToString())
                        {
                            dtDetalleCompra.Rows.Remove(dtDetalleCompra.Rows[i]);
                        }
                    }
                }
                g_dtDetOperacion = dtDetalleCompra;
                CalcularPago(dtDetalleCompra);
                RellenarGrilla(ref dgvDetalleVenta, dtDetalleCompra, this.nNroDetPed);
                dgvDetalleVenta.SelectedIndex = -1;
                HabilitarBtn(btnEliminar, false);
            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
        }
        protected void btnAceptarNuevoDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNroDocu.Text != "")
                {
                    lblTipoDoc.Value = ddlTipoDocu.SelectedValue;

                    //GUARDA EL TIPO DE DOC EN VARIABLE SESSION
                    this.AgregarVariableSession("lblTipoDoc", lblTipoDoc.Value);

                    lblDescriDocumento.Text = ddlTipoDocu.SelectedItem.Text;
                    lblNroDocumento.Text = txtNroSerie.Text + " - " + txtNroDocu.Text;

                    if (lblProceso.Value == "NEW")
                    {
                        HabilitarCampos(true);
                        g_dtDetOperacion.Rows.Clear();
                        RellenarGrilla(ref dgvDetalleVenta, g_dtDetOperacion, this.nNroDetPed);
                        CargarCombos();
                        CargarProveedores();

                        txtDireccion.Enabled = false;
                        txtDocCli.Enabled = false;
                        txtTelefono.Enabled = false;

                        txtSubTotal.Text = "0.00";
                        txtDescuento.Text = "0.00";
                        txtFlete.Text = "0.00";
                        txtIgv.Text = "0.00";
                        txtCostoTotal.Text = "0.00";

                        txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                        SetBotones(lblProceso.Value);
                        ConfigurarDocumento();
                    }
                    else if (lblProceso.Value == "NEWDOC")
                    {
                        clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();

                        long ndopCod = 0;
                        int OpeCod = int.Parse(lblNroPedido.Text);

                        lstDocumenOpe.Guardar(this, ref ndopCod);
                        lbldopCod.Value = ndopCod.ToString();

                        SetPanelDocumento(OpeCod);
                        lblProceso.Value = "EDIT";
                    }
                }
                else
                {
                    MessageBox("Debe poner un Numero de Documento");
                }

            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnGuardarDocu_Click(object sender, EventArgs e)
        {
            oThread = new Thread(delegate() { Esperar(1000); });
            oThread.Start();

            long ndopCod = 0;
            int ntcmCod = 0;
            clsOperaciones lstOperaciones = new clsOperaciones();
            try
            {
                txtDescuento.Text = txtDesEspec.Text;
                CalcularPago(g_dtDetOperacion);

                lstOperaciones.Guardar(this, gcOpeTipo, ref ndopCod, ref ntcmCod);  //GUARDAR OPERACION    
                lbldopCod.Value = ndopCod.ToString();
                lbltcmCod.Value = ntcmCod.ToString();

                HabilitarBtn(btnProcesar, true);
                oThread.Join();
                if (sender != btnProcesar)
                {
                    MessageBox("La Operacion de Compra se Guardo con Exito ");
                }

                lblProceso.Value = "EDIT";
                SetPanelDocumento((int)nOpeCod);
                pnDocumentos.Visible = true;
                ModalPopupGirarCompra.Hide();
                
                ConfigurarDocumento();
            }
            catch (Exception ex)
            {
                oThread.Abort();
                MessageBox("Error Interno: " + ex.Message + ex.InnerException );
            }
        }

        #endregion

        #region FUNCIONES DE CARGA DE LISTAS DE DATOS
        public void CargarCombos()
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Monedas
            ddlMoneda.DataSource = Atributos.ListAtributos(1);
            ddlMoneda.DataBind();
            ddlMoneda.SelectedValue = "PEN";
            ddlMoneda.Items.Insert(0, new ListItem("", "000"));

            //Cargamos combo de Tipo de Venta
            ddlTipoVenta.DataSource = Atributos.ListAtributos(2);
            ddlTipoVenta.DataBind();
            ddlTipoVenta.Items.Insert(0, new ListItem("", "000"));

            //Cargamos combo de Tipo de Documento
            ddlTipoDocu.DataSource = Atributos.ListAtributos(5);
            ddlTipoDocu.DataBind();
            ddlTipoDocu.Items.Insert(0, new ListItem("", "000"));

            //Cargamos combo de Tipo de Ciclo
            ddlTipCiclo.DataSource = Atributos.ListAtributos(10);
            ddlTipCiclo.DataBind();
            ddlTipCiclo.SelectedIndex = 2;

            //Cargamos combo de Zonas
            ddlZonas.DataSource = Atributos.ListAtributos(4);
            ddlZonas.DataBind();

            lblTasIGV.Value = ((List<Atributos>)Atributos.ListAtributos(7)).FirstOrDefault().AtrDescripcion;
            Atributos = null;

            CargarTipoCambio();
        }
        public void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();

            if (lblTipoDoc.Value != "4" && lblTipoDoc.Value != "5" )
            {
                ddlProveedor.DataTextField = "PrvRazon";
                ddlProveedor.DataValueField = "PrvCod";
                ddlProveedor.DataSource = lstProveedores.GetAll();
                ddlProveedor.DataBind();
                ddlProveedor.Items.Add(new ListItem("[NUEVO PROVEEDOR]", "999"));               
            }
            else
            {
                clsPersonal lstPersonal = new clsPersonal();
                var lstVendedores = lstPersonal.GetPersonalPorTipo(1);
                ddlProveedor.DataTextField = "VenNombre";
                ddlProveedor.DataValueField = "PerCod";
                ddlProveedor.DataSource = lstPersonal.GetVendedores();
                ddlProveedor.DataBind();
            }

            ddlProveedor.Items.Insert(0, new ListItem("", "000"));

            ddlProveedorFiltro.DataSource = lstProveedores.GetAll();
            ddlProveedorFiltro.DataBind();

            lstProveedores = null;
        }
        public void CargarTipoCambio()
        {
            clsTipoCambios oTipCamp = new clsTipoCambios();

            var oTip = oTipCamp.GetLastTipoCambio(ddlMoneda.SelectedValue);
            if (oTip != null)
            {
                lbltcmCod.Value = oTip.tcmCod.ToString();
                lblTipCambio.Value = oTip.tcmCambio.ToString();
                txtTipCambio.Text = lblTipCambio.Value;
                g_nTipoCambio = double.Parse(lblTipCambio.Value);
            }
            else
            {
                lbltcmCod.Value = "-1";
                lblTipCambio.Value = "0.0";
                txtTipCambio.Text = "0.0";
                g_nTipoCambio = 0.0;
                MessageBox("Debe Ingresar un Tipo de Cambio.");
                txtTipCambio.Focus();
            }

        }
        protected void CargarListaOpeCompra(int _nNroReg)
        {
            clsOperaciones lstOperaciones = new clsOperaciones();
            DataTable dtOpeCompra;
            string cEstado = "";
            int nPrvCod = 0;
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;

            try
            {
                if (chkPorEstado.Checked)
                    cEstado = ddlEstados.SelectedValue;

                if (chkPorFecha.Checked)
                {
                    dFecIni = DateTime.Parse(txtFecIni.Text);
                    dFecFin = DateTime.Parse(txtFecFin.Text);
                }

                if (chkPorProveedor.Checked)
                    nPrvCod = int.Parse(ddlProveedorFiltro.SelectedValue);


                dtOpeCompra = lstOperaciones.GetListOperaciones(gcOpeTipo, chkPorFecha.Checked, dFecIni, dFecFin, cEstado, nPrvCod);

                if (dtOpeCompra.Rows.Count == 0)
                    dtOpeCompra = CreatDTListaOpeCompra();

                RellenarGrilla(ref dgvListOperCompras, dtOpeCompra, _nNroReg);
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
            lstOperaciones = null;
        }

        #endregion

        #region FUNCIONES GENERALES
        private void IniciarCampos()
        {
            HabilitarCampos(false);

            SetBotones("INI");

            txtSubTotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtFlete.Text = "0.00";
            txtFleteTra.Text = "0.00";
            txtPrecioCompra.Text = "0.00";
            txtIgv.Text = "0.00";
            txtCostoTotal.Text = "0.00";
            lblDescuentoEsp.Value = "0.00";
            txtFecIni.Text = "";
            txtFecFin.Text = "";
            txtPuntoPartida.Text = "";
            txtFecTraslado.Text = "";
            txtNotaPedidoVen.Text = "";

            ddlTipCiclo.SelectedIndex = 2;

            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtDocCli.Text = "";
            txtNroDocu.Text = "";
            txtFecha.Text = "";
            txtDireccion.Enabled = false;
            txtDocCli.Enabled = false;
            txtTelefono.Enabled = false;
            txtFecIni.Enabled = false;
            txtFecFin.Enabled = false;
            ddlEstados.Enabled = false;
            ddlProveedorFiltro.Enabled = false;

            ddlTipCiclo.Visible = false;
            txtCiclo.Visible = false;
            txtNotaPedidoVen.Visible = false;

            lblProceso.Value = "";
            lblEstado.Value = "";
            txtTipCambio.Attributes.Add("onFocus", "JavaScript:this.select();");
            txtFleteTra.Attributes.Add("onFocus", "JavaScript:this.select();");

            CargarListaOpeCompra(nNroDetPed);
            CargarEstados(ref ddlEstados);
            CargarProveedores();
            ddlEstados.SelectedValue = "R";
        }
        private void HabilitarCampos(Boolean Value)
        {
            ddlTipoDocu.Enabled = Value;
            txtNroDocu.Enabled = Value;
            txtNroSerie.Enabled = Value;
            ddlProveedor.Enabled = Value;
            txtDireccion.Enabled = Value;
            txtDocCli.Enabled = Value;
            txtTelefono.Enabled = Value;
            ddlTipoVenta.Enabled = Value;
            txtFecha.Enabled = Value;
            txtProveedor.Enabled = Value;
            ddlMoneda.Enabled = Value;
            txtTipCambio.Enabled = Value;
            txtPuntoPartida.Enabled = Value;
            txtFecTraslado.Enabled = Value;
            txtNotaPedidoVen.Enabled = Value;

            ddlTipCiclo.Enabled = Value;
            txtCiclo.Enabled = Value;
        }
        private void ConfigurarDocumento()
        {
            lblFecTraslado.Visible = false;
            txtFecTraslado.Visible = false;
            lblPuntoPartida.Visible = false;
            txtPuntoPartida.Visible = false;
            divClienteFilter.Visible = true;
            txtNotaPedidoVen.Visible = false;
            lblNotaPedidoVen.Visible = false;
            ddlProveedor.CssClass = "cssHide";

            if (lblOpeEstado.Value.Trim() == "R")
                ddlTipoVenta.Enabled = true;

            switch (int.Parse(lblTipoDoc.Value))
            {
                case 2:
                    btnImprimir.OnClientClick = "AbrirVentanaGuia()";
                    lblFecTraslado.Visible = true;
                    txtFecTraslado.Visible = true;
                    lblPuntoPartida.Visible = true;
                    txtPuntoPartida.Visible = true;
                    break;
                case 4: case 5:
                    btnImprimir.OnClientClick = "";
                    divClienteFilter.Visible = false;
                    ddlTipoVenta.SelectedValue = "CO";
                    ddlTipoVenta.Enabled = false;
                    txtNotaPedidoVen.Visible = true;
                    lblNotaPedidoVen.Visible = true;
                    ddlProveedor.CssClass = "cssShow";
                    pnDocumentos.Visible = false;
                    break;
                default:
                    btnImprimir.OnClientClick = "AbrirVentanaFactura()";
                    lblFecTraslado.Visible = false;
                    txtFecTraslado.Visible = false;
                    lblPuntoPartida.Visible = false;
                    txtPuntoPartida.Visible = false;
                    txtFecTraslado.Text = "";
                    txtPuntoPartida.Text = "";
                    break;
            }



        }
        private void AsignarFuncionBotonImprimir()
        {
            //FUNCION PARA ASIGNAR LA FUNCION JAVASCRIPT QUE VA A EJECUTAR EL BOTON IMPRIMIR
            //DEPENDE DEL TIPO DE DOCUMENTO SELECCIONADO (FACTURA, BOLETA, GUIA,...)
            try
            {

                lblTipoDoc.Value = this.LeerVariableSesion("lblTipoDoc").ToString();

                switch (lblTipoDoc.Value)
                {
                    case "2":
                        btnImprimir.OnClientClick = "AbrirVentanaGuia()";
                        break;
                    case "4":
                        btnImprimir.OnClientClick = "AbrirVentanaNota()";
                        break;
                    case "5":
                        btnImprimir.OnClientClick = "AbrirVentanaBoleta()";
                        break;
                    default:
                        btnImprimir.OnClientClick = "AbrirVentanaFactura()";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        private void SetBotones(string pcTipo)
        {
            switch (pcTipo)
            {
                case ("INI"):
                    btnNuevo.Visible = true;
                    btnSalir.Visible = true;
                    btnEditar.Visible = true;
                    btnAnular.Visible = true;
                    btnAgregar.Visible = false;
                    btnImprimir.Visible = false;
                    btnGuardar.Visible = false;
                    btnProcesar.Visible = false;
                    btnCerrar.Visible = false;
                    HabilitarBtn(btnNuevo, true);
                    HabilitarBtn(btnSalir, false);
                    HabilitarBtn(btnAnular, false);
                    HabilitarBtn(btnEditar, false);
                    pnListOperCompras.Visible = true;
                    pnResultadoOper.Visible = false;
                    pnDocCompra.Visible = false;
                    pnDocumentos.Visible = false;
                    pnMenuArticulos.Visible = false;
                    pnTipCam.Visible = false;
                    break;
                case ("NEW"):
                    btnNuevo.Visible = false;
                    btnGuardar.Visible = true;
                    btnEditar.Visible = false;
                    btnAnular.Visible = false;
                    btnAgregar.Visible = true;
                    btnProcesar.Visible = true;
                    btnImprimir.Visible = true;
                    btnCerrar.Visible = false;

                    HabilitarBtn(btnSalir, true);
                    HabilitarBtn(btnImprimir, false);
                    HabilitarBtn(btnGuardar, false);
                    HabilitarBtn(btnProcesar, false);
                    HabilitarBtn(btnEliminar, false);

                    pnListOperCompras.Visible = false;
                    pnResultadoOper.Visible = true;
                    pnDocCompra.Visible = true;
                    pnMenuArticulos.Visible = true;
                    pnDocumentos.Visible = false;
                    pnTipCam.Visible = false;
                    break;
                case ("EDIT"):
                    btnNuevo.Visible = false;
                    btnGuardar.Visible = true;
                    btnEditar.Visible = false;
                    btnAnular.Visible = false;
                    btnAgregar.Visible = true;
                    btnProcesar.Visible = true;
                    btnImprimir.Visible = true;

                    if (lblOpeEstado.Value.Trim() == "R")
                    {
                        HabilitarBtn(btnSalir, true);
                        HabilitarBtn(btnImprimir, false);
                        HabilitarBtn(btnGuardar, true);
                        HabilitarBtn(btnProcesar, true);
                        HabilitarBtn(btnEliminar, false);
                        HabilitarBtn(btnAgregar, true);
                        pnMenuArticulos.Visible = true;
                    }
                    else if (lblOpeEstado.Value.Trim() == "P")
                    {
                        btnCerrar.Visible = true;
                        btnProcesar.Visible = false;
                        HabilitarCampos(false);
                        HabilitarBtn(btnSalir, true);
                        HabilitarBtn(btnImprimir, true);
                        HabilitarBtn(btnGuardar, false);
                        HabilitarBtn(btnProcesar, false);
                        HabilitarBtn(btnEliminar, false);
                        HabilitarBtn(btnAgregar, false);
                        HabilitarBtn(btnCerrar, ddlTipoVenta.SelectedValue == "CO");
                        pnMenuArticulos.Visible = false;
                    }
                    pnListOperCompras.Visible = false;
                    pnResultadoOper.Visible = true;
                    pnDocCompra.Visible = true;
                    pnDocumentos.Visible = true;
                    pnTipCam.Visible = (ddlMoneda.SelectedValue == "USD");
                    break;
                case ("ANULAR"):
                    HabilitarBtn(btnEditar, false);
                    HabilitarBtn(btnAnular, false);
                    break;
                case ("CLOSE"):
                    btnNuevo.Visible = false;
                    btnGuardar.Visible = true;
                    btnEditar.Visible = false;
                    btnAnular.Visible = false;
                    btnAgregar.Visible = true;
                    btnImprimir.Visible = true;
                    btnCerrar.Visible = true;
                    btnProcesar.Visible = false;
                    HabilitarCampos(false);
                    HabilitarBtn(btnSalir, true);
                    HabilitarBtn(btnImprimir, true);
                    HabilitarBtn(btnGuardar, false);
                    HabilitarBtn(btnProcesar, false);
                    HabilitarBtn(btnEliminar, false);
                    HabilitarBtn(btnAgregar, false);
                    HabilitarBtn(btnNuevoDocumento, false);
                    HabilitarBtn(btnCerrar, false);
                    pnMenuArticulos.Visible = false;
                    pnListOperCompras.Visible = false;
                    pnResultadoOper.Visible = true;
                    pnDocCompra.Visible = true;
                    pnDocumentos.Visible = true;
                    pnTipCam.Visible = (ddlMoneda.SelectedValue == "USD");
                    break;


                //if (lblProceso.Value == "NEW")
                //{

                //}
                //else if (lblProceso.Value == "EDIT")
                //{

            }
        }
        protected DataTable CreatDTCabeceraCompra()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("cRazonSocial", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cDireccion", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cTelefono", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cNroGuia", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cNroPedido", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cRuc", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cFormaPago", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cSimboloMon", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cAnio", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cMes", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cDia", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cValorVenta", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cFlete", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cIGV", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cTotal", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cTotalSinFlete", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cTotalLetras", typeof(String)));
            dtResult.Columns.Add(new DataColumn("Destinatario", typeof(String)));
            dtResult.Columns.Add(new DataColumn("PLlegada", typeof(String)));
            dtResult.Columns.Add(new DataColumn("NroFactura", typeof(String)));
            dtResult.Columns.Add(new DataColumn("Unidad", typeof(String)));
            dtResult.Columns.Add(new DataColumn("dOpeTipCiclo", typeof(String)));
            dtResult.Columns.Add(new DataColumn("dOpeCiclo", typeof(String)));
            dtResult.Columns.Add(new DataColumn("PPartida", typeof(String)));
            dtResult.Columns.Add(new DataColumn("dFecTraslado", typeof(String)));


            return dtResult;
        }
        protected DataTable CreatDTListaOpeCompra()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("OpeCod", typeof(int)));
            dtResult.Columns.Add(new DataColumn("PrvRazon", typeof(String)));
            dtResult.Columns.Add(new DataColumn("OpeMoneda", typeof(String)));
            dtResult.Columns.Add(new DataColumn("OpeTotal", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("OpeFecEmision", typeof(DateTime)));
            dtResult.Columns.Add(new DataColumn("OpeEstado", typeof(String)));
            dtResult.Columns.Add(new DataColumn("AtrDescripcion", typeof(String)));
            return dtResult;
        }
        protected void GuardarDtCabeceraDocumento()
        {
            DataTable dtCabeceraOperacion = CreatDTCabeceraCompra();
            DocumenOperacion DocumenOpe = new DocumenOperacion();
            clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();

            int OpeCod = int.Parse(lblNroPedido.Text);
            string NroGuia = "", NroFactura = "", moneda = "", cPuntoPartida = "", cFecTraslado = "";

            DocumenOpe = lstDocumenOpe.GetDocumenOperacion(OpeCod, 2);
            if (DocumenOpe != null)
            {
                NroGuia = DocumenOpe.dopNroSerie.ToString() + " - " + DocumenOpe.dopNumero.ToString();
                if (DocumenOpe.dopPunPartida != null)
                    cPuntoPartida = DocumenOpe.dopPunPartida;
                if (DocumenOpe.dopFecTraslado != null)
                    cFecTraslado = ((DateTime)DocumenOpe.dopFecTraslado).ToString("yyyy-MM-dd");
                else
                    cFecTraslado = ((DateTime)DocumenOpe.dopFecEmision).ToString("yyyy-MM-dd");
            }

            DocumenOpe = lstDocumenOpe.GetDocumenOperacion(OpeCod, 3);
            if (DocumenOpe != null)
            {
                NroFactura = DocumenOpe.dopNroSerie.ToString() + " - " + DocumenOpe.dopNumero.ToString();
            }

            if (ddlMoneda.SelectedValue == "PEN")
                moneda = " Nuevos Soles";
            else
                moneda = " Dolares";

            DataRow newRow = dtCabeceraOperacion.NewRow();
            newRow["cRazonSocial"] = ddlProveedor.SelectedItem.Text;
            newRow["cDireccion"] = txtDireccion.Text;
            newRow["cNroGuia"] = NroGuia;
            newRow["cNroPedido"] = "";
            newRow["cRuc"] = txtDocCli.Text;
            newRow["cFormaPago"] = ddlTipoVenta.SelectedItem.Text;
            newRow["cSimboloMon"] = ddlMoneda.SelectedValue == "PEN" ? "S/." : "$";
            newRow["cAnio"] = DateTime.Parse(txtFecha.Text).Year.ToString();
            newRow["cMes"] = DateTime.Parse(txtFecha.Text).Month.ToString();
            newRow["cDia"] = DateTime.Parse(txtFecha.Text).Day.ToString();
            newRow["cValorVenta"] = (GetNumero(txtSubTotal.Text, false) - GetNumero(txtDescuento.Text, false)).ToString();
            newRow["cFlete"] = GetNumero(txtFlete.Text, false).ToString();
            newRow["cIGV"] = GetNumero(txtIgv.Text, false).ToString();
            newRow["cTotal"] = GetNumero(txtCostoTotal.Text, false).ToString();
            newRow["cTotalSinFlete"] = SetFormatNum(GetNumero(newRow["cValorVenta"].ToString()) * (nTasIGV + 1));
            newRow["cTotalLetras"] = ConvertiraLetras((decimal)GetNumero(txtCostoTotal.Text)).ToUpper() + moneda.ToUpper();
            newRow["Destinatario"] = ddlProveedor.SelectedItem.Text;
            newRow["PLlegada"] = txtDireccion.Text;
            newRow["NroFactura"] = NroFactura;
            newRow["Unidad"] = "";
            newRow["PPartida"] = cPuntoPartida;
            newRow["dFecTraslado"] = cFecTraslado;

            if (ddlTipoVenta.SelectedValue == "CR")
            {
                newRow["dOpeTipCiclo"] = ddlTipCiclo.SelectedItem.Text;
                newRow["dOpeCiclo"] = txtCiclo.Text;
            }
            else
            {
                newRow["dOpeTipCiclo"] = "";
                newRow["dOpeCiclo"] = "";
            }


            dtCabeceraOperacion.Rows.Add(newRow);
            AgregarVariableSession("dtCabeceraOperacion", dtCabeceraOperacion);

            DocumenOpe = null;
            lstDocumenOpe = null;

        }
        protected void CalcularPago(DataTable dtDetCompra, Boolean bChangeMoneda = true)
        {
            double lnPrecioCompra = 0, lnDescuento = 0, lnSubTotal = 0, lnIgv = 0, lnCostoTotal = 0;

            if (txtFleteTra.Text.Trim() == "")
                txtFleteTra.Text = "0.0";

            if (txtDesEspec.Text.Trim() == "")
                txtDesEspec.Text = "0.0";

            nTasIGV = double.Parse(lblTasIGV.Value);
            CalcularTotales(dtDetCompra, gcOpeTipo, GetNumero(txtDesEspec.Text), nTasIGV, Double.Parse(txtFleteTra.Text),
                ref lnPrecioCompra, ref lnDescuento, ref lnSubTotal, ref lnIgv, ref lnCostoTotal);

            txtPrecioCompra.Text = SetFormatNum(lnPrecioCompra, bChangeMoneda);
            txtDescuento.Text = SetFormatNum(lnDescuento, bChangeMoneda);
            txtIgv.Text = SetFormatNum(lnIgv, bChangeMoneda);
            txtSubTotal.Text = SetFormatNum(lnSubTotal, bChangeMoneda);
            txtFlete.Text = SetFormatNum(Double.Parse(txtFleteTra.Text), bChangeMoneda);
            txtCostoTotal.Text = SetFormatNum(lnCostoTotal, bChangeMoneda);

            Boolean bValue = dtDetCompra.Rows.Count > 0;

            HabilitarBtn(btnGuardar, bValue);
            HabilitarBtn(btnProcesar, bValue);
        }
        private Boolean ValidarDatos(string cTipo, ref string cMensaje, string pcTipoEsp = "")
        {
            Boolean bRes = true;

            switch (cTipo)
            {
                case "ART":
                    if (lsbArticulos.Items.Count == 1 && lsbArticulos.Items[0].Value == "0")
                    {
                        cMensaje = "Por favor Debe Registrar Algun articulo antes de Continuar.";
                        lsbArticulos.Focus();
                        return false;
                    }
                    if (lsbArticulos.SelectedIndex < 0)
                    {
                        cMensaje = "Debe Escoger un Articulo.";
                        lsbArticulos.Focus();
                        return false;
                    }
                    if (txtArtPreUnitario.Text.Trim() == "")
                    {
                        cMensaje = "Debe Ingresar un Precio de Compra para el Articulo.";
                        txtArtPreUnitario.Focus();
                        return false;
                    }
                    if (double.Parse(txtArtPreUnitario.Text) <= 0)
                    {
                        MessageBox("Esta Ingresando una Compra con Precio 0, Debera Actualizarlo para que la compra se Valorize. ");
                        //cMensaje = "Debe Ingresar un Precio de Compra para el Articulo.";
                        //txtArtPreUnitario.Focus();
                        //return false;
                        return true;
                    }
                    if (ddlTipDcto.SelectedValue == "%" && double.Parse(txtArtDescuento.Text) > 100)
                    {
                        cMensaje = "El Descuento no Puede superar el 100%.";
                        txtArtDescuento.Focus();
                        return false;
                    }
                    if (ddlTipDcto.SelectedValue == "S/." && double.Parse(txtArtDescuento.Text) > double.Parse(txtArtPreUnitario.Text) * double.Parse(txtArtCant.Text))
                    {
                        cMensaje = "El Descuento no Puede el total de la compra.";
                        txtArtDescuento.Focus();
                        return false;
                    }
                    if (double.Parse(txtStockFis.Text) != double.Parse(hideStockLote.Value))
                    {
                        cMensaje = "El Stock Fisico (" + txtStockFis.Text + ") y el Stock de Lotes (" + hideStockLote.Value + ") son Diferentes, Primero debe Ajustar Estas diferencias para Continuar.";
                        txtStockFis.Focus();
                        return false;
                    }
                    if (txtLotVenci.Text.Trim() == string.Empty)
                    {
                        cMensaje = "Debe Ingresar una Fecha de Vencimiento.";
                        txtLotVenci.Focus();
                        return false;
                    }
                    break;
                case "OPE":
                    if ((ddlMoneda.Items.Count == 1 && ddlMoneda.Items[0].Value == "000") ||
                    (ddlTipoVenta.Items.Count == 1 && ddlTipoVenta.Items[0].Value == "000") ||
                    (ddlZonas.Items.Count == 0))
                    {
                        MessageBox("Por favor Registrar los tipos de Moneda, Tipo de Zonas y Tipo de Pago antes de Continuar. ");
                        return false;
                    }
                    if (ddlProveedor.SelectedValue == "000")
                    {
                        cMensaje = "Debe Escoger un Proveedor";
                        ddlProveedor.Focus();
                        return false;
                    }
                    if (ddlMoneda.SelectedValue == "000")
                    {
                        cMensaje = "Debe Escoger la Moneda";
                        ddlMoneda.Focus();
                        return false;
                    }
                    if (ddlTipoVenta.SelectedValue == "000")
                    {
                        cMensaje = "Debe Escoger el Tipo de Pago";
                        ddlTipoVenta.Focus();
                        return false;
                    }
                    if (pcTipoEsp == "PROC" && ddlTipoVenta.SelectedValue == "CR" && txtCiclo.Text == "")
                    {
                        cMensaje = "Debe Indicar el Ciclo de la Compra";
                        txtCiclo.Focus();
                        return false;
                    }
                    if (lblTipoDoc.Value == "2" && txtFecTraslado.Text == "")
                    {
                        cMensaje = "Ingrese una Fecha de Traslado";
                        txtCiclo.Focus();
                        return false;
                    }
                    if ((lblTipoDoc.Value == "4" || lblTipoDoc.Value == "5") && txtNotaPedidoVen.Text == "")
                    {
                        cMensaje = "Debe Ingresar el Numero de Pedido del Vendedor";
                        txtNotaPedidoVen.Focus();
                        return false;
                    }
                    

                    
                    // DataRow[] Filas = g_dtDetOperacion.Select("dtpSubTotal = 0");
                    //if (Filas.Count() > 0)
                    //{
                    //    MessageBox("Tiene " + Filas.Count().ToString() + " Detalles con Precio 0, No se puede Procesar la Compra.");
                    //    return false;
                    //}


                    break;
                default:
                    break;
            }


            return bRes;
        }
        private void CargarCamposOperacion(int _OpeCod)
        {
            clsOperaciones lstOperaciones = new clsOperaciones();
            Operaciones Operacion = new Operaciones();

            try
            {
                Operacion = lstOperaciones.GetOperacion(_OpeCod);

                //DATOS DE LA NUEVA OPERACION
                lbltcmCod.Value = Operacion.tcmCod.ToString();
                lblTipCambio.Value = Operacion.TipoCambios.tcmCambio.ToString();
                txtTipCambio.Text = lblTipCambio.Value;
                g_nTipoCambio = (Double)Operacion.TipoCambios.tcmCambio;

                lblNroPedido.Text = Operacion.OpeCod.ToString();
                txtFecha.Text = ((DateTime)Operacion.OpeFecEmision).ToString("yyyy-MM-dd");
                ddlZonas.SelectedValue = Operacion.ZonCod.ToString().Trim();
                if (lblTipoDoc.Value == "4" || lblTipoDoc.Value == "5")
                {
                    ddlProveedor.SelectedValue = Operacion.PerCod.ToString();
                }
                else
                {
                    ddlProveedor.SelectedValue = Operacion.PrvCod.ToString();
                }                
                ddlMoneda.SelectedValue = Operacion.OpeMoneda.ToString().Trim();
                ddlTipoVenta.SelectedValue = Operacion.OpeTipPago.ToString().Trim();
                txtPrecioCompra.Text = SetFormatNum((double)Operacion.OpeSubTotal);
                txtDescuento.Text = SetFormatNum((double)Operacion.OpeDscto);
                txtFlete.Text = SetFormatNum((double)Operacion.OpeFlete);
                txtSubTotal.Text = SetFormatNum((double)(Operacion.OpeTotal - Operacion.OpeImpuesto));
                txtIgv.Text = SetFormatNum((double)Operacion.OpeImpuesto);
                txtCostoTotal.Text = SetFormatNum((double)Operacion.OpeTotal);
                lblOpeEstado.Value = Operacion.OpeEstado.ToString();
                txtDesEspec.Text = SetFormatNum((double)Operacion.OpeDscto);
                if (Operacion.OpeCiclo != null)
                    if (Operacion.OpeTipPago == "CR" && Operacion.OpeCiclo == 0)
                        txtCiclo.Text = "";
                    else
                        txtCiclo.Text = Operacion.OpeCiclo.ToString();
                else
                    txtCiclo.Text = "0";

                if (Operacion.OpeTipCiclo != null)
                    ddlTipCiclo.SelectedValue = Operacion.OpeTipCiclo;

                if (ddlTipoVenta.SelectedValue == "CR")
                {
                    txtCiclo.Visible = true;
                    ddlTipCiclo.Visible = true;
                }
                else
                {
                    txtCiclo.Visible = false;
                    ddlTipCiclo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
            lstOperaciones = null;
            Operacion = null;
        }
        private void CargarCamposDocumento(int _dopCod, string cDopDescri)
        {
            clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();
            DocumenOperacion DocumenOpe = new DocumenOperacion();

            try
            {
                DocumenOpe = lstDocumenOpe.GetDocumenOperacion(_dopCod);

                lblTipoDoc.Value = ddlTipoDocu.SelectedValue;
                lblDescriDocumento.Text = cDopDescri;

                lblTipoDoc.Value = DocumenOpe.tdoCod.ToString();
                lbldopCod.Value = DocumenOpe.dopCod.ToString();
                lblNroDocumento.Text = DocumenOpe.dopNroSerie.ToString() + " - " + DocumenOpe.dopNumero.ToString();
                txtFecha.Text = ((DateTime)DocumenOpe.dopFecEmision).ToString("yyyy-MM-dd");
                txtNotaPedidoVen.Text = DocumenOpe.dopDocComple;
                if (DocumenOpe.dopFecTraslado != null)
                    txtFecTraslado.Text = ((DateTime)DocumenOpe.dopFecTraslado).ToString("yyyy-MM-dd");
                else
                    txtFecTraslado.Text = ((DateTime)DocumenOpe.dopFecEmision).ToString("yyyy-MM-dd");

                if (DocumenOpe.dopPunPartida != null)
                    txtPuntoPartida.Text = DocumenOpe.dopPunPartida;
                else
                    txtPuntoPartida.Text = "";

                ConfigurarDocumento();
                
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
            lstDocumenOpe = null;
            DocumenOpe = null;
        }
        private void SetPanelDocumento(int nOpeCod)
        {
            clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();

            dgvDocumentos.DataSource = lstDocumenOpe.GetListDocumenOperacion(nOpeCod);
            dgvDocumentos.DataBind();

            switch (lblTipoDoc.Value)
            {
                case "2":
                    btnNuevoDocumento.Text = "Canjear Factura";
                    HabilitarBtn(btnNuevoDocumento, true);
                    break;
                case "3":
                    btnNuevoDocumento.Text = "Canjear Guia";
                    HabilitarBtn(btnNuevoDocumento, true);
                    break;
                default:
                    btnNuevoDocumento.Text = "Canjear Factura";
                    HabilitarBtn(btnNuevoDocumento, false);
                    break;
            }
            HabilitarBtn(btnNuevoDocumento, dgvDocumentos.Rows.Count < 2);
            lstDocumenOpe = null;
        }

        #endregion

        #region FUNCIONES DE MANTENIMIENTO DE PROVEEDORES
        private void IniciarCamposProveedor()
        {
            txtCliCod.Text = "";
            txtNombre.Text = "";
            txtNroDoc.Text = "";
            txtCliDireccion.Text = "";
            txtCliTelefono.Text = "";
            txtRepresentante.Text = "";
            txtFecRegis.Text = "";
            lblDocumento.Text = "Nro. de RUC: ";
            lblNombre.Text = "Razon Social: ";
            lblRequired.Visible = false;
            HabilitarCamposProveedor(false);
        }
        private void HabilitarCamposProveedor(Boolean Value)
        {
            txtNombre.Enabled = Value;
            txtCliDireccion.Enabled = Value;
            txtCliTelefono.Enabled = Value;
            txtRepresentante.Enabled = Value;
            txtFecRegis.Enabled = Value;
            txtNroDoc.Enabled = Value;

            btnNuevoProveedor.Visible = !Value;
            btnGuardarProveedor.Visible = Value;
        }
        protected void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            HabilitarCamposProveedor(true);
            txtFecRegis.Text = DateTime.Today.ToString("yyyy-MM-dd");
            ddlProveedor.SelectedValue = "000";

            clsProveedores lstProveedor = new clsProveedores();
            int nPrvCod = lstProveedor.MaxPrvCod() + 1;
            txtCliCod.Text = nPrvCod.ToString().PadLeft(6, '0');
            lstProveedor = null;

            ModalPopupNuevoPrv.Show();
        }
        protected void btnCancelarProveedor_Click(object sender, EventArgs e)
        {
            IniciarCamposProveedor();
            ddlProveedor.SelectedValue = "000";
            ModalPopupNuevoPrv.Show();
            txtProveedor.Text = "";
        }
        protected void btnGuardarProveedor_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                clsProveedores lstProveedor = new clsProveedores();

                lblRequired.Visible = false;
                lstProveedor.Insert(this); //GUARDAR PROVEEDOR
                lstProveedor = null;

                CargarProveedores();
                ddlProveedor.SelectedValue = int.Parse(txtCliCod.Text).ToString();
                ddlProveedor_SelectedIndexChanged(sender, e);
            }
            else
            {
                lblRequired.Visible = true;
                ModalPopupNuevoPrv.Show();
            }

        }
        protected void VentaProveedor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = (args.Value.Trim() != "");
            }
            catch (Exception ex)
            {
                args.IsValid = false;
                MessageBox(ex.Message);
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

        #region FUNCIONES DE VENTANA DE ARTICULOS
        private void LimpiarCamposArticulos()
        {
            txtArtCod.Text = "";
            txtArtUniMed.Text = "";
            txtStockFact.Text = "";
            txtStockFis.Text = "";
            txtArtPreUnitario.Text = "0.0";
            txtArtDescuento.Text = "0";
            txtArtCant.Text = "0";
            txtImpTotal.Text = "0.0";
            txtLotVenci.Text = "";
            txtLotNro.Text = "";
        }
        private void IniciarCamposArticulos(string cTipo = "NORMAL")
        {
            LimpiarCamposArticulos();

            lblEstadoVenArticulo.Value = cTipo;
            txtArtPreUnitario.Attributes.Add("onFocus", "JavaScript:this.select();");
            txtArtDescuento.Attributes.Add("onFocus", "JavaScript:this.select();");
            txtArtCant.Attributes.Add("onFocus", "JavaScript:this.select();");
            txtImpTotal.Attributes.Add("onFocus", "JavaScript:this.select();");

            clsProveedores lstProveedores = new clsProveedores();
            ddlLaboratorios.DataSource = lstProveedores.GetAll();
            ddlLaboratorios.DataBind();
            ddlLaboratorios.Items.Insert(0, new ListItem("Todos", "-1"));

            clsArticulos oArticulos = new clsArticulos();
            lsbArticulos.DataSource = oArticulos.GetAll();
            lsbArticulos.DataBind();

            if (lsbArticulos.Items.Count == 0)
            {
                lsbArticulos.Items.Add(new ListItem("No existen Articulos en Stock", "0"));
            }

            if (cTipo == "ESPECIAL")
            {
                TablaArtLeft.Visible = false;

                txtArtPreUnitario.Enabled = true;
                txtArtCant.Enabled = false;
                txtLotVenci.Enabled = false;
                btnGuardarPrecio.Visible = true;
                btnAgregarArticulo.Visible = false;
            }
            else
            {
                TablaArtLeft.Visible = true;

                txtArtCant.Enabled = true;
                txtLotVenci.Enabled = true;
                btnGuardarPrecio.Visible = false;
                btnAgregarArticulo.Visible = true;
            }
        }
        private void CalcularTotalArticulo()
        {
            double nTotal = 0.0;
            if (txtArtDescuento.Text == "")
            {
                txtArtDescuento.Text = "0.00";
            }
            if (txtArtPreUnitario.Text.Trim() != "" && txtArtCant.Text.Trim() != "")
            {
                nTotal = double.Parse(txtArtPreUnitario.Text) * double.Parse(txtArtCant.Text);
                nTotal -= nTotal * (double.Parse(txtArtDescuento.Text) / 100);
                txtImpTotal.Text = Math.Round(nTotal, 2).ToString();
            }
            ModalPopupAgregar.Show();
        }
        private Boolean BuscarArticulo(int ArtCod, ref int LotNro, ref double LprPrecio, ref double LprDscto)
        {
            DataTable dtDetalleCompra = g_dtDetOperacion;
            if (dtDetalleCompra != null)
            {
                var Rows = dtDetalleCompra.Select("ArtCod = " + ArtCod.ToString());
                if (Rows.Length > 0)
                {
                    LotNro = int.Parse(Rows[0]["LotNro"].ToString());
                    LprPrecio = double.Parse(Rows[0]["dtpPrecioVen"].ToString());
                    LprDscto = double.Parse(Rows[0]["dtpDscto"].ToString());
                    return true;
                }
            }
            return false;
        }
        protected void lsbArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ArtCod = 0;
            int LotNro = 0;
            double LprPrecio = 0.0;
            double LprDscto = 0.0;
            double nTipCam = g_nTipoCambio;

            if (lsbArticulos.SelectedValue != "")
                ArtCod = int.Parse(lsbArticulos.SelectedValue);

            clsArticulos lstArticulos = new clsArticulos();
            clsListaPrecios lstPrecios = new clsListaPrecios();
            clsLotesArt lstLotesArt = new clsLotesArt();
            Articulos oArticulo = new Articulos();
            ListaPrecios oPrecio = new ListaPrecios();

            oPrecio = lstPrecios.GetArticuloPrecio(ArtCod);
            oArticulo = lstArticulos.GetArticulo(ArtCod);

            if (oArticulo != null)
            {
                txtArtCod.Text = oArticulo.ArtCod.ToString();
                txtArtUniMed.Text = oArticulo.Unidades.UniDescripcion;
                txtStockFact.Text = oArticulo.ArtStockFac.ToString();
                txtStockFis.Text = oArticulo.ArtStock.ToString();
                txtArtDescuento.Text = (oArticulo.Proveedores.PrvDscto).ToString();
                ddlTipDcto.SelectedValue = "%";
                txtLotVenci.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                ddlLaboratorios.SelectedValue = oArticulo.PrvCod.ToString();
                hideStockLote.Value = lstLotesArt.GetLoteArtStock(ArtCod).ToString();

                if (oArticulo.ArtStock <= 0)
                {
                    txtStockFis.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtStockFis.ForeColor = System.Drawing.Color.Gray;
                }

                if (oPrecio == null)
                {
                    txtArtPreUnitario.Text = "0.0";
                }
                else
                {
                    txtArtPreUnitario.Text = Math.Round(((Double)oPrecio.LprPrecio / nTipCam),2).ToString();
                }

                if (BuscarArticulo(oArticulo.ArtCod, ref LotNro, ref LprPrecio, ref LprDscto))
                {
                    if (lblEstadoVenArticulo.Value == "NORMAL")
                        txtArtPreUnitario.Enabled = false;
                    else
                        txtArtPreUnitario.Enabled = true;
                    txtLotNro.Enabled = false;
                    txtLotNro.Text = LotNro.ToString();
                    txtArtPreUnitario.Text = Math.Round((LprPrecio / nTipCam), 2).ToString();
                }
                else
                {
                    txtArtPreUnitario.Enabled = true;
                    txtLotNro.Enabled = true;
                    LotNro = lstLotesArt.MaxLotNro(ArtCod);
                    txtLotNro.Text = (LotNro + 1).ToString();
                }
                if (lblEstadoVenArticulo.Value == "NORMAL")
                    txtArtCant.Text = "1";
            }
            else
                LimpiarCamposArticulos();

            txtImpTotal.Text = "0.0";

            lstArticulos = null;
            oArticulo = null;
            lstPrecios = null;
            oPrecio = null;
            lstLotesArt = null;

            CalcularTotalArticulo();
            lsbArticulos.Focus();

        }
        protected void ddlLaboratorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsArticulos oArticulos = new clsArticulos();
            int PrvCod = Int32.Parse(ddlLaboratorios.SelectedValue);
            
            if (PrvCod == -1)
                lsbArticulos.DataSource = oArticulos.GetAll();
            else
                lsbArticulos.DataSource = oArticulos.GetArticulosByPrvCod(PrvCod);
            lsbArticulos.DataBind();
            if (lsbArticulos.Items.Count == 0)
            {
                lsbArticulos.Items.Add(new ListItem("Este Proveedor no tiene Articulos", "0"));
            }
            oArticulos = null;
            if (lblTipoDoc.Value != "4" && lblTipoDoc.Value != "5")
            {
                ddlProveedor.SelectedValue = ddlLaboratorios.SelectedValue;
                ddlProveedor_SelectedIndexChanged(sender, e);
            }
            
            ModalPopupAgregar.Show();
        }
        protected void btnGuardarPrecio_Click(object sender, EventArgs e)
        {
            DataTable dtDetalleCompra;
            string cMensaje = "";
            long ndopCod = 0;
            int ntcmCod = 0;
            double nTipCam = g_nTipoCambio;
            clsDetOperacion colDetOper = new clsDetOperacion();
            clsOperaciones colOper = new clsOperaciones();
            try
            {
                if (ValidarDatos("ART", ref cMensaje))
                {
                    //AGREGAMOS ARTICULO A LISTA DE ARTICULOS
                    dtDetalleCompra = g_dtDetOperacion;

                    if (dtDetalleCompra.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDetalleCompra.Rows.Count; i++)
                        {
                            if (dtDetalleCompra.Rows[i]["ArtCod"].ToString() == txtArtCod.Text)
                            {
                                dtDetalleCompra.Rows[i]["dtpPrecioVen"] = Double.Parse( txtArtPreUnitario.Text) * nTipCam;
                                dtDetalleCompra.Rows[i]["dtpDscto"] = txtArtDescuento.Text;
                                dtDetalleCompra.Rows[i]["dtpSubTotal"] = Double.Parse(txtImpTotal.Text) * nTipCam;
                                break;
                            }
                        }
                    }

                    CalcularPago(dtDetalleCompra);
                    g_dtDetOperacion = dtDetalleCompra;
                    DataTable dtDetOper = CopiarDT(dtDetalleCompra);
                    dtDetOper = CambiarMonedaDetOperacion(dtDetOper);
                    RellenarGrilla(ref dgvDetalleVenta, dtDetOper, this.nNroDetPed);

                    colDetOper.UpdatePrecio(this.nOpeCod, int.Parse(txtArtCod.Text), (Decimal)(Double.Parse(txtArtPreUnitario.Text) * nTipCam));

                    colOper.Guardar(this, gcOpeTipo, ref ndopCod, ref ntcmCod);  //GUARDAR OPERACION    
                    lbldopCod.Value = ndopCod.ToString();
                    lbltcmCod.Value = ntcmCod.ToString();
                }
                else
                {
                    ModalPopupAgregar.Show();
                    MessageBox(cMensaje);
                }

            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
        }
        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            DataTable dtDetalleCompra;
            DataRow NewRow;
            bool bExiste = false;
            double nPrecio = 0.0;
            double nCantidad = 0.0;
            double nDescuento = 0.0;
            double nTotal = 0.0;
            string cMensaje = "";
            double nTipCam = g_nTipoCambio;

            clsListaPrecios lstPrecios = new clsListaPrecios();
            clsArticulos colArticulos = new clsArticulos();
            ListaPrecios oPrecio = new ListaPrecios();

            try
            {
                if (ValidarDatos("ART", ref cMensaje))
                {
                    //AGREGAMOS ARTICULO A LISTA DE ARTICULOS
                    dtDetalleCompra = g_dtDetOperacion;

                    if (dtDetalleCompra.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDetalleCompra.Rows.Count; i++)
                        {
                            if (dtDetalleCompra.Rows[i]["ArtCod"].ToString() == txtArtCod.Text)
                            {
                                nPrecio = Double.Parse(dtDetalleCompra.Rows[i]["dtpPrecioVen"].ToString());
                                nCantidad = Double.Parse(dtDetalleCompra.Rows[i]["dtpCantidad"].ToString()) + double.Parse(txtArtCant.Text);
                                nTotal = Double.Parse(dtDetalleCompra.Rows[i]["dtpSubTotal"].ToString());
                                nTotal += Double.Parse(txtImpTotal.Text) * nTipCam;
                                //nDescuento = 100 - ((100 * nTotal) / ((nPrecio * nTipCam) * nCantidad));
                                //nDescuento = Math.Round(nDescuento, 2);
                                nDescuento = Double.Parse(txtArtDescuento.Text);

                                dtDetalleCompra.Rows[i]["dtpCantidad"] = nCantidad.ToString();
                                dtDetalleCompra.Rows[i]["dtpDscto"] = nDescuento;
                                dtDetalleCompra.Rows[i]["dtpSubTotal"] = nTotal.ToString();
                                i = dtDetalleCompra.Rows.Count + 10;
                                bExiste = true;
                            }
                        }
                    }

                    if (!bExiste)
                    {
                        NewRow = dtDetalleCompra.NewRow();
                        NewRow["ArtCod"] = txtArtCod.Text;
                        NewRow["ArtPeso"] = colArticulos.GetArticulo(int.Parse(txtArtCod.Text)).ArtPeso;
                        NewRow["LotNro"] = txtLotNro.Text;
                        NewRow["ArtDescripcion"] = lsbArticulos.SelectedItem.Text;
                        NewRow["UniAbrev"] = txtArtUniMed.Text;
                        NewRow["dtpCantidad"] = txtArtCant.Text;
                        NewRow["dtpPrecioVen"] = Double.Parse(txtArtPreUnitario.Text) * nTipCam;
                        if (ddlTipDcto.SelectedValue == "%")
                            NewRow["dtpDscto"] = txtArtDescuento.Text;
                        else
                            NewRow["dtpDscto"] = (double.Parse(txtArtDescuento.Text) * 100) / (double.Parse(txtArtPreUnitario.Text) * double.Parse(txtArtCant.Text));
                        NewRow["dtpSubTotal"] = Double.Parse(txtImpTotal.Text)*nTipCam;
                        NewRow["LotFecVenci"] = txtLotVenci.Text==""?DateTime.Today.AddMonths(1):DateTime.Parse(txtLotVenci.Text);
                        dtDetalleCompra.Rows.Add(NewRow);
                    }

                    CalcularPago(dtDetalleCompra);
                    g_dtDetOperacion = dtDetalleCompra;
                    DataTable dtDetOper = CopiarDT(dtDetalleCompra);
                    dtDetOper = CambiarMonedaDetOperacion(dtDetOper);
                    RellenarGrilla(ref dgvDetalleVenta, dtDetOper, this.nNroDetPed);

                    //LIMPIAR BUSQUEDA
                    txtBuscarArt.Text = string.Empty;
                }
                else
                {
                    ModalPopupAgregar.Show();
                    MessageBox(cMensaje);
                }

            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
            lstPrecios = null;
            oPrecio = null;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static String[] BuscarArticulos(string prefixText)
        {
            String[] sList = null;
            List<string> sArticuloList = new List<string>();
            clsArticulos lstArticulos = new clsArticulos();

            try
            {
                DataTable dtArticulos = lstArticulos.BuscarArticulos(prefixText);

                if (dtArticulos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtArticulos.Rows.Count; i++)
                    {
                        sArticuloList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem(dtArticulos.Rows[i]["ArtDescripcion"].ToString(), dtArticulos.Rows[i]["ArtCod"].ToString()));
                    }
                    sList = new String[10];
                    sList = sArticuloList.ToArray();
                }
                else
                {
                    sList = new String[1];
                    sList[0] = "Sin Resultados";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lstArticulos = null;
            }
            return sList;
        }

        #endregion

       



    }
}

