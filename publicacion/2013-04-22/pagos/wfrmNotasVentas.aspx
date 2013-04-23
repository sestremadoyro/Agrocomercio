<%@ Page Title=".:Registro de Notas:." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmNotasVentas.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmNotasVentas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setCliCod(source, eventargs) {
            document.getElementById('lblEstado').value = "PRV_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
        }
        function dgvNotasClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVLIS_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 800px; margin: 0 auto;">
                <tr>
                    <td colspan="2">
                        <h2 class="clsTituloInterior">
                            NOTAS DE AGROCOMERCIO S.R.L.</h2>
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                        &nbsp;<asp:HiddenField ID="lblProceso" runat="server" />
                        <asp:HiddenField ID="lblOpeCodigo" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="tabPedidos">
                            <tr>
                                <td class="tabIzquierda" valign="top">
                                    <asp:Panel ID="pnNotasCompras" runat="server">
                                        <table class="tabPedidos">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnBusqueda" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px">
                                                        <h3>
                                                            Filtro de Busqueda:</h3>
                                                        <table class="tabPedidos">
                                                            <tr>
                                                                <td>
                                                                    Tipo de Nota
                                                                </td>
                                                                <td>
                                                                    Estado
                                                                </td>
                                                                <td>
                                                                    Moneda
                                                                </td>
                                                                <td>
                                                                    Cliente:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNotas" runat="server" Width="100px" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlEstado" runat="server">
                                                                        <asp:ListItem Value="N">Disponibles</asp:ListItem>
                                                                        <asp:ListItem Value="C">Utilizadas</asp:ListItem>
                                                                        <asp:ListItem Value="0">Ambas</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMoneda" runat="server" Width="100px" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="clsCellDatos2" colspan="3">
                                                                    <asp:DropDownList ID="ddlClientes" runat="server" Width="250px" AutoPostBack="True"
                                                                        DataTextField="CliNombre" DataValueField="CliCod" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"
                                                                        Style="display: none;">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtCliente" runat="server" Width="250px"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txtCliente_TextBoxWatermarkExtender" runat="server"
                                                                        TargetControlID="txtCliente" WatermarkCssClass="clsWaterMark" WatermarkText="Busqueda de Clientes..."
                                                                        Enabled="true">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <div id="ClilistPlacement" class="cls_listPlacement" style="overflow: auto;">
                                                                    </div>
                                                                    <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" MinimumPrefixLength="2"
                                                                        TargetControlID="txtCliente" EnableCaching="true" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                        CompletionSetCount="10" CompletionInterval="100" ServiceMethod="BuscarClientes"
                                                                        runat="server" OnClientItemSelected="setCliCod" CompletionListElementID="ClilistPlacement">
                                                                    </asp:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Desde:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecDesde" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecDesde_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecDesde">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td align="right">
                                                                    Hasta:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechaHasta" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFechaHasta_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFechaHasta">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td colspan =4>
                                                            _______________________________________________________________________________________</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:GridView ID="dgvNotas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvNotas_RowDataBound">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                            <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="False" />
                                                                            <asp:BoundField DataField="inota" HeaderText="inota" ReadOnly="True" SortExpression="inota" />
                                                                            <asp:BoundField DataField="ccodnota" HeaderText="Cod. Nota" SortExpression="ccodnota" />
                                                                            <asp:BoundField DataField="FecEmesion" HeaderText="Fec. Emesion" DataFormatString="{0:yyyy-MM-dd}" />
                                                                            <asp:BoundField DataField="iprvcod" HeaderText="iprvcod" SortExpression="iprvcod"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" SortExpression="PrvRazon" />
                                                                            <asp:BoundField DataField="nmontoNota" HeaderText="Monto Nota" SortExpression="nmontoNota" />
                                                                            <asp:BoundField DataField="cestadoNota" HeaderText="Estado" SortExpression="cestadoNota"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="nmntutilizado" HeaderText="Monto Utilz." SortExpression="nmntutilizado" />
                                                                            <asp:BoundField DataField="estado" HeaderText="Estado" ReadOnly="True" SortExpression="estado" />
                                                                            <asp:BoundField DataField="moneda" HeaderText="moneda" SortExpression="moneda" />
                                                                            <asp:BoundField DataField="ctipNota" HeaderText="ctipnota" SortExpression="ctipNota" />
                                                                            <asp:BoundField DataField="tipoNota" HeaderText="Tip. Nota" SortExpression="tipoNota" />
                                                                            <asp:BoundField DataField="cobservaciones" HeaderText="cobservaciones" SortExpression="cobservaciones"
                                                                                Visible="false" />
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
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnNuevo" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px" BackColor="White">
                                                        <h3>
                                                            Registro De Notas</h3>
                                                        <table class="tabPedidos">
                                                            <tr>
                                                                <td align="right">
                                                                    Proveedor:
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPersona" runat="server" Text="Datos Provedor"></asp:Label>
                                                                    <asp:HiddenField ID="hdcodper" runat="server" Value="0" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Fecha de Registro:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtfecpro" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtfecpro_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtfecpro">
                                                                    </asp:CalendarExtender>
                                                                    (*)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Tipo de Nota:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTipNot_new" runat="server" Width="176px" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Height="19px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2" align="right">
                                                                    Codigo:
                                                                </td>
                                                                <td class="style2">
                                                                    <asp:TextBox ID="txtCodNota" runat="server"></asp:TextBox>
                                                                    (*)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2" align="right">
                                                                    Moneda:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMoneda_new" runat="server" Width="176px" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Height="19px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Monto:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmonto" runat="server"></asp:TextBox>
                                                                    (*)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Monto Utilizado:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmontoutl" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Observaciones:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtobservaciones" runat="server" Height="61px" TextMode="MultiLine"
                                                                        Width="297px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lbmensaje" runat="server" Text="Es necesario llenar todos los campos solicitados (*)"
                                                                        ForeColor="Red" Font-Size="Large"></asp:Label>
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
                                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" ToolTip="Nuevo" CssClass="clsBtnNuevo"
                                                            OnClick="btnNuevo_Click" CausesValidation="False" />
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="Guardar" CssClass="clsBtnGuardar"
                                                            OnClick="btnGuardar_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" ToolTip="Cancelar" CssClass="clsBtnCancelar"
                                                            OnClick="btnCancelar_Click" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" ToolTip="Editar" CssClass="clsBtnEditar"
                                                            OnClick="btnEditar_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnAnular" runat="server" Text="Anular" ToolTip="Anular" CssClass="clsBtnAnular"
                                                            OnClick="btnAnular_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Button ID="btnProcesar" runat="server" Text="Buscar" ToolTip="Procesar" CssClass="clsBtnProcesar"
                                                            OnClick="btnProcesar_Click" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" ToolTip="Imprimir" CssClass="clsBtnImprimir"
                                                            OnClientClick="AbrirVentanaFactura()" OnClick="btnImprimir_Click" />
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
