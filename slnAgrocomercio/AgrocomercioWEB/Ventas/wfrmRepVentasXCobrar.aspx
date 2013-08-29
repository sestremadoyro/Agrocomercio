<%@ Page Title=".:Reportes de Resumen de Clientes:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmRepVentasXCobrar.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmRepVentasXCobrar" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ob_gRGF
        {
            display: none !important;
        }
        
        .group-total
        {
            float: right;
            width: 150px;
            position: absolute;
            top: 0px;
            overflow: hidden;
            white-space: nowrap;
        }
    </style>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()

        function printGrid2() {
            var div, imp;
            div = document.getElementById("divGridView"); //seleccionamos el objeto
            imp = window.open(" ", "Impresion de Reporte", 'toolbar=0,scrollbars=0,status=0'); //damos un titulo
            imp.document.write('<HTML>\n<HEAD>\n<link href="gridview.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<BODY>\n'); //tambien podriamos agregarle un <link ...
            imp.document.write(div.innerHTML + '\n</BODY>\n</HTML>'); //agregamos el objeto
            imp.document.close();
            imp.print();   //Abrimos la opcion de imprimir
            imp.close(); //cerramos la ventana nueva

        }

    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="MainUpdatePanel"
        DynamicLayout="true">
        <ProgressTemplate>
            <div class="cls_ProgresShadow">
                &nbsp;
            </div>
            <div class="cls_ProgresIcon">
                <div style="padding: 10px; position: relative; top: 33%; left: 45%; height: 100px;
                    width: 150px;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/ajax-loader.gif"
                        AlternateText="Procesando ..." ToolTip="Trabajando ..." Style="height: 80px;
                        width: 80px;" />
                    <span style="padding: 10px; font-size: 13px; font-weight: bold; color: #555555;">Procesando...
                    </span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table class="MainContent">
                <tr>
                    <td colspan="2">
                        <h1 class="clsTituloPrincipal2">
                            REPORTES DE VENTAS DE AGROCOMERCIO S.R.L.</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="tabPedidos">
                            <tr>
                                <td class="tabIzquierda3" valign="top">
                                    <asp:Panel ID="pnListOperCompras" runat="server">
                                        <table class="tableIzquierda">
                                            <tr>
                                                <td colspan="4">
                                                    <h3>
                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Label">Reportes de Ventas Por Cobrar</asp:Label></h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="clsCellTituloDatos2">
                                                    <asp:Label ID="Label7" runat="server" Text="Tipo de Cambio:"></asp:Label>
                                                </td>
                                                <td class="clsCellDatos2">
                                                    <asp:TextBox ID="txtTipCam" runat="server" ReadOnly="True" Text="2.56"></asp:TextBox>
                                                    <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />   
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td class="clsCellTituloDatos2">Cliente: </td>
                                                <td >
                                                    <asp:ComboBox ID="cboxCliente" runat="server" AutoPostBack="True" DropDownStyle="DropDownList" 
                                                    AutoCompleteMode="SuggestAppend" DataTextField="CliNombre" 
                                                        DataValueField="CliCod" Font-Size="9px" Width="150px" 
                                                        onselectedindexchanged="cboxCliente_SelectedIndexChanged" >
                                                    </asp:ComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td class="tabDerecha3" valign="top">
                                    <div class="divDerecha">
                                        <asp:Panel runat="server" ID="pnBotones" CssClass="clspntablaDerecha">
                                            <table class="tablaDerecha">
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <h3>
                                                            Opciones</h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td valign="top">
                                                        <input id="Button1" type="button" value="Imprimir" onclick="printGrid2()" class="clsBtnImprimir"   />
                                                    </td>
                                                    <td valign="top">
                                                        <input type="button" onclick="tableToExcel('MainContent_gridVentasxCobrar', 'REPORTE VENTAS X COBRAR')" value="Excel"  class="clsBtnExcel" >
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="Button5" runat="server" Text="A Pdf" ToolTip="Pdf" CssClass="clsBtnPdf" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablaDerecha" valign="top" colspan="2">
                                    <table class="tablaDerecha">
                                        <tr>
                                            <td>
                                                <div id="divGridView" style="position: relative; width: 1080px; height:430px; overflow:scroll;">
                                                <asp:GridView ID="gridVentasxCobrar" runat="server" AutoGenerateColumns="False"  
                                                    CellPadding="4" GridLines="Vertical" Width="1060px" 
                                                    CssClass="mGrid mGrid3" ShowHeaderWhenEmpty="True" 
                                                        onrowcreated="gridVentasxCobrar_RowCreated" 
                                                        onrowdatabound="gridVentasxCobrar_RowDataBound" >
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <Columns>
                                                            <asp:BoundField DataField="OpeCod" HeaderText="Cod" >
                                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CliNombre" HeaderText="Nombre" Visible="false" >
                                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="CliDireccion" HeaderText="Direccion" Visible="false" >
                                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                            </asp:BoundField>    
                                                            <asp:BoundField DataField="OpeFecEmision" HeaderText="Fec.Emision"  DataFormatString="{0:d}"  >
                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:BoundField>                                                                                                                       
                                                            <asp:BoundField DataField="CliCreAsig" HeaderText="Credito Asign" >
                                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NroNota" HeaderText="Nro Pedido" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NroFactura" HeaderText="Nro Fatura" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Zona" HeaderText="Zona"   >
                                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Vendedor" HeaderText="Vendedor" >
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="dFecPago" HeaderText="Fec.Pago" DataFormatString="{0:d}" >
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Monto_USD" HeaderText="Monto" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACta_USD" HeaderText="A Cta"  DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_USD" HeaderText="Saldo" DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Monto_PEN" HeaderText="Monto" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ACta_PEN" HeaderText="A Cta"  DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Saldo_PEN" HeaderText="Saldo" DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="nSaldoTotal" HeaderText="Saldo Total" DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registrado ninguna operacion</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="pgr" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>
                                                     
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>