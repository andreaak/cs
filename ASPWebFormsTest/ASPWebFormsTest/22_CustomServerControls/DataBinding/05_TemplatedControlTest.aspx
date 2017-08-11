<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_TemplatedControlTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls.DataBinding.__05_TemplatedControlTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls.DataBinding" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Templated Control</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <cc1:_05_TemplatedControl ID="MessageBlock1" runat="server" Title="This is Title" MessageBody="Hello world!!! This is test text." Author="admin">

                <%--
                Задаем шаблон для заголовка, инициализируется свойство HeaderTemplate, 
                класса MessageBlock
                --%>
                <HeaderTemplate>
                    <div style="padding: 8px; margin: 8px; border: 2px solid black;">
                        <asp:Label ID="Label2" Text="<%# Container.Title %>" runat="server" />
                        <br />
                        <asp:Label ID="Label1" Text="<%# Container.Author %>" runat="server" />
                    </div>
                </HeaderTemplate>

                <%--
                Задаем шаблон для сообщения, инициализируется свойство MessageTemplate, 
                класса MessageBlock
                --%>
                <MessageTemplate>
                    <b style="text-transform: uppercase; margin: 8px;">
                        <%# Container.MessageBody %></b>
                </MessageTemplate>

            </cc1:_05_TemplatedControl>
        </div>
    </form>
</body>
</html>
