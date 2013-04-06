using System;
using System.Data;
using System.IO;

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
    public partial class wfrmRoles : BasePage 
    {
        #region  metodos privados de EVENTOS
        private void EventoActualizaGrilla()
        {
            clsRoles lstRoles = new clsRoles();
            DataTable dtRoles = lstRoles.GetList();

            gvwRoles.DataSource = dtRoles;
            gvwRoles.DataBind();
        }

        private void EventoEliminar(int rolCodigo)
        {

            clsRoles lstRoles = new clsRoles();

            try
            {
                lstRoles.DeleteRoles(rolCodigo);
                lstRoles.SaveChanges();
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

            Roles  oRoles= new Roles ();
            clsRoles lstRoles = new clsRoles();

            if (linOperacion == 1)        //NUEVO
            {
                oRoles.rolCod = lstRoles.MaxLotCod() + 1;
                oRoles.rolDescripcion = txtDescripcion.Text.ToUpper();
                lstRoles.Add(oRoles);
            }
            else                        // MODIFICAR
            {
                int linRolCodigo = (int)LeerVariableSesion("rolCod");
                oRoles = lstRoles.GetRoles(linRolCodigo);
                oRoles.rolDescripcion = txtDescripcion.Text.ToUpper();
                lstRoles.Update(oRoles);
            }

            try
            {
                lstRoles.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditRoles.Visible = false;
                pnlRoles.Visible = true;
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
            txtDescripcion.Text = string.Empty;

            //EventoActualizaGrilla();
            pnlEditRoles.Visible = false;
            pnlRoles.Visible = true;
            lblMensajes.Visible = false;
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EventoActualizaGrilla();
            }
        }

        protected void gvwRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaRoles");
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

        protected void gvwRoles_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridView grvGrilla = new GridView();
            grvGrilla = (GridView) sender ;
     
            grvGrilla.PageIndex = e.NewPageIndex;
            grvGrilla.DataBind();
        }

        protected void gvwRoles_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaRoles")
            {

                int linRolCodigo;
                linRolCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("rolCod", linRolCodigo);

                clsRoles lstRoles = new clsRoles();
                Roles oRoles = lstRoles.GetRoles(linRolCodigo);

                if (oRoles != null)
                {
                    txtCodigo.Text = linRolCodigo.ToString();
                    txtDescripcion.Text = oRoles.rolDescripcion;

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditRoles.Visible = true;
                    pnlRoles.Visible = false;
                    lblMensajes.Visible = false;
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaRoles")
            {
                int linRolCodigo;

                linRolCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linRolCodigo);

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
            pnlEditRoles.Visible = true;
            pnlRoles.Visible = false;
            txtCodigo.Text = "0";
            txtDescripcion.Text = String.Empty;
            txtDescripcion.Focus();
            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
    }
}