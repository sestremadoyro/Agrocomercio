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
    public partial class wfrmTipoPersonal : BasePage
    {

#region  metodos privados de EVENTOS

        private void EventoActualizaGrilla()
        {
            clsTipoPersonal objTipoPersonal = new clsTipoPersonal();
            DataTable dtTiposPersonal = objTipoPersonal.GetList();

            gvwTipoPersonal.DataSource = dtTiposPersonal;
            gvwTipoPersonal.DataBind();
        }

         private void EventoEliminar(int tpeCodigo)
        {
            
            clsTipoPersonal lstTipoPersonal = new clsTipoPersonal();

            try
            {
                lstTipoPersonal.DeleteTipoPersonal(tpeCodigo);
                lstTipoPersonal.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Eliminó Satisfactoriamente.";
                this.MessageBox("El registro se Eliminó Satisfactoriamente.");

                EventoActualizaGrilla();
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = ex.Message ;
                this.MessageBox(ex.Message);
            }
         }
         private void EventoGuardar()
         {
             int linOperacion = (int)LeerVariableSesion("operacion");

             TipoPersonal oTipoPersonal = new TipoPersonal();
             clsTipoPersonal lstTipoPersonal = new clsTipoPersonal();

             if (linOperacion == 1)        //NUEVO
             {
                 oTipoPersonal.tpeCod = lstTipoPersonal.MaxLotCod() + 1;
                 oTipoPersonal.tpeDescripcion  = txtDescripcion.Text.ToUpper();
                 lstTipoPersonal.Add(oTipoPersonal);
             }
             else                        // MODIFICAR
             {
                 int linTpeCodigo= (int)LeerVariableSesion("tpeCod");
                 
                 oTipoPersonal = lstTipoPersonal.GetTipoPersonal(linTpeCodigo);
                 oTipoPersonal.tpeDescripcion = txtDescripcion.Text.ToUpper();
                 lstTipoPersonal.Update(oTipoPersonal);
             }

             try
             {
                lstTipoPersonal.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditTipoPersonal.Visible = false;
                pnlTipoPersonal.Visible = true;
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
             pnlEditTipoPersonal.Visible = false;
             pnlTipoPersonal.Visible = true;
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

        protected void gvwTipoPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaTipoPersonal");
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

        protected void gvwTipoPersonal_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvwTipoPersonal.PageIndex = e.NewPageIndex;
            gvwTipoPersonal.DataBind();
        }

        protected void gvwTipoPersonal_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaTipoPersonal")
            {

                int linTpeCodigo;
                linTpeCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("tpeCod", linTpeCodigo);

                clsTipoPersonal lstTipoPersonal = new clsTipoPersonal();
                TipoPersonal oTipoPersonal = lstTipoPersonal.GetTipoPersonal(linTpeCodigo);

                if (oTipoPersonal != null)
                {
                    txtCodigo.Text = linTpeCodigo.ToString();
                    txtDescripcion.Text = oTipoPersonal.tpeDescripcion;

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditTipoPersonal.Visible = true;
                    pnlTipoPersonal.Visible = false;
                    lblMensajes.Visible = false;
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaTipoPersonal")
            {
                int linTpeCodigo;

                linTpeCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linTpeCodigo);

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
            pnlEditTipoPersonal.Visible = true;
            pnlTipoPersonal.Visible = false;
            txtCodigo.Text = "0";
            txtDescripcion.Text = String.Empty;
            txtDescripcion.Focus();
            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
    }
}