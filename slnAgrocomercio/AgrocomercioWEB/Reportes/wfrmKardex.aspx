<%@ Page Title=".:Reportes de Kardex Fisico y Valorado:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmKardex.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmKardex" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
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
                                                <asp:GridView ID="gridKardex" runat="server" AutoGenerateColumns="False"  
                                                    CellPadding="4" GridLines="Vertical"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" 
                                                        onrowcreated="gridKardex_RowCreated" onrowdatabound="gridKardex_RowDataBound" >
                                                            <AlternatingRowStyle CssClass="alt" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" >
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:BoundField>
                                                            <asp:BoundField DataField="ArtCod" HeaderText="Art.Codigo" >
                                                                <ItemStyle HorizontalAlign="Left" Width="20px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ArtDescripcion" HeaderText="Articulo" >
                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ArtStockIni" HeaderText="Art.Stock.Inicial"  >
                                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="OpeFecEmision" HeaderText="Fec.Emision" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Documento" HeaderText="Nro Documento" >
                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Decripcion" HeaderText="Descripcion" >
                                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Cantidad" HeaderText="Comp.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Left" Width="95px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Unidad" HeaderText="Comp.Unidad" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_PreUnitario" HeaderText="Comp.Precio.Unit" >
                                                                <ItemStyle HorizontalAlign="Left" Width="105px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nCom_Costo" HeaderText="Comp.Costo" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Cantidad" HeaderText="Venta.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Left" Width="95px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Unidad" HeaderText="Venta.Unidad" >
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_PreUnitario" HeaderText="Venta.Precio.Unit" >
                                                                <ItemStyle HorizontalAlign="Right" Width="105px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nVen_Costo" HeaderText="Venta.Costo" >
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_Cantidad" HeaderText="Saldo.Cantidad" >
                                                                <ItemStyle HorizontalAlign="Right" Width="95px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_Unidad" HeaderText="Saldo.Unidad">
                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="nSal_CostoTotal" HeaderText="Saldo.Costo Total" >
                                                                <ItemStyle HorizontalAlign="Right" Width="110px" />
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