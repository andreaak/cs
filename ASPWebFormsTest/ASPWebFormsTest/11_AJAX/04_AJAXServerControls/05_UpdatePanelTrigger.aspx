<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_UpdatePanelTrigger.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._05_UpdatePanelTrigger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Triggers in UpdatePanel</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server" />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%=DateTime.Now.ToLongTimeString() %>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%=DateTime.Now.ToLongTimeString() %>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%=DateTime.Now.ToLongTimeString() %>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button3" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button1" Text="Обновить 1" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="Button2" Text="Обновить 2" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="Button3" Text="Обновить 3" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
