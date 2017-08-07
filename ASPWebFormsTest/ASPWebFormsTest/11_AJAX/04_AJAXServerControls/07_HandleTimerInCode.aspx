<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_HandleTimerInCode.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._07_HandleTimerInCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Timer Sample #2</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer_Tick" Interval="1000">
                </asp:Timer>
                <asp:Label Text="0" ID="Label1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
