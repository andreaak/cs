<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_ServerControlInheritanceTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._06_ServerControlInheritanceTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom Panel</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <cc1:_06_ServerControlRenderContents ID="MyPanel1" runat="server" BackColor="Green" ForeColor="White" Font-Bold="true">
                <asp:Label runat="server">Test</asp:Label>
            </cc1:_06_ServerControlRenderContents>

        </div>
    </form>
</body>
</html>
