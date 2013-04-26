<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmRoles.aspx.cs" 
Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmRoles" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="/App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


   <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 445px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior">Administración de Roles</h2>
                </td>
            </tr>

            <tr>
            <td>
            <div id="pnlRoles" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwRoles" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="rolCod" BackColor="White" 
                    onrowdatabound="gvwRoles_RowDataBound"
                    OnRowCommand="gvwRoles_RowCommand1" AllowPaging="True" 
                    onpageindexchanging="gvwRoles_PageIndexChanging1">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbSeleccionaRoles" runat="server" 
                            CommandName="SeleccionaRoles" 
                            CommandArgument='<%# Eval("rolCod") %>'>[Editar]</asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaRoles" runat="server"
                            CommandName="EliminaRoles" 
                            CommandArgument='<%#Eval("rolCod")%>'>[Eliminar]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="rolCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="rolCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rolDescripcion" HeaderText="Roles"
                            SortExpression="rolDescripcion" >
                            <ItemStyle Width="400px" Font-Size="10pt" />
                            <ControlStyle Width="400px" Font-Size="10pt"/>
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
                <asp:Panel id="pnlEditRoles" runat="server" Visible="false" BackColor="White">
                
                <table>
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
                            <asp:Label ID="Label3" runat="server" Text="Descripción :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="250px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtDescripcion" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
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
