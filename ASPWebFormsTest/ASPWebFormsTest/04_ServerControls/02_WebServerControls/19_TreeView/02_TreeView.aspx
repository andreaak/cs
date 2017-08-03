<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_TreeView.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._19_TreeView._02_TreeView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TreeView пример использования</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <Nodes>
                    <asp:TreeNode Text="Поисковики">
                        <asp:TreeNode Text="Google" NavigateUrl="http://google.com"></asp:TreeNode>
                        <asp:TreeNode Text="Yandex" NavigateUrl="http://yandex.ru"></asp:TreeNode>
                        <asp:TreeNode Text="Yahoo" NavigateUrl="http://yahoo.com"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Регистраторы">
                        <asp:TreeNode Text="Name.com" NavigateUrl="http://name.com"></asp:TreeNode>
                        <asp:TreeNode Text="Godaddy" NavigateUrl="http://godaddy.com"></asp:TreeNode>
                        <asp:TreeNode Text="Имена UA" NavigateUrl="http://imena.ua"></asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
