﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_Form.aspx.cs" Inherits="ASPWebFormsTest._01_BaseInfo._08_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%--Форма для серверных элементов управления--%>
    <%--Все серверные элементы управления должны находиться в элементе form с атрибутом runat="server"--%>
    <%--Серверные элементы начиняются с префикса <asp:--%>
    <form id="form1" runat="server">
        <div>
            <p>
                Введите имя
                <%--При создании серверного элемента управления 
                    обязательно нужно задать два атрибута ID и runat="server"--%>
                <%--TextBox - поле ввода--%>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </p>
            <p>
                <%--Button - кнопка. OnClick="OkButton_Click" - задание обработчика нажатия по кнопке. 
                    OkButton_Click - имя метода, который находиться в файле кода.--%>
                <asp:Button ID="Button1" runat="server" Text="Ok" OnClick="OkButton_Click" />
            </p>
            <p>
                <%--Label - метка для вывода текста на страницу--%>
                <asp:Label ID="Label1" runat="server" />
            </p>
        </div>
    </form>
    
    <div>
        <%--Серверный элемент управления для вывода текста--%>
        <asp:Label ID="Label3" runat="server"/>
        <%--Cерверные элементы управления должны находиться в элементе form--%>
        <%--asp:TextBox ID="TextBox2" runat="server"></asp:TextBox--%>
    </div>
</body>
</html>
