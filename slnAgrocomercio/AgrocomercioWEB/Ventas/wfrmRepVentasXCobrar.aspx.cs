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
        double tempTotalUSD = 0;
        double tempTotalPEN = 0;
        double tempTotal = 0;

        Dictionary<int, double> totalUSD = new Dictionary<int, double>();
        Dictionary<int, double> totalPEN = new Dictionary<int, double>();
        Dictionary<int, double> total = new Dictionary<int, double>();
        Dictionary<int, GridRow> lastGroupHeaders = new Dictionary<int, GridRow>();

        protected void Page_Load(object sender, EventArgs e)
        {
            String _selectedValue = "";

            if (Page.IsPostBack)
            {
                if (lblEstado.Value == "CLI_SELECT")
                {
                    _selectedValue = Request.Params["__EVENTARGUMENT"].ToString();
                    if (_selectedValue == "")
                        _selectedValue = "0";
                    ddlClientes.SelectedValue = _selectedValue;
                    ddlClientes_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                CargarClientes();
                CreateGrid();
            }
        }

        public void CreateGrid()
        {
            int nCliCod = 0;
            clsOperaciones colOperaciones = new clsOperaciones();
            
            if (ddlClientes.SelectedValue != "")
                nCliCod = int.Parse( ddlClientes.SelectedValue);

            DataTable dtResultado = colOperaciones.RepVentasXComprar(nCliCod);

            gridVentasxCobrar.DataSource = dtResultado;
            gridVentasxCobrar.DataBind();
        }

        public void CargarClientes()
        {
            clsClientes lstClientes = new clsClientes();

            ddlClientes.DataSource = lstClientes.GetAll();
            ddlClientes.DataBind();
            ddlClientes.Items.Insert(0, new ListItem("[TODOS]", "0"));

            lstClientes = null;
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


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string FileName = gridVentasxCobrar.ExportToExcel();
        //    Download(gridVentasxCobrar.FolderExports.Replace("~", "..") + FileName);

        //}

        //static public void Download(string patch)
        //{
        //    System.IO.FileInfo toDownload =
        //               new System.IO.FileInfo(HttpContext.Current.Server.MapPath(patch));

        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.AddHeader("Content-Disposition",
        //               "attachment; filename=" + toDownload.Name);
        //    HttpContext.Current.Response.AddHeader("Content-Length",
        //               toDownload.Length.ToString());
        //    HttpContext.Current.Response.ContentType = "application/octet-stream";
        //    HttpContext.Current.Response.WriteFile(patch);
        //    HttpContext.Current.Response.End();
        //}
        protected void gridVentasxCobrar_RowDataBound(object sender, GridRowEventArgs e)
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                tempTotalUSD += double.Parse(e.Row.Cells[10].Text);
                tempTotalPEN += double.Parse(e.Row.Cells[13].Text);
                tempTotal += double.Parse(e.Row.Cells[14].Text);
            }
            else if (e.Row.RowType == GridRowType.GroupHeader)
            {
                //Literal textContainer = e.Row.Cells[0].Controls[0].Controls[1].Controls[0] as Literal;
                if (!lastGroupHeaders.ContainsKey(e.Row.GroupLevel))
                {
                    lastGroupHeaders.Add(e.Row.GroupLevel, null);
                }
                lastGroupHeaders[e.Row.GroupLevel] = e.Row;
            }
            else if (e.Row.RowType == GridRowType.GroupFooter)
            {
                if (e.Row.GroupLevel > 0)
                {
                    for (int level = e.Row.GroupLevel - 1; level >= 0; level--)
                    {
                        if (!totalUSD.ContainsKey(level))
                        {
                            totalUSD.Add(level, 0);
                            totalPEN.Add(level, 0);
                            total.Add(level, 0);
                        }

                        totalUSD[level] += tempTotalUSD;
                        totalPEN[level] += tempTotalPEN;
                        total[level] += tempTotal;
                    }
                }

                double totalUSDToDisplay = 0;
                double totalPENToDisplay = 0;
                double totalToDisplay = 0;

                if (totalUSD.ContainsKey(e.Row.GroupLevel))
                {
                    totalUSDToDisplay = totalUSD[e.Row.GroupLevel];
                    totalPENToDisplay = totalPEN[e.Row.GroupLevel];
                    totalToDisplay = total[e.Row.GroupLevel];

                    totalUSD[e.Row.GroupLevel] = 0;
                    totalPEN[e.Row.GroupLevel] = 0;
                    total[e.Row.GroupLevel] = 0;
                }
                else
                {
                    totalUSDToDisplay = tempTotalUSD;
                    totalPENToDisplay = tempTotalPEN;
                    totalToDisplay = tempTotal;
                }

                // Display information in Group header
                Literal textContainer = lastGroupHeaders[e.Row.GroupLevel].Cells[0].Controls[0].Controls[lastGroupHeaders[e.Row.GroupLevel].Cells[0].Controls[0].Controls.Count - 1].Controls[0] as Literal;
                textContainer.Text = "<div class='group-total'>" + textContainer.Text + "</div>";
                textContainer.Text += "<div class='group-total' style='margin-left: " + (690 - (23 * e.Row.GroupLevel)).ToString() + "px;'>Totales: $" + totalUSDToDisplay.ToString() + "</div>";
                textContainer.Text += "<div class='group-total' style='margin-left: " + (975 - (23 * e.Row.GroupLevel)).ToString() + "px;'>S/." + totalPENToDisplay.ToString() + "</div>";
                textContainer.Text += "<div class='group-total' style='margin-left: " + (1055 - (23 * e.Row.GroupLevel)).ToString() + "px;'>S/." + totalToDisplay.ToString() + "</div>";


                // Display information in Group footer            
                e.Row.Cells[10].Text = "$" + totalUSDToDisplay.ToString();
                e.Row.Cells[13].Text = "S/." + totalPENToDisplay.ToString();
                e.Row.Cells[14].Text = "S/." + totalToDisplay.ToString();

                //((Obout.Grid.Grid)sender).ShowGroupFooter = false;

                tempTotalUSD = 0;
                tempTotalPEN = 0;
                tempTotal = 0;
            }
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateGrid();            
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
                    sClienteList.Insert(0,AjaxControlToolkit.AutoCompleteExtender.
                          CreateAutoCompleteItem("[TODOS]", "0"));

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

        
    }
}

