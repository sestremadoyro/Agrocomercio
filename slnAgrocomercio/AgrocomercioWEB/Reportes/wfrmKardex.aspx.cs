using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
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
using System.Web.UI.WebControls;



namespace AgrocomercioWEB.Reportes
{

    public partial class wfrmKardex : BasePage
    {
        //string GroupBy = "PrvRazon,ArtCod";
        //string GroupHeaderFormat = "";
        //string GroupFooterFormat = "";

        // To keep track of the previous row Group Identifier
        string strPreviousRowPrvID = string.Empty;
        string strPreviousRowArtID = string.Empty;
        // To keep track the Index of Group Total
        int intSubTotalIndex = 1;

        // To temporarily store Sub Total
        double dblSubTotalPrvCOM = 0;
        double dblSubTotalPrvVEN = 0;
        double dblSubTotalCantPrvCOM = 0;
        double dblSubTotalCantPrvVEN = 0;
        double dblSubTotalCantPrv = 0;
        double dblSubTotalPrv = 0;

        // To temporarily store Sub Total
        double dblSubTotalArtCOM = 0;
        double dblSubTotalArtVEN = 0;        
        double dblSubTotalCantArtCOM = 0;
        double dblSubTotalCantArtVEN = 0;
        double dblSubTotalArt = 0;

        // To temporarily store Grand Total
        double dblTotalCOM = 0;
        double dblTotalVEN = 0;        
        double dblTotalCantCOM = 0;
        double dblTotalCantVEN = 0;
        double dblTotalCant = 0;
        double dblTotal = 0;

        double nStockIni = 0;
        double nCostoPromedio = 0;

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
            
            if(cboxProveedores.SelectedIndex !=-1)
                PrvCod = int.Parse( cboxProveedores.SelectedValue);

            if (cboxArticulos.SelectedIndex != -1)
                ArtCod = int.Parse(cboxArticulos.SelectedValue);

            DataTable dtResultado = colOperaciones.ReporteKardex(PrvCod, ArtCod);

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
                strPreviousRowPrvID = DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString();
                strPreviousRowArtID = DataBinder.Eval(e.Row.DataItem, "ArtCod").ToString();

                string clCOM = DataBinder.Eval(e.Row.DataItem, "nCom_Costo").ToString();
                string clVEN = DataBinder.Eval(e.Row.DataItem, "nVen_Costo").ToString();
                string clTOTAL = DataBinder.Eval(e.Row.DataItem, "nSal_CostoTotal").ToString();
                string clCantCOM = DataBinder.Eval(e.Row.DataItem, "nCom_Cantidad").ToString();
                string clCantVEN = DataBinder.Eval(e.Row.DataItem, "nVen_Cantidad").ToString();

                double dblCOM = clCOM == "" ? 0 : Convert.ToDouble(clCOM);
                double dblVEN = clVEN == "" ? 0 : Convert.ToDouble(clVEN);
                double dblTOTAL = clTOTAL == "" ? 0 : Convert.ToDouble(clTOTAL);
                double dblCantCOM = clCantCOM == "" ? 0 : Convert.ToDouble(clCantCOM);
                double dblCantVEN = clCantVEN == "" ? 0 : Convert.ToDouble(clCantVEN);

                //Acumulating Sub Total Proveedor
                dblSubTotalPrvCOM += dblCOM;
                dblSubTotalPrvVEN += dblVEN;
                dblSubTotalCantPrvCOM += dblCantCOM;
                dblSubTotalCantPrvVEN += dblCantVEN;

                //Acumulating Sub Total Articulos
                dblSubTotalArtCOM += dblCOM;
                dblSubTotalArtVEN += dblVEN;
                dblSubTotalArt += dblTOTAL;
                dblSubTotalCantArtCOM += dblCantCOM;
                dblSubTotalCantArtVEN += dblCantVEN;

                //Acumulating Grand Total
                dblTotalCOM += dblCOM;
                dblTotalVEN += dblVEN;
                //dblTotal += dblTOTAL;
                dblTotalCantCOM += dblCantCOM;
                dblTotalCantVEN += dblCantVEN;
            }
        }

        protected void gridKardex_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (gridKardex.DataSource == null)
            {
                return;
            }

            LastRowIndex = e.Row.RowIndex >= 0 ? e.Row.RowIndex : LastRowIndex;
            bool IsSubTotalPrvRowNeedToAdd = false;
            bool IsSubTotalArtRowNeedToAdd = false;
            bool IsTotalRowNeedtoAdd = false;
            int RowIndex = e.Row.RowIndex;            

            if ((strPreviousRowPrvID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") != null))
                if (strPreviousRowPrvID != DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString())
                    IsSubTotalPrvRowNeedToAdd = true;

            if ((strPreviousRowArtID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ArtCod") != null))
                if (strPreviousRowArtID != DataBinder.Eval(e.Row.DataItem, "ArtCod").ToString())
                    IsSubTotalArtRowNeedToAdd = true;

            if ((strPreviousRowArtID != string.Empty) && (strPreviousRowPrvID != string.Empty) &&
                (DataBinder.Eval(e.Row.DataItem, "ArtCod") == null) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") == null))
            {
                IsSubTotalPrvRowNeedToAdd = true;
                IsSubTotalArtRowNeedToAdd = true;
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
                cell.ColumnSpan = 3;
                cell.CssClass = "GridTitles";
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "VENTAS";
                cell.ColumnSpan = 3;
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
            if ((strPreviousRowPrvID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PrvRazon") != null))
            {
                GridView grdViewOrders = (GridView)sender;

                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "Proveedor: " + DataBinder.Eval(e.Row.DataItem, "PrvRazon").ToString();
                cell.ColumnSpan = 12;
                cell.CssClass = "GroupHeader1Style";
                row.Cells.Add(cell);

                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            
            //Agregar la primera fila de articulos
            if ((strPreviousRowArtID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ArtCod") != null))
            {
                GridView grdViewOrders = (GridView)sender;

                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                TableCell cell = new TableCell();
                cell.Text = "Articulo: " + DataBinder.Eval(e.Row.DataItem, "ArtCod").ToString() + " - " + DataBinder.Eval(e.Row.DataItem, "ArtDescripcion").ToString();
                cell.ColumnSpan = 3;
                cell.CssClass = "GroupHeader2Style";
                row.Cells.Add(cell);

                TableCell cell2 = new TableCell();
                cell2.Text = "Stock Inicial: " + DataBinder.Eval(e.Row.DataItem, "ArtStockIni").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "ArtStockIni") != null)
                    nStockIni = double.Parse(DataBinder.Eval(e.Row.DataItem, "ArtStockIni").ToString());
                if (DataBinder.Eval(e.Row.DataItem, "ArtCostoProm") != null)
                    nCostoPromedio = double.Parse(DataBinder.Eval(e.Row.DataItem, "ArtCostoProm").ToString());
                cell2.ColumnSpan = 9;
                cell2.CssClass = "GroupHeader2Style";
                row.Cells.Add(cell2);

                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }

            //AGREGAMOS SUBTOTAL POR ARTICULO
            if (IsSubTotalArtRowNeedToAdd)
            {
                #region Agregar Fila de Sub Totales por Articulo
                GridView grdViewOrders = (GridView)sender;

                // Creating a Row
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell 
                TableCell cell = new TableCell();
                cell.Text = "Sub Total Articulo : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 4;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);

                //Adding Sub Total Cantidad Compra
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCantArtCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);

                //Adding Sub Total Compra
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalArtCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);

                //Adding Sub Total Cantidad venta
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCantArtVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);

                //Adding Sub Total Venta
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalArtVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);

                //Adding Sub Total Cantidad venta
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", nStockIni + dblSubTotalCantArtCOM - dblSubTotalCantArtVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);
                dblSubTotalCantPrv += (nStockIni + dblSubTotalCantArtCOM - dblSubTotalCantArtVEN); 

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", (nStockIni + dblSubTotalCantArtCOM - dblSubTotalCantArtVEN)*nCostoPromedio);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow2Style";
                row.Cells.Add(cell);
                dblSubTotalPrv += ((nStockIni + dblSubTotalCantArtCOM - dblSubTotalCantArtVEN) * nCostoPromedio);

                //Adding the Row at the RowIndex position in the Grid
                grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion

                #region Resetear Variables de Totales
                dblSubTotalArtCOM = 0;
                dblSubTotalArtVEN = 0;
                dblSubTotalArt = 0;
                dblSubTotalCantArtCOM = 0;
                dblSubTotalCantArtVEN = 0;
                nStockIni = 0;
                nCostoPromedio = 0;
                #endregion
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
                cell.ColumnSpan = 4;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCantPrvCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalPrvCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCantPrvVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalPrvVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCantPrv);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);
                dblTotalCant += dblSubTotalCantPrv;
                

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalPrv);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "SubTotalRow1Style";
                row.Cells.Add(cell);
                dblTotal += dblSubTotalPrv;

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
                    cell.ColumnSpan = 12;
                    cell.CssClass = "GroupHeader1Style";
                    row.Cells.Add(cell);

                    grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

                #region Resetear Variables de Totales
                dblSubTotalPrvCOM = 0;
                dblSubTotalPrvVEN = 0;
                dblSubTotalCantPrvCOM = 0;
                dblSubTotalCantPrvVEN = 0;
                dblSubTotalCantPrv = 0;
                dblSubTotalPrv = 0;                

                dblSubTotalArtCOM = 0;
                dblSubTotalArtVEN = 0;
                dblSubTotalArt = 0;
                dblSubTotalCantArtCOM = 0;
                dblSubTotalCantArtVEN = 0;
                nStockIni = 0;
                nCostoPromedio = 0;
                #endregion
            }

            //AGREGAMOS SIGUIENTE CABECERA ARTICULO
            if (IsSubTotalArtRowNeedToAdd)
            {
                #region Agregar Siguiente Cabecera de Articulo
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                GridView grdViewOrders = (GridView)sender;
                if (DataBinder.Eval(e.Row.DataItem, "ArtCod") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                    TableCell cell = new TableCell();
                    cell.Text = "Articulo: " + DataBinder.Eval(e.Row.DataItem, "ArtCod").ToString() + " - " + DataBinder.Eval(e.Row.DataItem, "ArtDescripcion").ToString();
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeader2Style";
                    row.Cells.Add(cell);

                    TableCell cell2 = new TableCell();
                    cell2.Text = "Stock Inicial: " + DataBinder.Eval(e.Row.DataItem, "ArtStockIni").ToString();
                    if (DataBinder.Eval(e.Row.DataItem, "ArtStockIni") != null)
                        nStockIni = double.Parse(DataBinder.Eval(e.Row.DataItem, "ArtStockIni").ToString());
                    if (DataBinder.Eval(e.Row.DataItem, "ArtCostoProm") != null)
                        nCostoPromedio = double.Parse(DataBinder.Eval(e.Row.DataItem, "ArtCostoProm").ToString());
                    cell2.ColumnSpan = 9;
                    cell2.CssClass = "GroupHeader2Style";
                    row.Cells.Add(cell2);

                    grdViewOrders.Controls[0].Controls.AddAt(RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
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
                cell.ColumnSpan = 4;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotalCantCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotalCOM);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotalCantVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotalVEN);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotalCant);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Discount Column
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblTotal);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 1;
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

