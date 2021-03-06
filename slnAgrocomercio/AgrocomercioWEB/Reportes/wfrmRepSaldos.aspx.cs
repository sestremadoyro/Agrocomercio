﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pryAgrocomercioBLL.EntityCollection;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL;

namespace AgrocomercioWEB.Reportes.rpt
{
    public partial class wfrmRepSaldos :  BasePage
    {
        public void CargarProveedores()
        {
            clsProveedores lstProveedores = new clsProveedores();
            ddlProveedor.DataSource = lstProveedores.GetAll();
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("[Seleccione]", "9999"));
            ddlProveedor.Items.Add(new ListItem("<< SIN RESULTADOS >>", "8888"));
            lstProveedores = null;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProveedores();
            }
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnImprimir.OnClientClick = "AbrirVentanaImprimeSaldo()";
            
           
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            
            DataTable dtResultado = null;
            CReportes oReportes = new CReportes();
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;
            int nPrvCod = 0;
             try
            {

                nPrvCod = int.Parse(ddlProveedor.SelectedValue);
                Usuarios objUsuario = (Usuarios)LeerVariableSesion("oUsuario");

                if (objUsuario.RolCod == 1)
                {
                    dgvLista.Visible = true;
                    dgvLista2.Visible = false;
                    dtResultado = oReportes.fnListaSaldosDataTable(nPrvCod);
                }
                else
                {
                    dgvLista.Visible = false;
                    dgvLista2.Visible = true;
                    dtResultado = oReportes.fnListaSaldosRestringidaDataTable(nPrvCod);
                }        

                if (dtResultado.Rows.Count > 0)
                {
                    if (objUsuario.RolCod == 1)
                    {
                        dgvLista.DataSource = dtResultado;
                        dgvLista.DataBind();
                    }
                    else
                    {
                        dgvLista2.DataSource = dtResultado;
                        dgvLista2.DataBind();
                    }
                    AgregarVariableSession("dtSaldos", dtResultado);
                    AgregarVariableSession("nPrvCod", nPrvCod);

                }
            }
             catch (Exception ex)
             {
                 MessageBox("Error Interno: " + ex.Message);
             }
        }

    }
}