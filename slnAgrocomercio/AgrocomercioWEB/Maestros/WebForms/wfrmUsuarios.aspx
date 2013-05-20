<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmUsuarios.aspx.cs" 
Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 645px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior2">Administración de Usuarios</h2>
                </td>
            </tr>

            <tr>
            <td>
            <div id="pnlUsuarios" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwUsuarios" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="usrCod" BackColor="White" 
                    onrowdatabound="gvwUsuarios_RowDataBound"
                    OnRowCommand="gvwUsuarios_RowCommand1" AllowPaging="True" 
                    onpageindexchanging="gvwUsuarios_PageIndexChanging1">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbSeleccionaUsuario" runat="server" 
                            CommandName="SeleccionaUsuario" 
                            CommandArgument='<%# Eval("usrCod") %>'>[Editar]</asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaUsuario" runat="server"
                            CommandName="EliminaUsuario" 
                            CommandArgument='<%#Eval("usrCod")%>'>[Eliminar]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="usrCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="usrCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="usrLogin" HeaderText="Usuario"
                            SortExpression="usrLogin" >
                            <ItemStyle Width="50px" Font-Size="10pt" />
                            <ControlStyle Width="50px" Font-Size="10pt"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="perNombres" HeaderText="Nombres"
                            SortExpression="perNombres" >
                            <ItemStyle Width="100px" Font-Size="10pt" />
                            <ControlStyle Width="100px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="perApellidoPat" HeaderText="Apellido Paterno"
                            SortExpression="perApellidoPat" >
                            <ItemStyle Width="100px" Font-Size="10pt" />
                            <ControlStyle Width="100px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="perApellidoMat" HeaderText="Apellido Materno"
                            SortExpression="perApellidoMat" >
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
                <asp:Panel id="pnlEditUsuarios" runat="server" Visible="false" BackColor="White">
                
                <table>
                        <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label8" runat="server" Text="Rol Usuario :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoles" runat="server" Height="16px" Width="147px" 
                                DataTextField="rolDescripcion" DataValueField="rolCod">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label11" runat="server" Text="Código :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label12" runat="server" Text="Usuario :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label13" runat="server" Text="Contraseña :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClave" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label14" runat="server" Text="Código Personal:" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoPersonal" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                                <asp:ImageButton ID="imbBuscarEspecial" runat="server" BorderColor="White" 
                                BorderWidth="2px" Height="30px" ImageUrl="~/iconos/img_buscar16.png" 
                                onclick="imbBuscarEspecial_Click" oninit="imbBuscarEspecial_Init" 
                                ToolTip="Buscar Personal" Width="30px" />
                
                        </td>
                    </tr>

                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label15" runat="server" Text="Nombres :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="200px" 
                                CssClass="clsFormTextMayusculas" Enabled="False"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label16" runat="server" Text="Apellido Paterno :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApePaterno" runat="server" Width="250px" 
                                CssClass="clsFormTextMayusculas" Enabled="False"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label17" runat="server" Text="Apellido Materno :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApeMaterno" runat="server" Width="250px" 
                                CssClass="clsFormTextMayusculas" Enabled="False"></asp:TextBox>
                           
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
                        OnRowCommand="gvwPersonal_RowCommand1" AllowPaging="True" 
                        onpageindexchanging="gvwPersonal_PageIndexChanging1">
                        <Columns>
                        <asp:TemplateField  ItemStyle-Font-Size="10pt">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbSeleccionaPersonal" runat="server" 
                                CommandName="SeleccionaPersonal" 
                                CommandArgument='<%# Eval("perCod") %>'>[Seleccionar]</asp:LinkButton>
                            </ItemTemplate>                
                                <HeaderStyle Font-Size="10px" Width="8px" />
                                <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                            </asp:TemplateField>
                            <asp:BoundField DataField="perCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                                SortExpression="perCod"  ItemStyle-Font-Size="10pt">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="perNombres" HeaderText="Nombres"
                                SortExpression="perNombres" >
                                <ItemStyle Width="100px" Font-Size="10pt" />
                                <ControlStyle Width="100px" Font-Size="10pt"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="perApellidoPat" HeaderText="Apellido Paterno"
                                SortExpression="perApePaterno" >
                                <ItemStyle Width="100px" Font-Size="10pt" />
                                <ControlStyle Width="100px" Font-Size="10pt"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="perApellidoMat" HeaderText="Apellido Materno"
                                SortExpression="perApeMaterno" >
                                <ItemStyle Width="100px" Font-Size="10pt" />
                                <ControlStyle Width="100px" Font-Size="10pt"/>
                            </asp:BoundField>

                        </Columns>
                        <SelectedRowStyle BackColor="#CCCCCC" />
                        <HeaderStyle CssClass="clsBotones" />
                        <EditRowStyle Width="500px" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                    </asp:GridView>
                
                </div>



            </td>
            </tr>


        </table>
</ContentTemplate>        
</asp:UpdatePanel>



</asp:Content>
