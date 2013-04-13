<%@ Page Title=".:Reportes de Compras:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmRepVentasXCobrar.aspx.cs" Inherits="AgrocomercioWEB.Ventas.wfrmRepVentasXCobrar" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc4" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        
        function exportToExcel() {
            gridVentasxCobrar.exportToExcel();
            return false;
        }

        function exportToPdf() {
            gridVentasxCobrar.exportToWord();
            return false;
        }

        function printGrid() {
            gridBodyStyle = gridVentasxCobrar.GridBodyContainer.getAttribute('style');
            gridVentasxCobrar.GridBodyContainer.style.maxHeight = '';

            gridVentasxCobrar.print();

            window.setTimeout("gridVentasxCobrar.GridBodyContainer.setAttribute('style', gridBodyStyle);", 250);
            return false;
        }
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                    <span style="padding: 10px; font-size: 13px; font-weight: bold; color: #555555;">Procesando
                        ... </span>
                </div>
                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
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
                                                </td>
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
                                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" ToolTip="Procesar" CssClass="clsBtnProcesar"
                                                            OnClick="btnProcesar_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" ToolTip="Imprimir" CssClass="clsBtnImprimir"
                                                            OnClientClick="printGrid()" OnClick="btnImprimir_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" ToolTip="Excel" CssClass="clsBtnExcel"
                                                            OnClick="btnExcel_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnPdf" runat="server" Text="A Pdf" ToolTip="Pdf" 
                                                            CssClass="clsBtnPdf" onclick="btnPdf_Click"
                                                             />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div style="text-align: center; color: Green; font-size: 14px; font-weight: bold;">
                                        <asp:Label ID="lblExito" runat="server" Text="El Reporte de Proceso Correctamente."
                                            Visible="false"></asp:Label></div>
                                    <div style="text-align: center; color: Red; font-size: 14px; font-weight: bold;">
                                        <asp:Label ID="lblError" runat="server" Text="No hay Registros para Procesar el Reporte"
                                            Visible="false"></asp:Label></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablaDerecha" valign="top" colspan="2">
                                    <table class="tablaDerecha">
                                        <tr>
                                            <td>
                                                <div id="container" style="position: relative; width: 100%;">
                                                    <cc1:Grid ID="gridVentasxCobrar" runat="server" AllowAddingRecords="False" AllowColumnResizing="False"
                                                        CallbackMode="true" Serialize="true" AllowFiltering="True" AutoGenerateColumns="False"
                                                        Width="100%" FolderStyle="..\App_Themes\TemaAgrocomercio\Grid\style_6"
                                                        GroupBy="CliNombre" FolderLocalization="..\App_Themes\TemaAgrocomercio\Grid\localization"
                                                        Language="es" OnFiltering="gridVentasxCobrar_Filtering" 
                                                        FolderExports="~/TmpExports/">
                                                        <Columns>
                                                            <cc1:Column DataField="OpeCod" HeaderText="Codigo" Index="0" AllowGroupBy="False"
                                                                AllowFilter="False" Width="60">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="OpeFecEmision" HeaderText="Fec.Emision" Index="1" DataFormatString="{0:d/M/yyyy}"
                                                                AllowGroupBy="False" Width="190">
                                                                <FilterOptions>
                                                                    <cc1:CustomFilterOption IsDefault="true" ID="Between_OpeFecEmision" Text="Entre">
                                                                        <TemplateSettings FilterTemplateId="OpeFecEmisionBetweenFilter" FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" />
                                                                        <TemplateSettings FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                                            FilterControlsPropertyNames="value,value" FilterTemplateId="OpeFecEmisionBetweenFilter" />
                                                                    </cc1:CustomFilterOption>
                                                                </FilterOptions>
                                                            </cc1:Column>
                                                            <cc1:Column DataField="NroNota" HeaderText="Nro Pedido" Index="2" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="NroFactura" HeaderText="Nro Fatura" Index="3" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="CliNombre" HeaderText="Cliente" Index="4" Width="180" ShowFilterCriterias="false">
                                                                <TemplateSettings FilterTemplateId="ClienteFilter" />
                                                                <FilterOptions>
                                                                    <cc1:FilterOption IsDefault="true" Type="Contains" />
                                                                </FilterOptions>
                                                                <TemplateSettings FilterTemplateId="ClienteFilter" />
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Zona" HeaderText="Zona" Index="5" Width="80" ShowFilterCriterias="false">
                                                                <TemplateSettings FilterTemplateId="ZonasFilter" />
                                                                <TemplateSettings FilterTemplateId="ZonasFilter" />
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Vendedor" HeaderText="Vendedor" Index="6" Width="100" ShowFilterCriterias="false">
                                                                <TemplateSettings FilterTemplateId="VendedorFilter" />
                                                                <TemplateSettings FilterTemplateId="VendedorFilter" />
                                                            </cc1:Column>
                                                            <cc1:Column DataField="dFecPago" HeaderText="Fec.Pago" Index="7" DataFormatString="{0:d/M/yyyy}"
                                                                AllowGroupBy="False" AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Monto_USD" HeaderText="Monto USD" Index="8" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="ACta_USD" HeaderText="A Cta USD" Index="9" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Saldo_USD" HeaderText="Saldo USD" Index="10" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Monto_PEN" HeaderText="Monto PEN" Index="11" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="ACta_PEN" HeaderText="A Cta PEN" Index="12" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Saldo_PEN" HeaderText="Saldo PEN" Index="13" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nSaldoTotal" HeaderText="Saldo Total" Index="14" AllowGroupBy="False"
                                                                Width="100">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="CliCreAsig" HeaderText="Credito Asign" Index="15" AllowGroupBy="False"
                                                                AllowFilter="False" Width="80">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="CliDireccion" HeaderText="Direccion" Index="16" AllowGroupBy="False"
                                                                AllowFilter="False" Width="180">
                                                            </cc1:Column>
                                                        </Columns>
                                                        <ScrollingSettings ScrollWidth="870px" />
                                                        <Templates>
                                                            <cc1:GridTemplate runat="server" ID="OpeFecEmisionBetweenFilter">
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
                                                            </cc1:GridTemplate>
                                                            <cc1:GridTemplate runat="server" ID="ZonasFilter" ControlID="ddlZonas" ControlPropertyName="value">
                                                                <Template>
                                                                    <cc3:OboutDropDownList runat="server" ID="ddlZonas" Width="100%" MenuWidth="100"
                                                                        FolderStyle="styles/premiere_blue/interface/OboutDropDownList" DataSourceID="odsZonas"
                                                                        DataTextField="AtrDescripcion" DataValueField="AtrCodigo" />
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                            <cc1:GridTemplate runat="server" ID="VendedorFilter" ControlID="ddlVendedor" ControlPropertyName="value">
                                                                <Template>
                                                                    <cc3:OboutDropDownList runat="server" ID="ddlVendedor" Width="100%" MenuWidth="100"
                                                                        FolderStyle="styles/premiere_blue/interface/OboutDropDownList" DataSourceID="odsVendedor"
                                                                        DataTextField="PerNombres" DataValueField="perCod" />
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                            <cc1:GridTemplate runat="server" ID="ClienteFilter" ControlID="txtCliente">
                                                                <Template>
                                                                    <cc3:OboutTextBox runat="server" ID="txtCliente" Width="100%">
                                                                        <ClientSideEvents OnKeyUp="applyFilter" />
                                                                    </cc3:OboutTextBox>
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                        </Templates>
                                                        <ExportingSettings ExportAllPages="True" ExportColumnsFooter="True" ExportDetails="True"
                                                            ExportGroupFooter="True" ExportGroupHeader="True" FileName="VentasXCobrar"
                                                            KeepColumnSettings="True" />
                                                    </cc1:Grid>
                                                    <script type="text/javascript">

                                                        var applyFilterTimeout = null;

                                                        function applyFilter() {
                                                            if (applyFilterTimeout) {
                                                                window.clearTimeout(applyFilterTimeout);
                                                            }

                                                            applyFilterTimeout = window.setTimeout(doFiltering, 500);
                                                        }

                                                        function doFiltering() {
                                                            gridVentasxCobrar.filter();
                                                        }
                                                    </script>
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
