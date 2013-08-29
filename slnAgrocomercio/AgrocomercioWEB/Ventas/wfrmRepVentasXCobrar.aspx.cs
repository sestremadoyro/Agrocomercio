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

    public partial class wfrmRepVentasXCobrar : BasePage
    {
        // To keep track of the previous row Group Identifier
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total
        double nSubTotalUSD = 0;
        double nSubTotalPEN = 0;
        double nSubTotal = 0;

        double nTotalUSD = 0;
        double nTotalPEN = 0;
        double nTotal = 0;
        
        int LastRowIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarClientes();
                CreateGrid();
            }
        }

        public void CreateGrid()
        {
            int nCliCod = 0;
            clsOperaciones colOperaciones = new clsOperaciones();

            if (cboxCliente.SelectedIndex != -1)
                nCliCod = int.Parse(cboxCliente.SelectedValue);

            DataTable dtResultado = colOperaciones.RepVentasXComprar(nCliCod);

            gridVentasxCobrar.DataSource = dtResultado;
            gridVentasxCobrar.DataBind();
        }

        public void CargarClientes()
        {
            clsClientes lstClientes = new clsClientes();

            cboxCliente.DataSource = lstClientes.GetAll();
            cboxCliente.DataBind();
            cboxCliente.Items.Insert(0, new ListItem("[TODOS]", "0"));

            lstClientes = null;
        }

        protected void gridVentasxCobrar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CliNombre").ToString();

                double nTotUSD = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Saldo_USD").ToString());
                double nTotPEN = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Saldo_PEN").ToString());
                double nTotal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "nSaldoTotal").ToString());
                
                //Acumulating Sub Total Cliente
                nSubTotalUSD += nTotUSD;
                nSubTotalPEN += nTotPEN;
                nSubTotal += nTotal;

                nTotalUSD += nTotUSD;
                nTotalPEN += nTotPEN;
                nTotal += nTotal;
            }
        }

        protected void gridVentasxCobrar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (gridVentasxCobrar.DataSource == null)
            {
                return;
            }

            LastRowIndex = e.Row.RowIndex >= 0 ? e.Row.RowIndex : LastRowIndex;
            bool IsSubTotalPrvRowNeedToAdd = false;
            bool IsTotalRowNeedtoAdd = false;
            int RowIndex = e.Row.RowIndex;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CliNombre") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CliNombre").ToString())
                    IsSubTotalPrvRowNeedToAdd = true;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CliNombre") == null))
            {
                IsSubTotalPrvRowNeedToAdd = true;
                IsTotalRowNeedtoAdd = true;
                RowIndex = LastRowIndex + 1;
            }

            //Agregar primera fila 
            if (intSubTotalIndex <= 1)
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "";
                cell.ColumnSpan = 8;
                cell.CssClass = "GridTitlesEmpty";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "SOLES";
                cell.ColumnSpan = 3;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "DOLARES";
                cell.ColumnSpan = 3;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "TOTAL";
                cell.ColumnSpan = 1;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                grdViewOrders.Controls[0].Controls.AddAt(0, row);
                intSubTotalIndex++;
            }

            //Agregar primera agrupacion por proveedor
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CliNombre") != null))
            {
                GridView grdViewOrders = (GridView)sender;

                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "Cliente: " + DataBinder.Eval(e.Row.DataItem, "CliNombre").ToString();
                cell.ColumnSpan = 5;
                cell.CssClass = "GroupHeader1Style";
                row.Cells.Add(cell);

                //Adding Total Cell 
                cell = new TableCell();
                cell.Text = "Direccion: " + DataBinder.Eval(e.Row.DataItem, "CliDireccion").ToString();
                cell.ColumnSpan = 10;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }

            //AGREGAMOS SUBTOTAL POR PROVEEDOR
            if (IsSubTotalPrvRowNeedToAdd)
            {
                #region Agregar Row de Sub Totales por Proveedor
                GridView grdViewOrders = (GridView)sender;

                // Creating a Row
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell 
                TableCell cell = new TableCell();
                cell.Text = "Sub Total Cliente : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 8;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);


                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalPEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalUSD);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotal);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion

                #region Agregar La siguiente Cabecera de Proveedor
                if (DataBinder.Eval(e.Row.DataItem, "CliNombre") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                    cell = new TableCell();
                    cell.Text = "Cliente: " + DataBinder.Eval(e.Row.DataItem, "CliNombre").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeader1Style";
                    row.Cells.Add(cell);

                    //Adding Total Cell 
                    cell = new TableCell();
                    cell.Text = "Direccion: " + DataBinder.Eval(e.Row.DataItem, "CliDireccion").ToString();
                    cell.ColumnSpan = 10;
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.CssClass = "SubTotalRow1Style";
                    row.Cells.Add(cell);

                    grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

                #region Resetear Variables de Totales
                nSubTotalPEN = 0;
                nSubTotalUSD = 0;
                nSubTotal = 0;
                #endregion
            }

            if (IsTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView grdViewOrders = (GridView)sender;

                // Creating a Row
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell 
                TableCell cell = new TableCell();
                cell.Text = "Total : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 8;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalPEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalUSD);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotal);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }

        protected void cboxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridVentasxCobrar.DataSource = null;
            gridVentasxCobrar.DataBind();
            CreateGrid();
        }
        
    }
}

