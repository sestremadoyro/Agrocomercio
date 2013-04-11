<%@ Page Title=".:Pago Letras:." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmLstPagReal.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmLstPagReal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function dgvDetalleVentaClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVCOM_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        } function dgvListNotasClickEvent(event, rowindex) {

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
            var cabecera = document.getElementById('titulo');
            var cuerpo = document.getElementById('DivContenido');
            var linea = "";
            var Ventana_Impresion = window.open(' ', 'popimpr', "left=10,top=10");
            Ventana_Impresion.document.write(cabecera.innerHTML);
            Ventana_Impresion.document.write(cuerpo.innerHTML);
            Ventana_Impresion.document.close();
            Ventana_Impresion.print();
            Ventana_Impresion.close();
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 45px;
        }
        .style2
        {
            height: 21px;
        }
        .style3
        {
            height: 48px;
        }
        .style4
        {
            width: 126px;
        }
        .style5
        {
            width: 86px;
        }
        .style6
        {
            width: 135px;
        }
        #titulo
        {
            width: 762px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 691px; margin: 0 auto;">
                <tr>
                    <td colspan="2" class="style5">
                    <div id="titulo">
                        <h2 class="clsTituloInterior">Lista de LETRAS Pagadas</h2>
                    </div>
                    
                            
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbid_detletra" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbmomento" runat="server" />
                        <asp:HiddenField ID="HiddenField4" runat="server" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style5">
                        <table class="tabLista">
                            <tr>
                                <td class="tabIzquierda" valign="top">
                                    <asp:Panel ID="pnNotas" runat="server" BackColor="White">
                                        <table class="tabFormularios">
                                            <tr>
                                                <td colspan="3">
                                                    <h3>
                                                        Filtro de busqueda:</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td class="style2">
                                                    Fecha de Pago:
                                                </td>
                                               
                                                <td class="style2">
                                                    Proveedor:
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                            <td class="style3">
                                                    <table class="fec">
                                                        <tr>
                                                            <td>
                                                                Desde:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFecDesde" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="txtFecDesde_CalendarExtender" runat="server" Enabled="True"
                                                                    Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFecDesde">
                                                                </asp:CalendarExtender>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Hasta:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFechaHasta" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="txtFechaHasta_CalendarExtender" runat="server" Enabled="True"
                                                                    Format="yyyy-MM-dd" PopupButtonID="Image2" TargetControlID="txtFechaHasta">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                                <td class="style3">
                                                    <asp:DropDownList ID="ddlProveedor" runat="server" DataTextField="PrvRazon" DataValueField="PrvCod"
                                                        AutoPostBack="true" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right">
                                                    <asp:Button ID="btnBuscar" runat="server" CssClass="clsBtnEditar" Height="41px" OnClick="btnBuscar_Click"
                                                        Text="Buscar" Width="99px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="3">
                                                
                                                    <asp:Panel ID="pnlista" runat="server" BorderColor="#549900" BorderStyle="Solid">
                                                <div id="DivContenido">        
                                                        <asp:GridView ID="dgvlistNotas" runat="server" AutoGenerateColumns="False"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Width="462px" 
                                                            OnRowDataBound="dgvListNotas_RowDataBound" BorderColor="#999999" 
                                                            BorderStyle="Solid" BackColor="#CCCCCC" BorderWidth="3px" CellPadding="4" 
                                                            CellSpacing="2" ForeColor="Black">
                                                            <Columns>
                                                                <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                <asp:BoundField DataField="idetletra" HeaderText="Cod." DataFormatString="{0:D4}" />
                                                                <asp:BoundField DataField="dfecven" HeaderText="Fec. Vcnt" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="cuota" HeaderText="cuota" />
                                                                <asp:BoundField DataField="ccodletra" HeaderText="Cod. Letra" />
                                                                <asp:BoundField DataField="cnumletra" HeaderText="Num. Letra" />
                                                                <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" />                                                                
                                                                <asp:BoundField DataField="dfecpag" HeaderText="Fec.Pago" />
                                                                <asp:BoundField DataField="cmoneda" HeaderText="Mon." />
                                                                <asp:BoundField DataField="mnt_cuota" HeaderText="Mnt a Pagar" DataFormatString="{0:C}" />
                                                                <asp:BoundField DataField="mnt_letra" HeaderText="Deuda Tot." DataFormatString="{0:C}" />
                                                                <asp:BoundField DataField="Saldo_total" HeaderText="Saldo Tot." DataFormatString="{0:C}" />
                                                                <asp:BoundField DataField="ninteres" HeaderText="Interes." DataFormatString="{0:C}" />
                                                                <asp:BoundField DataField="PrvCod" HeaderText="Cod." DataFormatString="{0:D4}" Visible="False" />
                                                                <asp:BoundField DataField="icodletra" HeaderText="Cod." DataFormatString="{0:D4}"
                                                                    Visible="False" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registro Ninguna Letra</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle CssClass="pgr" BackColor="#CCCCCC" ForeColor="Black" 
                                                                HorizontalAlign="Left" />
                                                            <RowStyle BackColor="White" />
                                                            <SelectedRowStyle CssClass="selrow" BackColor="#000099" Font-Bold="True" 
                                                                ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                        </asp:GridView>
                                                         </div>
                                                    </asp:Panel>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                            <td align="right">
                                                &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">

                                                    <asp:Panel ID="pnPago" runat="server" BorderColor="#549900" BorderStyle="Solid">
                                                        <table class="Nuevo">
                                                            <tr>
                                                                <td class="style4" style="font-weight: bold" colspan="3">
                                                                    <h3>
                                                                        Pago de letra</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Fecha de Vcnt.:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lbldfecVcnt" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Proveedor:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblProveedor" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Cuota:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblCuota" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Monto de Letra:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblMonto" runat="server" Text="Label"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Fecha de Pago:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtFecPag" runat="server" Width="82px" Height="23px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        PopupButtonID="Image2" TargetControlID="txtFecPag">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style4" style="font-weight: bold">
                                                                    Interes u Otros:
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtinter" runat="server" Width="72px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td  align="right">
                                                                    
                                                                </td>
                                                                <td  align="right">
                                                                    <asp:Button ID="btnPagar" runat="server"  Height="41px"
                                                                        OnClick="btnPagar_Click" Text="Pagar" Width="99px" />
                                                                </td>
                                                                <td class="style6">
                                                                    <asp:Button ID="btnAnulPago" runat="server" Height="41px"
                                                                        OnClick="btnAnular_Click" Text="Anular Pago" Width="145px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3">
                                                    <asp:Panel ID="pnMensaje" runat="server" >
                                                        <asp:Label ID="lblOkMensaje" runat="server" Text="Label" Font-Bold="True" 
                                                            Font-Size="Larger" ForeColor="#009900"></asp:Label>
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
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="clsBtnNuevo" OnClick="btnNuevo_Click"
                                                            CausesValidation="False" Height="41px" Width="106px"  ToolTip="Nuevo" />
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="clsBtnGuardar"
                                                            OnClick="btnGuardar_Click" Height="41px" Width="110px"  ToolTip="Guardar" />
                                                    </td>
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="clsBtnCancelar"
                                                            OnClick="btnCancelar_Click" CausesValidation="False" Height="41px"  ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="style2">
                                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="clsBtnEditar" OnClick="btnEditar_Click"
                                                            Height="41px" Width="99px"  ToolTip="Editar" />
                                                    </td>
                                                    <td valign="top" class="style2">
                                                        <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="clsBtnAnular" Height="41px"
                                                            OnClick="btnAnular_Click" Width="101px"  ToolTip="Anular" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="clsBtnProcesar"
                                                            OnClick="btnProcesar_Click" Height="41px" ToolTip="Procesar"  />
                                                    </td>
                                                    <td valign="top" class="style1">
                                                    <input type="button" ID="btnprint" runat="server" value="Imprimir " onclick="Imprimir_Contenido()" 
                                                    Height="38px" Width="102px"  ToolTip="Imprimir" />
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="clsBtnImprimir" onclick="btnImprimir_Click" 
                                                            Height="38px" Width="102px"  ToolTip="Imprimir" />
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
