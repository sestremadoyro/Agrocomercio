<%@ Page Title=".:Registro de Ventas:." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmVentas.aspx.cs" Inherits="AgrocomercioWEB.Ventas.wfrmVentas" 
Theme="TemaAgro" StylesheetTheme="TemaAgro"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgro/ventas.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function ElegirMeza(mezaID) {
        document.getElementById('ctl00_Body_lblEstado').value = "EMEZA";
        __doPostBack('ctl00_Body_UpdatePanel11', mezaID);
    }
    function ElegirTipoPlato(tipoPlato) {
        document.getElementById('ctl00_Body_lblEstado').value = "EPLATO";
        __doPostBack('ctl00_Body_UpdatePanel11', tipoPlato);
    }

    function dgvPlatosClickEvent(event, rowindex) {

        //alert(event);
        //alert(rowindex);
        document.getElementById('ctl00_Body_lblEstado').value = "DGVPLA_" + event;
        __doPostBack('ctl00_Body_UpdatePanel11', rowindex);
    }
    function dgvPedidoClickEvent(event, rowindex) {

        //alert(event);
        //alert(rowindex);
        document.getElementById('ctl00_Body_lblEstado').value = "DGVPED_" + event;
        __doPostBack('ctl00_Body_UpdatePanel11', rowindex);
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
<ContentTemplate>
<table style="width: 800px; margin:0 auto;" >
    <tr>
        <td colspan="2"> <h2 class="clsTituloInterior">VENTAS DE AGROCOMERCIO S.R.L.</h2>
            <asp:HiddenField ID="lblEstado" runat="server" />
   
            <asp:HiddenField ID="lblProceso" runat="server" />
            <br />            
        </td>
    </tr>
    <tr>
            <td colspan="2">
                <table class="tabPedidos">
                <tr>
                    <td class="tabIzquierda" valign="top">
                    <table class="tableIzquierda">
                    <tr>
                        <td style=" width:20%;" ></td>
                        <td style=" width:45%;"  colspan="3"></td>
                        <td style=" width:15%;" ></td>
                        <td style=" width:20%;" ></td>
                    </tr>   
                    <tr>
                        <td class="clsCellTituloDatos2" >Tipo de Documento: </td>
                        <td class="clsCellDatos2"  colspan="3">
                            <asp:DropDownList ID="ddlTipoDocu" runat="server" Width="250px" 
                                DataTextField="AtrDescripcion" DataValueField="AtrCodigo">
                                        </asp:DropDownList></td>
                        <td class="clsCellTituloDatos2" >Nro de Doc:</td>
                        <td  class="clsCellDatos2">
                                        <asp:TextBox ID="txtNroDocu" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>                                    
                    </tr>  
                    <tr>
                        <td colspan="6" >&nbsp;&nbsp;</td>                                    
                    </tr>  
                    <tr>
                        <td class="clsCellTituloDatos2" >Señor(es): </td>
                        <td class="clsCellDatos2" colspan="3">
                            <asp:DropDownList ID="ddlClientes" runat="server" Width="250px" 
                                AutoPostBack="True" onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                        <td class="clsCellTituloDatos2" >
                            <asp:Label ID="lblDocCli" runat="server" Text="R.U.C.:"></asp:Label></td>
                        <td  class="clsCellDatos2">
                                        <asp:TextBox ID="txtDocCli" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>                                       
                    </tr>    
                    <tr>
                        <td class="clsCellTituloDatos2" >Dirección: </td>
                        <td class="clsCellDatos2" colspan="3">
                            <asp:TextBox ID="txtDireccion" runat="server" Width="248px" ReadOnly="True"></asp:TextBox></td>
                        <td class="clsCellTituloDatos2" >Telefono: </td>
                        <td  class="clsCellDatos2">
                            <asp:TextBox ID="txtTelefono" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>                                       
                    </tr>    
                    <tr>
                        <td class="clsCellTituloDatos2" >Moneda: </td>
                        <td class="clsCellDatos2">
                            <asp:DropDownList ID="ddlMoneda" runat="server" Width="100px" DataTextField="AtrDescripcion" DataValueField="AtrCodigo">
                                        </asp:DropDownList></td>
                        <td class="clsCellTituloDatos2" >Venta Al: </td>
                        <td  class="clsCellDatos2">
                            <asp:DropDownList ID="ddlTipoVenta" runat="server" Width="100px" DataTextField="AtrDescripcion" DataValueField="AtrCodigo"></asp:DropDownList>
                        </td> 
                        <td class="clsCellTituloDatos2" >Fecha: </td>
                        <td  class="clsCellDatos2">
                            <asp:TextBox ID="txtFecha" runat="server" Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>                                        
                    </tr>
                    <tr>
                        <td colspan="6" >&nbsp;&nbsp;</td>                                    
                    </tr>      
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="pnGridPedido" runat="server" Height="320px" ScrollBars="Auto">
                            <asp:GridView ID="dgvDetalleVenta" runat="server"  
                                AutoGenerateColumns="False"
                                GridLines="None"
                                CssClass="mGrid mGrid2" ShowHeaderWhenEmpty="True" 
                                onrowdatabound="dgvDetalleVenta_RowDataBound" >
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
                                    <asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="False" />
                                    <asp:BoundField DataField="ArtCod" HeaderText="Codigo" 
                                        ConvertEmptyStringToNull="False" NullDisplayText=" " >
                                        <ItemStyle HorizontalAlign="Left" Width="10px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ArtDescripcion" HeaderText="Descripcion">
                                        <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UniAbrev" HeaderText="Unidad" >
                                        <ItemStyle Width="20px"  />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dtpCantidad" HeaderText="Cant.">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="dtpPrecioVen" HeaderText="P.Unit" DataFormatString="{0:N}">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="dtpDcto" HeaderText="Dcto." DataFormatString="{0:N}">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="dtpSubTotal" HeaderText="Importe" DataFormatString="{0:N}">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>                                            
                                </Columns>
                                <EmptyDataTemplate><i><div class="clsError1" id="lblError1" runat="server">No hay Platos Disponibles</div></i></EmptyDataTemplate>
                                <PagerStyle CssClass="pgr" />
                                <SelectedRowStyle CssClass="selrow" />
                            </asp:GridView>   
                            </asp:Panel>                                     
                        </td>
                    </tr>
                    </table>
                    </td>
                    <td class="tabDerecha" valign="top">
                                <div class="divDerecha">
                                <table class="tablaDerecha" >
                                <tr>
                                    <td colspan="2" align="left"><h3>Opciones</h3></td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                            CssClass="clsBtnAgregar" onclick="btnAgregar_Click" /></td>
                                    <td valign="top">
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                                    CssClass="clsBtnEliminar"  /></td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Button ID="btnGirar" runat="server" Text="Girar" CssClass="clsBtnGirar" 
                                                    /></td>
                                    <td valign="top">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                                    CssClass="clsBtnGuardar" /></td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="clsBtnNuevo" onclick="btnNuevo_Click" 
                                                    /></td>
                                    <td valign="top">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                                    CssClass="clsBtnCancelar" onclick="btnCancelar_Click"/></td>
                                </tr>
                                </table><br />
                                <table class="tablaDerecha" >
                                <tr>
                                    <td colspan="2" align="left"><h3>Resultado</h3></td>
                                </tr>
                                <tr>
                                    <td class="clsCellTituloSmall"  style=" width:70px">VALOR DE VENTA: </td>
                                    <td style=" width:80px"><asp:Label ID="txtSubTotal" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="clsCellTituloSmall">DESCUENTO: </td>
                                    <td><asp:Label ID="txtDescuento" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="clsCellTituloSmall">FLETE: </td>
                                    <td><asp:Label ID="txtFlete" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td  class="clsCellTituloSmall">IGV (18%): </td>
                                    <td><asp:Label ID="txtIgv" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                               
                                <tr>
                                    <td class="clsCellTituloSmall">PRECIO DE VENTA: </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="clsCellDatosLarge2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="txtTotal" runat="server" Text="S/. 0.00"></asp:Label></td>
                                </tr>
                                </table>
                                </div>
                                
                            </td>
                        </tr>
                        </table>        
                </td>
    </tr>    
    <tr>
        <td>
            <div style=" display:none; ">
                <asp:Button ID="btnNuevoCliTMP" runat="server" Text="Nuevo Cliente" />
            </div>
            <asp:modalpopupextender id="ModalPopupNuevoCli" runat="server" 
	                cancelcontrolid="btnClose" 
	                targetcontrolid="btnNuevoCliTMP" popupcontrolid="pnPopNuevoCliente" 
	                backgroundcssclass="ModalPopupBG" Enabled="True" DynamicServicePath="" >
            </asp:modalpopupextender>
            <asp:panel id="pnPopNuevoCliente" style="display: none" runat="server">
	            <div class="clsModalPopup">
	                <div class="PopupHeader" id="PopupHeader">REGISTRO DE NUEVO CLIENTE</div>
	                <div class="PopupClose" id="PopupClose">
	                    <asp:Button ID="btnClose" runat="server" Text="X" CssClass="clsBtnCerrar2" /></div>
	                    <div class="PopupBody" >
	                    <table class="TablePopupBody">
	                    <tr>
	                        <td class="BodyContent">
	                            <asp:panel id="pnCliente" runat="server" >
                                <fieldset style="height:100%;">
                                <legend>Datos del Cliente</legend>
                                    <table style=" width:450px; height:100%;">
                                        <tr>
                                            <td class="clsCellTituloDatos2" valign="top" style=" width:23%;" >
                                                Codigo: </td>
                                            <td class="clsCellDatos2"  valign="top" >
                                                <asp:Label ID="txtCliCod" runat="server" Text=" "></asp:Label>                       
                                            </td> 
                                            <td class="clsCellTituloDatos2" valign="top" style=" width:25%;">
                                                Tipo Persona: </td>
                                            <td class="clsCellDatos2"  valign="top" >
                                                <asp:DropDownList ID="ddlTipoPer" runat="server" Width="123px" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlTipoPer_SelectedIndexChanged">
                                                <asp:ListItem Value="N" Selected="True" >Natural</asp:ListItem>
                                                <asp:ListItem Value="J">Juridica</asp:ListItem>
                                                </asp:DropDownList>                      
                                            </td>                     
                                        </tr>
                                        <tr>
                                            <td class="clsCellTituloDatos2" valign="top" >
                                                Tipo Documento: </td>
                                            <td class="clsCellDatos2" valign="top" >
                                                <asp:DropDownList ID="ddlTipDoc" runat="server" Width="100px">
                                                <asp:ListItem Value="DNI" Selected="True" >DNI</asp:ListItem>
                                                <asp:ListItem Value="RUC">RUC</asp:ListItem>
                                                </asp:DropDownList> 
                                            </td>
                                            <td class="clsCellTituloDatos2" valign="top">   
                                                <asp:Label ID="lblDocumento" runat="server" Text="Nro. de RUC: "></asp:Label>                        
                                            </td>
                                            <td class="clsCellDatos2"  valign="bottom" >
                                                <asp:TextBox ID="txtNroDoc" runat="server"  Width="120px"></asp:TextBox>                        
                                            </td>                    
                                        </tr>
                                        <tr>
                                            <td class="clsCellTituloDatos2" >
                                                <asp:Label ID="lblNombre" runat="server" Text="Razon Social: "></asp:Label></td>
                                            <td class="clsCellDatos2" colspan="3"><asp:TextBox ID="txtNombre" runat="server" Width="340px"></asp:TextBox></td>
                    
                                        </tr>
                                        <tr>
                                            <td class="clsCellTituloDatos2" >Dirección: </td>
                                            <td class="clsCellDatos2" colspan="3"><asp:TextBox ID="txtCliDireccion" runat="server" Width="340px"></asp:TextBox></td>                                                                    
                                        </tr>
                                        <tr>
                                            <td class="clsCellTituloDatos2" >Representante: </td>
                                            <td class="clsCellDatos2" colspan="3"><asp:TextBox ID="txtRepresentante" runat="server" Width="340px"></asp:TextBox></td>                                                                    
                                        </tr>
                                        <tr>
                                            <td class="clsCellTituloDatos2" >
                                                <asp:Label ID="lblTelefono" runat="server" Text="Telefono: "></asp:Label> </td>
                                            <td class="clsCellDatos2" ><asp:TextBox ID="txtCliTelefono" runat="server" Width="100px"></asp:TextBox></td>                                                                        
                                            <td class="clsCellTituloDatos2" > Fecha de Ingreso:  </td>
                                            <td class="clsCellDatos2" >
                                                <asp:TextBox ID="txtFecRegis" runat="server" Width="117px" ></asp:TextBox>                                                                            
                                                <asp:CalendarExtender ID="txtFecRegis_CalendarExtender" runat="server" 
                                                    TargetControlID="txtFecRegis" Format="yyyy-MM-dd"
                                                    PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                                    </asp:panel>                         
	                        </td>
	                    </tr>
	                    <tr>
	                        <td class="Controls">
	                        <div  style=" text-align:center;"> 
	                            <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo" 
                                    CssClass="clsBtnNuevo" onclick="btnNuevoCliente_Click" />
                                &nbsp;
                                <asp:Button ID="btnGuardarCliente" runat="server" Text="Guardar" 
                                    CssClass="clsBtnGuardar" onclick="btnGuardarCliente_Click" />
                                &nbsp;
                                <asp:Button ID="btnCancelarCliente" runat="server" Text="Cancelar" 
                                    CssClass="clsBtnCancelar" onclick="btnCancelarCliente_Click" />
                                
	                        </div>
	                        </td>	                        
	                    </tr>
	                    </table> 
	                </div>
	            </div>
            </asp:panel>
        </td>
        <td>
            <div style=" display:none; ">
                <asp:Button ID="btnAgregarTMP" runat="server" Text="Nuevo Cliente" />
            </div>
            <asp:modalpopupextender id="ModalPopupAgregar" runat="server" 
	                cancelcontrolid="btnClose2" 
	                targetcontrolid="btnAgregarTMP" popupcontrolid="pnPopAgregarArticulo" 
	                backgroundcssclass="ModalPopupBG" Enabled="True" DynamicServicePath="" >
            </asp:modalpopupextender>
            <asp:panel id="pnPopAgregarArticulo" style="display: none" runat="server">
	            <div class="clsModalPopup">
	                <div class="PopupHeader" id="Div1">AGREGAR UN ARTICULO</div>
	                <div class="PopupClose" id="Div2">
	                    <asp:Button ID="btnClose2" runat="server" Text="X" CssClass="clsBtnCerrar2" /></div>
	                    <div class="PopupBody" >
	                    <table class="TablePopupBody">
	                    <tr>
	                        <td class="BodyContent">
	                            <asp:panel id="pnDatosArticulo" runat="server" >
                                    <table>
                                    <tr>
                                        <td valign="top">
                                            <table style=" width:200px; height:100%;">
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Laboratorio: </td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:DropDownList ID="ddlLaboratorios" runat="server" Width="200px" 
                                                            AutoPostBack="True" DataTextField="PrvRazon" DataValueField="PrvCod" >
                                                        </asp:DropDownList>                           
                                                    </td>                                                                         
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Articulos: </td>
                                                    <td class="clsCellDatos2"  valign="top" > 
                                                        <asp:ListBox ID="lsbArticulos" runat="server" Width="200px" Height="160px" 
                                                            AutoPostBack="True" DataTextField="ArtDescripcion" DataValueField="ArtCod" 
                                                            onselectedindexchanged="lsbArticulos_SelectedIndexChanged"></asp:ListBox>                                                                       
                                                    </td> 
                                                </tr>                                                
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style=" width:200px; height:100%;">
                                                <tr>
                                                    <td style=" width:50%;" ></td>
                                                    <td style=" width:50%;" ></td>                                                
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Codigo : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtArtCod" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr> 
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Unidad de Med. : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtArtUniMed" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>              
                                                    </td>                     
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Stock Fact : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtStockFact" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        Stock Fisico : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtStockFis" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>              
                                                    </td>                     
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" >
                                                        P.Unitario : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtArtPreUnitario" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>              
                                                    </td>                     
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" style=" font-weight:bold; color:#444444;">
                                                        Cantidad : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtArtCant" runat="server"  Width="100px" ></asp:TextBox>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" style=" font-weight:bold; color:#444444;" >
                                                        Descuento : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtArtDescuento" runat="server"  Width="100px" ></asp:TextBox>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" valign="top" style=" font-weight:bold; color:#444444;">
                                                        Importe Total : &nbsp;</td>
                                                    <td class="clsCellDatos2"  valign="top" >
                                                        <asp:TextBox ID="txtImpTotal" runat="server"  Width="100px" Enabled="false" ></asp:TextBox>              
                                                    </td>                     
                                                </tr>
                                                <tr>
                                                    <td class="clsCellTituloDatos2" style=" font-weight:bold; color:#444444;"> 
                                                        Vencimiento : &nbsp;</td>
                                                    <td class="clsCellDatos2" >
                                                        <asp:TextBox ID="txtArtVenci" runat="server" Width="100px" ></asp:TextBox>                                                                            
                                                        <asp:CalendarExtender ID="txtArtVenci_CalendarExtender" runat="server" 
                                                            TargetControlID="txtArtVenci" Format="yyyy-MM-dd"
                                                            PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </table>
                                </asp:panel>                         
	                        </td>
	                    </tr>
	                    <tr>
	                        <td class="Controls">
	                        <div  style=" text-align:center;"> 
	                            <asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar" 
                                    CssClass="clsBtnAgregar"  />
                                &nbsp;
                                <asp:Button ID="btnCancelarArticulo" runat="server" Text="Cancelar" 
                                    CssClass="clsBtnCancelar" />
                                
	                        </div>
	                        </td>	                        
	                    </tr>
	                    </table> 
	                </div>
	            </div>
            </asp:panel>
        </td>
    </tr>

</table>
</ContentTemplate>

</asp:UpdatePanel>


</asp:Content>
