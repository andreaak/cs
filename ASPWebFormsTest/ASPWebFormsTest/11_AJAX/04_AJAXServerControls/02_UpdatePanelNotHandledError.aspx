<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_UpdatePanelNotHandledError.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._02_UpdatePanelNotHandledError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpdatePanel ExceptionSample #1</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button Text="Обновить" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
