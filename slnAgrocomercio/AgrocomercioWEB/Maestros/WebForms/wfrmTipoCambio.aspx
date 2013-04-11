<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmTipoCambio.aspx.cs" 
Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmTipoCambio"  Theme="TemaAgrocomercio" StylesheetTheme="TemaAgrocomercio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


   <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 645px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior">Administración de Tipos de Cambio</h2>
                </td>
            </tr>

            <tr>
            <td>
            <div id="pnlTiposCambio" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwTiposCambio" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="tcmCod" BackColor="White" 
                    onrowdatabound="gvwTiposCambio_RowDataBound"
                    OnRowCommand="gvwTiposCambio_RowCommand1" AllowPaging="True" 
                    onpageindexchanging="gvwTiposCambio_PageIndexChanging1">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbSeleccionaTiposCambio" runat="server" 
                            CommandName="SeleccionaTiposCambio" 
                            CommandArgument='<%# Eval("tcmCod") %>'>[Editar]</asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaTiposCambio" runat="server"
                            CommandName="EliminaRoles" 
                            CommandArgument='<%#Eval("tcmCod")%>'>[Eliminar]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="tcmCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="tcmCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tcmCambio" HeaderText="Cambio" InsertVisible="False" ReadOnly="True"
                            SortExpression="tcmCambio"  ItemStyle-Font-Size="10pt" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tcmFecha" HeaderText="Fecha"
                            SortExpression="tcmFecha" >
                            <ItemStyle Width="200px" Font-Size="10pt" />
                            <ControlStyle Width="200px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="tcmMoneda" HeaderText="Moneda"
                            SortExpression="tcmMoneda" >
                            <ItemStyle Width="100px" Font-Size="10pt" />
                            <ControlStyle Width="100px" Font-Size="10pt"/>
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#CCCCCC" />
                    <HeaderStyle CssClass="clsBotones" />
                    <EditRowStyle Width="500px" />
                    <AlternatingRowStyle BackColor="#EEEEEE" />
                </asp:GridView>
                            <br />
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                    CssClass="clsBtnNuevo"  onclick="btnNuevo_Click" ForeColor="Black" />
                
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Panel id="pnlEditTiposCambio" runat="server" Visible="false" BackColor="White">
                
                <table style="width: 445px; margin:0 auto;">
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label1" runat="server" Text="Código :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label3" runat="server" Text="Cambio :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCambio" runat="server" Width="200px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtCambio" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label2" runat="server" Text="Fecha :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecha" runat="server" Width="200px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCambio" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr> <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label4" runat="server" Text="Moneda :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMoneda" runat="server">
                                <asp:ListItem Value="USD">Dolares</asp:ListItem>
                                <asp:ListItem Value="EUR">Euros</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align:center;">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                CssClass="clsBtnGuardar" onclick="btnGuardar_Click" ForeColor="Black" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                CssClass="clsBtnCancelar" onclick="btnCancelar_Click" 
                                ForeColor="Black" />
                        </td>
                    </tr>

                </table>
                
                
                </asp:Panel>
            </td>
            </tr>
            
            <tr><td colspan="2">
                <asp:Label ID="lblMensajes" runat="server" CssClass="clsError"></asp:Label>
            </td></tr>

        </table>

        </ContentTemplate>        
</asp:UpdatePanel>


</asp:Content>
