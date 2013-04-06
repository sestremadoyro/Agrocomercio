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
    public partial class wfrmTransportista : BasePage 
    {


        #region  metodos privados de EVENTOS
        private void MostrarOcultar()
        {
            Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
            if (!objUsuario.Roles.rolOpcionModificar.Value)
            {
                btnGuardar.Visible = false;
            }
            if (!objUsuario.Roles.rolOpcionCrear.Value)
            {
                btnNuevo.Visible = false;
            }
        }
        private void EventoEliminar(int traCodigo)
        {
            
            clsTransportistas ListaTransportistas = new clsTransportistas();
            try
            {
                int regs = ListaTransportistas.DeleteTransportista(traCodigo);
                if (regs == 0)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "No se pudo ELIMINAR el registro, tiene Operaciones Relacionados";
                    this.MessageBox("No se pudo ELIMINAR el registro, tiene Operaciones Relacionados");
                }
                else
                {
                    //ListaTransportistas.SaveChanges();
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El registro se Eliminó Satisfactoriamente.";
                    this.MessageBox("El registro se Eliminó Satisfactoriamente.");

                    EventoActualizaGrilla();
                }
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = ex.Message + " " + ex.InnerException.Message;
                this.MessageBox(ex.Message + " " + ex.InnerException.Message);
            }
            
        }
        private void EventoActualizaGrilla()
        {
            clsTransportistas ListaTransportistas = new clsTransportistas();
            gvwTransportistas.DataSource = ListaTransportistas.GetListActivos();
            gvwTransportistas.DataBind();
        }

        private void EventoGuardar()
        {
            
            int linOperacion = (int)LeerVariableSesion ("operacion");

            Transportistas oTransportista = new Transportistas();
            clsTransportistas ListaTransportistas = new clsTransportistas();

            if (linOperacion == 1)        //NUEVO
            {
                oTransportista.TraCod = ListaTransportistas.MaxTraCod() + 1;
                oTransportista.TraRazonSocial = txtNombre.Text.ToUpper();
                oTransportista.TraDireccion = txtDireccion.Text;
                oTransportista.TraRuc = txtNumDoc.Text;
                oTransportista.TraTelefono = txtTelefono.Text;
                oTransportista.TraFecRegis = DateTime.Parse(txtFecRegistro.Text);
                oTransportista.TraEstado = chkestado.Checked;

                ListaTransportistas.Add(oTransportista);
            }
            else                        // MODIFICAR
            {
                int lintraCodigo = (int)LeerVariableSesion("traCod");
                oTransportista = ListaTransportistas.GetTransportista(lintraCodigo);
                oTransportista.TraRazonSocial = txtNombre.Text.ToUpper();
                oTransportista.TraDireccion = txtDireccion.Text;
                oTransportista.TraRuc = txtNumDoc.Text;
                oTransportista.TraFecRegis = DateTime.Parse(txtFecRegistro.Text);
                oTransportista.TraTelefono = txtTelefono.Text;
                oTransportista.TraEstado = chkestado.Checked;

                ListaTransportistas.Update(oTransportista);
            }

            try
            {
                ListaTransportistas.SaveChanges();
                lblMensajes.Visible = true;
                lblMensajes.Text = "El registro se Grabo Satisfactoriamente.";
                pnlEditTransportistas.Visible = false;
                pnlTransportistas.Visible = true;
                
                this.MessageBox("El registro se Grabo Satisfactoriamente.");
                EventoActualizaGrilla();
            }
            catch (Exception ex)
            {

                lblMensajes.Visible = true;
                lblMensajes.Text = ex.Message +" " + ex.InnerException.Message ;
                this.MessageBox(ex.Message  +" " + ex.InnerException.Message );
            }


        }
        private void EventoCancelar()
        {
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtFecRegistro.Text = DateTime.Today.ToString();
            txtNumDoc.Text = string.Empty;

            //EventoActualizaGrilla();
            pnlEditTransportistas.Visible = false;
            pnlTransportistas.Visible = true;
            lblMensajes.Visible = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EventoActualizaGrilla();
                MostrarOcultar();
            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {

            pnlEditTransportistas.Visible = true;
            pnlTransportistas.Visible = false;
            txtNombre.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtFecRegistro.Text = DateTime.Today.ToShortDateString();
            txtNumDoc.Text = string.Empty;
            chkestado.Checked = true;
            txtNombre.Focus();
            //NUEVO , se guarda en variable cache
            AgregarVariableSession("operacion", 1);
            lblMensajes.Visible = false;
        }
        protected void gvwTransportistas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AGREGAR CONFIRMACION DE ELIMINAR
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f0fff0';this.style.color='#3496bb'");
                LinkButton lbtEliminar = (LinkButton)e.Row.FindControl("lnbEliminaTransportista");
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
            EventoCancelar();
        }

        protected void gvwTransportistas_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //presiona BOTON MODIFICAR EN GRILLA
            if (e.CommandName == "SeleccionaTransportista")
            {
                int lintraCodigo;
                lintraCodigo = Convert.ToInt32(e.CommandArgument);

                AgregarVariableSession("traCod", lintraCodigo);

                clsTransportistas ListaTransportistas = new clsTransportistas();
                Transportistas oTransportista = new Transportistas();

                oTransportista =  ListaTransportistas.GetTransportista(lintraCodigo);

                if (oTransportista != null)
                {
                    txtCodigo.Text = lintraCodigo.ToString();
                    txtNombre.Text = oTransportista.TraRazonSocial;
                    txtNumDoc.Text = oTransportista.TraRuc ;
                    txtDireccion.Text = oTransportista.TraDireccion;
                    if (oTransportista.TraEstado.HasValue)
                        chkestado.Checked = oTransportista.TraEstado.Value;
                    else
                        chkestado.Checked = false;
                    txtTelefono.Text = oTransportista.TraTelefono ;
                    txtFecRegistro.Text = oTransportista.TraFecRegis.ToString();

                    //MODIFICAR , se guarda en variable cache
                    AgregarVariableSession("operacion", 2);
                    pnlEditTransportistas.Visible = true;
                    pnlTransportistas.Visible = false;
                    lblMensajes.Visible = false;
                    MostrarOcultar();
                }
            }
            //presiona BOTON ELIMINAR EN GRILLA
            if (e.CommandName == "EliminaTransportista")
            {
                int lintraCodigo;

                lintraCodigo = Convert.ToInt32(e.CommandArgument);
                EventoEliminar(lintraCodigo);

            }

        }
        protected void gvwTransportistas_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvwTransportistas.PageIndex = e.NewPageIndex;
            gvwTransportistas.DataBind();
        }


    }
}