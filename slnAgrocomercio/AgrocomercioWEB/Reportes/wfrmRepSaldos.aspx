<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmRepSaldos.aspx.cs" 
Inherits="AgrocomercioWEB.Reportes.rpt.wfrmRepSaldos" %>
<%@ Import Namespace="pryAgrocomercioDAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">

    function printGrid2() {
        var div, imp;
        
        div = document.getElementById("divGridView"); //seleccionamos el objeto
        //imp = window.open(" ", "Impresion de Reporte", 'toolbar=0,scrollbars=0,status=0'); //damos un titulo
        imp = window.open(' ', '_blank', 'width=880px,height=600px,toolbar=0,scrollbars=0,status=0'); //damos un titulo
        imp.document.write('<HTML>\n<HEAD>\n<link href="gridview.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<BODY>\n'); //tambien podriamos agregarle un <link ...
        imp.document.write(div.innerHTML + '\n</BODY>\n</HTML>'); //agregamos el objeto
        imp.document.close();
        imp.print();   //Abrimos la opcion de imprimir
        imp.close(); //cerramos la ventana nueva

    }
    /*
        function AbrirVentanaImprimeSaldo() {
            window.open('ImpresionSaldos.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
        }
        */
</script>
    <style type="text/css">
        .style1
        {
            width: 600px;
        }
        .style2
        {
            width: 600px;
        }
        .style4
        {
            width: 600px;
        }
        .style5
        {
            width: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


 <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 680px;">
                <tr>
                    <td colspan="1">
                        <table style="width: 680px; margin: 0 auto;">
                            <tr>
                                <td class="style4" valign="top">
                                    <asp:Panel ID="pnListOperCompras" runat="server" Width="720px">
                                        <table style="width: 680px;">
                                            <tr>
                                                <td colspan="1">
                                                    <h2 class="clsTituloInterior">
                                                        Reporte de Saldos</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5" colspan="1">
                                                    <asp:Label ID="Label1" runat="server" Text="Eliga un Reporte: " Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    <asp:RadioButton ID="rbResumido" runat="server" Text="Resumido" Checked="true" 
                                                        GroupName="GRUPO1" Visible="False" />
                                                    <asp:RadioButton ID="rbDetallado" runat="server" GroupName="GRUPO1" 
                                                        Text="Detallado" Visible="False" />
                                                </td>
                                                <td class="">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    Proveedores:
                                                    <asp:DropDownList ID="ddlProveedor" runat="server" DataTextField="PrvRazon" 
                                                        DataValueField="PrvCod" 
                                                        onselectedindexchanged="ddlProveedor_SelectedIndexChanged" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" class="style5">
                                                    <asp:Panel ID="pnLista" runat="server" >
                                                    <div id="divGridView" style="position: relative; width: 875px;  overflow:scroll;">

                                                        <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                            CssClass="" ShowHeaderWhenEmpty="True" Width="712px">
                                                            <AlternatingRowStyle BackColor="#E5EBF1" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ArtCod" HeaderText="Código" ItemStyle-Width="100px" 
                                                                    SortExpression="ArtCod" >
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ArtDescripcion" HeaderText="Descripción" 
                                                                    ReadOnly="True" ItemStyle-Width="350px"
                                                                    SortExpression="ArtDescripcion" >
                                                                    <ItemStyle Width="350px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="StockFisico" HeaderText="Stock Físico" 
                                                                    ReadOnly="True" ItemStyle-Width="150px"
                                                                    SortExpression="StockFisico" >
                                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ArtCostoProm" HeaderText="Costo Promedio"  ItemStyle-Width="150px"
                                                                    SortExpression="ArtCostoProm" ReadOnly="True" >
                                                                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="StockValorado" HeaderText="Stock Valorado" ItemStyle-Width="200px"
                                                                    ReadOnly="True" SortExpression="StockValorado" >
                                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1"> </div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle BackColor="#333333" ForeColor="White" />
                                                            <PagerStyle CssClass="pgr" />
                                                            <RowStyle Width="500px" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>
                                                        
                                                        <asp:GridView ID="dgvLista2" runat="server" AutoGenerateColumns="False" 
                                                            GridLines="None" ShowHeaderWhenEmpty="True" Width="712px">
                                                            <AlternatingRowStyle BackColor="#E5EBF1" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ArtCod" HeaderText="Código" ItemStyle-Width="100px" 
                                                                    SortExpression="ArtCod" >
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ArtDescripcion" HeaderText="Descripción" 
                                                                    ReadOnly="True" ItemStyle-Width="350px"
                                                                    SortExpression="ArtDescripcion" >
                                                                    <ItemStyle Width="350px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="StockFisico" HeaderText="Stock Físico" 
                                                                    ReadOnly="True" ItemStyle-Width="150px"
                                                                    SortExpression="StockFisico" >
                                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ArtFecVen" HeaderText="Fec.Vencimiento"  ItemStyle-Width="150px"
                                                                    SortExpression="ArtFeVen" ReadOnly="True" DataFormatString="{0:d}" >
                                                                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <i>
                                                                    <div class="clsError1" id="lblError1"> </div>
                                                                </i>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle BackColor="#333333" ForeColor="White" />
                                                            <PagerStyle CssClass="pgr" />
                                                            <RowStyle Width="500px" />
                                                            <SelectedRowStyle CssClass="selrow" />
                                                        </asp:GridView>

                                                    </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnListDet" runat="server" ScrollBars="Vertical">
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
                                                        <asp:Button ID="btnProcesar" runat="server" CssClass="clsBtnProcesar" 
                                                            Height="41px" OnClick="btnProcesar_Click" Text="Procesar" ToolTip="Procesar" />
                                                    </td>
                                                    <td valign="top">
                                                        <%
                                                            int RolUsuario = 0;
                                                            Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
                                                            if (objUsuario.Roles.rolCod==1)
                                                                RolUsuario = 1;
                                                            else
                                                                RolUsuario = 2;
                                                         %>
                                                            <asp:Button ID="btnImprimir" runat="server" CssClass="clsBtnImprimir" 
                                                                Height="38px" 
                                                                Text="Imprimir" ToolTip="Imprimir"
                                                                OnClientClick='printGrid2()'/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2" valign="top">
                                                        &nbsp;</td>
                                                    <td class="style2" valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" valign="top">
                                                        &nbsp;</td>
                                                    <td class="style1" valign="top">
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
