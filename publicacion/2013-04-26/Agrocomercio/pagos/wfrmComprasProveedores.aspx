<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmComprasProveedores.aspx.cs" Inherits="AgrocomercioWEB.pagos.wfrmComprasProveedores"
     %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function dgvListOperLetras_RowDataBound(event, rowindex) {

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan =2>
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                <td> Proveedor:
                </td>
                    <td>
                        <asp:DropDownList ID="ddlProveedor" runat="server"  DataTextField="PrvRazon"
                            DataValueField="PrvCod" Width="250px">
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td colspan =2>
                        <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False" GridLines="None"
                            CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" OnRowDataBound="dgvListOperLetras_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="tipo" HeaderText="tipo" SortExpression="tipo" Visible="false" />
                                <asp:BoundField DataField="icodletra" HeaderText="Cod. Letra" SortExpression="icodletra" />
                                <asp:BoundField DataField="valor" HeaderText="valor" SortExpression="valor" Visible="false" />
                                <asp:BoundField DataField="dfecemision" HeaderText="Fec. Fact" SortExpression="dfecemision" />
                                <asp:BoundField DataField="dopCod" HeaderText="dopCod" SortExpression="dopCod" Visible="false" />
                                <asp:BoundField DataField="numfac" HeaderText="Factura" SortExpression="numfac" />
                                <asp:BoundField DataField="PrvRazon" HeaderText="Proveedor" SortExpression="PrvRazon" />
                                <asp:BoundField DataField="opetotpagar" HeaderText="Tot. Factura" SortExpression="opetotpagar" />
                                <asp:BoundField DataField="PrvCod" HeaderText="PrvCod" SortExpression="PrvCod" Visible="false" />
                                <asp:BoundField DataField="CMONEDA" HeaderText="Moneda" SortExpression="CMONEDA" />
                                <asp:BoundField DataField="ITOTCUOTA" HeaderText="Cuotas" SortExpression="ITOTCUOTA" />
                                <asp:BoundField DataField="IESTADO" HeaderText="IESTADO" SortExpression="IESTADO"
                                    Visible="false" />
                                <asp:BoundField DataField="mntletDol" HeaderText="Total USD" ReadOnly="True" SortExpression="mntletDol" />
                                <asp:BoundField DataField="SalDol" HeaderText="Saldo USD" ReadOnly="True" SortExpression="SalDol" />
                                <asp:BoundField DataField="MNTLTSOL" HeaderText="Total Soles" ReadOnly="True" SortExpression="MNTLTSOL" />
                                <asp:BoundField DataField="salsol" HeaderText="Saldo Soles" ReadOnly="True" SortExpression="salsol" />
                            </Columns>
                            <EmptyDataTemplate>
                                <i>
                                    <div class="clsError1" id="lblError1" runat="server">
                                        No se ha Registro Ninguna Letra</div>
                                </i>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
