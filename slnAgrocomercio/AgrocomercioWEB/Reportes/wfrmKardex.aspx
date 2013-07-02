<%@ Page Title=".:Reportes de Kardex Fisico y Valorado:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmKardex.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmKardex" %>

<%--<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>--%>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        
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


        var gridBodyStyle = null;
        function printGrid() {
            gridBodyStyle = gridKardex.GridBodyContainer.getAttribute('style');
            gridKardex.GridBodyContainer.style.maxHeight = '';
            gridKardex.GridMainContainer.style.width = gridKardex.HorizontalScroller.firstChild.firstChild.offsetWidth + 'px';
            gridKardex.HorizontalScroller.style.display = 'none';

            gridKardex.print();

            window.setTimeout("gridKardex.GridBodyContainer.setAttribute('style', gridBodyStyle);", 250);
            return false;
        }
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
                            KARDEX FISICO Y VALORADO AGROCOMERCIO S.R.L.</h1>
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
                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Label">Reportes de Kardex Fisico y Valorado</asp:Label></h3>
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
                                                <td class="clsCellTituloDatos2">Proveedor: </td>
                                                <td >
                                                    <asp:ComboBox ID="cboxProveedores" runat="server" AutoPostBack="True" DropDownStyle="DropDownList" 
                                                    AutoCompleteMode="SuggestAppend" DataTextField="PrvRazon" 
                                                        DataValueField="PrvCod" Font-Size="9px" Width="150px" 
                                                        onselectedindexchanged="cboxProveedores_SelectedIndexChanged">
                                                    </asp:ComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="clsCellTituloDatos2">Articulos: </td>
                                                <td >
                                                    <asp:ComboBox ID="cboxArticulos" runat="server" AutoPostBack="True" DropDownStyle="DropDownList" 
                                                    AutoCompleteMode="SuggestAppend" DataTextField="ArtDescripcion" 
                                                        DataValueField="ArtCod" Font-Size="9px" Width="150px" 
                                                        onselectedindexchanged="cboxArticulos_SelectedIndexChanged">
                                                    </asp:ComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td class="tabDerecha3" valign="top" >
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
                                                        <input type="button" onclick="tableToExcel('MainContent_gridKardex', 'REPORTE KARDEX')" value="Excel"  class="clsBtnExcel" >
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
                                <td class="tablaDerecha" valign="top" colspan="2"  >
                                    <table class="tablaDerecha" >
                                        <tr>
                                            <td>
                                                <div id="divGridView" style="position: relative; width: 875px; height:430px; overflow:scroll;">
                                                <asp:GridView ID="gridKardex" runat="server" AutoGenerateColumns="False"  
                                                    CellPadding="4" GridLines="Vertical" Width="1300px" 
                                                            CssClass="mGrid mGrid3" ShowHeaderWhenEmpty="True" 
                                                        onrowcreated="gridKardex_RowCreated" onrowdatabound="gridKardex_RowDataBound" >
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" Visible="False" >
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:BoundField>
                                                            <asp:BoundField DataField="ArtCod" HeaderText="Art.Codigo" Visible="False" >
                                                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ArtDescripcion" HeaderText="Articulo" Visible="False"  >
                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ArtStockIni" HeaderText="Art.Stock.Inicial" Visible="False"    >
                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="OpeFecEmision" HeaderText="Fec.Emision"  DataFormatString="{0:d}" >
                                                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Documento" HeaderText="Nro Guia" >
                                                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Decripcion" HeaderText="Descripcion" >
                                                                <ItemStyle HorizontalAlign="Left" Width="220px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Cantidad" HeaderText="Comp.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Unidad" HeaderText="Comp.Unidad" >
                                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_PreUnitario" HeaderText="Comp.Precio.Unit" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Costo" HeaderText="Comp.Costo" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Cantidad" HeaderText="Venta.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Unidad" HeaderText="Venta.Unidad" >
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_PreUnitario" HeaderText="Venta.Precio.Unit" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Costo" HeaderText="Venta.Costo" DataFormatString="{0:n}">
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_Cantidad" HeaderText="Saldo.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_Unidad" HeaderText="Saldo.Unidad">
                                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_CostoTotal" HeaderText="Saldo.Costo Total" DataFormatString="{0:n}" >
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
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