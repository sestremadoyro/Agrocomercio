<%@ Page Title=".:Reportes de Compras:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmKardex.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmKardex" %>

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
                                                        Language="es" OnFiltering="gridVentasxCobrar_Filtering" PageSize="50" FolderExports="~/TmpExports/">
                                                        <Columns>
                                                            <cc1:Column DataField="ArtCod" HeaderText="Art.Codigo" Index="0" AllowGroupBy="False"
                                                                AllowFilter="False" Width="70">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="ArtDescripcion" HeaderText="Articulo" Index="1" AllowGroupBy="False"
                                                                AllowFilter="False" Width="130">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="ArtStockIni" HeaderText="Articulo Stock Inicial" Index="2" AllowGroupBy="False"
                                                                AllowFilter="False" Width="60">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="OpeFecEmision" HeaderText="Fec.Emision" Index="3" DataFormatString="{0:d/M/yyyy}"
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
                                                            <cc1:Column DataField="Documento" HeaderText="Nro Documento" Index="4" AllowGroupBy="False"
                                                                AllowFilter="False" Width="100">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="Decripcion" HeaderText="Descripcion" Index="5" AllowGroupBy="False"
                                                                AllowFilter="False" Width="180">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nCom_Cantidad" HeaderText="Comp.Cantidad" Index="6" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nCom_Unidad" HeaderText="Comp.Unidad" Index="7" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nCom_PreUnitario" HeaderText="Comp.Precio.Unit" Index="8" AllowGroupBy="False"
                                                                AllowFilter="False" Width="105">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nCom_Costo" HeaderText="Comp.Costo" Index="9" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nVen_Cantidad" HeaderText="Venta.Cantidad" Index="10" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nVen_Unidad" HeaderText="Venta.Unidad" Index="11" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nVen_PreUnitario" HeaderText="Venta.Precio.Unit" Index="12" AllowGroupBy="False"
                                                                AllowFilter="False" Width="105">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nVen_Costo" HeaderText="Venta.Costo" Index="13" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nSal_Cantidad" HeaderText="Saldo.Cantidad" Index="14" AllowGroupBy="False"
                                                                AllowFilter="False" Width="95">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nSal_Unidad" HeaderText="Saldo.Unidad" Index="15" AllowGroupBy="False"
                                                                AllowFilter="False" Width="90">
                                                            </cc1:Column>
                                                            <cc1:Column DataField="nSal_CostoTotal" HeaderText="Saldo.Costo Total" Index="16" AllowGroupBy="False"
                                                                AllowFilter="False" Width="110">
                                                            </cc1:Column>
                                                        </Columns>
                                                        <ScrollingSettings ScrollWidth="870px" />
                                                        <ExportingSettings ExportAllPages="True" ExportColumnsFooter="True" ExportDetails="True"
                                                            ExportGroupFooter="True" ExportGroupHeader="True" FileName="Kardex"
                                                            KeepColumnSettings="True" />
                                                    </cc1:Grid>
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
