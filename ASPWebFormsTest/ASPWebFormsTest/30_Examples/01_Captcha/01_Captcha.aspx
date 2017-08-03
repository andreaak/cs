<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_Captcha.aspx.cs" Inherits="ASPWebFormsTest._30_Examples._01_Captcha._01_Captcha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        CAPTCHA - Completely Automated Public Turing test to tell Computers and Humans Apart - 
        полностью автоматизированный публичный тест Тьюринга для различения компьютеров и людей.
    </p>
    <form id="form1" runat="server">
        <div>

            <img src="01_CaptchaHandler.ashx" alt="CAPTCHA" />

            <br />
            Введите текст с картинки
        <asp:TextBox ID="CaptchaTextBox" runat="server"></asp:TextBox><br />

            <asp:Button ID="OkButton" Text="Ok" runat="server" OnClick="OkButton_Click" /><br />

            <asp:Label ID="ResultLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
