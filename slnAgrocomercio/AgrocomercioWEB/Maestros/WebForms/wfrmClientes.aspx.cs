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
    public partial class wfrmClientes : BasePage 
    {
        #region  metodos privados de EVENTOS

        private void MostrarOcultar()
        {
            Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
            if (!objUsuario.Roles.rolOpcionModificar.Value)
            {
                btnGuardar.Visible = false;
                ddlTipoPersona.Enabled = false;
                ddlTipoDoc.Enabled = false;
                txtNombres.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelefono.Enabled = false;
                txtDocumento.Enabled = false;
                txtFecRegistro.Enabled = false;
                txtRepresentante.Enabled = false;


            }
            if (!objUsuario.Roles.rolOpcionCrear.Value)
            {
                btnNuevo.Visible = false;
                imbNuevoProducto.Visible = false;
            }
        }
        private void EventoEliminar(int cliCodigo)
        {
            CCliente oCliente = new CCliente();
            int nResp = oCliente.fnClienteDelete(cliCodigo);
            if (nResp <= 0)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = oCliente.DescripcionError;
                this.MessageBox(oCliente.DescripcionError);
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
            CCliente oCliente = new CCliente();
            gvwClientes.DataSource = (DataTable) oCliente.fnListaClientes(chkOpcionTodos.Checked );
            gvwClientes.DataBind();
        }

        private void EventoGuardar()
        {
            int nResp;
            int linOperacion = (int)LeerVariableSesion("operacion");

            int linCliCodigo = 0;
            if (LeerVariableSesion("cliCodigo") != null)
                linCliCodigo = (int)LeerVariableSesion("cliCodigo");

            CCliente oCliente = new CCliente();
            oCliente.CliCod = linCliCodigo;
            oCliente.CliTipoDoc = ddlTipoDoc.SelectedItem.Value;
            oCliente.CliTipoPer = ddlTipoPersona.SelectedItem.Value;
            oCliente.CliNombres = txtNombres.Text.ToUpper();
            oCliente.CliNumDoc = txtDocumento.Text.ToUpper();
            oCliente.CliRepresen = txtRepresentante.Text.ToUpper();
            oCliente.CliDireccion = txtDireccion.Text.ToUpper();
            oCliente.CliTelefono = txtTelefono.Text;
            oCliente.CliFecRegis = Convert.ToDateTime(txtFecRegistro.Text);
            oCliente.CliEstado = chkActivo.Checked;

            if (linOperacion == 1)        //NUEVO
            {
                nResp = oCliente.fnClienteInsert();
            }
            else                        // MODIFICAR
                nResp = oCliente.fnClienteUpdate();


            if (nResp <= 0)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = oCliente.DescripcionError;
                this.MessageBox(oCliente.DescripcionError);
            }
            else
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditClientes.Visible = false;
                pnlClientes.Visible = true;
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
                    EventoBuscarClientes(sBuscado);
                }

            }

        }
        private void LlenarListas()
        {

            ddlTipoDoc.Items.Clear();
            ListItem liDNI = new ListItem("DNI", "1");
            ListItem liRUC = new ListItem("RUC", "2");
            ListItem liOTROS = new ListItem("OTROS", "3");

            ddlTipoDoc.Items.Add(liDNI);
            ddlTipoDoc.Items.Add(liRUC);
            ddlTipoDoc.Items.Add(liOTROS);

            ddlTipoPersona.Items.Clear();
            ListItem liNATURAL = new ListItem("NATURAL", "1");
            ListItem liJURIDICA = new ListItem("JURIDICA", "2");

            ddlTipoPersona.Items.Add(liNATURAL);
            ddlTipoPersona.Items.Add(liJURIDICA);

        }
        private void EventoCancelar(int pOperacion)
        {
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtRepresentante.Text = string.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtDocumento.Text = string.Empty;

            LlenarListas();

            lblMensajes.Visible = false;

            //nuevo
            if (pOperacion == 1)
            {
                pnlEditClientes.Visible = false;
                pnlClientes.Visible = false;
                pnlBusqueda.Visible = true;
            }
            else   //modificar
            {
                string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
                EventoBuscarClientes(sBuscado);

                pnlEditClientes.Visible = false;
                pnlClientes.Visible = true;
                pnlBusqueda.Visible = true;

            }

        }

        private void EventoBuscarClientes(string pBuscado)
        {
            //string sBuscado = txtBusqueda.Text.ToUpper();
            string sCadenaFiltro = "cliNombre like '%" + pBuscado + "%'";
            //DataTable con todos los clientes
            CCliente objCliente = new CCliente();

            DataTable dtClientes = (DataTable) objCliente.fnListaClientes(chkOpcionTodos.Checked );
            DataTable dtResultado;
            //DataTable con los clientes ya filtrados
            DataView oDataView = new DataView(dtClientes);
            oDataView.RowFilter = sCadenaFiltro;
            dtResultado = oDataView.ToTable();

            if (dtResultado.Rows.Count > 0)
            {
                gvwClientes.DataSource = dtResultado;
                gvwClientes.DataBind();
            }
            else
            {
                lblMensajes.Text = "No existen registros";
                lblMensajes.Visible = true;
                this.MessageBox("No existen registros");

            }

        }

        private void EventoNuevoProducto()
        {
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtRepresentante.Text = string.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtDocumento.Text = string.Empty;
            chkActivo.Checked = true;

            LlenarListas();

            pnlEditClientes.Visible = true;
            pnlBusqueda.Visible = false;
            pnlClientes.Visible = false;

            lblMensajes.Visible = false;

            //NUEVO , se guarda en variable cache
            this.AgregarVariableSession("operacion", 1);
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                pnlBusqueda.Visible = true;
                pnlEditClientes.Visible = false;
                pnlClientes.Visible = false;
                MostrarOcultar();
            }

            //EventoActualizaGrilla();
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlEditClientes.Visible = true;
            pnlClientes.Visible = false;

            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtRepresentante.Text = string.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtDocumento.Text = string.Empty;

            LlenarListas();

            txtNombres.Focus();
            //NUEVO , se guarda en variable cache
            this.AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
        protected void gvwClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaCliente");
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
        protected void gvwClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaCliente")
            {
                int linCliCodigo;
                linCliCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("cliCodigo", linCliCodigo);

                DataRow drCliente;
                CCliente objCliente = new CCliente();
                drCliente = objCliente.fnDatosCliente(linCliCodigo);
                if (drCliente != null)
                {
                    txtCodigo.Text = linCliCodigo.ToString();
                    txtNombres.Text = drCliente["cliNombre"].ToString();
                    txtRepresentante.Text = drCliente["CliRepresen"].ToString();
                    txtFecRegistro.Text = drCliente["CliFecRegis"].ToString();
                    txtDireccion.Text = drCliente["cliDireccion"].ToString();
                    txtTelefono.Text = drCliente["cliTelefono"].ToString();
                    txtDocumento.Text = drCliente["CliNumDoc"].ToString();
                    chkActivo.Checked = Convert.ToBoolean(  drCliente["CliEstado"]);

                    LlenarListas();

                    ListItem liElegido = ddlTipoPersona.Items.FindByValue(drCliente["CliTipoPer"].ToString().Trim());
                    ddlTipoPersona.SelectedIndex = ddlTipoPersona.Items.IndexOf(liElegido);

                    ListItem liElegido2 = ddlTipoDoc.Items.FindByValue(drCliente["CliTipoDoc"].ToString().Trim());
                    ddlTipoDoc.SelectedIndex = ddlTipoDoc.Items.IndexOf(liElegido2);

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditClientes.Visible = true;
                    pnlClientes.Visible = false;
                    pnlBusqueda.Visible = false;
                    lblMensajes.Visible = false;

                    MostrarOcultar();
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaCliente")
            {
                int linCliCodigo;
                linCliCodigo = Convert.ToInt32(e.CommandArgument);

                EventoEliminar(linCliCodigo);

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

        protected void gvwClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwClientes.PageIndex = e.NewPageIndex;
            string sBuscado = this.LeerVariableSesion("sBuscado").ToString();
            EventoBuscarClientes(sBuscado);

            //gvwClientes.DataBind();
        }
        protected void imbBuscarEspecial_Click(object sender, ImageClickEventArgs e)
        {
            string sBuscado = txtBusqueda.Text.ToUpper();
            this.AgregarVariableSession("sBuscado", sBuscado);
            EventoBuscarClientes(sBuscado);
            pnlClientes.Visible = true;
            pnlEditClientes.Visible = false;
            pnlBusqueda.Visible = true;
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

    }
}