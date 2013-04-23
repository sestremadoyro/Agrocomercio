<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmTipoNotas.aspx.cs" Inherits="AgrocomercioWEB.Maestros.WebForms.wfrmTipoNotas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../App_Themes/TemaAgrocomercio/ventas.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function dgvDetalleVentaClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVCOM_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }
        function dgvListNotasClickEvent(event, rowindex) {

            document.getElementById('lblEstado').value = "DGVLIS_" + event;
            __doPostBack('MainUpdatePanel', rowindex);
        }

        function AbrirVentana() {
            window.open('', '', 'width=200,height=100')
        }

        function setArtCod(source, eventargs) {
            document.getElementById('lblEstado').value = "ART_SELECT";
            __doPostBack('MainUpdatePanel', eventargs.get_value());
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="MainUpdatePanel" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table style="width: 691px; margin: 0 auto;">
                <tr>
                    <td colspan="2" class="style5">
                        <h2 class="clsTituloInterior">
                            TIPOS DE NOTAS DE AGROCOMERCIO S.R.L.</h2>
                        <asp:HiddenField ID="lblEstado" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbid_nota" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="lbmomento" runat="server" />
                        <asp:HiddenField ID="HiddenField4" runat="server" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style5">
                     
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" 
                            DataKeyNames="AtrTipoCod,AtrCodigo" DataSourceID="sqldsTipoNota">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="AtrTipoCod" HeaderText="AtrTipoCod" ReadOnly="True" 
                                    SortExpression="AtrTipoCod" Visible =false />
                                <asp:BoundField DataField="AtrNivel" HeaderText="AtrNivel" 
                                    SortExpression="AtrNivel" Visible =false />
                                <asp:BoundField DataField="AtrCodigo" HeaderText="Codigo" ReadOnly="True" 
                                    SortExpression="AtrCodigo" />
                                <asp:BoundField DataField="AtrDescripcion" HeaderText="Descripcion" 
                                    SortExpression="AtrDescripcion" />
                                <asp:CheckBoxField DataField="AtrEstado" HeaderText="Estado" 
                                    SortExpression="AtrEstado" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqldsTipoNota" runat="server" 
                            ConflictDetection="CompareAllValues" 
                            ConnectionString="<%$ ConnectionStrings:AgrocomercioConnectionString %>" 
                            DeleteCommand="DELETE FROM [Atributos] WHERE [AtrTipoCod] = @original_AtrTipoCod AND [AtrCodigo] = @original_AtrCodigo AND (([AtrNivel] = @original_AtrNivel) OR ([AtrNivel] IS NULL AND @original_AtrNivel IS NULL)) AND (([AtrDescripcion] = @original_AtrDescripcion) OR ([AtrDescripcion] IS NULL AND @original_AtrDescripcion IS NULL)) AND (([AtrEstado] = @original_AtrEstado) OR ([AtrEstado] IS NULL AND @original_AtrEstado IS NULL))" 
                            InsertCommand="INSERT INTO [Atributos] ([AtrTipoCod], [AtrNivel], [AtrCodigo], [AtrDescripcion], [AtrEstado]) VALUES (@AtrTipoCod, @AtrNivel, @AtrCodigo, @AtrDescripcion, @AtrEstado)" 
                            OldValuesParameterFormatString="original_{0}" 
                            SelectCommand="SELECT * FROM [Atributos] WHERE (([AtrTipoCod] = @AtrTipoCod) AND ([AtrCodigo] &lt;&gt; @AtrCodigo))" 
                            UpdateCommand="UPDATE [Atributos] SET [AtrNivel] = @AtrNivel, [AtrDescripcion] = @AtrDescripcion, [AtrEstado] = @AtrEstado WHERE [AtrTipoCod] = @original_AtrTipoCod AND [AtrCodigo] = @original_AtrCodigo AND (([AtrNivel] = @original_AtrNivel) OR ([AtrNivel] IS NULL AND @original_AtrNivel IS NULL)) AND (([AtrDescripcion] = @original_AtrDescripcion) OR ([AtrDescripcion] IS NULL AND @original_AtrDescripcion IS NULL)) AND (([AtrEstado] = @original_AtrEstado) OR ([AtrEstado] IS NULL AND @original_AtrEstado IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_AtrTipoCod" Type="Int32" />
                                <asp:Parameter Name="original_AtrCodigo" Type="String" />
                                <asp:Parameter Name="original_AtrNivel" Type="Int32" />
                                <asp:Parameter Name="original_AtrDescripcion" Type="String" />
                                <asp:Parameter Name="original_AtrEstado" Type="Boolean" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="AtrTipoCod" Type="Int32" />
                                <asp:Parameter Name="AtrNivel" Type="Int32" />
                                <asp:Parameter Name="AtrCodigo" Type="String" />
                                <asp:Parameter Name="AtrDescripcion" Type="String" />
                                <asp:Parameter Name="AtrEstado" Type="Boolean" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:Parameter DefaultValue="9" Name="AtrTipoCod" Type="Int32" />
                                <asp:Parameter DefaultValue="*" Name="AtrCodigo" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="AtrNivel" Type="Int32" />
                                <asp:Parameter Name="AtrDescripcion" Type="String" />
                                <asp:Parameter Name="AtrEstado" Type="Boolean" />
                                <asp:Parameter Name="original_AtrTipoCod" Type="Int32" />
                                <asp:Parameter Name="original_AtrCodigo" Type="String" />
                                <asp:Parameter Name="original_AtrNivel" Type="Int32" />
                                <asp:Parameter Name="original_AtrDescripcion" Type="String" />
                                <asp:Parameter Name="original_AtrEstado" Type="Boolean" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                     
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
