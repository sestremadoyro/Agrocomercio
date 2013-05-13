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
using AgrocomercioWEB;
using Obout.Grid;
using System.Collections;

namespace AgrocomercioWEB.Reportes
{

    public partial class wfrmTotalPorProveedor : BasePage
    {
        int tempTotalStockIni = 0;
        double tempTotalIni = 0;
        int tempCOMPRA_Cantidad = 0;
        double tempCOMPRA_Total = 0;
        int tempVENTA_Cantidad = 0;
        double tempVENTA_Total = 0;
        int tempTotalStockFin = 0;
        double tempTotalFin = 0;

        Dictionary<int, int> TotalStockIni = new Dictionary<int, int>();
        Dictionary<int, double> TotalIni = new Dictionary<int, double>();
        Dictionary<int, int> COMPRA_Cantidad = new Dictionary<int, int>();
        Dictionary<int, double> COMPRA_Total = new Dictionary<int, double>();
        Dictionary<int, int> VENTA_Cantidad = new Dictionary<int, int>();
        Dictionary<int, double> VENTA_Total = new Dictionary<int, double>();
        Dictionary<int, int> TotalStockFin = new Dictionary<int, int>();
        Dictionary<int, double> TotalFin = new Dictionary<int, double>();
        Dictionary<int, GridRow> lastGroupHeaders = new Dictionary<int, GridRow>();
        GridRow lastGroupHeader;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProveedor();
                CargarArticulos();
                CreateGrid();
            }
        }

        public void CreateGrid()
        {
            clsOperaciones colOperaciones = new clsOperaciones();

            IEnumerable<object> lstResultado = colOperaciones.ReporteTotalProveedor();

            gridTotales.DataSource = lstResultado;
            gridTotales.DataBind();
        }

        public void CargarProveedor()
        {
            clsProveedores colProveedores = new clsProveedores();

            cbProveedores.DataSource = colProveedores.GetProveedoresConArticulos();
            cbProveedores.DataBind();

            colProveedores = null;
        }

        public void CargarArticulos()
        {
            clsArticulos colArticulos = new clsArticulos();

            cbArticulos.DataSource = colArticulos.GetAll();
            cbArticulos.DataBind();

            colArticulos = null;
        }

        public void RebindGrid(object sender, EventArgs e)
        {
            CreateGrid();
        }

        protected void gridTotales_RowDataBound(object sender, GridRowEventArgs e)
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                tempTotalStockIni += e.Row.Cells[3].Text == "" ? 0 : (int)double.Parse(e.Row.Cells[3].Text);
                tempTotalIni += e.Row.Cells[4].Text == "" ? 0.0 : double.Parse(e.Row.Cells[4].Text);
                tempCOMPRA_Cantidad += e.Row.Cells[5].Text == "" ? 0 : (int)double.Parse(e.Row.Cells[5].Text);
                tempCOMPRA_Total += e.Row.Cells[6].Text == "" ? 0.0 : double.Parse(e.Row.Cells[6].Text);
                tempVENTA_Cantidad += e.Row.Cells[7].Text == "" ? 0 : (int)double.Parse(e.Row.Cells[7].Text);
                tempVENTA_Total += e.Row.Cells[8].Text == "" ? 0.0 : double.Parse(e.Row.Cells[8].Text);
                tempTotalStockFin += e.Row.Cells[9].Text == "" ? 0 : (int)double.Parse(e.Row.Cells[9].Text);
                tempTotalFin += e.Row.Cells[10].Text == "" ? 0.0 : double.Parse(e.Row.Cells[10].Text);

                //if (lastGroupHeader != null)
                //{
                //    Literal textContainer = lastGroupHeader.Cells[0].Controls[0].Controls[lastGroupHeader.Cells[0].Controls[0].Controls.Count - 1].Controls[0] as Literal;
                //    textContainer.Text = "<div style='margin-right:100px; float:left;' >Articulo: " + ((GridDataControlFieldCell)e.Row.Cells[1]).Text + " - " + ((GridDataControlFieldCell)e.Row.Cells[2]).Text;
                //    textContainer.Text += "</div><div style='float:left;'>";
                //    textContainer.Text += "Stock Inicial: " + ((GridDataControlFieldCell)e.Row.Cells[3]).Text + "</div>";

                //    lastGroupHeader = null;
                //}
            }
            else if (e.Row.RowType == GridRowType.GroupHeader)
            {
                //Literal textContainer = e.Row.Cells[0].Controls[0].Controls[1].Controls[0] as Literal;
                if (!lastGroupHeaders.ContainsKey(e.Row.GroupLevel))
                {
                    lastGroupHeaders.Add(e.Row.GroupLevel, null);
                }
                lastGroupHeaders[e.Row.GroupLevel] = e.Row;

                if (e.Row.GroupLevel == 1)
                {
                    lastGroupHeader = e.Row;
                }
            }
            else if (e.Row.RowType == GridRowType.GroupFooter)
            {
                if (e.Row.GroupLevel > 0)
                {
                    for (int level = e.Row.GroupLevel - 1; level >= 0; level--)
                    {
                        if (!TotalStockIni.ContainsKey(level))
                        {
                            TotalStockIni.Add(level, 0);
                            TotalIni.Add(level, 0);
                            COMPRA_Cantidad.Add(level, 0);
                            COMPRA_Total.Add(level, 0);
                            VENTA_Cantidad.Add(level, 0);
                            VENTA_Total.Add(level, 0);
                            TotalStockFin.Add(level, 0);
                            TotalFin.Add(level, 0);
                        }

                        TotalStockIni[level] += tempTotalStockIni;
                        TotalIni[level] += tempTotalIni;
                        COMPRA_Cantidad[level] += tempCOMPRA_Cantidad;
                        COMPRA_Total[level] += tempCOMPRA_Total;
                        VENTA_Cantidad[level] += tempVENTA_Cantidad;
                        VENTA_Total[level] += tempVENTA_Total;
                        TotalStockFin[level] += tempTotalStockFin;
                        TotalFin[level] += tempTotalFin;
                    }
                }

                int TotalStockIniToDisplay = 0;
                double TotalIniToDisplay = 0;
                int COMPRA_CantidadToDisplay = 0;
                double COMPRA_TotalToDisplay = 0;
                int VENTA_CantidadToDisplay = 0;
                double VENTA_TotalToDisplay = 0;
                int TotalStockFinToDisplay = 0;
                double TotalFinToDisplay = 0;

                if (TotalStockIni.ContainsKey(e.Row.GroupLevel))
                {
                    TotalStockIniToDisplay = TotalStockIni[e.Row.GroupLevel];
                    TotalIniToDisplay = TotalIni[e.Row.GroupLevel];
                    COMPRA_CantidadToDisplay = COMPRA_Cantidad[e.Row.GroupLevel];
                    COMPRA_TotalToDisplay = COMPRA_Total[e.Row.GroupLevel];
                    VENTA_CantidadToDisplay = VENTA_Cantidad[e.Row.GroupLevel];
                    VENTA_TotalToDisplay = VENTA_Total[e.Row.GroupLevel];
                    TotalStockFinToDisplay = TotalStockFin[e.Row.GroupLevel];
                    TotalFinToDisplay = TotalFin[e.Row.GroupLevel];

                    TotalStockIni[e.Row.GroupLevel] = 0;
                    TotalIni[e.Row.GroupLevel] = 0;
                    COMPRA_Cantidad[e.Row.GroupLevel] = 0;
                    COMPRA_Total[e.Row.GroupLevel] = 0;
                    VENTA_Cantidad[e.Row.GroupLevel] = 0;
                    VENTA_Total[e.Row.GroupLevel] = 0;
                    TotalStockFin[e.Row.GroupLevel] = 0;
                    TotalFin[e.Row.GroupLevel] = 0;
                }
                else
                {
                    TotalStockIniToDisplay = tempTotalStockIni;
                    TotalIniToDisplay = tempTotalIni;
                    COMPRA_CantidadToDisplay = tempCOMPRA_Cantidad;
                    COMPRA_TotalToDisplay = tempCOMPRA_Total;
                    VENTA_CantidadToDisplay = tempVENTA_Cantidad;
                    VENTA_TotalToDisplay = tempVENTA_Total;
                    TotalStockFinToDisplay = tempTotalStockFin;
                    TotalFinToDisplay = tempTotalFin;
                }

                // Display information in Group footer   
                e.Row.Cells[3].Text = TotalStockIniToDisplay.ToString();
                e.Row.Cells[4].Text = "S/." + TotalIniToDisplay.ToString();
                e.Row.Cells[5].Text = COMPRA_CantidadToDisplay.ToString();
                e.Row.Cells[6].Text = "S/." + COMPRA_TotalToDisplay.ToString();
                e.Row.Cells[7].Text = VENTA_CantidadToDisplay.ToString();
                e.Row.Cells[8].Text = "S/." + VENTA_TotalToDisplay.ToString();
                e.Row.Cells[9].Text = TotalStockFinToDisplay.ToString();
                e.Row.Cells[10].Text = "S/." + TotalFinToDisplay.ToString();

                tempTotalStockIni = 0;
                tempTotalIni = 0;
                tempCOMPRA_Cantidad = 0;
                tempCOMPRA_Total = 0;
                tempVENTA_Cantidad = 0;
                tempVENTA_Total = 0;
                tempTotalStockFin = 0;
                tempTotalFin = 0;
            }

        }



        
        //protected void gridVentasxCobrar_RowDataBound(object sender, GridRowEventArgs e)
        //{
        //    if (e.Row.RowType == GridRowType.DataRow)
        //    {
        //        tempTotalCOM += e.Row.Cells[10].Text == "" ? 0.0 : double.Parse(e.Row.Cells[10].Text);
        //        tempTotalVEN += e.Row.Cells[14].Text == "" ? 0.0 : double.Parse(e.Row.Cells[14].Text);
        //        tempTotal += e.Row.Cells[17].Text == "" ? 0.0 : double.Parse(e.Row.Cells[17].Text);

        //        if (lastGroupHeader != null)
        //        {
        //            Literal textContainer = lastGroupHeader.Cells[0].Controls[0].Controls[lastGroupHeader.Cells[0].Controls[0].Controls.Count - 1].Controls[0] as Literal;
        //            textContainer.Text = "<div style='margin-right:100px; float:left;' >Articulo: " + ((GridDataControlFieldCell)e.Row.Cells[1]).Text + " - " + ((GridDataControlFieldCell)e.Row.Cells[2]).Text;
        //            textContainer.Text += "</div><div style='float:left;'>";
        //            textContainer.Text += "Stock Inicial: " + ((GridDataControlFieldCell)e.Row.Cells[3]).Text + "</div>";

        //            lastGroupHeader = null;
        //        } 
        //    }
        //    else if (e.Row.RowType == GridRowType.GroupHeader)
        //    {
        //        //Literal textContainer = e.Row.Cells[0].Controls[0].Controls[1].Controls[0] as Literal;
        //        if (!lastGroupHeaders.ContainsKey(e.Row.GroupLevel))
        //        {
        //            lastGroupHeaders.Add(e.Row.GroupLevel, null);
        //        }
        //        lastGroupHeaders[e.Row.GroupLevel] = e.Row;

        //        if (e.Row.GroupLevel == 1)
        //        {
        //            lastGroupHeader = e.Row;
        //        }
        //    }
        //    else if (e.Row.RowType == GridRowType.GroupFooter)
        //    {
        //        if (e.Row.GroupLevel > 0)
        //        {
        //            for (int level = e.Row.GroupLevel - 1; level >= 0; level--)
        //            {
        //                if (!totalCOM.ContainsKey(level))
        //                {
        //                    totalCOM.Add(level, 0);
        //                    totalVEN.Add(level, 0);
        //                    total.Add(level, 0);
        //                }

        //                totalCOM[level] += tempTotalCOM;
        //                totalVEN[level] += tempTotalVEN;
        //                total[level] += tempTotal;
        //            }
        //        }

        //        double totalCOMToDisplay = 0;
        //        double totalVENToDisplay = 0;
        //        double totalToDisplay = 0;

        //        if (totalCOM.ContainsKey(e.Row.GroupLevel))
        //        {
        //            totalCOMToDisplay = totalCOM[e.Row.GroupLevel];
        //            totalVENToDisplay = totalVEN[e.Row.GroupLevel];
        //            totalToDisplay = total[e.Row.GroupLevel];

        //            totalCOM[e.Row.GroupLevel] = 0;
        //            totalVEN[e.Row.GroupLevel] = 0;
        //            total[e.Row.GroupLevel] = 0;
        //        }
        //        else
        //        {
        //            totalCOMToDisplay = tempTotalCOM;
        //            totalVENToDisplay = tempTotalVEN;
        //            totalToDisplay = tempTotal;
        //        }

        //        // Display information in Group footer            
        //        e.Row.Cells[10].Text = "S/." + totalCOMToDisplay.ToString();
        //        e.Row.Cells[14].Text = "S/." + totalVENToDisplay.ToString();
        //        //e.Row.Cells[17].Text = "S/." + totalToDisplay.ToString();

        //        tempTotalCOM = 0;
        //        tempTotalVEN = 0;
        //        tempTotal = 0;
        //    }
        //}

   

        
    }
}

