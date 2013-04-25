<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="wfrmAtributos.aspx.cs" Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmAtributos" 
 %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 645px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior">Administración de Tipos</h2>
                </td>
            </tr>
            <tr>
            <td>
            <div id="pnlTipos" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                    <table>
                    <tr>
                        <td rowspan="2" style="vertical-align:top;">

                            <asp:ListBox ID="lstTipos" runat="server" Height="190px" Width="191px" 
                                onselectedindexchanged="lstTipos_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>

                            <br />
                            <asp:Button ID="btnNuevo" runat="server" CssClass="clsBtnNuevo" 
                                ForeColor="Black" onclick="btnNuevo_Click" Text="Nuevo" />

                        </td>
                        <td style="vertical-align:top;">
                            <asp:GridView ID="gvwTipos" runat="server" Width="500px" 
                                AutoGenerateColumns="False" 
                                DataKeyNames="AtrCodigo" BackColor="White" 
                                onrowdatabound="gvwTipos_RowDataBound"
                                OnRowCommand="gvwTipos_RowCommand" AllowPaging="True" 
                                onpageindexchanging="gvwTipos_PageIndexChanging" PageSize="20">
                                <Columns>
                                <asp:TemplateField  ItemStyle-Font-Size="10pt">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbSeleccionaTipo" runat="server" 
                                        CommandName="SeleccionaTipo" 
                                        CommandArgument='<%# Eval("atrCodigo") %>'>[Editar]</asp:LinkButton>
                                    </ItemTemplate>                
                                    <HeaderStyle Font-Size="10px" Width="8px" />
                                    <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbEliminaTipo" runat="server"
                                        CommandName="EliminaTipo" 
                                        CommandArgument='<%#Eval("atrCodigo")%>'>[Eliminar]</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="10px" Width="8px" />
                                    <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="AtrCodigo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                        SortExpression="AtrCodigo"  ItemStyle-Font-Size="10pt">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AtrDescripcion" HeaderText="Descripción"
                                        SortExpression="AtrDescripcion" >
                                        <ItemStyle Width="250px" Font-Size="10pt" />
                                        <ControlStyle Width="250px" Font-Size="10pt"/>
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle CssClass="clsBotones" />
                                <EditRowStyle Width="300px" />
                                <AlternatingRowStyle BackColor="#EEEEEE" />
                            </asp:GridView>

                        </td>
                    </tr>
                    </table>
                            <br />
                
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Panel id="pnlEditTipos" runat="server" Visible="false" BackColor="White">
                
                <table style="width: 545px; margin:0 auto;">
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label1" runat="server" Text="Código :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" Width="78px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txtCodigo" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label3" runat="server" Text="Descripción :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="388px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtDescripcion" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label2" runat="server" Text="Estado :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEstado" runat="server" Text="Activo" />
                            
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
            
            <tr><td>
                <asp:Label ID="lblMensajes" runat="server" CssClass="clsError"></asp:Label>
            </td></tr>

        </table>
</ContentTemplate>        
</asp:UpdatePanel>


</asp:Content>
