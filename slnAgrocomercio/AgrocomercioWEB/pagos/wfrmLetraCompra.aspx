<%@ Page Title=".:Registro de Letras:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmLetraCompra.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmLetraCompra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setPrvCod(source, eventargs) {
            document.getElementById('lblEstado').value = "PRV_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
        }
        function dgvFacPendClickEvent(event, rowindex) {
            document.getElementById('lblEstado').value = "DGVLIS_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        } dgvFacPendClickEvent
        function dgvLetRegClickEvent(event, rowindex) {
            document.getElementById('lblEstado').value = "DGVLET_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 128px;
        }
        .style2
        {
            width: 144px;
        }
        .style3
        {
            width: 163px;
        }
        .style4
        {
            width: 274px;
        }
        .style5
        {
            width: 200px;
        }
        .style6
        {
            width: 91px;
        }
        .style7
        {
            width: 100px;
        }
        .style8
        {
            width: 129px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 800px; margin: 0 auto;">
                <tr>
                    <td colspan="2">
                        <h2 class="clsTituloInterior">
                            SISTEMA DE PAGOS DE LAS COMPRAS DE AGROCOMERCIO S.R.L.</h2>
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lblPaso" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lblProceso" runat="server" />
                        <asp:HiddenField ID="lblOpeEstado" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="tabLista">
                            <tr>
                                <td class="tabIzquierda" valign="top">
                                    <asp:Panel ID="pnListLetras" runat="server">
                                        <table class="tableIzquierda">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnBusqueda" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px">
                                                        <h3>
                                                            Filtro de Busqueda:</h3>
                                                        <table class="tabPedidos">
                                                            <tr>
                                                                <td align="right">
                                                                    Estado Del Documento:
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbtPendiente" runat="server" Text="Pendientes" GroupName="GRUPO1"
                                                                        Checked="true" OnCheckedChanged="rbtPendiente_CheckedChanged" AutoPostBack="true" />
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:RadioButton ID="rbtProcesado" runat="server" Text="Registradas" GroupName="GRUPO1"
                                                                        OnCheckedChanged="rbtPendiente_CheckedChanged" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Moneda:
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Width="100px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    _____________________________________________________________________
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Proveedor:
                                                                </td>
                                                                <td class="clsCellDatos2" colspan="3">
                                                                    <asp:DropDownList ID="ddlProveedor" runat="server" Width="250px" AutoPostBack="True"
                                                                        DataTextField="PrvRazon" DataValueField="PrvCod" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"
                                                                        Style="display: none;">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtProveedor" runat="server" Width="270px"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txtProveedor_TextBoxWatermarkExtender" runat="server"
                                                                        TargetControlID="txtProveedor" WatermarkCssClass="clsWaterMark" WatermarkText="Busqueda de Proveedores..."
                                                                        Enabled="true">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <div id="PrvlistPlacement" class="cls_listPlacement" style="overflow: auto;">
                                                                    </div>
                                                                    <asp:AutoCompleteExtender ID="txtProveedor_AutoCompleteExtender" MinimumPrefixLength="2"
                                                                        TargetControlID="txtProveedor" EnableCaching="true" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                        CompletionSetCount="10" CompletionInterval="100" ServiceMethod="BuscarProveedores"
                                                                        runat="server" OnClientItemSelected="setPrvCod" CompletionListElementID="PrvlistPlacement">
                                                                    </asp:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Panel ID="pnFilPend" runat="server">
                                                                        filtro Pendientes
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    Numero de Documento:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNumDoc" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha de Registro:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtflfecpro" runat="server" Width="100px"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="txtflfecpro_CalendarExtender" runat="server" Enabled="True"
                                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtflfecpro">
                                                                                    </asp:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <asp:GridView ID="dgvFacPend" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvFacPend_RowDataBound">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                                            <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="false" />
                                                                                            <asp:BoundField DataField="dfecemision" HeaderText="dfecemision" SortExpression="dfecemision"
                                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                                            <asp:BoundField DataField="dopCod" HeaderText="dopCod" ReadOnly="True" SortExpression="dopCod"
                                                                                                Visible="false" />
                                                                                            <asp:BoundField DataField="numfac" HeaderText="numfac" SortExpression="numfac" />
                                                                                            <asp:BoundField DataField="PrvRazon" HeaderText="PrvRazon" SortExpression="PrvRazon" />
                                                                                            <asp:BoundField DataField="opetotpagar" HeaderText="opetotpagar" SortExpression="opetotpagar" />
                                                                                            <asp:BoundField DataField="PrvCod" HeaderText="PrvCod" SortExpression="PrvCod" Visible="false" />
                                                                                            <asp:BoundField DataField="monedas" HeaderText="monedas" SortExpression="monedas" />
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <i>
                                                                                                <div class="clsError1" id="lblError1" runat="server">
                                                                                                    ____ . ____
                                                                                                </div>
                                                                                            </i>
                                                                                        </EmptyDataTemplate>
                                                                                        <PagerStyle CssClass="pgr" />
                                                                                        <SelectedRowStyle CssClass="selrow" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Panel ID="pnFilReg" runat="server">
                                                                        filtro Registradas
                                                                        <table style="width: 456px">
                                                                            <tr>
                                                                                <td class="style8">
                                                                                    Estado:
                                                                                </td>
                                                                                <td class="style5">
                                                                                    Fecha Max del Prox. Pago:
                                                                                </td>
                                                                                <td class="style4">
                                                                                    Numero de Letra:
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style8">
                                                                                    <asp:DropDownList ID="dllEstado" runat="server">
                                                                                        <asp:ListItem Value="PEND">Pendiente</asp:ListItem>
                                                                                        <asp:ListItem Value="CANC">Cancelado</asp:ListItem>
                                                                                        <asp:ListItem Value="AMB">Ambas</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="style5">
                                                                                    <asp:TextBox ID="TxtFecPAgo" runat="server" Width="100px"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                                        PopupButtonID="Image2" TargetControlID="TxtFecPAgo">
                                                                                    </asp:CalendarExtender>
                                                                                </td>
                                                                                <td class="style4">
                                                                                    <asp:TextBox ID="txtNumLetra" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:GridView ID="dgvLetReg" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvLetReg_RowDataBound">
                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                        <Columns>
                                                                                            <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                                            <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="false" />
                                                                                            <asp:BoundField DataField="numDoc" HeaderText="Num. Letra" SortExpression="numDoc" />
                                                                                            <asp:BoundField DataField="fac_acum" HeaderText="fact. Acum" SortExpression="fac_acum" />
                                                                                            <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" ReadOnly="True" SortExpression="PrvRazon" />
                                                                                            <asp:BoundField DataField="FecEmesion" HeaderText="Fec.Registro" SortExpression="FecEmesion"
                                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                                            <asp:BoundField DataField="itotcuota" HeaderText="Tot. Cuo" SortExpression="itotcuota" />
                                                                                            <asp:BoundField DataField="nmontocuota" HeaderText="Tot. Pago" SortExpression="nmontocuota" />
                                                                                            <asp:BoundField DataField="pagpendiente" HeaderText="Saldo" SortExpression="pagpendiente" />
                                                                                            <asp:BoundField DataField="cmoneda" HeaderText="Moneda" SortExpression="cmoneda" />
                                                                                            <asp:BoundField DataField="nintpag" HeaderText="Int. Pag" SortExpression="nintpag" />
                                                                                            <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                                                                                            <asp:BoundField DataField="dfecultpago" HeaderText="Fec. Ult. Pago" SortExpression="dfecultpago"
                                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                                            <asp:BoundField DataField="fec_nxtpg" HeaderText="Fec. Sig. Pago" SortExpression="fec_nxtpg"
                                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                                            <asp:BoundField DataField="icodletra" HeaderText="icodletra" ReadOnly="True" SortExpression="icodletra" />
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <i>
                                                                                                <div class="clsError1" id="lblError1" runat="server">
                                                                                                    ____ . ____
                                                                                                </div>
                                                                                            </i>
                                                                                        </EmptyDataTemplate>
                                                                                        <PagerStyle CssClass="pgr" />
                                                                                        <SelectedRowStyle CssClass="selrow" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnNuevo" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px">
                                                        <table class="tableIzquierda">
                                                            <tr>
                                                                <td colspan="3" style="width: 20%;">
                                                                    <asp:HiddenField ID="lblTipoDoc" runat="server" />
                                                                    <asp:HiddenField ID="lbldopCod" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style6">
                                                                    Fecha de Registro:
                                                                </td>
                                                                <td class="style7">
                                                                    <asp:TextBox ID="txtFecha" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" TargetControlID="txtFecha"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" Enabled="True">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td align="right">
                                                                    <table class="clsTitDocumento">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblDescriDocumento" runat="server" Text="Letra" CssClass="clsTitDocumento_CAB"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblNroDocumento" runat="server" Text="Label" CssClass="clsTitDocumento_NUM"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Proveedor:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblPersona" runat="server" Text="Datos Provedor"></asp:Label>
                                                                    <asp:HiddenField ID="hdcodper" runat="server" Value="0" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Moneda:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:DropDownList ID="ddlMoneda_new" runat="server" AutoPostBack="True" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Width="100px" OnSelectedIndexChanged="ddlMoneda_new_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Panel ID="pnaddfactura" runat="server" Font-Bold="True" ForeColor="#006600">
                                                                        <table class="tableIzquierda">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <h3>
                                                                                        ENLAZAR FACTURAS A LA LETRA</h3>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:GridView ID="dgvListFact" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Style="margin-top: 0px; padding-top: 0px;"
                                                                                        PageSize="5" Width="233px" OnSelectedIndexChanged="dgvListFact_SelectedIndexChanged">
                                                                                        <Columns>
                                                                                            <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" OnCheckedChanged="grdchk_CheckedChanged"
                                                                                                        AutoPostBack="true" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="dopcod" HeaderText="dop.cod">
                                                                                                <ItemStyle Width="20px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="dfecemision" HeaderText="Fec. Facturacion" DataFormatString="{0:d}">
                                                                                                <ItemStyle Width="20px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="numfac" HeaderText="Nro. de Factura" ReadOnly="True">
                                                                                                <HeaderStyle Width="100px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="OpeTotal" HeaderText="S/.">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <i>
                                                                                                <div class="clsError1" id="lblError1" runat="server">
                                                                                                    No hay Facturas Dispoibles Disponibles</div>
                                                                                            </i>
                                                                                        </EmptyDataTemplate>
                                                                                        <PagerStyle CssClass="pgr" />
                                                                                        <SelectedRowStyle CssClass="selrow" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" colspan="2">
                                                                                    Monto Facturado:
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lbSuma" runat="server" Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Panel ID="pnNotas" runat="server" Font-Bold="True" ForeColor="#006600">
                                                                        <table class="tableIzquierda">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <h3>
                                                                                        ENLAZAR Notas de credito al pago</h3>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:GridView ID="dgvNotas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Style="margin-top: 0px; padding-top: 0px;"
                                                                                        PageSize="5" Width="233px" OnSelectedIndexChanged="dgvListFact_SelectedIndexChanged">
                                                                                        <Columns>
                                                                                            <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="grdchknota" runat="server" Checked="False" OnCheckedChanged="grdchknota_CheckedChanged"
                                                                                                        AutoPostBack="true" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="tiponota" HeaderText="Tip. Nota">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ctipNota" HeaderText="Tip. Nota">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="FecEmesion" HeaderText="Fec. Nota" DataFormatString="{0:d}">
                                                                                                <ItemStyle Width="20px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="inota" HeaderText="id" ReadOnly="True">
                                                                                                <HeaderStyle Width="100px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="ccodnota" HeaderText="Codigo" ReadOnly="True">
                                                                                                <HeaderStyle Width="100px" />
                                                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nmontonota" HeaderText="Monto">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nmntutilizado" HeaderText="Monto Disponible">
                                                                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <i>
                                                                                                <div class="clsError1" id="lblError1" runat="server">
                                                                                                    No hay Notas Disponibles</div>
                                                                                            </i>
                                                                                        </EmptyDataTemplate>
                                                                                        <PagerStyle CssClass="pgr" />
                                                                                        <SelectedRowStyle CssClass="selrow" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="right">
                                                                                    Monto Deducido:
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lbresta" runat="server" Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" align="right">
                                                                                    &nbsp;<asp:Label ID="Label4" runat="server" Text="TOTAL A PAGAR:" Font-Bold="True"
                                                                                        Font-Size="Medium" ForeColor="#006600"></asp:Label>
                                                                                    <asp:Label ID="lbpago" runat="server" Text="0.00" Font-Bold="True" Font-Size="Medium"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="right">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Numero de Cuotas:" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td class="clsCellDatos2" colspan="2">
                                                                                    <asp:TextBox ID="txtNumCuotas" runat="server" Width="33px" OnTextChanged="txtnumCuotas_TextChange"
                                                                                        AutoPostBack="true" BorderColor="Red" BorderStyle="Dashed" Font-Bold="True" Font-Size="Medium"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" align="right" class="style3">
                                                                                    <asp:Button ID="btgenCuotas" runat="server" Text="Generar" CssClass="clsBtnAgregar"
                                                                                        OnClick="btnGenerar_Click" Visible="false" Height="41px" Width="105px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Panel ID="pnCuotas" runat="server" Font-Bold="True" ForeColor="#006600">
                                                                        <table class="tableIzquierda">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="dgvcuotas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Style="margin-top: 0px; padding-top: 0px;"
                                                                                        Height="30px" PageSize="5" Width="483px">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText="Cuota" />
                                                                                            <asp:TemplateField HeaderText="Fec.Vencimiento">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtFecVen" runat="server" Width="100px"></asp:TextBox>
                                                                                                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" TargetControlID="txtFecVen"
                                                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" Enabled="True">
                                                                                                    </asp:CalendarExtender>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Nro.Letra">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="num_let" runat="server" Height="22px" Width="88px"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Cod.Unico">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="cod_unic" runat="server" Height="22px" Width="88px"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Monto">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="monto" runat="server"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td class="tabDerecha" valign="top">
                                    <div class="divDerecha">
                                    </div>
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
                                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="clsBtnNuevo" OnClick="btnNuevo_Click"
                                                        CausesValidation="False" Height="41px" Width="98px" ToolTip="Nuevo" />
                                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="clsBtnGuardar"
                                                        OnClick="btnGuardar_Click" Height="41px" Width="110px" ToolTip="Guardar" />
                                                </td>
                                                <td valign="top">
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="clsBtnCancelar"
                                                        OnClick="btnCancelar_Click" CausesValidation="False" Height="41px" ToolTip="Cancelar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style2">
                                                    <asp:Button ID="btnEditar" runat="server" Text="Detalle" CssClass="clsBtnEditar"
                                                        OnClick="btnEditar_Click" Height="41px" Width="99px" ToolTip="Editar" />
                                                </td>
                                                <td valign="top" class="style2">
                                                    <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="clsBtnAnular" Height="41px"
                                                        Width="106px" ToolTip="Anular" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:Button ID="btnProcesar" runat="server" Text="Buscar" CssClass="clsBtnProcesar"
                                                        OnClick="btnProcesar_Click" Height="41px" ToolTip="Procesar" />
                                                </td>
                                                <td valign="top" class="style1">
                                                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="clsBtnImprimir"
                                                        Height="38px" ToolTip="Imprimir" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
