﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmVistaRepProveedoresGeneral.aspx.cs" Inherits="AgrocomercioWEB.Reportes.wfrmVistaRepProveedoresGeneral" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
     <table>
    <tr><td>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" HasToggleGroupTreeButton="False" 
            HasToggleParameterPanelButton="False" ToolPanelView="None" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
    </CR:CrystalReportSource>
    
        </td>
    </tr>
    
    </table>
    </div>
    </form>
</body>
</html>
