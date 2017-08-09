<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_LifeCycle.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._01_LifeCycle" %>

<!--Регистрация пользовательского элемента управления-->
<%@ Register TagName="_01_UserControl" TagPrefix="test" Src="01_UserControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!--Создание пользовательского элемента управления-->
            <test:_01_UserControl runat="server" ID="WebUserControl1" />
        </div>
    </form>
    <br />
    <asp:Label ID="Output" runat="server" EnableViewState="False" />
</body>
</html>
