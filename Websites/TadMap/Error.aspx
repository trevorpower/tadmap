<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>tadmap - Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            <asp:Label runat="server" ID="Heading">Internal Server Error</asp:Label>
        </h1>
        <p>
            <asp:Label runat="server" ID="Info"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
