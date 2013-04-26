<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmRepNotasVentas.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmRepNotasVentas"
     %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function dgvDetalleVentaClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVCOM_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }
        function dgvListOperLetrasClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVLIS_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }

        function AbrirVentana() {
            window.open('', '', 'width=200,height=100')
        }

        function setArtCod(source, eventargs) {
            document.getElementById('lblEstado').value = "ART_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
        }
        function AbrirVentanaReporte() {
            window.open('../Reportes/ImpresionNotaGeneral.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 46px;
        }
        .style2
        {
            height: 41px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 800px; margin: 0 auto;">
                <tr>
                    <td colspan="2">
                        <table class="tabLista">
                            <tr>
                                <td class="tabIzquierda" valign="top">
                                    <asp:Panel ID="pnListOperCompras" runat="server">
                                        <table class="tableIzquierda">
                                            <tr>
                                                <td colspan="2">
                                                    <h2 class="clsTituloInterior">
                                                        reporte de notas DE DEbito</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="clsCellTituloDatos1" colspan="2">
                                                    <asp:Label ID="Label1" runat="server" Text="Eliga un Reporte: "></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rbResumido" runat="server" Text="Resumido" Checked="true" GroupName="GRUPO1" />
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rbDetallado" runat="server" Text="Detallado" GroupName="GRUPO1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Cliente:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProveedor" runat="server" DataTextField="PrvRazon" DataValueField="PrvCod"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnLista" runat="server" ScrollBars="Vertical">
                                                        <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="false" />
                                                                <asp:BoundField DataField="TIPO" HeaderText="TIPO" ReadOnly="True" Visible="false"
                                                                    SortExpression="TIPO" />
                                                                <asp:BoundField DataField="inota" HeaderText="inota" ReadOnly="True" Visible="false"
                                                                    SortExpression="inota" />
                                                                <asp:BoundField DataField="ccodnota" HeaderText="Cod.Nota" SortExpression="ccodnota" />
                                                                <asp:BoundField DataField="FecEmesion" HeaderText="Fec.Emision" ReadOnly="True" SortExpression="FecEmesion" />
                                                                <asp:BoundField DataField="iprvcod" HeaderText="iprvcod" SortExpression="iprvcod"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Cliente" ReadOnly="True" SortExpression="PrvRazon" />
                                                                <asp:BoundField DataField="cestadoNota" HeaderText="cestadoNota" SortExpression="cestadoNota"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="estado" HeaderText="Estado" ReadOnly="True" SortExpression="estado" />
                                                                <asp:BoundField DataField="COD_LETRA" HeaderText="Cod.Letra" SortExpression="COD_LETRA" />
                                                                <asp:BoundField DataField="TOTFACT" HeaderText="Total Facturas" ReadOnly="True" SortExpression="TOTFACT" />
                                                                <asp:BoundField DataField="nmntdolares" HeaderText="Saldo Dolares" ReadOnly="True"
                                                                    SortExpression="nmntdolares" />
                                                                <asp:BoundField DataField="nmntsoles" HeaderText="Saldo Dolares" ReadOnly="True"
                                                                    SortExpression="nmntsoles" />
                                                                <asp:BoundField DataField="moneda" HeaderText="Moneda" SortExpression="moneda" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registro Ninguna Nota</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="pgr" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnListDet" runat="server" ScrollBars="Vertical">
                                                        <asp:GridView ID="dgvlistDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="false" />
                                                                <asp:BoundField DataField="TIPO" HeaderText="TIPO" ReadOnly="True" Visible="false"
                                                                    SortExpression="TIPO" />
                                                                <asp:BoundField DataField="inota" HeaderText="inota" ReadOnly="True" Visible="false"
                                                                    SortExpression="inota" />
                                                                <asp:BoundField DataField="ccodnota" HeaderText="Cod.Nota" SortExpression="ccodnota" />
                                                                <asp:BoundField DataField="FecEmesion" HeaderText="Fec.Emision" ReadOnly="True" SortExpression="FecEmesion" />
                                                                <asp:BoundField DataField="iprvcod" HeaderText="iprvcod" SortExpression="iprvcod"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Cliente" ReadOnly="True" SortExpression="PrvRazon" />
                                                                <asp:BoundField DataField="cestadoNota" HeaderText="cestadoNota" SortExpression="cestadoNota"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="estado" HeaderText="Estado" ReadOnly="True" SortExpression="estado" />
                                                                <asp:BoundField DataField="COD_LETRA" HeaderText="Cod.Letra" SortExpression="COD_LETRA" />
                                                                <asp:BoundField DataField="factura" HeaderText="Facturas" ReadOnly="True" SortExpression="factura" />
                                                                <asp:BoundField DataField="nmntdolutl" HeaderText="Monto utl. Dolares" ReadOnly="True"
                                                                    SortExpression="nmntdolutl" />
                                                                <asp:BoundField DataField="nmntsolutl" HeaderText="Monto utl. Soles" ReadOnly="True"
                                                                    SortExpression="nmntsolutl" />
                                                                <asp:BoundField DataField="moneda" HeaderText="Moneda" SortExpression="moneda" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registro Ninguna Nota</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="pgr" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td class="tabDerecha" valign="top">
                                    <div class="divDerecha">
                                        <asp:Panel ID="pnBotones" runat="server" CssClass="clspntablaDerecha">
                                            <table class="tablaDerecha">
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <h3>
                                                            Opciones</h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Button ID="btnNuevo" runat="server" CausesValidation="False" CssClass="clsBtnNuevo"
                                                            Height="41px" OnClick="btnNuevo_Click" Text="Nuevo" Width="98px" ToolTip="Nuevo" />
                                                        <asp:Button ID="btnGuardar" runat="server" CssClass="clsBtnGuardar" Height="41px"
                                                            OnClick="btnGuardar_Click" Text="Guardar" Width="110px" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="clsBtnCancelar"
                                                            Height="41px" OnClick="btnCancelar_Click" Text="Cancelar" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2" valign="top">
                                                        <asp:Button ID="btnEditar" runat="server" CssClass="clsBtnEditar" Height="41px" OnClick="btnEditar_Click"
                                                            Text="Editar" Width="99px" ToolTip="Editar" />
                                                    </td>
                                                    <td class="style2" valign="top">
                                                        <asp:Button ID="btnAnular" runat="server" CssClass="clsBtnAnular" Height="41px" OnClick="btnAnular_Click"
                                                            Text="Anular" Width="106px" ToolTip="Anular" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" valign="top">
                                                        <asp:Button ID="btnProcesar" runat="server" CssClass="clsBtnProcesar" Height="41px"
                                                            OnClick="btnProcesar_Click" Text="Procesar" ToolTip="Procesar" />
                                                    </td>
                                                    <td class="style1" valign="top">
                                                        <asp:Button ID="btnImprimir" runat="server" CssClass="clsBtnImprimir" Height="38px"
                                                            OnClick="btnImprimir_Click" Text="Imprimir" ToolTip="Imprimir"  OnClientClick="AbrirVentanaReporte()"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
