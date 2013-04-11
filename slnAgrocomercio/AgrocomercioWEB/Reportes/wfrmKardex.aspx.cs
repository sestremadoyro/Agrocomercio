﻿using System;
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
using Obout.Grid;
using Obout.ComboBox;

namespace AgrocomercioWEB.Reportes
{

    public partial class wfrmKardex : BasePage
    {
        public String _click = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SetEstado("INI");
            if (Page.IsPostBack)
            {
               
            }
            
        }

#region FUNCIONES DEL FORMULARIO

     
        
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string cMensaje = "";
            DataTable dtResultado = null;
            clsOperaciones colOperaciones = new clsOperaciones();
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;
            int prvCod = 0;
            try
            {
                dtResultado = colOperaciones.RepVentasXComprar(prvCod, false, dFecIni, dFecFin);

                if (dtResultado.Rows.Count > 0)
                {
                    SetEstado("PRO");

                    gridVentasxCobrar.DataSource = dtResultado;
                    gridVentasxCobrar.DataBind();

                    AgregarVariableSession("dtRepClientes", dtResultado);
                    AgregarVariableSession("nTipCam", txtTipCam.Text);
                }
                else
                    SetEstado("ERR");
            }
            catch (Exception ex)
            {
                MessageBox("Error Interno: " + ex.Message);
            }
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            SetEstado("PRO");
        }
      

        

#endregion

#region FUNCIONES DE CARGA DE LISTAS DE DATOS

        //public void CargarCombos()
        //{
        //    clsAtributos Atributos = new clsAtributos();

        //   //Cargamos combo de Zonas
            
        //    ddlZonas.DataSource = Atributos.ListAtributos(4);
        //    ddlZonas.DataBind();

        //    lblTasIGV.Value = ((List<Atributos>)Atributos.ListAtributos(7)).FirstOrDefault().AtrDescripcion;

        //    CargarTipoCambio();
        //    Atributos = null;
        //}
        
#endregion

#region FUNCIONES GENERALES
        private void SetEstado(string cTipo)
        {
            switch (cTipo)
            {
                case ("INI"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                                lblExito.Visible = false;
                    lblError.Visible = false;

                    clsAtributos Atributos = new clsAtributos();
                    var oTip = ((List<Atributos>)Atributos.ListAtributos(8)).FirstOrDefault();
                    if (oTip == null) 
                        txtTipCam.Text = "2.56";
                    else
                        txtTipCam.Text = oTip.AtrDescripcion;
                    Atributos = null;

                    break;
                case ("PRO"):
                    HabilitarBtn(btnProcesar, false);
                    HabilitarBtn(btnImprimir, true);
                    lblExito.Visible = true;
                    lblError.Visible = false;
                    break;
                case ("ERR"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                    lblExito.Visible = false;
                    lblError.Visible = true;
                    break;
            }

            
        }
        
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static String[] BuscarClientes(string prefixText)
        {
            String[] sList = null;
            List<string> sClienteList = new List<string>();
            clsClientes lstClientes = new clsClientes();

            try
            {
                DataTable dtClientes = lstClientes.BuscarClientes(prefixText);

                if (dtClientes.Rows.Count > 0)
                {
                    for (int i = 0; i < dtClientes.Rows.Count; i++)
                    {
                        sClienteList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem(dtClientes.Rows[i]["CliNombre"].ToString(), dtClientes.Rows[i]["CliCod"].ToString()));
                    }
                    sList = new String[10];
                    sList = sClienteList.ToArray();
                }
                else
                {
                    sClienteList.Add(AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem("[NUEVO CLIENTE]", "999"));

                    sList = new String[1];
                    sList = sClienteList.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lstClientes = null;
            }
            return sList;
        }

#endregion

        protected void gridVentasxCobrar_ColumnsCreated(object sender, EventArgs e)
        {
            //int width = 100;
            //int count = gridVentasxCobrar.Columns.Count;
            //int average = 0;
            //if (count > 0)
            //    average = width / count;
            //int i = 0;

            //foreach (Column column in gridVentasxCobrar.Columns)
            //{
            //    if (i < count - 1)
            //    {                    
            //        //column.Width = average.ToString() + "%";
            //        column.Width = (column.HeaderText.Length * 10).ToString();
            //    }                

            //    //width -= average;
            //    i++;
            //}
        }

        protected void gridVentasxCobrar_Filtering(object sender, EventArgs e)
        {
            // filter for OrderDate
            Column orderDateColumn = gridVentasxCobrar.Columns[1];

            if (orderDateColumn.FilterCriteria.Option is CustomFilterOption)
            {
                CustomFilterOption filterOption = orderDateColumn.FilterCriteria.Option as CustomFilterOption;

                switch (filterOption.ID)
                {
                    case "Between_OpeFecEmision":
                        string startDate = orderDateColumn.FilterCriteria.Values["StartDate_OpeFecEmision"].ToString();
                        string endDate = orderDateColumn.FilterCriteria.Values["EndDate_OpeFecEmision"].ToString();

                        if (startDate != "" && endDate != "")
                        {
                            DateTime startDate2 = DateTime.Parse(startDate);
                            DateTime endDate2 = DateTime.Parse(endDate);

                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                // we filter between start date at 12:00AM and end date at 11:59PM
                                //orderDateColumn.FilterCriteria.FilterExpression = "(" + orderDateColumn.DataField + " >= #" + startDate + " 00:00:01 # AND " + orderDateColumn.DataField + " <= #" + endDate + " 24:59:59 #)";
                                orderDateColumn.FilterCriteria.FilterExpression = "(" + orderDateColumn.DataField + " >= '" + startDate2.ToString("yyyy-MM-dd") + "' AND " + orderDateColumn.DataField + " <= '" + endDate2.ToString("yyyy-MM-dd") + "' )";
                            }
                        }
                       
                        break;                   
                }
            }

        }

        //protected void ddlZonas_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        //{
        //    clsAtributos Atributos = new clsAtributos();

        //    //Cargamos Zonas
        //    var lstZonas = Atributos.ListAtributos(4);
        //    (sender as ComboBox).DataSource = lstZonas;
        //    (sender as ComboBox).DataBind();
        //    // Looping through the items and adding them to the "Items" collection of the ComboBox
        //    //foreach (object Row in lstZonas)
        //    //{
        //    //    (sender as ComboBox).Items.Add(new ComboBoxItem(Row.["CountryName"].ToString(), data.Rows[i]["CountryName"].ToString()));
        //    //}
            
        //    //e.ItemsLoadedCount = data.Rows.Count;
        //    //e.ItemsCount = data.Rows.Count;
        //    Atributos = null;
        //}
       

        
    }
}

