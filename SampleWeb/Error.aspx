<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="SampleWeb.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" title="BRAD BROKE IT!!!">
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" 
        Text="There has been an error!"></asp:Label>
    <p>
        Somebody is responsible....</p>
    <asp:Label ID="ui_ErrorText" runat="server"></asp:Label>
    </form>
</body>
</html>
