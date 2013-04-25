<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmClientes.aspx.cs" 
Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmClientes"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Import Namespace="pryAgrocomercioBLL.EntityCollection" %>
<%@ Import Namespace="pryAgrocomercioDAL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
   
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    
            <table style="width: 645px; margin:0 auto;" >
            <tr>
                <td> <h2 class="clsTituloInterior">Administración de Clientes</h2>
                </td>
            </tr>

            <tr>
                <td class="clsCellDatos">
                
                <div id="pnlTipoProductos" visible="true" runat="server" 
                    style="background-color: #FFFFFF">


                <asp:Panel ID="pnlBusqueda" runat="server">
                <table class="clsSubTituloInterior">
                    <tr>
                        <td class="clsPanelBusquedaTitulo">
                                <asp:Label ID="Label10" runat="server" Text="Ingrese el Nombre del Cliente a Buscar:"></asp:Label>
                            </td>
                            <td rowspan="2" class="clsPanelBusquedaBotones">
                                <asp:ImageButton ID="imbBuscarEspecial" runat="server" Height="30px" 
                                    ImageUrl="~/iconos/img_buscar16.png" Width="30px" BorderColor="White" 
                                    BorderWidth="2px" ToolTip="Buscar Producto" 
                                    onclick="imbBuscarEspecial_Click" oninit="imbBuscarEspecial_Init" />
                                    
                                    &nbsp;&nbsp;
                                    
                                    <asp:ImageButton ID="imbNuevoProducto" runat="server" Height="30px" 
                                    ImageUrl="~/iconos/img_adicionar8.png" Width="30px" BorderColor="White" 
                                    BorderWidth="2px" ToolTip="Nuevo Cliente" 
                                    onclick="imbNuevoProducto_Click" oninit="imbNuevoProducto_Init" />
                              <asp:CheckBox ID="chkOpcionTodos" runat="server" Checked="false" Text="Ver Todos" />
                            </tr>
                    <tr>
                        <td class="clsPanelBusquedaTitulo">
                                <asp:TextBox ID="txtBusqueda" runat="server" Width="370px" 
                                    CssClass="clsCellDatos"></asp:TextBox>
                            </td>
                    </tr>
                   
                </table>
                </asp:Panel>                       
                </div>
            </td>
            </tr>

            <tr>
            <td>
            <div id="pnlClientes" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwClientes" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="cliCod" BackColor="White" 
                    onrowdatabound="gvwClientes_RowDataBound"
                    OnRowCommand="gvwClientes_RowCommand" AllowPaging="True" 
                    onpageindexchanging="gvwClientes_PageIndexChanging" PageSize="15">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                             <asp:LinkButton ID="lnbSeleccionaCliente" runat="server" 
                            CommandName="SeleccionaCliente" 
                            CommandArgument='<%# Eval("cliCod") %>'>
                            <%
                                Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
                                if (objUsuario.Roles.rolOpcionModificar.Value)
                                {
                             %>
                                [Editar]
                            <%
                                }
                                else
                                {
                             %>
                                  [Seleccionar]
                            <%
                                }
                             %>
                            </asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaCliente" runat="server"
                            CommandName="EliminaCliente" 
                            CommandArgument='<%#Eval("cliCod")%>'>
                            <%
                                Usuarios objUsuario = (Usuarios)this.LeerVariableSesion("oUsuario");
                                if (objUsuario.Roles.rolOpcionEliminar.Value)
                                {
                             %>
                                [Eliminar]
                            <%
                                }
                             %>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="cliCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="cliCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cliNombre" HeaderText="Nombre del Cliente"
                            SortExpression="cliNombre" >
                            <ItemStyle Width="500px" Font-Size="10pt" />
                            <ControlStyle Width="500px" Font-Size="10pt"/>
                        </asp:BoundField>
                        
                    </Columns>
                    <SelectedRowStyle BackColor="#CCCCCC" />
                    <HeaderStyle CssClass="clsBotones" />
                    <EditRowStyle Width="500px" />
                    <AlternatingRowStyle BackColor="#EEEEEE" />
                </asp:GridView>
                            <br />
                <div style=" float:right;">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                        CssClass="clsBtnNuevo"   onclick="btnNuevo_Click" ForeColor="Black" 
                        Visible="False" />
                </div>
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Panel id="pnlEditClientes" runat="server" Visible="false" BackColor="White">
                
                <table style="width: 645px; margin:0 auto;">
                <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label8" runat="server" Text="Tipo Persona :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoPersona" runat="server">
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
                            <asp:Label ID="Label3" runat="server" Text="Nombre del Cliente :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombres" runat="server" Width="350px" 
                                CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtNombres" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label2" runat="server" Text="Dirección :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDireccion" runat="server" Width="400px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
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
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label6" runat="server" Text="Tipo Documento :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoDoc" runat="server">
                            </asp:DropDownList>
                            
                            
                        </td>
                    </tr>  
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label5" runat="server" Text="Doc.Identidad :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDocumento" runat="server" Width="150px" MaxLength="12"></asp:TextBox>
                           
                        </td>
                    </tr>                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label7" runat="server" Text="Representante :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRepresentante" runat="server" Width="250px"
                            CssClass="clsFormTextMayusculas" ></asp:TextBox>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label9" runat="server" Text="Fec.registro :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecRegistro" runat="server" Width="150px" ></asp:TextBox>
                              <asp:CalendarExtender ID="txtFechaCompra_CalendarExtender" runat="server" 
                                    TargetControlID="txtFecRegistro" Format="yyyy-MM-dd"
                                    PopupButtonID="Image2" Enabled="True" >
                                </asp:CalendarExtender>

                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label11" runat="server" Text="Activo :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkActivo" runat="server" />
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
