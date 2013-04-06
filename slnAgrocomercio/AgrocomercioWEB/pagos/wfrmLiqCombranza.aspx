<%--prueba de cambio 06042013--%>
<%@ Page Title=".:Liquidacion Cobranza:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmLiqCombranza.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmLiqCombranza" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function setCliCod(source, eventargs) {
            document.getElementById('lblEstado').value = "CLI_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
        }
        function dgvNotasClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVLIS_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }

        function AbrirVentanaReporte() {

            var cuerpo = document.getElementById('DivContenido');
            var linea = "";
            var Ventana_Impresion = window.open(' ', 'popimpr', "left=10,top=10");

            Ventana_Impresion.document.write(cuerpo.innerHTML);
            Ventana_Impresion.document.close();
            Ventana_Impresion.print();
            Ventana_Impresion.close();
        }
    </script>
    <style type="text/css" media="print"> 
div.page {  
writing-mode: tb-rl; 
height: 80%; 
margin: 10% 0%; 
} 
</style> 
    <style type="text/css">
        .style1
        {
            text-align: right;
            font-size: 11px;
            height: 30px;
        }
        .style2
        {
            text-align: left;
            font-size: 11px;
            font-weight: bold;
            color: #333333;
            height: 30px;
        }
        .style3
        {
            text-align: right;
            font-size: 11px;
            height: 33px;
        }
        .style4
        {
            text-align: left;
            font-size: 11px;
            font-weight: bold;
            color: #333333;
            height: 33px;
        }
        .style5
        {
            text-align: right;
            font-size: 11px;
            height: 36px;
        }
        .style6
        {
            text-align: left;
            font-size: 11px;
            font-weight: bold;
            color: #333333;
            height: 36px;
        }
        .style12
        {
            height: 190px;
        }
        .style15
        {
            height: 6px;
        }
        .style17
        {
            height: 7px;
        }
        .style18
        {
            height: 9px;
        }
        .style24
        {
            text-align: center;
            font-size: 11px;
            font-weight: bold;
            color: #333333;
            height: 6px;
        }
        .style25
        {
            height: 30px;
        }
        .style26
        {
            height: 35px;
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
                            LIQUIDACION DE COBRANZA DE AGROCOMERCIO S.R.L.</h2>
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
                                    <asp:Panel ID="pnLiquidaciones" runat="server" BackColor="White">
                                        <table class="tableIzquierda">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnBusqueda" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px">
                                                        <h3>
                                                            Filtro de Busqueda:</h3>
                                                        <table class="tableIzquierda">
                                                            <tr>
                                                                <td class="clsCellTituloDatos2" align="right">
                                                                    Tipo:
                                                                </td>
                                                                <td class="clsCellDatos2">
                                                                    <asp:RadioButton ID="rbtResumido" runat="server" Text="Resumido" GroupName="GRUPO1"
                                                                        Checked="true" AutoPostBack="true" />
                                                                    <asp:RadioButton ID="rbtDetallado" runat="server" Text="Detallado" GroupName="GRUPO1"
                                                                        AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="right">
                                                                    Moneda:
                                                                </td>
                                                                <td class="style2">
                                                                    <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True" DataTextField="AtrDescripcion"
                                                                        DataValueField="AtrCodigo" Width="100px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style3">
                                                                    Vendedor:
                                                                </td>
                                                                <td class="style4">
                                                                    <asp:DropDownList ID="ddlListaVendedores" runat="server" Width="250px" DataTextField="PerNombres"
                                                                        DataValueField="PerCod" AutoPostBack="True" OnSelectedIndexChanged="ddlListaVendedores_SelectedIndexChanged">
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
                                                                    <div id="ClilistPlacement" class="cls_listPlacement" style="overflow: auto; margin-top: 17px;">
                                                                    </div>
                                                                    <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" MinimumPrefixLength="2"
                                                                        TargetControlID="txtCliente" EnableCaching="true" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                        CompletionSetCount="10" CompletionInterval="100" ServiceMethod="BuscarClientes"
                                                                        runat="server" OnClientItemSelected="setCliCod" CompletionListElementID="ClilistPlacement">
                                                                    </asp:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    _____________________________________________________________________
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Panel ID="pnResumindo" runat="server" BackColor="White">
                                                                        <h4>
                                                                            Lista de Cobranza Resumida</h4>
                                                                        <asp:GridView ID="dgvListCobranza" runat="server" AutoGenerateColumns="False" DataKeyNames="icodletra"
                                                                            GridLines="None" CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="TraNombre" HeaderText="Vendedor" SortExpression="TraNombre" />
                                                                                <asp:BoundField DataField="numDoc" HeaderText="Num. Letra" SortExpression="numDoc" />
                                                                                <asp:BoundField DataField="fac_acum" HeaderText="# Doc." SortExpression="fac_acum" />
                                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Cliente" SortExpression="PrvRazon" />
                                                                                <asp:BoundField DataField="FecEmesion" HeaderText="Fec. Generacion" SortExpression="FecEmesion"
                                                                                    DataFormatString="{0:yyyy-MM-dd}" />
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="fec_nxtpg" HeaderText="Fec.Prox. Vncto" SortExpression="fec_nxtpg"
                                                                                    DataFormatString="{0:yyyy-MM-dd}" />
                                                                                <asp:BoundField DataField="cmoneda" HeaderText="Moneda" SortExpression="cmoneda" />
                                                                                <asp:BoundField DataField="nmontocuota" HeaderText="Monto Total" SortExpression="nmontocuota" />
                                                                                <asp:BoundField DataField="pagpendiente" HeaderText="Saldo" SortExpression="pagpendiente" />
                                                                                <asp:TemplateField HeaderText="Pago">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtmonto" runat="server" Width="74px" Text=""></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--10--%>
                                                                                <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" />
                                                                                <asp:BoundField DataField="codPer" HeaderText="codPer" SortExpression="codPer" />
                                                                                <asp:BoundField DataField="itotcuota" HeaderText="itotcuota" SortExpression="itotcuota" />
                                                                                <asp:BoundField DataField="nintpag" HeaderText="nintpag" SortExpression="nintpag" />
                                                                                <asp:BoundField DataField="estado" HeaderText="estado" SortExpression="estado" />
                                                                                <asp:BoundField DataField="dfecultpago" HeaderText="dfecultpago" SortExpression="dfecultpago" />
                                                                                <asp:BoundField DataField="icodletra" HeaderText="icodletra" ReadOnly="True" SortExpression="icodletra" />
                                                                                <asp:BoundField DataField="ctippago" HeaderText="ctippago" SortExpression="ctippago" />
                                                                                <%--18--%>
                                                                                <asp:BoundField DataField="PerCod" HeaderText="PerCod" SortExpression="PerCod" />
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <i>
                                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                                        No Presenta Deuda
                                                                                    </div>
                                                                                </i>
                                                                            </EmptyDataTemplate>
                                                                            <PagerStyle CssClass="pgr" />
                                                                            <SelectedRowStyle CssClass="selrow" />
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Panel ID="pnDetallado" runat="server">
                                                                        <h4>
                                                                            Lista de Cobranza Detallada</h4>
                                                                        <asp:GridView ID="dgvListCobDet" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True">
                                                                            <AlternatingRowStyle CssClass="alt" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="TraNombre" HeaderText="Vendedor" ReadOnly="True" SortExpression="TraNombre" />
                                                                                <asp:BoundField DataField="numDoc" HeaderText="Letra" ReadOnly="True" SortExpression="numDoc" />
                                                                                <asp:BoundField DataField="numfac" HeaderText="Documento" ReadOnly="True" SortExpression="numfac" />
                                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Cliente" SortExpression="PrvRazon" />
                                                                                <asp:BoundField DataField="dopFecEmision" HeaderText="Fec. Emision" ReadOnly="True"
                                                                                    SortExpression="dopFecEmision" DataFormatString="{0:yyyy-MM-dd}" />
                                                                                <asp:BoundField DataField="fec_nxtpg" HeaderText="Fec.Prox Pago" ReadOnly="True"
                                                                                    SortExpression="fec_nxtpg" DataFormatString="{0:yyyy-MM-dd}" />
                                                                                <asp:BoundField DataField="cmoneda" HeaderText="Moneda" ReadOnly="True" SortExpression="cmoneda" />
                                                                                <asp:BoundField DataField="OpeTotPagar" HeaderText="Total" ReadOnly="True" SortExpression="OpeTotPagar" />
                                                                                <asp:BoundField DataField="saldo" HeaderText="saldo" ReadOnly="True" SortExpression="saldo" />
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <i>
                                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                                        No Presenta Deuda
                                                                                    </div>
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
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="pnLiquidacion" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                                        Style="margin-bottom: 0px">
                                                        <div id="DivContenido"  class="page">
                                                        <h3>
                                                            Liquidacion de Cobranza
                                                        </h3>
                                                        <table width="100%" border="1" cellpadding="0" cellspacing="1" bordercolor="#000000"
                                                            style="border-collapse: separate;">
                                                            <tr>
                                                                <td align="right" class="style3">
                                                                    Vendedor
                                                                </td>
                                                                <td class="style4">
                                                                    <asp:Label ID="lblnomVen" runat="server" Text="Nombre Vendedor..."></asp:Label>
                                                                </td>
                                                                <td align="right" class="style3">
                                                                    Fecha Registro:
                                                                </td>
                                                                <td class="style4" colspan="4">
                                                                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecha">
                                                                    </asp:CalendarExtender>
                                                                    (*)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style3">
                                                                    Fecha del:
                                                                </td>
                                                                <td class="style4">
                                                                    <asp:TextBox ID="txtFecMin" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecMin_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecMin">
                                                                    </asp:CalendarExtender>
                                                                    (*)
                                                                </td>
                                                                <td align="right" class="style5">
                                                                    al:
                                                                </td>
                                                                <td class="style6" colspan="4">
                                                                    <asp:TextBox ID="txtFecMax" runat="server" ReadOnly="True" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecMax_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecMax">
                                                                    </asp:CalendarExtender>
                                                                    (*)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style12" colspan="7">
                                                                    
                                                                    <asp:GridView ID="dgvPagoLetra" runat="server" AutoGenerateColumns="False" CssClass="mGrid mGrid2"
                                                                        DataKeyNames="icodletra" ShowHeaderWhenEmpty="True" >
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="vendedor" HeaderText="vendedor" SortExpression="vendedor" />
                                                                            <asp:BoundField DataField="ccodletra" HeaderText="ccodletra" SortExpression="ccodletra"
                                                                                Visible="false" />
                                                                            <asp:BoundField DataField="cnumletra" HeaderText="cnumletra" SortExpression="cnumletra"
                                                                                Visible="false" />
                                                                            <asp:BoundField DataField="cnumcuota" HeaderText="Cuota" SortExpression="cnumcuota" />
                                                                            <asp:BoundField DataField="CliNombre" HeaderText="Cliente" SortExpression="CliNombre" />
                                                                            <%--5--%>
                                                                            <asp:BoundField DataField="OpeFecEmision" HeaderText="Fec.Emision" SortExpression="OpeFecEmision"
                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                            <asp:BoundField DataField="dfecvenc" HeaderText="Fec. Vencimiento" SortExpression="dfecvenc"
                                                                                DataFormatString="{0:yyyy-MM-dd}" />
                                                                            <asp:BoundField DataField="dfecpago" HeaderText="dfecpago" SortExpression="dfecpago"
                                                                                DataFormatString="{0:yyyy-MM-dd}" Visible=false />
                                                                            <asp:BoundField DataField="diaatr" HeaderText="Dias Atraso" SortExpression="diaatr" />
                                                                            <asp:BoundField DataField="cmoneda" HeaderText="Mon." SortExpression="cmoneda" />
                                                                            <%--10--%>
                                                                            <asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" />
                                                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" SortExpression="saldo" 
                                                                                ItemStyle-HorizontalAlign="Right" >
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="PAG_TOTAL" HeaderText="PAG_TOTAL" 
                                                                                SortExpression="PAG_TOTAL" ItemStyle-HorizontalAlign="Right"
                                                                                Visible="false" >
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="mnt_cuota" HeaderText="mnt_cuota" 
                                                                                SortExpression="mnt_cuota" ItemStyle-HorizontalAlign="Right"
                                                                                Visible="false" >
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="saldo_cuota" HeaderText="Saldo Cuota" 
                                                                                SortExpression="saldo_cuota" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <%--15--%>
                                                                            <asp:TemplateField HeaderText="Pago(*)" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtmonto" runat="server" 
                                                                                        Text="0.00" Width="74px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Num. Recibo(*)">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtnumrecibo" 
                                                                                        runat="server" Text="" Width="74px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Tip. Pago">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chktipPago" runat="server" Text="Efectivo" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Fec. Pago(*)">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtFecPago" runat="server" ReadOnly="True" Width="100px"
                                                                                        ></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="txtFecPago_CalendarExtender" runat="server" Enabled="True"
                                                                                        Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecPago">
                                                                                    </asp:CalendarExtender>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Observaciones">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtobsdetalle" runat="server" Text="" Width="74px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--20--%>
                                                                            <asp:BoundField DataField="pag_cuota" HeaderText="pag_cuota" SortExpression="pag_cuota"
                                                                                Visible="false" />
                                                                            <asp:BoundField DataField="ninteres" HeaderText="ninteres" SortExpression="ninteres"
                                                                                Visible="false" />
                                                                            <asp:BoundField DataField="traCod" HeaderText="traCod" SortExpression="traCod" />
                                                                            <asp:BoundField DataField="OpeTipo" HeaderText="OpeTipo" SortExpression="OpeTipo" />
                                                                            <asp:BoundField DataField="cestado" HeaderText="cestado" SortExpression="cestado" />
                                                                            <%--25--%>
                                                                            <asp:BoundField DataField="idetletra" HeaderText="idetletra" ReadOnly="True" SortExpression="idetletra" />
                                                                            <asp:BoundField DataField="icodletra" HeaderText="icodletra" ReadOnly="True" SortExpression="icodletra" />
                                                                            <asp:BoundField DataField="inumletra" HeaderText="inumletra" ReadOnly="True" SortExpression="inumletra" />
                                                                            <asp:BoundField DataField="cli_cod" HeaderText="cli_cod" SortExpression="cli_cod" />
                                                                            <asp:BoundField DataField="ult" HeaderText="ult" ReadOnly="True" SortExpression="ult" />
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <i>
                                                                                <div id="lblError1" runat="server" class="clsError1">
                                                                                    No Presenta Deuda
                                                                                </div>
                                                                            </i>
                                                                        </EmptyDataTemplate>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <SelectedRowStyle CssClass="selrow" />
                                                                    </asp:GridView>
                                                                    <h3>
                                                                        (*) recuerda que no se tomara en cuenta los pagos sin Num.Recibo ni Fecha de Pago</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style15" colspan="3">
                                                                </td>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    S/.
                                                                </td>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    $.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="3" style="border: thin solid #000000;" class="style25">
                                                                    Subtotal:
                                                                </td>
                                                                <td align="right" style="border: thin solid #000000;" class="style25">
                                                                    <asp:Label ID="lblSubSoles" runat="server" Font-Size="Medium" Text="0.00"></asp:Label>
                                                                </td>
                                                                <td align="right" style="border: thin solid #000000;" class="style25">
                                                                    <asp:Label ID="lblSubDolares" runat="server" Font-Size="Medium" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" class="style26">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="style24" colspan="2">
                                                                    RESUMEN
                                                                </td>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    S/.
                                                                </td>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    $.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    INGRESOS
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    Cobros de Letras:
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblCobEfecSol" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblCobEfecDol" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style18" style="border: thin solid #000000;">
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    Entregas a cta.Viaticos:
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtctaViasoles" Style="text-align: right" Text="0.00" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtctaViadolares" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style24" style="border: thin solid #000000;" colspan="2" align="right">
                                                                    SUBTOTALES
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblingsoles" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblingdolares" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style24" style="border: thin solid #000000;">
                                                                    EGRESOS (MENOS):
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    Cobros sin efectivo:
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                     <asp:Label ID="lblCobSnefeSol" runat="server" Text="0.00"></asp:Label></td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                     <asp:Label ID="lblCobSnefeDol" runat="server" Text="0.00"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style18" style="border: thin solid #000000;">
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    Gastos Varios:
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtGasVSol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtGasVDol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style18" style="border: thin solid #000000;">
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    Entrega Cheques y depositos:
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtchqSol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtchqDol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style18" style="border: thin solid #000000;">
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    Entregas Efectivo:
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtegrEfeSol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                                <td align="right" class="style18" style="border: thin solid #000000;">
                                                                    <asp:TextBox ID="txtegrEfeDol" Text="0.00" Style="text-align: right" runat="server"
                                                                        AutoPostBack="true" OnTextChanged="txtmonto_TextChange"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style24" style="border: thin solid #000000;" colspan="2" align="right">
                                                                    SUBTOTALES
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblegrSoles" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblegrDolares" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" class="style26">
                                                                </td>
                                                                <td>
                                                                    Firma Vendedor
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style24" style="border: thin solid #000000;" colspan="2" align="right">
                                                                    SALDOS
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblSalSoles" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                                <td align="right" class="style17" style="border: thin solid #000000;">
                                                                    <asp:Label ID="lblSalDolares" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td class="style24" style="border: thin solid #000000;"  align="right">Observaciones:</td>
                                                            <td colspan=3 align=left>
                                                                <asp:TextBox ID="txtObservaciones" runat="server" Height="43px" 
                                                                    TextMode="MultiLine" Width="514px"></asp:TextBox>

                                                            </td>
                                                            </tr>
                                                            </caption>
                                                        </table>
                                                        </div>
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
                                                        <asp:Button ID="btnImprimir" runat="server" CssClass="clsBtnImprimir" Height="38px"
                                                            OnClick="btnImprimir_Click" Text="Imprimir" ToolTip="Imprimir"  OnClientClick="AbrirVentanaReporte()" />
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
