<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="wfrmPersonal.aspx.cs" Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmPersonal" 
 %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 645px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior2">Administración de Personal</h2>
                </td>
            </tr>
            <tr>
            <td>
            <div id="pnlPersonal" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwPersonal" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="perCod" BackColor="White" 
                    onrowdatabound="gvwPersonal_RowDataBound"
                    OnRowCommand="gvwPersonal_RowCommand" AllowPaging="True" 
                    onpageindexchanging="gvwPersonal_PageIndexChanging" PageSize="15">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbSeleccionaPersonal" runat="server" 
                            CommandName="SeleccionaPersonal" 
                            CommandArgument='<%# Eval("perCod") %>'>[Editar]</asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaPersonal" runat="server"
                            CommandName="EliminaPersonal" 
                            CommandArgument='<%#Eval("perCod")%>'>[Eliminar]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="perCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="perCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="perNombres" HeaderText="Nombres "
                            SortExpression="perNombres" >
                            <ItemStyle Width="150px" Font-Size="10pt" />
                            <ControlStyle Width="150px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="perApellidoPat" HeaderText="Apellido Paterno "
                            SortExpression="perApellidoPat" >
                            <ItemStyle Width="200px" Font-Size="10pt" />
                            <ControlStyle Width="200px" Font-Size="10pt"/>
                        </asp:BoundField>
                          <asp:BoundField DataField="perApellidoMat" HeaderText="Apellido Materno "
                            SortExpression="perApellidoMat" >
                            <ItemStyle Width="200px" Font-Size="10pt" />
                            <ControlStyle Width="200px" Font-Size="10pt"/>
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#CCCCCC" />
                    <HeaderStyle CssClass="clsBotones" />
                    <EditRowStyle Width="500px" />
                    <AlternatingRowStyle BackColor="#EEEEEE" />
                </asp:GridView>
                            <br />
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                    CssClass="clsBtnNuevo"   onclick="btnNuevo_Click" ForeColor="Black" />
                
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Panel id="pnlEditPersonal" runat="server" Visible="false" BackColor="White">
                
                <table style="width: 545px; margin:0 auto;">
                <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label8" runat="server" Text="Tipo Personal :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoPersonal" runat="server" 
                                DataTextField="tpeDescripcion" DataValueField="tpeCod">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
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
                            <asp:Label ID="Label3" runat="server" Text="Nombres :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server" Width="250px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtNombres" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label2" runat="server" Text="Apellido Paterno :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellidoPat" runat="server" Width="300px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtApellidoPat" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label11" runat="server" Text="Apellido Materno :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellidoMat" runat="server" Width="300px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtApellidoMat" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                     <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label5" runat="server" Text="Dirección :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDireccion" runat="server" Width="400px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtDireccion" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label4" runat="server" Text="Teléfono :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefono" runat="server" Width="150px" MaxLength="18"></asp:TextBox>
                           
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
