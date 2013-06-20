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

    public partial class wfrmKardex_ant : BasePage
    {
        double tempTotalCOM = 0;
        double tempTotalVEN = 0;
        double tempTotal = 0;

        Dictionary<int, double> totalCOM = new Dictionary<int, double>();
        Dictionary<int, double> totalVEN = new Dictionary<int, double>();
        Dictionary<int, double> total = new Dictionary<int, double>();
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

        public void RebindGrid(object sender, EventArgs e)
        {
            CreateGrid();
        }


        protected void gridVentasxCobrar_ColumnsCreated(object sender, EventArgs e)
        {
            Grid grid = sender as Grid;

            foreach (Column column in grid.Columns)
            {
                column.TemplateSettings.TemplateId = "Template1";
                column.TemplateSettings.HeaderTemplateId = "Template1";
            }
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

   

        
    }
}

