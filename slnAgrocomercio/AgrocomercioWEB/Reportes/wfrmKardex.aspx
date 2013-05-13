<%@ Page Title=".:Reportes de Kardex Fisico y Valorado:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmKardex.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmKardex" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div#ob_ctl00$MainContent$cbArticulosTrialDiv
        {
            top:-9999999;
            z-index:-100;
            font-weight:bold;
            margin:200px;
            cursor:crosshair;
            display:none;
            direction:none;
        }
        
    </style>
    <script type="text/javascript">

        document.getElementById("ob_ctl00$MainContent$cbArticulosTrialDiv").style.color = "blue";

        function exportToExcel() {
            gridKardex.exportToExcel();
            return false;
        }

        function exportToPdf() {
            gridKardex.exportToWord();
            return false;
        }

        function onChangePrv(sender, index) {
            gridKardex.addFilterCriteria('PrvRazon', OboutGridFilterCriteria.EqualTo, sender.options[index].text);
            gridKardex.executeFilter();
        }

        function onChangeArt(sender, index) {
            gridKardex.addFilterCriteria('ArtDescripcion', OboutGridFilterCriteria.EqualTo, sender.options[index].text);
            gridKardex.executeFilter();
        }


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
                                                        DataTextField="ArtDescripcion" DataValueField="ArtCod" >
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
                                <td class="tablaDerecha" valign="top" colspan="2"  >
                                    <table class="tablaDerecha"  >
                                        <tr>
                                            <td>
                                                <div id="Div1" style="position: relative; width: 875px; height:430px; overflow:hidden;">


                                                    <obout:Grid ID="gridKardex" runat="server" CallbackMode="true" Serialize="true"
                                                        AutoGenerateColumns="false" PageSize="-1" AllowAddingRecords="false" ShowMultiPageGroupsInfo="false"
                                                        AllowColumnResizing="true"
                                                        ShowGroupFooter="true"  OnRowDataBound="gridVentasxCobrar_RowDataBound"
                                                        AllowGrouping="true" GroupBy="PrvRazon,ArtCod" AllowFiltering="True" 
                                                        Width="100%" HideColumnsWhenGrouping="True"
                                                        FolderLocalization="~/App_Themes/TemaAgrocomercio/Grid/localization" Language="es"
                                                        FolderStyle="~/App_Themes/TemaAgrocomercio/Grid/style_6" 
                                                        FilterType="ProgrammaticOnly" >
                                                        <Columns>
                                                            <obout:Column DataField="PrvRazon" HeaderText="Proveedor" Index="0" Width="150">
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtCod" HeaderText="Art.Codigo" Index="1" AllowFilter="False" Visible="false" >
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtDescripcion" HeaderText="Articulo" Index="2" AllowGroupBy="False" Visible="false" 
                                                                Width="150">
                                                            </obout:Column>
                                                            <obout:Column DataField="ArtStockIni" HeaderText="Art.Stock.Inicial" Index="3" AllowGroupBy="False"
                                                                AllowFilter="False" Visible="false" >
                                                            </obout:Column>
                                                            <obout:Column DataField="OpeFecEmision" HeaderText="Fec.Emision" Index="4" DataFormatString="{0:d/M/yyyy}"
                                                                AllowGroupBy="False" Width="90">
                                                                <FilterOptions>
                                                                    <obout:CustomFilterOption IsDefault="true" ID="Between_OpeFecEmision" Text="Entre">
                                                                        <TemplateSettings FilterTemplateId="OpeFecEmisionBetweenFilter" FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" />
                                                                        <TemplateSettings FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" FilterTemplateId="OpeFecEmisionBetweenFilter" />
                                                                    </obout:CustomFilterOption>
                                                                </FilterOptions>
                                                            </obout:Column>
                                                            <obout:Column DataField="Documento" HeaderText="Nro Documento" Index="5" AllowGroupBy="False"
                                                                AllowFilter="False" Width="100">
                                                            </obout:Column>
                                                            <obout:Column DataField="Decripcion" HeaderText="Descripcion" Index="6" AllowGroupBy="False"
                                                                AllowFilter="False" Width="180">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_Cantidad" HeaderText="Comp.Cantidad" Index="7" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_Unidad" HeaderText="Comp.Unidad" Index="8" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_PreUnitario" HeaderText="Comp.Precio.Unit" Index="9" AllowGroupBy="False"
                                                                AllowFilter="False" Width="105">
                                                            </obout:Column>
                                                            <obout:Column DataField="nCom_Costo" HeaderText="Comp.Costo" Index="10" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_Cantidad" HeaderText="Venta.Cantidad" Index="11" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_Unidad" HeaderText="Venta.Unidad" Index="12" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_PreUnitario" HeaderText="Venta.Precio.Unit" Index="13" AllowGroupBy="False"
                                                                AllowFilter="False" Width="105">
                                                            </obout:Column>
                                                            <obout:Column DataField="nVen_Costo" HeaderText="Venta.Costo" Index="14" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nSal_Cantidad" HeaderText="Saldo.Cantidad" Index="15" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </obout:Column>
                                                            <obout:Column DataField="nSal_Unidad" HeaderText="Saldo.Unidad" Index="16" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </obout:Column>
                                                            <obout:Column DataField="nSal_CostoTotal" HeaderText="Saldo.Costo Total" Index="17" AllowGroupBy="False"
                                                                AllowFilter="False" Width="110">
                                                            </obout:Column>
                                                        </Columns>
                                                        <GroupingSettings AllowChanges="False" />
                                                        <ScrollingSettings ScrollWidth="870" ScrollHeight="350" />
                                                        <ExportingSettings Encoding="UTF8" ExportAllPages="True" FileName="Kardex"
                                                            KeepColumnSettings="True" ExportGroupFooter="true"  />
                                                        <CssSettings 
                                                             CSSExportHeaderCellStyle="font-weight: bold; color: #000000;  border:1px solid #222; background:#ddd;"
                                                             CSSExportCellStyle="font-weight: normal; color: #111111; border:1px solid #444;"/>
                                                        <Templates>
                                                            <obout:GridTemplate runat="server" ID="OpeFecEmisionBetweenFilter">
                                                                <Template>
                                                                    <div style="width: 99%; padding: 0px; margin: 0px; font-size: 5px;">
                                                                        <table width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                                                                            <tr>
                                                                                <td valign="middle">
                                                                                    <input readonly="readonly" type="text" id="StartDate_OpeFecEmision" style="width: 45px;
                                                                                        font-size: 9px" />
                                                                                </td>
                                                                                <td valign="middle" width="30">
                                                                                    <obout:Calendar ID="cal1" runat="server" StyleFolder="..\App_Themes\TemaAgrocomercio\calendar\styles\default"
                                                                                        DatePickerMode="true" DateFormat="d/M/yyyy" YearSelectorType="HtmlList" TitleText="<span style='color:crimson'>Eliga el Año:</span> "
                                                                                        DatePickerImagePath="..\App_Themes\TemaAgrocomercio\calendar\styles\icon2.gif"
                                                                                        TextBoxId="StartDate_OpeFecEmision">
                                                                                    </obout:Calendar>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="separator">
                                                                                        -</div>
                                                                                </td>
                                                                                <td valign="middle">
                                                                                    <input readonly="readonly" type="text" id="EndDate_OpeFecEmision" style="width: 45px;
                                                                                        font-size: 9px" />
                                                                                </td>
                                                                                <td valign="middle" width="30">
                                                                                    <obout:Calendar ID="cal2" runat="server" StyleFolder="..\App_Themes\TemaAgrocomercio\calendar\styles\default"
                                                                                        DatePickerMode="true" DateFormat="d/M/yyyy" YearSelectorType="HtmlList" TitleText="<span style='color:crimson'>Eliga el Año:</span> "
                                                                                        DatePickerImagePath="..\App_Themes\TemaAgrocomercio\calendar\styles\icon2.gif"
                                                                                        TextBoxId="EndDate_OpeFecEmision">
                                                                                    </obout:Calendar>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </Template>
                                                            </obout:GridTemplate>
                                                            <obout:GridTemplate runat="server" ID="ZonasFilter" ControlID="ddlZonas" ControlPropertyName="value">
                                                                <Template>
                                                                    <obout:OboutDropDownList runat="server" ID="ddlZonas" Width="100%" MenuWidth="100"
                                                                        FolderStyle="~/App_Themes/TemaAgrocomercio/Grid/style_6/interface/OboutDropDownList" DataSourceID="odsZonas"
                                                                        DataTextField="AtrDescripcion" DataValueField="AtrCodigo" />
                                                                </Template>
                                                            </obout:GridTemplate>
                                                            <obout:GridTemplate runat="server" ID="VendedorFilter" ControlID="ddlVendedor" ControlPropertyName="value">
                                                                <Template>
                                                                    <obout:OboutDropDownList runat="server" ID="ddlVendedor" Width="100%" MenuWidth="100"
                                                                        FolderStyle="~/App_Themes/TemaAgrocomercio/Grid/style_6/interface/OboutDropDownList" DataSourceID="odsVendedor"
                                                                        DataTextField="PerNombres" DataValueField="perCod" />
                                                                </Template>
                                                            </obout:GridTemplate>
                                                        </Templates>
                                                    </obout:Grid>
                                                    
                                                     <asp:ObjectDataSource ID="odsZonas" runat="server" SelectMethod="ListDataAtributos"
                                                        TypeName="pryAgrocomercioBLL.EntityCollection.clsAtributos">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="4" Name="pcAtrTipoCod" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="odsVendedor" runat="server" SelectMethod="GetDataPersonalPorTipo"
                                                        TypeName="pryAgrocomercioBLL.EntityCollection.clsPersonal">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="2" Name="pnTpecod" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>


                                                    
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