using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using pryAgrocomercioBLL.Maestros;

namespace AgrocomercioWEB.Maestros.Reportes
{
    public partial class wfrmReporteArticulos : BasePage
    {

#region "Atributos y Propiedades"
    private string  _lstCodProveedor = string.Empty ;
    public string CodProveedor
    {
        get
        {   _lstCodProveedor = LeerVariableSesion("_lstCodProveedor").ToString();
            return _lstCodProveedor;
        }

        set
        {   _lstCodProveedor = value;
            this.AgregarVariableSession ("_lstCodProveedor",_lstCodProveedor);
        }
    }

    private string  _lstNomProveedor  = string.Empty;
    public string NomProveedor
    {
       get
       {    _lstNomProveedor = LeerVariableSesion("_lstNomProveedor").ToString();
            return _lstNomProveedor;
        }

        set
        {    _lstNomProveedor = value;
             this.AgregarVariableSession("_lstNomProveedor", _lstNomProveedor);
        }
    }
#endregion

#region "Metodos privados del Web Form"

    private void LlenarMeses()
    {
        
            this.ddlMesDesde.Items.Clear();
            this.ddlMesDesde.Items.Add("");
            this.ddlMesDesde.Items.Add("Enero");
            this.ddlMesDesde.Items.Add("Febrero");
            this.ddlMesDesde.Items.Add("Marzo");
            this.ddlMesDesde.Items.Add("Abril");
            this.ddlMesDesde.Items.Add("Mayo");
            this.ddlMesDesde.Items.Add("Junio");
            this.ddlMesDesde.Items.Add("Julio");
            this.ddlMesDesde.Items.Add("Agosto");
            this.ddlMesDesde.Items.Add("Septiembre");
            this.ddlMesDesde.Items.Add("Octubre");
            this.ddlMesDesde.Items.Add("Noviembre");
            this.ddlMesDesde.Items.Add("Diciembre");
        
         
            this.ddlMesHasta.Items.Clear();
            this.ddlMesHasta.Items.Add("");
            this.ddlMesHasta.Items.Add("Enero");
            this.ddlMesHasta.Items.Add("Febrero");
            this.ddlMesHasta.Items.Add("Marzo");
            this.ddlMesHasta.Items.Add("Abril");
            this.ddlMesHasta.Items.Add("Mayo");
            this.ddlMesHasta.Items.Add("Junio");
            this.ddlMesHasta.Items.Add("Julio");
            this.ddlMesHasta.Items.Add("Agosto");
            this.ddlMesHasta.Items.Add("Septiembre");
            this.ddlMesHasta.Items.Add("Octubre");
            this.ddlMesHasta.Items.Add("Noviembre");
            this.ddlMesHasta.Items.Add("Diciembre");
        
        this.ddlMesDesde.SelectedIndex = 1;
        this.ddlMesHasta.SelectedIndex = DateTime.Today.Month;
    }
    private void LlenarAños()
    {
        //cargamos los combos de meses 
        int  linAnio = 2000;
        for (int a = 1; a<= 15;a++)
        {
            this.ddlAnioDesde.Items.Add( linAnio.ToString());
            this.ddlAnioHasta.Items.Add( linAnio.ToString());
            linAnio = linAnio + 1;
        }
        this.ddlAnioDesde.Text = DateTime.Today.Year.ToString();
        this.ddlAnioHasta.Text = DateTime.Today.Year.ToString();
    }
    private void LlenarProveedores()
    {
        DataTable  dtProveedores = new DataTable();
        CProveedor oProveedor = new CProveedor();
        dtProveedores = oProveedor.fnListaProveedores(false );

        lbxLaboratorios.DataSource = dtProveedores;
        lbxLaboratorios.DataValueField = "prvCod";
        lbxLaboratorios.DataTextField = "prvRazon";
        lbxLaboratorios.DataBind();
    }
#endregion
#region "Eventos del Web Form"
    private void Evento_Imprimir()
    {
        string lstFecDesde  = "";
        string lstFecHasta = "";

        int  linCodAgencia = this.lbxLaboratorios.SelectedIndex ;
        if( linCodAgencia <= 0 )
        {
            this.MessageBox ("Debe seleccionar una Agencia");
            this.lblMensajes.Text = "Debe seleccionar una Agencia";
            this.lblMensajes.Visible = true;
            return ;
        }
        else
        {
            //verifica si se filtrará por fechas
            if( rdoDesdeHasta.Checked )
            {
                //'''''''''''''''''''''''''''''''''''''''''''
                //' Debo verificar las fechas desde - hasta'
                //'''''''''''''''''''''''''''''''''''''''''''
                //if( string.Format(this.ddlAnioDesde.SelectedItem.Value, "0000") > string.Format(this.ddlAnioHasta.SelectedItem.Value, "0000"))
                if( Convert.ToInt32( this.ddlAnioDesde.SelectedItem.Value) > Convert.ToInt32( this.ddlAnioHasta.SelectedItem.Value))
                {
                    this.MessageBox("Imposible realizar el filtro, Valores de Años incorrectos");
                    this.lblMensajes.Text = "Imposible realizar el filtro, Valores de Años incorrectos";
                    this.lblMensajes.Visible = true;

                    return;
                }
                else
                    if (this.ddlAnioDesde.SelectedItem.Value == this.ddlAnioHasta.SelectedItem.Value)
                    {
                        //'El año desde es = al año hasta, controlo los meses
                        //if (string.Format(this.ddlMesDesde.SelectedItem.Value, "00") > string.Format(this.ddlMesDesde.SelectedItem.Value, "00") )
                        if (Convert.ToInt32( this.ddlMesDesde.SelectedItem.Value) > Convert.ToInt32( this.ddlMesDesde.SelectedItem.Value ))
                        {
                            this.MessageBox("Imposible realizar el filtro, Valores de Meses incorrectos");
                            this.lblMensajes.Text = "Imposible realizar el filtro, Valores de Meses incorrectos";
                            this.lblMensajes.Visible = true;

                            return;
                        }
                    }
            }

            lstFecDesde = this.ddlAnioDesde.SelectedItem.Value + string.Format( this.ddlMesDesde.SelectedIndex.ToString(), "00");
            lstFecHasta = this.ddlAnioHasta.SelectedItem.Value + string.Format(this.ddlMesHasta.SelectedIndex.ToString(), "00");
            this.CodProveedor = lbxLaboratorios .SelectedItem.Value;
            this.NomProveedor = lbxLaboratorios.SelectedItem.Text;

            Server.Transfer("wfrmVistasReportes.aspx?NumRep=1&CodProvee=" + this.CodProveedor + "&nProvee=" + this.NomProveedor + "&fDes=" + lstFecDesde + "&fHas=" + lstFecHasta);
        }
        //else
        //{
        //    //''se filtrará sólo por agencia
        //    Server.Transfer("wfrmVistasReportes.aspx?NumRep=1&CodProvee=" + this.CodProveedor + "&nProvee=" + this.NomProveedor);
        //}

     
    }
#endregion

        protected void ibtImprimir_Click(object sender, ImageClickEventArgs e)
        {
            Evento_Imprimir();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             if( !Page.IsPostBack)
             {
                LlenarAños();
                LlenarMeses();
                LlenarProveedores();
                lblMensajes.Visible = false;

             }
        }

        protected void ibtImprimir_Init(object sender, EventArgs e)
        {
            ibtImprimir.Attributes.Add("onmouseover", "this.style.borderColor='#8C3522';this.style.borderWidth=2;");
            ibtImprimir.Attributes.Add("onmouseout", "this.style.borderColor='white';this.style.borderWidth=2;");

        }

        protected void rdoTodo_CheckedChanged(object sender, EventArgs e)
        {
                    if( this.rdoTodo.Checked == true)
                    {
                        this.ddlAnioDesde.Enabled = false; ;
                        this.ddlAnioHasta.Enabled = false; ;
                        this.ddlMesDesde.Enabled = false; ;
                        this.ddlMesHasta.Enabled = false; ;
                        this.ddlMesDesde.SelectedIndex = 0;
                        this.ddlMesHasta.SelectedIndex = 0;
                        this.ddlAnioDesde.SelectedIndex = 0;
                        this.ddlAnioHasta.SelectedIndex = 2;
                    }
                    else 
                    {
                        this.ddlAnioDesde.Enabled = true;
                        this.ddlAnioHasta.Enabled =  true;
                        this.ddlMesDesde.Enabled =  true;
                        this.ddlMesHasta.Enabled = true;
                    }

        }

        protected void rdoDesdeHasta_CheckedChanged(object sender, EventArgs e)
        {
            if( this.rdoDesdeHasta.Checked == true )
            {
                this.ddlAnioDesde.Enabled = true;
                this.ddlAnioHasta.Enabled = true;
                this.ddlMesDesde.Enabled = true;
                this.ddlMesHasta.Enabled = true;
            }
        else
            {
                this.ddlAnioDesde.Enabled = false;
                this.ddlAnioHasta.Enabled = false;
                this.ddlMesDesde.Enabled = false;
                this.ddlMesHasta.Enabled = false;

                this.ddlMesDesde.SelectedIndex = 0;
                this.ddlMesHasta.SelectedIndex = 0;
                this.ddlAnioDesde.SelectedIndex = 0;
                this.ddlAnioHasta.SelectedIndex = 0;
            }

        }

        protected void lbtImprimir_Click(object sender, EventArgs e)
        {
            Evento_Imprimir();
        }
    }
}