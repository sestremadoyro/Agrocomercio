<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpresionGuiaRemision.aspx.cs" Inherits="AgrocomercioWEB.Ventas.ImpresionGuiaRemision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table>
    <tr><td>
    <%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" %>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    </CR:CrystalReportSource>
        </td>
    </tr>
    
    </table>
    </div>
    </form>
</body>
</html>
