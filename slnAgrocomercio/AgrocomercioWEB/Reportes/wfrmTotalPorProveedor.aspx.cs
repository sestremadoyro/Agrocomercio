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
        // To keep track of the previous row Group Identifier
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total
        int nSubTotalStockIni = 0;
        double nSubTotal_Inicial = 0;
        int nSubTotalCompra_Cantidad = 0;
        double nSubTotalCompra_Total = 0;
        int nSubTotalVenta_Cantidad = 0;
        double nSubTotalVenta_Total = 0;
        int nSubTotalStockFin = 0;
        double nSubTotal_Fin = 0;

        int nTotalStockIni = 0;
        double nTotal_Inicial = 0;
        int nTotalCompra_Cantidad = 0;
        double nTotalCompra_Total = 0;
        int nTotalVenta_Cantidad = 0;
        double nTotalVenta_Total = 0;
        int nTotalStockFin = 0;
        double nTotal_Fin = 0;

        int LastRowIndex = 0;

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
            int ArtCod = 0;
            int PrvCod = 0;
            clsOperaciones colOperaciones = new clsOperaciones();

            if (cboxProveedores.SelectedIndex != -1)
                PrvCod = int.Parse(cboxProveedores.SelectedValue);

            if (cboxArticulos.SelectedIndex != -1)
                ArtCod = int.Parse(cboxArticulos.SelectedValue);

            IEnumerable<Object> dtResultado = colOperaciones.ReporteTotalProveedor(PrvCod, ArtCod);

            gridKardex.DataSource = dtResultado;
            gridKardex.DataBind();
        }

        public void CargarProveedor()
        {
            clsProveedores colProveedores = new clsProveedores();

            cboxProveedores.DataSource = colProveedores.GetProveedoresConArticulos();
            cboxProveedores.DataBind();
            cboxProveedores.Items.Insert(0, new ListItem("[TODOS]", "0"));
            colProveedores = null;
        }

        public void CargarArticulos()
        {
            int PrvCod = 0;
            clsArticulos colArticulos = new clsArticulos();

            if (cboxProveedores.SelectedIndex != -1)
                PrvCod = int.Parse(cboxProveedores.SelectedValue);

            if (PrvCod == 0)
                cboxArticulos.DataSource = colArticulos.GetAll();
            else
                cboxArticulos.DataSource = colArticulos.GetArticulosByPrvCod(PrvCod);
            cboxArticulos.DataBind();
            cboxArticulos.Items.Insert(0, new ListItem("[TODOS]", "0"));
            colArticulos = null;
        }

        protected void gridKardex_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString();

                int nStockIni = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ArtStockIni"));
                double nTotIni = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "nTotalIni").ToString());
                int nComCan = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "nCom_Cantidad"));
                double nComTot = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "nCom_Total").ToString());
                int nVenCan = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "nVen_Cantidad"));
                double nVenTot = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "nVen_Total").ToString());
                int ntmpArtStock = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ArtStock"));
                double nTotFin = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "nTotalFin").ToString());

                //Acumulating Sub Total Proveedor
                nSubTotalStockIni += nStockIni;
                nSubTotal_Inicial += nTotIni;
                nSubTotalCompra_Cantidad += nComCan;
                nSubTotalCompra_Total += nComTot ;
                nSubTotalVenta_Cantidad += nVenCan ;
                nSubTotalVenta_Total += nVenTot ;
                nSubTotalStockFin += ntmpArtStock;
                nSubTotal_Fin += nTotFin;

                nTotalStockIni += nStockIni;
                nTotal_Inicial += nTotIni;
                nTotalCompra_Cantidad += nComCan;
                nTotalCompra_Total += nComTot;
                nTotalVenta_Cantidad += nVenCan;
                nTotalVenta_Total += nVenTot;
                nTotalStockFin += ntmpArtStock;
                nTotal_Fin += nTotFin;
            }
        }

        protected void gridKardex_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (gridKardex.DataSource == null) {
                return;
            }

            LastRowIndex = e.Row.RowIndex >= 0 ? e.Row.RowIndex : LastRowIndex;
            bool IsSubTotalPrvRowNeedToAdd = false;
            bool IsTotalRowNeedtoAdd = false;
            int RowIndex = e.Row.RowIndex;            
            
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString())
                    IsSubTotalPrvRowNeedToAdd = true;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") == null))
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
                cell.ColumnSpan = 4;
                cell.CssClass = "GridTitlesEmpty";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "COMPRAS";
                cell.ColumnSpan = 2;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "VENTAS";
                cell.ColumnSpan = 2;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "SALDOS";
                cell.ColumnSpan = 2;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                grdViewOrders.Controls[0].Controls.AddAt(0, row);
                intSubTotalIndex++;
            }

            //Agregar primera agrupacion por proveedor
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") != null))
            {
                GridView grdViewOrders = (GridView)sender;

                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "Proveedor: " + DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString();
                cell.ColumnSpan = 10;
                cell.CssClass = "GroupHeader1Style";
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
                cell.Text = "Sub Total Proveedor : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                
                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalStockIni);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotal_Inicial);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalCompra_Cantidad);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalCompra_Total);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalVenta_Cantidad);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalVenta_Total);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotalStockFin);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nSubTotal_Fin);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion

                #region Agregar La siguiente Cabecera de Proveedor
                if (DataBinder.Eval(e.Row.DataItem, "PrvRazon") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                    cell = new TableCell();
                    cell.Text = "Proveedor: " + DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString();
                    cell.ColumnSpan = 10;
                    cell.CssClass = "GroupHeader1Style";
                    row.Cells.Add(cell);

                    grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

                #region Resetear Variables de Totales
                nSubTotalStockIni = 0;
                nSubTotal_Inicial = 0;
                nSubTotalCompra_Cantidad = 0;
                nSubTotalCompra_Total = 0;
                nSubTotalVenta_Cantidad = 0;
                nSubTotalVenta_Total = 0;
                nSubTotalStockFin = 0;
                nSubTotal_Fin = 0;

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
                cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalStockIni);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotal_Inicial);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalCompra_Cantidad);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalCompra_Total);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalVenta_Cantidad);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalVenta_Total);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotalStockFin);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nTotal_Fin);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }

        protected void cboxProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArticulos();
            gridKardex.DataSource = null;
            gridKardex.DataBind();
            CreateGrid();
        }

        protected void cboxArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridKardex.DataSource = null;
            gridKardex.DataBind();
            CreateGrid();
        }
        
    }
}

