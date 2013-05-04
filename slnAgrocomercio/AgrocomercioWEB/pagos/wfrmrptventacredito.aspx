<%@ Page Title="Compras a Credito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmrptventacredito.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmrptventacredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setCliCod(source, eventargs) {
            document.getElementById('lblEstado').value = "CLI_SELECT";
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
        function Imprimir_Contenido(nombre) {
            var tipo = document.getElementById('lblPaso').value;
            //  alert(tipo);
            var linea = "";
            if (tipo == "Detalle") {
                var cuerpo = document.getElementById('DivCnt_detalle');
                cuerpo.style.display = "";
            }
            var Ventana_Impresion = window.open(' ', 'popimpr', "left=10,top=10");
            Ventana_Impresion.document.write(cuerpo.innerHTML);
            Ventana_Impresion.document.close();
            Ventana_Impresion.print();
            Ventana_Impresion.close();
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 800px; margin: 0 auto;">
                <tr>
                    <td colspan="2">
                        <h2 class="clsTituloInterior">
                            REPORTE DE COMPRAS AL CREDITO DE AGROCOMERCIO S.R.L.</h2>
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
                                                            <td colspan=3 align="right">Tipo Cambio: </td>
                                                            <td align="left">
                                                                <asp:Label ID="lbltc" runat="server" Text="Label" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="clsCellTituloDatos2">
                                                                    Vendedor:
                                                                </td>
                                                                <td class="clsCellDatos2" colspan="3">
                                                                    <asp:DropDownList ID="ddlListaVendedores" runat="server" Width="250px" DataTextField="PerNombres"
                                                                        DataValueField="PerCod">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="clsCellTituloDatos2" align="right">
                                                                    Cliente:
                                                                </td>
                                                                <td class="clsCellDatos2" colspan="3">
                                                                    <asp:DropDownList ID="ddlCliente" runat="server" Width="250px" AutoPostBack="True"
                                                                        DataTextField="CliNombre" DataValueField="CliCod" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"
                                                                        Style="display: none;">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtCliente" runat="server" Width="270px"></asp:TextBox>
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
                                                                    Moneda:
                                                                </td>
                                                                <td >
                                                                    <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Width="100px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    Tipo de Pago:
                                                                </td>
                                                                <td >
                                                                    <asp:DropDownList ID="ddlTipPago" runat="server" AutoPostBack="True" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Width="100px">
                                                                        <asp:ListItem>Ambos</asp:ListItem>
                                                                        <asp:ListItem>Contado</asp:ListItem>
                                                                        <asp:ListItem>Credito</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="4">
                                                                    Fecha de Registro:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Desde:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecRegDesde" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecRegDesde">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td align="left">
                                                                    Hasta:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecRegHasta" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecRegHasta">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="4">
                                                                    Fecha de ultimo pago:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Desde:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecPagDesde" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecPagDesde">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td align="left">
                                                                    Hasta:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecPagHasta" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecPagHasta">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="4">
                                                                    Fecha del proximo vencimiento:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Desde:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecVenDesde" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecVenDesde">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                                <td align="left">
                                                                    Hasta:
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtFecVenHasta" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="TxtFecVenHasta">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    Estado:
                                                                </td>
                                                                <td class="style8" >
                                                                    <asp:DropDownList ID="dllEstado" runat="server">
                                                                        <asp:ListItem Value="P">Pendiente</asp:ListItem>
                                                                        <asp:ListItem Value="C">Cancelado</asp:ListItem>
                                                                        <asp:ListItem Value="A">Ambas</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="clsCellTituloDatos2" >Tipo de Documento: </td>
                                            <td class="clsCellDatos2" >
                                                <asp:DropDownList ID="ddlTipoDocu" runat="server" Width="250px" 
                                                    DataTextField="AtrDescripcion" DataValueField="AtrCodigo">
                                                            </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="4">
                                                                    <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                        CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvLista_RowDataBound">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="tipPag" HeaderText="Tip.Pago" SortExpression="tipPag"/>
                                                                            <asp:BoundField DataField="tipdoc" HeaderText="Doc." SortExpression="tipdoc"/>
                                                                            <asp:BoundField DataField="icodletra" HeaderText="icodletra" SortExpression="icodletra" visible="false"/>
                                                                            <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor"  visible="false"/>
                                                                            <asp:BoundField DataField="dfecemision" HeaderText="Fec. Emision" SortExpression="dfecemision"  DataFormatString="{0:yyyy-MM-dd}" />
                                                                            <asp:BoundField DataField="dfecultpag" HeaderText="Fec. Ult. Pago" SortExpression="dfecultpag"  DataFormatString="{0:yyyy-MM-dd}" />
                                                                            <asp:BoundField DataField="dfecnxtvct" HeaderText="Fec. Prox. Vnct" SortExpression="dfecnxtvct"  DataFormatString="{0:yyyy-MM-dd}" />                                                                       
                                                                            <asp:BoundField DataField="dopCod" HeaderText="dopCod" ReadOnly="True" SortExpression="dopCod"  visible="false"/>
                                                                            <%--8--%>
                                                                            <asp:BoundField DataField="OpeCod" HeaderText="OpeCod" ReadOnly="True" SortExpression="OpeCod"  visible="false"/>
                                                                            <asp:BoundField DataField="numfac" HeaderText="N° Factura" SortExpression="numfac" />
                                                                            <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" SortExpression="PrvRazon" />
                                                                            <asp:BoundField DataField="opetotpagar" HeaderText="Total." SortExpression="opetotpagar" />
                                                                            <asp:BoundField DataField="OpeTotal" HeaderText="OpeTotal" SortExpression="OpeTotal" visible="false"/>
                                                                            <%--13--%>
                                                                            <asp:BoundField DataField="OpeTotPagPen" HeaderText="Saldo." SortExpression="OpeTotPagPen" />
                                                                            <asp:BoundField DataField="nsalvenc" HeaderText="Sal x Venc" SortExpression="nsalvenc" />
                                                                            <asp:BoundField DataField="PrvCod" HeaderText="PrvCod" SortExpression="PrvCod"  visible="false"/>
                                                                            <asp:BoundField DataField="moneda" HeaderText="Mon." SortExpression="moneda" />
                                                                            <asp:BoundField DataField="opeestado" HeaderText="opeestado" SortExpression="opeestado"  visible="false"/>
                                                                            <asp:BoundField DataField="estado" HeaderText="Estado" ReadOnly="True" SortExpression="estado" />
                                                                            <asp:BoundField DataField="tra" HeaderText="Vendedor" ReadOnly="True" SortExpression="tra" />
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
                                                            <tr>
                                                            <td colspan=4>
                                                                <asp:Panel ID="pntotales" runat="server" BorderStyle="Inset" BorderColor="#006600">
                                                                    <table class="tabPedidos">
                                                                    <tr> <td colspan="2" align="right"> Total 
                                                                    Compra-Credito   </td>
                                                                    <td align="center"> Saldo 
                                                                    Total</td>
                                                                    <td align="center"> Saldo 
                                                                    X Vencer</td>
                                                                    <td align="center"> Saldo 
                                                                    Vencido</td>
                                                                    </tr> 
                                                                    <tr><td> Total Soles </td>
                                                                    <td align="right"><asp:Label ID="lblcompras" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td  align="right"><asp:Label ID="lblsaltots" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalxvens" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalvens" runat="server" Text="Label"></asp:Label> </td>
                                                                    </tr>
                                                                    <tr><td> Total Dolares </td>
                                                                    <td align="right"><asp:Label ID="lblcomprad" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsaltotd" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalxvend" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalvend" runat="server" Text="Label"></asp:Label> </td>
                                                                    </tr>
                                                                    <tr><td> Total Deuda</td>
                                                                    <td align="right"><asp:Label ID="lblcompra" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsaltot" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalxven" runat="server" Text="Label"></asp:Label> </td>
                                                                    <td align="right"><asp:Label ID="lblsalven" runat="server" Text="Label"></asp:Label> </td>
                                                                    </tr>
                                                                    </Table>
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
                                                        Width="106px" ToolTip="Anular" OnClick="btnAnular_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">
                                                    <asp:Button ID="btnProcesar" runat="server" Text="Buscar" CssClass="clsBtnProcesar"
                                                        OnClick="btnProcesar_Click" Height="41px" ToolTip="Procesar" />
                                                </td>
                                                <td valign="top" class="style1">
                                                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="clsBtnImprimir"
                                                        Height="38px" ToolTip="Imprimir" OnClientClick="Imprimir_Contenido()" OnClick="btnImprimir_Click" />
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
