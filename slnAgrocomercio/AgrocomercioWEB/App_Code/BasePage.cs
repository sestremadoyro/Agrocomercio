using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Text;
using System.Xml;
using System.Threading;
using System.Globalization;
using pryAgrocomercioBLL.EntityCollection;

namespace AgrocomercioWEB
{
    public class BasePage : System.Web.UI.Page
    {

        public int nNroDetPed = 15;
        public Double nTasIGV;

        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Esperar(int nTiempo)
        {
            Thread.Sleep(nTiempo);
        }


        protected int ExtraeQueryStringEntero(string var)
        {

            if (Request.QueryString[var] == null)
                return -1;
            else
                return Convert.ToInt32(Request.QueryString[var]);
        }
        protected string ExtraeQueryStringCadena(string var)
        {
            if (Request.QueryString[var] == null)
                return null;
            else
                return (string)Request.QueryString[var];
        }
        public void AgregarVariableCache(string variableCache, Object variable)
        {
            //'guardamos la informacion en las variables cache
            HttpContext contexto = HttpContext.Current;
            if (contexto.Cache[variableCache] == null)
                contexto.Cache.Insert(variableCache, variable, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero);
            else
            {
                contexto.Cache.Remove(variableCache);
                contexto.Cache.Insert(variableCache, variable, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero);
            }
        }
        public Object LeerVariableCache(string variableCache)
        {
            HttpContext contexto = HttpContext.Current;
            return (Object)contexto.Cache[variableCache];
        }
        public void EliminarVariableCache(string variableCache)
        {
            HttpContext contexto = HttpContext.Current;
            contexto.Cache.Remove(variableCache);
        }
        public void AgregarVariableSession(string variableSesion, Object variable)
        {
            //'guardamos la informacion en las variables cache
            HttpContext contexto = HttpContext.Current;
            if (contexto.Session[variableSesion] == null)
                contexto.Session.Add(variableSesion, variable);
            else
            {
                contexto.Session.Remove(variableSesion);
                contexto.Session.Add(variableSesion, variable);
            }
        }
        public void EliminarVariableSesion(string variableSesion)
        {
            HttpContext contexto = HttpContext.Current;
            contexto.Session.Remove(variableSesion);
        }
        public Object LeerVariableSesion(string variableSesion)
        {
            HttpContext contexto = HttpContext.Current;
            return (Object)contexto.Session[variableSesion];
        }

        public void MessageBox(string mensaje)
        {
            String scriptMsj = "";
            scriptMsj = "alert('" + mensaje + "')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MENSAJE", scriptMsj, true);
        }
        public string GetMes(int nMes)
        {
            string mes = "";
            switch (nMes)
            {
                case (1):
                    mes = "ENERO";
                    break;
                case (2):
                    mes = "FEBRERO";
                    break;
                case (3):
                    mes = "MARZO";
                    break;
                case (4):
                    mes = "ABRIL";
                    break;
                case (5):
                    mes = "MAYO";
                    break;
                case (6):
                    mes = "JUNIO";
                    break;
                case (7):
                    mes = "JULIO";
                    break;
                case (8):
                    mes = "AGOSTO";
                    break;
                case (9):
                    mes = "SETIEMBRE";
                    break;
                case (10):
                    mes = "OCTUBRE";
                    break;
                case (11):
                    mes = "NOVIMBRE";
                    break;
                case (12):
                    mes = "DICIEMBRE";
                    break;
            }
            return mes;
        }
        public void HabilitarBtn(Button btnControl, Boolean bValue)
        {
            btnControl.Enabled = bValue;
            if (bValue)
                btnControl.CssClass = "clsBtn" + btnControl.ToolTip.Replace(" ", "_");
            else
                btnControl.CssClass = "clsBtn" + btnControl.ToolTip.Replace(" ", "_") + "Disabled";
        }

        private string[] _ones =
        {
        "cero",
        "un",
        "dos",
        "tres",
        "cuatro",
        "cinco",
        "seis",
        "siete",
        "ocho",
        "nueve"
        };

        private string[] _teens =
        {
        "diez",
        "once",
        "doce",
        "trece",
        "catorce",
        "quince",
        "dieciseis",
        "diecisiete",
        "dieciocho",
        "diecinueve"
        };

        private string[] _tens =
        {
        "",
        "diez",
        "veinte",
        "treinta",
        "cuarenta",
        "cincuenta",
        "sesenta",
        "setenta",
        "ochenta",
        "noventa"
        };

        private string[] _centenas =
        {
        "",
        "ciento",
        "doscientos",
        "trescientos",
        "cuatrocientos",
        "quinientos",
        "seiscientos",
        "setecientos",
        "ochocientos",
        "novecientos"
        };

        private string[] _thousands =
        {
        "",
        "mil",
        "millon",
        "billon",
        "trillon",
        "cuatrillon"
        };

        /// <summary>
        /// Converts a numeric value to words suitable for the portion of
        /// a check that writes out the amount.
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <returns></returns>
        public string ConvertiraLetras(decimal value)
        {
            string digits, temp;
            bool showThousands = false;
            bool allZeros = true;

            // Use StringBuilder to build result
            StringBuilder builder = new StringBuilder();
            // Convert integer portion of value to string
            digits = ((long)value).ToString();
            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (int)(digits[i] - '0');
                int column = (digits.Length - (i + 1));

                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = String.Format("{0} ", _ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = String.Format("{0} ", _teens[ndigit]);
                            // Skip tens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            // Any non-zero digit
                            //if (column == 0)
                            //{
                            temp = String.Format("{0} ", _ones[ndigit]);
                            //}
                            //else
                            //{
                            //    temp = String.Format("{0} ", ndigit == 1 ? "un" : _ones[ndigit]);
                            //}
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            temp = String.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                            {
                                temp = String.Format("{0}{1}{2}",
                                temp,
                                _thousands[column / 3],
                                allZeros ? " " : ", ");
                            }
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:        // Tens column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0}{1}",
                            _tens[ndigit],
                            (digits[i + 1] != '0') ? " y " : " ");
                            builder.Insert(0, temp);
                        }
                        break;

                    case 2:        // Hundreds column
                        if (ndigit > 0)
                        {
                            temp = String.Format("{0} ", _centenas[ndigit]);
                            builder.Insert(0, temp);
                        }
                        break;
                }
            }

            // Append fractional portion/cents
            builder.AppendFormat("con {0:00}/100", (value - (long)value) * 100);

            // Capitalize first letter
            return String.Format("{0}{1}",
            Char.ToUpper(builder[0]),
            builder.ToString(1, builder.Length - 1));
        }
        public string SetFormatNum(double pnMonto)
        {
            string cMonto = "";
            double nTipCam = g_nTipoCambio;
            string cCulture = "";
            if (nTipCam == 1)
                cCulture = "es-PE";
            else
                cCulture = "en-US";
            nTipCam = nTipCam == 0 ? 1 : nTipCam;
            cMonto = (pnMonto / nTipCam).ToString("C", CultureInfo.CreateSpecificCulture(cCulture));

            return cMonto;
        }
        public double GetNumero(string pcMonto)
        {
            double nMonto = 0.0;
            pcMonto = pcMonto.Replace("S/.", "").Trim();
            pcMonto = pcMonto.Replace("$", "").Trim();
            pcMonto = pcMonto.Replace(".", "");
            pcMonto = pcMonto.Replace(",", "");
            nMonto = double.Parse(pcMonto) / 100;
            nMonto = Math.Round(nMonto * g_nTipoCambio, 2);

            return nMonto;
        }
        public void AddCLickToGridView(ref GridView dgvGrilla)
        {
            string _cError = "";
            string GridName = "";
            GridName = dgvGrilla.ID.ToString();

            try
            {
                foreach (GridViewRow row in dgvGrilla.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell c in row.Cells)
                        {
                            if (c != row.Cells[0] && c != row.Cells[1])
                            {
                                c.Attributes["onclick"] = GridName + "ClickEvent(\"SIMPLECLICK\",\"" + row.RowIndex + "\");";
                                c.Attributes["ondblclick"] = GridName + "ClickEvent(\"DOUBLECLICK\",\"" + row.RowIndex + "\");";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _cError = ex.Message;
            }
        }

        protected void CalcularTotales(DataTable dtDetalleOpe, string cOpeTipo, double pnDescuentoEsp, double pnTasIGV, double pnFlete,
            ref double pcPrecioCompra, ref double pcDescuento, ref double pcSubTotal,
            ref double pcIgv, ref double pcCostoTotal)
        {
            double nPrecio = 0.0;
            double nPrecioFin = 0.0;
            double nIGV = 0.0;
            double nSubTotal = 0.0;
            double nCostoTotal = 0.0;

            int i = 0;
            string TipCalIGV = "";

            if (dtDetalleOpe.Rows.Count > 0)
            {
                for (i = 0; i < dtDetalleOpe.Rows.Count; i++)
                {
                    nPrecio += Double.Parse(dtDetalleOpe.Rows[i]["dtpSubTotal"].ToString());
                }
            }
            else
                nIGV = 0.0;

            TipCalIGV = ConfigurationManager.AppSettings["TipoCalculoIGV"];

            if (cOpeTipo == "C") //PARA COMPRAS
            {
                if (TipCalIGV == "2")
                {
                    nSubTotal = nPrecio - (nPrecio * 100 / 118);
                    nPrecio = nSubTotal - (nSubTotal / 101);
                }
                nPrecioFin = nPrecio - pnDescuentoEsp;
                nSubTotal = Math.Round(nPrecioFin + pnFlete, 2);
                nIGV = Math.Round(nSubTotal * pnTasIGV, 2);
                nCostoTotal = Math.Round(nSubTotal + nIGV, 2);
            }
            else // PARA VENTAS
            {
                if (TipCalIGV == "1")
                {
                    var nIgv = nPrecio - Math.Round((nPrecio * 100 / 118), 2);
                    nSubTotal = nPrecio - nIgv;
                }
                else
                    nSubTotal = nCostoTotal;

                nPrecioFin = nSubTotal - pnDescuentoEsp;
                nSubTotal = Math.Round(nPrecioFin + pnFlete, 2);
                nIGV = Math.Round(nSubTotal * nTasIGV, 2);
                nCostoTotal = Math.Round(nSubTotal + nIGV, 2);
            }

            pcPrecioCompra = nPrecioFin;
            pcDescuento = pnDescuentoEsp;
            pcSubTotal = nSubTotal;
            pcIgv = nIGV;
            pcCostoTotal = nCostoTotal;
        }


        protected void RellenarGrilla(ref GridView dgvGrilla, DataTable dtDatos, int _nNroReg)
        {
            DataRow newRow;
            DataTable dtResult = new DataTable();

            foreach (DataColumn Col in dtDatos.Columns)
                dtResult.Columns.Add(new DataColumn(Col.ColumnName, Col.DataType));

            if (_nNroReg > 0)
            {
                for (int i = 0; i < _nNroReg; i++)
                {
                    newRow = dtResult.NewRow();
                    for (int j = 0; j < dtDatos.Columns.Count; j++)
                    {
                        if (i < dtDatos.Rows.Count)
                            newRow[j] = dtDatos.Rows[i][j];
                        else
                            newRow[j] = DBNull.Value;
                    }
                    dtResult.Rows.Add(newRow);
                }
            }

            dgvGrilla.DataSource = dtResult;
            dgvGrilla.DataBind();
        }


        protected DataTable CreatDTDetOperacion()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("ArtCod", typeof(int)));
            dtResult.Columns.Add(new DataColumn("ArtPeso", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("LotNro", typeof(int)));
            dtResult.Columns.Add(new DataColumn("ArtDescripcion", typeof(String)));
            dtResult.Columns.Add(new DataColumn("UniAbrev", typeof(String)));
            dtResult.Columns.Add(new DataColumn("dtpCantidad", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("dtpPrecioVen", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("dtpDscto", typeof(Double)));
            dtResult.Columns.Add(new DataColumn("dtpSubTotal", typeof(Double)));

            return dtResult;
        }
        public DataTable g_dtDetOperacion
        {
            get
            {
                if (LeerVariableSesion("dtDetOperacion") == null)
                    return CreatDTDetOperacion();
                else if (((DataTable)LeerVariableSesion("dtDetOperacion")).Rows.Count == 0)
                    return CreatDTDetOperacion();
                else
                    return (DataTable)LeerVariableSesion("dtDetOperacion");
            }
            set { AgregarVariableSession("dtDetOperacion", (DataTable)value); }
        }
        public Double g_nTipoCambio
        {
            get
            {
                if (LeerVariableSesion("nTipoCambio") == null)
                    return 0.0;
                else
                    return (Double)LeerVariableSesion("nTipoCambio");
            }
            set { AgregarVariableSession("nTipoCambio", (Double)value); }
        }

        public void CargarEstados(ref DropDownList ddlLista)
        {
            clsAtributos Atributos = new clsAtributos();

            //Cargamos combo de Tipo de Documento
            ddlLista.DataSource = Atributos.ListAtributos(6);
            ddlLista.DataBind();
        }


    }

}