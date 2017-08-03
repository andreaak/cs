<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_RepeaterTemplates.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._08_Repeater._03_RepeaterTemplates" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RepeaterSample #3</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">

                <%--Шаблон заголовка (используется один раз)--%>
                <HeaderTemplate>
                    <h1>Список пользователей</h1>
                </HeaderTemplate>

                <%--Шаблон четных элементов списка--%>
                <AlternatingItemTemplate>
                    <div style="background-color: LightGreen">
                        <%# Eval("FirstName") %> <%# Eval("LastName") %>
                        <br />
                        Phone: <%#Eval("PhoneNumber") %><br />
                        Email: <%#Eval("Email") %>
                    </div>
                </AlternatingItemTemplate>

                <%--Шаблон разделителя элементов списка--%>
                <SeparatorTemplate>
                    <hr />
                </SeparatorTemplate>

                <%--Шаблон нечетных элементов списка--%>
                <ItemTemplate>
                    <%# Eval("FirstName") %> <%# Eval("LastName") %>
                    <br />
                    Phone: <%#Eval("PhoneNumber") %><br />
                    Email: <%#Eval("Email") %>
                </ItemTemplate>

                <%--Шаблон подписи Repeater (используется один раз)--%>
                <FooterTemplate>
                    <br />
                    <br />
                    <br />
                    <small><a href="mailto:admin@example.com">Связь с администратором</a></small>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
