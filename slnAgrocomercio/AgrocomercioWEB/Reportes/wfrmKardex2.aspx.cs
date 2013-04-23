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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;

namespace AgrocomercioWEB.Reportes
{

    public partial class wfrmKardex2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
               
            }
            SetEstado("INI");
        }

#region FUNCIONES DEL FORMULARIO

     
        
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataTable dtResultado = null;
            clsOperaciones colOperaciones = new clsOperaciones();
            DateTime dFecIni = DateTime.Today;
            DateTime dFecFin = DateTime.Today;
            try
            {
                dtResultado = colOperaciones.ReporteKardex();

                if (dtResultado.Rows.Count > 0)
                {
                    SetEstado("PRO");

                    gridVentasxCobrar.DataSource = dtResultado;
                    gridVentasxCobrar.DataBind();
                    gridVentasxCobrar.Width = 100;

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
                    HabilitarBtn(btnExcel, false);
                    HabilitarBtn(btnPdf, false);
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
                    HabilitarBtn(btnExcel, true);
                    HabilitarBtn(btnPdf, true);
                    lblExito.Visible = true;
                    lblError.Visible = false;
                    break;
                case ("ERR"):
                    HabilitarBtn(btnProcesar, true);
                    HabilitarBtn(btnImprimir, false);
                    HabilitarBtn(btnExcel, false);
                    HabilitarBtn(btnPdf, false);
                    lblExito.Visible = false;
                    lblError.Visible = true;
                    break;
            }

            
        }
        
     

#endregion

      

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
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string FileName = gridVentasxCobrar.ExportToExcel();
            Download(gridVentasxCobrar.FolderExports.Replace("~", "..") + FileName);
            SetEstado("PRO");
        }


        protected void btnPdf_Click(object sender, EventArgs e)
        {
            gridVentasxCobrar.PageSize = -1;
            gridVentasxCobrar.DataBind();
            // Stream which will be used to render the data
            MemoryStream fileStream = new MemoryStream();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            try
            {
                //Create Document class object and set its size to letter and give space left, right, Top, Bottom Margin
                PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);

                doc.Open();//Open Document to write

                Font font8 = FontFactory.GetFont("ARIAL", 7);

                //Write some content
                Paragraph paragraph = new Paragraph("ASP.NET Grid - Export to PDF");

                //Craete instance of the pdf table and set the number of column in that table
                PdfPTable PdfTable = new PdfPTable(gridVentasxCobrar.Columns.Count);
                PdfPCell PdfPCell = null;

                //Add headers of the pdf table
                foreach (Column col in gridVentasxCobrar.Columns)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfTable.AddCell(PdfPCell);
                }

                //How add the data from the Grid to pdf table
                for (int i = 0; i < gridVentasxCobrar.Rows.Count; i++)
                {
                    Hashtable dataItem = gridVentasxCobrar.Rows[i].ToHashtable();

                    foreach (Column col in gridVentasxCobrar.Columns)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField].ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);
                    }
                }

                PdfTable.SpacingBefore = 15f;

                doc.Add(paragraph);
                doc.Add(PdfTable);
            }
            catch (DocumentException docEx)
            {
                //handle pdf document exception if any
            }
            catch (IOException ioEx)
            {
                // handle IO exception
            }
            catch (Exception ex)
            {
                // ahndle other exception if occurs
            }
            finally
            {
                //Close document and writer
                doc.Close();
            }

            // Send the data and the appropriate headers to the browser
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=oboutGrid.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            Response.End();
            SetEstado("PRO");
        }
       

        
    }
}

