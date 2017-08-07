<!--Неявная локализация с помощью локальных файлов-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ImplicitLocalizationChangeLanguage.aspx.cs"
    Inherits="ASPWebFormsTest._12_Localization._03_ImplicitLocalizationChangeLanguage" Culture="auto" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="LanguageList">
                <asp:ListItem Text="English" Value="en-US" />
                <asp:ListItem Text="Russian" Value="ru-RU" />
            </asp:DropDownList>
        </div>
        <div>
            <table cellpadding="3" cellspacing="4">
                <tr>
                    <td>
                        <asp:Label ID="LabelLogin" runat="server" meta:resourcekey="LabelLogin"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxLogin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelPassword" runat="server" meta:resourcekey="LabelPassword"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonLogin" runat="server" meta:resourcekey="ButtonLogin" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
