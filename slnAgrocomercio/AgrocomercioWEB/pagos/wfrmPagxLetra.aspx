<%@ Page Title=".:Registro de Letras:." Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="wfrmPagxLetra.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmPagxLetra"
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
        function Imprimir_Contenido(nombre) {

            var cuerpo = document.getElementById('DivContenido');
            var linea = "";
            var Ventana_Impresion = window.open(' ', 'popimpr', "left=10,top=10");

            Ventana_Impresion.document.write(cuerpo.innerHTML);
            Ventana_Impresion.document.close();
            Ventana_Impresion.print();
            Ventana_Impresion.close();
        }
    </script>

    <style type="text/css">
        .style3
        {
            height: 37px;
        }
        .style4
        {
            width: 15%;
        }
        .style5
        {
            text-align: right;
            font-size: 11px;
            width: 15%;
        }
        .style7
        {
            text-align: left;
            font-size: 11px;
            font-weight: bold;
            color: #333333;
            width: 268435328px;
        }
        .style9
        {
            width: 299px;
        }
        .style10
        {
            height: 52px;
        }
        .style12
        {
            width: 522px;
        }
        .style13
        {
            width: 268435328px;
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
                            LETRAS DE AGROCOMERCIO S.R.L.</h2>
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
                                <td class="style9" valign="top">
                                    <asp:Panel ID="pnListOperLetras" runat="server" Width="668px">
                                        <table class="tableIzquierda">
                                            <tr>
                                                <td class="style12">
                                                    <h3>
                                                        Lista de Letras</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style12">
                                                    <asp:Panel ID="pnListOperaciones" runat="server" Height="320px" ScrollBars="Auto">
                                                        <asp:GridView ID="dgvListOperLetras" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvListOperLetras_RowDataBound">
                                                            <Columns>
                                                                <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                <asp:BoundField DataField="numDoc" HeaderText="Codigo Oper." ConvertEmptyStringToNull="False"
                                                                    NullDisplayText=" ">
                                                                    <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fac_acum" HeaderText="Fac. Acum">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FecEmesion" HeaderText="Fec.Emesion" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fec_nxtpg" HeaderText="Fec. Prox. Pago" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor">
                                                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="itotcuota" HeaderText="Num. Cuotas">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="nmontocuota" HeaderText="Importe Total">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="pagpendiente" HeaderText="Importe Pendiente">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="nintpag" HeaderText="Interes Pagado">
                                                                    <ItemStyle Width="20px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registro Ninguna Letra</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle CssClass="pgr" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr><td class="style12"></td>                                            </tr>
                                            <tr><td class="style12"></td>                                            </tr>
                                            <tr><td class="style12"></td>                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <div id="DivContenido">
                                        <asp:Panel ID="pnDocLetra" runat="server" Height="967px" Width="610px">
                                            <table class="tableIzquierda">
                                                <tr>
                                                    <td style="width: 20%;">
                                                    </td>
                                                    <td style="width: 45%;" colspan="3">
                                                    </td>
                                                    <td class="style4">
                                                    </td>
                                                    <td class="style13">
                                                        <asp:HiddenField ID="lblTipoDoc" runat="server" />
                                                        <asp:HiddenField ID="lbldopCod" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2">
                                                        Fecha:
                                                    </td>
                                                    <td class="clsCellDatos2" colspan="2">
                                                        <asp:TextBox ID="txtFecha" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" TargetControlID="txtFecha"
                                                            Format="yyyy-MM-dd" PopupButtonID="Image2" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td colspan="3" align="right">
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
                                                    <td class="clsCellTituloDatos2">
                                                        Proveedor:
                                                    </td>
                                                    <td class="clsCellDatos2" colspan="3">
                                                        <asp:Label ID="lblProveedor" runat="server" Text="NombreProveedor.."></asp:Label>
                                                    </td>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style7">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2">
                                                        Numero de Cuotas:
                                                    </td>
                                                    <td class="clsCellDatos2" colspan="3">
                                                        <asp:TextBox ID="txtNumCuotas" runat="server" Width="33px" OnTextChanged="txtnumCuotas_TextChange"
                                                            AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="style10">
                                                        &nbsp;&nbsp;
                                                        <asp:HiddenField ID="lblDescuentoEsp" runat="server" Value="0.0" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnaddfactura" runat="server" Width="472px">
                                                            <asp:Panel ID="Pnokaddfactura" runat="server" Font-Bold="True" 
                                                                ForeColor="#006600" Width="590px">
                                                                <h3>
                                                                    ENLAZAR FACTURAS A LA LETRA</h3>
                                                                <asp:GridView ID="dgvListFact" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                    CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Style="margin-top: 0px; padding-top: 0px;"
                                                                    PageSize="5" Width="233px" OnSelectedIndexChanged="dgvListFact_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" OnCheckedChanged="grdchk_CheckedChanged"
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
                                                                        <asp:BoundField DataField="opetotpagar" HeaderText="S/.">
                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Monto Facturado:
                                                                <asp:Label ID="lbSuma" runat="server" Text="0.00"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Pnerraddfactura" runat="server" Width="456px">
                                                                <asp:Label ID="nofactpend" runat="server" Text="NO EXISTEN FACTURAS PENDIENTES DE REGISTRO DE LETRA PARA ESTE PROVEEDOR"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnNotas" runat="server" Width="471px">
                                                            <asp:Panel ID="pnaddNotas" runat="server" Font-Bold="True" ForeColor="#006600" 
                                                                Width="460px">
                                                                <h3>
                                                                    ENLAZAR Notas de credito al pago</h3>
                                                                <asp:GridView ID="dgvNotas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                    CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Style="margin-top: 0px; padding-top: 0px;"
                                                                    PageSize="5" Width="233px" OnSelectedIndexChanged="dgvListFact_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="grdchknota" runat="server" Checked="true" OnCheckedChanged="grdchknota_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="dfecreg" HeaderText="Fec. Nota" DataFormatString="{0:d}">
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
                                                                        <asp:BoundField DataField="cestadoNota" HeaderText="Estado">
                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="nmntutilizado" HeaderText="Monto Disponible">
                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Monto Deducido:
                                                                <asp:Label ID="lbresta" runat="server" Text="0.00"></asp:Label>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnnotasempty" runat="server" Width="456px">
                                                                <asp:Label ID="lblemptynot" runat="server" Text="No hay Notas de credito disponibles para este Proveedor."
                                                                    Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="right">
                                                        &nbsp;<asp:Label ID="Label4" runat="server" Text="TOTAL A PAGAR:" Font-Bold="True"
                                                            Font-Size="Medium" ForeColor="#006600"></asp:Label>
                                                        <asp:Label ID="lbpago" runat="server" Text="0.00" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#006600"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="right" class="style3">
                                                        <asp:Button ID="btgenCuotas" runat="server" Text="Generar" CssClass="clsBtnAgregar"
                                                            OnClick="btnGenerar_Click" Visible="false" Height="41px" Width="105px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="pnCuotas" runat="server">
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
                                                                            <asp:TextBox ID="num_let" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cod.Unico">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="cod_unic" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Monto">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="monto" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>                                                
                                            </table>
                                        </asp:Panel>
                                    </div>
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
                                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="clsBtnNuevo" OnClick="btnNuevo_Click"
                                                            CausesValidation="False" ToolTip ="Nuevo"  />
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="clsBtnGuardar"
                                                            OnClick="btnGuardar_Click"  ToolTip ="Guardar" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="clsBtnCancelar"
                                                            OnClick="btnCancelar_Click" CausesValidation="False" Height="41px"  ToolTip ="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" >
                                                        <asp:Button ID="btnEditar" runat="server" Text="Detalle" CssClass="clsBtnEditar" OnClick="btnEditar_Click"
                                                            Height="41px" Width="99px" ToolTip ="Editar"  />
                                                        <input type="button" ID="btnprint" runat="server" value="Imprimir " onclick="Imprimir_Contenido()"  ToolTip ="Imprimir" />
                                                    </td>
                                                    <td valign="top" >
                                                        <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="clsBtnAnular" Height="41px"
                                                            Width="106px" ToolTip ="Anular"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" >
                                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="clsBtnProcesar"
                                                            OnClick="btnProcesar_Click" Height="41px"  ToolTip ="Procesar" />
                                                    </td>
                                                    <td valign="top" >
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="clsBtnImprimir"
                                                            Height="38px" ToolTip ="Imprimir"  />
                                                    </td>
                                                    <td valign="top" >
                                                        &nbsp;</td>
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
