using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;


namespace AgrocomercioWEB.Maestros.WebForms
{
    public partial class wfrmTipoCambio : BasePage
    {
        #region eventos privados
        private void EventoActualizaGrilla()
        {
            clsTipoCambios lstTipoCambio = new clsTipoCambios();
            DataTable dtTiposCambio = lstTipoCambio.GetList();

            gvwTiposCambio.DataSource = dtTiposCambio;
            gvwTiposCambio.DataBind();
        }

        private void EventoEliminar(int tcmCodigo)
        {

            clsTipoCambios lstTipoCambio = new clsTipoCambios();

            try
            {
                lstTipoCambio.DeleteTiposCambio(tcmCodigo);
                lstTipoCambio.SaveChanges();
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

            TipoCambios oTiposCambio = new TipoCambios();
            clsTipoCambios lstTipoCambio = new clsTipoCambios();

            if (linOperacion == 1)        //NUEVO
            {
                oTiposCambio.tcmCod = lstTipoCambio.MaxTcmCod() + 1;
                oTiposCambio.tcmCambio = Convert.ToDecimal(txtCambio.Text);
                oTiposCambio.tcmMoneda = ddlMoneda.SelectedItem.Value;
                oTiposCambio.tcmFecha = DateTime.Parse(txtFecha.Text);
                oTiposCambio.tcmfecmod = DateTime.Now;
                //Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
                //oTiposCambio.usrCod = objUsuario.usrCod;
                lstTipoCambio.Add(oTiposCambio);
            }
            else                        // MODIFICAR
            {
                int linTipoCambioCodigo = (int)LeerVariableSesion("tcmCod");

                oTiposCambio = lstTipoCambio.GetTipoCambio(linTipoCambioCodigo);
                oTiposCambio.tcmCambio = Convert.ToDecimal(txtCambio.Text);
                oTiposCambio.tcmMoneda =ddlMoneda.SelectedItem.Value;
                oTiposCambio.tcmFecha = DateTime.Parse(txtFecha.Text);
                oTiposCambio.tcmfecmod = DateTime.Today;
                lstTipoCambio.Update(oTiposCambio);
            }

            try
            {
                lstTipoCambio.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditTiposCambio.Visible = false;
                pnlTiposCambio.Visible = true;
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
            txtCambio.Text = "0";

            txtFecha.Text = DateTime.Today.ToString("dd/mm/yyyy");
            txtCambio.Focus();
            pnlEditTiposCambio.Visible = false;
            pnlTiposCambio.Visible = true;
            lblMensajes.Visible = false;

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                EventoActualizaGrilla();
        }

        protected void gvwTiposCambio_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridView grvGrilla = new GridView();
            grvGrilla = (GridView)sender;

            grvGrilla.PageIndex = e.NewPageIndex;
            grvGrilla.DataBind();
        }

        protected void gvwTiposCambio_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaTiposCambio")
            {

                int linCodTipoCambio;
                linCodTipoCambio = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("tcmCod", linCodTipoCambio);

                clsTipoCambios lstTiposCambio = new clsTipoCambios();
                TipoCambios oTiposCambio = lstTiposCambio.GetTipoCambio(linCodTipoCambio);

                if (oTiposCambio != null)
                {
                    txtCodigo.Text = linCodTipoCambio.ToString();
                    txtCambio.Text = oTiposCambio.tcmCambio.ToString();
                    txtFecha.Text = DateTime.Parse(oTiposCambio.tcmFecha.ToString()).ToShortDateString();

                    ListItem liElegido;
                    //si DropDownList está visible
                    if (ddlMoneda.Items.Count > 0)
                    {
                        liElegido = ddlMoneda.Items.FindByValue(oTiposCambio.tcmCod.ToString());
                        ddlMoneda.SelectedIndex = ddlMoneda.Items.IndexOf(liElegido);
                    }


                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditTiposCambio.Visible = true;
                    pnlTiposCambio.Visible = false;
                    lblMensajes.Visible = false;
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaTiposCambio")
            {
                int linRolCodigo;

                linRolCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(linRolCodigo);

            }
        }

        protected void gvwTiposCambio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaTiposCambio");
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlEditTiposCambio.Visible = true;
            pnlTiposCambio.Visible = false;
            txtCodigo.Text = "0";
            txtCambio.Text = "0";
            ddlMoneda.SelectedIndex = 0;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtCambio.Focus();
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
    }
}