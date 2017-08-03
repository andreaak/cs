<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_RepeaterBindObjects.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._08_Repeater._02_RepeaterBindObjects" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Repeater Sample #2</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <%# Eval("FirstName") %>&nbsp;<%# Eval("LastName") %>
                    <br />
                    Phone: <%#Eval("PhoneNumber") %><br />
                    Email: <%#Eval("Email") %>
                    <hr />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
