using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Text;
using pryAgrocomercioBLL.EntityCollection;
//using pryAgrocomercioBLL.Clases;

using pryAgrocomercioDAL;
using System.Globalization;
using System.Configuration;

namespace AgrocomercioWEB.pagos
{
    public partial class wfrmLstPagReal : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int _selectedIndex = 0;
            txtFecDesde.Attributes.Add("readonly", "readonly");
            txtFechaHasta.Attributes.Add("readonly", "readonly");
            txtFecPag.Attributes.Add("readonly", "readonly");
            if (Page.IsPostBack)
            {
                switch (lblEstado.Value)
                {
                    case ("DGVLIS_SIMPLECLICK"):
                        _selectedIndex = int.Parse(Request.Params["__EVENTARGUMENT"].ToString());

                        if (dgvlistNotas.Rows[_selectedIndex].Cells[5].Text.Trim() != "Pagada")
                        {
                            dgvlistNotas.SelectedIndex = _selectedIndex;                           

                        }
                        else
                        {
                            dgvlistNotas.SelectedIndex = -1;
                        }
                        lblEstado.Value = "EMPTY";
                        break;
                }
            }
            else
            {
                iniciar_valores();
                CargarProveedores(1);
                HabilitarBtn(btnImprimir, false);
            }
        }

        protected void dgvListNotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _cError = "";
            try
            {
                foreach (GridViewRow row in dgvlistNotas.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = "dgvListNotasClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = "dgvListNotasClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }


        }
        #region FUNCIONES DEL BOTONES
        protected void btnNuevo_Click(object sender, EventArgs e)
        {


        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnPago.Visible = false;
            pnlista.Visible = true;
            HabilitarBtn(btnProcesar, true);
            HabilitarBtn(btnCancelar, false);
            btnCancelar.Visible = false;
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            int opeCod;
            int respuesta;
            opeCod = Convert.ToInt32(lbid_detletra.Value);

            clsdetletra form = new clsdetletra();
            det_letra obj = new det_letra();
            obj = form.GetDetLetra(opeCod);
            obj.cestado = "1";
            obj.ninteres = Convert.ToDecimal("00.00");
            //obj.dfecpago = 'NULL';
            respuesta = form.fnDetLetraUpdate(obj);
            if (respuesta == 0)
            {
                lblOkMensaje.Text = "ANULACION SATISFACTORIA.";
                pnMensaje.Visible = true;
                pnPago.Visible = false;
                HabilitarBtn(btnCancelar, false);
                btnCancelar.Visible = false;

            }
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            pnPago.Visible = true;
            pnlista.Visible = false;

            int nOpeCod = int.Parse(dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[2].Text);
            lbid_detletra.Value = nOpeCod.ToString();
            lbldfecVcnt.Text = dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[3].Text;
            lblProveedor.Text = dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[7].Text;
            lblCuota.Text = dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[4].Text;
            lblMonto.Text = dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[11].Text;
            txtinter.Text = "0.00";
            txtFecPag.Text = DateTime.Today.ToString("yyyy-MM-dd");

            if (dgvlistNotas.Rows[dgvlistNotas.SelectedIndex].Cells[8].Text == "X Pagar")
            {
                btnPagar.Text = "Procesar Pago";
                btnAnulPago.Visible = false;
            }
            else
            {
                btnPagar.Text = "Actualizar Pago";
                btnAnulPago.Visible = true;
            }
            HabilitarBtn(btnProcesar, false);
            HabilitarBtn(btnCancelar, true);
            btnCancelar.Visible = true;

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlista.Visible = true;
            pnMensaje.Visible = false;
            DateTime cfecdesde, cfechasta;
            int nPrvCod = int.Parse(ddlProveedor.SelectedValue);
            String cEstadoCod = "P";
            int ifecdesde = txtFecDesde.Text.ToString().Length;
            int ifechasta = txtFechaHasta.Text.ToString().Length;
            if (ifecdesde > 0)
            {
                cfecdesde = Convert.ToDateTime(txtFecDesde.Text.ToString());
            }
            else
            {
                cfecdesde = Convert.ToDateTime("1990-01-01");
            }
            if (ifechasta > 0)
            {
                cfechasta = Convert.ToDateTime(txtFechaHasta.Text.ToString());
            }
            else
            {
                cfechasta = Convert.ToDateTime("2230-01-01");
            }
            clslistdetletra lsdetlet = new clslistdetletra();
            DataTable dtlsdetlet;
            dtlsdetlet = lsdetlet.GetList(cEstadoCod, cfecdesde, cfechasta, nPrvCod, "C");
            dgvlistNotas.DataSource = dtlsdetlet;
            dgvlistNotas.DataBind();
            HabilitarBtn(btnImprimir, true);
            
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            int opeCod;
            int respuesta;
            opeCod = Convert.ToInt32(lbid_detletra.Value);

            clsdetletra form = new clsdetletra();
            det_letra obj = new det_letra();
            obj = form.GetDetLetra(opeCod);
            obj.cestado = "2";
            obj.ninteres = Convert.ToDecimal(txtinter.Text);
            obj.dfecpago = Convert.ToDateTime(txtFecPag.Text);
            respuesta = form.fnDetLetraUpdate(obj);
            if (respuesta == 0)
            {
                if (btnPagar.Text == "Actualizar Pago")
                    lblOkMensaje.Text = "LA ACTUALIZACION FUE SATISFACTORIA.";
                else
                    lblOkMensaje.Text = "EL INGRESO FUE SATISFACTORIO.";
                pnMensaje.Visible = true;
                pnPago.Visible = false;
                HabilitarBtn(btnCancelar, false);
                btnCancelar.Visible = false;

            }
        }
        #endregion

        #region FUNCIONES GENERALES
        private void iniciar_valores()
        {
            btnNuevo.Visible = false;
            btnBuscar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            btnEditar.Visible = false;
            btnAnular.Visible = false;
            btnProcesar.Visible = false;
            btnImprimir.Visible = false;


            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnProcesar, false);
            HabilitarBtn(btnImprimir, false);
            HabilitarBtn(btnBuscar, true);

            pnPago.Visible = false;
            pnlista.Visible = false;
            pnMensaje.Visible = false;

            //lbmensaje.Visible = false;
            //lbmensaje.Text = "Es necesario llenar todos los campos solicitados (*)";
            ///DATOS DE PAGO//////////////
            lbid_detletra.Value = "";
            lbldfecVcnt.Text = "";
            lblProveedor.Text = "";
            lblCuota.Text = "";
            lblMonto.Text = "";
            txtinter.Text = "0.00";
        }
        public void CargarProveedores(int i)
        {
            clsProveedores lstProveedores = new clsProveedores();

            if (i == 1)
            {
                ddlProveedor.DataSource = lstProveedores.GetAll();
                ddlProveedor.DataBind();
                //ddlProveedor.Items.Add(new ListItem("[NUEVO PROVEEDOR]", "999"));
                ddlProveedor.Items.Insert(0, new ListItem("", "000"));
            }
            else
            {
                //ddlproveedor_new.DataSource = lstProveedores.GetAll();
                //ddlproveedor_new.DataBind();
                ////ddlProveedor.Items.Add(new ListItem("[NUEVO PROVEEDOR]", "999"));
                //ddlproveedor_new.Items.Insert(0, new ListItem("", "000"));
            }
            lstProveedores = null;

        }
        #endregion

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }

    }
}