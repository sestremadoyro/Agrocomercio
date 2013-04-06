<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmArticulos.aspx.cs" 
Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmArticulos" %>
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
                <td> <h2 class="clsTituloInterior">Administración de Productos</h2>
                </td>
            </tr>
            <tr>
            <td class="clsCellDatos">
                <div id="pnlTipoProductos" visible="true" runat="server" 
                        style="background-color: #FFFFFF">
                       
                <asp:Panel ID="pnlBusqueda" runat="server">
                <table class=clsSubTituloInterior>
                    <tr>
                        <td class="clsPanelBusquedaTitulo">
                                <asp:Label ID="Label8" runat="server" Text="Ingrese el Nombre del Producto a Buscar:"></asp:Label>
                            </td>
                            <td rowspan="2" class="clsPanelBusquedaBotones">
                                <asp:ImageButton ID="imbBuscarEspecial" runat="server" Height="30px" 
                                    ImageUrl="~/iconos/img_buscar16.png" Width="30px" BorderColor="White" 
                                    BorderWidth="2px" ToolTip="Buscar Producto" 
                                    onclick="imbBuscarEspecial_Click" oninit="imbBuscarEspecial_Init"  />
                                    
                                    &nbsp;&nbsp;
                                    
                                    <asp:ImageButton ID="imbNuevoProducto" runat="server" Height="30px" 
                                    ImageUrl="~/iconos/img_adicionar8.png" Width="30px" BorderColor="White" 
                                    BorderWidth="2px" ToolTip="Nuevo Producto" 
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
            <div id="pnlProductos" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                <asp:GridView ID="gvwProductos" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="artCod" BackColor="White" 
                    onrowdatabound="gvwProductos_RowDataBound"
                    OnRowCommand="gvwProducto_RowCommand" AllowPaging="True" 
                    onpageindexchanging="gvwProductos_PageIndexChanging" PageSize="15">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="lnbSeleccionaProducto" runat="server" 
                            CommandName="SeleccionaProducto" 
                            CommandArgument='<%# Eval("ArtCod") %>'>
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
                            <asp:LinkButton ID="lnbEliminaProducto" runat="server"
                            CommandName="EliminaProducto" 
                            CommandArgument='<%#Eval("ArtCod")%>'>
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
                        <asp:BoundField DataField="artCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="artCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="artDescripcion" HeaderText="Descripción del Producto"
                            SortExpression="ArtDescripcion" >
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
                            CssClass="clsBtnNuevo" onclick="btnNuevo_Click" ForeColor="Black" 
                            Visible="False" />
                    </div>
                
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Panel id="pnlEditProductos" runat="server" Visible="false" BackColor="White">
                    <span class="clsSubTituloInterior" style="float:left;">Datos del Producto</span>
                
                 <table style="width: 645px; margin:0 auto;">
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
                            <asp:Label ID="Label2" runat="server" Text="Descripción :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="400px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtDescripcion" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label10" runat="server" Text="Unidad Medida :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnidadMedida" runat="server" 
                                DataTextField="UniDescripcion" DataValueField="UniCod">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label3" runat="server" Text="Stock :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" Width="100px" 
                                CssClass="clsFormTextMayusculas" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="lblCostoPromedio" runat="server" Text="Costo Promedio :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCostoPromedio" runat="server" Width="100px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                ControlToValidate="txtCostoPromedio" 
                                ErrorMessage="Costo Promedio Ingrese un Monto" Operator="DataTypeCheck" 
                                Type="Double">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label6" runat="server" Text="Stock Máximo:" 
                                ForeColor="Black" Visible="False" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStockMax" runat="server" Width="100px" Visible="False"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtStockMax" ErrorMessage="Ingrese Stock Máximo Correcto" 
                                Operator="DataTypeCheck" Type="Double" Visible="False">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label7" runat="server" Text="Stock Mínimo :" 
                                ForeColor="Black" Visible="False" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStockMin" runat="server" Width="100px" Visible="False"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="txtStockMin" 
                                ErrorMessage="Ingrese un ´Stock Mínimo Correcto" Operator="DataTypeCheck" 
                                Type="Double" Visible="False">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label9" runat="server" Text="Fec.Vencimiento :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecVencimiento" runat="server" Width="150px"></asp:TextBox>
                              <asp:CalendarExtender ID="txtFechaCompra_CalendarExtender" runat="server" 
                                    TargetControlID="txtFecVencimiento" Format="yyyy-MM-dd"
                                    PopupButtonID="Image2" Enabled="True" >
                                </asp:CalendarExtender>
                        </td>
                    </tr>

                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label5" runat="server" Text="Activo :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEstado" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label11" runat="server" Text="Laboratorio :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProveedor" runat="server" Height="29px" Width="328px" 
                                DataTextField="prvRazon" DataValueField="prvCod">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label15" runat="server" Text="Peso :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeso" runat="server" Width="100px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="txtPeso" 
                                ErrorMessage="Costo Promedio Ingrese un Monto" Operator="DataTypeCheck" 
                                Type="Double">*</asp:CompareValidator>
                        </td>
                    </tr>

                    <tr>
                        
                        <td style="text-align:center;" colspan="2">
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
            <tr>
            <td>
            <div id="pnlListaPrecios" visible="true" runat="server" 
                    style="background-color: #FFFFFF">
                    <span class="clsSubTituloInterior" style="float:left;">Precios - Lotes</span>
                    <div style=" float:right;">
                         <asp:Button ID="btnNuevoPrecio" runat="server" Text="Nuevo" 
                        CssClass="clsBtnNuevo" onclick="btnNuevoPrecio_Click" ForeColor="Black" 
                        Visible="true" />
                    </div>
                <asp:GridView ID="gvwListaPrecios" runat="server" Width="648px" 
                    AutoGenerateColumns="False" 
                    DataKeyNames="LprCod" BackColor="White" 
                    onrowdatabound="gvwListaPrecios_RowDataBound"
                    OnRowCommand="gvwListaPrecios_RowCommand" AllowPaging="True" 
                    onpageindexchanging="gvwListaPrecios_PageIndexChanging">
                    <Columns>
                    <asp:TemplateField  ItemStyle-Font-Size="10pt">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbSeleccionaPrecio" runat="server" 
                            CommandName="SeleccionaPrecio" 
                            CommandArgument='<%# Eval("LprCod") %>'>[Editar]</asp:LinkButton>
                        </ItemTemplate>                
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnbEliminaPrecio" runat="server"
                            CommandName="EliminaPrecio" 
                            CommandArgument='<%#Eval("LprCod")%>'>[Eliminar]</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="10px" Width="8px" />
                        <ItemStyle Font-Size="8pt" Width="8px"  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                        <asp:BoundField DataField="LprCod" HeaderText="#" InsertVisible="False" ReadOnly="True"
                            SortExpression="artCod"  ItemStyle-Font-Size="10pt">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LprPrecio" HeaderText="Precio"
                            SortExpression="LprPrecio" >
                            <ItemStyle Width="50px" Font-Size="10pt" />
                            <ControlStyle Width="50px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="LprDscto" HeaderText="Dscto"
                            SortExpression="LprDscto" >
                            <ItemStyle Width="50px" Font-Size="10pt" />
                            <ControlStyle Width="50px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="LotNro" HeaderText="Lote"
                            SortExpression="LotNro" >
                            <ItemStyle Width="20px" Font-Size="10pt" />
                            <ControlStyle Width="20px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="LotStock" HeaderText="Stock"
                            SortExpression="LotStock" >
                            <ItemStyle Width="50px" Font-Size="10pt" />
                            <ControlStyle Width="50px" Font-Size="10pt"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="LotFecVenci" HeaderText="F.Vence"
                            SortExpression="LotFecVenci" >
                            <ItemStyle Width="70px" Font-Size="10pt" />
                            <ControlStyle Width="70px" Font-Size="10pt"/>
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#CCCCCC" />
                    <HeaderStyle CssClass="clsBotones" />
                    <EditRowStyle Width="500px" />
                    <AlternatingRowStyle BackColor="#EEEEEE" />
                </asp:GridView>
                            <br />
               
                
            </div>
            </td>
            </tr>
            <tr>
            <td>
            <asp:Panel id="pnlDetallePrecio" runat="server" Visible="false" BackColor="White">
                <span class="clsSubTituloInterior" style="float:left;">Datos del Precio y Lote</span>

                <table style="width: 645px; margin:0 auto;">
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label12" runat="server" Text="Código :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodigoPrecio" runat="server" Width="40px" Enabled="False"></asp:TextBox>
                            <asp:Label ID="lblCodigoLote" runat="server" Text="Label" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label13" runat="server" Text="Precio :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecio" runat="server" Width="120px"
                             CssClass="clsFormTextMayusculas"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtPrecio" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                ControlToValidate="txtPrecio" 
                                ErrorMessage="Ingrese un Precio Correcto" Operator="DataTypeCheck" 
                                Type="Double" Visible="False" Display="Dynamic">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label14" runat="server" Text="Descuento :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtDescuento" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label16" runat="server" Text="Lote :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLote" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label18" runat="server" Text="Stock :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStockLote" runat="server" Width="120px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                ControlToValidate="txtStockLote" 
                                ErrorMessage="Ingrese un Stock Correcto" Operator="DataTypeCheck" 
                                Type="Double" Visible="False">*</asp:CompareValidator>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label17" runat="server" Text="Fec.Vencimiento :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecVenceLote" runat="server" Width="150px"></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    TargetControlID="txtFecVenceLote" Format="yyyy-MM-dd"
                                    PopupButtonID="Image2" Enabled="True" >
                                </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtFecVenceLote" Display="Dynamic" 
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    

                    <tr>
                        <td class="clsFormEtiquetas">
                            <asp:Label ID="Label20" runat="server" Text="Estado :" 
                                ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEstadoPrecio" runat="server" Enabled="False" />
                            
                        </td>
                    </tr>
                    
                    <tr>
                        
                        <td style="text-align:center;" colspan="2">
                            <asp:Button ID="btnGuadarPrecio" runat="server" Text="Guardar" 
                                CssClass="clsBtnGuardar" onclick="btnGuardarPrecio_Click" 
                                ForeColor="Black" />
                            <asp:Button ID="btnEliminarPrecio" runat="server" Text="Cancelar" 
                                CssClass="clsBtnCancelar" onclick="btnCancelarPrecio_Click" 
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
