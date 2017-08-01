<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_MultiView.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls._03_MultiView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MultiView Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View runat="server">
                    Admin Controls
                </asp:View>
                <asp:View runat="server">
                    Moderator Controls
                </asp:View>
                <asp:View runat="server">
                    SimpleUser Controls
                </asp:View>
            </asp:MultiView>
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" Text="1 View" runat="server" OnClick="Button1_Click" />
            <asp:Button ID="Button2" Text="2 View" runat="server" OnClick="Button2_Click" />
            <asp:Button ID="Button3" Text="3 View" runat="server" OnClick="Button3_Click" />
        </div>
    </form>
</body>
</html>
