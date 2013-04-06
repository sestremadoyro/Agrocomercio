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
    public partial class wfrmcntrPagVentas : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFec.Attributes.Add("readonly", "readonly");
            if (Page.IsPostBack)
            { }
            else
            {
                iniciar_valores();

            }

        }

        #region FUNCIONES DEL BOTONES
        protected void btnNuevo_Click(object sender, EventArgs e) { }
        protected void btnGuardar_Click(object sender, EventArgs e) { }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciar_valores();
        }
        protected void btnEditar_Click(object sender, EventArgs e) { }
        protected void btnAnular_Click(object sender, EventArgs e) { }
        protected void btnProcesar_Click(object sender, EventArgs e) { }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlista.Visible = true;
            btnpagar.Visible = false;
            String cliente = txtNomCli.Text;
            int nPrvCod = int.Parse(ddlVendedor.SelectedValue);
            clspag_pend form = new clspag_pend();
            clspag_pendnow form2 = new clspag_pendnow();

            DataTable dtlista;
            if (rbtodos.Checked == true)
                dtlista = form.GetList(nPrvCod, cliente, "V");
            else
                dtlista = form2.GetList(nPrvCod, cliente, "V");
            dgvlist.DataSource = dtlista;
            if (dtlista.Rows.Count > 0)
            {
                btnpagar.Visible = true;

            }

            dgvlist.DataBind();
            dgvlist.Columns[13].Visible = false;
            dgvlist.Columns[14].Visible = false;
            dgvlist.Columns[15].Visible = false;
            dgvlist.Columns[16].Visible = false;
            dgvlist.Columns[5].Visible = false;

        }
        protected void btnBusPend_Click(object sender, EventArgs e) { }
        protected void btnGenerar_Click(object sender, EventArgs e) { }
        protected void btnDetalle_Click(object sender, EventArgs e) { }
        protected void btnPagar_Click(object sender, EventArgs e)
        {
            int cod_det;
            int ultimo;
            int cod_ven;
            int cod_letra;
            int cod_cliente;
            decimal monto_total;
            decimal monto_cuota;
            decimal pago;
            string fecha;
            int flag = 0;
            clspag_Letra form = new clspag_Letra();
            int cod_mov = Convert.ToInt32(form.Maxcod_pago()) + 1;
            if (btnpagar.Text == "Pagar")
            {
                pncargar.Visible = true;
                dgvlist.Columns[13].Visible = true;
                txtFec.Text = DateTime.Today.ToString();
                btnpagar.Text = "Guargar";
                HabilitarBtn(btnCancelar, true);
                btnCancelar.Visible = true;
            }
            else
            {
                dgvlist.Columns[5].Visible = true;
                dgvlist.Columns[14].Visible = true;
                dgvlist.Columns[15].Visible = true;
                dgvlist.Columns[16].Visible = true;
                foreach (GridViewRow row in dgvlist.Rows)
                {
                    flag = 0;
                    fecha = txtFec.Text;
                    TextBox pag = row.FindControl("txtmonto") as TextBox;
                    cod_det = Convert.ToInt32(row.Cells[2].Text);
                    cod_ven = Convert.ToInt32(row.Cells[5].Text);
                    cod_letra = Convert.ToInt32(row.Cells[14].Text);
                    cod_cliente = Convert.ToInt32(row.Cells[16].Text);
                    ultimo = Convert.ToInt32(row.Cells[15].Text);
                    pago = Convert.ToDecimal(pag.Text);
                    if (pago > 0)
                    {
                        monto_total = Convert.ToDecimal(row.Cells[10].Text);
                        monto_cuota = Convert.ToDecimal(row.Cells[12].Text);

                        while (flag == 0)
                        {
                            Pag_letras obj = new Pag_letras();
                            if (pago > monto_cuota)
                            {

                                clsdetletra update = new clsdetletra();
                                det_letra obj_upd = new det_letra();
                                obj_upd = update.GetDetLetra(cod_det);
                                obj_upd.cestado = "2";
                                obj_upd.dfecpago = Convert.ToDateTime(fecha);

                                update.fnDetLetraUpdate(obj_upd);

                                if (ultimo == 0)
                                {
                                    ////////////////////
                                    //crea la nota/////
                                    ////////////////////
                                    clsOpeNotas formnota = new clsOpeNotas();
                                    Notas objnt = new Notas();
                                    objnt.inota = Convert.ToInt32(formnota.MaxOpeCod()) + 1;

                                    objnt.dfecreg = Convert.ToDateTime(fecha);
                                    objnt.ccodnota = cod_letra.ToString();
                                    objnt.iprvcod = cod_cliente;
                                    objnt.nmontoNota = pago - monto_cuota;
                                    objnt.cestadoNota = "N";
                                    objnt.nmntutilizado = Convert.ToDecimal(0.00);
                                    objnt.cobservaciones = "pago excesivo de factura";
                                    objnt.ctipo = "V";
                                    formnota.fnNotaInsert(objnt);

                                    ////////////////////////////////////////

                                    flag = 1;
                                }
                                else
                                {
                                    obj.idetletra = obj_upd.idetletra;
                                    obj.idpagletra = Convert.ToInt32(form.Maxidpaglet()) + 1;
                                    obj.nmonto = monto_cuota;
                                    obj.dfecpago = Convert.ToDateTime(fecha);
                                    obj.dfecmodifi = DateTime.Today;
                                    obj.ipercod = cod_ven;
                                    obj.icodpago = cod_mov;
                                    clspag_Letra inserta = new clspag_Letra();
                                    inserta.fnpag_letraInsertar(obj);

                                    pago = pago - monto_cuota;
                                    cod_det = Convert.ToInt32(update.Mindetletpend_cod(cod_letra));
                                    monto_cuota = fnnextDeuda(cod_det);
                                }
                            }
                            else
                            {
                                obj.idetletra = cod_det;
                                obj.idpagletra = Convert.ToInt32(form.Maxidpaglet()) + 1;
                                obj.nmonto = pago;
                                obj.dfecpago = Convert.ToDateTime(fecha);
                                obj.dfecmodifi = DateTime.Today;
                                obj.ipercod = cod_ven;
                                obj.icodpago = cod_mov;
                                clspag_Letra inserta = new clspag_Letra();
                                inserta.fnpag_letraInsertar(obj);
                                if (pago == monto_cuota)
                                {
                                    clsdetletra update = new clsdetletra();
                                    det_letra obj_upd = new det_letra();
                                    obj_upd = update.GetDetLetra(cod_det);
                                    obj_upd.cestado = "2";
                                    obj_upd.dfecpago = Convert.ToDateTime(fecha);

                                    update.fnDetLetraUpdate(obj_upd);


                                }
                                flag = 1;

                            }
                        }
                    }
                }
                btnpagar.Text = "Pagar";
                iniciar_valores();
            }
        }
        #endregion

        #region FUNCIONES DE TABLAS
        protected void dgvList_RowDataBound(object sender, GridViewRowEventArgs e) { }
        #endregion

        #region FUNCIONES GENERALES
        private void iniciar_valores()
        {
            btnNuevo.Visible = true;
            //btnBuscar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            btnEditar.Visible = false;
            btnAnular.Visible = false;
            btnProcesar.Visible = false;
            btnImprimir.Visible = false;
            btnpagar.Visible = false;

            pnlista.Visible = false;
            pncargar.Visible = false;
            btnpagar.Text = "Pagar";
            CargarVendedor();
        }

        public void CargarVendedor()
        {
            clsPersonal lstTrabajadores = new clsPersonal();

            ddlVendedor.DataSource = lstTrabajadores.GetList(0);
            ddlVendedor.DataBind();
            ddlVendedor.Items.Insert(0, new ListItem("", "000"));

            lstTrabajadores = null;

        }
        public decimal fnnextDeuda(int cod_Det)
        {
            clsdetletra form = new clsdetletra();
            det_letra obj = new det_letra();
            obj = form.GetDetLetra(cod_Det);

            return Convert.ToDecimal(obj.nmonto);
        }
        #endregion
    }
}