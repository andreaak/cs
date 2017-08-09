<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_UserControlViewStateTest.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._05_UserControlViewStateTest" %>

<%@ Register Src="05_UserControlSetProperty.ascx" TagName="TestControl1" TagPrefix="uc1" %>
<%@ Register Src="05_UserControlViewState.ascx" TagName="TestControl2" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UserControl Properties and ViewState</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <uc1:TestControl1 ID="Control1" runat="server" />
            <uc2:TestControl2 ID="Control2" runat="server" />

            <asp:Button Text="Reload" runat="server" />
        </div>
    </form>
</body>
</html>
