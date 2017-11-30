<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Baza.aspx.cs" Inherits="RESTService.Baza" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SlivkoDatabaseConnectionString %>" SelectCommand="SELECT * FROM [Persons]"></asp:SqlDataSource>
    </form>
</body>
</html>
