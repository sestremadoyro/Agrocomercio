<%@ Page Title=".:Reportes de Compras:." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmReportesCompras.aspx.cs" Inherits="AgrocomercioWEB.Compras.wfrmReportesCompras" 
%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
   
    function AbrirVentanaProveedoresMes() {
        window.open('Reportes/wfrmVistaRepProveedoresMes.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
    }
    function AbrirVentanaProveedoresGeneral() {
        window.open('Reportes/wfrmVistaRepProveedoresGeneral.aspx', '_blank', 'width=880px,height=600px,scrollbars=si,menubar=no,resizable=no,left=200px, top=70px');
    }
    
    function setPrvCod(source, eventargs) {
        document.getElementById('lblEstado').value = "PRV_SELECT";
        __doPostBack('MainUpdatePanel', eventargs.get_value());
    }
 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdateProgress id="updateProgress" runat="server" 
                AssociatedUpdatePanelID="MainUpdatePanel"  dynamiclayout="true">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 8888888; background-color: #ffffff; opacity: 0.7; filter: alpha(opacity=70);">
            &nbsp;
        </div>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; ">
            <div style="padding: 10px;position:relative;top:33%; left:45%; height: 100px; width: 150px;">
                <asp:Image ID="imgUpdateProgress" runat="server" 
                ImageUrl="../images/ajax-loader.gif" AlternateText="Procesando ..." 
                ToolTip="Trabajando ..." 
                style="height: 80px; width: 80px;" />
                <span style="padding: 10px; font-size:13px; font-weight:bold; color:#555555; " >Procesando ... </span>
            </div>
        </div>
        
    </ProgressTemplate>
</asp:UpdateProgress>        
<asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
<ContentTemplate>
<table style="width: 800px; margin:0 auto;" >
    <tr>
        <td colspan="2"> <h2 class="clsTituloInterior">REPORTES DE COMPRAS DE AGROCOMERCIO S.R.L.</h2>
            &nbsp;<br />    
            
        </td>
    </tr>
    <tr>
            <td colspan="2">
                <table class="tabPedidos">
              
                <tr>
                    <td class="tabIzquierda" valign="top">
                    <asp:panel id="pnListOperCompras" runat="server">
                        <table class="tableIzquierda">
                        <tr>
                            <td class="clsCellTituloDatos2" >
                                <asp:Label ID="Label1" runat="server" Text="Eliga un Reporte: "></asp:Label>
                            </td>
                            <td class="clsCellDatos2" >
                                <asp:DropDownList ID="ddlReportes" runat="server" Width="250px" 
                                    AutoPostBack="True" onselectedindexchanged="ddlReportes_SelectedIndexChanged">
                                    <asp:ListItem Value="PG">Proveedores General</asp:ListItem>
                                    <asp:ListItem Value="CPM">Compras Proveedor Por Mes</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />   
                            </td>                            
                        </tr>   
                        <tr>
                            <td colspan="4" >
                                &nbsp; &nbsp;<br />&nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td  colspan="4" >
                                <h3><asp:Label ID="lblSubTitulo" runat="server" Text="Label">Reportes de Compras</asp:Label></h3>
                            </td>
                        </tr> 
                        <tr>
                            <td colspan="4" >
                                &nbsp; &nbsp;</td>
                        </tr>  
                        <tr>
                            <td class="clsCellTituloDatos2" >
                                <asp:Label ID="Label4" runat="server" Text="Proveedores: "></asp:Label>
                            </td>
                            <td class="clsCellDatos2" >
                                <asp:DropDownList ID="ddlProveedor" runat="server" Width="250px" 
                                    AutoPostBack="True" DataTextField="PrvRazon" DataValueField="PrvCod" 
                                    onselectedindexchanged="ddlProveedor_SelectedIndexChanged" style=" display:none;" >
                                </asp:DropDownList>
                                <asp:TextBox ID="txtProveedor" runat="server" Width="270px" ></asp:TextBox>
                                <asp:TextBoxWatermarkExtender 
	                                ID="txtProveedor_TextBoxWatermarkExtender" runat="server"
	                                TargetControlID="txtProveedor" WatermarkCssClass = "clsWaterMark" 
	                                WatermarkText="Busqueda de Proveedores..." Enabled="true">
                                </asp:TextBoxWatermarkExtender>
                                <div id="PrvlistPlacement" class="cls_listPlacement" style=" overflow:auto; "></div>
                                <asp:AutoCompleteExtender ID="txtProveedor_AutoCompleteExtender"
	                                MinimumPrefixLength="2" TargetControlID="txtProveedor" 
                                    enablecaching="true" 
                                    showonlycurrentwordincompletionlistitem="true" 
	                                CompletionSetCount="10" CompletionInterval="100" 
	                                ServiceMethod="BuscarProveedores" 
	                                runat="server" OnClientItemSelected="setPrvCod" 
	                                CompletionListElementID="PrvlistPlacement">
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
                                <asp:CheckBox ID="chkPorMes" runat="server" Text="Por Mes:  " 
                                    AutoPostBack="True" oncheckedchanged="chkPorMes_CheckedChanged" />
                               
                            </td>
                            <td class="clsCellDatos2" >
                                <div id="divFechas" runat="server">
                                    <asp:Label ID="Label2" runat="server" Text="Desde "></asp:Label>
                                    <asp:TextBox ID="txtFecIni" runat="server"  Width="120px" AutoPostBack="True"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender_txtFecIni" runat="server" 
                                        TargetControlID="txtFecIni" Format="yyyy-MM-dd"
                                        PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                    <asp:Label ID="Label3" runat="server" Text=" Hasta "></asp:Label>
                                    <asp:TextBox ID="txtFecFin" runat="server"  Width="120px" AutoPostBack="True"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender_txtFecFin" runat="server" 
                                        TargetControlID="txtFecFin" Format="yyyy-MM-dd"
                                        PopupButtonID="Image2" Enabled="True" ></asp:CalendarExtender>
                                </div>
                                <asp:DropDownList ID="ddlMeses" runat="server" Width="250px" 
                                    AutoPostBack="True" >
                                </asp:DropDownList>

                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" >
                                      &nbsp; &nbsp;
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
                    <td class="tabDerecha" valign="top">
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
                        </table>        
                </td>
    </tr>    


</table>
</ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
