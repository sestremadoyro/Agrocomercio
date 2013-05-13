<%@ Page Title=".:Reportes de Kardex Fisico y Valorado:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmTotalPorProveedor.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmTotalPorProveedor" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
    <script type="text/javascript">

        function exportToExcel() {
            gridTotales.exportToExcel();
            return false;
        }

        function exportToPdf() {
            gridTotales.exportToWord();
            return false;
        }

        function onChangePrv(sender, index) {
            gridTotales.addFilterCriteria('PrvRazon', OboutGridFilterCriteria.EqualTo, sender.options[index].text);
            gridTotales.executeFilter();
        }

        function onChangeArt(sender, index) {
            gridTotales.addFilterCriteria('ArtDescripcion', OboutGridFilterCriteria.EqualTo, sender.options[index].text);
            gridTotales.executeFilter();
        }

        function printGrid() {
            gridBodyStyle = gridTotales.GridBodyContainer.getAttribute('style');
            gridTotales.GridBodyContainer.style.maxHeight = '';
            gridTotales.print();

            window.setTimeout("gridKardex.GridBodyContainer.setAttribute('style', gridBodyStyle);", 250);
            return false;
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
                                                    <obout:ComboBox ID="cbProveedores" runat="server" Width="300" 
                                                    FilterType="StartsWith" EmptyText="Buscar Proveedores ..."
                                                    DataTextField="prvRazon" DataValueField="prvRazon" >
                                                        <ClientSideEvents OnSelectedIndexChanged="onChangePrv" />
                                                    </obout:ComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="clsCellTituloDatos2">Articulos: </td>
                                                <td >
                                                    <div class="comboBoxMaster">
                                                        <obout:ComboBox ID="cbArticulos" runat="server" Width="300" Height="150" 
                                                        FilterType="StartsWith" EmptyText="Buscar Articulos ..."
                                                        DataTextField="ArtDescripcion" DataValueField="ArtCod" 
                                                            style="top: 0px; left: 0px" >
                                                            <ClientSideEvents OnSelectedIndexChanged="onChangeArt" />
                                                        </obout:ComboBox>
                                                        <div class="comboBoxOver"></div>
                                                        <div class="comboBoxOver1"></div>
                                                        <div class="comboBoxOver2"></div>
                                                    </div>
                                                    
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
                                                        <asp:Button ID="Button3" runat="server" Text="Imprimir" ToolTip="Imprimir" CssClass="clsBtnImprimir"
                                                            OnClientClick="printGrid()" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="Button4" runat="server" Text="Excel" ToolTip="Excel" CssClass="clsBtnExcel"
                                                            OnClientClick="exportToExcel()" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="Button5" runat="server" Text="A Pdf" ToolTip="Pdf" CssClass="clsBtnPdf" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablaDerecha" valign="top" colspan="2">
                                    <table class="tablaDerecha" >
                                        <tr>
                                            <td>
                                                <div id="Div1" style="position: relative; width: 875px; height:430px; overflow:hidden;">

                                                    <obout:Grid ID="gridTotales" runat="server" CallbackMode="true" Serialize="true"
                                                        AutoGenerateColumns="false" PageSize="-1" AllowAddingRecords="false" ShowMultiPageGroupsInfo="false"
                                                        AllowColumnResizing="true"
                                                        ShowGroupFooter="true" OnRowDataBound="gridTotales_RowDataBound"
                                                        AllowGrouping="true" GroupBy="PrvRazon" AllowFiltering="True" 
                                                        Width="100%" HideColumnsWhenGrouping="True"
                                                        FolderLocalization="~/App_Themes/TemaAgrocomercio/Grid/localization" Language="es"
                                                        FolderStyle="~/App_Themes/TemaAgrocomercio/Grid/style_6" 
                                                        FilterType="ProgrammaticOnly" >
                                                        <Columns>
                                                            <obout:Column DataField="PrvRazon" HeaderText="Proveedor" Index="0" >
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtCod" HeaderText="Art.Codigo" Index="1" Width="50" Visible="false" >
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtDescripcion" HeaderText="Articulo" Index="2" AllowGroupBy="False" Width="150">
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtStockIni" HeaderText="Stock.Inicial" Index="3" AllowGroupBy="False" Width="80">
                                                            </obout:Column>
                                                            <obout:Column DataField="nTotalIni" HeaderText="Total.Inicial" Index="4" AllowGroupBy="False" Width="80">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_Cantidad" HeaderText="Compras.Cant" Index="5" AllowGroupBy="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_Total" HeaderText="Compras.Total" Index="6" AllowGroupBy="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_Cantidad" HeaderText="Venta.Cant" Index="7" AllowGroupBy="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_Total" HeaderText="Venta.Costo" Index="8" AllowGroupBy="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtStock" HeaderText="Stock.Final" Index="9" AllowGroupBy="False" Width="80">
                                                            </obout:Column>
                                                            <obout:Column DataField="nTotalFin" HeaderText="Total.Final" Index="10" AllowGroupBy="False" Width="80">
                                                            </obout:Column>
                                                        </Columns>
                                                        <GroupingSettings AllowChanges="False" />
                                                        <ScrollingSettings ScrollWidth="870" ScrollHeight="350" />
                                                        <ExportingSettings Encoding="UTF8" ExportAllPages="True" FileName="Total Proveedor"
                                                            KeepColumnSettings="True" ExportGroupFooter="true"  />
                                                        <CssSettings 
                                                             CSSExportHeaderCellStyle="font-weight: bold; color: #000000;  border:1px solid #222; background:#ddd;"
                                                             CSSExportCellStyle="font-weight: normal; color: #111111; border:1px solid #444;"/>
                                                        
                                                    </obout:Grid>
                                                    
                                                     
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