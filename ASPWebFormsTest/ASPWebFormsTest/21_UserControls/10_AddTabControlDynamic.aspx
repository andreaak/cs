<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="10_AddTabControlDynamic.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._10_AddTabControlDynamic" %>
<%@ Reference Control="Controls/09_TabControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder ID="ControlPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
