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
    public partial class wfrmAtributos : BasePage
    {

#region  metodos privados de EVENTOS
        private void EventoLlenarLista()
        {
            clsAtributos lstAtributos = new clsAtributos();
            List<Atributos> listaTipos = lstAtributos.ListAtributosPadre();

            foreach (Atributos tipo in listaTipos)
            {
                ListItem liTipo = new ListItem();
                liTipo.Text = tipo.AtrDescripcion ;
                liTipo.Value = tipo.AtrTipoCod.ToString() ;
                lstTipos.Items.Add(liTipo);
            }
        }
        
        private void EventoActualizaGrilla()
        {
            clsAtributos lstAtributos = new clsAtributos();
            DataTable dtPersonal = lstAtributos.ListDataAtributosToTable(Convert.ToInt32( lstTipos.SelectedItem.Value) );

            gvwTipos.DataSource = dtPersonal;
            gvwTipos.DataBind();

            lblMensajes.Visible = false;
        }

        private void EventoEliminar(int pArtTipoCod, string  pArtCodigo)
        {

            clsAtributos lstAtributos = new clsAtributos();

            try
            {
                lstAtributos.DeleteAtributo(pArtTipoCod, pArtCodigo);
                lstAtributos.SaveChanges();
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

            Atributos oAtributo = new Atributos();
            clsAtributos lstAtributos = new clsAtributos();

            string lstrAtrCodigo = string.Empty ;
            int linAtrTipoCod;

            if (linOperacion == 1)        //NUEVO
            {
                oAtributo.AtrTipoCod = Convert.ToInt32(lstTipos.SelectedItem.Value);
                oAtributo.AtrNivel = 1;
                oAtributo.AtrCodigo = txtCodigo.Text.ToUpper() ;
                oAtributo.AtrDescripcion = txtDescripcion.Text.ToUpper();
                oAtributo.AtrEstado = chkEstado.Checked;

                lstAtributos.Add(oAtributo);
            }
            else                        // MODIFICAR
            {
                lstrAtrCodigo = this.LeerVariableSesion("AtrCodigo").ToString();
                linAtrTipoCod = Convert.ToInt32(this.LeerVariableSesion("AtrTipoCodigo"));

                oAtributo = lstAtributos.GetAtributo(linAtrTipoCod, lstrAtrCodigo);

                oAtributo.AtrTipoCod = linAtrTipoCod;
                oAtributo.AtrCodigo = txtCodigo.Text.ToUpper();
                oAtributo.AtrNivel = 1;
                oAtributo.AtrDescripcion = txtDescripcion.Text.ToUpper();
                oAtributo.AtrEstado = chkEstado.Checked;

                lstAtributos.Update(oAtributo);
            }

            try
            {
                lstAtributos.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditTipos.Visible = false;
                pnlTipos.Visible = true;
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
            chkEstado.Checked = true ;

            //EventoActualizaGrilla();
            pnlEditTipos.Visible = false;
            pnlTipos.Visible = true;
            lblMensajes.Visible = false;
        }
#endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EventoLlenarLista();
                lstTipos.SelectedIndex = 0;
                EventoActualizaGrilla();
            }
        }

        protected void gvwTipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaTipo");
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

        protected void gvwTipos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwTipos.PageIndex = e.NewPageIndex;
            gvwTipos.DataBind();
        }

        protected void gvwTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string lstrAtrCodigo;
            lstrAtrCodigo = e.CommandArgument.ToString();

            int linAtrTipoCod;
            linAtrTipoCod = Convert.ToInt32(lstTipos.SelectedItem.Value);

            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaTipo")
            {
                AgregarVariableSession("AtrCodigo", lstrAtrCodigo);
                AgregarVariableSession("AtrTipoCodigo", linAtrTipoCod);

                Atributos oAtributo = new Atributos();
                clsAtributos lstAtributos = new clsAtributos();


                oAtributo = lstAtributos.GetAtributo(linAtrTipoCod,lstrAtrCodigo);
                if (oAtributo != null)
                {
                    txtCodigo.Text = lstrAtrCodigo;
                    txtDescripcion .Text = oAtributo.AtrDescripcion ;
                    chkEstado.Checked  = oAtributo.AtrEstado.Value  ;
                    
                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditTipos.Visible = true;
                    pnlTipos.Visible = false;
                    lblMensajes.Visible = false;
                }

            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaTipo")
            {
                EventoEliminar(linAtrTipoCod, lstrAtrCodigo);
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
            pnlEditTipos.Visible = true;
            pnlTipos.Visible = false;
            txtCodigo.Text = "0";
            txtDescripcion.Text = String.Empty;
            chkEstado.Checked = true;
            txtCodigo.Focus();

            //NUEVO tipo plato, se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }

        protected void lstTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventoActualizaGrilla();
        }


    }
}