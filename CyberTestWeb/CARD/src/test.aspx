<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CyberTestWeb.CARD.src.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%:Styles.Render("~/bundles/css") %>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Label Text="我愛夏天" runat="server" CssClass="Label_char"></asp:Label>
        <br />
         <asp:Label Text="我愛夏天" runat="server" ></asp:Label>
         <br />
        <asp:Label Text="" runat="server" ID="lblTest" ></asp:Label>
         <br />
         SessionID:
        <%= Session.SessionID %>
    </div>
    </form>
</body>
</html>
