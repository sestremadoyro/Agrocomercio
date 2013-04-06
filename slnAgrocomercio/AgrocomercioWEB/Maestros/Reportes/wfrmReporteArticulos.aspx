<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmReporteArticulos.aspx.cs" Inherits="AgrocomercioWEB.Maestros.Reportes.wfrmReporteArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table style="width: 400px; margin:0 auto;">
        <tr>
            <td colspan="1">
                <h2 class="clsTituloInterior">Reporte de Articulos por Proveedores</h2>
            </td>
        </tr>
        <tr>
            <td>

                <table style=" text-align:right; width: 20%;  background-color: #E4E4E4;" align="right">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtImprimir" runat="server" onclick="lbtImprimir_Click">[Imprimir]</asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="ibtImprimir" runat="server" Height="30px" 
                                ImageUrl="~/iconos/img_printer_48.png" Width="30px" BorderColor="#E4E4E4" 
                                BorderStyle="Solid" BorderWidth="1px" oninit="ibtImprimir_Init" 
                                onclick="ibtImprimir_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align:center; margin-left: 40px;">
                <asp:Label ID="Label3" runat="server" Text="Seleccione el Laboratorio :"></asp:Label>
                <asp:DropDownList ID="lbxLaboratorios" runat="server" Height="18px" 
                    Width="208px">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <table style="width: 100%">
                 <tr>
                    <td colspan="4" style="text-align:center; "   >
                        <asp:Label ID="Label1" runat="server" Text="Seleccione las Fechas de Vencimiento :"></asp:Label>
                        
                    </td>
                </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <asp:RadioButton ID="rdoTodo" runat="server" Text="Todo" 
                                GroupName="seleccion" AutoPostBack="True" 
                                oncheckedchanged="rdoTodo_CheckedChanged" />
                        </td>
                        <td colspan="2" style="text-align:center;" >
                            <asp:RadioButton ID="rdoDesdeHasta" runat="server" Text="Desde - Hasta" 
                                GroupName="seleccion" AutoPostBack="True" Checked="True" 
                                oncheckedchanged="rdoDesdeHasta_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                        <div style="float:left; width:40px; color: #7A3D30; font-weight: bold;">Desde
                        </div>
                            <asp:Label ID="Label4" runat="server" Text="Mes :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMesDesde" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label5" runat="server" Text="Año :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnioDesde" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 26px" class="clsFormEtiquetas">
                            <div style="float:left; width:40px; color: #7A3D30; font-weight: bold;">Hasta
                            </div>

                            <asp:Label ID="Label6" runat="server" Text="Mes :"></asp:Label>
                        </td>
                        <td style="height: 26px">
                            <asp:DropDownList ID="ddlMesHasta" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 26px" class="clsFormEtiquetas">
                            <asp:Label ID="Label7" runat="server" Text="Año :"></asp:Label>
                        </td>
                        <td style="height: 26px">
                            <asp:DropDownList ID="ddlAnioHasta" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="float:left; ">
                                <asp:Label ID="lblMensajes" runat="server" Text="Label" BorderColor="Red" 
                                    BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" Visible="False" ></asp:Label>

                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


</asp:Content>
