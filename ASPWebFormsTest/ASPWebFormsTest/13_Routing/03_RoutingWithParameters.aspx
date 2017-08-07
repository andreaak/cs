<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_RoutingWithParameters.aspx.cs"
    Inherits="ASPWebFormsTest._13_Routing._03_RoutingWithParameters" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="/calc/sum/10/20">10 + 20</a><br />
            <a href="/calc/sub/10/3">10 - 3</a><br />
            <a href="/calc/mul/10/2">10 * 2</a><br />
            <a href="/calc/div/10/5">10 / 5</a><br />
        </div>
        <div>
            <asp:HyperLink
                ID="SumLink"
                runat="server"
                Text="10 + 20"
                NavigateUrl="<%$RouteUrl:operation=sum, x=10, y=20, routename=Calculator %>">
            </asp:HyperLink>
            <br />
            <asp:HyperLink
                ID="SubLink"
                runat="server"
                Text="10 - 2"
                NavigateUrl="<%$RouteUrl:operation=sub, x=10, y=2, routename=Calculator %>">
            </asp:HyperLink>
            <br />
        </div>
    </form>
</body>
</html>
