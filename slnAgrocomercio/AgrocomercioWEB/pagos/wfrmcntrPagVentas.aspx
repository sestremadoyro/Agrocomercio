<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmcntrPagVentas.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmcntrPagVentas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 45px;
        }
        .style2
        {
            height: 21px;
        }
        .style5
        {
            width: 389px;
        }
        .style6
        {
            width: 177px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 691px; margin: 0 auto;">
                <tr>
                    <td colspan="2" class="style5">
                        <h2 class="clsTituloInterior">
                            CONTROL DE PAGOS DE LAS VENTAS DE AGROCOMERCIO S.R.L.</h2>
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbid_letra" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbmomento" runat="server" />
                        <asp:HiddenField ID="lblId_Cliente" runat="server" ClientIDMode="Static" />
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
                                                <td>
                                                    Vendedor:
                                                </td>
                                                <td>
                                                    Cliente:
                                                </td>
                                                <td>
                                                    Tipo:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendedor" runat="server" DataTextField="nombre" DataValueField="perCod"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNomCli" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rbtodos" Text="Todos" runat="server" GroupName="GRUPO1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rbpendientes" Text="Pendientes" runat="server" GroupName="GRUPO1"
                                                                    Checked="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right">
                                                    <asp:Button ID="btnBuscar" runat="server" CssClass="clsBtnEditar" Height="41px" OnClick="btnBuscar_Click"
                                                        Text="Buscar" Width="99px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="pnlista" runat="server" BorderColor="#549900" BorderStyle="Solid">
                                                        <asp:GridView ID="dgvlist" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" Width="462px" OnRowDataBound="dgvList_RowDataBound">
                                                            <Columns>
                                                                <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                                                <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                                                <asp:BoundField HeaderText="Cod. Det" DataField="idetletra" DataFormatString="{0:D4}" />
                                                                <asp:BoundField HeaderText="NumLetra" DataField="inumletra" DataFormatString="{0:D4}" />
                                                                <asp:BoundField HeaderText="Fec. Vencimiento" DataField="dfecvenc" DataFormatString="{0:d}" />
                                                                <asp:BoundField HeaderText="Cod. Ven" DataField="cod_vendedor" />
                                                                <asp:BoundField HeaderText="Vendedor" DataField="vendedor" />
                                                                <asp:BoundField HeaderText="Cliente" DataField="cliente" />
                                                                <asp:BoundField HeaderText="Tip. Pag" DataField="ctippago" />
                                                                <asp:BoundField HeaderText="mnt.Total Deuda" DataField="DeudTot" DataFormatString="{0:F2}" />
                                                                <asp:BoundField HeaderText="Saldo.Total Deuda" DataField="Sal_tot" DataFormatString="{0:F2}" />
                                                                <asp:BoundField HeaderText="mnt.Cuota" DataField="mntCuot" DataFormatString="{0:F2}" />
                                                                <asp:BoundField HeaderText="Saldo Cuota" DataField="sal_cuot" DataFormatString="{0:F2}" />                                                                
                                                                <asp:TemplateField HeaderText="Monto">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtmonto" runat="server" Width="74px" Text ="0.00"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:BoundField HeaderText="codigo letra" DataField="icodletra" DataFormatString="{0:F0}" />
                                                            <asp:BoundField HeaderText="codigo letra" DataField="ult" DataFormatString="{0:F0}" />
                                                            <asp:BoundField HeaderText="codigo letra" DataField="cod_cliente" DataFormatString="{0:F0}" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1" runat="server">
                                                                        No se ha Registro Deuda con estas caracteristicas</div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp; &nbsp;
                                                    <asp:Panel ID="pncargar" runat="server" BorderColor="#549900" 
                                                        BorderStyle="Solid">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    Fecha de pago:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFec" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFec_CalendarExtender" runat="server" 
                                                                        Enabled="True" Format="yyyy-MM-dd" PopupButtonID="Image2" 
                                                                        TargetControlID="txtFec">
                                                                    </asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnpagar" runat="server" CssClass="clsBtnEditar" Height="41px" OnClick="btnPagar_Click"
                                                        Text="Pagar" Width="99px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
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
                                                            CausesValidation="False" Height="41px" Width="106px" />
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="clsBtnGuardar"
                                                            OnClick="btnGuardar_Click" Height="41px" Width="110px" />
                                                    </td>
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="clsBtnCancelar"
                                                            OnClick="btnCancelar_Click" CausesValidation="False" Height="41px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="style2">
                                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="clsBtnEditar" OnClick="btnEditar_Click"
                                                            Height="41px" Width="99px" />
                                                    </td>
                                                    <td valign="top" class="style2">
                                                        <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="clsBtnAnular" Height="41px"
                                                            OnClick="btnAnular_Click" Width="101px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="clsBtnProcesar"
                                                            OnClick="btnProcesar_Click" Height="41px" />
                                                    </td>
                                                    <td valign="top" class="style1">
                                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="clsBtnImprimir"
                                                            Height="38px" Width="102px" />
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

