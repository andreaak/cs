<!--Явная локализация с помощью локальных файлов-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_ExplicitLocalizationLocalRes.aspx.cs"
    Inherits="ASPWebFormsTest._12_Localization._06_ExplicitLocalizationLocalRes"
    UICulture="ru-RU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="3" cellspacing="4">
                <tr>
                    <td>
                        <asp:Label ID="LabelLogin" runat="server" Text="<%$ Resources:LoginText %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxLogin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelPassword" runat="server" Text="<%$ Resources:PassText %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonLogin" runat="server" Text="<%$ Resources:ButtonEnter %>" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
