<%@ Page Title=".:Reportes de Compras:." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmRepVentasXCobrar.aspx.cs" Inherits="AgrocomercioWEB.Compras.wfrmRepVentasXCobrar" 
%>

<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
   
    function AbrirVentanaProveedoresMes() {
        window.open('Reportes/wfrmVistaRepProveedoresMes.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
    }
    function AbrirVentanaProveedoresGeneral() {
        window.open('Reportes/wfrmVistaRepProveedoresGeneral.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
    }

    function setCliCod(source, eventargs) {
        document.getElementById('lblEstado').value = "CLI_SELECT";
        __doPostBack('MainUpdatePanel', eventargs.get_value());
    }
 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdateProgress id="updateProgress" runat="server" 
                AssociatedUpdatePanelID="MainUpdatePanel"  dynamiclayout="true">
    <ProgressTemplate>
        <div class="cls_ProgresShadow">
            &nbsp;
        </div>
        <div  class="cls_ProgresIcon">
            <div style="padding: 10px;position:relative;top:33%; left:45%; height: 100px; width: 150px;">
                <asp:Image ID="imgUpdateProgress" runat="server" 
                ImageUrl="../images/ajax-loader.gif" AlternateText="Procesando ..." 
                ToolTip="Trabajando ..." 
                style="height: 80px; width: 80px;" />
                <span style="padding: 10px; font-size:13px; font-weight:bold; color:#555555; " >Procesando ... </span>
            </div>
            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
            
        </div>
        
    </ProgressTemplate>
</asp:UpdateProgress>        
<asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
<ContentTemplate>
<table  class="MainContent" >
    <tr>
        <td colspan="2"> 
            <h1 class="clsTituloPrincipal2">REPORTES DE VENTAS DE AGROCOMERCIO S.R.L.</h1>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table class="tabPedidos">
                <tr>
                    <td class="tabIzquierda3" valign="top">
                    <asp:panel id="pnListOperCompras" runat="server">
                        <table class="tableIzquierda">
                        
                        <tr>
                            <td  colspan="4" >
                            <asp:HiddenField ID="lblEstado" runat="server" />
                                <h3><asp:Label ID="lblSubTitulo" runat="server" Text="Label">Reportes de Ventas Por Cobrar</asp:Label></h3>
                            </td>
                        </tr> 
                        <tr>
                            <td colspan="4" >
                                &nbsp; &nbsp;</td>
                        </tr>  
                        <tr>
                            <td class="clsCellTituloDatos2" >
                                <asp:Label ID="Label4" runat="server" Text="Clientes: "></asp:Label>
                            </td>
                            <td class="clsCellDatos2" >
                                <asp:DropDownList ID="ddlClientes" runat="server" Width="250px" 
                                    AutoPostBack="True" DataTextField="CliNombre" DataValueField="CliCod" 
                                    onselectedindexchanged="ddlClientes_SelectedIndexChanged"  style=" display:none;" >
                                            </asp:DropDownList>
                                 <asp:TextBox ID="txtCliente" runat="server" Width="250px" ></asp:TextBox>
                                <asp:TextBoxWatermarkExtender 
	                                ID="txtCliente_TextBoxWatermarkExtender" runat="server"
	                                TargetControlID="txtCliente" WatermarkCssClass = "clsWaterMark" 
	                                WatermarkText="Busqueda de Clientes..." Enabled="true">
                                </asp:TextBoxWatermarkExtender>
                                <div id="ClilistPlacement" class="cls_listPlacement" style=" overflow:auto; "></div>
                                <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender"
	                                MinimumPrefixLength="2" TargetControlID="txtCliente" 
                                    enablecaching="true" 
                                    showonlycurrentwordincompletionlistitem="true" 
	                                CompletionSetCount="10" CompletionInterval="100" 
	                                ServiceMethod="BuscarClientes" 
	                                runat="server" OnClientItemSelected="setCliCod" 
	                                CompletionListElementID="ClilistPlacement">
                                </asp:AutoCompleteExtender>
                                
                            </td>                            
                        </tr>   
                        <tr>
                            <td class="clsCellTituloDatos2" >
                                <asp:Label ID="Label7" runat="server" Text="Tipo de Cambio:"></asp:Label>
                            </td>
                            <td class="clsCellDatos2" >
                                <asp:TextBox ID="txtTipCam" runat="server" Text= "2.56" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="clsCellTituloDatos2" >
                                <asp:CheckBox ID="chkPorFecha" runat="server" Text="Por Fechas:  " 
                                    AutoPostBack="True" oncheckedchanged="chkPorFecha_CheckedChanged" />                                
                               
                            </td>
                            <td class="clsCellDatos2" >
                                <div id="divFechas" runat="server">
                                    <asp:Label ID="Label2" runat="server" Text="Desde "></asp:Label>
                                    <asp:TextBox ID="txtFecIni" runat="server"  Width="120px" ></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender_txtFecIni" runat="server" 
                                        TargetControlID="txtFecIni" Format="yyyy-MM-dd"
                                        PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                    <asp:Label ID="Label3" runat="server" Text=" Hasta "></asp:Label>
                                    <asp:TextBox ID="txtFecFin" runat="server"  Width="120px" ></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender_txtFecFin" runat="server" 
                                        TargetControlID="txtFecFin" Format="yyyy-MM-dd"
                                        PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                </div>

                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" >
                                      &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" >
                                                          
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" >
                                <div style=" text-align:center; color:Green; font-size:14px; font-weight:bold;"><asp:Label ID="lblExito" runat="server" Text="El Reporte de Proceso Correctamente." Visible= "false"></asp:Label></div>
                                <div style=" text-align:center; color:Red; font-size:14px; font-weight:bold;"><asp:Label ID="lblError" runat="server" Text="No hay Registros para Procesar el Reporte" Visible= "false"></asp:Label></div>                          

                            </td>
                            
                        </tr>
                        </table>
                    </asp:panel>
                   
                     
                    </td>
                    <td class="tabDerecha3" valign="top">
                        <div class="divDerecha">
                                <asp:Panel runat="server" ID="pnBotones" CssClass="clspntablaDerecha" >
                                <table class="tablaDerecha" >
                                <tr>
                                    <td colspan="2" align="left"><h3>Opciones</h3></td>
                                </tr>                                
                                <tr>
                                    <td valign="top">
                                        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" ToolTip="Procesar" 
                                            CssClass="clsBtnProcesar" onclick="btnProcesar_Click" />
                                    </td>
                                    <td valign="top">
                                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" ToolTip="Imprimir" 
                                            CssClass="clsBtnImprimir" OnClientClick="AbrirVentanaProveedoresGeneral()" onclick="btnImprimir_Click"  />
                                    </td>
                                </tr>
                                
                                </table>
                                </asp:Panel>                                
                                </div>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="tablaDerecha" valign="top" colspan="2">
                                <table class="tablaDerecha" >
                                <tr>
                                    <td>
                                        <div id="container" style="position: relative; width: 100%;">
                                    <cc1:Grid ID="gridVentasxCobrar" runat="server" AllowAddingRecords="False" 
                                        AllowColumnResizing="False" AllowFiltering="True" 
                                        AutoGenerateColumns="False" Width="100%" 
                                        FolderStyle="..\App_Themes\TemaAgrocomercio\Grid\style_6" 
                                        oncolumnscreated="gridVentasxCobrar_ColumnsCreated" GroupBy="CliNombre" 
                                                FolderLocalization="..\App_Themes\TemaAgrocomercio\Grid\localization" Language="es">
                                        <Columns>
                                            <cc1:Column DataField="OpeCod" HeaderText="Codigo" Index="0" AllowGroupBy="False" AllowFilter="False" Width="50"></cc1:Column>
                                            <cc1:Column DataField="OpeFecEmision" HeaderText="Fec.Emision" Index="1" DataFormatString="{0:d/M/yyyy}" AllowGroupBy="False" >
                                                 <FilterOptions>
                                                    <cc1:CustomFilterOption IsDefault="true" ID="Between_OpeFecEmision" Text="Entre">
                                                        <TemplateSettings FilterTemplateId="OpeFecEmisionBetweenFilter" 
                                                            FilterControlsIds="StartDate_OpeFecEmision,EndDate_OpeFecEmision"
                                                            FilterControlsPropertyNames="value,value" />
                                                    </cc1:CustomFilterOption>
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="NroNota" HeaderText="Nro Pedido" Index="2" AllowGroupBy="False"  AllowFilter="False" Width="190"></cc1:Column>
                                            <cc1:Column DataField="NroFactura" HeaderText="Nro Fatura" Index="3" AllowGroupBy="False" AllowFilter="False" Width="190"></cc1:Column>
                                            <cc1:Column DataField="CliNombre" HeaderText="Cliente" Index="4" Width="250"></cc1:Column>
                                            <cc1:Column DataField="Zona" HeaderText="Zona" Index="5"></cc1:Column>
                                            <cc1:Column DataField="Vendedor" HeaderText="Vendedor" Index="6"></cc1:Column>
                                            <cc1:Column DataField="dFecPago" HeaderText="Fec.Pago" Index="7" DataFormatString="{0:d/M/yyyy}" AllowGroupBy="False" AllowFilter="False"></cc1:Column>
                                            <cc1:Column DataField="Monto_USD" HeaderText="Monto USD" Index="8" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="ACta_USD" HeaderText="A Cta USD" Index="9" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="Saldo_USD" HeaderText="Saldo USD" Index="10" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="Monto_PEN" HeaderText="Monto PEN" Index="11" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="ACta_PEN" HeaderText="A Cta PEN" Index="12" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="Saldo_PEN" HeaderText="Saldo PEN" Index="13" AllowGroupBy="False" AllowFilter="False" ></cc1:Column>
                                            <cc1:Column DataField="nSaldoTotal" HeaderText="Saldo Total" Index="14" AllowGroupBy="False" ></cc1:Column>
                                            <cc1:Column DataField="CliCreAsig" HeaderText="Credito Asign" Index="15" AllowGroupBy="False" AllowFilter="False"></cc1:Column>
                                            <cc1:Column DataField="CliDireccion" HeaderText="Direccion" Index="16" AllowGroupBy="False"  AllowFilter="False"></cc1:Column>
                                        </Columns>
                                        <ScrollingSettings ScrollWidth="870" />
                                        <FilteringSettings FilterLinksPosition="Top" FilterPosition="Top" />
                                    </cc1:Grid>

                                    <script type="text/javascript">
                                        oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
                                        oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
                                            this._resizeColumn(columnIndex, amountToResize, false);

                                            var width = this._getColumnWidth();

                                            if (width > 0) {
                                                if (this.ScrollWidth == '0px') {
                                                    this.GridMainContainer.style.width = width + 'px';
                                                } else {
                                                    var scrollWidth = parseInt(this.ScrollWidth);
                                                    if (width < scrollWidth) {
                                                        this.GridMainContainer.style.width = width + 'px';
                                                        this.HorizontalScroller.style.display = 'none';
                                                    } else {
                                                        this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                                                        this.HorizontalScroller.style.display = '';
                                                    }
                                                }
                                            }
                                        }

                                        oboutGrid.prototype._getColumnWidth = function () {
                                            var totalWidth = 0;
                                            for (var i = 0; i < this.ColumnsCollection.length; i++) {
                                                if (this.ColumnsCollection[i].Visible) {
                                                    totalWidth += this.ColumnsCollection[i].Width;
                                                }
                                            }

                                            return totalWidth;
                                        }
                                    </script>

                                </div>  
                                    </td>
                                </tr>
                                </table>
                                
                            </td>
                        </tr>
                        </table>        
                </td>
    </tr>    
    

</table>
</ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
