using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;
using System.Data;
using System.Text;

namespace AgrocomercioWEB
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CargarTipos();
            }
            txtTotal.Value = txtArtCant22.Value + txtArtCant33.Value ;

        }

        public void CargarTipos() {
            clsAtributos lstAtributos = new clsAtributos();
            gvTipos.DataSource = lstAtributos.GetAll();
            gvTipos.DataBind();
            lstAtributos = null;

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            clsAtributos lstAtributos = new clsAtributos();
            Atributos oAtribu = new Atributos();
            oAtribu.AtrTipoCod = int.Parse(txtUso.Text);
            oAtribu.AtrCodigo = txtCodigo.Text;
            oAtribu.AtrDescripcion = txtDescripcion.Text;
            oAtribu.AtrEstado = true;
            oAtribu.AtrNivel = 1;

            lstAtributos.Add(oAtribu);
            lstAtributos.SaveChanges();
            lstAtributos = null;
            oAtribu = null;

            CargarTipos();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string tdoCod = "";
            tdoCod = txtCodigo.Text;

            clsAtributos lstAtributos = new clsAtributos();
            Atributos oAtribu = new Atributos();

            oAtribu = lstAtributos.Find(Tip => Tip.AtrCodigo == tdoCod).First<Atributos>();
            oAtribu.AtrDescripcion = txtDescripcion.Text;
            oAtribu.AtrTipoCod = int.Parse( txtUso.Text);

            lstAtributos.Update(oAtribu);
            lstAtributos.SaveChanges();

            lstAtributos = null;
            oAtribu = null;

            CargarTipos();
        }

        protected void gvTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tdoCod = "";
            tdoCod = gvTipos.SelectedValue.ToString();

            clsAtributos lstAtributos = new clsAtributos();
            Atributos oAtribu = new Atributos();

            oAtribu = lstAtributos.Find(Tip => Tip.AtrCodigo == tdoCod).First<Atributos>();
            txtCodigo.Text = oAtribu.AtrCodigo.ToString();
            txtDescripcion.Text = oAtribu.AtrDescripcion;
            txtUso.Text = oAtribu.AtrTipoCod.ToString();

            lstAtributos = null;
            oAtribu = null;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string tdoCod = "";
            tdoCod = gvTipos.SelectedValue.ToString();

            clsAtributos lstAtributos = new clsAtributos();
            Atributos oAtribu = new Atributos();

            oAtribu = lstAtributos.Find(Tip => Tip.AtrCodigo == tdoCod).First<Atributos>();
            lstAtributos.Delete(oAtribu);
            lstAtributos.SaveChanges();

            lstAtributos = null;
            oAtribu = null;

            CargarTipos();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCountries(string prefixText)
        {
            clsAtributos lstAtributos = new clsAtributos();
            Atributos oAtribu = new Atributos();

            var objCompanyList = lstAtributos.Find(Tip => Tip.AtrDescripcion.Contains(prefixText)).Select(Tip => Tip.AtrDescripcion).ToList();

            lstAtributos = null;
            oAtribu = null;


            return objCompanyList;
        }

        
    }
}

