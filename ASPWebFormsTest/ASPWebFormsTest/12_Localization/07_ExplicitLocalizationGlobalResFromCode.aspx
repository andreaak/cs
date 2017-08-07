<!--Явная локализация с помощью локальных файлов-->

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_ExplicitLocalizationGlobalResFromCode.aspx.cs"
    Inherits="ASPWebFormsTest._12_Localization._07_ExplicitLocalizationGlobalResFromCode"
    UICulture="en-US" %>

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
                        <asp:Label ID="LabelLogin" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxLogin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelPassword" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonLogin" runat="server" />
                    </td>
                </tr>
            </table>
        </div>

        <br />
        <div>
            <asp:Label ID="Label1" runat="server" />
            <br />
            <asp:Label ID="Label2" runat="server" />
        </div>
    </form>
</body>
</html>
