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
        window.onload = function () {
            oboutGrid.prototype.resizeColumnOld = oboutGrid.prototype.resizeColumn;
            oboutGrid.prototype.resizeColumn = function (param1, param2, param3) {
                this.resizeColumnOld(param1, param2, param3);

                updateGroupTotalsWidths();
            }
        }

        function updateGroupTotalsWidths() {
            var elements = document.getElementsByTagName('DIV');
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].className == 'ob_gRGHC') {

                    var marginIndent = 0;
                    var tempElement = elements[i];
                    while (tempElement && tempElement.previousSibling) {
                        marginIndent -= tempElement.previousSibling.offsetWidth;
                        tempElement = tempElement.previousSibling;
                    }

                    var indent = -3;

                    for (var j = 0; j < 4; j++) {
                        elements[i].childNodes[j].style.marginLeft = (j > 0 ? indent : 0) + 'px';
                        elements[i].childNodes[j].style.width = (gridVentasxCobrar.ColumnsCollection[j + 2].Width + (j == 0 ? marginIndent : -20)) + 'px';
                        indent += gridVentasxCobrar.ColumnsCollection[j + 2].Width + (j == 0 ? marginIndent + 20 : 0);
                    }
                }
            }
        }


        function exportToExcel() {
            gridVentasxCobrar.exportToExcel();
            return false;
        }

        function exportToPdf() {
            gridVentasxCobrar.exportToWord();
            return false;
        }


        var gridBodyStyle = null;
        function printGrid() {
            gridBodyStyle = gridVentasxCobrar.GridBodyContainer.getAttribute('style');
            gridVentasxCobrar.GridBodyContainer.style.maxHeight = '';
            gridVentasxCobrar.GridMainContainer.style.width = gridVentasxCobrar.HorizontalScroller.firstChild.firstChild.offsetWidth + 'px';
            gridVentasxCobrar.HorizontalScroller.style.display = 'none';

            gridVentasxCobrar.print();

            window.setTimeout("gridVentasxCobrar.GridBodyContainer.setAttribute('style', gridBodyStyle);", 250);
            return false;
        }

        function setCliCod(source, eventargs) {
            document.getElementById('lblEstado').value = "CLI_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
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
                                                <td class="clsCellTituloDatos2" > </td>
                                                <td class="clsCellDatos2" colspan="3">
                                                    <asp:DropDownList ID="ddlClientes" runat="server" Width="250px" 
                                                        AutoPostBack="True" DataTextField="CliNombre" DataValueField="CliCod" 
                                                         style=" display:none;" 
                                                        onselectedindexchanged="ddlClientes_SelectedIndexChanged" >
                                                                </asp:DropDownList>
                                                     <asp:TextBox ID="txtCliente" runat="server" Width="250px" Visible="False" ></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender 
	                                                    ID="txtCliente_TextBoxWatermarkExtender" runat="server"
	                                                    TargetControlID="txtCliente" WatermarkCssClass = "clsWaterMark" 
	                                                    WatermarkText="Busqueda de Clientes..." Enabled="true">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <div id="ClilistPlacement" class="cls_listPlacement" style=" overflow:auto; display:none; "></div>
                                                    <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender"
	                                                    MinimumPrefixLength="2" TargetControlID="txtCliente" 
                                                        enablecaching="true" 
                                                        showonlycurrentwordincompletionlistitem="true" 
	                                                    CompletionSetCount="10" CompletionInterval="100" 
	                                                    ServiceMethod="BuscarClientes" 
	                                                    runat="server" OnClientItemSelected="setCliCod" 
	                                                    CompletionListElementID="ClilistPlacement">
                                                    </asp:AutoCompleteExtender>
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
                                    <table class="tablaDerecha">
                                        <tr>
                                            <td>
                                                <div id="Div1" style="position: relative; width: 100%;">


                                                    <obout:Grid ID="gridVentasxCobrar" runat="server" CallbackMode="true" Serialize="true"
                                                        AutoGenerateColumns="false" PageSize="-1" AllowAddingRecords="false" ShowMultiPageGroupsInfo="false"
                                                        AllowColumnResizing="true" ShowColumnsFooter="false" 
                                                        ShowGroupFooter="true" OnRowDataBound="gridVentasxCobrar_RowDataBound"
                                                        AllowGrouping="true" GroupBy="CliNombre" AllowFiltering="True" 
                                                        Width="100%" HideColumnsWhenGrouping="True"
                                                        FolderLocalization="~/App_Themes/TemaAgrocomercio/Grid/localization" Language="es"
                                                        FolderStyle="~/App_Themes/TemaAgrocomercio/Grid/style_6" 
                                                        onrebind="RebindGrid">
                                                        <ClientSideEvents OnClientCallback="updateGroupTotalsWidths" />
                                                        <Columns>
                                                            <obout:Column ID="Column18" DataField="OpeCod" HeaderText="Cod" Width="40" runat="server"  AllowFilter="false" />
                                                            <obout:Column ID="Column19" DataField="OpeFecEmision" HeaderText="Fec.Emision" DataFormatString="{0:d/M/yyyy}"
                                                                Width="100" runat="server" ShowFilterCriterias="false">
                                                                <FilterOptions>
                                                                    <obout:CustomFilterOption IsDefault="true" ID="Between_OpeFecEmision" Text="Entre">
                                                                        <TemplateSettings FilterTemplateId="OpeFecEmisionBetweenFilter" FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" />
                                                                        <TemplateSettings FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" FilterTemplateId="OpeFecEmisionBetweenFilter" />
                                                                    </obout:CustomFilterOption>
                                                                </FilterOptions>
                                                            </obout:Column>
                                                            <obout:Column ID="Column20" DataField="NroNota" HeaderText="Nro Pedido" Width="90"
                                                                runat="server" />
                                                            <obout:Column ID="Column21" DataField="NroFactura" HeaderText="Nro Fatura" Width="90"
                                                                runat="server" />
                                                            <obout:Column ID="Column22" DataField="CliNombre" HeaderText="Cliente" Width="150"
                                                                runat="server" />
                                                            <obout:Column ID="Column23" DataField="Zona" HeaderText="Zona" Width="80" runat="server" ShowFilterCriterias="false">
                                                                <TemplateSettings FilterTemplateId="ZonasFilter" />
                                                            </obout:Column>
                                                            <obout:Column ID="Column24" DataField="Vendedor" HeaderText="Vendedor" Width="100"
                                                                runat="server" ShowFilterCriterias="false">
                                                                <TemplateSettings FilterTemplateId="VendedorFilter" />
                                                            </obout:Column>
                                                            <obout:Column ID="Column25" DataField="dFecPago" HeaderText="Fec.Pago" DataFormatString="{0:d/M/yyyy}"
                                                                Width="80" runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column26" DataField="Monto_USD" HeaderText="Monto USD" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column27" DataField="ACta_USD" HeaderText="A Cta USD" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column28" DataField="Saldo_USD" HeaderText="Saldo USD" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column29" DataField="Monto_PEN" HeaderText="Monto PEN" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column30" DataField="ACta_PEN" HeaderText="A Cta PEN" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column31" DataField="Saldo_PEN" HeaderText="Saldo PEN" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column32" DataField="nSaldoTotal" HeaderText="Saldo Total" Width="100"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column33" DataField="CliCreAsig" HeaderText="Credito Asign" Width="80"
                                                                runat="server"  AllowFilter="false"/>
                                                            <obout:Column ID="Column34" DataField="CliDireccion" HeaderText="Direccion" Width="200"
                                                                runat="server"  AllowFilter="false"/>
                                                        </Columns>
                                                        <GroupingSettings AllowChanges="False" />
                                                        <ScrollingSettings ScrollWidth="870" ScrollHeight="350" />
                                                        <ExportingSettings Encoding="UTF8" ExportAllPages="True" FileName="VentasXCobrar"
                                                            KeepColumnSettings="True" />
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
                                                            <asp:Parameter DefaultValue="1" Name="pnTpecod" />
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