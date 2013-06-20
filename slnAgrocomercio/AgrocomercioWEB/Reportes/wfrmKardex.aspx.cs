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

    public partial class wfrmKardex : BasePage
    {
        double tempTotalCOM = 0;
        double tempTotalVEN = 0;
        double tempTotal = 0;

        Dictionary<int, double> totalCOM = new Dictionary<int, double>();
        Dictionary<int, double> totalVEN = new Dictionary<int, double>();
        Dictionary<int, double> total = new Dictionary<int, double>();
        Dictionary<int, GridRow> lastGroupHeaders = new Dictionary<int, GridRow>();
        GridRow lastGroupHeader;

        // To keep track of the previous row Group Identifier
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total
        double dblSubTotalCOM = 0;
        double dblSubTotalVEN = 0;
        double dblSubTotal = 0;

        // To temporarily store Grand Total
        double dblGrandTotalCOM = 0;
        double dblGrandTotalVEN = 0;
        double dblGrandTotal = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProveedor();
                CargarArticulos();
            }
        }

        public void CreateGrid()
        {
            clsOperaciones colOperaciones = new clsOperaciones();
            
            DataTable dtResultado = colOperaciones.ReporteKardex();

            gridKardex.DataSource = dtResultado;
            gridKardex.DataBind();
        }

        public void CargarProveedor()
        {
            clsProveedores colProveedores = new clsProveedores();

            cbProveedores.DataSource = colProveedores.GetProveedoresConArticulos();
            cbProveedores.DataBind();
            //ddlClientes.Items.Insert(0, new ListItem("[TODOS]", "0"));

            colProveedores = null;
        }

        public void CargarArticulos()
        {
            clsArticulos colArticulos = new clsArticulos();

            cbArticulos.DataSource = colArticulos.GetAll();
            cbArticulos.DataBind();
            //ddlClientes.Items.Insert(0, new ListItem("[TODOS]", "0"));

            colArticulos = null;
        }



        protected void gridVentasxCobrar_RowDataBound(object sender, GridRowEventArgs e)
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                tempTotalCOM += e.Row.Cells[10].Text == "" ? 0.0 : double.Parse(e.Row.Cells[10].Text);
                tempTotalVEN += e.Row.Cells[14].Text == "" ? 0.0 : double.Parse(e.Row.Cells[14].Text);
                tempTotal += e.Row.Cells[17].Text == "" ? 0.0 : double.Parse(e.Row.Cells[17].Text);

                if (lastGroupHeader != null)
                {
                    Literal textContainer = lastGroupHeader.Cells[0].Controls[0].Controls[lastGroupHeader.Cells[0].Controls[0].Controls.Count - 1].Controls[0] as Literal;
                    textContainer.Text = "<div style='margin-right:100px; float:left;' >Articulo: " + ((GridDataControlFieldCell)e.Row.Cells[1]).Text + " - " + ((GridDataControlFieldCell)e.Row.Cells[2]).Text;
                    textContainer.Text += "</div><div style='float:left;'>";
                    textContainer.Text += "Stock Inicial: " + ((GridDataControlFieldCell)e.Row.Cells[3]).Text + "</div>";

                    lastGroupHeader = null;
                } 
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
                        if (!totalCOM.ContainsKey(level))
                        {
                            totalCOM.Add(level, 0);
                            totalVEN.Add(level, 0);
                            total.Add(level, 0);
                        }

                        totalCOM[level] += tempTotalCOM;
                        totalVEN[level] += tempTotalVEN;
                        total[level] += tempTotal;
                    }
                }

                double totalCOMToDisplay = 0;
                double totalVENToDisplay = 0;
                double totalToDisplay = 0;

                if (totalCOM.ContainsKey(e.Row.GroupLevel))
                {
                    totalCOMToDisplay = totalCOM[e.Row.GroupLevel];
                    totalVENToDisplay = totalVEN[e.Row.GroupLevel];
                    totalToDisplay = total[e.Row.GroupLevel];

                    totalCOM[e.Row.GroupLevel] = 0;
                    totalVEN[e.Row.GroupLevel] = 0;
                    total[e.Row.GroupLevel] = 0;
                }
                else
                {
                    totalCOMToDisplay = tempTotalCOM;
                    totalVENToDisplay = tempTotalVEN;
                    totalToDisplay = tempTotal;
                }

                // Display information in Group footer            
                e.Row.Cells[10].Text = "S/." + totalCOMToDisplay.ToString();
                e.Row.Cells[14].Text = "S/." + totalVENToDisplay.ToString();
                //e.Row.Cells[17].Text = "S/." + totalToDisplay.ToString();

                tempTotalCOM = 0;
                tempTotalVEN = 0;
                tempTotal = 0;
            }
        }

        protected void gridKardex_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridKardex_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CustomerID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CustomerID").ToString())
                    IsSubTotalRowNeedToAdd = true;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CustomerID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }

            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CustomerID") != null))
            {
                GridView grdViewOrders = (GridView)sender;

                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "Customer Name : " + DataBinder.Eval(e.Row.DataItem, "CompanyName").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion

            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView grdViewOrders = (GridView)sender;

                // Creating a Row
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell 
                TableCell cell = new TableCell();
                cell.Text = "Sub Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmount);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion

                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "CustomerID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                    cell = new TableCell();
                    cell.Text = "Customer Name : " + DataBinder.Eval(e.Row.DataItem, "CompanyName").ToString();
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

                #region Reseting the Sub Total Variables
                dblSubTotalUnitPrice = 0;
                dblSubTotalQuantity = 0;
                dblSubTotalDiscount = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView grdViewOrders = (GridView)sender;

                // Creating a Row
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell 
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalAmount);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }

   

        
    }
}

