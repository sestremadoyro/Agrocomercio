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
using pryAgrocomercioBLL.Maestros;

namespace AgrocomercioWEB.Maestros.WebForms
{
    public partial class wfrmArticulos : BasePage 
    {
        #region  metodos privados de EVENTOS
        private void MostrarOcultar()
        {
            Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
            if (!objUsuario.Roles.rolOpcionModificar.Value)
            {

                btnGuardar.Visible = false;
                btnGuadarPrecio.Visible = false;

                gvwListaPrecios.Visible = false;
                ddlProveedor.Enabled = false;
                ddlUnidadMedida.Enabled = false;
                txtCostoPromedio.Visible = false;
                lblCostoPromedio.Visible = false;
                pnlListaPrecios.Visible = false;

            }
            if (!objUsuario.Roles.rolOpcionCrear.Value)
            {
                btnNuevoPrecio.Visible = false;
                imbNuevoProducto.Visible = false;

                btnNuevo.Visible = false;
            }
        }
        private void EventoLlenarListaProveedores()
        {
            CProveedor oProveedor = new CProveedor();
            DataTable dtListaProveedores = oProveedor.fnListaProveedores(false );
            ddlProveedor.DataSource = dtListaProveedores;
            ddlProveedor.DataBind();
        }
        private void EventoLlenarLista()
        {

            CUnidad oUnidad = new CUnidad();
            DataTable  dtListaUnidades = (DataTable) oUnidad.fnListaDescripcionUnidades();
            ddlUnidadMedida.DataSource = dtListaUnidades;
            ddlUnidadMedida.DataBind();
        }

        private void EventoEliminar(int proCodigo)
        {
            clsLotesArt oLotesArtLst = new clsLotesArt();
            var lstPreciosLotes = oLotesArtLst.GetListaLotes(proCodigo);

            if (lstPreciosLotes.Count > 0)
            {
                lblMensajes.Text = "No se pudo ELIMINAR el registro, tiene Precios Asignados.";
                this.MessageBox("No se pudo ELIMINAR el registro, tiene Precios Asignados.");
            }
            else
            {

                CArticulo oArticulo = new CArticulo();
                int nResp = oArticulo.fnArticuloDelete(proCodigo);
                if (nResp <= 0)
                {
                    lblMensajes.Visible = true;
                    if (oArticulo.NroError == 547)
                    {
                        lblMensajes.Text = "No se pudo ELIMINAR el registro, tiene Precios Asignados.";
                        this.MessageBox("No se pudo ELIMINAR el registro, tiene Precios Asignados.");
                    }
                    else
                    {
                        lblMensajes.Text = oArticulo.DescripcionError;
                        this.MessageBox(oArticulo.DescripcionError);
                    }
                }
                else
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El registro se Eliminó Satisfactoriamente.";
                    this.MessageBox("El registro se Eliminó Satisfactoriamente.");

                    string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                    EventoBuscarProductos(sBuscado);

                }
            }
        }
        private void EventoGuardar(bool  pDesactivar)
        {
            int nResp;
            int linOperacion = (int)LeerVariableSesion("operacion");
            CArticulo oArticulo = new CArticulo();
            if (string.IsNullOrEmpty(txtCodigo.Text))
                oArticulo.ArtCod = 0;
            else
                oArticulo.ArtCod = Convert.ToInt32(txtCodigo.Text);

            oArticulo.ArtDescripcion = txtDescripcion.Text.ToUpper();
            oArticulo.ArtStock = Convert.ToDecimal(txtStock.Text);
            //oArticulo.ArtStockMax = Convert.ToDecimal(txtStockMax.Text);
            //oArticulo.ArtStockMin = Convert.ToDecimal(txtStockMin.Text);
            oArticulo.ArtCostoProm = Convert.ToDecimal(txtCostoPromedio.Text);
            if (linOperacion == 1)        //NUEVO
            {
                oArticulo.ArtFecRegis = DateTime.Today;
                oArticulo.ArtStockFac = 0;
            }
            oArticulo.ArtFecVen = Convert.ToDateTime(txtFecVencimiento.Text);
            oArticulo.ArtFecModi = DateTime.Today;
            oArticulo.UniCod = Convert.ToInt32(ddlUnidadMedida.SelectedItem.Value);
            oArticulo.ArtEstado = chkEstado.Checked;
            oArticulo.ArtPeso = Convert.ToDecimal(txtPeso.Text);
            oArticulo.ArtStockIni = Convert.ToDecimal(txtStockIni.Text);

            if (ddlProveedor.Items.Count > 0)
                oArticulo.PrvCod = Convert.ToInt32(ddlProveedor.SelectedItem.Value);
            else
                oArticulo.PrvCod = 0;

            if (linOperacion == 1)        //NUEVO
            {
                nResp = oArticulo.fnArticuloInsert();
                //btnNuevoPrecio.Visible = true;
            }
            else                        // MODIFICAR
                nResp = oArticulo.fnArticuloUpdate();

            if (nResp <= 0)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = oArticulo.DescripcionError;
                this.MessageBox(oArticulo.DescripcionError);
            }
            else
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                
                this.MessageBox("El registro se Grabo Satisfactoriamente.");
                if (pDesactivar)
                {
                    pnlEditProductos.Visible = false;
                    pnlBusqueda.Visible = true;
                    pnlListaPrecios.Visible = false;
                    pnlEditProductos.Visible = false;
                    pnlTipoProductos.Visible = true;
                    pnlProductos.Visible = true;

                    string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                    EventoBuscarProductos(sBuscado);
                }
            }

        }
        private void EventoCancelar(int pOperacion)
        {

            txtDescripcion.Text = String.Empty;
            txtCostoPromedio.Text = String.Empty;
            txtStock.Text = String.Empty;
            //txtStockMax.Text = "0";
            //txtStockMin.Text = "0";
            txtCostoPromedio.Text = "0";
            txtFecVencimiento.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtPeso.Text = "0";
            txtStockIni.Text = "0";
            chkEstado.Checked = true;

            EventoLlenarLista();
            EventoLlenarListaProveedores();

            lblMensajes.Visible = false;

            //nuevo
            if (pOperacion == 1)
            {
                pnlEditProductos.Visible = false;
                pnlBusqueda.Visible = true;
                pnlProductos.Visible = false;
                pnlListaPrecios.Visible = false;
                pnlDetallePrecio.Visible = false;
            }
            else   //modificar
            {
                string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                EventoBuscarProductos(sBuscado);

                pnlEditProductos.Visible = false;
                pnlProductos.Visible = true;
                pnlBusqueda.Visible = true;
                pnlListaPrecios.Visible = false;
                pnlDetallePrecio.Visible = false;

            }
        }
        private void EventoBuscarProductos(string pBuscado)
        {
            //string sBuscado = txtBusqueda.Text.ToUpper();
            string sCadenaFiltro = "artDescripcion like '%" + pBuscado + "%'";
            //DataTable con todos los productos
            CArticulo objArticulo = new CArticulo();

            DataTable dtProductos = (DataTable) objArticulo.fnListaArticulos(chkOpcionTodos.Checked );
            DataTable dtResultado;
            //DataTable con los productos ya filtrados
            DataView oDataView = new DataView(dtProductos);
            oDataView.RowFilter = sCadenaFiltro;
            dtResultado = oDataView.ToTable();

            gvwProductos.DataSource = dtResultado;
            gvwProductos.DataBind();
        }


        private void CargarProductoNuevo(CArticulo pObjArticulo)
        {
            txtCodigo.Text = pObjArticulo.ArtCod.ToString();
            txtDescripcion.Text = pObjArticulo.ArtDescripcion;
            txtStock.Text = pObjArticulo.ArtStock.ToString();
            //txtStockMax.Text = pObjArticulo.ArtStockMax.ToString();
            //txtStockMin.Text = pObjArticulo.ArtStockMin.ToString();
            txtFecVencimiento.Text = pObjArticulo.ArtFecRegis.ToString("yyyy-MM-dd");;
            txtCostoPromedio.Text = pObjArticulo.ArtCostoProm.ToString();
            txtPeso.Text = pObjArticulo.ArtPeso.ToString();
            txtStockIni.Text = pObjArticulo.ArtStockIni.ToString();

            txtDescripcion.Focus();
        }
        private void EventoNuevoProducto()
        {
            txtCodigo.Text = "0";
            txtDescripcion.Text = String.Empty;
            txtCostoPromedio.Text = String.Empty;
            txtStock.Text = "0";
            //txtStockMax.Text = "0";
            //txtStockMin.Text = "0";
            txtCostoPromedio.Text = "0";
            txtFecVencimiento.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtPeso.Text = "0";
            txtStockIni.Text = "0";
            chkEstado.Checked = true;

            //CONSULTAR PRECIOS Y LOTES DEL PRODUCTO
            MostrarPreciosLotes(0);

            EventoLlenarLista();
            EventoLlenarListaProveedores();

            pnlEditProductos.Visible = true;
            pnlBusqueda.Visible = false;
            pnlProductos.Visible = false;
            pnlListaPrecios.Visible = true;
            pnlDetallePrecio.Visible = false;

            // primero debe grabar articulo, luego puede ingresar precios , lotes
            btnNuevoPrecio.Visible = false;

            //NUEVO , se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;

            string sBuscado = txtBusqueda.Text.ToUpper().Trim();
            this.AgregarVariableSession("sBuscado", sBuscado);

        }
        private void EventoModificarProducto()
        {

            EventoLlenarLista();
            EventoLlenarListaProveedores();

            int linProCodigo;
            linProCodigo = (int)LeerVariableSesion("proCodigo");

            DataRow drArticulo;
            CArticulo objArticulo = new CArticulo();
            drArticulo = objArticulo.fnDatosArticulo(linProCodigo);

            Articulos oArticulo = new Articulos();
            clsArticulos lstArticulos = new clsArticulos();

            oArticulo = lstArticulos.GetArticulo(linProCodigo);
            this.AgregarVariableSession("oArticulo", oArticulo);


            if (drArticulo != null)
            {
                txtCodigo.Text = drArticulo["artCod"].ToString();
                txtDescripcion.Text = drArticulo["artDescripcion"].ToString();

                //grabamos el stock del producto
                decimal ldeStockProducto = 0;
                if (drArticulo["artStock"] != null)
                    ldeStockProducto = Convert.ToDecimal( drArticulo["artStock"]);

                txtStock.Text = ldeStockProducto.ToString();
                this.AgregarVariableSession("stockProducto", ldeStockProducto);

                //txtStockMax.Text = drArticulo["artStockMax"].ToString();
                //txtStockMin.Text = drArticulo["artStockMin"].ToString();
                txtFecVencimiento.Text = drArticulo["artFecVen"].ToString();
                txtCostoPromedio.Text = drArticulo["artCostoProm"].ToString();
                txtPeso.Text = drArticulo["artPeso"].ToString();
                chkEstado.Checked = Convert.ToBoolean( drArticulo["artEstado"]);
                txtStockIni.Text = drArticulo["artStockIni"].ToString();

                ListItem liElegido;
                //si DropDownList está visible
                if (ddlUnidadMedida.Items.Count > 0)
                {
                    liElegido = ddlUnidadMedida.Items.FindByValue(drArticulo["UniCod"].ToString());
                    ddlUnidadMedida.SelectedIndex = ddlUnidadMedida.Items.IndexOf(liElegido);
                }

                ListItem liElegidoProveedor;
                //si DropDownList está visible
                if (ddlProveedor.Items.Count > 0)
                {
                    liElegidoProveedor = ddlProveedor.Items.FindByValue(drArticulo["PrvCod"].ToString());
                    ddlProveedor.SelectedIndex = ddlProveedor.Items.IndexOf(liElegidoProveedor);
                }

                //CONSULTAR PRECIOS Y LOTES DEL PRODUCTO
                MostrarPreciosLotes(linProCodigo);

                //MODIFICAR , se guarda en variable cache
                AgregarVariableSession("operacion", 2);
                pnlBusqueda.Visible = false;
                pnlProductos.Visible = false;
                pnlEditProductos.Visible = true;
                pnlListaPrecios.Visible = true;
                pnlDetallePrecio.Visible = false;
                lblMensajes.Visible = false;

                //btnNuevoPrecio.Visible = true;
            }
        }
        private decimal  CalcularStock(int pArtCod)
        {
            Decimal decStockTotal = 0;
            int linProCodigo = (int)LeerVariableSesion("proCodigo");
            clsLotesArt oLotesArtLst = new clsLotesArt();
            var lstPreciosLotes = oLotesArtLst.GetListaLotes(linProCodigo);

            decStockTotal = (decimal)lstPreciosLotes.Sum(Lo => Lo.LotStock);
            return decStockTotal;
        }

        private void MostrarPreciosLotes(int pArtCod)
        {
            clsLotesArt oLotesArtLst = new clsLotesArt();
            gvwListaPrecios.DataSource = oLotesArtLst.GetListaLotes(pArtCod);
            gvwListaPrecios.DataBind();

        }
        private void EventoGuardarPrecio()
        {

            int linOperacion = (int)LeerVariableSesion("operacionPrecio");

            if (linOperacion == 1)        //NUEVO
            {
                int linProCodigo = (int)LeerVariableSesion("proCodigo");

                clsLotesArt oLotesArtLst = new clsLotesArt();
                LotesArt oLotesArt = new LotesArt();
              
                int linLotCod = oLotesArtLst.MaxLotesCod() + 1;
                int linNroLot = oLotesArtLst.MaxLotNro(linProCodigo) + 1;

                oLotesArt.LotStock = Convert.ToDecimal(txtStockLote.Text);
                oLotesArt.LotFecVenci = DateTime.Parse( txtFecVenceLote.Text);
                oLotesArt.LotCod = linLotCod;
                oLotesArt.ArtCod = linProCodigo;
                oLotesArt.LotPrecioCom = 0;
                //oLotesArt.LotPrecioVen = Convert.ToDecimal(txtDescuento.Text) - Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDescuento.Text)/100;
                oLotesArt.LotPrecioVen = Convert.ToDecimal(txtPrecio.Text);
                oLotesArt.LotNro = Convert.ToInt32(txtLote.Text);
                oLotesArt.LotFecModi = DateTime.Today;
                oLotesArt.LotFecRegis = DateTime.Today;
                oLotesArt.LotEstado = "A";

                try
                {
                    oLotesArtLst.Add(oLotesArt);
                    oLotesArtLst.SaveChanges();

                    RestaurarDatosProducto_sesion();
                    txtStock.Text = CalcularStock(linProCodigo).ToString();

                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                    this.MessageBox("El registro se Grabo Satisfactoriamente.");
                    pnlBusqueda.Visible = false;
                    //pnlTipoProductos.Visible = false;
                    pnlProductos.Visible = false;
                    pnlEditProductos.Visible = true;
                    pnlListaPrecios.Visible = true;
                    pnlDetallePrecio.Visible = false;
                }
                catch (Exception ex)
                {
                    lblMensajes.Text = "Error:" + ex.Message;
                    this.MessageBox("Error:" + ex.Message);
                }
                
            }
            else             // MODIFICAR
            {
                int linProCodigo = (int)LeerVariableSesion("proCodigo");
                int loteCodigo = (int)LeerVariableSesion("loteCodigo");

                LotesArt oLotesArt = new LotesArt();
                clsLotesArt oLotesArtLst = new clsLotesArt();
                oLotesArt = oLotesArtLst.GetLoteArt(linProCodigo,loteCodigo);
               
                oLotesArt.LotPrecioVen = Convert.ToDecimal(txtDescuento.Text) - Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDescuento.Text) / 100;
                oLotesArt.LotStock = Convert.ToDecimal(txtStockLote.Text);
                oLotesArt.LotFecVenci = DateTime.Parse( txtFecVenceLote.Text);

                try 
                {
                    oLotesArtLst.Update(oLotesArt);
                    oLotesArtLst.SaveChanges();

                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                    this.MessageBox("El registro se Grabo Satisfactoriamente.");
                    pnlBusqueda.Visible = false;
                    //pnlTipoProductos.Visible = false;
                    pnlProductos.Visible = false;
                    pnlEditProductos.Visible = true;
                    pnlListaPrecios.Visible = true;
                    pnlDetallePrecio.Visible = false;

                    RestaurarDatosProducto_sesion();

                    txtStock.Text = CalcularStock(linProCodigo).ToString();
                }
                catch (Exception ex)
                {
                    lblMensajes.Text = "Error:" + ex.Message;
                    this.MessageBox("Error:" + ex.Message);
                }

            }

        }
        private void RestaurarDatosProducto_sesion()
        {
            EventoLlenarLista();
            EventoLlenarListaProveedores();

            Articulos oArticulo = new Articulos();
            oArticulo = (Articulos)LeerVariableSesion("oArticulo");

            if (oArticulo != null)
            {
                txtCodigo.Text = oArticulo.ArtCod.ToString();
                txtDescripcion.Text = oArticulo.ArtDescripcion;
                txtStock.Text = oArticulo.ArtStock.ToString();
                //txtStockMax.Text = drArticulo["artStockMax"].ToString();
                //txtStockMin.Text = drArticulo["artStockMin"].ToString();
                txtFecVencimiento.Text = oArticulo.ArtFecVen.ToString();
                txtCostoPromedio.Text = oArticulo.ArtCostoProm.ToString();
                txtStockIni.Text = oArticulo.ArtStockIni.ToString();
                chkEstado.Checked = oArticulo.Artestado;

                ListItem liElegido;
                //si DropDownList está visible
                if (ddlUnidadMedida.Items.Count > 0)
                {
                    liElegido = ddlUnidadMedida.Items.FindByValue(oArticulo.UniCod.ToString());
                    ddlUnidadMedida.SelectedIndex = ddlUnidadMedida.Items.IndexOf(liElegido);
                }

                ListItem liElegidoProveedor;
                //si DropDownList está visible
                if (ddlProveedor.Items.Count > 0)
                {
                    liElegidoProveedor = ddlProveedor.Items.FindByValue(oArticulo.PrvCod.ToString());
                    ddlProveedor.SelectedIndex = ddlProveedor.Items.IndexOf(liElegidoProveedor);
                }

                //CONSULTAR PRECIOS Y LOTES DEL PRODUCTO
                MostrarPreciosLotes(oArticulo.ArtCod);

                //pnlBusqueda.Visible = false;
                //pnlProductos.Visible = false;
                //pnlEditProductos.Visible = true;
                //pnlListaPrecios.Visible = true;
                //pnlDetallePrecio.Visible = false;
                //lblMensajes.Visible = false;
            }
        }
        private void EventoCancelarPrecio()
        {


            RestaurarDatosProducto_sesion();

            //txtCodigoPrecio.Text = "0";
            //txtPrecio.Text = "0";
            //txtDescuento.Text = "0";
            //txtLote.Text = "0";
            //txtStockLote.Text = "0";
            //txtFecVenceLote.Text = DateTime.Today.ToString(); ;
            //chkEstadoPrecio.Checked = true ;
            //lblCodigoLote.Text = "0";

            lblMensajes.Visible = false;

            pnlBusqueda.Visible = false;
            pnlProductos.Visible = false;
            pnlEditProductos.Visible = true;
            pnlListaPrecios.Visible = true;
            pnlDetallePrecio.Visible = false;
            lblMensajes.Visible = false;
        }
        #endregion
        #region  metodos del formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                pnlBusqueda.Visible = true;
                pnlEditProductos.Visible = false;
                pnlProductos.Visible = false;
                pnlListaPrecios.Visible = false;
                pnlDetallePrecio.Visible = false;
                MostrarOcultar();
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EventoGuardar(true );
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int linOperacion = (int)LeerVariableSesion("operacion");

            EventoCancelar(linOperacion);
            MostrarOcultar();
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            EventoNuevoProducto();
        }
        protected void gvwProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaProducto");
                lbtEliminar.Attributes.Add("onclick", "return confirm('El Registro elegido será eliminado. ¿Desea Continuar?');");

            }
            //Agregando atributos a los gridview para entorno grafico
            if ((e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff';this.style.color='';");

                if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#eeeeee';this.style.color='';");

                if (e.Row.RowState == DataControlRowState.Selected)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';this.style.color='#8E4436';");
            }
        }
        protected void gvwProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaProducto")
            {

                int linProCodigo;
                linProCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("proCodigo", linProCodigo);
                AgregarVariableSession("operacion", 2);
                EventoModificarProducto();
                MostrarOcultar();
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaProducto")
            {
                int linProCodigo;

                linProCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linProCodigo);
            }
        }
        private void GuardarDatosProducto_Sesion()
        {
            int linOperacion = (int)LeerVariableSesion("operacion");
            Articulos oArticulo = new Articulos();
            if (string.IsNullOrEmpty(txtCodigo.Text))
                oArticulo.ArtCod = 0;
            else
                oArticulo.ArtCod = Convert.ToInt32(txtCodigo.Text);

            oArticulo.ArtDescripcion = txtDescripcion.Text.ToUpper();
            oArticulo.ArtCostoProm = Convert.ToDecimal( txtCostoPromedio.Text);
            oArticulo.Artestado = chkEstado.Checked;
            oArticulo.ArtStock = Convert.ToDecimal( txtStock.Text);
            oArticulo.ArtStockIni = Convert.ToDecimal(txtStockIni.Text);
            //oArticulo.ArtStockMax = Convert.ToDecimal(txtStockMax.Text);
            //oArticulo.ArtStockMin = Convert.ToDecimal(txtStockMin.Text);
            oArticulo.ArtFecVen = DateTime.Parse(txtFecVencimiento.Text);
            if (linOperacion == 1)        //NUEVO
                oArticulo.ArtFecRegis = DateTime.Today;
            oArticulo.UniCod = Convert.ToInt32(ddlUnidadMedida.SelectedItem.Value);
            if (ddlProveedor.Items.Count > 0)
                oArticulo.PrvCod = Convert.ToInt32(ddlProveedor.SelectedItem.Value);
            else
                oArticulo.PrvCod = 0;

            AgregarVariableSession("nuevoProducto", oArticulo);
        }


        #endregion


        protected void imbBuscarEspecial_Click(object sender, ImageClickEventArgs e)
        {
            string sBuscado = txtBusqueda.Text.ToUpper();
            this.AgregarVariableSession("sBuscado", sBuscado);
            EventoBuscarProductos(sBuscado);
            pnlProductos.Visible = true;
            pnlEditProductos.Visible = false;
            pnlBusqueda.Visible = true;
            pnlListaPrecios.Visible = false;
            pnlDetallePrecio.Visible = false;
            lblMensajes.Visible = false;
        }

        protected void imbNuevoProducto_Click(object sender, ImageClickEventArgs e)
        {
            
            EventoNuevoProducto();
        }
        protected void imbBuscarEspecial_Init(object sender, EventArgs e)
        {
            imbBuscarEspecial.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            imbBuscarEspecial.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");

        }
        protected void imbNuevoProducto_Init(object sender, EventArgs e)
        {
            imbNuevoProducto.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            imbNuevoProducto.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");

        }
        protected void gvwProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwProductos.PageIndex = e.NewPageIndex;
            string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
            EventoBuscarProductos(sBuscado);
            //gvwProductos.DataBind();

        }

        protected void gvwListaPrecios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaPrecio");
                lbtEliminar.Attributes.Add("onclick", "return confirm('El Registro elegido será eliminado. ¿Desea Continuar?');");

            }
            //Agregando atributos a los gridview para entorno grafico
            if ((e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff';this.style.color='';");

                if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#eeeeee';this.style.color='';");

                if (e.Row.RowState == DataControlRowState.Selected)
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';this.style.color='#8E4436';");
            }

        }

        protected void gvwListaPrecios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwProductos.PageIndex = e.NewPageIndex;
            gvwProductos.DataBind();
        }

        protected void gvwListaPrecios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int linProCodigo = (int)LeerVariableSesion("proCodigo");
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaPrecio")
            {

                int LoteCodigo;
                LoteCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("loteCodigo", LoteCodigo);
                AgregarVariableSession("operacionPrecio", 2);

                clsLotesArt oLotesArtList = new clsLotesArt();
                LotesArt oLotesArt = new LotesArt();
                oLotesArt = oLotesArtList.GetLoteArt(linProCodigo,LoteCodigo);

                if (oLotesArt != null)
                {

                    txtCodigoPrecio.Text = oLotesArt.LotCod.ToString();
                    txtPrecio.Text = oLotesArt.LotPrecioVen.ToString();
                    txtDescuento.Text = "0.0";
                    txtLote.Text = oLotesArt.LotNro.ToString();
                    //grabamos el stock del lote
                    decimal ldeStockLote = 0;
                    if (oLotesArt.LotStock.HasValue ) 
                       ldeStockLote = oLotesArt.LotStock.Value;

                    txtStockLote.Text = ldeStockLote.ToString();
                    this.AgregarVariableSession("stockLote", ldeStockLote);

                    DateTime FechaVenci ;

                    FechaVenci = oLotesArt.LotFecVenci.Value;

                    txtFecVenceLote.Text = FechaVenci.ToString("yyyy-MM-dd");
                    if (oLotesArt.LotEstado == "A")
                        chkEstadoPrecio.Checked = true;
                    else
                        chkEstadoPrecio.Checked = false;

                    lblCodigoLote.Text = oLotesArt.LotCod.ToString();

                    pnlBusqueda.Visible = false;
                    pnlProductos.Visible = false;
                    pnlEditProductos.Visible = false ;
                    pnlListaPrecios.Visible = false;
                    pnlDetallePrecio.Visible = true;
                    lblMensajes.Visible = false;

                    GuardarDatosProducto_Sesion();
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaPrecio")
            {
                int LoteCodigo;
                LoteCodigo = Convert.ToInt32(e.CommandArgument);

                clsLotesArt oLotesArtList = new clsLotesArt();
                LotesArt oLotesArt = new LotesArt();
                oLotesArt = oLotesArtList.GetLoteArt(linProCodigo, LoteCodigo);
                oLotesArt.LotEstado = "I";
                oLotesArt.LotStock = 0;
                oLotesArt.LotPrecioCom = 0;
                oLotesArt.LotPrecioVen = 0;

                try
                {
                    oLotesArtList.Update(oLotesArt);
                    oLotesArtList.SaveChanges();

                    RestaurarDatosProducto_sesion();

                    txtStock.Text = CalcularStock(linProCodigo).ToString();
                    EventoGuardar(false );

                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El registro se Elimino Satisfactoriamente.";
                    this.MessageBox("El registro se Elimino Satisfactoriamente.");
                }
                catch (Exception ex)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = ex.Message;
                    this.MessageBox(ex.Message);
                }
            }
        }

        protected void btnGuardarPrecio_Click(object sender, EventArgs e)
        {
            EventoGuardarPrecio();
            EventoGuardar(false);
        }

        protected void btnCancelarPrecio_Click(object sender, EventArgs e)
        {
           
            EventoCancelarPrecio();
        }

        protected void btnNuevoPrecio_Click(object sender, EventArgs e)
        {
            AgregarVariableSession("operacionPrecio", 1);
            int linProCodigo = (int)LeerVariableSesion("proCodigo");

            clsLotesArt oLotesArtLst = new clsLotesArt();

            int linLotCod = oLotesArtLst.MaxLotesCod();
            int linNroLot = oLotesArtLst.MaxLotNro(linProCodigo) + 1;

            txtLote.Text = linNroLot.ToString();
            lblCodigoLote.Text = linLotCod.ToString();

            //GUARDAR DATOS NUEVO PRODUCTO INGRESADOS
            GuardarDatosProducto_Sesion();

            pnlEditProductos.Visible = false;
            pnlBusqueda.Visible = false;
            pnlProductos.Visible = false;
            pnlListaPrecios.Visible = false ;
            pnlDetallePrecio.Visible = true ;
            lblMensajes.Visible = false;

            txtPrecio.Text = "0";
            txtDescuento.Text = "0";
            txtStockLote.Text = "0";
            txtFecVenceLote.Text = DateTime.Today.ToShortDateString();
            chkEstadoPrecio.Checked = true;

        }


    }
}