using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


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
namespace AgrocomercioWEB.Maestros.WebForms
{
    public partial class wfrmUsuarios : BasePage
    {
        #region  metodos privados de EVENTOS
        private void EventoLlenarLista()
        {

            clsRoles lstRoles = new clsRoles();
            DataTable dtRoles = lstRoles.GetList();


            ddlRoles.DataSource = dtRoles;
            ddlRoles.DataBind();
        }
        private void EventoActualizaGrilla()
        {
            clsUsuario lstUsuarios = new clsUsuario();
            DataTable dtUsuarios = lstUsuarios.GetListUsuarios();

            gvwUsuarios.DataSource = dtUsuarios;
            gvwUsuarios.DataBind();
        }

        private void EventoActualizaGrillaPersonal()
        {
            clsPersonal lstPersonal = new clsPersonal();
            DataTable dtPersonal = lstPersonal.GetList();

            gvwPersonal.DataSource = dtPersonal;
            gvwPersonal.DataBind();
        }
        private void EventoEliminar(int usrCodigo)
        {

            clsUsuario lstUsuarios = new clsUsuario();

            try
            {
                lstUsuarios.DeleteUsuario(usrCodigo);
                lstUsuarios.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Eliminó Satisfactoriamente.";
                this.MessageBox("El registro se Eliminó Satisfactoriamente.");

                EventoActualizaGrilla();
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = ex.Message;
                this.MessageBox(ex.Message);
            }
        }
        private void EventoGuardar()
        {
            int linOperacion = (int)LeerVariableSesion("operacion");

            Usuarios oUsuario = new Usuarios();
            clsUsuario lstUsuarios = new clsUsuario();

            if (linOperacion == 1)        //NUEVO
            {
                oUsuario.usrCod = lstUsuarios.MaxpnUserCod() + 1;
                oUsuario.usrLogin = txtUsuario.Text;
                oUsuario.usrClave = txtClave.Text;
                oUsuario.RolCod  = Convert.ToInt32( ddlRoles.SelectedItem.Value);
                oUsuario.perCod = Convert.ToInt32( txtCodigoPersonal.Text);

                lstUsuarios.Add(oUsuario);
            }
            else                        // MODIFICAR
            {
                int linUsrCodigo = (int)LeerVariableSesion("usrCod");
                oUsuario = lstUsuarios.GetUsuario(linUsrCodigo);
                oUsuario.usrLogin = txtUsuario.Text.ToUpper();
                oUsuario.usrClave = txtClave.Text.ToUpper();
                oUsuario.RolCod = Convert.ToInt32(ddlRoles.SelectedItem.Value);
                oUsuario.perCod = Convert.ToInt32(txtCodigoPersonal.Text);

                lstUsuarios.Update(oUsuario);
            }

            try
            {
                lstUsuarios.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditUsuarios.Visible = false;
                pnlUsuarios.Visible = true;
                pnlPersonal.Visible = false ;
                this.MessageBox("El registro se Grabo Satisfactoriamente.");
                EventoActualizaGrilla();
            }
            catch (Exception ex)
            {

                lblMensajes.Visible = true;
                lblMensajes.Text = ex.Message;
                this.MessageBox(ex.Message);
            }

        }
        private void EventoCancelar()
        {
            txtCodigo.Text = "0";
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;

            //EventoActualizaGrilla();
            pnlEditUsuarios.Visible = false;
            pnlPersonal.Visible = true;
            lblMensajes.Visible = false;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EventoLlenarLista();
                EventoActualizaGrilla();
            }
        }

        protected void gvwUsuarios_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvwUsuarios.PageIndex = e.NewPageIndex;
            gvwUsuarios.DataBind();
        }

        protected void gvwUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaUsuario");
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

        protected void gvwUsuarios_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaUsuario")
            {

                int linUsrCodigo;
                linUsrCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("usrCod", linUsrCodigo);

                Usuarios oUsuario = new Usuarios();
                clsUsuario lstUsuarios = new clsUsuario();

                oUsuario = lstUsuarios.GetUsuario(linUsrCodigo);
                if (oUsuario != null)
                {
                    txtCodigo.Text = linUsrCodigo.ToString();
                    txtUsuario.Text = oUsuario.usrLogin;
                    txtClave.Text = oUsuario.usrClave;
                    txtCodigoPersonal.Text = oUsuario.Personal.perCod.ToString();
                    txtNombre.Text = oUsuario.Personal.perNombres;
                    txtApePaterno.Text = oUsuario.Personal.perApellidoPat;
                    txtApeMaterno.Text = oUsuario.Personal.perApellidoMat;

                    ListItem liElegido;
                    //si DropDownList está visible
                    if (ddlRoles.Items.Count > 0)
                    {
                        liElegido = ddlRoles.Items.FindByValue(oUsuario.RolCod.ToString());
                        ddlRoles.SelectedIndex = ddlRoles.Items.IndexOf(liElegido);
                    }

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditUsuarios.Visible = true;
                    pnlPersonal.Visible = false;
                    lblMensajes.Visible = false;
                }

            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaUsuario")
            {
                int linPerCodigo;

                linPerCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linPerCodigo);

            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlEditUsuarios.Visible = true;
            pnlUsuarios.Visible = false;
            pnlPersonal.Visible = false;
            txtCodigo.Text = "0";
            txtCodigoPersonal.Text = "0";
            txtUsuario.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApePaterno.Text = String.Empty;
            txtApeMaterno.Text = String.Empty;
            txtUsuario.Focus();

            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EventoGuardar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            EventoCancelar();
        }

        protected void gvwPersonal_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvwPersonal.PageIndex = e.NewPageIndex;
            gvwPersonal.DataBind();
        }

        protected void gvwPersonal_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaPersonal")
            {

                int linPerCodigo;
                linPerCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("perCod", linPerCodigo);

                Personal oPersonal = new Personal();
                clsPersonal lstPersonal = new clsPersonal();

                oPersonal = lstPersonal.GetPersonal(linPerCodigo);
                if (oPersonal != null)
                {
                    txtCodigoPersonal.Text = linPerCodigo.ToString();
                    txtNombre.Text = oPersonal.perNombres;
                    txtApePaterno.Text = oPersonal.perApellidoPat;
                    txtApeMaterno.Text = oPersonal.perApellidoMat;

                    pnlEditUsuarios.Visible = true;
                    pnlPersonal.Visible = false;
                    pnlUsuarios.Visible = false;
                    lblMensajes.Visible = false;
                }

            }

        }

        protected void gvwPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        protected void btnSeleccionaPersonal_Click(object sender, EventArgs e)
        {
            
        }

        protected void imbBuscarEspecial_Init(object sender, EventArgs e)
        {
            imbBuscarEspecial.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            imbBuscarEspecial.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");

        }

        protected void imbBuscarEspecial_Click(object sender, ImageClickEventArgs e)
        {
            pnlPersonal.Visible = true;
            EventoActualizaGrillaPersonal();
        }
    }
}