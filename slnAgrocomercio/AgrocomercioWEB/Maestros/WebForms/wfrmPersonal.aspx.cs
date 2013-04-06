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
    public partial class wfrmPersonal : BasePage
    {

        #region  metodos privados de EVENTOS
        private void EventoLlenarLista()
        {

            clsTipoPersonal lstTipoPersonal = new clsTipoPersonal();
            DataTable dtTipoPersonal = lstTipoPersonal.GetList();


            ddlTipoPersonal.DataSource = dtTipoPersonal;
            ddlTipoPersonal.DataBind();
        }
        private void EventoActualizaGrilla()
        {
            clsPersonal lstPersonal = new clsPersonal();
            DataTable dtPersonal = lstPersonal.GetList();

            gvwPersonal.DataSource = dtPersonal;
            gvwPersonal.DataBind();
        }

        private void EventoEliminar(int perCodigo)
        {

            clsPersonal lstPersonal = new clsPersonal();

            try
            {
                lstPersonal.DeletePersonal(perCodigo);
                lstPersonal.SaveChanges();
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

            Personal oPersonal = new Personal();
            clsPersonal lstPersonal = new clsPersonal();

            if (linOperacion == 1)        //NUEVO
            {
                oPersonal.perCod = lstPersonal.MaxpnPerCod() + 1;
                oPersonal.perNombres = txtNombres.Text.ToUpper();
                oPersonal.perApellidoPat = txtApellidoPat.Text.ToUpper();
                oPersonal.perApellidoMat = txtApellidoMat.Text.ToUpper();
                oPersonal.perDireccion = txtDireccion.Text;
                oPersonal.perTelefono = txtTelefono.Text;
                oPersonal.tpecod = Convert.ToInt32( ddlTipoPersonal.SelectedItem.Value);

                lstPersonal.Add(oPersonal);
            }
            else                        // MODIFICAR
            {
                int linPerCodigo = (int)LeerVariableSesion("perCod");
                oPersonal = lstPersonal.GetPersonal(linPerCodigo);
                oPersonal.perNombres = txtNombres.Text.ToUpper();
                oPersonal.perApellidoPat = txtApellidoPat.Text.ToUpper();
                oPersonal.perApellidoMat = txtApellidoMat.Text.ToUpper();
                oPersonal.perDireccion = txtDireccion.Text;
                oPersonal.perTelefono = txtTelefono.Text;
                oPersonal.tpecod = Convert.ToInt32(ddlTipoPersonal.SelectedItem.Value);

                lstPersonal.Update(oPersonal);
            }

            try
            {
                lstPersonal.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditPersonal.Visible = false;
                pnlPersonal.Visible = true;
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
            txtNombres.Text = string.Empty;
            txtApellidoPat.Text = string.Empty;
            txtApellidoMat.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            //EventoActualizaGrilla();
            pnlEditPersonal.Visible = false;
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

        protected void gvwPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaPersonal");
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

        protected void gvwPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwPersonal.PageIndex = e.NewPageIndex;
            gvwPersonal.DataBind();
        }

        protected void gvwPersonal_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    txtCodigo.Text = linPerCodigo.ToString();
                    txtNombres.Text = oPersonal.perNombres;
                    txtApellidoPat.Text = oPersonal.perApellidoPat;
                    txtApellidoMat.Text = oPersonal.perApellidoMat;
                    txtDireccion.Text = oPersonal.perDireccion;
                    txtTelefono.Text = oPersonal.perTelefono;

                    ListItem liElegido;
                    //si DropDownList está visible
                    if (ddlTipoPersonal.Items.Count > 0)
                    {
                        liElegido = ddlTipoPersonal.Items.FindByValue(oPersonal.tpecod.ToString());
                        ddlTipoPersonal.SelectedIndex = ddlTipoPersonal.Items.IndexOf(liElegido);
                    }
                    
                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditPersonal.Visible = true;
                    pnlPersonal.Visible = false;
                    lblMensajes.Visible = false;
                }

            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaPersonal")
            {
                int linPerCodigo;

                linPerCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linPerCodigo);

            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EventoGuardar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            EventoCancelar();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlEditPersonal.Visible = true;
            pnlPersonal.Visible = false;
            txtCodigo.Text = "0";
            txtNombres.Text = String.Empty;
            txtNombres.Focus();
            txtApellidoPat.Text = String.Empty;
            txtApellidoMat.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            
            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
    }
}