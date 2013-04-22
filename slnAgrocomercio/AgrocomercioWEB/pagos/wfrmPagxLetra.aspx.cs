using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Text;
using pryAgrocomercioBLL.EntityCollection;
//using pryAgrocomercioBLL.Clases;

using pryAgrocomercioDAL;
using System.Globalization;
using System.Configuration;

namespace AgrocomercioWEB.pagos
{
    public partial class wfrmPagxLetra : BasePage
    {
        public String _click = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int _selectedIndex = 0;
            
            if (Page.IsPostBack)
            {
                if (lblEstado.Value != "")
                {
                    switch (lblEstado.Value)
                    {
                        case ("DGVCOM_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            //if (dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvDetalleVenta.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            //{
                            //    dgvDetalleVenta.SelectedIndex = _selectedIndex;
                            //    HabilitarBtn(btnEliminar, true);
                            //}
                            //else
                            //{
                            //    dgvDetalleVenta.SelectedIndex = -1;
                            //    HabilitarBtn(btnEliminar, false);
                            //}

                            break;
                        case ("DGVLIS_SIMPLECLICK"):
                            _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                            if (dgvListOperLetras.Rows[_selectedIndex].Cells[2].Text.Trim() != "&nbsp;" && dgvListOperLetras.Rows[_selectedIndex].Cells[2].Text.Trim() != "")
                            {
                                dgvListOperLetras.SelectedIndex = _selectedIndex;
                                
                                HabilitarBtn(btnEditar, true);
                            }
                            else
                            {
                                dgvListOperLetras.SelectedIndex = -1;
                                
                                HabilitarBtn(btnEditar, false);
                            }

                            break;
                      
                        //case ("DGVPLA_DOUBLECLICK"):
                        //    _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());
                        //    dgvPlatos.SelectedIndex = _selectedIndex;
                        //    txtCodPlato.Text = dgvPlatos.Rows[_selectedIndex].Cells[2].Text;
                        //    txtPlaCantidad.Text = "1";
                        //    HabilitarBtn(btnAgregar, true);
                        //    AgregarPlato(int.Parse(txtCodPlato.Text), 1);
                        //    break;

                    }
                    lblEstado.Value = "";
                }
            }
            else
            {
                IniciarCampos();
            }

        }

        #region FUNCIONES DEL FORMULARIO

        protected void dgvDetalleVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string _cError = "";
            //try
            //{
            //    foreach (GridViewRow row in dgvDetalleVenta.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            foreach (TableCell c in row.Cells)
            //            {
            //                if (c != row.Cells[0] && c != row.Cells[1])
            //                {
            //                    c.Attributes["onclick"] = "dgvDetalleVentaClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
            //                    c.Attributes["ondblclick"] = "dgvDetalleVentaClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _cError = ex.Message;
            //}
        }

        protected void dgvListOperLetras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvListOperLetras.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvListOperLetrasClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvListOperLetrasClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
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
                foreach (GridViewRow row in dgvListOperLetras.Rows)
                {
                }

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
                btgenCuotas.Visible = true;
                btgenCuotas.Enabled = true;
            }
            catch 
            {
                txtNumCuotas.Text = "";
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

        protected void grdchknota_CheckedChanged(object sender, EventArgs e)
        {
            string _cError = "";
            Double total_acum = 0.0;
            try
            {
                foreach (GridViewRow row in dgvNotas.Rows)
                {
                    //CheckBox chk_Publicar = (CheckBox)row.Cells[1].Controls[0];
                    CheckBox check = row.FindControl("grdchknota") as CheckBox;
                    if (check.Checked)
                    {

                        total_acum = total_acum + Convert.ToDouble(row.Cells[5].Text);
                    }
                }
                lbresta.Text = (total_acum * -1).ToString();
                CalculartotalPago();
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                HabilitarCampos(true);
                
                lblProceso.Value = "NEW";

                IniciarLetra();


            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnImprimir.Visible = true;
            HabilitarBtn(btnImprimir, true);
            int nOpeCod = 0;            
            try
            {
                HabilitarCampos(true);
                             
                nOpeCod = int.Parse(dgvListOperLetras.Rows[dgvListOperLetras.SelectedIndex].Cells[2].Text);
                lblNroDocumento.Text = dgvListOperLetras.Rows[dgvListOperLetras.SelectedIndex].Cells[2].Text;
                txtFecha.Text  = dgvListOperLetras.Rows[dgvListOperLetras.SelectedIndex].Cells[4].Text;
                txtFecha.Enabled = false;
                lblProveedor.Text = dgvListOperLetras.Rows[dgvListOperLetras.SelectedIndex].Cells[6].Text;

                txtNumCuotas.Text = dgvListOperLetras.Rows[dgvListOperLetras.SelectedIndex].Cells[7].Text;
                clsCabletra lstOperaciones = new clsCabletra();
                cab_letra obj = new cab_letra();
                obj = lstOperaciones.GetCabLetra(nOpeCod);
                int icodLetra = int.Parse(obj.icodigo.ToString());
                /////////////////////carga facturas enlazadas
                clsfac_x_letra form = new clsfac_x_letra();
                DataTable dtFact;
                dtFact = form.GetList(icodLetra);
                dgvListFact.DataSource = dtFact;
                dgvListFact.DataBind();
                grdchk_CheckedChanged(sender, e);
                dgvListFact.Enabled = false;

                dgvNotas.Columns[7].Visible = false;
                dgvNotas.Columns[6].Visible = false;
                /////////////////////carga Notas enlazadas
                clsviewrel_not_fac formNot = new clsviewrel_not_fac();
                DataTable dtNot;
                dtNot = formNot.GetListNotas(icodLetra);                
                if (dtNot.Rows.Count == 0)
                    pnnotasempty.Visible = true;
                else {
                    pnaddNotas.Visible = true;
                    pnnotasempty.Visible = false;
                    dgvNotas.DataSource = dtNot;
                    dgvNotas.DataBind();
                    grdchknota_CheckedChanged(sender, e);
                    dgvNotas.Enabled = false;
                    

                }
                /////////////////////carga detalle de letras
                clsdetletra formDet = new clsdetletra();
                DataTable dtDetalle;

                dtDetalle = formDet.GetList(icodLetra);
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
                ////////////////////////////////////////////////////
                this.pnListOperaciones.Visible = false;
                this.pnDocLetra.Visible = true;
                pnCuotas.Visible = true;
                pnListOperLetras.Visible = true;
                pnCuotas.Visible = true;
                Pnokaddfactura.Visible = true;
                
                pnaddfactura.Visible = true;
                pnNotas.Visible = true;
                btnCancelar.Visible = true;
                HabilitarBtn(btnNuevo, false);
                HabilitarBtn(btnCancelar, true);
                HabilitarBtn(btnAnular, false);
                HabilitarBtn(btnEditar, false);
                HabilitarBtn(btnImprimir, true);
                ///////////////////////////////////////////////////

                lblPaso.Value = "Detalle";
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {  
                IniciarCampos();
        }
      

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //string ArtCod = dgvDetalleVenta.Rows[dgvDetalleVenta.SelectedIndex].Cells[2].Text;

            //DataTable dtDetalleCompra;

            //try
            //{
            //    dtDetalleCompra = LeerDetalleCompra();
            //    if (dtDetalleCompra.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtDetalleCompra.Rows.Count; i++)
            //        {
            //            if (dtDetalleCompra.Rows[i]["ArtCod"].ToString() == ArtCod.ToString())
            //            {
            //                dtDetalleCompra.Rows.Remove(dtDetalleCompra.Rows[i]);
            //            }
            //        }
            //    }

            //    CalcularPago(dtDetalleCompra);
            //    RellenarDetalleVenta(dtDetalleCompra, this.nNroDetPed);
            //    dgvDetalleVenta.SelectedIndex = -1;
            //    HabilitarBtn(btnEliminar, false);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox(ex.Message);
            //}
        }
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
            btnGuardar.Visible = true;


        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnGuardarDocu_Click(object sender, EventArgs e)
        {
            //clsArticulos lstArticulos = new clsArticulos();
            //clsOpePagos lstOperaciones = new clsOpePagos();
            //clsDocumenOperacion lstDocumenOpe = new clsDocumenOperacion();
            //clsDetOperacion lstDetOperacion = new clsDetOperacion();

            //Operaciones Operacion = new Operaciones();
            //DocumenOperacion DocumenOpe = new DocumenOperacion();

            //long nOpeCod = 0;
            //long nDopCod = 0;
            //long nDtpCod = 0;

            //int NroImpre = 0;

            //try
            //{
            //    DataTable dtDetalleOper = LeerDetalleCompra();
            //    txtDescuento.Text = txtDesEspec.Text;
            //    CalcularPago(dtDetalleOper);

            //    nOpeCod = long.Parse(lblNroPedido.Text);
            //    if (lblProceso.Value == "NEW")
            //    {
            //        Operacion = new Operaciones();
            //        DocumenOpe = new DocumenOperacion();
            //        nDopCod = lstDocumenOpe.MaxDopCod() + 1;
            //        lbldopCod.Value = nDopCod.ToString();
            //        NroImpre = 0;
            //    }
            //    else if (lblProceso.Value == "EDIT")
            //    {
            //        Operacion = lstOperaciones.GetOperacion(int.Parse(lblNroPedido.Text));
            //        nDopCod = int.Parse(lbldopCod.Value);
            //        DocumenOpe = lstDocumenOpe.GetDocumenOperacion((int)nDopCod);
            //        NroImpre = (int)DocumenOpe.dopNroImpre;
            //        lstDetOperacion.DeleteDetOperacion((int)nOpeCod);
            //    }

            //    //DATOS DE LA NUEVA OPERACION               
            //    Operacion.OpeCod = nOpeCod;
            //    Operacion.OpeTipo = "C";
            //    Operacion.OpeFecEmision = DateTime.Parse(txtFecha.Text);
            //    Operacion.OpeFecCancela = DateTime.Parse(txtFechaVenc.Text);
            //    Operacion.OpePersona = 0;
            //    Operacion.ZonCod = int.Parse(ddlZonas.SelectedValue.Trim());
            //    Operacion.PerCod = int.Parse(ddlVendedores.SelectedValue);
            //    Operacion.TraCod = int.Parse(ddlTransportistas.SelectedValue);
            //    Operacion.PrvCod = int.Parse(ddlProveedor.SelectedValue);
            //    Operacion.OpeMoneda = ddlMoneda.SelectedValue.Trim();
            //    Operacion.OpeTipPago = ddlTipoVenta.SelectedValue.Trim();
            //    Operacion.OpeSubTotal = decimal.Parse(txtSubTotal.Text.Replace("S/.", "").Trim());
            //    Operacion.OpeDscto = decimal.Parse(txtDescuento.Text.Replace("S/.", "").Trim());
            //    Operacion.OpeImpuesto = decimal.Parse(txtIgv.Text.Replace("S/.", "").Trim());
            //    Operacion.OpeFlete = decimal.Parse(txtFlete.Text.Replace("S/.", "").Trim());
            //    Operacion.OpeTotal = decimal.Parse(txtTotal.Text.Replace("S/.", "").Trim());
            //    Operacion.OpeEstado = "R";
            //    Operacion.OpeModifica = DateTime.Now;
            //    Operacion.UsrCod = 999;

            //    //DATOS DE NUEVO DOCUMENTO DE OPERACION
            //    DocumenOpe.dopCod = nDopCod;
            //    DocumenOpe.dopNroSerie = lblNroDocumento.Text.Split('-')[0].Trim();
            //    DocumenOpe.dopNumero = lblNroDocumento.Text.Split('-')[1].Trim();
            //    DocumenOpe.OpeCod = nOpeCod;
            //    DocumenOpe.tdoCod = int.Parse(lblTipoDoc.Value);
            //    DocumenOpe.dopMoneda = ddlMoneda.SelectedValue;
            //    DocumenOpe.dopNroImpre = NroImpre;
            //    DocumenOpe.dopFecEmision = DateTime.Parse(txtFecha.Text);
            //    DocumenOpe.dopFecCancela = DateTime.Parse(txtFechaVenc.Text);
            //    DocumenOpe.dopEstado = "A";

            //    //if (lblProceso.Value == "NEW")
            //    //{
            //    //    lstOperaciones.Add(Operacion);
            //    //    lstDocumenOpe.Add(DocumenOpe);
            //    //}
            //    //else if (lblProceso.Value == "EDIT")
            //    //{
            //    //    lstOperaciones.Update(Operacion);
            //    //    lstDocumenOpe.Update(DocumenOpe);
            //    //}

            //    //DATOS DEL DETALLE DE LA OPERACION
            //    nDtpCod = lstDetOperacion.MaxDtpCod();

            //    foreach (DataRow row in dtDetalleOper.Rows)
            //    {
            //        DetOperacion DetOper = new DetOperacion();
            //        nDtpCod++;
            //        DetOper.dtpCod = nDtpCod;
            //        DetOper.OpeCod = nOpeCod;
            //        DetOper.ArtCod = int.Parse(row["ArtCod"].ToString());
            //        DetOper.dtpCantidad = decimal.Parse(row["dtpCantidad"].ToString());
            //        DetOper.dtpPrecioVen = decimal.Parse(row["dtpPrecioVen"].ToString());
            //        DetOper.dtpDscto = decimal.Parse(row["dtpDscto"].ToString());
            //        DetOper.dtpSubTotal = decimal.Parse(row["dtpSubTotal"].ToString());
            //        DetOper.UniCod = lstArticulos.GetUnidadCod((int)DetOper.ArtCod);
            //        DetOper.dtpEstado = true;
            //        lstDetOperacion.Add(DetOper);
            //    }

            //    lstOperaciones.SaveChanges();
            //    lstDocumenOpe.SaveChanges();
            //    lstDetOperacion.SaveChanges();

            //    HabilitarBtn(btnProcesar, true);

            //    if (sender != btnProcesar)
            //    {
            //        MessageBox("La Operacion de Compra se Guardo con Exito ");
            //    }

            //    lblProceso.Value = "EDIT";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox("Error Interno: " + ex.Message);
            //}
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            //    clsArticulos lstArticulos = new clsArticulos();
            //    clsOpePagos lstOperaciones = new clsOpePagos();

            //    Operaciones Operacion = new Operaciones();
            //    Articulos Articulo = new Articulos();


            //    try
            //    {
            //        btnGuardarDocu_Click(sender, e);

            //        DataTable dtDetalleOper = LeerDetalleCompra();
            //        Operacion = lstOperaciones.GetOperacion(int.Parse(lblNroPedido.Text));

            //        Operacion.OpeEstado = "C";
            //        Operacion.OpeModifica = DateTime.Now;
            //        lstOperaciones.Update(Operacion);

            //        //DATOS DEL DETALLE DE LA OPERACION
            //        foreach (DataRow row in dtDetalleOper.Rows)
            //        {
            //            Articulo = lstArticulos.GetArticulo(int.Parse(row["ArtCod"].ToString()));
            //            Articulo.ArtStock += decimal.Parse(row["dtpCantidad"].ToString());
            //            lstArticulos.Update(Articulo);
            //        }

            //        lstOperaciones.SaveChanges();
            //        lstArticulos.SaveChanges();

            //        HabilitarBtn(btnGuardar, false);
            //        HabilitarBtn(btnProcesar, false);
            //        HabilitarBtn(btnAgregar, false);
            //        HabilitarBtn(btnImprimir, true);
            //        pnMenuArticulos.Visible = false;
            //        MessageBox("La Operacion fue Procesada con Exito, Ahora puede imprimir.");
            //        lblProceso.Value = "EDIT";
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox("Error Interno: " + ex.Message);
            //    }
        }


        #endregion

        #region FUNCIONES DE CARGA DE LISTAS DE DATOS
         
       
       
        protected void CargarListaOpeLetras(int _nNroReg)
        {
            clsList_letra lstOperaciones = new clsList_letra();
            DataTable dtOpeLetras;
            dtOpeLetras = lstOperaciones.GetListOperaciones("C");

            if (dtOpeLetras.Rows.Count > 0)
            {
                DataRow newRow;
                if (_nNroReg > 0)
                {
                    for (int i = dtOpeLetras.Rows.Count; i < _nNroReg; i++)
                    {
                        newRow = dtOpeLetras.NewRow();
                        for (int j = 0; j < dtOpeLetras.Columns.Count; j++)
                        {
                            newRow[j] = DBNull.Value;
                        }
                        dtOpeLetras.Rows.Add(newRow);
                    }
                }

                dgvListOperLetras.DataSource = dtOpeLetras;
                dgvListOperLetras.DataBind();
            }
            
        }
        protected Boolean CargarFactPendientes(int _nNroReg, int op, string _moneda)  
        {
            Boolean valid;
            clsFac_pnd_let lstOperaciones = new clsFac_pnd_let();
            DataTable dtFactPen;
            dtFactPen = lstOperaciones.GetList(op, "C", _moneda);

            if (dtFactPen.Rows.Count == 0)
            {
                dtFactPen = CreatDTListaOpeCompra();
                Pnokaddfactura.Visible = false;
                Pnerraddfactura.Visible = true;
                valid = false;
            }
            else
            {
                Pnokaddfactura.Visible = true;
                Pnerraddfactura.Visible = false;
                valid = true;
            }

            if (op == 0)
            {
                
            }
            else
            {

                dgvListFact.DataSource = dtFactPen;
                dgvListFact.DataBind();
            }
            return valid;

        }
        protected Boolean CargarNotasDisp(int _nNroReg, int op)
        {
            clsOpeNotas lstOperaciones = new clsOpeNotas();
            DataTable dtNotasPen;
            dtNotasPen = lstOperaciones.GetListNotas(op, "C", "PEN");

            if (dtNotasPen.Rows.Count == 0)
            {
                pnaddNotas.Visible = false;
                pnnotasempty.Visible = true;
                return false;
            }
            else
            {
                pnaddNotas.Visible = true;
                pnnotasempty.Visible = false;
                dgvNotas.DataSource = dtNotasPen;
                dgvNotas.DataBind();
                return true;
            }






        }
        #endregion

        #region FUNCIONES GENERALES
        private void IniciarCampos()
        {
            HabilitarCampos(false);
            CargarListaOpeLetras(nNroDetPed);
            
            btnNuevo.Visible = false;
            btnCancelar.Visible = false;
            btnEditar.Visible = true;
            btnAnular.Visible = false;
            btnImprimir.Visible = false;
            btnGuardar.Visible = false;
            btnProcesar.Visible = false;

            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnImprimir, false);
           
            lblDescuentoEsp.Value = "0.00";

            txtNumCuotas.Enabled = false;


           
                       txtFecha.Text = "";
            lblProceso.Value = "";
            lblEstado.Value = "";

            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnAnular, false);
            btnImprimir.Visible = false;
            pnDocLetra.Visible = false;
            pnListOperaciones.Visible = true;
            pnCuotas.Visible = false;
            

        }
        private void HabilitarCampos(Boolean Value)
        {
           
            txtFecha.Enabled = Value;

            Pnokaddfactura.Visible = false;
            Pnerraddfactura.Visible = false;
            pnaddNotas.Visible = false;
            pnnotasempty.Visible = false;
        }

        protected DataTable CreatDTDetalleCompra()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("Cuota", typeof(int)));
            dtResult.Columns.Add(new DataColumn("txtFecVen", typeof(String)));
            dtResult.Columns.Add(new DataColumn("NumLetra", typeof(String)));
            dtResult.Columns.Add(new DataColumn("cod_unic", typeof(String)));
            dtResult.Columns.Add(new DataColumn("monto", typeof(String)));

            return dtResult;
        }
        protected DataTable CreatDTListaCuotas()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("ArtCod", typeof(int)));
            return dtResult;
        }
        protected DataTable CreatDTListaOpeCompra()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("numfac", typeof(int)));
            dtResult.Columns.Add(new DataColumn("Let.dopCod", typeof(int)));
            dtResult.Columns.Add(new DataColumn("PrvRazon", typeof(String)));
            dtResult.Columns.Add(new DataColumn("opetotpagar", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("dfecemision", typeof(DateTime)));
            //dtResult.Columns.Add(new DataColumn("OpeEstado", typeof(String)));

            return dtResult;
        }
      


        protected void RellenarDetalleVenta(DataTable dtDetVenta, int _nNroReg)
        {
            //DataRow newRow;
            //if (_nNroReg > 0)
            //{
            //    for (int i = dtDetVenta.Rows.Count; i < _nNroReg; i++)
            //    {
            //        newRow = dtDetVenta.NewRow();
            //        for (int j = 0; j < dtDetVenta.Columns.Count; j++)
            //        {
            //            newRow[j] = DBNull.Value;
            //        }
            //        dtDetVenta.Rows.Add(newRow);
            //    }
            //}

            //dgvDetalleVenta.DataSource = dtDetVenta;
            //dgvDetalleVenta.DataBind();
        }

        protected void CalculartotalPago()
        {
            Double debe, haber;
            debe = Convert.ToDouble(lbSuma.Text);
            haber = Convert.ToDouble(lbresta.Text);
            lbpago.Text = (debe + haber).ToString();
        }
        protected void CalcularPago(DataTable dtDetCompra)
        {

            double nValorCompra = 0.0;
            double nSubTotal = 0.0;
            double nIGV = 0.0;
            double nDescuento = double.Parse(lblDescuentoEsp.Value);
            
            double nTotal = 0.0;

            int i = 0;
            string TipCalIGV = "";

            if (dtDetCompra.Rows.Count > 0)
            {
                for (i = 0; i < dtDetCompra.Rows.Count; i++)
                {
                    nSubTotal += Double.Parse(dtDetCompra.Rows[i]["dtpCantidad"].ToString()) * Double.Parse(dtDetCompra.Rows[i]["dtpPrecioVen"].ToString());
                    nValorCompra += Double.Parse(dtDetCompra.Rows[i]["dtpSubTotal"].ToString());
                }
            }
            else
                nIGV = 0.0;

            TipCalIGV = ConfigurationManager.AppSettings["TipoCalculoIGV"];

            if (TipCalIGV == "1")
            {
                nDescuento += (nSubTotal - nValorCompra);
              
                nIGV = Math.Round(nValorCompra * nTasIGV, 2);
                nTotal = Math.Round(nValorCompra + nIGV, 2);
            }
            else
            {
                double nTotSinDes = nSubTotal;
                double nTotConDes = nValorCompra;
                nDescuento += (nTotConDes * 100 / 118) * (1 - (nTotConDes / nTotSinDes));
                nSubTotal = (nTotConDes * 100 / 118) + nDescuento;

  

            }

          
            Boolean bValue = dtDetCompra.Rows.Count > 0;

            HabilitarBtn(btnGuardar, bValue);

        }
        private Boolean ValidarDatos(string cTipo, ref string cMensaje)
        {
            Boolean bRes = true;

            return bRes;
        }

        private void CargarCamposOperacion(int _OpeCod)
        {
           
        }

        private void CargarCamposDocumento(int _dopCod, string cDopDescri)
        {
           
        }
        private void IniciarLetra()
        {
            this.pnListOperaciones.Visible = false;
            this.pnDocLetra.Visible = true;
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnCancelar, true);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnEditar, false);

            clsLetra lstLetras = new clsLetra();
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            lblNroDocumento.Text = Rellenartexto("0", (lstLetras.MaxOpeCod()).ToString(), 8, 0);
           
        }
        #endregion

        #region FUNCIONES DE MANTENIMIENTO DE PROVEEDORES
        private void IniciarCamposProveedor()
        {
            
        }

        private void HabilitarCamposProveedor(Boolean Value)
        {
                   }

      

         #endregion

        #region FUNCIONES DE VENTANA DE ARTICULOS
  
       
       
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
        #region FUNCIONES VARIOS
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
        #endregion
    }
}