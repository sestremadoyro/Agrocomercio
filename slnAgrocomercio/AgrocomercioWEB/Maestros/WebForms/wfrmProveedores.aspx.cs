using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.Maestros;

using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;

namespace AgrocomercioWEB.Maestros.WebForms
{
    public partial class wfrmProveedores : BasePage 
    {

        #region  metodos privados de EVENTOS
        private void MostrarOcultar()
        {
            Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
            if (!objUsuario.Roles.rolOpcionModificar.Value)
            {
                btnGuardar.Visible = false;
                txtPorcentaje.Visible = false;
                txtDescuento.Visible = false;
                lblDescuento.Visible = false;
                lblPorcentaje.Visible = false;
                txtNombre.Enabled = false;
                txtDireccion.Enabled = false;
                txtNumDoc.Enabled = false;
                txtTelefono.Enabled = false;
                txtFecRegistro.Enabled = false;
            }
            if (!objUsuario.Roles.rolOpcionCrear.Value)
            {
                btnNuevo.Visible = false;
                imbNuevoProveedor.Visible = false;
            }
        }
        private void EventoBuscarProveedor(string pBuscado)
        {
            //string sBuscado = txtBusqueda.Text.ToUpper();
            string sCadenaFiltro = "prvRazon like '%" + pBuscado + "%'";
            //DataTable con todos los proveedores
            CProveedor objProveedor = new CProveedor();

            DataTable dtProveedores = (DataTable)objProveedor.fnListaProveedores(chkOpcionTodos.Checked);
            DataTable dtResultado;
            //DataTable con los clientes ya filtrados
            DataView oDataView = new DataView(dtProveedores);
            oDataView.RowFilter = sCadenaFiltro;
            dtResultado = oDataView.ToTable();

            if (dtResultado.Rows.Count > 0)
            {
                gvwProveedores.DataSource = dtResultado;
                gvwProveedores.DataBind();
            }
            else
            {
                lblMensajes.Text = "No existen registros";
                lblMensajes.Visible = true;
                this.MessageBox("No existen registros");

            }

        }
        private void EventoNuevoProveedor()
        {
            pnlEditProveedores.Visible = true;
            pnlProveedores.Visible = false;
            pnlBusqueda.Visible = false;

            txtNombre.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtFecRegistro.Text = DateTime.Today.ToShortDateString();
            txtNumDoc.Text = string.Empty;
            chkestado.Checked = true;
            txtPorcentaje.Text = "0";
            txtDescuento.Text = "0";
            txtNombre.Focus();
            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;        
        }
        private void EventoEliminar(int prvCodigo)
        {
            CProveedor oProveedor = new CProveedor();

            int nResp = oProveedor.fnProvedorDelete(prvCodigo);
            if (nResp <= 0)
            {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = oProveedor.DescripcionError;
                    this.MessageBox(oProveedor.DescripcionError);
            }
            else
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Eliminó Satisfactoriamente.";
                this.MessageBox("El registro se Eliminó Satisfactoriamente.");

                EventoActualizaGrilla();
            }
           
           
        }
        private void EventoActualizaGrilla()
        {
            CProveedor oProveedor = new CProveedor();
            gvwProveedores.DataSource = oProveedor.fnListaProveedores(chkOpcionTodos.Checked );
            gvwProveedores.DataBind();
        }

        private void EventoGuardar()
        {
            int nResp;
            int linOperacion = (int)LeerVariableSesion ("operacion");

            int linPrvCodigo;
            if (LeerVariableSesion("prvCod") != null)
                linPrvCodigo = (int)LeerVariableSesion("prvCod");
            else
                linPrvCodigo = 1;

            CProveedor oProveedor = new CProveedor();
            oProveedor.prvCodigo = linPrvCodigo;
            oProveedor.prvRazon = txtNombre.Text.ToUpper();
            oProveedor.PrvNumDoc = txtNumDoc.Text.Trim();
            oProveedor.prvDireccion = txtDireccion.Text;
            oProveedor.prvTelefono = txtTelefono.Text;
            oProveedor.prvEstado = chkestado.Checked;
            oProveedor.PrvGanancia = Convert.ToDecimal(txtPorcentaje.Text);
            oProveedor.PrvDscto = Convert.ToDecimal(txtDescuento.Text);
            oProveedor.prvFecRegis = DateTime.Parse(txtFecRegistro.Text);

            if (linOperacion == 1)        //NUEVO
            {
                nResp = oProveedor.fnProveedorInsert();
            }
            else                        // MODIFICAR
                nResp = oProveedor.fnProveedorUpdate();
            if (nResp <= 0)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = oProveedor.DescripcionError;
                this.MessageBox(oProveedor.DescripcionError);
            }
            else
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditProveedores.Visible = false;
                pnlProveedores.Visible = true;
                pnlBusqueda.Visible = true;
                this.MessageBox("El registro se Grabo Satisfactoriamente.");

                //nuevo
                if (linOperacion == 1)
                {
                    EventoActualizaGrilla();
                }
                else   //modificar
                {
                    string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                    EventoBuscarProveedor(sBuscado);
                }
            }

        }
        private void EventoCancelar(int pOperacion)
        {
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString();
            txtNumDoc.Text = string.Empty;
            txtPorcentaje.Text = "0";
            txtDescuento.Text = "0";

            lblMensajes.Visible = false;

            //nuevo
            if (pOperacion == 1)
            {
                pnlEditProveedores.Visible = false;
                pnlProveedores.Visible = false;
                pnlBusqueda.Visible = true;
            }
            else   //modificar
            {
                string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                EventoBuscarProveedor(sBuscado);

                pnlEditProveedores.Visible = false;
                pnlProveedores.Visible = true;
                pnlBusqueda.Visible = true;

            }
           
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                pnlBusqueda.Visible = true;
                pnlEditProveedores.Visible = false;
                pnlProveedores.Visible = false;
                MostrarOcultar();
            }
            //EventoActualizaGrilla();
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {

            pnlEditProveedores.Visible = true;
            pnlProveedores.Visible = false;
            txtNombre.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString();
            txtNumDoc.Text = string.Empty;
            chkestado.Checked = true;
            txtPorcentaje.Text = "0";
            txtDescuento.Text = "0";
            txtNombre.Focus();
            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
        protected void gvwProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaProveedor");
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EventoGuardar();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int linOperacion = (int)LeerVariableSesion("operacion");

            EventoCancelar(linOperacion);
        }

        protected void gvwProveedores_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaProveedor")
            {
                int linPrvCodigo;
                linPrvCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("prvCod", linPrvCodigo);
                DataRow drProveedor;
                CProveedor objProveedor = new CProveedor();
                drProveedor = objProveedor.fnDatosProveedor(linPrvCodigo);
                if (drProveedor != null)
                {
                    txtCodigo.Text = linPrvCodigo.ToString();
                    txtNombre.Text = drProveedor["PrvRazon"].ToString();
                    txtNumDoc.Text = drProveedor["PrvNumDoc"].ToString();
                    txtDireccion.Text = drProveedor["prvDireccion"].ToString();
                    chkestado.Checked = Convert.ToBoolean(drProveedor["prvEstado"]);
                    txtTelefono.Text = drProveedor["prvTelefono"].ToString();
                    txtFecRegistro.Text = drProveedor["PrvFecRegis"].ToString();
                    txtPorcentaje.Text = drProveedor["PrvGanancia"].ToString();
                    txtDescuento.Text = drProveedor["PrvDscto"].ToString();

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditProveedores.Visible = true;
                    pnlProveedores.Visible = false;
                    pnlBusqueda.Visible = false;
                    lblMensajes.Visible = false;
                    MostrarOcultar();
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaProveedor")
            {
                int linPrvCodigo;

                linPrvCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linPrvCodigo);

            }

        }
        protected void gvwProveedores_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvwProveedores.PageIndex = e.NewPageIndex;
            string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
            EventoBuscarProveedor(sBuscado);
            //gvwProveedores.DataBind();
        }

        protected void imbNuevoProveedor_Click(object sender, ImageClickEventArgs e)
        {

            EventoNuevoProveedor();
        }

        protected void imbNuevoProveedor_Init(object sender, EventArgs e)
        {
            imbNuevoProveedor.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            imbNuevoProveedor.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");
        }

        protected void imbBuscarEspecial_Click(object sender, ImageClickEventArgs e)
        {
            string sBuscado = txtBusqueda.Text.ToUpper();
            this.AgregarVariableSession("sBuscado", sBuscado);
            EventoBuscarProveedor(sBuscado);
            pnlProveedores.Visible = true;
            pnlEditProveedores.Visible = false;
            pnlBusqueda.Visible = true;
            lblMensajes.Visible = false;
        }

        protected void imbBuscarEspecial_Init(object sender, EventArgs e)
        {
            imbBuscarEspecial.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            imbBuscarEspecial.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");
        }


    }
}