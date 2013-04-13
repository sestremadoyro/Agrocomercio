<%@ Page Title=".:Reportes de Compras:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmRepResumenCliente.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmRepResumenCliente" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc4" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
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
                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Label">Reportes de Resumen de Clientes</asp:Label></h3>
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
                                                            Width="100%" FolderStyle="..\App_Themes\TemaAgrocomercio\Grid\style_6" GroupBy="CliNombre"
                                                            FolderLocalization="..\App_Themes\TemaAgrocomercio\Grid\localization" Language="es"
                                                            OnFiltering="gridVentasxCobrar_Filtering" FolderExports="~/TmpExports/">
                                                            <Columns>
                                                                <cc1:Column DataField="OpeCod" HeaderText="Codigo" Index="0" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="60">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="Anio" HeaderText="Año" Index="1" AllowGroupBy="False" AllowFilter="False"
                                                                    Width="50">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="Mes" HeaderText="Mes" Index="2" AllowGroupBy="False" AllowFilter="False"
                                                                    Width="70">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="Dia_mes" HeaderText="Dia-Mes" Index="3" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="60">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="NroFactura" HeaderText="Nro Fatura" Index="4" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="90">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="NroNota" HeaderText="Nro Pedido" Index="5" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="90">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="Zona" HeaderText="Zona" Index="6" Width="80" ShowFilterCriterias="false">
                                                                    <TemplateSettings FilterTemplateId="ZonasFilter" />
                                                                    <TemplateSettings FilterTemplateId="ZonasFilter" />
                                                                </cc1:Column>
                                                                <cc1:Column DataField="dFecPago" HeaderText="Fec.Pago" Index="7" DataFormatString="{0:d/M/yyyy}"
                                                                    AllowGroupBy="False" AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nVentaUSD" HeaderText="Venta USD" Index="8" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nPagoUSD" HeaderText="Pago USD" Index="9" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nVentaPEN" HeaderText="Venta PEN" Index="10" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nPagoPEN" HeaderText="Pago PEN" Index="11" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nSaldoUSD" HeaderText="Saldo USD" Index="12" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nSaldoPEN" HeaderText="Saldo PEN" Index="13" AllowGroupBy="False"
                                                                    AllowFilter="False" Width="80">
                                                                </cc1:Column>
                                                                <cc1:Column DataField="nSaldo" HeaderText="Saldo Total" Index="14" AllowGroupBy="False"
                                                                    Width="100">
                                                                </cc1:Column>
                                                            </Columns>
                                                            <ScrollingSettings ScrollWidth="870px" />
                                                            <Templates>
                                                                <cc1:GridTemplate runat="server" ID="ZonasFilter" ControlID="ddlZonas" ControlPropertyName="value">
                                                                    <Template>
                                                                        <cc3:OboutDropDownList runat="server" ID="ddlZonas" Width="100%" MenuWidth="100"
                                                                            FolderStyle="styles/premiere_blue/interface/OboutDropDownList" DataSourceID="odsZonas"
                                                                            DataTextField="AtrDescripcion" DataValueField="AtrCodigo" />
                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                            </Templates>
                                                            <ExportingSettings ExportAllPages="True" ExportColumnsFooter="True" ExportDetails="True"
                                                            ExportGroupFooter="True" ExportGroupHeader="True" FileName="ResumenCliente"
                                                            KeepColumnSettings="True" />
                                                        </cc1:Grid>
                                                        <asp:ObjectDataSource ID="odsZonas" runat="server" SelectMethod="ListDataAtributos"
                                                            TypeName="pryAgrocomercioBLL.EntityCollection.clsAtributos">
                                                            <SelectParameters>
                                                                <asp:Parameter DefaultValue="4" Name="pcAtrTipoCod" />
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