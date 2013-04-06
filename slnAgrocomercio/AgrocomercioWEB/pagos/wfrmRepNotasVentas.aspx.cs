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
//using pryAgrocomercioBLL.Clases;
using pryAgrocomercioDAL;

using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Configuration;
using AgrocomercioWEB;
using System.Threading;
namespace AgrocomercioWEB.pagos
{
    public partial class wfrmRepNotasVentas : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
            }
            else {
                iniciarCampos();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int tiprep;
            HabilitarBtn(btnImprimir, true);
            if(rbResumido .Checked ==true){
                tiprep = 1;
                pnLista.Visible = true;
                pnListDet.Visible = false;                
                clsreptNotasGen form = new clsreptNotasGen();
                DataTable dtlsdetlet;
                DateTime fec_desde, fec_hasta;
                int tip_search;
                fec_desde = Convert.ToDateTime ("1900-01-01");
                fec_hasta = Convert.ToDateTime ("2050-01-01");
                int nPrvCod = int.Parse(ddlProveedor.SelectedValue);
                //String _LetEst, int _cod_prd, DateTime _fecdesde, DateTime _fechasta, String _valor, int tipo
                if(nPrvCod >0)
                    tip_search =4;
                else
                    tip_search =3;
                dtlsdetlet = form.GetList("T", nPrvCod, fec_desde, fec_hasta, "V", tip_search);
                dgvLista.DataSource = dtlsdetlet;
                dgvLista.DataBind();

                AgregarVariableSession("dtRepNotasGen", dtlsdetlet);
                
            }
            else{
                tiprep = 2;
                pnListDet.Visible = true;
                pnLista.Visible = false;
                clsreptNotasDet form2 = new clsreptNotasDet();
                DataTable dtlsdetlet;
                DateTime fec_desde, fec_hasta;
                int tip_search;
                fec_desde = Convert.ToDateTime ("1900-01-01");
                fec_hasta = Convert.ToDateTime ("2050-01-01");
                int nPrvCod = int.Parse(ddlProveedor.SelectedValue);
                //String _LetEst, int _cod_prd, DateTime _fecdesde, DateTime _fechasta, String _valor, int tipo
                if(nPrvCod >0)
                tip_search =4;
                else
                tip_search =3;
                dtlsdetlet = form2.GetList("T", nPrvCod, fec_desde, fec_hasta, "V", tip_search);
                dgvlistDet.DataSource = dtlsdetlet;
                dgvlistDet.DataBind();

                AgregarVariableSession("dtRepNotasDet", dtlsdetlet);
            }
            AgregarVariableSession("tiprep", tiprep);
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        private void iniciarCampos() {
            HabilitarBtn(btnNuevo, false);
            HabilitarBtn(btnGuardar, false);
            HabilitarBtn(btnAnular, false);
            HabilitarBtn(btnCancelar, false);
            HabilitarBtn(btnEditar, false);
            HabilitarBtn(btnImprimir, false);
            HabilitarBtn(btnProcesar, true);

            btnNuevo.Visible= false;
            btnGuardar.Visible = false;
            btnAnular.Visible=false;
            btnCancelar.Visible = false;
            btnEditar.Visible=false;
            btnImprimir.Visible=true;
            btnProcesar.Visible=true;

            pnLista.Visible = false;

            CargarProveedores();
            
        }
        public void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();
            ddlProveedor.DataSource = lstProveedores.GetAll();
            ddlProveedor.DataBind();
            //ddlProveedor.Items.Add(new ListItem("[NUEVO PROVEEDOR]", "999"));
            ddlProveedor.Items.Insert(0, new ListItem("", "000"));
            
        }
    }
}