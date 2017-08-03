<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_RepeaterWithTable.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._08_Repeater._04_RepeaterSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Repeater Sample #4</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <%--Заголовок таблицы--%>
                    <table border="1" cellpadding="4">
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Phone</th>
                            <th>Email</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <%--Содержимое таблицы--%>
                    <tr>
                        <td><%#Eval("FirstName") %></td>
                        <td><%#Eval("LastName") %></td>
                        <td><%#Eval("PhoneNumber") %></td>
                        <td><%#Eval("Email") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
